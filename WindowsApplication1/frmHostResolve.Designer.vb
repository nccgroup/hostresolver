<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHostResolve
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblInput = New System.Windows.Forms.Label()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.lbOutput = New System.Windows.Forms.ListBox()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.ofdOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.lblOutput = New System.Windows.Forms.Label()
        Me.sfdSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.gbMain = New System.Windows.Forms.GroupBox()
        Me.rbIPToHost = New System.Windows.Forms.RadioButton()
        Me.rbHostToIP = New System.Windows.Forms.RadioButton()
        Me.chkInput = New System.Windows.Forms.CheckBox()
        Me.pbProgressBar = New System.Windows.Forms.ProgressBar()
        Me.btnNSLookup = New System.Windows.Forms.Button()
        Me.btnWhoIs = New System.Windows.Forms.Button()
        Me.gbMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblInput
        '
        Me.lblInput.AutoSize = True
        Me.lblInput.Location = New System.Drawing.Point(5, 10)
        Me.lblInput.Name = "lblInput"
        Me.lblInput.Size = New System.Drawing.Size(53, 13)
        Me.lblInput.TabIndex = 0
        Me.lblInput.Text = "Input File:"
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(5, 26)
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(524, 20)
        Me.txtInput.TabIndex = 1
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(478, 451)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(88, 23)
        Me.btnStart.TabIndex = 2
        Me.btnStart.Text = "&Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'lbOutput
        '
        Me.lbOutput.FormattingEnabled = True
        Me.lbOutput.Location = New System.Drawing.Point(5, 166)
        Me.lbOutput.Name = "lbOutput"
        Me.lbOutput.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbOutput.Size = New System.Drawing.Size(561, 277)
        Me.lbOutput.TabIndex = 3
        '
        'btnInput
        '
        Me.btnInput.Location = New System.Drawing.Point(536, 23)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.Size = New System.Drawing.Size(30, 23)
        Me.btnInput.TabIndex = 4
        Me.btnInput.Text = "..."
        Me.btnInput.UseVisualStyleBackColor = True
        '
        'ofdOpenFileDialog
        '
        Me.ofdOpenFileDialog.FileName = "InputFile"
        '
        'btnOutput
        '
        Me.btnOutput.Location = New System.Drawing.Point(536, 61)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.Size = New System.Drawing.Size(30, 23)
        Me.btnOutput.TabIndex = 7
        Me.btnOutput.Text = "..."
        Me.btnOutput.UseVisualStyleBackColor = True
        '
        'txtOutput
        '
        Me.txtOutput.Location = New System.Drawing.Point(6, 64)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.Size = New System.Drawing.Size(524, 20)
        Me.txtOutput.TabIndex = 6
        '
        'lblOutput
        '
        Me.lblOutput.AutoSize = True
        Me.lblOutput.Location = New System.Drawing.Point(6, 48)
        Me.lblOutput.Name = "lblOutput"
        Me.lblOutput.Size = New System.Drawing.Size(61, 13)
        Me.lblOutput.TabIndex = 5
        Me.lblOutput.Text = "Output File:"
        '
        'gbMain
        '
        Me.gbMain.Controls.Add(Me.rbIPToHost)
        Me.gbMain.Controls.Add(Me.rbHostToIP)
        Me.gbMain.Location = New System.Drawing.Point(6, 91)
        Me.gbMain.Name = "gbMain"
        Me.gbMain.Size = New System.Drawing.Size(205, 69)
        Me.gbMain.TabIndex = 8
        Me.gbMain.TabStop = False
        Me.gbMain.Text = "Conversion Type"
        '
        'rbIPToHost
        '
        Me.rbIPToHost.AutoSize = True
        Me.rbIPToHost.Location = New System.Drawing.Point(10, 44)
        Me.rbIPToHost.Name = "rbIPToHost"
        Me.rbIPToHost.Size = New System.Drawing.Size(179, 17)
        Me.rbIPToHost.TabIndex = 1
        Me.rbIPToHost.TabStop = True
        Me.rbIPToHost.Text = "Convert IP Address to Hostname"
        Me.rbIPToHost.UseVisualStyleBackColor = True
        '
        'rbHostToIP
        '
        Me.rbHostToIP.AutoSize = True
        Me.rbHostToIP.Checked = True
        Me.rbHostToIP.Location = New System.Drawing.Point(10, 20)
        Me.rbHostToIP.Name = "rbHostToIP"
        Me.rbHostToIP.Size = New System.Drawing.Size(179, 17)
        Me.rbHostToIP.TabIndex = 0
        Me.rbHostToIP.TabStop = True
        Me.rbHostToIP.Text = "Convert Hostname to IP Address"
        Me.rbHostToIP.UseVisualStyleBackColor = True
        '
        'chkInput
        '
        Me.chkInput.AutoSize = True
        Me.chkInput.Checked = True
        Me.chkInput.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInput.Location = New System.Drawing.Point(233, 100)
        Me.chkInput.Name = "chkInput"
        Me.chkInput.Size = New System.Drawing.Size(185, 17)
        Me.chkInput.TabIndex = 9
        Me.chkInput.Text = "Include Input Data in Output File?"
        Me.chkInput.UseVisualStyleBackColor = True
        '
        'pbProgressBar
        '
        Me.pbProgressBar.Location = New System.Drawing.Point(5, 450)
        Me.pbProgressBar.Name = "pbProgressBar"
        Me.pbProgressBar.Size = New System.Drawing.Size(460, 23)
        Me.pbProgressBar.TabIndex = 10
        '
        'btnNSLookup
        '
        Me.btnNSLookup.Location = New System.Drawing.Point(331, 129)
        Me.btnNSLookup.Name = "btnNSLookup"
        Me.btnNSLookup.Size = New System.Drawing.Size(92, 23)
        Me.btnNSLookup.TabIndex = 11
        Me.btnNSLookup.Text = "Name Servers"
        Me.btnNSLookup.UseVisualStyleBackColor = True
        '
        'btnWhoIs
        '
        Me.btnWhoIs.Location = New System.Drawing.Point(233, 129)
        Me.btnWhoIs.Name = "btnWhoIs"
        Me.btnWhoIs.Size = New System.Drawing.Size(92, 23)
        Me.btnWhoIs.TabIndex = 12
        Me.btnWhoIs.Text = "WhoIs Query"
        Me.btnWhoIs.UseVisualStyleBackColor = True
        '
        'frmHostResolve
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(571, 484)
        Me.Controls.Add(Me.btnWhoIs)
        Me.Controls.Add(Me.btnNSLookup)
        Me.Controls.Add(Me.pbProgressBar)
        Me.Controls.Add(Me.chkInput)
        Me.Controls.Add(Me.gbMain)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.lblOutput)
        Me.Controls.Add(Me.btnInput)
        Me.Controls.Add(Me.lbOutput)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.txtInput)
        Me.Controls.Add(Me.lblInput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmHostResolve"
        Me.Text = "HostResolver"
        Me.gbMain.ResumeLayout(False)
        Me.gbMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInput As System.Windows.Forms.Label
    Friend WithEvents txtInput As System.Windows.Forms.TextBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents lbOutput As System.Windows.Forms.ListBox
    Friend WithEvents btnInput As System.Windows.Forms.Button
    Friend WithEvents ofdOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnOutput As System.Windows.Forms.Button
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents lblOutput As System.Windows.Forms.Label
    Friend WithEvents sfdSaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents gbMain As System.Windows.Forms.GroupBox
    Friend WithEvents rbIPToHost As System.Windows.Forms.RadioButton
    Friend WithEvents rbHostToIP As System.Windows.Forms.RadioButton
    Friend WithEvents chkInput As System.Windows.Forms.CheckBox
    Friend WithEvents pbProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnNSLookup As System.Windows.Forms.Button
    Friend WithEvents btnWhoIs As System.Windows.Forms.Button

End Class
