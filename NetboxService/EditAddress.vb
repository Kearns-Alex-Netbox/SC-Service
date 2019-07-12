'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: EditAddress.vb
'
' Description: Allows the User to Edit or create an entry in our Address list. Used for shipping and billing. Not all of the textboxes
'	need to be filled out. Duplicate Company Names are not allowed.
'
' Company: The name of the company.
' Address: The street address of the compnay.
' City: The city where the company is located.
' State: The state where the compnay is located.
' Zip: The zip code of the address.
' Country: The country where the company is located.
' Phone: The primary phone number for the company.
' Contact: The information for the contact dealing with the RMA
'		Name: The name of the contact.
'		Number: The contact's number.
'		E-mail: The contact's E-mail.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class EditAddress
	Dim thisGUID As String
	Dim isNew As Boolean = False

	Public Sub New(ByRef id As String)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		thisGUID = id
	End Sub

	Private Sub EditAddress_Load() Handles MyBase.Load
		'Check the passed in id [set to thisGUID] to see if we are editing or creating.
		If thisGUID = "NEW" Then
			isNew = True
			Delete_Button.Enabled = False
		Else
			Dim resultTable As New DataTable()

			'Get all of the existing information from the database.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & thisGUID & "'", myConn)
			resultTable.Load(myCmd.ExecuteReader)

			'Fill in all of the information.
			Company_TextBox.Text = resultTable(0)(DB_HEADER_COMPANY).ToString
			Address_TextBox.Text = resultTable(0)(DB_HEADER_ADDRESS).ToString
			City_TextBox.Text = resultTable(0)(DB_HEADER_CITY).ToString
			State_TextBox.Text = resultTable(0)(DB_HEADER_STATE).ToString
			Zip_TextBox.Text = resultTable(0)(DB_HEADER_ZIPCODE).ToString
			Country_TextBox.Text = resultTable(0)(DB_HEADER_COUNTRY).ToString
			Phone_TextBox.Text = resultTable(0)(DB_HEADER_PHONE).ToString
			ContactName_TextBox.Text = resultTable(0)(DB_HEADER_CONTACTNAME).ToString
			ContactNumber_TextBox.Text = resultTable(0)(DB_HEADER_CONTACTPHONE).ToString
			ContactEmail_TextBox.Text = resultTable(0)(DB_HEADER_CONTACTEMAIL).ToString
		End If
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim cmd As New SqlCommand("", myConn)
		Dim company As String = Company_TextBox.Text
		Dim address As String = Address_TextBox.Text
		Dim city As String = City_TextBox.Text
		Dim state As String = State_TextBox.Text
		Dim zip As String = Zip_TextBox.Text
		Dim country As String = Country_TextBox.Text
		Dim phone As String = Phone_TextBox.Text
		Dim contactName As String = ContactName_TextBox.Text
		Dim contactNumber As String = ContactNumber_TextBox.Text
		Dim contactEmail As String = ContactEmail_TextBox.Text

		Try
			'Check to see if our isNew flag is set. If yes, then we need to create a new entry in the database.
			If isNew = True Then
				'Add new entry to the database
				cmd.CommandText = "INSERT INTO " & TABLE_RMAADDRESSES & " ([" & DB_HEADER_ID & "],[" & DB_HEADER_COMPANY & "],[" & DB_HEADER_ADDRESS & "],[" & DB_HEADER_CITY & "],[" & DB_HEADER_STATE & "],[" & DB_HEADER_ZIPCODE & "],[" & DB_HEADER_COUNTRY & "],[" & DB_HEADER_PHONE & "],[" & DB_HEADER_CONTACTNAME & "],[" & DB_HEADER_CONTACTPHONE & "],[" & DB_HEADER_CONTACTEMAIL & "]) " &
					"VALUES(NEWID(),'" & company & "','" & address & "','" & city & "','" & state & "','" & zip & "','" & country & "','" & phone & "','" & contactName & "','" & contactNumber & "','" & contactEmail & "')"
				cmd.ExecuteNonQuery()
			Else
				'Confirm with the user that they want to update the Address.
				Select Case MsgBox("Are you sure you want to UPDATE " & Company_TextBox.Text & "-" & ContactName_TextBox.Text & "?" & vbNewLine &
								   "Doing so will update the record for any other RMAs that are associated with it.", MsgBoxStyle.YesNo, "Confirmation")
					Case MsgBoxResult.No
						Return

					Case MsgBoxResult.Yes
						'Update the code in the database
						cmd.CommandText = "UPDATE " & TABLE_RMAADDRESSES & " SET [" & DB_HEADER_COMPANY & "] = '" & company & "',[" & DB_HEADER_ADDRESS & "] = '" & address & "',[" & DB_HEADER_CITY & "] = '" & city & "',[" & DB_HEADER_STATE & "] = '" & state & "',[" & DB_HEADER_ZIPCODE & "] = '" & zip & "',[" & DB_HEADER_COUNTRY & "] = '" & country & "',[" & DB_HEADER_PHONE & "] = '" & phone & "',[" & DB_HEADER_CONTACTNAME & "] = '" & contactName & "',[" & DB_HEADER_CONTACTPHONE & "] = '" & contactNumber & "',[" & DB_HEADER_CONTACTEMAIL & "] = '" & contactEmail & "' WHERE [" & DB_HEADER_ID & "] = '" & thisGUID & "'"
						cmd.ExecuteNonQuery()

				End Select
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Find the view Address form and give it focus because this form is created with a NEW method.
		Dim frmCollection = Application.OpenForms
		Dim index As Integer
		For index = 0 To frmCollection.Count - 1
			If frmCollection.Item(index).Name = "ViewAddresses" Then
				frmCollection.Item(index).Activate()
				Exit For
			End If
		Next index

		Dim dialog As ViewAddresses = frmCollection.Item(index)
		'Update the table becuase we have either added a new entry or updated an existing one.
		dialog.UpdateTable()
		Close()
	End Sub

	Private Sub Delete_Button_Click() Handles Delete_Button.Click
		Try
			'Confirm with the user that they want to delete the record.
			Select Case MsgBox("Are you sure you want to DELETE " & Company_TextBox.Text & "-" & ContactName_TextBox.Text & "?" & vbNewLine &
							   "Doing so will delete the relationship for any other RMAs that are associated with it.", MsgBoxStyle.YesNo, "Confirmation")
				Case MsgBoxResult.No
					Return
				Case MsgBoxResult.Yes
					'Delete the record from the database
					Dim myCmd As New SqlCommand("DELETE FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & thisGUID & "'", myConn)

					myCmd.ExecuteNonQuery()

					'Now we need to update our records that use this information because it will no longer exist.
					myCmd.CommandText = "UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_SHIPID & "] = NULL WHERE [" & DB_HEADER_SHIPID & "] = '" & thisGUID & "'"
					myCmd.ExecuteNonQuery()
					myCmd.CommandText = "UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_BILLID & "] = NULL WHERE [" & DB_HEADER_BILLID & "] = '" & thisGUID & "'"
					myCmd.ExecuteNonQuery()
			End Select

		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Find the view Address form and give it focus because this form is created with a NEW method.
		Dim frmCollection = Application.OpenForms
		Dim index As Integer
		For index = 0 To frmCollection.Count - 1
			If frmCollection.Item(index).Name = "ViewAddresses" Then
				frmCollection.Item(index).Activate()
				Exit For
			End If
		Next index

		Dim dialog As ViewAddresses = frmCollection.Item(index)
		'Update the table becuase we have deleted a record.
		dialog.UpdateTable()
		Close()
	End Sub

End Class