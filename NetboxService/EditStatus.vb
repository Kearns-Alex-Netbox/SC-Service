'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: EditStatus.vb
'
' Description: Allows the User to Edit or create an entry in our Status list. Used for when filling our of the unit and what the company
'	has decided on what to do. Updating a status will update it for both Evaluation And Approval Duplicate Status are Not allowed.
'
' Status: The name of the Status.
'
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class EditStatus
	Dim thisStatus As String
	Dim isNew As Boolean = False

	Public Sub New(ByRef Status As String)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		If Status <> "NEW" Then
			thisStatus = Status.Substring(Status.IndexOf("-") + 1).Trim
		Else
			thisStatus = Status
		End If

	End Sub

	Private Sub EditCode_Load() Handles MyBase.Load
		'Check the passed in id [set as thisCode] to see if we are creating a new entry or not.
		If thisStatus = "NEW" Then
			isNew = True
			Delete_Button.Enabled = False
		Else
			'Populate all of the information we have.
			Status_TextBox.Text = thisStatus
		End If
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim cmd As New SqlCommand("", myConn)
		Dim status As String = Status_TextBox.Text

		Try
			'Check to see if we are creating a new entry or updating an old one.
			If isNew = True Then
				'Add new entry to the database
				cmd.CommandText = "INSERT INTO " & TABLE_RMASTATUS & " ([" & DB_HEADER_ID & "],[" & DB_HEADER_STATUS & "],[" & DB_HEADER_ISAPPROVAL & "]) VALUES(NEWID(),'Evaluated - " & status & "','1')"
				cmd.ExecuteNonQuery()

				cmd.CommandText = "INSERT INTO " & TABLE_RMASTATUS & " ([" & DB_HEADER_ID & "],[" & DB_HEADER_STATUS & "],[" & DB_HEADER_ISAPPROVAL & "]) VALUES(NEWID(),'Approved - " & status & "','1')"
				cmd.ExecuteNonQuery()
			Else
				'Update the code in the database
				cmd.CommandText = "UPDATE " & TABLE_RMASTATUS & " SET [" & DB_HEADER_STATUS & "] = 'Evaluated - " & status & "' WHERE [" & DB_HEADER_STATUS & "] = 'Evaluated - " & thisStatus & "'"
				cmd.ExecuteNonQuery()

				cmd.CommandText = "UPDATE " & TABLE_RMASTATUS & " SET [" & DB_HEADER_STATUS & "] = 'Approved - " & status & "' WHERE [" & DB_HEADER_STATUS & "] = 'Approved - " & thisStatus & "'"
				cmd.ExecuteNonQuery()
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Update the table becuase we have either added a new entry or updated an existing one.
		ViewStatusList.UpdateTable()
		Close()
	End Sub

	Private Sub Delete_Button_Click() Handles Delete_Button.Click
		Try
			'Confirm with the user that they want to delete the record.
			Select Case MsgBox("Are you sure you want to DELETE Status " & thisStatus & "? It will be deleted for both Evaluation and Approval.", MsgBoxStyle.YesNo, "Confirmation")
				Case MsgBoxResult.No
					Return
				Case MsgBoxResult.Yes
					'Grab the GUID of both records
					Dim myCmd As New SqlCommand("SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = 'Evaluated - " & thisStatus & "'", myConn)
					Dim evaluation As String = myCmd.ExecuteScalar.ToString

					myCmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = 'Approved - " & thisStatus & "'"
					Dim approval As String = myCmd.ExecuteScalar.ToString

					'Delete the record from the database
					myCmd.CommandText = "DELETE FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_ID & "] = '" & evaluation & "' OR [" & DB_HEADER_ID & "] = '" & approval & "'"
					myCmd.ExecuteNonQuery()

					'Now we need to update our records that use this information because it will no longer exist.
					myCmd.CommandText = "UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_STATUSID & "] = NULL WHERE [" & DB_HEADER_STATUSID & "] = '" & evaluation & "' OR [" & DB_HEADER_STATUSID & "] = '" & approval & "'"
					myCmd.ExecuteNonQuery()
			End Select

		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Update the table becuase we have deleted a record.
		ViewStatusList.UpdateTable()
		Close()
	End Sub

End Class