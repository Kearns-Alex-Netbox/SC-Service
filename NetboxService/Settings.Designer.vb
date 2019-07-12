<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Settings
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
		Me.ServiceForm_TextBox = New System.Windows.Forms.TextBox()
		Me.Label15 = New System.Windows.Forms.Label()
		Me.NewSFN_TextBox = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Save_Button = New System.Windows.Forms.Button()
		Me.Close_Button = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.PDFFile_TextBox = New System.Windows.Forms.TextBox()
		Me.BrowsePDFFile_Button = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.ReturnsDirectory_TextBox = New System.Windows.Forms.TextBox()
		Me.ReturnServiceDirectory_Button = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'ServiceForm_TextBox
		'
		Me.ServiceForm_TextBox.Enabled = False
		Me.ServiceForm_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ServiceForm_TextBox.Location = New System.Drawing.Point(16, 32)
		Me.ServiceForm_TextBox.Name = "ServiceForm_TextBox"
		Me.ServiceForm_TextBox.Size = New System.Drawing.Size(76, 23)
		Me.ServiceForm_TextBox.TabIndex = 1
		'
		'Label15
		'
		Me.Label15.AutoSize = True
		Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label15.Location = New System.Drawing.Point(12, 9)
		Me.Label15.Name = "Label15"
		Me.Label15.Size = New System.Drawing.Size(162, 20)
		Me.Label15.TabIndex = 0
		Me.Label15.Text = "Service Form Number"
		'
		'NewSFN_TextBox
		'
		Me.NewSFN_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.NewSFN_TextBox.Location = New System.Drawing.Point(199, 32)
		Me.NewSFN_TextBox.Name = "NewSFN_TextBox"
		Me.NewSFN_TextBox.Size = New System.Drawing.Size(76, 23)
		Me.NewSFN_TextBox.TabIndex = 3
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(134, 32)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(23, 20)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "->"
		'
		'Save_Button
		'
		Me.Save_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Save_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Save_Button.Location = New System.Drawing.Point(86, 238)
		Me.Save_Button.Name = "Save_Button"
		Me.Save_Button.Size = New System.Drawing.Size(87, 29)
		Me.Save_Button.TabIndex = 10
		Me.Save_Button.Text = "Save"
		Me.Save_Button.UseVisualStyleBackColor = True
		'
		'Close_Button
		'
		Me.Close_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Close_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Close_Button.Location = New System.Drawing.Point(179, 238)
		Me.Close_Button.Name = "Close_Button"
		Me.Close_Button.Size = New System.Drawing.Size(87, 29)
		Me.Close_Button.TabIndex = 11
		Me.Close_Button.Text = "Close"
		Me.Close_Button.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 58)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(70, 20)
		Me.Label3.TabIndex = 4
		Me.Label3.Text = "PDF File"
		'
		'PDFFile_TextBox
		'
		Me.PDFFile_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.PDFFile_TextBox.Location = New System.Drawing.Point(16, 81)
		Me.PDFFile_TextBox.Name = "PDFFile_TextBox"
		Me.PDFFile_TextBox.Size = New System.Drawing.Size(259, 26)
		Me.PDFFile_TextBox.TabIndex = 5
		'
		'BrowsePDFFile_Button
		'
		Me.BrowsePDFFile_Button.AutoSize = True
		Me.BrowsePDFFile_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.BrowsePDFFile_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
		Me.BrowsePDFFile_Button.Location = New System.Drawing.Point(16, 113)
		Me.BrowsePDFFile_Button.Name = "BrowsePDFFile_Button"
		Me.BrowsePDFFile_Button.Size = New System.Drawing.Size(72, 30)
		Me.BrowsePDFFile_Button.TabIndex = 6
		Me.BrowsePDFFile_Button.Text = "Browse"
		Me.BrowsePDFFile_Button.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(12, 146)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(212, 20)
		Me.Label2.TabIndex = 7
		Me.Label2.Text = "Returns for Service Directory"
		'
		'ReturnsDirectory_TextBox
		'
		Me.ReturnsDirectory_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ReturnsDirectory_TextBox.Location = New System.Drawing.Point(16, 169)
		Me.ReturnsDirectory_TextBox.Name = "ReturnsDirectory_TextBox"
		Me.ReturnsDirectory_TextBox.Size = New System.Drawing.Size(259, 26)
		Me.ReturnsDirectory_TextBox.TabIndex = 8
		'
		'ReturnServiceDirectory_Button
		'
		Me.ReturnServiceDirectory_Button.AutoSize = True
		Me.ReturnServiceDirectory_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.ReturnServiceDirectory_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
		Me.ReturnServiceDirectory_Button.Location = New System.Drawing.Point(16, 201)
		Me.ReturnServiceDirectory_Button.Name = "ReturnServiceDirectory_Button"
		Me.ReturnServiceDirectory_Button.Size = New System.Drawing.Size(72, 30)
		Me.ReturnServiceDirectory_Button.TabIndex = 9
		Me.ReturnServiceDirectory_Button.Text = "Browse"
		Me.ReturnServiceDirectory_Button.UseVisualStyleBackColor = True
		'
		'Settings
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(278, 278)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.ReturnsDirectory_TextBox)
		Me.Controls.Add(Me.ReturnServiceDirectory_Button)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.PDFFile_TextBox)
		Me.Controls.Add(Me.BrowsePDFFile_Button)
		Me.Controls.Add(Me.Save_Button)
		Me.Controls.Add(Me.Close_Button)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.NewSFN_TextBox)
		Me.Controls.Add(Me.ServiceForm_TextBox)
		Me.Controls.Add(Me.Label15)
		Me.Name = "Settings"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Settings"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents ServiceForm_TextBox As TextBox
	Friend WithEvents Label15 As Label
	Friend WithEvents NewSFN_TextBox As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Save_Button As Button
	Friend WithEvents Close_Button As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents PDFFile_TextBox As TextBox
	Friend WithEvents BrowsePDFFile_Button As Button
	Friend WithEvents Label2 As Label
	Friend WithEvents ReturnsDirectory_TextBox As TextBox
	Friend WithEvents ReturnServiceDirectory_Button As Button
End Class
