<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditAddress
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
		Me.Delete_Button = New System.Windows.Forms.Button()
		Me.Update_Button = New System.Windows.Forms.Button()
		Me.Cancel_Button = New System.Windows.Forms.Button()
		Me.ContactEmail_TextBox = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.ContactNumber_TextBox = New System.Windows.Forms.TextBox()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.ContactName_TextBox = New System.Windows.Forms.TextBox()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.Country_TextBox = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Phone_TextBox = New System.Windows.Forms.TextBox()
		Me.Zip_TextBox = New System.Windows.Forms.TextBox()
		Me.State_TextBox = New System.Windows.Forms.TextBox()
		Me.City_TextBox = New System.Windows.Forms.TextBox()
		Me.Address_TextBox = New System.Windows.Forms.TextBox()
		Me.Company_TextBox = New System.Windows.Forms.TextBox()
		Me.Label23 = New System.Windows.Forms.Label()
		Me.Label24 = New System.Windows.Forms.Label()
		Me.Label19 = New System.Windows.Forms.Label()
		Me.Label20 = New System.Windows.Forms.Label()
		Me.Label21 = New System.Windows.Forms.Label()
		Me.Label22 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'Delete_Button
		'
		Me.Delete_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Delete_Button.Enabled = False
		Me.Delete_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Delete_Button.Location = New System.Drawing.Point(265, 272)
		Me.Delete_Button.Name = "Delete_Button"
		Me.Delete_Button.Size = New System.Drawing.Size(87, 29)
		Me.Delete_Button.TabIndex = 21
		Me.Delete_Button.Text = "Delete"
		Me.Delete_Button.UseVisualStyleBackColor = True
		'
		'Update_Button
		'
		Me.Update_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Update_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Update_Button.Location = New System.Drawing.Point(172, 272)
		Me.Update_Button.Name = "Update_Button"
		Me.Update_Button.Size = New System.Drawing.Size(87, 29)
		Me.Update_Button.TabIndex = 20
		Me.Update_Button.Text = "Update"
		Me.Update_Button.UseVisualStyleBackColor = True
		'
		'Cancel_Button
		'
		Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cancel_Button.Location = New System.Drawing.Point(358, 272)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(87, 29)
		Me.Cancel_Button.TabIndex = 22
		Me.Cancel_Button.Text = "Cancel"
		Me.Cancel_Button.UseVisualStyleBackColor = True
		'
		'ContactEmail_TextBox
		'
		Me.ContactEmail_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ContactEmail_TextBox.Location = New System.Drawing.Point(173, 240)
		Me.ContactEmail_TextBox.Name = "ContactEmail_TextBox"
		Me.ContactEmail_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.ContactEmail_TextBox.TabIndex = 19
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Location = New System.Drawing.Point(80, 241)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(53, 20)
		Me.Label9.TabIndex = 18
		Me.Label9.Text = "E-mail"
		'
		'ContactNumber_TextBox
		'
		Me.ContactNumber_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ContactNumber_TextBox.Location = New System.Drawing.Point(173, 211)
		Me.ContactNumber_TextBox.Name = "ContactNumber_TextBox"
		Me.ContactNumber_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.ContactNumber_TextBox.TabIndex = 17
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Location = New System.Drawing.Point(80, 212)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(65, 20)
		Me.Label13.TabIndex = 16
		Me.Label13.Text = "Number"
		'
		'ContactName_TextBox
		'
		Me.ContactName_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ContactName_TextBox.Location = New System.Drawing.Point(173, 182)
		Me.ContactName_TextBox.Name = "ContactName_TextBox"
		Me.ContactName_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.ContactName_TextBox.TabIndex = 15
		'
		'Label12
		'
		Me.Label12.AutoSize = True
		Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.Location = New System.Drawing.Point(12, 183)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(119, 20)
		Me.Label12.TabIndex = 14
		Me.Label12.Text = "Contact:  Name"
		'
		'Country_TextBox
		'
		Me.Country_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Country_TextBox.Location = New System.Drawing.Point(173, 124)
		Me.Country_TextBox.Name = "Country_TextBox"
		Me.Country_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.Country_TextBox.TabIndex = 11
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 125)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(64, 20)
		Me.Label3.TabIndex = 10
		Me.Label3.Text = "Country"
		'
		'Phone_TextBox
		'
		Me.Phone_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Phone_TextBox.Location = New System.Drawing.Point(173, 153)
		Me.Phone_TextBox.Name = "Phone_TextBox"
		Me.Phone_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.Phone_TextBox.TabIndex = 13
		'
		'Zip_TextBox
		'
		Me.Zip_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Zip_TextBox.Location = New System.Drawing.Point(357, 95)
		Me.Zip_TextBox.Name = "Zip_TextBox"
		Me.Zip_TextBox.Size = New System.Drawing.Size(88, 23)
		Me.Zip_TextBox.TabIndex = 9
		'
		'State_TextBox
		'
		Me.State_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.State_TextBox.Location = New System.Drawing.Point(173, 95)
		Me.State_TextBox.Name = "State_TextBox"
		Me.State_TextBox.Size = New System.Drawing.Size(131, 23)
		Me.State_TextBox.TabIndex = 7
		'
		'City_TextBox
		'
		Me.City_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.City_TextBox.Location = New System.Drawing.Point(173, 66)
		Me.City_TextBox.Name = "City_TextBox"
		Me.City_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.City_TextBox.TabIndex = 5
		'
		'Address_TextBox
		'
		Me.Address_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Address_TextBox.Location = New System.Drawing.Point(173, 37)
		Me.Address_TextBox.Name = "Address_TextBox"
		Me.Address_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.Address_TextBox.TabIndex = 3
		'
		'Company_TextBox
		'
		Me.Company_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Company_TextBox.Location = New System.Drawing.Point(173, 8)
		Me.Company_TextBox.Name = "Company_TextBox"
		Me.Company_TextBox.Size = New System.Drawing.Size(272, 23)
		Me.Company_TextBox.TabIndex = 1
		'
		'Label23
		'
		Me.Label23.AutoSize = True
		Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label23.Location = New System.Drawing.Point(12, 154)
		Me.Label23.Name = "Label23"
		Me.Label23.Size = New System.Drawing.Size(55, 20)
		Me.Label23.TabIndex = 12
		Me.Label23.Text = "Phone"
		'
		'Label24
		'
		Me.Label24.AutoSize = True
		Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label24.Location = New System.Drawing.Point(320, 96)
		Me.Label24.Name = "Label24"
		Me.Label24.Size = New System.Drawing.Size(31, 20)
		Me.Label24.TabIndex = 8
		Me.Label24.Text = "Zip"
		'
		'Label19
		'
		Me.Label19.AutoSize = True
		Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label19.Location = New System.Drawing.Point(12, 96)
		Me.Label19.Name = "Label19"
		Me.Label19.Size = New System.Drawing.Size(48, 20)
		Me.Label19.TabIndex = 6
		Me.Label19.Text = "State"
		'
		'Label20
		'
		Me.Label20.AutoSize = True
		Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label20.Location = New System.Drawing.Point(12, 67)
		Me.Label20.Name = "Label20"
		Me.Label20.Size = New System.Drawing.Size(35, 20)
		Me.Label20.TabIndex = 4
		Me.Label20.Text = "City"
		'
		'Label21
		'
		Me.Label21.AutoSize = True
		Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label21.Location = New System.Drawing.Point(12, 38)
		Me.Label21.Name = "Label21"
		Me.Label21.Size = New System.Drawing.Size(68, 20)
		Me.Label21.TabIndex = 2
		Me.Label21.Text = "Address"
		'
		'Label22
		'
		Me.Label22.AutoSize = True
		Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label22.Location = New System.Drawing.Point(12, 9)
		Me.Label22.Name = "Label22"
		Me.Label22.Size = New System.Drawing.Size(76, 20)
		Me.Label22.TabIndex = 0
		Me.Label22.Text = "Company"
		'
		'EditAddress
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.ClientSize = New System.Drawing.Size(457, 313)
		Me.Controls.Add(Me.ContactEmail_TextBox)
		Me.Controls.Add(Me.Label9)
		Me.Controls.Add(Me.ContactNumber_TextBox)
		Me.Controls.Add(Me.Label13)
		Me.Controls.Add(Me.ContactName_TextBox)
		Me.Controls.Add(Me.Label12)
		Me.Controls.Add(Me.Country_TextBox)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Phone_TextBox)
		Me.Controls.Add(Me.Zip_TextBox)
		Me.Controls.Add(Me.State_TextBox)
		Me.Controls.Add(Me.City_TextBox)
		Me.Controls.Add(Me.Address_TextBox)
		Me.Controls.Add(Me.Company_TextBox)
		Me.Controls.Add(Me.Label23)
		Me.Controls.Add(Me.Label24)
		Me.Controls.Add(Me.Label19)
		Me.Controls.Add(Me.Label20)
		Me.Controls.Add(Me.Label21)
		Me.Controls.Add(Me.Label22)
		Me.Controls.Add(Me.Delete_Button)
		Me.Controls.Add(Me.Update_Button)
		Me.Controls.Add(Me.Cancel_Button)
		Me.Name = "EditAddress"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Edit Address"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Delete_Button As Button
	Friend WithEvents Update_Button As Button
	Friend WithEvents Cancel_Button As Button
	Friend WithEvents ContactEmail_TextBox As TextBox
	Friend WithEvents Label9 As Label
	Friend WithEvents ContactNumber_TextBox As TextBox
	Friend WithEvents Label13 As Label
	Friend WithEvents ContactName_TextBox As TextBox
	Friend WithEvents Label12 As Label
	Friend WithEvents Country_TextBox As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Phone_TextBox As TextBox
	Friend WithEvents Zip_TextBox As TextBox
	Friend WithEvents State_TextBox As TextBox
	Friend WithEvents City_TextBox As TextBox
	Friend WithEvents Address_TextBox As TextBox
	Friend WithEvents Company_TextBox As TextBox
	Friend WithEvents Label23 As Label
	Friend WithEvents Label24 As Label
	Friend WithEvents Label19 As Label
	Friend WithEvents Label20 As Label
	Friend WithEvents Label21 As Label
	Friend WithEvents Label22 As Label
End Class
