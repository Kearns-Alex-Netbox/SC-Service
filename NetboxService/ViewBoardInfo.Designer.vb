<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewBoardInfo
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
		Me.RTB_Results = New System.Windows.Forms.RichTextBox()
		Me.CPUID = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.MACAddress = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.HardwareVersion = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.BoardType = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.LastUpdate = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.Label19 = New System.Windows.Forms.Label()
		Me.ExitButton = New System.Windows.Forms.Button()
		Me.BoardStatus = New System.Windows.Forms.TextBox()
		Me.Labelll = New System.Windows.Forms.Label()
		Me.BoardVersion = New System.Windows.Forms.TextBox()
		Me.label = New System.Windows.Forms.Label()
		Me.BootloaderVersion = New System.Windows.Forms.TextBox()
		Me.labell = New System.Windows.Forms.Label()
		Me.L_BoardSerialNumber = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'RTB_Results
		'
		Me.RTB_Results.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RTB_Results.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RTB_Results.Location = New System.Drawing.Point(187, 61)
		Me.RTB_Results.Name = "RTB_Results"
		Me.RTB_Results.ReadOnly = True
		Me.RTB_Results.Size = New System.Drawing.Size(366, 367)
		Me.RTB_Results.TabIndex = 18
		Me.RTB_Results.Text = ""
		'
		'CPUID
		'
		Me.CPUID.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CPUID.Location = New System.Drawing.Point(17, 355)
		Me.CPUID.Name = "CPUID"
		Me.CPUID.ReadOnly = True
		Me.CPUID.Size = New System.Drawing.Size(164, 23)
		Me.CPUID.TabIndex = 14
		Me.CPUID.TabStop = False
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Location = New System.Drawing.Point(13, 332)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(63, 20)
		Me.Label4.TabIndex = 13
		Me.Label4.Text = "CPU ID"
		'
		'MACAddress
		'
		Me.MACAddress.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MACAddress.Location = New System.Drawing.Point(17, 306)
		Me.MACAddress.Name = "MACAddress"
		Me.MACAddress.ReadOnly = True
		Me.MACAddress.Size = New System.Drawing.Size(164, 23)
		Me.MACAddress.TabIndex = 12
		Me.MACAddress.TabStop = False
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(13, 283)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(107, 20)
		Me.Label3.TabIndex = 11
		Me.Label3.Text = "MAC Address"
		'
		'HardwareVersion
		'
		Me.HardwareVersion.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.HardwareVersion.Location = New System.Drawing.Point(17, 257)
		Me.HardwareVersion.Name = "HardwareVersion"
		Me.HardwareVersion.ReadOnly = True
		Me.HardwareVersion.Size = New System.Drawing.Size(164, 23)
		Me.HardwareVersion.TabIndex = 10
		Me.HardwareVersion.TabStop = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Location = New System.Drawing.Point(13, 234)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(136, 20)
		Me.Label2.TabIndex = 9
		Me.Label2.Text = "Hardware Version"
		'
		'BoardType
		'
		Me.BoardType.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BoardType.Location = New System.Drawing.Point(17, 110)
		Me.BoardType.Name = "BoardType"
		Me.BoardType.ReadOnly = True
		Me.BoardType.Size = New System.Drawing.Size(164, 23)
		Me.BoardType.TabIndex = 4
		Me.BoardType.TabStop = False
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(13, 87)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(90, 20)
		Me.Label1.TabIndex = 3
		Me.Label1.Text = "Board Type"
		'
		'LastUpdate
		'
		Me.LastUpdate.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LastUpdate.Location = New System.Drawing.Point(17, 404)
		Me.LastUpdate.Name = "LastUpdate"
		Me.LastUpdate.ReadOnly = True
		Me.LastUpdate.Size = New System.Drawing.Size(164, 23)
		Me.LastUpdate.TabIndex = 16
		Me.LastUpdate.TabStop = False
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.Location = New System.Drawing.Point(13, 381)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(97, 20)
		Me.Label7.TabIndex = 15
		Me.Label7.Text = "Last Update"
		'
		'Label19
		'
		Me.Label19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label19.AutoSize = True
		Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label19.Location = New System.Drawing.Point(297, 38)
		Me.Label19.Name = "Label19"
		Me.Label19.Size = New System.Drawing.Size(133, 20)
		Me.Label19.TabIndex = 17
		Me.Label19.Text = "Board Comments"
		'
		'ExitButton
		'
		Me.ExitButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ExitButton.AutoSize = True
		Me.ExitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
		Me.ExitButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ExitButton.Location = New System.Drawing.Point(489, 434)
		Me.ExitButton.Name = "ExitButton"
		Me.ExitButton.Size = New System.Drawing.Size(64, 30)
		Me.ExitButton.TabIndex = 19
		Me.ExitButton.Text = "Close"
		Me.ExitButton.UseVisualStyleBackColor = True
		'
		'BoardStatus
		'
		Me.BoardStatus.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BoardStatus.Location = New System.Drawing.Point(17, 61)
		Me.BoardStatus.Name = "BoardStatus"
		Me.BoardStatus.ReadOnly = True
		Me.BoardStatus.Size = New System.Drawing.Size(164, 23)
		Me.BoardStatus.TabIndex = 2
		Me.BoardStatus.TabStop = False
		'
		'Labelll
		'
		Me.Labelll.AutoSize = True
		Me.Labelll.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Labelll.Location = New System.Drawing.Point(13, 38)
		Me.Labelll.Name = "Labelll"
		Me.Labelll.Size = New System.Drawing.Size(103, 20)
		Me.Labelll.TabIndex = 1
		Me.Labelll.Text = "Board Status"
		'
		'BoardVersion
		'
		Me.BoardVersion.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BoardVersion.Location = New System.Drawing.Point(17, 208)
		Me.BoardVersion.Name = "BoardVersion"
		Me.BoardVersion.ReadOnly = True
		Me.BoardVersion.Size = New System.Drawing.Size(164, 23)
		Me.BoardVersion.TabIndex = 8
		Me.BoardVersion.TabStop = False
		'
		'label
		'
		Me.label.AutoSize = True
		Me.label.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.label.Location = New System.Drawing.Point(13, 185)
		Me.label.Name = "label"
		Me.label.Size = New System.Drawing.Size(131, 20)
		Me.label.TabIndex = 7
		Me.label.Text = "Software Version"
		'
		'BootloaderVersion
		'
		Me.BootloaderVersion.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.BootloaderVersion.Location = New System.Drawing.Point(17, 159)
		Me.BootloaderVersion.Name = "BootloaderVersion"
		Me.BootloaderVersion.ReadOnly = True
		Me.BootloaderVersion.Size = New System.Drawing.Size(164, 23)
		Me.BootloaderVersion.TabIndex = 6
		Me.BootloaderVersion.TabStop = False
		'
		'labell
		'
		Me.labell.AutoSize = True
		Me.labell.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.labell.Location = New System.Drawing.Point(13, 136)
		Me.labell.Name = "labell"
		Me.labell.Size = New System.Drawing.Size(145, 20)
		Me.labell.TabIndex = 5
		Me.labell.Text = "Bootloader Version"
		'
		'L_BoardSerialNumber
		'
		Me.L_BoardSerialNumber.AutoSize = True
		Me.L_BoardSerialNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.L_BoardSerialNumber.Location = New System.Drawing.Point(12, 9)
		Me.L_BoardSerialNumber.Name = "L_BoardSerialNumber"
		Me.L_BoardSerialNumber.Size = New System.Drawing.Size(259, 29)
		Me.L_BoardSerialNumber.TabIndex = 0
		Me.L_BoardSerialNumber.Text = "Board Serial Number"
		'
		'ViewBoardInfo
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = True
		Me.ClientSize = New System.Drawing.Size(565, 476)
		Me.Controls.Add(Me.RTB_Results)
		Me.Controls.Add(Me.CPUID)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.MACAddress)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.HardwareVersion)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.BoardType)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.LastUpdate)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.Label19)
		Me.Controls.Add(Me.ExitButton)
		Me.Controls.Add(Me.BoardStatus)
		Me.Controls.Add(Me.Labelll)
		Me.Controls.Add(Me.BoardVersion)
		Me.Controls.Add(Me.label)
		Me.Controls.Add(Me.BootloaderVersion)
		Me.Controls.Add(Me.labell)
		Me.Controls.Add(Me.L_BoardSerialNumber)
		Me.Name = "ViewBoardInfo"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Board Info"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents RTB_Results As RichTextBox
	Friend WithEvents CPUID As TextBox
	Friend WithEvents Label4 As Label
	Friend WithEvents MACAddress As TextBox
	Friend WithEvents Label3 As Label
	Friend WithEvents HardwareVersion As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents BoardType As TextBox
	Friend WithEvents Label1 As Label
	Friend WithEvents LastUpdate As TextBox
	Friend WithEvents Label7 As Label
	Friend WithEvents Label19 As Label
	Friend WithEvents ExitButton As Button
	Friend WithEvents BoardStatus As TextBox
	Friend WithEvents Labelll As Label
	Friend WithEvents BoardVersion As TextBox
	Friend WithEvents label As Label
	Friend WithEvents BootloaderVersion As TextBox
	Friend WithEvents labell As Label
	Friend WithEvents L_BoardSerialNumber As Label
End Class
