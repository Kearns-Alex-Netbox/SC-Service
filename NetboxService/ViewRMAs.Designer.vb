<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewRMAs
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
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.CB_Operand2 = New System.Windows.Forms.ComboBox()
		Me.CB_Operand1 = New System.Windows.Forms.ComboBox()
		Me.TB_Search2 = New System.Windows.Forms.TextBox()
		Me.CB_Display = New System.Windows.Forms.ComboBox()
		Me.CB_Search2 = New System.Windows.Forms.ComboBox()
		Me.RB_DescendingSort = New System.Windows.Forms.RadioButton()
		Me.B_First = New System.Windows.Forms.Button()
		Me.B_Last = New System.Windows.Forms.Button()
		Me.DGV_Items = New System.Windows.Forms.DataGridView()
		Me.L_Results = New System.Windows.Forms.Label()
		Me.TB_Search = New System.Windows.Forms.TextBox()
		Me.RB_AscendingSort = New System.Windows.Forms.RadioButton()
		Me.B_Search = New System.Windows.Forms.Button()
		Me.CB_Search = New System.Windows.Forms.ComboBox()
		Me.B_Sort = New System.Windows.Forms.Button()
		Me.B_ListAll = New System.Windows.Forms.Button()
		Me.CB_Sort = New System.Windows.Forms.ComboBox()
		Me.B_Close = New System.Windows.Forms.Button()
		Me.B_CreateExcel = New System.Windows.Forms.Button()
		Me.ViewDetails_Button = New System.Windows.Forms.Button()
		Me.Code_ComboBox = New System.Windows.Forms.ComboBox()
		Me.Code2_ComboBox = New System.Windows.Forms.ComboBox()
		Me.B_Previous = New System.Windows.Forms.Button()
		Me.B_Next = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.CB_Term_Operand = New System.Windows.Forms.ComboBox()
		CType(Me.DGV_Items, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'CB_Operand2
		'
		Me.CB_Operand2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Operand2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Operand2.FormattingEnabled = True
		Me.CB_Operand2.Items.AddRange(New Object() {"LIKE", "=", "<=", ">="})
		Me.CB_Operand2.Location = New System.Drawing.Point(230, 78)
		Me.CB_Operand2.Name = "CB_Operand2"
		Me.CB_Operand2.Size = New System.Drawing.Size(64, 28)
		Me.CB_Operand2.TabIndex = 6
		'
		'CB_Operand1
		'
		Me.CB_Operand1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Operand1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Operand1.FormattingEnabled = True
		Me.CB_Operand1.Items.AddRange(New Object() {"LIKE", "=", "<=", ">="})
		Me.CB_Operand1.Location = New System.Drawing.Point(230, 28)
		Me.CB_Operand1.Name = "CB_Operand1"
		Me.CB_Operand1.Size = New System.Drawing.Size(64, 28)
		Me.CB_Operand1.TabIndex = 2
		'
		'TB_Search2
		'
		Me.TB_Search2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TB_Search2.Location = New System.Drawing.Point(300, 79)
		Me.TB_Search2.Name = "TB_Search2"
		Me.TB_Search2.Size = New System.Drawing.Size(254, 26)
		Me.TB_Search2.TabIndex = 7
		'
		'CB_Display
		'
		Me.CB_Display.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CB_Display.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Display.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Display.FormattingEnabled = True
		Me.CB_Display.Items.AddRange(New Object() {"100", "250", "500"})
		Me.CB_Display.Location = New System.Drawing.Point(206, 469)
		Me.CB_Display.Name = "CB_Display"
		Me.CB_Display.Size = New System.Drawing.Size(69, 28)
		Me.CB_Display.TabIndex = 19
		'
		'CB_Search2
		'
		Me.CB_Search2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Search2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Search2.FormattingEnabled = True
		Me.CB_Search2.Location = New System.Drawing.Point(12, 78)
		Me.CB_Search2.Name = "CB_Search2"
		Me.CB_Search2.Size = New System.Drawing.Size(212, 28)
		Me.CB_Search2.TabIndex = 5
		'
		'RB_DescendingSort
		'
		Me.RB_DescendingSort.AutoSize = True
		Me.RB_DescendingSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RB_DescendingSort.Location = New System.Drawing.Point(403, 130)
		Me.RB_DescendingSort.Name = "RB_DescendingSort"
		Me.RB_DescendingSort.Size = New System.Drawing.Size(122, 24)
		Me.RB_DescendingSort.TabIndex = 11
		Me.RB_DescendingSort.TabStop = True
		Me.RB_DescendingSort.Text = "Descending"
		Me.RB_DescendingSort.UseVisualStyleBackColor = True
		'
		'B_First
		'
		Me.B_First.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.B_First.AutoSize = True
		Me.B_First.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_First.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_First.Location = New System.Drawing.Point(12, 468)
		Me.B_First.Name = "B_First"
		Me.B_First.Size = New System.Drawing.Size(50, 30)
		Me.B_First.TabIndex = 15
		Me.B_First.Text = "First"
		Me.B_First.UseVisualStyleBackColor = True
		'
		'B_Last
		'
		Me.B_Last.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.B_Last.AutoSize = True
		Me.B_Last.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Last.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_Last.Location = New System.Drawing.Point(150, 468)
		Me.B_Last.Name = "B_Last"
		Me.B_Last.Size = New System.Drawing.Size(50, 30)
		Me.B_Last.TabIndex = 18
		Me.B_Last.Text = "Last"
		Me.B_Last.UseVisualStyleBackColor = True
		'
		'DGV_Items
		'
		Me.DGV_Items.AllowUserToAddRows = False
		Me.DGV_Items.AllowUserToDeleteRows = False
		Me.DGV_Items.AllowUserToOrderColumns = True
		DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
		Me.DGV_Items.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
		Me.DGV_Items.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DGV_Items.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
		Me.DGV_Items.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
		Me.DGV_Items.BackgroundColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DGV_Items.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
		Me.DGV_Items.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle3.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.DGV_Items.DefaultCellStyle = DataGridViewCellStyle3
		Me.DGV_Items.Location = New System.Drawing.Point(12, 162)
		Me.DGV_Items.MultiSelect = False
		Me.DGV_Items.Name = "DGV_Items"
		Me.DGV_Items.ReadOnly = True
		Me.DGV_Items.Size = New System.Drawing.Size(816, 300)
		Me.DGV_Items.TabIndex = 0
		'
		'L_Results
		'
		Me.L_Results.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.L_Results.AutoSize = True
		Me.L_Results.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.L_Results.Location = New System.Drawing.Point(281, 473)
		Me.L_Results.Name = "L_Results"
		Me.L_Results.Size = New System.Drawing.Size(151, 20)
		Me.L_Results.TabIndex = 20
		Me.L_Results.Text = "Number of results"
		'
		'TB_Search
		'
		Me.TB_Search.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TB_Search.Location = New System.Drawing.Point(300, 29)
		Me.TB_Search.Name = "TB_Search"
		Me.TB_Search.Size = New System.Drawing.Size(254, 26)
		Me.TB_Search.TabIndex = 3
		'
		'RB_AscendingSort
		'
		Me.RB_AscendingSort.AutoSize = True
		Me.RB_AscendingSort.Checked = True
		Me.RB_AscendingSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RB_AscendingSort.Location = New System.Drawing.Point(258, 130)
		Me.RB_AscendingSort.Name = "RB_AscendingSort"
		Me.RB_AscendingSort.Size = New System.Drawing.Size(111, 24)
		Me.RB_AscendingSort.TabIndex = 10
		Me.RB_AscendingSort.TabStop = True
		Me.RB_AscendingSort.Text = "Ascending"
		Me.RB_AscendingSort.UseVisualStyleBackColor = True
		'
		'B_Search
		'
		Me.B_Search.AutoSize = True
		Me.B_Search.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Search.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_Search.Location = New System.Drawing.Point(630, 27)
		Me.B_Search.Name = "B_Search"
		Me.B_Search.Size = New System.Drawing.Size(70, 30)
		Me.B_Search.TabIndex = 13
		Me.B_Search.Text = "Search"
		Me.B_Search.UseVisualStyleBackColor = True
		'
		'CB_Search
		'
		Me.CB_Search.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Search.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Search.FormattingEnabled = True
		Me.CB_Search.Location = New System.Drawing.Point(12, 28)
		Me.CB_Search.Name = "CB_Search"
		Me.CB_Search.Size = New System.Drawing.Size(212, 28)
		Me.CB_Search.TabIndex = 1
		'
		'B_Sort
		'
		Me.B_Sort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Sort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_Sort.Location = New System.Drawing.Point(560, 127)
		Me.B_Sort.Name = "B_Sort"
		Me.B_Sort.Size = New System.Drawing.Size(140, 30)
		Me.B_Sort.TabIndex = 12
		Me.B_Sort.Text = "Sort"
		Me.B_Sort.UseVisualStyleBackColor = True
		'
		'B_ListAll
		'
		Me.B_ListAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_ListAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_ListAll.Location = New System.Drawing.Point(560, 77)
		Me.B_ListAll.Name = "B_ListAll"
		Me.B_ListAll.Size = New System.Drawing.Size(140, 30)
		Me.B_ListAll.TabIndex = 14
		Me.B_ListAll.Text = "List All/Refresh"
		Me.B_ListAll.UseVisualStyleBackColor = True
		'
		'CB_Sort
		'
		Me.CB_Sort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Sort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Sort.FormattingEnabled = True
		Me.CB_Sort.Location = New System.Drawing.Point(12, 128)
		Me.CB_Sort.Name = "CB_Sort"
		Me.CB_Sort.Size = New System.Drawing.Size(212, 28)
		Me.CB_Sort.TabIndex = 9
		'
		'B_Close
		'
		Me.B_Close.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Close.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_Close.Location = New System.Drawing.Point(721, 27)
		Me.B_Close.Name = "B_Close"
		Me.B_Close.Size = New System.Drawing.Size(109, 30)
		Me.B_Close.TabIndex = 23
		Me.B_Close.Text = "Close"
		Me.B_Close.UseVisualStyleBackColor = True
		'
		'B_CreateExcel
		'
		Me.B_CreateExcel.AutoSize = True
		Me.B_CreateExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_CreateExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_CreateExcel.Location = New System.Drawing.Point(721, 127)
		Me.B_CreateExcel.Name = "B_CreateExcel"
		Me.B_CreateExcel.Size = New System.Drawing.Size(109, 30)
		Me.B_CreateExcel.TabIndex = 21
		Me.B_CreateExcel.Text = "Create Excel"
		Me.B_CreateExcel.UseVisualStyleBackColor = True
		'
		'ViewDetails_Button
		'
		Me.ViewDetails_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.ViewDetails_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ViewDetails_Button.Location = New System.Drawing.Point(721, 77)
		Me.ViewDetails_Button.Name = "ViewDetails_Button"
		Me.ViewDetails_Button.Size = New System.Drawing.Size(109, 30)
		Me.ViewDetails_Button.TabIndex = 22
		Me.ViewDetails_Button.Text = "View Details"
		Me.ViewDetails_Button.UseVisualStyleBackColor = True
		'
		'Code_ComboBox
		'
		Me.Code_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Code_ComboBox.Enabled = False
		Me.Code_ComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Code_ComboBox.FormattingEnabled = True
		Me.Code_ComboBox.Location = New System.Drawing.Point(230, 28)
		Me.Code_ComboBox.Name = "Code_ComboBox"
		Me.Code_ComboBox.Size = New System.Drawing.Size(324, 28)
		Me.Code_ComboBox.TabIndex = 4
		Me.Code_ComboBox.Visible = False
		'
		'Code2_ComboBox
		'
		Me.Code2_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.Code2_ComboBox.Enabled = False
		Me.Code2_ComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Code2_ComboBox.FormattingEnabled = True
		Me.Code2_ComboBox.Location = New System.Drawing.Point(230, 78)
		Me.Code2_ComboBox.Name = "Code2_ComboBox"
		Me.Code2_ComboBox.Size = New System.Drawing.Size(324, 28)
		Me.Code2_ComboBox.TabIndex = 8
		Me.Code2_ComboBox.Visible = False
		'
		'B_Previous
		'
		Me.B_Previous.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.B_Previous.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Previous.BackgroundImage = Global.NetboxService.My.Resources.Resources.LeftArrow
		Me.B_Previous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.B_Previous.Enabled = False
		Me.B_Previous.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_Previous.Location = New System.Drawing.Point(68, 468)
		Me.B_Previous.Name = "B_Previous"
		Me.B_Previous.Size = New System.Drawing.Size(35, 30)
		Me.B_Previous.TabIndex = 16
		Me.B_Previous.UseVisualStyleBackColor = True
		'
		'B_Next
		'
		Me.B_Next.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.B_Next.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Next.BackgroundImage = Global.NetboxService.My.Resources.Resources.RightArrow
		Me.B_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.B_Next.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.B_Next.Location = New System.Drawing.Point(109, 468)
		Me.B_Next.Name = "B_Next"
		Me.B_Next.Size = New System.Drawing.Size(35, 30)
		Me.B_Next.TabIndex = 17
		Me.B_Next.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(12, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(103, 16)
		Me.Label1.TabIndex = 24
		Me.Label1.Text = "Search term 1"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(12, 59)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(103, 16)
		Me.Label2.TabIndex = 27
		Me.Label2.Text = "Search term 2"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(12, 109)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(36, 16)
		Me.Label4.TabIndex = 28
		Me.Label4.Text = "Sort"
		'
		'CB_Term_Operand
		'
		Me.CB_Term_Operand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CB_Term_Operand.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CB_Term_Operand.FormattingEnabled = True
		Me.CB_Term_Operand.Items.AddRange(New Object() {"AND", "OR"})
		Me.CB_Term_Operand.Location = New System.Drawing.Point(560, 28)
		Me.CB_Term_Operand.Name = "CB_Term_Operand"
		Me.CB_Term_Operand.Size = New System.Drawing.Size(64, 28)
		Me.CB_Term_Operand.TabIndex = 29
		'
		'ViewRMAs
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.ClientSize = New System.Drawing.Size(842, 510)
		Me.Controls.Add(Me.CB_Term_Operand)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.ViewDetails_Button)
		Me.Controls.Add(Me.B_Close)
		Me.Controls.Add(Me.B_CreateExcel)
		Me.Controls.Add(Me.CB_Operand2)
		Me.Controls.Add(Me.CB_Operand1)
		Me.Controls.Add(Me.TB_Search2)
		Me.Controls.Add(Me.CB_Display)
		Me.Controls.Add(Me.CB_Search2)
		Me.Controls.Add(Me.RB_DescendingSort)
		Me.Controls.Add(Me.B_First)
		Me.Controls.Add(Me.B_Last)
		Me.Controls.Add(Me.DGV_Items)
		Me.Controls.Add(Me.L_Results)
		Me.Controls.Add(Me.TB_Search)
		Me.Controls.Add(Me.B_Previous)
		Me.Controls.Add(Me.RB_AscendingSort)
		Me.Controls.Add(Me.B_Next)
		Me.Controls.Add(Me.B_Search)
		Me.Controls.Add(Me.CB_Search)
		Me.Controls.Add(Me.B_Sort)
		Me.Controls.Add(Me.B_ListAll)
		Me.Controls.Add(Me.CB_Sort)
		Me.Controls.Add(Me.Code_ComboBox)
		Me.Controls.Add(Me.Code2_ComboBox)
		Me.Name = "ViewRMAs"
		Me.Text = "View RMAs"
		CType(Me.DGV_Items, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents CB_Operand2 As ComboBox
	Friend WithEvents CB_Operand1 As ComboBox
	Friend WithEvents TB_Search2 As TextBox
	Friend WithEvents CB_Display As ComboBox
	Friend WithEvents CB_Search2 As ComboBox
	Friend WithEvents RB_DescendingSort As RadioButton
	Friend WithEvents B_First As Button
	Friend WithEvents B_Last As Button
	Friend WithEvents DGV_Items As DataGridView
	Friend WithEvents L_Results As Label
	Friend WithEvents TB_Search As TextBox
	Friend WithEvents B_Previous As Button
	Friend WithEvents RB_AscendingSort As RadioButton
	Friend WithEvents B_Next As Button
	Friend WithEvents B_Search As Button
	Friend WithEvents CB_Search As ComboBox
	Friend WithEvents B_Sort As Button
	Friend WithEvents B_ListAll As Button
	Friend WithEvents CB_Sort As ComboBox
	Friend WithEvents B_Close As Button
	Friend WithEvents B_CreateExcel As Button
	Friend WithEvents ViewDetails_Button As Button
	Friend WithEvents Code_ComboBox As ComboBox
	Friend WithEvents Code2_ComboBox As ComboBox
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents CB_Term_Operand As ComboBox
End Class
