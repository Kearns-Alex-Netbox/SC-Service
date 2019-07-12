'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: Receive.vb
'
' Description: Receive step in our RMA Process. This handles receiving [and creation if needed] of the RMA with what details are given to
'	us at the time we recieve the unit.
'
' Receive Date: The date that the unit was received [Also used for information date if we are creating on this step].
' Customer: What type of approval was given. This is taken from a list of 'approval' status's and can have more added.
' Contact: The information for the contact dealing with the RMA
'		Name: The name of the contact.
'		Number: The contact's number.
'		E-mail: The contact's E-mail.
' RGA Number: The RGA number associated with the unit coming back.
' Invoice Number: The invoice number associated with the unit coming back.
' Customer Tested: Did the customer test the unit on their end before sending it back to us.
' Description: Customer's description of the problem.
' Codes: Any codes that are associated with the unit coming back.
' Update: Update the record and then clears the form for the next record.
' Update + Next: Updates the record and then moves onto the next step [shipping and billing] with the same Service Form and Serial Number.
'
' Special Keys:
'	delete = deletes the selected row in the table depending on which table has the focus.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class Receive
	Dim isNewRecord As Boolean = False

	Dim wait As Boolean = False

	Dim dt_customerCodeItems As New DataTable()
	Dim thisGUID As String
	Dim lastwasNA As Boolean = False	

	Private Sub Receive_Load() Handles MyBase.Load
		'Get today's date and populate
		Date_DTP.Value = Date.Now

		PopulateCodes(Code_ComboBox)

		' set up prdictive text for customers
		Customer_TextBox.AutoCompleteMode = AutoCompleteMode.Suggest
        Customer_TextBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        Dim DataCollection As New AutoCompleteStringCollection()

		' getData(DataCollection)
		Dim cmd As New SqlCommand("SELECT DISTINCT [" & DB_HEADER_CUSTOMER & "] FROM " & TABLE_RMA, myConn)
		Dim dt_results As New DataTable()
		dt_results.Load(cmd.ExecuteReader())
		For each row As DataRow In dt_results.Rows
			DataCollection.Add(row(0).ToString())
		Next
        
        Customer_TextBox.AutoCompleteCustomSource = DataCollection

		'Set up the font for our DataGridView so it does not change on us.
		Dim newFont As New Font("Consolas", 9.75)
		CodeItems_DataGridView.DefaultCellStyle.Font = newFont
		CodeItems_DataGridView.ColumnHeadersDefaultCellStyle.Font = newFont
		CodeItems_DataGridView.RowHeadersDefaultCellStyle.Font = newFont

		'Set up our code data table.
		dt_customerCodeItems.Columns.Add(DB_HEADER_CODE)
		dt_customerCodeItems.Columns.Add(DB_HEADER_TYPE)
		dt_customerCodeItems.Columns.Add(DB_HEADER_DESCRIPTION)
		dt_customerCodeItems.Columns.Add(DB_HEADER_FIX)

		'Set up our code DataGridView.
		CodeItems_DataGridView.DataSource = dt_customerCodeItems
		CodeItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

		CodeItems_DataGridView.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		CodeItems_DataGridView.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		CodeItems_DataGridView.Columns(2).Width = 300

		CodeItems_DataGridView.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		CodeItems_DataGridView.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		CodeItems_DataGridView.Columns(3).Width = 300
		KeyPreview = True
	End Sub

	Private Sub AddCode_Button_Click() Handles AddCode_Button.Click
		'Check to make sure we have something selected in our drop down.
		If Code_ComboBox.Text.Length <> 0 Then
			'See if we have added it already to the datatable
			Dim rows As DataRow() = dt_customerCodeItems.Select("[" & DB_HEADER_CODE & "] = '" & Code_ComboBox.Text & "'")

			'Only add if we have not already.
			If rows.Count = 0 Then
				Dim mycmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & Code_ComboBox.Text & "'", myConn)
				Dim results As New DataTable
				results.Load(mycmd.ExecuteReader)

				'Add it to the Datatable.
				dt_customerCodeItems.Rows.Add(results(0)(DB_HEADER_CODE), results(0)(DB_HEADER_TYPE), results(0)(DB_HEADER_DESCRIPTION), results(0)(DB_HEADER_FIX))
			End If
		End If
	End Sub

	Private Sub DeleteCode_Button_Click() Handles DeleteCode_Button.Click
		'First check to see if we have selected any of the rows in the datatable.
		If CodeItems_DataGridView.SelectedCells.Count = 1 Then
			'Search for the row that we are going to be deleting.
			Dim rowIndex As Integer = CodeItems_DataGridView.SelectedCells.Item(0).RowIndex
			Dim foundRows As DataRow() = dt_customerCodeItems.Select("[" & DB_HEADER_CODE & "] = '" & CodeItems_DataGridView.Rows(rowIndex).Cells(DB_HEADER_CODE).Value & "'")

			'Delete the row in our datatable.
			For Each row As DataRow In foundRows
				row.Delete()
			Next
		End If
	End Sub

	Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode.Equals(Keys.Delete) Then
			'If we press the Delete key, call our remove function.
			Call DeleteCode_Button_Click()
		End If
	End Sub

	Private Sub PopulateCodes(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of our codes from the database.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " ORDER BY [" & DB_HEADER_CODE & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_CODE).ToString)
		Next
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub SerialNumber_TextBox_GotFocus() Handles SerialNumber_TextBox.GotFocus
		'Disable our update buttons so we can avoid an unpleasnet bug that deals with the serial number losing focus and running it's search and then trying to update.
		Update_Button.Enabled = False
		UpdateAndNext_Button.Enabled = False
	End Sub

	Private Sub SerialNumber_TextBox_LostFocus() Handles SerialNumber_TextBox.LostFocus
		If lastwasNA = True And SerialNumber_TextBox.Text <> "NA" Then
			Update_Button.Enabled = True
			UpdateAndNext_Button.Enabled = True
			lastwasNA = False
			Return
		End If

		Dim cmd As New SqlCommand("", myConn)
		Dim active As Boolean = False

		'Check to see that one of the MDI children is active or not. If none are active then do nothing.
		If MdiParent.ActiveMdiChild Is Nothing Then
			Return
		End If

		'Check to see if the Active Child is the current form.
		If MdiParent.ActiveMdiChild.Name <> Name Then
			Return
		End If

		For Each control As Control In ParentForm.ActiveMdiChild.Controls
			If control.Focused = True Then
				active = True
				Exit For
			End If
		Next

		If active = False Then
			Return
		End If

		'Check to see if we are giving focus to the cancel button.
		If Cancel_Button.Focused = True Then
			Return
		End If

		If SerialNumber_TextBox.Text.Length = 0 Or wait = True Then
			Return
		End If

		'Set our flag that we are starting our look up. This prevents an infinite loop of look ups.
		wait = True

		Dim dt_results As New DataTable()

		'Check to see if we are a valid serial number.
		If SerialNumber_TextBox.Text = "NA" Then
			lastwasNA = True
		Else
			cmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
			dt_results.Load(cmd.ExecuteReader())

			If dt_results.Rows.Count < 1 Then
				'Check to see if we are a valid Board serial number.
				cmd.CommandText = "SELECT * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
				dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				If dt_results.Rows.Count < 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					MsgBox("Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the " & SQL_API.DATABASE & " database.")
					wait = False
					Return
				End If
			End If
		End If
		
		'Next, check to see if we have any RMA records in the database.
		'If we have 0 then we are dealing with a new record.
		'If we have 1 or more, we need to go into a special form to choose which record we are dealing with or creating a new one.
		cmd.CommandText = "SELECT [" & DB_HEADER_SERVICEFORM & "], [" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_INFORMATIONDATE & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERIAL_NUMBER & "] = '" & SerialNumber_TextBox.Text & "' ORDER BY [" & DB_HEADER_SERVICEFORM & "] ASC"
		dt_results = New DataTable()
		dt_results.Load(cmd.ExecuteReader())

		If dt_results.Rows.Count = 0 Then
			'We are dealing with a new record.
			'Grab the next SFN
			cmd.CommandText = "SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
			ServiceForm_TextBox.Text = cmd.ExecuteScalar

			isNewRecord = True
			UseRecord("NEW")
		Else
			'We need to figure out if we are choosing an old record or creating a new one.
			Dim selectRecordDialog As New SelectRecord(dt_results, True)
			selectRecordDialog.ShowDialog()

			'Pass in the response from our dialog.
			UseRecord(selectRecordDialog.response)
		End If

		'Set our flag to show that we are done with out look up.
		wait = False
	End Sub

	Public Sub UseRecord(ByRef id As String)
		'Re-enable our update buttons since we are no longer in a condition that will cause us to try to update empty records or by accident.
		Update_Button.Enabled = True
		UpdateAndNext_Button.Enabled = True

		Dim cmd As New SqlCommand("", myConn)

		'Depending on what is passed through will determine what happens.
		Select Case id
			Case "CANCEL"
				'We decided not to deal with it.
				SerialNumber_TextBox.Text = ""
				SerialNumber_TextBox.Refresh()
			Case "NEW"
				'We are dealing with a New record.
				'Grab the next SFN
				cmd.CommandText = "SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
				ServiceForm_TextBox.Text = cmd.ExecuteScalar

				isNewRecord = True
			Case Else
				'We are dealing with an old record and need to fill in all of the information that we have on it.
				cmd.CommandText = "SELECT * FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & id & "'"
				Dim dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				'Now that we have the information from the old record, we need to fill in the text boxes with all of the information that we have.
				ServiceForm_TextBox.Text = dt_results.Rows(0)(DB_HEADER_SERVICEFORM).ToString
				Customer_TextBox.Text = dt_results.Rows(0)(DB_HEADER_CUSTOMER).ToString
				ContactName_TextBox.Text = dt_results.Rows(0)(DB_HEADER_CONTACTNAME).ToString
				ContactNumber_TextBox.Text = dt_results.Rows(0)(DB_HEADER_CONTACTPHONE).ToString
				ContactEmail_TextBox.Text = dt_results.Rows(0)(DB_HEADER_CONTACTEMAIL).ToString
				RGANumber_TextBox.Text = dt_results.Rows(0)(DB_HEADER_C_RGA).ToString
				InvoiceNumber_TextBox.Text = dt_results.Rows(0)(DB_HEADER_C_INVOICE).ToString
				Description_TextBox.Text = dt_results.Rows(0)(DB_HEADER_DESCRIPTION).ToString
				thisGUID = dt_results(0)(DB_HEADER_ID).ToString

				Dim testedCheck As String = dt_results(0)(DB_HEADER_TESTED).ToString
				Dim tested As Boolean = False

				If testedCheck.Length = 0 Then
					tested = False
				Else
					tested = dt_results(0)(DB_HEADER_TESTED).ToString
				End If

				Tested_CheckBox.Checked = tested

				'Get all of the repair codes that are associated with this RMA.
				dt_customerCodeItems.Rows.Clear()

				cmd.CommandText = "SELECT * FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "' AND [" & DB_HEADER_CUSTOMER & "] = '1'"
				Dim dt_coderesults As New DataTable
				dt_coderesults.Load(cmd.ExecuteReader)

				For Each row As DataRow In dt_coderesults.Rows
					Try
						cmd.CommandText = "SELECT * FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_ID & "] = '" & row(DB_HEADER_RMACODESID).ToString & "'"
					Catch ex As Exception
						MsgBox(ex.Message)
					End Try

					Dim dt_codeInformation As New DataTable
					dt_codeInformation.Load(cmd.ExecuteReader)

					If dt_codeInformation.Rows.Count <> 0 Then
						dt_customerCodeItems.Rows.Add(dt_codeInformation(0)(DB_HEADER_CODE).ToString, dt_codeInformation(0)(DB_HEADER_TYPE).ToString, dt_codeInformation(0)(DB_HEADER_DESCRIPTION).ToString, dt_codeInformation(0)(DB_HEADER_FIX).ToString)
					End If
				Next


				CodeItems_DataGridView.DataSource = Nothing
				CodeItems_DataGridView.DataSource = dt_customerCodeItems
				CodeItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

				CodeItems_DataGridView.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
				CodeItems_DataGridView.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
				CodeItems_DataGridView.Columns(2).Width = 300

				CodeItems_DataGridView.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
				CodeItems_DataGridView.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
				CodeItems_DataGridView.Columns(3).Width = 300
				CodeItems_DataGridView.ClearSelection()

				isNewRecord = False
		End Select
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		'Try to update our record. If we fail, do not close the window so the user can fix any issues.
		If UpdateRMA() = False Then
			Return
		End If
	End Sub

	Private Function UpdateRMA() As Boolean
		'Check to see if the folder directory has been set up before moving on.
		If My.Settings.ServiceFoldersDir.Length = 0 Then
			MsgBox("Service Folder Directory has not been set up yet. Please update your settings.")
			Return False
		End If

		'Check to see if the folder path is valid or not.
		If IO.Directory.Exists(My.Settings.ServiceFoldersDir) = False Then
			MsgBox("Service Folder Directory does not exist. Please update your settings.")
			Return False
		End If

		Dim isBoard As Boolean = False
		Dim hasErrors As Boolean = False
		Dim errorMessage As String = ""

		'Serial Number logic checking
		Dim cmd As New SqlCommand("", myConn)

		Dim InstanceString As String = ", [" & DB_HEADER_INSTANCE & "]"
		Dim InstanceValue As String = ", NULL"

		'Check to make sure that we have a serial number to work with.
		If SerialNumber_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a Serial Number." & vbNewLine
		Else
			'Check to see if we are a valid System serial number.
			cmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "' AND 
