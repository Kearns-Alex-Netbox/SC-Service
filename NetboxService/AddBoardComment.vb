Imports System.Data.SqlClient

Public Class AddBoardComment
	Dim myCmd As New SqlCommand("", myConn)
	Dim systemID As String = ""
	Public response As String = ""

	Public Sub New(ByRef id As String)
		InitializeComponent()
		systemID = id
	End Sub

	Private Sub AddBoardComment_Load() Handles MyBase.Load

	End Sub

	Private Sub AddBoardCommentButton_Click() Handles AddBoardComment_Button.Click
		Dim record As Guid = Nothing
		Dim result As String = ""

		If BoardSNO_TextBox.Text.Length <> 0 And BoardComment_TextBox.Text.Length <> 0 Then

			' grab the system SNO if we can
			myCmd.CommandText = "SELECT [" & DB_HEADER_SERIALNUMBER & "] FROM " & TABLE_SYSTEM & " WHERE [systemid] = '" & systemID & "'"
			Dim systemSNO = myCmd.ExecuteScalar()

			' create the text that we are going to attach
			response = systemSNO & ":" & BoardSNO_TextBox.Text & " - " & BoardComment_TextBox.Text

			' add the board comment
			If sqlapi.AddBoardComment(myCmd, BoardSNO_TextBox.Text, response, UserName, record, result) = False Then
				MsgBox(result)
				Return
			End If

			' we need to make sure we are part of a system first.
			If systemID.Length <> 0 Then
				' add the system comment
				myCmd.CommandText = "INSERT INTO dbo.SystemAudit(id,[dbo.System.systemid],Comment,LastUpdate, [User]) VALUES(NEWID(), '" _
					& systemID & "','" & response & "',GETDATE(),'" & UserName & "')"
				myCmd.ExecuteNonQuery()
			End If
		Else
			MsgBox("Make sure Board Serial Number and Comment text fields are filled out.")
			Return
		End If

		ResultStatus.Text = "Comment was added to Board '" & BoardSNO_TextBox.Text & "'"
		BoardSNO_TextBox.Text = ""

		Close()
	End Sub

	Private Sub CancelButton_Click() Handles Close_Button.Click
		Close()
	End Sub

End Class