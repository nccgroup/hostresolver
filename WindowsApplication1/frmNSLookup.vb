Option Explicit On

Imports System.IO

Public Class frmNSLookup

    Private strNSOutputFile As String = ""
    Private strPrevSearch As String = ""


    Private Sub frmNSLookup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Initialise controls and look-up table
        '======================================


        If blnTestNameServers Then
            Me.Text = "Nameserver Test"
        Else
            Me.Text = "WhoIs Query"
        End If

        txtNSLookup.Text = ""
        pbToolStripProgressBar.Value = 0
        pbToolStripProgressBar.Maximum = arlHostList.Count
        slToolStripStatusLabel.Text = "Scanning..."

        LoadWhoisHash()

    End Sub

    Private Sub LookupListItem(ByVal HostName As String)
        ' Get DNS info for item and add to screen output
        '===============================================

        txtNSLookup.AppendText("Domain: " & HostName & vbNewLine)
        txtNSLookup.AppendText(DomainLookUp(HostName) & vbNewLine & vbNewLine)
        txtNSLookup.AppendText(DomainLookUp(HostName) & vbNewLine & vbNewLine)
        txtNSLookup.AppendText("=============================================================================" & vbNewLine)
        pbToolStripProgressBar.Increment(1)
        Me.Refresh()

    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        ' Save results to specified output file
        '====================================

        sfdSaveFileDialog.Filter = "Text Files(*.txt)|*.txt"
        Dim intSuccess As Integer = sfdSaveFileDialog.ShowDialog()

        If intSuccess <> DialogResult.Cancel Then
            strNSOutputFile = sfdSaveFileDialog.FileName
        End If

        If strNSOutputFile.Trim = "" Then Exit Sub

        Dim fsFileStream As New FileStream(strNSOutputFile, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite, IO.FileShare.None)
        Dim fwFileWriter As New StreamWriter(fsFileStream)

        pbToolStripProgressBar.Value = 0
        pbToolStripProgressBar.Maximum = txtNSLookup.Lines.Count
        slToolStripStatusLabel.Text = "Saving..."

        Try
            '== Write results ==
            For Each strLine In txtNSLookup.Lines()
                fwFileWriter.WriteLine(strLine)
                pbToolStripProgressBar.Increment(1)
                Me.Refresh()
            Next

            pbToolStripProgressBar.Value = 0
            slToolStripStatusLabel.Text = "Finished saving."

        Catch exError As Exception
            MsgBox(exError.Message)
        Finally
            '== Close file ==
            fwFileWriter.Close()
            fsFileStream.Close()
        End Try

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        ' Close dialog 
        '=============

        '== Has user saved results ==
        If strNSOutputFile.Trim = "" Then
            If MsgBox("Exit without saving results?", vbYesNo, "Results Not Saved") = vbYes Then Me.Close()
        End If

    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        'Copy selected text
        '==================
        If txtNSLookup.SelectedText <> "" Then Clipboard.SetText(txtNSLookup.SelectedText)
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        ' Locate the selected text in the Results box
        '============================================
        Dim strSearch As String
        Dim intPos As Integer = 0

        strSearch = InputBox("Enter Text to Search For", strPrevSearch)

        ' Find string in text.
        intPos = InStr(txtNSLookup.Text, strSearch)

        If intPos > 0 Then
            txtNSLookup.Focus()
            txtNSLookup.SelectionStart = intPos - 1
            txtNSLookup.SelectionLength = Len(strSearch)
        Else
            MsgBox("String not found.")
        End If

        strPrevSearch = strSearch

    End Sub

    Private Sub frmNSLookup_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' Get DNS information
        '====================

        If arlHostList.Count > 0 Then
            For Each itmItem In arlHostList
                LookupListItem(itmItem)
            Next
            pbToolStripProgressBar.Value = 0
            slToolStripStatusLabel.Text = "Finished scanning."
        End If

    End Sub
End Class