[dbo.SystemStatus.id] != (Select id from SystemStatus where name = 'Scrapped')"
			Dim dt_results As New DataTable()
			dt_results.Load(cmd.ExecuteReader())

			If dt_results.Rows.Count < 1 Then
				'Check to see if we are a valid Board serial number.
				cmd.CommandText = "SELECT * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
				dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())
				isBoard = True

				If dt_results.Rows.Count < 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					errorMessage = errorMessage & "Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the PRODUCTION database." & vbNewLine
				End If
			Else
				InstanceValue = ",'" & dt_results.Rows(0)(DB_HEADER_INSTANCE) & "'"
			End If
		End If

		'If we have any errors, display them and do not continue the update.
		If errorMessage.Length <> 0 Then
			MsgBox(errorMessage)
			Return False
		End If

		'Add our information to the database as a new entry.
		Dim sfn As Integer = ServiceForm_TextBox.Text
		Dim infoDate As new Date (Date_DTP.Value.Year, Date_DTP.Value.Month, Date_DTP.Value.Day)
		Dim serialNumber As String = SerialNumber_TextBox.Text
		Dim customer As String = Customer_TextBox.Text
		Dim contactName As String = ContactName_TextBox.Text
		Dim contactNumber As String = ContactNumber_TextBox.Text
		Dim contactEmail As String = ContactEmail_TextBox.Text
		Dim rgaNumber As String = RGANumber_TextBox.Text
		Dim invoiceNumber As String = InvoiceNumber_TextBox.Text
		Dim description As String = Description_TextBox.Text
		Dim tested As Boolean = Tested_CheckBox.Checked
		Dim lastUpdate As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)

		'Replace any single ['] quotes with double single [''] quotes for the SQL syntax.
		description = description.Replace("'", "''")

		'Get the GUID of the status that we want
		cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = 'Received'"
		Dim statusGUID As Guid = cmd.ExecuteScalar

		If isNewRecord = True Then
			'We are adding in a new record so we need to treate it differently.
			Try
				cmd.CommandText = "INSERT INTO " & TABLE_RMA & "(
  [" & DB_HEADER_ID & "]
