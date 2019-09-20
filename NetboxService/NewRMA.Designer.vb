<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewRMA
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
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.Label14 = New System.Windows.Forms.Label()
		Me.Label13 = New System.Windows.Forms.Label()
		Me.ContactName_TextBox = New System.Windows.Forms.TextBox()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.Description_TextBox = New System.Windows.Forms.TextBox()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.Customer_TextBox = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.InvoiceNumber_TextBox = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.RGANumber_TextBox = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.ContactNumber_TextBox = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.ContactEmail_TextBox = New System.Windows.Forms.TextBox()
		Me.Cancel_Button = New System.Windows.Forms.Button()
		Me.AddRMA_Button = New System.Windows.Forms.Button()
		Me.Code_ComboBox = New System.Windows.Forms.ComboBox()
		Me.AddCode_Button = New System.Windows.Forms.Button()
		Me.ServiceForm_TextBox = New System.Windows.Forms.TextBox()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.SerialNumber_TextBox = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Tested_CheckBox = New System.Windows.Forms.CheckBox()
		Me.CodeItems_DataGridView = New System.Windows.Forms.DataGridView()
		Me.DeleteCode_Button = New System.Windows.Forms.Button()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Information_DTP = New System.Windows.Forms.DateTimePicker()
		CType(Me.CodeItems_DataGridView,System.ComponentModel.ISupportInitialize).BeginInit
		Me.GroupBox1.SuspendLayout
		Me.SuspendLayout
		'
		'Label14
		'
		Me.Label14.AutoSize = true
		Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label14.Location = New System.Drawing.Point(12, 95)
		Me.Label14.Name = "Label14"
		Me.Label14.Size = New System.Drawing.Size(129, 20)
		Me.Label14.TabIndex = 5
		Me.Label14.Text = "Information Date"
		'
		'Label13
		'
		Me.Label13.AutoSize = true
		Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label13.Location = New System.Drawing.Point(80, 182)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(65, 20)
		Me.Label13.TabIndex = 16
		Me.Label13.Text = "Number"
		'
		'ContactName_TextBox
		'
		Me.ContactName_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ContactName_TextBox.Location = New System.Drawing.Point(173, 152)
		Me.ContactName_TextBox.Name = "ContactName_TextBox"
		Me.ContactName_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.ContactName_TextBox.TabIndex = 15
		'
		'Label12
		'
		Me.Label12.AutoSize = true
		Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label12.Location = New System.Drawing.Point(12, 153)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(119, 20)
		Me.Label12.TabIndex = 14
		Me.Label12.Text = "Contact:  Name"
		'
		'Description_TextBox
		'
		Me.Description_TextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Description_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Description_TextBox.Location = New System.Drawing.Point(16, 352)
		Me.Description_TextBox.Multiline = true
		Me.Description_TextBox.Name = "Description_TextBox"
		Me.Description_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.Description_TextBox.Size = New System.Drawing.Size(766, 137)
		Me.Description_TextBox.TabIndex = 27
		'
		'Label11
		'
		Me.Label11.AutoSize = true
		Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label11.Location = New System.Drawing.Point(12, 327)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(238, 20)
		Me.Label11.TabIndex = 26
		Me.Label11.Text = "Customer description of problem"
		'
		'Customer_TextBox
		'
		Me.Customer_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Customer_TextBox.Location = New System.Drawing.Point(173, 123)
		Me.Customer_TextBox.Name = "Customer_TextBox"
		Me.Customer_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.Customer_TextBox.TabIndex = 13
		'
		'Label5
		'
		Me.Label5.AutoSize = true
		Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label5.Location = New System.Drawing.Point(12, 124)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(78, 20)
		Me.Label5.TabIndex = 12
		Me.Label5.Text = "Customer"
		'
		'InvoiceNumber_TextBox
		'
		Me.InvoiceNumber_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.InvoiceNumber_TextBox.Location = New System.Drawing.Point(173, 268)
		Me.InvoiceNumber_TextBox.Name = "InvoiceNumber_TextBox"
		Me.InvoiceNumber_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.InvoiceNumber_TextBox.TabIndex = 23
		'
		'Label4
		'
		Me.Label4.AutoSize = true
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label4.Location = New System.Drawing.Point(12, 269)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(145, 20)
		Me.Label4.TabIndex = 22
		Me.Label4.Text = "Customer Invoice #"
		'
		'RGANumber_TextBox
		'
		Me.RGANumber_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.RGANumber_TextBox.Location = New System.Drawing.Point(173, 239)
		Me.RGANumber_TextBox.Name = "RGANumber_TextBox"
		Me.RGANumber_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.RGANumber_TextBox.TabIndex = 21
		'
		'Label3
		'
		Me.Label3.AutoSize = true
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 240)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(131, 20)
		Me.Label3.TabIndex = 20
		Me.Label3.Text = "Customer RGA #"
		'
		'Label8
		'
		Me.Label8.AutoSize = true
		Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label8.Location = New System.Drawing.Point(12, 9)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(87, 20)
		Me.Label8.TabIndex = 0
		Me.Label8.Text = "New RMA"
		'
		'ContactNumber_TextBox
		'
		Me.ContactNumber_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ContactNumber_TextBox.Location = New System.Drawing.Point(173, 181)
		Me.ContactNumber_TextBox.Name = "ContactNumber_TextBox"
		Me.ContactNumber_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.ContactNumber_TextBox.TabIndex = 17
		'
		'Label9
		'
		Me.Label9.AutoSize = true
		Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label9.Location = New System.Drawing.Point(80, 211)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(53, 20)
		Me.Label9.TabIndex = 18
		Me.Label9.Text = "E-mail"
		'
		'ContactEmail_TextBox
		'
		Me.ContactEmail_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ContactEmail_TextBox.Location = New System.Drawing.Point(173, 210)
		Me.ContactEmail_TextBox.Name = "ContactEmail_TextBox"
		Me.ContactEmail_TextBox.Size = New System.Drawing.Size(260, 23)
		Me.ContactEmail_TextBox.TabIndex = 19
		'
		'Cancel_Button
		'
		Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Cancel_Button.Location = New System.Drawing.Point(695, 495)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(87, 29)
		Me.Cancel_Button.TabIndex = 30
		Me.Cancel_Button.Text = "Cancel"
		Me.Cancel_Button.UseVisualStyleBackColor = true
		'
		'AddRMA_Button
		'
		Me.AddRMA_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.AddRMA_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.AddRMA_Button.Location = New System.Drawing.Point(602, 495)
		Me.AddRMA_Button.Name = "AddRMA_Button"
		Me.AddRMA_Button.Size = New System.Drawing.Size(87, 29)
		Me.AddRMA_Button.TabIndex = 29
		Me.AddRMA_Button.Text = "Add"
		Me.AddRMA_Button.UseVisualStyleBackColor = true
		'
		'Code_ComboBox
		'
		Me.Code_ComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Code_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Code_ComboBox.Font = New System.Drawing.Font("Consolas", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Code_ComboBox.FormattingEnabled = true
		Me.Code_ComboBox.Location = New System.Drawing.Point(110, 27)
		Me.Code_ComboBox.Name = "Code_ComboBox"
		Me.Code_ComboBox.Size = New System.Drawing.Size(227, 27)
		Me.Code_ComboBox.TabIndex = 0
		'
		'AddCode_Button
		'
		Me.AddCode_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.AddCode_Button.Location = New System.Drawing.Point(6, 25)
		Me.AddCode_Button.Name = "AddCode_Button"
		Me.AddCode_Button.Size = New System.Drawing.Size(98, 29)
		Me.AddCode_Button.TabIndex = 1
		Me.AddCode_Button.Text = "Add Code"
		Me.AddCode_Button.UseVisualStyleBackColor = true
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
		'SerialNumber_TextBox
		'
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
		'Label10
		'
		Me.Label10.AutoSize = true
		Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label10.Location = New System.Drawing.Point(12, 298)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(131, 20)
		Me.Label10.TabIndex = 24
		Me.Label10.Text = "Customer Tested"
		'
		'Tested_CheckBox
		'
		Me.Tested_CheckBox.AutoSize = true
		Me.Tested_CheckBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Tested_CheckBox.Location = New System.Drawing.Point(173, 301)
		Me.Tested_CheckBox.Name = "Tested_CheckBox"
		Me.Tested_CheckBox.Size = New System.Drawing.Size(15, 14)
		Me.Tested_CheckBox.TabIndex = 25
		Me.Tested_CheckBox.UseVisualStyleBackColor = true
		'
		'CodeItems_DataGridView
		'
		Me.CodeItems_DataGridView.AllowUserToAddRows = false
		Me.CodeItems_DataGridView.AllowUserToDeleteRows = false
		DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
		Me.CodeItems_DataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
		Me.CodeItems_DataGridView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.CodeItems_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
		Me.CodeItems_DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
		Me.CodeItems_DataGridView.BackgroundColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.CodeItems_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.CodeItems_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.CodeItems_DataGridView.Location = New System.Drawing.Point(110, 60)
		Me.CodeItems_DataGridView.MultiSelect = false
		Me.CodeItems_DataGridView.Name = "CodeItems_DataGridView"
		Me.CodeItems_DataGridView.ReadOnly = true
		Me.CodeItems_DataGridView.RowHeadersVisible = false
		Me.CodeItems_DataGridView.Size = New System.Drawing.Size(227, 214)
		Me.CodeItems_DataGridView.TabIndex = 2
		'
		'DeleteCode_Button
		'
		Me.DeleteCode_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.DeleteCode_Button.Location = New System.Drawing.Point(6, 60)
		Me.DeleteCode_Button.Name = "DeleteCode_Button"
		Me.DeleteCode_Button.Size = New System.Drawing.Size(98, 29)
		Me.DeleteCode_Button.TabIndex = 3
		Me.DeleteCode_Button.Text = "Delete Code"
		Me.DeleteCode_Button.UseVisualStyleBackColor = true
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.AddCode_Button)
		Me.GroupBox1.Controls.Add(Me.CodeItems_DataGridView)
		Me.GroupBox1.Controls.Add(Me.Code_ComboBox)
		Me.GroupBox1.Controls.Add(Me.DeleteCode_Button)
		Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.GroupBox1.Location = New System.Drawing.Point(439, 66)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(343, 281)
		Me.GroupBox1.TabIndex = 28
		Me.GroupBox1.TabStop = false
		Me.GroupBox1.Text = "Codes"
		'
		'Information_DTP
		'
		Me.Information_DTP.CalendarFont = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Information_DTP.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Information_DTP.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
		Me.Information_DTP.Location = New System.Drawing.Point(173, 92)
		Me.Information_DTP.Name = "Information_DTP"
		Me.Information_DTP.Size = New System.Drawing.Size(120, 26)
		Me.Information_DTP.TabIndex = 31
		Me.Information_DTP.Value = New Date(2019, 4, 9, 8, 52, 40, 0)
		'
		'NewRMA
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = true
		Me.ClientSize = New System.Drawing.Size(792, 528)
		Me.Controls.Add(Me.Information_DTP)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.Tested_CheckBox)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.SerialNumber_TextBox)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.ServiceForm_TextBox)
		Me.Controls.Add(Me.Label15)
		Me.Controls.Add(Me.AddRMA_Button)
		Me.Controls.Add(Me.Cancel_Button)
		Me.Controls.Add(Me.ContactEmail_TextBox)
		Me.Controls.Add(Me.Label9)
		Me.Controls.Add(Me.ContactNumber_TextBox)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Label14)
		Me.Controls.Add(Me.Label13)
		Me.Controls.Add(Me.ContactName_TextBox)
		Me.Controls.Add(Me.Label12)
		Me.Controls.Add(Me.Description_TextBox)
		Me.Controls.Add(Me.Label11)
		Me.Controls.Add(Me.Customer_TextBox)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.InvoiceNumber_TextBox)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.RGANumber_TextBox)
		Me.Controls.Add(Me.Label3)
		Me.Name = "NewRMA"
		Me.Text = "New RMA"
		CType(Me.CodeItems_DataGridView,System.ComponentModel.ISupportInitialize).EndInit
		Me.GroupBox1.ResumeLayout(false)
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub
	Friend WithEvents Label14 As Label
	Friend WithEvents Label13 As Label
	Friend WithEvents ContactName_TextBox As TextBox
	Friend WithEvents Label12 As Label
	Friend WithEvents Description_TextBox As TextBox
	Friend WithEvents Label11 As Label
	Friend WithEvents Customer_TextBox As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents InvoiceNumber_TextBox As TextBox
	Friend WithEvents Label4 As Label
	Friend WithEvents RGANumber_TextBox As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Label8 As Label
	Friend WithEvents ContactNumber_TextBox As TextBox
	Friend WithEvents Label9 As Label
	Friend WithEvents ContactEmail_TextBox As TextBox
	Friend WithEvents Cancel_Button As Button
	Friend WithEvents AddRMA_Button As Button
	Friend WithEvents Code_ComboBox As ComboBox
	Friend WithEvents AddCode_Button As Button
	Friend WithEvents ServiceForm_TextBox As TextBox
	Friend WithEvents Label15 As Label
	Friend WithEvents SerialNumber_TextBox As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Label10 As Label
	Friend WithEvents Tested_CheckBox As CheckBox
	Friend WithEvents CodeItems_DataGridView As DataGridView
	Friend WithEvents DeleteCode_Button As Button
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents Information_DTP As DateTimePicker
End Class
