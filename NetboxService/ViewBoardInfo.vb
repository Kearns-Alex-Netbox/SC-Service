'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ViewBoardInfo.vb
'
' Description: Displays all of the board information regarding the selected board.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class ViewBoardInfo

	Dim sqlapi As New SQL_API()
	Dim boardSerialNumber As String = ""
	Dim myReader As SqlDataReader

	Public Sub New(ByRef SerialNumber As String)
		InitializeComponent()
		boardSerialNumber = SerialNumber
	End Sub

	Private Sub ViewBoardInfo_Load() Handles MyBase.Load
		sqlapi._Username = UserName
		sqlapi._Password = Password
		Dim result As String = ""

		SetInfo(boardSerialNumber)
		CenterToParent()
	End Sub

	Private Sub ExitButton_Click() Handles ExitButton.Click
		Close()
	End Sub

	Private Sub SetInfo(ByRef boardSerialNo As String)
		Dim result As String = ""
		Dim record As Guid = Nothing
		Dim mycmd As New SqlCommand("", myConn)

		L_BoardSerialNumber.Text = boardSerialNo

		sqlapi.GetBoardBootloaderVersion(myCmd, myReader, boardSerialNo, BootloaderVersion.Text, result)

		sqlapi.GetBoardSoftwareVersion(myCmd, myReader, boardSerialNo, BoardVersion.Text, result)

		sqlapi.GetBoardLastUpdate(myCmd, myReader, boardSerialNo, LastUpdate.Text, record, result)

		sqlapi.GetBoardCurrentStatus(myCmd, myReader, boardSerialNo, BoardStatus.Text, result)

		sqlapi.GetBoardAudit(myCmd, myReader, boardSerialNo, RTB_Results, result)

		sqlapi.GetBoardCurrentType(myCmd, myReader, boardSerialNo, record, BoardType.Text, result)

		sqlapi.GetBoardHardwareVersion(myCmd, myReader, boardSerialNo, HardwareVersion.Text, result)

		sqlapi.GetMACAddress(myCmd, myReader, boardSerialNo, MACAddress.Text, result)

		sqlapi.GetCPUID(myCmd, myReader, boardSerialNo, CPUID.Text, result)
	End Sub

End Class