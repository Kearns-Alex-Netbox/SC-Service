'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: EditBillingType.vb
'
' Description: Allows the User to Edit or create an entry in our Billing Type list. Used for billing. Not all of the textboxes need to be 
'	filled out. Duplicate Billing type Names are Not allowed.
'
' Name: The name for the billing type.
' Needs Address: Indicates if this type needs a billing address or not.
' Same as Shipping: Indicates if this type will use whatever the shipping address is or not.
'
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class EditBillingType
	Dim thisName As String
	Dim isNew As Boolean = False
	Dim thisGUID As String

	Public Sub New(ByRef name As String)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		thisName = name
	End Sub

	Private Sub EditCode_Load() Handles MyBase.Load
		'Check the passed in id [set as thisName] to see if we are creating a new entry or not.
		If thisName = "NEW" Then
			isNew = True
			Delete_Button.Enabled = False
		Else
			Dim resultTable As New DataTable()

			'Grab all of the information that we have on this record.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_BILLTYPE & "] = '" & thisName & "'", myConn)
			resultTable.Load(myCmd.ExecuteReader)

			'Populate all of the information that we have.
			Name_TextBox.Text = resultTable(0)(DB_HEADER_BILLTYPE).ToString
			NeedsAddress_CheckBox.Checked = resultTable(0)(DB_HEADER_NEEDSADDRESS).ToString
			SameAsShipping_CheckBox.Checked = resultTable(0)(DB_HEADER_SAMEASSHIPPING).ToString
			thisGUID = resultTable(0)(DB_HEADER_ID).ToString

			'Prevent the user from changing the code number because this is what we use to distinguish the record.
			Name_TextBox.Enabled = False
		End If
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim cmd As New SqlCommand("", myConn)
		Dim name As String = Name_TextBox.Text
		Dim needAddress As Boolean = NeedsAddress_CheckBox.Checked
		Dim sameAsShipping As Boolean = SameAsShipping_CheckBox.Checked

		Try
			'Check to see if we are creating a new entry or updating an old one.
			If isNew = True Then
				'Add new entry to the database
				cmd.CommandText = "INSERT INTO " & TABLE_RMABILLTYPE & " ([" & DB_HEADER_ID & "],[" & DB_HEADER_BILLTYPE & "],[" & DB_HEADER_NEEDSADDRESS & "],[" & DB_HEADER_SAMEASSHIPPING & "]) VALUES(NEWID(),'" & name & "','" & needAddress & "','" & sameAsShipping & "')"
				cmd.ExecuteNonQuery()
			Else
				'Update the code in the database
				cmd.CommandText = "UPDATE " & TABLE_RMABILLTYPE & " SET [" & DB_HEADER_NEEDSADDRESS & "] = '" & needAddress & "', [" & DB_HEADER_SAMEASSHIPPING & "] = '" & sameAsShipping & "' WHERE [" & DB_HEADER_BILLTYPE & "] = '" & name & "'"
				cmd.ExecuteNonQuery()
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Update the table becuase we have either added a new entry or updated an existing one.
		ViewBillTypes.UpdateTable()
		Close()
	End Sub

	Private Sub Delete_Button_Click() Handles Delete_Button.Click
		Try
			'Confirm with the user that they want to delete the record.
			Select Case MsgBox("Are you sure you want to DELETE " & Name & "?", MsgBoxStyle.YesNo, "Confirmation")
				Case MsgBoxResult.No
					Return

				Case MsgBoxResult.Yes
					'Delete the record from the database
					Dim myCmd As New SqlCommand("DELETE FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_BILLTYPE & "] = '" & Name_TextBox.Text & "'", myConn)

					myCmd.ExecuteNonQuery()

					'Now we need to update our records that use this information because it will no longer exist.
					myCmd.CommandText = "UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_BILLTYPEID & "] = NULL WHERE [" & DB_HEADER_BILLTYPEID & "] = '" & thisGUID & "'"
					myCmd.ExecuteNonQuery()

			End Select

		Catch ex As Exception
			MsgBox(ex.Message)
		End Try

		'Update the table becuase we have deleted a record.
		ViewBillTypes.UpdateTable()
		Close()
	End Sub

End Class