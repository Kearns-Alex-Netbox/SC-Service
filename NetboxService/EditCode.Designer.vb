<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditCode
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
		Me.Cancel_Button = New System.Windows.Forms.Button()
		Me.Update_Button = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Code_TextBox = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Type_TextBox = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Description_TextBox = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Fix_TextBox = New System.Windows.Forms.TextBox()
		Me.Delete_Button = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'Cancel_Button
		'
		Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Cancel_Button.Location = New System.Drawing.Point(293, 229)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(87, 29)
		Me.Cancel_Button.TabIndex = 10
		Me.Cancel_Button.Text = "Cancel"
		Me.Cancel_Button.UseVisualStyleBackColor = True
		'
		'Update_Button
		'
		Me.Update_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Update_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Update_Button.Location = New System.Drawing.Point(107, 229)
		Me.Update_Button.Name = "Update_Button"
		Me.Update_Button.Size = New System.Drawing.Size(87, 29)
		Me.Update_Button.TabIndex = 8
		Me.Update_Button.Text = "Update"
		Me.Update_Button.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(47, 20)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Code"
		'
		'Code_TextBox
		'
		Me.Code_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Code_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Code_TextBox.Location = New System.Drawing.Point(107, 8)
		Me.Code_TextBox.Name = "Code_TextBox"
		Me.Code_TextBox.Size = New System.Drawing.Size(273, 23)
		Me.Code_TextBox.TabIndex = 1
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(12, 38)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(43, 20)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Type"
		'
		'Type_TextBox
		'
		Me.Type_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Type_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Type_TextBox.Location = New System.Drawing.Point(107, 37)
		Me.Type_TextBox.Name = "Type_TextBox"
		Me.Type_TextBox.Size = New System.Drawing.Size(273, 23)
		Me.Type_TextBox.TabIndex = 3
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(12, 67)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(89, 20)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "Description"
		'
		'Description_TextBox
		'
		Me.Description_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Description_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Description_TextBox.Location = New System.Drawing.Point(107, 66)
		Me.Description_TextBox.Multiline = True
		Me.Description_TextBox.Name = "Description_TextBox"
		Me.Description_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.Description_TextBox.Size = New System.Drawing.Size(273, 76)
		Me.Description_TextBox.TabIndex = 5
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(12, 148)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(29, 20)
		Me.Label4.TabIndex = 6
		Me.Label4.Text = "Fix"
		'
		'Fix_TextBox
		'
		Me.Fix_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Fix_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Fix_TextBox.Location = New System.Drawing.Point(107, 148)
		Me.Fix_TextBox.Multiline = True
		Me.Fix_TextBox.Name = "Fix_TextBox"
		Me.Fix_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.Fix_TextBox.Size = New System.Drawing.Size(273, 76)
		Me.Fix_TextBox.TabIndex = 7
		'
		'Delete_Button
		'
		Me.Delete_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Delete_Button.Enabled = False
		Me.Delete_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Delete_Button.Location = New System.Drawing.Point(200, 229)
		Me.Delete_Button.Name = "Delete_Button"
		Me.Delete_Button.Size = New System.Drawing.Size(87, 29)
		Me.Delete_Button.TabIndex = 9
		Me.Delete_Button.Text = "Delete"
		Me.Delete_Button.UseVisualStyleBackColor = True
		'
		'EditCode
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.ClientSize = New System.Drawing.Size(392, 269)
		Me.Controls.Add(Me.Delete_Button)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Fix_TextBox)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Description_TextBox)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Type_TextBox)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Code_TextBox)
		Me.Controls.Add(Me.Update_Button)
		Me.Controls.Add(Me.Cancel_Button)
		Me.Name = "EditCode"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Edit Code"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Cancel_Button As Button
	Friend WithEvents Update_Button As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents Code_TextBox As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Type_TextBox As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Description_TextBox As TextBox
	Friend WithEvents Label4 As Label
	Friend WithEvents Fix_TextBox As TextBox
	Friend WithEvents Delete_Button As Button
End Class
