<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewBillTypes
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
		Me.BillTypes_DataGridView = New System.Windows.Forms.DataGridView()
		Me.New_Button = New System.Windows.Forms.Button()
		Me.Select_Button = New System.Windows.Forms.Button()
		Me.Close_Button = New System.Windows.Forms.Button()
		CType(Me.BillTypes_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'BillTypes_DataGridView
		'
		Me.BillTypes_DataGridView.AllowUserToAddRows = False
		Me.BillTypes_DataGridView.AllowUserToDeleteRows = False
		DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
		Me.BillTypes_DataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
		Me.BillTypes_DataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BillTypes_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
		Me.BillTypes_DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
		Me.BillTypes_DataGridView.BackgroundColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.BillTypes_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.BillTypes_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.BillTypes_DataGridView.Location = New System.Drawing.Point(12, 12)
		Me.BillTypes_DataGridView.MultiSelect = False
		Me.BillTypes_DataGridView.Name = "BillTypes_DataGridView"
		Me.BillTypes_DataGridView.ReadOnly = True
		Me.BillTypes_DataGridView.Size = New System.Drawing.Size(516, 195)
		Me.BillTypes_DataGridView.TabIndex = 0
		'
		'New_Button
		'
		Me.New_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.New_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.New_Button.Location = New System.Drawing.Point(12, 213)
		Me.New_Button.Name = "New_Button"
		Me.New_Button.Size = New System.Drawing.Size(87, 29)
		Me.New_Button.TabIndex = 1
		Me.New_Button.Text = "New..."
		Me.New_Button.UseVisualStyleBackColor = True
		'
		'Select_Button
		'
		Me.Select_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Select_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Select_Button.Location = New System.Drawing.Point(348, 213)
		Me.Select_Button.Name = "Select_Button"
		Me.Select_Button.Size = New System.Drawing.Size(87, 29)
		Me.Select_Button.TabIndex = 2
		Me.Select_Button.Text = "Select"
		Me.Select_Button.UseVisualStyleBackColor = True
		'
		'Close_Button
		'
		Me.Close_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Close_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Close_Button.Location = New System.Drawing.Point(441, 213)
		Me.Close_Button.Name = "Close_Button"
		Me.Close_Button.Size = New System.Drawing.Size(87, 29)
		Me.Close_Button.TabIndex = 3
		Me.Close_Button.Text = "Close"
		Me.Close_Button.UseVisualStyleBackColor = True
		'
		'ViewBillTypes
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(540, 250)
		Me.Controls.Add(Me.BillTypes_DataGridView)
		Me.Controls.Add(Me.New_Button)
		Me.Controls.Add(Me.Select_Button)
		Me.Controls.Add(Me.Close_Button)
		Me.Name = "ViewBillTypes"
		Me.Text = "View Bill Types"
		CType(Me.BillTypes_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents BillTypes_DataGridView As DataGridView
	Friend WithEvents New_Button As Button
	Friend WithEvents Select_Button As Button
	Friend WithEvents Close_Button As Button
End Class
