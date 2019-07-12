'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: EditCode.vb
'
' Description: Allows the User to Edit or create an entry in our Code list. Used for when filling our the customers report and our own 
'	evaluation of the unit. Not all of the textboxes need to be filled out. Duplicate Codes are Not allowed.
'
' Code: The name of the code.
' Type: What type of code is it. Error/Reliability/Panel
' Description: More detailed description of the code.
' Fix: Description on what to be done to fix the issue.
'
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class EditCode
	Dim thisCode As String
	Dim thisGUID As String
	Dim isNew As Boolean = False

	Public Sub New(ByRef code As String)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		thisCode = code
	End Sub

	Private Sub EditCode_Load() Handles MyBase.Load
		'Check the passed in id [set as thisCode] to see if we are creating a new entry or not.
		If thisCode = "NEW" Then
			isNew = True
			Delete_Button.Enabled = False
		Else
			Dim resultTable As New DataTable()

			'Get all of the information we have on this record.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & thisCode & "'", myConn)
			resultTable.Load(myCmd.ExecuteReader)

			'Populate all of the information we have.
			Code_TextBox.Text = resultTable(0)(DB_HEADER_CODE).ToString
			Type_TextBox.Text = resultTable(0)(DB_HEADER_TYPE).ToString
			Description_TextBox.Text = resultTable(0)(DB_HEADER_DESCRIPTION).ToString
			Fix_TextBox.Text = resultTable(0)(DB_HEADER_FIX).ToString
			thisGUID = resultTable(0)(DB_HEADER_ID).ToString
		End If
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim cmd As New SqlCommand("", myConn)
		Dim code As String = Code_TextBox.Text
		Dim type As String = Type_TextBox.Text
		Dim description As String = Description_TextBox.Text
		Dim fix As String = Fix_TextBox.Text

		Try
			'Check to see if we are creating a new entry or updating an old one.
			If isNew = True Then
				'Add new entry to the database
				cmd.CommandText = "INSERT INTO " & TABLE_RMACODES & " ([" & DB_HEADER_ID & "],[" & DB_HEADER_CODE & "],[" & DB_HEADER_TYPE & "],[" & DB_HEADER_DESCRIPTION & "],[" & DB_HEADER_FIX & "]) VALUES(NEWID(),'" & code & "','" & type & "','" & description & "','" & fix & "')"
				cmd.ExecuteNonQuery()
			Else
				'Update the code in the database
				cmd.CommandText = "UPDATE " & TABLE_RMACODES & " SET [" & DB_HEADER_TYPE & "] = '" & type & "', [" & DB_HEADER_CODE & "] = '" & code & "', [" & DB_HEADER_DESCRIPTION & "] = '" & description & "', [" & DB_HEADER_FIX & "] = '" & fix & "' WHERE [" & DB_HEADER_ID & "] = '" & thisGUID & "'"
				cmd.ExecuteNonQuery()
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Update the table becuase we have either added a new entry or updated an existing one.
		ViewErrorCodes.UpdateTable()
		Close()
	End Sub

	Private Sub Delete_Button_Click() Handles Delete_Button.Click
		Try
			'Confirm with the user that they want to delete the record.
			Select Case MsgBox("Are you sure you want to DELETE code " & Code_TextBox.Text & "?", MsgBoxStyle.YesNo, "Confirmation")
				Case MsgBoxResult.No
					Return
				Case MsgBoxResult.Yes
					'Delete the record from the database
					Dim myCmd As New SqlCommand("DELETE FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_ID & "] = '" & thisGUID & "'", myConn)
					myCmd.ExecuteNonQuery()

					'Delete the record from the database
					myCmd.CommandText = "DELETE FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMACODESID & "] = '" & thisGUID & "'"
					myCmd.ExecuteNonQuery()
			End Select

		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Update the table becuase we have deleted a record.
		ViewErrorCodes.UpdateTable()
		Close()
	End Sub

End Class