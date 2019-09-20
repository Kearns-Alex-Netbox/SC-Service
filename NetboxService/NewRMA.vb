'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: NewRMA.vb
'
' Description: New RMA step in our RMA Process. This handles the creation of the RMA with what details are given to us before we have
'	recieved the unit.
'
' Information Date: The date that the information was given.
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
' Add: Adds the record to the database and only clears the Serial number incase we have multiple serial numbers to put in the same
'	information. The Service Form Number is also then incremented by one.
'
' Special Keys:
'	delete = deletes the selected row in the table depending on which table has the focus.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class NewRMA

	Dim dt_customerCodeItems As New DataTable()

	Private Sub GeneralInformation_Load() Handles MyBase.Load
		'Get today's date and populate
		Information_DTP.Value = Date.Now

		'Get the most recent Service Form number.
		Dim cmd As New SqlCommand("SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'", myConn)
		ServiceForm_TextBox.Text = cmd.ExecuteScalar

		PopulateCodes(Code_ComboBox)

		' set up prdictive text for customers
		Customer_TextBox.AutoCompleteMode = AutoCompleteMode.Suggest
        Customer_TextBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        Dim DataCollection As New AutoCompleteStringCollection()

		' getData(DataCollection)
		cmd.CommandText = "SELECT DISTINCT [" & DB_HEADER_CUSTOMER & "] FROM " & TABLE_RMA
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
			box.Items.Add(dr(DB_HEADER_CODE))
		Next
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub AddRMA_Button_Click() Handles AddRMA_Button.Click
		'Check to see if the folder directory has been set up before moving on.
		If My.Settings.ServiceFoldersDir.Length = 0 Then
			MsgBox("Service Folder Directory has not been set up yet. Please update your settings.")
			Return
		End If

		'Check to see if the folder path is valid or not.
		If IO.Directory.Exists(My.Settings.ServiceFoldersDir) = False Then
			MsgBox("Service Folder Directory does not exist. Please update your settings.")
			Return
		End If

		Dim hasErrors As Boolean = False
		Dim errorMessage As String = ""
		
		'Serial Number logic checking
		Dim cmd As New SqlCommand("", myConn)

		Dim InstanceString As String = ", [" & DB_HEADER_INSTANCE & "]"
		Dim InstanceValue As String = ", NULL"

		'Check to make sure that we have a serial number to work with.
		If SerialNumber_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a Serial Number." & vbNewLine
		Elseif SerialNumber_TextBox.Text <> "NA"
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
			Return
		End If

		'Add our information to the database as a new entry.
		Dim sfn As Integer = ServiceForm_TextBox.Text
		Dim infoDate As new Date (Information_DTP.Value.Year, Information_DTP.Value.Month, Information_DTP.Value.Day)
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

		Try
			'Get the GUID of the status that we want
			cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = 'Issued'"
			Dim statusGUID As Guid = cmd.ExecuteScalar

			'Add the record to the database.
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
,'" & tested & "'
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
			cmd.CommandText = "SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
			ServiceForm_TextBox.Text = cmd.ExecuteScalar

			'Clear our Serial Number incase we want to add the same information to another unit.
			SerialNumber_TextBox.Text = ""
		Catch ex As Exception
			MsgBox(ex.Message)
			Return
		End Try

		'Create the folder for any files we want for this Service form.
		IO.Directory.CreateDirectory(My.Settings.ServiceFoldersDir & "\" & sfn)
	End Sub

	Private Sub NewRMA_Activated() Handles Me.Activated
		'Reload our codes in case the user decided to add new ones.
		Code_ComboBox.Items.Clear()
		PopulateCodes(Code_ComboBox)
	End Sub

End Class