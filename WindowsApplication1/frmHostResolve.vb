Option Explicit On

Imports System.IO
Imports System.Text.RegularExpressions


Public Class frmHostResolve

    Private Sub btnInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInput.Click
        ' Get input file from user
        '=========================

        ofdOpenFileDialog.Filter = "Text Files(*.txt)|*.txt"
        Dim intSuccess As Integer = ofdOpenFileDialog.ShowDialog()

        If intSuccess <> DialogResult.Cancel Then
            strInputFile = ofdOpenFileDialog.FileName.Trim
            txtInput.Text = strInputFile
        End If

    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        ' Get list of hosts from input file and resolve as appropriate
        '=============================================================

        If strInputFile = "" Or strOutputFile = "" Then Exit Sub

        BuildHostList()
        GetIPs(arlHostList)

    End Sub

    Private Sub GetIPs(ByVal HostList As ArrayList)
        ' Take the list of URLs and resolve into list of IPs
        ' Write results to screen and to output file
        '====================================================
        On Error Resume Next

        Dim strResolvedName As String
        Dim IPFuncs As New clsIP

        Dim fsFileStream As New FileStream(strOutputFile, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite, IO.FileShare.None)
        Dim fwFileWriter As New StreamWriter(fsFileStream)


        '== Initialise progress bar ==
        pbProgressBar.Value = 0
        pbProgressBar.Maximum = HostList.Count

        '== Iterate through list and resolve each item ==
        If Not HostList Is Nothing Then
            For Each strHost In HostList

                If blnGetIP Then
                    strResolvedName = IPFuncs.HostNameToIP(strHost)
                Else
                    strResolvedName = IPFuncs.IPToHostName(strHost)
                End If

                '== Show result in listbox ==
                lbOutput.Items.Add(strHost + " => " + strResolvedName)

                '== Write to output file ==
                If blnIncludeInput Then
                    fwFileWriter.WriteLine(strHost & "," & strResolvedName)
                Else
                    fwFileWriter.WriteLine(strResolvedName)
                End If

                '== Increment progress bar ==
                pbProgressBar.Increment(1)
                Me.Refresh()

            Next
        End If

        '== Tidy up ==
        fwFileWriter.Close()
        fsFileStream.Close()
        pbProgressBar.Value = 0

    End Sub

    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click
        ' Show dialog box for new output file
        '====================================

        sfdSaveFileDialog.Filter = "CSV Files(*.csv)|*.csv|Text Files(*.txt)|*.txt"
        Dim intSuccess As Integer = sfdSaveFileDialog.ShowDialog()

        If intSuccess <> DialogResult.Cancel Then
            strOutputFile = sfdSaveFileDialog.FileName
            txtOutput.Text = strOutputFile
        End If

    End Sub

    Private Sub rbHostToIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbHostToIP.CheckedChanged, rbIPToHost.CheckedChanged
        blnGetIP = rbHostToIP.Checked
    End Sub

    Private Sub txtInput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInput.KeyDown
        ' Set new input file name
        '========================

        If e.KeyCode = Keys.Enter Then
            If txtInput.Text.Trim() <> "" Then strInputFile = txtInput.Text.Trim()
        End If

    End Sub

    Private Sub txtOutput_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOutput.KeyDown
        ' Set new output file name
        '=========================

        If e.KeyCode = Keys.Enter Then
            If txtOutput.Text.Trim() <> "" Then strOutputFile = txtOutput.Text.Trim()
        End If

    End Sub

    Private Sub chkInput_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInput.CheckedChanged
        blnIncludeInput = chkInput.Checked
    End Sub

    Private Sub btnNSLookup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNSLookup.Click
        ' Show NSLookup results in separate window
        '=========================================

        blnTestNameServers = True

        If arlHostList.Count < 1 Then BuildHostList()
        If arlHostList.Count > 0 Then frmNSLookup.ShowDialog(Me)

    End Sub

    Private Sub BuildHostList()
        ' Get list of hosts from input file and build array
        '==================================================

        Dim strHost As String
        Dim arrStrings As String()
        Dim intIndex As Integer


        If strInputFile = "" Then Exit Sub


        '== Get input data from file ==
        If File.Exists(strInputFile) = True Then
            Dim objReader As New StreamReader(strInputFile)

            arlHostList.Clear()

            '== Initialise progress bar ==
            Dim fiFileInfo As New FileInfo(strInputFile)
            pbProgressBar.Value = 0
            pbProgressBar.Maximum = fiFileInfo.Length

            While (Not (objReader.EndOfStream))

                strHost = Trim(objReader.ReadLine)

                '== Increment progress bar ==
                pbProgressBar.Increment(strHost.Length)
                Me.Refresh()

                '== Trim anything from the start ==
                If strHost.StartsWith("ftp://") Then
                    strHost = strHost.Substring(6)
                ElseIf strHost.StartsWith("http://") Or strHost.StartsWith("ftps://") Then
                    strHost = strHost.Substring(7)
                ElseIf strHost.StartsWith("https://") Then
                    strHost = strHost.Substring(8)
                End If


                '== Trim anything from the end ==
                intIndex = InStr(strHost, "/")

                If intIndex > 0 Then
                    arrStrings = strHost.Split("/")
                    strHost = arrStrings(0).Trim
                End If

                '== Add formatted address to input list ==
                If strHost <> "" Then arlHostList.Add(strHost)

            End While

            pbProgressBar.Value = 0
            objReader.Close()
        Else
            MsgBox("File Does Not Exist")
        End If

    End Sub

    Private Sub btnWhoIs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWhoIs.Click
        ' Show WhoIs results in separate window
        '======================================

        blnTestNameServers = False

        If arlHostList.Count < 1 Then BuildHostList()
        If arlHostList.Count > 0 Then frmNSLookup.ShowDialog(Me)

    End Sub

End Class
