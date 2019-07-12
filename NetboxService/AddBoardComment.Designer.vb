<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddBoardComment
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
		Me.BoardSNO_TextBox = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.BoardComment_TextBox = New System.Windows.Forms.TextBox()
		Me.Close_Button = New System.Windows.Forms.Button()
		Me.AddBoardComment_Button = New System.Windows.Forms.Button()
		Me.ResultStatus = New System.Windows.Forms.TextBox()
		Me.SuspendLayout()
		'
		'BoardSNO_TextBox
		'
		Me.BoardSNO_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BoardSNO_TextBox.Location = New System.Drawing.Point(17, 33)
		Me.BoardSNO_TextBox.Margin = New System.Windows.Forms.Padding(4)
		Me.BoardSNO_TextBox.Name = "BoardSNO_TextBox"
		Me.BoardSNO_TextBox.Size = New System.Drawing.Size(253, 26)
		Me.BoardSNO_TextBox.TabIndex = 33
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(13, 9)
		Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(175, 20)
		Me.Label3.TabIndex = 34
		Me.Label3.Text = "Board Serial Number"
		'
		'BoardComment_TextBox
		'
		Me.BoardComment_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BoardComment_TextBox.Location = New System.Drawing.Point(17, 67)
		Me.BoardComment_TextBox.Margin = New System.Windows.Forms.Padding(4)
		Me.BoardComment_TextBox.Multiline = True
		Me.BoardComment_TextBox.Name = "BoardComment_TextBox"
		Me.BoardComment_TextBox.Size = New System.Drawing.Size(253, 147)
		Me.BoardComment_TextBox.TabIndex = 35
		'
		'Close_Button
		'
		Me.Close_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Close_Button.Location = New System.Drawing.Point(208, 296)
		Me.Close_Button.Name = "Close_Button"
		Me.Close_Button.Size = New System.Drawing.Size(64, 29)
		Me.Close_Button.TabIndex = 38
		Me.Close_Button.Text = "Close"
		Me.Close_Button.UseVisualStyleBackColor = True
		'
		'AddBoardComment_Button
		'
		Me.AddBoardComment_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AddBoardComment_Button.Location = New System.Drawing.Point(12, 296)
		Me.AddBoardComment_Button.Name = "AddBoardComment_Button"
		Me.AddBoardComment_Button.Size = New System.Drawing.Size(156, 29)
		Me.AddBoardComment_Button.TabIndex = 39
		Me.AddBoardComment_Button.Text = "Add Board Comment"
		Me.AddBoardComment_Button.UseVisualStyleBackColor = True
		'
		'ResultStatus
		'
		Me.ResultStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ResultStatus.Location = New System.Drawing.Point(17, 221)
		Me.ResultStatus.Multiline = True
		Me.ResultStatus.Name = "ResultStatus"
		Me.ResultStatus.ReadOnly = True
		Me.ResultStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.ResultStatus.Size = New System.Drawing.Size(255, 69)
		Me.ResultStatus.TabIndex = 58
		Me.ResultStatus.TabStop = False
		'
		'AddBoardComment
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(284, 337)
		Me.Controls.Add(Me.ResultStatus)
		Me.Controls.Add(Me.AddBoardComment_Button)
		Me.Controls.Add(Me.Close_Button)
		Me.Controls.Add(Me.BoardComment_TextBox)
		Me.Controls.Add(Me.BoardSNO_TextBox)
		Me.Controls.Add(Me.Label3)
		Me.Name = "AddBoardComment"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "AddBoardComment"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents BoardSNO_TextBox As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents BoardComment_TextBox As TextBox
	Friend WithEvents Close_Button As Button
	Friend WithEvents AddBoardComment_Button As Button
	Friend WithEvents ResultStatus As TextBox
End Class
