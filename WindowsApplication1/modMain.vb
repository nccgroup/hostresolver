Option Explicit On

Imports System.Net
Imports System.Net.Dns
Imports System.Text.RegularExpressions

Module modMain

    '== Current list of hosts ==
    Public arlHostList As New ArrayList

    '== Input and output files ==
    Public strInputFile As String = ""
    Public strOutputFile As String = ""

    '== Determines whether to resolve hostnames or IP addresses ==
    Public blnGetIP As Boolean = True

    '== Include Input details in output file ==
    Public blnIncludeInput As Boolean = True

    '== Do we want normal WhoIs query or a test against name servers? ==
    Public blnTestNameServers As Boolean = False


    Public Class clsIP

        Public Function GetLocalHostIP() As String
            ' Return IP of localhost
            '=======================
            Dim objAddress As IPAddress
            Dim strResult As String


            Try
                objAddress = New IPAddress(GetHostByName(GetLocalHostName).AddressList(0).Address)
                strResult = objAddress.ToString
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                strResult = "Error obtaining local IP address."
            End Try

            Return strResult

        End Function

        Public Function GetLocalHostName() As String
            Return GetHostName()
        End Function

        Public Function IPToHostName(ByVal IPAddress As String) As String
            ' Resolve IP address into hostname
            '=================================
            Dim objEntry As IPHostEntry
            Dim strResult As String


            If Not Regex.IsMatch(IPAddress, "^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$") Then
                strResult = "Not a valid IP address."
            Else
                Try
                    objEntry = GetHostEntry(IPAddress)
                    strResult = objEntry.HostName
                Catch ex As Exception
                    strResult = "IP not found."
                End Try
            End If



            Return strResult

        End Function

        Public Function HostNameToIP(ByVal Host As String) As String
            ' Resolve hostname to IP address
            '===============================
            Dim objAddress As IPAddress
            Dim strResult As String


            Try
                objAddress = New IPAddress(GetHostEntry(Host).AddressList(0).Address)
                strResult = objAddress.ToString
            Catch
                strResult = "Not resolved."
            End Try

            Return strResult

        End Function

    End Class

    Public Function CheckDomainName(ByVal DomainName As String) As Boolean
        ' Return true if the value is a correctly formatted domain name
        '==============================================================
        Dim blnRetVal As Boolean = False

        If Regex.IsMatch(DomainName, "^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}$") Then blnRetVal = True

        Return blnRetVal

    End Function

    Public Function CheckIPAddress(ByVal IpAddress As String) As Boolean
        ' Return true if the value is a correctly formatted IP address
        '=============================================================
        Dim blnRetVal As Boolean = False

        If Regex.IsMatch(IpAddress, "^(?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(?(\.?\d)\.)){4}$") Then blnRetVal = True

        Return blnRetVal

    End Function

    Public Function GetFirstItem(ByVal ListString As String, Optional ByVal Separator As String = " ") As String
        'Split string on specified character (default: space) and return last item
        '=========================================================================
        Dim strRetVal As String = ""
        Dim arrStrings As String()

        ListString = ListString.Trim()

        Select Case Separator
            Case " "
                ' This regex prevents a split on space from returning empty strings
                arrStrings = Regex.Split(ListString, "\s+")
            Case Else
                arrStrings = ListString.Split(Separator)
        End Select

        ' Return first item
        strRetVal = arrStrings.First.Trim

        Return strRetVal

    End Function

    Public Function GetLastItem(ByVal ListString As String, Optional ByVal Separator As String = " ") As String
        'Split string on specified character (default: space) and return last item
        '=========================================================================
        Dim strRetVal As String = ""
        Dim arrStrings As String()

        ListString = ListString.Trim()

        Select Case Separator
            Case " "
                ' This regex prevents a split on space from returning empty strings
                arrStrings = Regex.Split(ListString, "\s+")
            Case Else
                arrStrings = ListString.Split(Separator)
        End Select

        ' Return final item
        strRetVal = arrStrings.Last.Trim

        Return strRetVal

    End Function

End Module
