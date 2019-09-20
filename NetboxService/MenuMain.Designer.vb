<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuMain
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
		Me.GeneralInfo_Button = New System.Windows.Forms.Button()
		Me.ViewBillType_Button = New System.Windows.Forms.Button()
		Me.Settings_Button = New System.Windows.Forms.Button()
		Me.Exit_Button = New System.Windows.Forms.Button()
		Me.ViewRMAs_Button = New System.Windows.Forms.Button()
		Me.P_Menu = New System.Windows.Forms.Panel()
		Me.B_ViewStatus = New System.Windows.Forms.Button()
		Me.ViewAddresses_Button = New System.Windows.Forms.Button()
		Me.ViewErrorCodes_Button = New System.Windows.Forms.Button()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.B_Close = New System.Windows.Forms.Button()
		Me.B_Cascade = New System.Windows.Forms.Button()
		Me.B_Minimize = New System.Windows.Forms.Button()
		Me.B_Tile = New System.Windows.Forms.Button()
		Me.P_Menu.SuspendLayout
		Me.Panel1.SuspendLayout
		Me.GroupBox1.SuspendLayout
		Me.SuspendLayout
		'
		'GeneralInfo_Button
		'
		Me.GeneralInfo_Button.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.GeneralInfo_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.GeneralInfo_Button.Location = New System.Drawing.Point(4, 13)
		Me.GeneralInfo_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.GeneralInfo_Button.Name = "GeneralInfo_Button"
		Me.GeneralInfo_Button.Size = New System.Drawing.Size(178, 30)
		Me.GeneralInfo_Button.TabIndex = 0
		Me.GeneralInfo_Button.Text = "New RMA"
		Me.GeneralInfo_Button.UseVisualStyleBackColor = true
		'
		'ViewBillType_Button
		'
		Me.ViewBillType_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ViewBillType_Button.Location = New System.Drawing.Point(4, 127)
		Me.ViewBillType_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.ViewBillType_Button.Name = "ViewBillType_Button"
		Me.ViewBillType_Button.Size = New System.Drawing.Size(178, 30)
		Me.ViewBillType_Button.TabIndex = 8
		Me.ViewBillType_Button.Text = "Billing Types"
		Me.ViewBillType_Button.UseVisualStyleBackColor = true
		'
		'Settings_Button
		'
		Me.Settings_Button.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
		Me.Settings_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Settings_Button.Location = New System.Drawing.Point(4, 642)
		Me.Settings_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.Settings_Button.Name = "Settings_Button"
		Me.Settings_Button.Size = New System.Drawing.Size(178, 30)
		Me.Settings_Button.TabIndex = 10
		Me.Settings_Button.Text = "Settings"
		Me.Settings_Button.UseVisualStyleBackColor = true
		'
		'Exit_Button
		'
		Me.Exit_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Exit_Button.Location = New System.Drawing.Point(394, 13)
		Me.Exit_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.Exit_Button.Name = "Exit_Button"
		Me.Exit_Button.Size = New System.Drawing.Size(82, 29)
		Me.Exit_Button.TabIndex = 10
		Me.Exit_Button.Text = "Exit"
		Me.Exit_Button.UseVisualStyleBackColor = true
		'
		'ViewRMAs_Button
		'
		Me.ViewRMAs_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ViewRMAs_Button.Location = New System.Drawing.Point(4, 51)
		Me.ViewRMAs_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.ViewRMAs_Button.Name = "ViewRMAs_Button"
		Me.ViewRMAs_Button.Size = New System.Drawing.Size(178, 30)
		Me.ViewRMAs_Button.TabIndex = 6
		Me.ViewRMAs_Button.Text = "RMA List"
		Me.ViewRMAs_Button.UseVisualStyleBackColor = true
		'
		'P_Menu
		'
		Me.P_Menu.AutoScroll = true
		Me.P_Menu.BackColor = System.Drawing.Color.Silver
		Me.P_Menu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.P_Menu.Controls.Add(Me.B_ViewStatus)
		Me.P_Menu.Controls.Add(Me.ViewAddresses_Button)
		Me.P_Menu.Controls.Add(Me.ViewErrorCodes_Button)
		Me.P_Menu.Controls.Add(Me.Settings_Button)
		Me.P_Menu.Controls.Add(Me.GeneralInfo_Button)
		Me.P_Menu.Controls.Add(Me.ViewRMAs_Button)
		Me.P_Menu.Controls.Add(Me.ViewBillType_Button)
		Me.P_Menu.Dock = System.Windows.Forms.DockStyle.Left
		Me.P_Menu.Location = New System.Drawing.Point(0, 0)
		Me.P_Menu.Name = "P_Menu"
		Me.P_Menu.Size = New System.Drawing.Size(190, 687)
		Me.P_Menu.TabIndex = 0
		'
		'B_ViewStatus
		'
		Me.B_ViewStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.B_ViewStatus.Location = New System.Drawing.Point(4, 203)
		Me.B_ViewStatus.Margin = New System.Windows.Forms.Padding(4)
		Me.B_ViewStatus.Name = "B_ViewStatus"
		Me.B_ViewStatus.Size = New System.Drawing.Size(178, 30)
		Me.B_ViewStatus.TabIndex = 11
		Me.B_ViewStatus.Text = "Status List"
		Me.B_ViewStatus.UseVisualStyleBackColor = true
		'
		'ViewAddresses_Button
		'
		Me.ViewAddresses_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ViewAddresses_Button.Location = New System.Drawing.Point(4, 165)
		Me.ViewAddresses_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.ViewAddresses_Button.Name = "ViewAddresses_Button"
		Me.ViewAddresses_Button.Size = New System.Drawing.Size(178, 30)
		Me.ViewAddresses_Button.TabIndex = 9
		Me.ViewAddresses_Button.Text = "Address List"
		Me.ViewAddresses_Button.UseVisualStyleBackColor = true
		'
		'ViewErrorCodes_Button
		'
		Me.ViewErrorCodes_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.ViewErrorCodes_Button.Location = New System.Drawing.Point(4, 89)
		Me.ViewErrorCodes_Button.Margin = New System.Windows.Forms.Padding(4)
		Me.ViewErrorCodes_Button.Name = "ViewErrorCodes_Button"
		Me.ViewErrorCodes_Button.Size = New System.Drawing.Size(178, 30)
		Me.ViewErrorCodes_Button.TabIndex = 7
		Me.ViewErrorCodes_Button.Text = "Error Codes"
		Me.ViewErrorCodes_Button.UseVisualStyleBackColor = true
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = true
		Me.Panel1.BackColor = System.Drawing.Color.Silver
		Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Panel1.Controls.Add(Me.GroupBox1)
		Me.Panel1.Controls.Add(Me.Exit_Button)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.Panel1.Location = New System.Drawing.Point(190, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(904, 60)
		Me.Panel1.TabIndex = 1
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.B_Close)
		Me.GroupBox1.Controls.Add(Me.B_Cascade)
		Me.GroupBox1.Controls.Add(Me.B_Minimize)
		Me.GroupBox1.Controls.Add(Me.B_Tile)
		Me.GroupBox1.Location = New System.Drawing.Point(4, -1)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(383, 50)
		Me.GroupBox1.TabIndex = 0
		Me.GroupBox1.TabStop = false
		Me.GroupBox1.Text = "Window Buttons"
		'
		'B_Close
		'
		Me.B_Close.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.B_Close.Location = New System.Drawing.Point(295, 14)
		Me.B_Close.Name = "B_Close"
		Me.B_Close.Size = New System.Drawing.Size(82, 29)
		Me.B_Close.TabIndex = 3
		Me.B_Close.Text = "Close"
		Me.B_Close.UseVisualStyleBackColor = true
		'
		'B_Cascade
		'
		Me.B_Cascade.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.B_Cascade.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.B_Cascade.Location = New System.Drawing.Point(6, 14)
		Me.B_Cascade.Name = "B_Cascade"
		Me.B_Cascade.Size = New System.Drawing.Size(107, 29)
		Me.B_Cascade.TabIndex = 0
		Me.B_Cascade.Text = "Cascade"
		Me.B_Cascade.UseVisualStyleBackColor = true
		'
		'B_Minimize
		'
		Me.B_Minimize.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.B_Minimize.Location = New System.Drawing.Point(207, 14)
		Me.B_Minimize.Name = "B_Minimize"
		Me.B_Minimize.Size = New System.Drawing.Size(82, 29)
		Me.B_Minimize.TabIndex = 2
		Me.B_Minimize.Text = "Minimize"
		Me.B_Minimize.UseVisualStyleBackColor = true
		'
		'B_Tile
		'
		Me.B_Tile.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!)
		Me.B_Tile.Location = New System.Drawing.Point(119, 14)
		Me.B_Tile.Name = "B_Tile"
		Me.B_Tile.Size = New System.Drawing.Size(82, 29)
		Me.B_Tile.TabIndex = 1
		Me.B_Tile.Text = "Tile"
		Me.B_Tile.UseVisualStyleBackColor = true
		'
		'MenuMain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = true
		Me.ClientSize = New System.Drawing.Size(1094, 687)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.P_Menu)
		Me.Name = "MenuMain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Main Menu"
		Me.P_Menu.ResumeLayout(false)
		Me.Panel1.ResumeLayout(false)
		Me.GroupBox1.ResumeLayout(false)
		Me.ResumeLayout(false)

End Sub

	Friend WithEvents GeneralInfo_Button As Button
	Friend WithEvents ViewBillType_Button As Button
	Friend WithEvents Settings_Button As Button
	Friend WithEvents Exit_Button As Button
	Friend WithEvents ViewRMAs_Button As Button
	Friend WithEvents P_Menu As Panel
	Friend WithEvents Panel1 As Panel
	Friend WithEvents GroupBox1 As GroupBox
	Friend WithEvents B_Close As Button
	Friend WithEvents B_Cascade As Button
	Friend WithEvents B_Minimize As Button
	Friend WithEvents B_Tile As Button
	Friend WithEvents ViewErrorCodes_Button As Button
	Friend WithEvents ViewAddresses_Button As Button
	Friend WithEvents B_ViewStatus As Button
End Class
