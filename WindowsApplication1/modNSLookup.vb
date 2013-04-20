Option Explicit On

Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Net.Sockets


Module modNSLookup

    Private htWhois As New Hashtable()

    Private Const FULL_INFO As Integer = 0
    Private Const NAME_SERVER As Integer = 1


    Public Function RunNSLookup(ByRef Parameters As String) As String
        'Run nslookup and extract returned data
        '======================================
        Dim Proc As New Process
        Dim strRetVal As String = ""


        With Proc.StartInfo
            .Arguments = Parameters
            .UseShellExecute = False
            .RedirectStandardError = True
            .FileName = "nslookup.exe"
            .CreateNoWindow = True
            .RedirectStandardOutput = True
        End With


        Try
            Proc.Start()
            If Proc.WaitForExit(8000) Then
                '== Obtain output ==
                strRetVal = Proc.StandardOutput.ReadToEnd
            Else
                strRetVal = "NSLookup timed out..."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

        Return strRetVal

    End Function

    Private Function LoadWhoisServers(ByVal FileName As String) As Hashtable

        Dim arrTempString() As String
        Dim htWhoIsTable As New Hashtable()


        Try
            Dim srStreamReader As StreamReader = IO.File.OpenText(FileName)

            Do While srStreamReader.Peek() >= 0
                arrTempString = srStreamReader.ReadLine().Split("|")
                If (arrTempString.GetLength(0) = 6 And arrTempString(0).StartsWith("#") = False) Then
                    htWhoIsTable.Add(arrTempString(0), arrTempString(1))
                End If
            Loop

            srStreamReader.Close()

        Catch e As Exception
            MsgBox(e.Message + vbLf + "Fatal error - the application has been forced to close.", MsgBoxStyle.Critical, "Fatal Error.")
            End
        End Try

        Return htWhoIsTable

    End Function

    Public Sub LoadWhoisHash()
        htWhois = LoadWhoisServers(Application.StartupPath & "\servers.lst")
    End Sub

    Public Function DomainLookUp(ByVal DomainName As String) As String

        Dim strWhoIsServer As String = ""
        Dim strNameFragment As String
        Dim strResult As String = ""
        Dim strServerInfo As String = ""
        Dim strRetVal As String = ""
        Dim arrFragments As String()


        DomainName = DomainName.Trim

        If (CheckIPAddress(DomainName)) Then
            strWhoIsServer = "whois.arin.net"
        ElseIf (CheckDomainName(DomainName)) Then
            arrFragments = DomainName.Split(".")
            If arrFragments.Count < 3 Then Return ("Not a valid domain name or IP address: " & DomainName)

            strNameFragment = arrFragments.Last
            strWhoIsServer = htWhois.Item(strNameFragment)
            If strWhoIsServer Is Nothing Then
                strNameFragment = arrFragments(arrFragments.Length - 2) & "." & strNameFragment
                strWhoIsServer = htWhois.Item(strNameFragment)
                If strWhoIsServer Is Nothing Then Return ("Unable to identify WhoIs server for domain name: " & DomainName)
            End If

            '== Ensure domain name is valid for ARIN requirements (i.e. it has 3 parts *only*) ==
            If arrFragments.Count > 3 Then DomainName = arrFragments(arrFragments.Length - 3) & "." & strNameFragment
        Else
            Return ("Not a valid domain name or IP address: " & DomainName)
        End If

        If strWhoIsServer.Trim <> "" Then strResult = DoWhoisQuery(DomainName, strWhoIsServer)

        strRetVal += "[" + strWhoIsServer + "]" + vbLf
        strRetVal += strResult + vbLf

        '== Check for a redirect whois server ==
        strServerInfo = GetServerInfo(strResult)
        If (strServerInfo <> "") Then
            strResult = DoWhoisQuery(DomainName, strServerInfo)
            strRetVal += "[" + strServerInfo + "]" + vbLf
            strRetVal += strResult
        End If

        Return strRetVal

    End Function

    Private Function GetServerInfo(ByVal QueryText As String, Optional ByVal QueryType As String = "Whois Server:\s+(.+)") As String
        ' Locate any WhoIs info in the returned expression and extract useful data
        '=========================================================================
        Dim strRetVal As String = ""
        Dim remMatch As RegularExpressions.Match


        '== Locate server info ==
        remMatch = Regex.Match(QueryText, QueryType, RegularExpressions.RegexOptions.IgnoreCase)

        '== Extract the required data ==
        If (remMatch.Value <> "") Then strRetVal = remMatch.Value.Substring(Len(QueryType))

        Return strRetVal.Trim

    End Function

    Private Function DoWhoisQuery(ByVal DomainName As String, ByVal ServerName As String) As String

        Dim strRetVal As String = ""
        Dim tcTcpClient As New TcpClient()
        Dim bytSendBytes As [Byte]()
        Dim intRecvSize As Int32


        Try
            tcTcpClient.Connect(ServerName, 43)

            Dim nsNetworkStream As NetworkStream = tcTcpClient.GetStream()

            If nsNetworkStream.CanWrite And nsNetworkStream.CanRead Then
                bytSendBytes = Encoding.ASCII.GetBytes(DomainName + vbCrLf)
                nsNetworkStream.Write(bytSendBytes, 0, bytSendBytes.Length)

                Dim bytBuffer(tcTcpClient.ReceiveBufferSize) As Byte

                intRecvSize = nsNetworkStream.Read(bytBuffer, 0, CInt(tcTcpClient.ReceiveBufferSize))

                While (intRecvSize <> 0)
                    strRetVal += Encoding.ASCII.GetString(bytBuffer, 0, intRecvSize)
                    intRecvSize = nsNetworkStream.Read(bytBuffer, 0, CInt(tcTcpClient.ReceiveBufferSize))
                End While


                If strRetVal.Trim <> "" And blnTestNameServers = True Then strRetVal = GetNameServers(strRetVal)

            Else
                If Not nsNetworkStream.CanWrite Then
                    strRetVal = "Cannot write data to this stream."
                Else
                    If Not nsNetworkStream.CanRead Then
                        strRetVal = "Cannot read data from this stream."
                    End If
                End If
            End If

        Catch
            strRetVal = "Failed to get data from WhoIs server."
        Finally
            tcTcpClient.Close()
        End Try

        Return strRetVal

    End Function

    Public Function GetNameServers(ByVal WhoIsData As String) As String
        ' Extract nameserver names from WhoIs info and return as formatted text
        '======================================================================
        Dim strRetVal As String = vbNewLine & "Name Servers: " & vbNewLine
        Dim arlServerNames As New ArrayList


        '== Extract the servernames from the string ==
        arlServerNames = GetNameServerArray(WhoIsData)

        If arlServerNames.Count > 0 Then

            '== Write to output ==
            For Each strServerName In arlServerNames
                strRetVal += strServerName & vbNewLine
            Next

            '== Test for DNS recursion ==
            strRetVal += vbNewLine & "---------------------------------------------------------------------------------------------------" & vbNewLine
            strRetVal += "Testing for DNS recursion:" & vbNewLine
            For Each strServerName In arlServerNames
                strRetVal += RunNSLookup(strServerName) & vbNewLine
            Next

            '== Test for zone transfer ==
            strRetVal += vbNewLine & "---------------------------------------------------------------------------------------------------" & vbNewLine
            strRetVal += "Testing for DNS zone transfer:" & vbNewLine
            For Each strServerName In arlServerNames
                strRetVal += RunNSLookup("-d nccgroup.com " & strServerName) & vbNewLine
            Next

        End If

        Return strRetVal

    End Function

    Public Function GetNameServerArray(ByVal WhoIsData As String) As ArrayList
        ' Extract nameserver names from WhoIs info and return array of all nameservers
        '=============================================================================
        Dim arlRetVal As New ArrayList
        Dim arrFragments As String()

        Dim strTemp As String = ""

        Dim intIndex As Integer


        If Regex.IsMatch(WhoIsData, "Name\s*servers\s*:*([^ \r\n]*)", RegularExpressions.RegexOptions.IgnoreCase) Then

            '== Nameservers will be preceded by one instance of "Name servers:" on the first line when listed collectively ==
            arrFragments = Regex.Split(WhoIsData, "Name\s*servers*\s*:")
            strTemp = arrFragments.Last.Trim
            arrFragments = strTemp.Split

            If arrFragments.Count > 1 Then
                For intIndex = 1 To arrFragments.Count - 1
                    strTemp = GetFirstItem(arrFragments(intIndex).Trim)
                    If strTemp.Contains(":") And Not strTemp.Contains("::") Then Exit For
                    If CheckDomainName(strTemp) Then arlRetVal.Add(strTemp)
                Next intIndex
            End If

        ElseIf Regex.IsMatch(WhoIsData, "Name\s*server*\s*:*([^ \r\n]*)", RegularExpressions.RegexOptions.IgnoreCase) Then

            '== Nameservers will each be preceded immediately by "Name server:" when listed individually ==
            arrFragments = Regex.Split(WhoIsData, "Name\s*server*\s*:")

            If arrFragments.Count > 1 Then
                For intIndex = 1 To arrFragments.Count - 1
                    strTemp = GetFirstItem(arrFragments(intIndex).Trim)
                    If CheckDomainName(strTemp) Then arlRetVal.Add(strTemp)
                Next intIndex
            End If
        End If

        Return arlRetVal

    End Function

End Module
