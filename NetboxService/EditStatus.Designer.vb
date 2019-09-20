<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditStatus
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
		Me.Update_Button = New System.Windows.Forms.Button()
		Me.Cancel_Button = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Status_TextBox = New System.Windows.Forms.TextBox()
		Me.Order_Numeric = New System.Windows.Forms.NumericUpDown()
		Me.Label1 = New System.Windows.Forms.Label()
		CType(Me.Order_Numeric,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'Update_Button
		'
		Me.Update_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Update_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Update_Button.Location = New System.Drawing.Point(137, 34)
		Me.Update_Button.Name = "Update_Button"
		Me.Update_Button.Size = New System.Drawing.Size(87, 29)
		Me.Update_Button.TabIndex = 3
		Me.Update_Button.Text = "Update"
		Me.Update_Button.UseVisualStyleBackColor = true
		'
		'Cancel_Button
		'
		Me.Cancel_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Cancel_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Cancel_Button.Location = New System.Drawing.Point(230, 34)
		Me.Cancel_Button.Name = "Cancel_Button"
		Me.Cancel_Button.Size = New System.Drawing.Size(87, 29)
		Me.Cancel_Button.TabIndex = 5
		Me.Cancel_Button.Text = "Cancel"
		Me.Cancel_Button.UseVisualStyleBackColor = true
		'
		'Label3
		'
		Me.Label3.AutoSize = true
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label3.Location = New System.Drawing.Point(12, 9)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(56, 20)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Status"
		'
		'Status_TextBox
		'
		Me.Status_TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.Status_TextBox.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Status_TextBox.Location = New System.Drawing.Point(74, 8)
		Me.Status_TextBox.Name = "Status_TextBox"
		Me.Status_TextBox.Size = New System.Drawing.Size(243, 23)
		Me.Status_TextBox.TabIndex = 1
		'
		'Order_Numeric
		'
		Me.Order_Numeric.Font = New System.Drawing.Font("Consolas", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Order_Numeric.Location = New System.Drawing.Point(74, 37)
		Me.Order_Numeric.Name = "Order_Numeric"
		Me.Order_Numeric.Size = New System.Drawing.Size(59, 22)
		Me.Order_Numeric.TabIndex = 6
		'
		'Label1
		'
		Me.Label1.AutoSize = true
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Label1.Location = New System.Drawing.Point(12, 38)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(49, 20)
		Me.Label1.TabIndex = 7
		Me.Label1.Text = "Order"
		'
		'EditStatus
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(329, 75)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Order_Numeric)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.Status_TextBox)
		Me.Controls.Add(Me.Update_Button)
		Me.Controls.Add(Me.Cancel_Button)
		Me.Name = "EditStatus"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Edit Status"
		CType(Me.Order_Numeric,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub
	Friend WithEvents Update_Button As Button
	Friend WithEvents Cancel_Button As Button
	Friend WithEvents Label3 As Label
	Friend WithEvents Status_TextBox As TextBox
	Friend WithEvents Order_Numeric As NumericUpDown
	Friend WithEvents Label1 As Label
End Class
