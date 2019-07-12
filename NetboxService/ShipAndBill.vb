'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ShipAndBill.vb
'
' Description: Shipping and Billing step in our RMA Process. Handles where we shipped our unit and where we are billing the repairs. Not
'	all fields need to be populated. Option to select an existing address or creating a new address.
'
' Shipping Date: The date that the RMA Unit was shipped
' RMA PO Number: The PO number associated with the unit as it was shipped and billed.
' Netbox Invoice: The Invoice number for Netbox.
'
'			Both Shipping and Billing
' Select Address: Opens up the Address windows and allows you to pick which address you want to pre fill the information with.
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

Public Class ShipAndBill

	'Protection variable to prevent our combo box change event.
	Dim doNotUpdate As Boolean = True
	Dim wait As Boolean = False

	Private Sub ShipAndBill_Load() Handles MyBase.Load
		'Get today's date and populate
		Date_DTP.Value = Date.Now

		PopulateBillTypes(BillingType_ComboBox)
		doNotUpdate = False
	End Sub

	Private Sub PopulateBillTypes(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of our bill types.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMABILLTYPE, myConn)

		resultTable.Load(myCmd.ExecuteReader)

		box.Items.Add("")
		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_BILLTYPE).ToString)
		Next
	End Sub

	Private Sub SerialNumber_TextBox_GotFocus() Handles SerialNumber_TextBox.GotFocus
		'Disable our update buttons so we can avoid an unpleasnet bug that deals with the serial number losing focus and running it's search and then trying to update.
		Update_Button.Enabled = False
	End Sub

	Private Sub SerialNumber_TextBox_LostFocus() Handles SerialNumber_TextBox.LostFocus
		Dim cmd As New SqlCommand("", myConn)
		Dim active As Boolean = False

		'Check to see that one of the MDI children is active or not. If none are active then do nothing.
		If MdiParent.ActiveMdiChild Is Nothing Then
			Return
		End If

		'Check to see if the Active Child is the current form.
		If MdiParent.ActiveMdiChild.Name = Name Then
			For Each control As Control In ParentForm.ActiveMdiChild.Controls
				If control.Focused = True Then
					active = True
				End If
			Next
		End If

		'Check to see if we are giving focus to the cancel button or if we are coming from an Update and next call.
		If Cancel_Button.Focused = True Or WorkingServiceForm = True Then
			Return
		End If

		'Check to make sure that we have a serial number to look up and we are already not in the middle of a look up and we are the active child.
		If SerialNumber_TextBox.Text.Length <> 0 And wait = False And active = True Then
			'Set our flag that we are starting our look up. This prevents an infinite loop of look ups.
			wait = True

			'Check to see if we are a valid serial number.
			cmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
			Dim dt_results As New DataTable()
			dt_results.Load(cmd.ExecuteReader())

			If dt_results.Rows.Count < 1 Then
				'Check to see if we are a valid Board serial number.
				cmd.CommandText = "SELECT * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
				dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				If dt_results.Rows.Count < 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					MsgBox("Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the PRODUCTION database.")
					Return
				End If
			End If

			'Next, check to see if we have any RMA records in the database.
			'If we have 1 or more, we need to go into a special form to choose which record we are dealing with.
			cmd.CommandText = "SELECT [" & DB_HEADER_SERVICEFORM & "], [" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_INFORMATIONDATE & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERIAL_NUMBER & "] = '" & SerialNumber_TextBox.Text & "' ORDER BY [" & DB_HEADER_SERVICEFORM & "] ASC"
			dt_results = New DataTable()
			dt_results.Load(cmd.ExecuteReader())

			If dt_results.Rows.Count = 0 Then
				Select Case MsgBox("This serial number is not in the RMA database. Would you like to create a record?" & vbNewLine &
								   "Be sure that you have entered the Serial Number correctly before you create a new record.", MsgBoxStyle.YesNo, "Confirmation")
					Case DialogResult.Yes
						'We are dealing with a New record.
						'Grab the next SFN
						cmd.CommandText = "SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
						Dim serviceForm As String = cmd.ExecuteScalar
						ServiceForm_TextBox.Text = serviceForm

						Dim lastUpdate As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)

						cmd.CommandText = "INSERT INTO " & TABLE_RMA & "([" & DB_HEADER_ID & "], [" & DB_HEADER_SERIAL_NUMBER & "], [" & DB_HEADER_SERVICEFORM & "], [" & DB_HEADER_LASTUPDATE & "]) " &
						"Values(NEWID(),'" & SerialNumber_TextBox.Text & "','" & serviceForm & "','" & lastUpdate & "')"

						cmd.ExecuteNonQuery()
				End Select

			ElseIf dt_results.Rows.Count = 1 Then
				'Pass in the response from our dialog.
				UseRecord(dt_results.Rows(0)(DB_HEADER_SERVICEFORM).ToString)
			Else
				'We need to figure out which record we are going to use.
				Dim selectRecordDialog As New SelectRecord(dt_results, False)
				selectRecordDialog.ShowDialog()

				'Pass in the response from our dialog.
				UseRecord(selectRecordDialog.response)
			End If
			'Set our flag to show that we are done with out look up.

			wait = False
		End If
	End Sub

	Public Sub UseRecord(ByRef id As String)
		'Re-enable our update buttons since we are no longer in a condition that will cause us to try to update empty records or by accident.
		Update_Button.Enabled = True
		ShipAddress_TextBox.Focus()
		Dim cmd As New SqlCommand("", myConn)

		'Depending on what is passed through will determine what happens.
		Select Case id
			Case "CANCEL"
				'We decided not to deal with it.
				SerialNumber_TextBox.Text = ""
				SerialNumber_TextBox.Refresh()

			Case Else
				doNotUpdate = True
				'We are dealing with an old record and need to fill in all of the information that we have on it.
				cmd.CommandText = "SELECT * FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & id & "'"
				Dim dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				'Now that we have the information from the old record, we need to fill in the text boxes with all of the information that we have.
				ServiceForm_TextBox.Text = dt_results.Rows(0)(DB_HEADER_SERVICEFORM).ToString
				SerialNumber_TextBox.Text = dt_results.Rows(0)(DB_HEADER_SERIAL_NUMBER).ToString

				'Get the shipping information that we have with the record.
				Dim shipGUID As String = dt_results(0)(DB_HEADER_SHIPID).ToString
				If shipGUID.Length <> 0 Then
					cmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & shipGUID & "'"
					Dim dt_Ship = New DataTable()
					dt_Ship.Load(cmd.ExecuteReader())

					ShipCompany_TextBox.Text = dt_Ship(0)(DB_HEADER_COMPANY).ToString
					ShipAddress_TextBox.Text = dt_Ship(0)(DB_HEADER_ADDRESS).ToString
					ShipCity_TextBox.Text = dt_Ship(0)(DB_HEADER_CITY).ToString
					ShipState_TextBox.Text = dt_Ship(0)(DB_HEADER_STATE).ToString
					ShipZip_TextBox.Text = dt_Ship(0)(DB_HEADER_ZIPCODE).ToString
					ShipCountry_TextBox.Text = dt_Ship(0)(DB_HEADER_COUNTRY).ToString
					ShipPhone_TextBox.Text = dt_Ship(0)(DB_HEADER_PHONE).ToString
					ShipContactName_TextBox.Text = dt_Ship(0)(DB_HEADER_CONTACTNAME).ToString
					ShipContactNumber_TextBox.Text = dt_Ship(0)(DB_HEADER_CONTACTPHONE).ToString
					ShipContactEmail_TextBox.Text = dt_Ship(0)(DB_HEADER_CONTACTEMAIL).ToString
				End If

				RMAPONumber_TextBox.Text = dt_results(0)(DB_HEADER_C_RMAPO).ToString
				NetBoxInvoice_TextBox.Text = dt_results(0)(DB_HEADER_NB_INVOICE).ToString

				'Get the Billing type information
				Dim billingTypeGUID As String = dt_results(0)(DB_HEADER_BILLTYPEID).ToString
				If billingTypeGUID.Length <> 0 Then
					'Get the Billing Type information
					cmd.CommandText = "SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_ID & "] = '" & billingTypeGUID & "'"
					Dim dt_BillingType = New DataTable()
					dt_BillingType.Load(cmd.ExecuteReader())

					'Set the dropdown menu to this box
					BillingType_ComboBox.SelectedIndex = BillingType_ComboBox.Items.IndexOf(dt_BillingType(0)(DB_HEADER_BILLTYPE))

					'Depending on the billing type boolean, enable or disable the billing side.
					If dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = False Then
						'We do not need a billing address so disable each of the textboxes and autofill
						BillCompany_TextBox.Enabled = False
						BillAddress_TextBox.Enabled = False
						BillCity_TextBox.Enabled = False
						BillState_TextBox.Enabled = False
						BillZip_TextBox.Enabled = False
						BillCountry_TextBox.Enabled = False
						BillPhone_TextBox.Enabled = False
						BillContactName_TextBox.Enabled = False
						BillContactNumber_TextBox.Enabled = False
						BillContactEmail_TextBox.Enabled = False

						BillAutoFill_Button.Enabled = False
					Else
						'We need an address so enable all each of the textboxes and autofill
						BillCompany_TextBox.Enabled = True
						BillAddress_TextBox.Enabled = True
						BillCity_TextBox.Enabled = True
						BillState_TextBox.Enabled = True
						BillZip_TextBox.Enabled = True
						BillCountry_TextBox.Enabled = True
						BillPhone_TextBox.Enabled = True
						BillContactName_TextBox.Enabled = True
						BillContactNumber_TextBox.Enabled = True
						BillContactEmail_TextBox.Enabled = True

						BillAutoFill_Button.Enabled = True
					End If
				End If

				'Get the billing information that we have with the record.
				Dim billGUID As String = dt_results(0)(DB_HEADER_BILLID).ToString

				'Populate only if we have a GUID.
				If billGUID.Length <> 0 Then
					cmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & billGUID & "'"
					Dim dt_Bill = New DataTable()
					dt_Bill.Load(cmd.ExecuteReader())

					'Populate the textboxes with the correct information.
					BillCompany_TextBox.Text = dt_Bill(0)(DB_HEADER_COMPANY).ToString
					BillAddress_TextBox.Text = dt_Bill(0)(DB_HEADER_ADDRESS).ToString
					BillCity_TextBox.Text = dt_Bill(0)(DB_HEADER_CITY).ToString
					BillState_TextBox.Text = dt_Bill(0)(DB_HEADER_STATE).ToString
					BillZip_TextBox.Text = dt_Bill(0)(DB_HEADER_ZIPCODE).ToString
					BillCountry_TextBox.Text = dt_Bill(0)(DB_HEADER_COUNTRY).ToString
					BillPhone_TextBox.Text = dt_Bill(0)(DB_HEADER_PHONE).ToString
					BillContactName_TextBox.Text = dt_Bill(0)(DB_HEADER_CONTACTNAME).ToString
					BillContactNumber_TextBox.Text = dt_Bill(0)(DB_HEADER_CONTACTPHONE).ToString
					BillContactEmail_TextBox.Text = dt_Bill(0)(DB_HEADER_CONTACTEMAIL).ToString
				End If

		End Select

		doNotUpdate = False
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim hasErrors As Boolean = False
		Dim errorMessage As String = ""

		'Serial Number logic checking
		Dim cmd As New SqlCommand("", myConn)

		'Check to make sure that we have a serial number to work with.
		If SerialNumber_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a Serial Number." & vbNewLine
		Else
			'Check to see if we are a valid serial number.
			cmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
			Dim dt_results As New DataTable()
			dt_results.Load(cmd.ExecuteReader())

			If dt_results.Rows.Count < 1 Then
				'Check to see if we are a valid Board serial number.
				cmd.CommandText = "SELECT * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
				dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				If dt_results.Rows.Count < 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					errorMessage = errorMessage & "Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the PRODUCTION database." & vbNewLine
				End If
			End If
		End If

		'If we have any errors, display them and do not continue the update.
		If errorMessage.Length <> 0 Then
			MsgBox(errorMessage)
			Return
		End If

		'Check the billing type that was selected.
		If BillingType_ComboBox.Text.Length = 0 Then
			MsgBox("Please select a billing type.")
			Return
		End If

		'Update our information to the database.
		Dim sfn As Integer = ServiceForm_TextBox.Text
		Dim infoDate As new Date (Date_DTP.Value.Year, Date_DTP.Value.Month, Date_DTP.Value.Day)
		Dim serialNumber As String = SerialNumber_TextBox.Text
		Dim lastUpdate As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)

		'Check our shipping information to see if we have the record or if we need to create a new record
		Dim shipCompany As String = ShipCompany_TextBox.Text
		Dim shipAddress As String = ShipAddress_TextBox.Text
		Dim shipCity As String = ShipCity_TextBox.Text
		Dim shipState As String = ShipState_TextBox.Text
		Dim shipZip As String = ShipZip_TextBox.Text
		Dim shipCountry As String = ShipCountry_TextBox.Text
		Dim shipPhone As String = ShipPhone_TextBox.Text
		Dim shipContactName As String = ShipContactName_TextBox.Text
		Dim shipContactNumber As String = ShipContactNumber_TextBox.Text
		Dim shipContactEmail As String = ShipContactEmail_TextBox.Text
		Dim billingType As String = BillingType_ComboBox.Text

		Dim rmaPO As String = RMAPONumber_TextBox.Text
		Dim netboxInvoice As String = NetBoxInvoice_TextBox.Text

		'Declare GUID here and assign it later in our if statements. This will allow us to use it later.
		Dim shipGUID As String = "NULL"

		If shipCompany.Length = 0 And shipAddress.Length = 0 And shipCity.Length = 0 And shipState.Length = 0 And shipZip.Length = 0 And shipCountry.Length = 0 And
				shipPhone.Length = 0 And shipContactName.Length = 0 And shipContactNumber.Length = 0 And shipContactEmail.Length = 0 Then
			'Set the GUID to NULL only if all of the textboxes are empty.
			shipGUID = "NULL"
		Else
			'Try to select the record that matches the information provided.
			cmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_COMPANY & "] = '" & shipCompany & "' AND [" & DB_HEADER_ADDRESS & "] = '" & shipAddress & "' AND " &
				"[" & DB_HEADER_CITY & "] = '" & shipCity & "'  AND [" & DB_HEADER_STATE & "] = '" & shipState & "'  AND [" & DB_HEADER_ZIPCODE & "] = '" & shipZip & "'  AND " &
				"[" & DB_HEADER_COUNTRY & "] = '" & shipCountry & "'  AND [" & DB_HEADER_PHONE & "] = '" & shipPhone & "'  AND [" & DB_HEADER_CONTACTNAME & "] = '" & shipContactName & "'  AND " &
				"[" & DB_HEADER_CONTACTPHONE & "] = '" & shipContactNumber & "'  AND [" & DB_HEADER_CONTACTEMAIL & "] = '" & shipContactEmail & "'"
			Dim dt_shipResults = New DataTable()
			dt_shipResults.Load(cmd.ExecuteReader())

			If dt_shipResults.Rows.Count <> 1 Then
				'This should be a return of 0 since we cannot have duplicates in the database. This means that we have to add it to the database.
				cmd.CommandText = "INSERT INTO " & TABLE_RMAADDRESSES & "([" & DB_HEADER_ID & "],[" & DB_HEADER_COMPANY & "],[" & DB_HEADER_ADDRESS & "],[" & DB_HEADER_CITY & "], " &
					"[" & DB_HEADER_STATE & "],[" & DB_HEADER_ZIPCODE & "],[" & DB_HEADER_COUNTRY & "],[" & DB_HEADER_PHONE & "],[" & DB_HEADER_CONTACTNAME & "], " &
					"[" & DB_HEADER_CONTACTPHONE & "],[" & DB_HEADER_CONTACTEMAIL & "]) " &
						"VALUES(NEWID(),'" & shipCompany & "','" & shipAddress & "','" & shipCity & "','" & shipState & "','" & shipZip & "','" & shipCountry & "','" & shipPhone & "'," &
					"'" & shipContactName & "','" & shipContactNumber & "','" & shipContactEmail & "')"
				cmd.ExecuteNonQuery()

				'Get the new GUID of the shipping address
				cmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_COMPANY & "] = '" & shipCompany & "' AND [" & DB_HEADER_ADDRESS & "] = '" & shipAddress & "' AND " &
					"[" & DB_HEADER_CITY & "] = '" & shipCity & "'  AND [" & DB_HEADER_STATE & "] = '" & shipState & "'  AND [" & DB_HEADER_ZIPCODE & "] = '" & shipZip & "'  AND " &
					"[" & DB_HEADER_COUNTRY & "] = '" & shipCountry & "'  AND [" & DB_HEADER_PHONE & "] = '" & shipPhone & "'  AND [" & DB_HEADER_CONTACTNAME & "] = '" & shipContactName & "'  AND " &
					"[" & DB_HEADER_CONTACTPHONE & "] = '" & shipContactNumber & "'  AND [" & DB_HEADER_CONTACTEMAIL & "] = '" & shipContactEmail & "'"
				shipGUID = "'" & cmd.ExecuteScalar.ToString & "'"

			Else
				'This should be a return of 1 since we cannot have duplicates in the database.
				shipGUID = "'" & dt_shipResults(0)(DB_HEADER_ID).ToString & "'"
			End If
		End If

		'Declare GUID here and assign it later in our if statements. This will allow us to use it later.
		Dim billGUID As String = "NULL"
		Dim billingTypeGUID As String = "NULL"

		If billingType.Length <> 0 Then
			'Get the information of the Bill Type
			cmd.CommandText = "SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_BILLTYPE & "] = '" & billingType & "'"
			Dim dt_BillingType = New DataTable()
			dt_BillingType.Load(cmd.ExecuteReader())

			billingTypeGUID = "'" & dt_BillingType(0)(DB_HEADER_ID).ToString & "'"

			'The booleans with the bill type will dertermine how to use the address.
			If dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = False And dt_BillingType(0)(DB_HEADER_SAMEASSHIPPING) = False Then
				billGUID = "NULL"

			ElseIf dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = True And dt_BillingType(0)(DB_HEADER_SAMEASSHIPPING) = True Then
				billGUID = shipGUID

			ElseIf dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = False And dt_BillingType(0)(DB_HEADER_SAMEASSHIPPING) = True Then
				billGUID = shipGUID

			ElseIf dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = True And dt_BillingType(0)(DB_HEADER_SAMEASSHIPPING) = False Then
				'Check our Billing information to see if we have the record or if we need to create a new record
				Dim billCompany As String = BillCompany_TextBox.Text
				Dim billAddress As String = BillAddress_TextBox.Text
				Dim billCity As String = BillCity_TextBox.Text
				Dim billState As String = BillState_TextBox.Text
				Dim billZip As String = BillZip_TextBox.Text
				Dim billCountry As String = BillCountry_TextBox.Text
				Dim billPhone As String = BillPhone_TextBox.Text
				Dim billContactName As String = BillContactName_TextBox.Text
				Dim billContactNumber As String = BillContactNumber_TextBox.Text
				Dim billContactEmail As String = BillContactEmail_TextBox.Text

				If billCompany.Length = 0 And billAddress.Length = 0 And billCity.Length = 0 And billState.Length = 0 And billZip.Length = 0 And billCountry.Length = 0 And
						billPhone.Length = 0 And billContactName.Length = 0 And billContactNumber.Length = 0 And billContactEmail.Length = 0 Then
					billGUID = "NULL"
				Else
					cmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_COMPANY & "] = '" & billCompany & "' AND [" & DB_HEADER_ADDRESS & "] = '" & billAddress & "' AND " &
						"[" & DB_HEADER_CITY & "] = '" & billCity & "'  AND [" & DB_HEADER_STATE & "] = '" & billState & "'  AND [" & DB_HEADER_ZIPCODE & "] = '" & billZip & "'  AND " &
						"[" & DB_HEADER_COUNTRY & "] = '" & billCountry & "'  AND [" & DB_HEADER_PHONE & "] = '" & billPhone & "'  AND [" & DB_HEADER_CONTACTNAME & "] = '" & billContactName & "'  AND " &
						"[" & DB_HEADER_CONTACTPHONE & "] = '" & billContactNumber & "'  AND [" & DB_HEADER_CONTACTEMAIL & "] = '" & billContactEmail & "'"
					Dim dt_billResults = New DataTable()
					dt_billResults.Load(cmd.ExecuteReader())

					If dt_billResults.Rows.Count <> 1 Then
						'This should be a return of 0 since we cannot have duplicates in the database. This means that we have to add it to the database.
						cmd.CommandText = "INSERT INTO " & TABLE_RMAADDRESSES & "([" & DB_HEADER_ID & "],[" & DB_HEADER_COMPANY & "],[" & DB_HEADER_ADDRESS & "],[" & DB_HEADER_CITY & "], " &
							"[" & DB_HEADER_STATE & "],[" & DB_HEADER_ZIPCODE & "],[" & DB_HEADER_COUNTRY & "],[" & DB_HEADER_PHONE & "],[" & DB_HEADER_CONTACTNAME & "], " &
							"[" & DB_HEADER_CONTACTPHONE & "],[" & DB_HEADER_CONTACTEMAIL & "]) " &
							"VALUES(NEWID(),'" & billCompany & "','" & billAddress & "','" & billCity & "','" & billState & "','" & billZip & "','" & billCountry & "','" & billPhone & "'," &
							"'" & billContactName & "','" & billContactNumber & "','" & billContactEmail & "')"
						cmd.ExecuteNonQuery()

						'Get the new GUID of the billing address
						cmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_COMPANY & "] = '" & billCompany & "' AND [" & DB_HEADER_ADDRESS & "] = '" & billAddress & "' AND " &
							"[" & DB_HEADER_CITY & "] = '" & billCity & "'  AND [" & DB_HEADER_STATE & "] = '" & billState & "'  AND [" & DB_HEADER_ZIPCODE & "] = '" & billZip & "'  AND " &
							"[" & DB_HEADER_COUNTRY & "] = '" & billCountry & "'  AND [" & DB_HEADER_PHONE & "] = '" & billPhone & "'  AND [" & DB_HEADER_CONTACTNAME & "] = '" & billContactName & "'  AND " &
							"[" & DB_HEADER_CONTACTPHONE & "] = '" & billContactNumber & "'  AND [" & DB_HEADER_CONTACTEMAIL & "] = '" & billContactEmail & "'"
						billGUID = "'" & cmd.ExecuteScalar.ToString & "'"

					Else
						'This should be a return of 1 since we cannot have duplicates in the database.
						billGUID = "'" & dt_billResults(0)(DB_HEADER_ID).ToString & "'"
					End If
				End If
			End If
		End If

		'Get the GUID of the status that we are cahnging to.
		cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = 'Shipped'"
		Dim statusGUID As Guid = cmd.ExecuteScalar

		'Update the RMA record
		cmd.CommandText = "UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_STATUSID & "] = '" & statusGUID.ToString & "', [" & DB_HEADER_SHIPID & "] = " & shipGUID & ", " &
			"[" & DB_HEADER_SERIAL_NUMBER & "] = '" & serialNumber & "', [" & DB_HEADER_BILLTYPEID & "] = " & billingTypeGUID & ", [" & DB_HEADER_BILLID & "] = " & billGUID & ", " &
			"[" & DB_HEADER_C_RMAPO & "] = '" & rmaPO & "', [" & DB_HEADER_SHIPDATE & "] = '" & infoDate & "', [" & DB_HEADER_LASTUPDATE & "] = '" & lastUpdate & "', [" & DB_HEADER_NB_INVOICE & "] = '" & netboxInvoice & "' WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & sfn & "'"
		cmd.ExecuteNonQuery()

		'Update our SFN display
		ServiceForm_TextBox.Text = ""

		'Clear our Serial Number
		SerialNumber_TextBox.Text = ""
	End Sub

	Private Sub BillingType_ComboBox_SelectedIndexChanged() Handles BillingType_ComboBox.SelectedIndexChanged
		If doNotUpdate = False Then
			'Get the Billing type information
			Dim name As String = BillingType_ComboBox.Text

			If name <> "" Then
				'Get the bill type information.
				Dim cmd As New SqlCommand("SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_BILLTYPE & "] = '" & name & "'", myConn)
				Dim dt_BillingType = New DataTable()
				dt_BillingType.Load(cmd.ExecuteReader())

				If dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = False Or dt_BillingType(0)(DB_HEADER_SAMEASSHIPPING) = True Then
					'We do not need a billing address so disable each of the textboxes and autofill
					BillCompany_TextBox.Enabled = False
					BillAddress_TextBox.Enabled = False
					BillCity_TextBox.Enabled = False
					BillState_TextBox.Enabled = False
					BillZip_TextBox.Enabled = False
					BillCountry_TextBox.Enabled = False
					BillPhone_TextBox.Enabled = False
					BillContactName_TextBox.Enabled = False
					BillContactNumber_TextBox.Enabled = False
					BillContactEmail_TextBox.Enabled = False

					BillAutoFill_Button.Enabled = False

					If dt_BillingType(0)(DB_HEADER_SAMEASSHIPPING) = True Then
						BillCompany_TextBox.Text = "Same as Shipping"
					End If
				Else
					'We need an address so enable all each of the textboxes and autofill
					BillCompany_TextBox.Enabled = True
					BillAddress_TextBox.Enabled = True
					BillCity_TextBox.Enabled = True
					BillState_TextBox.Enabled = True
					BillZip_TextBox.Enabled = True
					BillCountry_TextBox.Enabled = True
					BillPhone_TextBox.Enabled = True
					BillContactName_TextBox.Enabled = True
					BillContactNumber_TextBox.Enabled = True
					BillContactEmail_TextBox.Enabled = True

					BillAutoFill_Button.Enabled = True

					BillCompany_TextBox.Text = ""
				End If
			End If
		End If
	End Sub

	Private Sub ShipAutoFill_Button_Click() Handles ShipAutoFill_Button.Click
		Dim viewAddressesDialog As New ViewAddresses(True, False)
		viewAddressesDialog.ShowDialog()

		Dim shipguid As String = viewAddressesDialog.thisGUID

		If shipguid.Length <> 0 Then
			Dim dt_Ship As New DataTable()

			'Get the address information of what was selected.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & shipguid & "'", myConn)

			dt_Ship.Load(myCmd.ExecuteReader)

			'Populate with data.
			ShipCompany_TextBox.Text = dt_Ship(0)(DB_HEADER_COMPANY).ToString
			ShipAddress_TextBox.Text = dt_Ship(0)(DB_HEADER_ADDRESS).ToString
			ShipCity_TextBox.Text = dt_Ship(0)(DB_HEADER_CITY).ToString
			ShipState_TextBox.Text = dt_Ship(0)(DB_HEADER_STATE).ToString
			ShipZip_TextBox.Text = dt_Ship(0)(DB_HEADER_ZIPCODE).ToString
			ShipCountry_TextBox.Text = dt_Ship(0)(DB_HEADER_COUNTRY).ToString
			ShipPhone_TextBox.Text = dt_Ship(0)(DB_HEADER_PHONE).ToString
			ShipContactName_TextBox.Text = dt_Ship(0)(DB_HEADER_CONTACTNAME).ToString
			ShipContactNumber_TextBox.Text = dt_Ship(0)(DB_HEADER_CONTACTPHONE).ToString
			ShipContactEmail_TextBox.Text = dt_Ship(0)(DB_HEADER_CONTACTEMAIL).ToString
		End If
	End Sub

	Private Sub BillAutoFill_Button_Click() Handles BillAutoFill_Button.Click
		Dim viewAddressesDialog As New ViewAddresses(True, False)
		viewAddressesDialog.ShowDialog()

		Dim billguid As String = viewAddressesDialog.thisGUID

		If billguid.Length <> 0 Then
			Dim dt_Bill As New DataTable()

			'Get the address information of what was selected.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & billguid & "'", myConn)

			dt_Bill.Load(myCmd.ExecuteReader)

			'Populate with data.
			BillCompany_TextBox.Text = dt_Bill(0)(DB_HEADER_COMPANY).ToString
			BillAddress_TextBox.Text = dt_Bill(0)(DB_HEADER_ADDRESS).ToString
			BillCity_TextBox.Text = dt_Bill(0)(DB_HEADER_CITY).ToString
			BillState_TextBox.Text = dt_Bill(0)(DB_HEADER_STATE).ToString
			BillZip_TextBox.Text = dt_Bill(0)(DB_HEADER_ZIPCODE).ToString
			BillCountry_TextBox.Text = dt_Bill(0)(DB_HEADER_COUNTRY).ToString
			BillPhone_TextBox.Text = dt_Bill(0)(DB_HEADER_PHONE).ToString
			BillContactName_TextBox.Text = dt_Bill(0)(DB_HEADER_CONTACTNAME).ToString
			BillContactNumber_TextBox.Text = dt_Bill(0)(DB_HEADER_CONTACTPHONE).ToString
			BillContactEmail_TextBox.Text = dt_Bill(0)(DB_HEADER_CONTACTEMAIL).ToString
		End If
	End Sub

	Private Sub ShipAndBill_Activated() Handles Me.Activated
		'Reload our billing types in case the user decided to add new ones.
		doNotUpdate = True
		BillingType_ComboBox.Items.Clear()
		PopulateBillTypes(BillingType_ComboBox)
		doNotUpdate = False
	End Sub

End Class