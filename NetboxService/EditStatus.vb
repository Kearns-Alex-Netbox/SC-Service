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
	Dim thisOrder As Integer
	Dim isNew As Boolean = False

	Public Sub New(ByRef Status As String, byref order As Integer)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		thisStatus = Status
		thisOrder = order
	End Sub

	Private Sub EditCode_Load() Handles MyBase.Load
		'Check the passed in id [set as thisCode] to see if we are creating a new entry or not.
		If thisStatus = "NEW" Then
			isNew = True
		Else
			'Populate all of the information we have.
			Status_TextBox.Text = thisStatus
			Order_Numeric.Value = thisOrder
		End If
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim cmd As New SqlCommand("", myConn)
		Dim status As String = Status_TextBox.Text
		Dim order As Integer = Order_Numeric.Value

		Try
			'Check to see if we are creating a new entry or updating an old one.
			If isNew = True Then
				'Add new entry to the database
				cmd.CommandText = "INSERT INTO " & TABLE_RMASTATUS & " ([" & DB_HEADER_ID & "],[" & DB_HEADER_STATUS & "],[" & DB_HEADER_ORDER & "]) VALUES(NEWID(),'" & status & "', " & order & ")"
				cmd.ExecuteNonQuery()
			Else
				'Update the code in the database
				cmd.CommandText = "UPDATE " & TABLE_RMASTATUS & " SET [" & DB_HEADER_STATUS & "] = '" & status & "', [" & DB_HEADER_ORDER & "] = " & order & " WHERE [" & DB_HEADER_STATUS & "] = '" & thisStatus & "'"
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

End Class