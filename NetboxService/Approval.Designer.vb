<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Approval
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
		Me.SerialNumber_TextBox = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.ServiceForm_TextBox = New System.Windows.Forms.TextBox()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label14 = New System.Windows.Forms.Label()
		Me.Update_Button = New System.Windows.Forms.Button()
		Me.Cancel_Button = New System.Windows.Forms.Button()
		Me.Status_ComboBox = New System.Windows.Forms.ComboBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.ApprovalNotes_TextBox = New System.Windows.Forms.TextBox()
		Me.Label16 = New System.Windows.Forms.Label()
		Me.UpdateAndNext_Button = New System.Windows.Forms.Button()
		Me.Date_DTP = New System.Windows.Forms.DateTimePicker()
		Me.SuspendLayout
		'
		'SerialNumber_TextBox
		'
		Me.SerialNumber_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.SerialNumber_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.SerialNumber_TextBox.Location = New System.Drawing.Point(173, 65)
		Me.SerialNumber_TextBox.Name = "SerialNumber_TextBox"
		Me.SerialNumber_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.SerialNumber_TextBox.TabIndex = 4
		'
		'Label1
		'
		Me.Label1.AutoSize = true
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label1.Location = New System.Drawing.Point(12, 66)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(109, 20)
		Me.Label1.TabIndex = 3
		Me.Label1.Text = "Serial Number"
		'
		'ServiceForm_TextBox
		'
		Me.ServiceForm_TextBox.Enabled = false
		Me.ServiceForm_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ServiceForm_TextBox.Location = New System.Drawing.Point(173, 36)
		Me.ServiceForm_TextBox.Name = "ServiceForm_TextBox"
		Me.ServiceForm_TextBox.Size = New System.Drawing.Size(141, 23)
		Me.ServiceForm_TextBox.TabIndex = 2
		'
		'Label15
		'
		Me.Label15.AutoSize = true
		Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label15.Location = New System.Drawing.Point(12, 37)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(162, 20)
		Me.Label15.TabIndex = 1
		Me.Label15.Text = "Service Form Number"
		'
		'Label8
		'
		Me.Label8.AutoSize = true
		Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label8.Location = New System.Drawing.Point(12, 9)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(176, 20)
		Me.Label8.TabIndex = 0
		Me.Label8.Text = "Approval Information"
		'
		'Label14
		'
		Me.Label14.AutoSize = true
		Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label14.Location = New System.Drawing.Point(12, 95)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(110, 20)
		Me.Label14.TabIndex = 5
		Me.Label14.Text = "Approval Date"
		'
		'Update_Button
		'
		Me.Update_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Update_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Update_Button.Location = New System.Drawing.Point(253, 535)
		Me.Update_Button.Name = "Update_Button"
		Me.Update_Button.Size = New System.Drawing.Size(87, 29)
		Me.Update_Button.TabIndex = 17
		Me.Update_Button.Text = "Update"
		Me.Update_Button.UseVisualStyleBackColor = true
		'
		'Cancel_Button
		'
		Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Cancel_Button.Location = New System.Drawing.Point(346, 535)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(87, 29)
		Me.Cancel_Button.TabIndex = 18
		Me.Cancel_Button.Text = "Cancel"
		Me.Cancel_Button.UseVisualStyleBackColor = true
		'
		'Status_ComboBox
		'
		Me.Status_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Status_ComboBox.Font = New System.Drawing.Font("Consolas", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Status_ComboBox.FormattingEnabled = true
		Me.Status_ComboBox.Location = New System.Drawing.Point(173, 123)
		Me.Status_ComboBox.Name = "Status_ComboBox"
		Me.Status_ComboBox.Size = New System.Drawing.Size(260, 27)
		Me.Status_ComboBox.TabIndex = 13
		'
		'Label3
		'
		Me.Label3.AutoSize = true
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 124)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(56, 20)
		Me.Label3.TabIndex = 12
		Me.Label3.Text = "Status"
		'
		'ApprovalNotes_TextBox
		'
		Me.ApprovalNotes_TextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.ApprovalNotes_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ApprovalNotes_TextBox.Location = New System.Drawing.Point(16, 181)
		Me.ApprovalNotes_TextBox.Multiline = true
		Me.ApprovalNotes_TextBox.Name = "ApprovalNotes_TextBox"
		Me.ApprovalNotes_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.ApprovalNotes_TextBox.Size = New System.Drawing.Size(415, 348)
		Me.ApprovalNotes_TextBox.TabIndex = 15
		'
		'Label16
		'
		Me.Label16.AutoSize = true
		Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label16.Location = New System.Drawing.Point(12, 153)
		Me.Label16.Name = "Label16"
		Me.Label16.Size = New System.Drawing.Size(55, 20)
		Me.Label16.TabIndex = 14
		Me.Label16.Text = "Notes:"
		'
		'UpdateAndNext_Button
		'
		Me.UpdateAndNext_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.UpdateAndNext_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.UpdateAndNext_Button.Location = New System.Drawing.Point(133, 535)
		Me.UpdateAndNext_Button.Name = "UpdateAndNext_Button"
		Me.UpdateAndNext_Button.Size = New System.Drawing.Size(114, 29)
		Me.UpdateAndNext_Button.TabIndex = 16
		Me.UpdateAndNext_Button.Text = "Update + Next"
		Me.UpdateAndNext_Button.UseVisualStyleBackColor = true
		'
		'Date_DTP
		'
		Me.Date_DTP.CalendarFont = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Date_DTP.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Date_DTP.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
		Me.Date_DTP.Location = New System.Drawing.Point(173, 92)
		Me.Date_DTP.Name = "Date_DTP"
		Me.Date_DTP.Size = New System.Drawing.Size(120, 26)
		Me.Date_DTP.TabIndex = 32
		Me.Date_DTP.Value = New Date(2019, 4, 9, 8, 52, 40, 0)
		'
		'Approval
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = true
		Me.ClientSize = New System.Drawing.Size(443, 568)
		Me.Controls.Add(Me.Date_DTP)
		Me.Controls.Add(Me.UpdateAndNext_Button)
		Me.Controls.Add(Me.ApprovalNotes_TextBox)
		Me.Controls.Add(Me.Label16)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Status_ComboBox)
		Me.Controls.Add(Me.Update_Button)
		Me.Controls.Add(Me.Cancel_Button)
		Me.Controls.Add(Me.SerialNumber_TextBox)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.ServiceForm_TextBox)
		Me.Controls.Add(Me.Label15)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Label14)
		Me.Name = "Approval"
		Me.Text = "Approval"
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub

	Friend WithEvents SerialNumber_TextBox As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents ServiceForm_TextBox As TextBox
	Friend WithEvents Label15 As Label
	Friend WithEvents Label8 As Label
	Friend WithEvents Label14 As Label
	Friend WithEvents Update_Button As Button
	Friend WithEvents Cancel_Button As Button
	Friend WithEvents Status_ComboBox As ComboBox
	Friend WithEvents Label3 As Label
	Friend WithEvents ApprovalNotes_TextBox As TextBox
	Friend WithEvents Label16 As Label
	Friend WithEvents UpdateAndNext_Button As Button
	Friend WithEvents Date_DTP As DateTimePicker
End Class