, [" & DB_HEADER_STATUSID & "]
, [" & DB_HEADER_CUSTOMER & "]
, [" & DB_HEADER_SERIAL_NUMBER & "]
, [" & DB_HEADER_CONTACTNAME & "]
, [" & DB_HEADER_CONTACTPHONE & "]
, [" & DB_HEADER_CONTACTEMAIL & "]
, [" & DB_HEADER_C_RGA & "]
, [" & DB_HEADER_C_INVOICE & "]
, [" & DB_HEADER_SERVICEFORM & "]
, [" & DB_HEADER_DESCRIPTION & "]
, [" & DB_HEADER_INFORMATIONDATE & "]
, [" & DB_HEADER_RECEIVEDDATE & "]
, [" & DB_HEADER_TESTED & "]
, [" & DB_HEADER_LASTUPDATE & "]" & 
InstanceString & ") " &
				"Values(
  NEWID()
,'" & statusGUID.ToString & "'
,'" & customer & "'
,'" & serialNumber & "'
,'" & contactName & "'
,'" & contactNumber & "'
,'" & contactEmail & "'
,'" & rgaNumber & "'
,'" & invoiceNumber & "'
,'" & ServiceForm_TextBox.Text & "'
,'" & description & "'
,'" & infoDate & "'
,'" & infoDate & "'
, '" & tested & "'
,'" & lastUpdate & "'" & 
InstanceValue & ")"

				cmd.ExecuteNonQuery()

				'Get the new GUID of the record.
				cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & ServiceForm_TextBox.Text & "'"
				Dim RMAguid As Guid = cmd.ExecuteScalar

				'Add each item that is found in our code Datagridview
				For Each row As DataGridViewRow In CodeItems_DataGridView.Rows
					cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & row.Cells(DB_HEADER_CODE).Value.ToString & "'"
					Dim codeguid As Guid = cmd.ExecuteScalar


					cmd.CommandText = "INSERT INTO " & TABLE_RMACODELIST & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_RMACODESID & "],[" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_EVALUATION & "]) " &
					"VALUES('" & RMAguid.ToString & "','" & codeguid.ToString & "','1','0')"
					cmd.ExecuteNonQuery()
				Next

				'Update our SFN Number.
				Dim updateSFN As Integer = ServiceForm_TextBox.Text + 1
				cmd.CommandText = "UPDATE " & TABLE_PARAMETERS & " SET " & DB_HEADER_VALUESTRING & " = '" & updateSFN & "' WHERE [" & DB_HEADER_ID & "] = 'SFN'"
				cmd.ExecuteNonQuery()

				'Update our SFN display
				ServiceForm_TextBox.Text = ""

				'Clear our Serial Number incase we want to add the same information to another unit.
				SerialNumber_TextBox.Text = ""
			Catch ex As Exception
				MsgBox(ex.Message)
				Return False
			End Try

			'Create the folder for any files we want for this Service form.
			IO.Directory.CreateDirectory(My.Settings.ServiceFoldersDir & "\" & sfn)

			If isBoard = False Then
				'Update the system status
				sqlapi.UpdateSystemStatus(cmd, myConn, "Evaluation", serialNumber, UserName, "", True)
				Dim systemGUID As Guid

				sqlapi.GetSystemGUID(cmd, serialNumber, systemGUID, "")
				sqlapi.AddSystemComment(cmd, serialNumber, "Returned to us as a RMA.", UserName, systemGUID, "")
			Else
				'Add board comment
				sqlapi.UpdateBoardStatus(cmd, "Evaluation", serialNumber, UserName, "")
				Dim boardGUID As Guid

				sqlapi.AddBoardComment(cmd, serialNumber, "Returned to us as a RMA.", UserName, boardGUID, "")
			End If
		Else
			Try
				'We are updating a record that is already in the database.
				cmd.CommandText = "UPDATE " & TABLE_RMA & " SET 
  [" & DB_HEADER_STATUSID & "] = '" & statusGUID.ToString & "'
, [" & DB_HEADER_CUSTOMER & "] = '" & customer & "'
, [" & DB_HEADER_SERIAL_NUMBER & "] = '" & serialNumber & "'
, [" & DB_HEADER_CONTACTNAME & "] = '" & contactName & "'
, [" & DB_HEADER_CONTACTPHONE & "] = '" & contactNumber & "'
, [" & DB_HEADER_CONTACTEMAIL & "] = '" & contactEmail & "'
, [" & DB_HEADER_C_RGA & "] = '" & rgaNumber & "'
, [" & DB_HEADER_C_INVOICE & "] = '" & invoiceNumber & "'
, [" & DB_HEADER_DESCRIPTION & "] = '" & description & "'
, [" & DB_HEADER_RECEIVEDDATE & "] = '" & infoDate & "'
, [" & DB_HEADER_TESTED & "] = '" & tested & "'
, [" & DB_HEADER_LASTUPDATE & "] = '" & lastUpdate & "' 
WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & sfn & "'"
				cmd.ExecuteNonQuery()

				'Next we need to remove all of the codelists that are associated with the record and add in the new list.
				'Get the new GUID of the record.
				cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & ServiceForm_TextBox.Text & "'"
				Dim RMAguid As Guid = cmd.ExecuteScalar

				cmd.CommandText = "DELETE FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & RMAguid.ToString & "' AND [" & DB_HEADER_CUSTOMER & "] = '1'"
				cmd.ExecuteNonQuery()

				'Add each item that is found in our code Datagridview
				For Each row As DataGridViewRow In CodeItems_DataGridView.Rows
					cmd.CommandText = "Select [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & row.Cells(DB_HEADER_CODE).Value.ToString & "'"
					Dim codeguid As Guid = cmd.ExecuteScalar

					cmd.CommandText = "INSERT INTO " & TABLE_RMACODELIST & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_RMACODESID & "],[" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_EVALUATION & "]) " &
					"VALUES('" & RMAguid.ToString & "','" & codeguid.ToString & "','1','0')"
					cmd.ExecuteNonQuery()
				Next

				If isBoard = False Then
					'Update the system status
					sqlapi.UpdateSystemStatus(cmd, myConn, "Evaluation", serialNumber, UserName, "", True)
					Dim systemGUID As Guid

					sqlapi.GetSystemGUID(cmd, serialNumber, systemGUID, "")
					sqlapi.AddSystemComment(cmd, serialNumber, "Returned to us as a RMA.", UserName, systemGUID, "")
				Else
					'Add board comment
					sqlapi.UpdateBoardStatus(cmd, "Evaluation", serialNumber, UserName, "")
					Dim boardGUID As Guid

					sqlapi.AddBoardComment(cmd, serialNumber, "Returned to us as a RMA.", UserName, boardGUID, "")
				End If

				'Update our SFN display
				ServiceForm_TextBox.Text = ""

				'Clear our Serial Number incase we want to add the same information to another unit.
				SerialNumber_TextBox.Text = ""
			Catch ex As Exception
				MsgBox(ex.Message)
				Return False
			End Try
		End If

		Return True
	End Function

	Private Sub Receive_Activated() Handles Me.Activated
		'Reload our codes in case the user decided to add new ones.
		Code_ComboBox.Items.Clear()
		PopulateCodes(Code_ComboBox)
	End Sub

	Private Sub UpdateAndNext_Button_Click() Handles UpdateAndNext_Button.Click
		Dim serviceForm As String = ServiceForm_TextBox.Text

		'Try to update the RMA. If we fail, do not close and continue the process.
		If UpdateRMA() = False Then
			Return
		End If

		'Open up the next window with the service form that we are working with.
		MenuMain.ContinueWithForm(Me, Evaluation, serviceForm)
		Close()
	End Sub

End Class