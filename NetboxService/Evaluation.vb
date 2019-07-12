'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: Evaluation.vb
'
' Description: This is the evaluation step in our RMA Process. After We put in the Serial Number that we are working with, the program 
'	will lookup and see if we have any Service Forms associated with it. Any information that has been put in will be filled out 
'   automatically. Ability to grab versions over IP network. Add repair items and codes added as tables.
'
' Evaluation Date: Date that the evaluation was preformed.
' Technician: Name of the technician that preformed the evaluation.
' Versions: The versions found with the RMA unit.
'		- Software
'		- I/O
'		- Bootloader
' Technician Notes: Free text for the technician to add any other remarks to their discoveries.
' Ver. Over IP: Gets the versions from the RMA unit over the Network. Uses the IP provided. *Must be a valid IP Address* Also checks to 
'	see if the serial number that we put in the top matches what is found in the box.
' Codes: Adds the code in the drop down to the table. These items are then linked together inside the database.
' Repair Items: Adds the free text along with the provided cost to the table. *Both text boxes need to be filled out* These items are then
'	linked together inside the database.
' Update: Update the record and then clears the form for the next record.
' Update + Next: Updates the record and then moves onto the next step [shipping and billing] with the same Service Form and Serial Number.
'
' Special Keys:
'   enter = Addes the Repair Item to the data table if both item [String] and cost [Decimal] are filled out.
'	delete = deletes the selected row in the table depending on which table has the focus.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient
Imports System.Net
Imports Newtonsoft.Json

Public Class Evaluation

	Dim dt_repairItems As New DataTable()
	Dim dt_evalCodeItems As New DataTable()
	Dim jsonapi As New JSON_API()

	Dim wait As Boolean = False

	Dim thisSystemID As String = ""

	Private Sub Evaluation_Load() Handles MyBase.Load
		KeyPreview = True

		'Get today's date and populate
		Date_DTP.Value = Date.Now

		'Create our font for the DataGridViews to keep them from changing on us.
		Dim newFont As New Font("Consolas", 9.75)
		EvalCodeItems_DataGridView.DefaultCellStyle.Font = newFont
		EvalCodeItems_DataGridView.ColumnHeadersDefaultCellStyle.Font = newFont
		EvalCodeItems_DataGridView.RowHeadersDefaultCellStyle.Font = newFont

		'Set up our eval code dataTable.
		dt_evalCodeItems.Columns.Add(DB_HEADER_CODE)
		dt_evalCodeItems.Columns.Add(DB_HEADER_TYPE)
		dt_evalCodeItems.Columns.Add(DB_HEADER_DESCRIPTION)
		dt_evalCodeItems.Columns.Add(DB_HEADER_FIX)

		'Set up our Eval Code DataGridView.
		EvalCodeItems_DataGridView.DataSource = dt_evalCodeItems
		EvalCodeItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

		EvalCodeItems_DataGridView.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		EvalCodeItems_DataGridView.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		EvalCodeItems_DataGridView.Columns(2).Width = 300

		EvalCodeItems_DataGridView.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		EvalCodeItems_DataGridView.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		EvalCodeItems_DataGridView.Columns(3).Width = 300

		'Set up our repair items dataTable.
		dt_repairItems.Columns.Add(DB_HEADER_DESCRIPTION)
		dt_repairItems.Columns.Add(DB_HEADER_CHARGE)

		'Set up our repair items DataGridView.
		RepairItems_DataGridView.DefaultCellStyle.Font = newFont
		RepairItems_DataGridView.ColumnHeadersDefaultCellStyle.Font = newFont
		RepairItems_DataGridView.RowHeadersDefaultCellStyle.Font = newFont

		RepairItems_DataGridView.DataSource = dt_repairItems
		RepairItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

		RepairItems_DataGridView.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		RepairItems_DataGridView.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		RepairItems_DataGridView.Columns(0).Width = 150

		PopulateCodes(EvalCode_ComboBox)
		PopulateStatus(Status_ComboBox)
	End Sub

	Private Sub PopulateCodes(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of the codes that we have in the database.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " ORDER BY [" & DB_HEADER_CODE & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_CODE).ToString)
		Next
	End Sub

	Private Sub PopulateStatus(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Select only the status's that are 'Approval' based.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_ISAPPROVAL & "] = '1' AND [" & DB_HEADER_STATUS & "] LIKE '%Evaluated - %' ORDER BY [" & DB_HEADER_STATUS & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		box.Items.Add("")
		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_STATUS))
		Next
		box.SelectedIndex = 0
	End Sub

	Private Sub SerialNumber_TextBox_GotFocus() Handles SerialNumber_TextBox.GotFocus
		'Disable our update buttons so we can avoid an unpleasnet bug that deals with the serial number losing focus and running it's search and then trying to update.
		Update_Button.Enabled = False
		UpdateAndNext_Button.Enabled = False
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

						' select the '0' instance because this is the first time this system has come back. we have not made a second instance in our database
						cmd.CommandText = "SELECT [systemid] FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & dt_results.Rows(0)(DB_HEADER_SERIAL_NUMBER).ToString & "' AND [" & DB_HEADER_INSTANCE & "] = '0'"
						thisSystemID = cmd.ExecuteScalar().ToString()
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
		UpdateAndNext_Button.Enabled = True
		TechnicianNotes_TextBox.Focus()
		Dim cmd As New SqlCommand("", myConn)

		'Depending on what is passed through will determine what happens.
		Select Case id
			Case "CANCEL"
				'We decided not to deal with it.
				SerialNumber_TextBox.Text = ""
				SerialNumber_TextBox.Refresh()

			Case Else
				'We are dealing with an old record and need to fill in all of the information that we have on it.
				cmd.CommandText = "SELECT * FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & id & "'"
				Dim dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				'Now that we have the information from the old record, we need to fill in the text boxes with all of the information that we have.
				ServiceForm_TextBox.Text = dt_results.Rows(0)(DB_HEADER_SERVICEFORM).ToString
				SerialNumber_TextBox.Text = dt_results.Rows(0)(DB_HEADER_SERIAL_NUMBER).ToString
				Technician_TextBox.Text = dt_results(0)(DB_HEADER_TECHNICIAN).ToString
				SoftwareVer_TextBox.Text = dt_results(0)(DB_HEADER_SOFTWAREVERSION).ToString
				IOVer_TextBox.Text = dt_results(0)(DB_HEADER_IOVERSION).ToString
				BootVer_TextBox.Text = dt_results(0)(DB_HEADER_BOOTVERSION).ToString
				TechnicianNotes_TextBox.Text = dt_results(0)(DB_HEADER_TECHNICIANNOTES).ToString

				' we need to grab the id of the system that we are working with. This needs to have the serial number and the instance.
				Dim instance As String = dt_results(0)(DB_HEADER_INSTANCE).ToString
				cmd.CommandText = "SELECT [systemid] FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & dt_results.Rows(0)(DB_HEADER_SERIAL_NUMBER).ToString & "' AND [" & DB_HEADER_INSTANCE & "] = '" & instance & "'"
				thisSystemID = cmd.ExecuteScalar().ToString()

				'Get all of the repair items that are associated with this RMA.
				Dim thisGUID As String = dt_results(0)(DB_HEADER_ID).ToString
				dt_repairItems.Rows.Clear()

				cmd.CommandText = "SELECT [" & DB_HEADER_DESCRIPTION & "],[" & DB_HEADER_CHARGE & "] FROM " & TABLE_RMAREPAIRITEMS & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "'"

				dt_repairItems.Load(cmd.ExecuteReader())

				RepairItems_DataGridView.DataSource = Nothing
				RepairItems_DataGridView.DataSource = dt_repairItems
				RepairItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

				RepairItems_DataGridView.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
				RepairItems_DataGridView.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
				RepairItems_DataGridView.Columns(0).Width = 150
				RepairItems_DataGridView.ClearSelection()

				'Get all of the repair codes that are associated with this RMA.
				dt_evalCodeItems.Rows.Clear()

				cmd.CommandText = "SELECT * FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "' AND [" & DB_HEADER_EVALUATION & "] = '1'"
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
						dt_evalCodeItems.Rows.Add(dt_codeInformation(0)(DB_HEADER_CODE).ToString, dt_codeInformation(0)(DB_HEADER_TYPE).ToString, dt_codeInformation(0)(DB_HEADER_DESCRIPTION).ToString, dt_codeInformation(0)(DB_HEADER_FIX).ToString)
					End If
				Next

				EvalCodeItems_DataGridView.DataSource = Nothing
				EvalCodeItems_DataGridView.DataSource = dt_evalCodeItems
				EvalCodeItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

				EvalCodeItems_DataGridView.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
				EvalCodeItems_DataGridView.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
				EvalCodeItems_DataGridView.Columns(2).Width = 300

				EvalCodeItems_DataGridView.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
				EvalCodeItems_DataGridView.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
				EvalCodeItems_DataGridView.Columns(3).Width = 300
				EvalCodeItems_DataGridView.ClearSelection()
		End Select
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		'Try to update our record. If we fail, do not close the window so the user can fix any issues.
		If UpdateRMA() = False Then
			Return
		End If
	End Sub

	Private Function UpdateRMA() As Boolean
		Dim hasErrors As Boolean = False
		Dim includeStatus As Boolean = False
		Dim errorMessage As String = ""

		'Serial Number logic checking
		Dim cmd As New SqlCommand("", myConn)

		Dim InstanceString As String = ", [" & DB_HEADER_INSTANCE & "]"
		Dim InstanceValue As String = ", NULL"

		'Check to make sure that we have a serial number to work with.
		If SerialNumber_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a Serial Number." & vbNewLine
		Else
			'Check to see if we are a valid serial number.
			cmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "' AND 
[dbo.SystemStatus.id] != (Select id from SystemStatus where name = 'Scrapped')"
			Dim dt_results As New DataTable()
			dt_results.Load(cmd.ExecuteReader())

			If dt_results.Rows.Count < 1 Then
				'Check to see if we are a valid Board serial number.
				cmd.CommandText = "Select * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
				dt_results = New DataTable()
				dt_results.Load(cmd.ExecuteReader())

				If dt_results.Rows.Count < 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					errorMessage = errorMessage & "Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the '" & SQL_API.DATABASE & "' database." & vbNewLine
				End If
			Else
				InstanceValue = ",'" & dt_results.Rows(0)(DB_HEADER_INSTANCE) & "'"
			End If
		End If

		If 0 < Status_ComboBox.Text.Length Then
			includeStatus = True
		End If

		'If we have any errors, display them and do not continue the update.
		If errorMessage.Length <> 0 Then
			MsgBox(errorMessage)
			Return False
		End If

		'Update our information to the database.
		Dim sfn As Integer = ServiceForm_TextBox.Text
		Dim infoDate As new Date (Date_DTP.Value.Year, Date_DTP.Value.Month, Date_DTP.Value.Day)
		Dim serialNumber As String = SerialNumber_TextBox.Text
		Dim technician As String = Technician_TextBox.Text
		Dim softwareVer As String = SoftwareVer_TextBox.Text
		Dim IOVer As String = IOVer_TextBox.Text
		Dim BootVer As String = BootVer_TextBox.Text
		Dim Notes As String = TechnicianNotes_TextBox.Text
		Dim lastUpdate As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)

		'Replace any single ['] quotes with double single [''] quotes for the SQL syntax.
		Notes = Notes.Replace("'", "''")

		'Get the GUID of the status that we are cahnging to.
		Dim statusGUID As Guid = cmd.ExecuteScalar
		Dim statusString As String = ""
		If includeStatus = True Then
			cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = '" & Status_ComboBox.Text & "'"
			statusGUID = cmd.ExecuteScalar
			statusString = "[" & DB_HEADER_STATUSID & "] = '" & statusGUID.ToString & "', "
		End If

		Try
			'Updating our record in the database.
			cmd.CommandText = "UPDATE " & TABLE_RMA & " SET " & statusString & "[" & DB_HEADER_TECHNICIAN & "] = '" & technician & "', " &
				"[" & DB_HEADER_SERIAL_NUMBER & "] = '" & serialNumber & "', [" & DB_HEADER_SOFTWAREVERSION & "] = '" & softwareVer & "', [" & DB_HEADER_IOVERSION & "] = '" & IOVer & "', " &
				"[" & DB_HEADER_BOOTVERSION & "] = '" & BootVer & "', [" & DB_HEADER_TECHNICIANNOTES & "] = '" & Notes & "', " &
				"[" & DB_HEADER_EVALUATIONDATE & "] = '" & infoDate & "', [" & DB_HEADER_LASTUPDATE & "] = '" & lastUpdate & "' WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & sfn & "'"
			cmd.ExecuteNonQuery()

			'Next we need to get the GUID of the SFN 
			cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & sfn & "'"
			Dim rmaGUID As Guid = cmd.ExecuteScalar

			'Delete all of the previous repair items associated with this form.
			cmd.CommandText = "DELETE FROM " & TABLE_RMAREPAIRITEMS & " WHERE [" & DB_HEADER_RMAID & "] = '" & rmaGUID.ToString & "'"
			cmd.ExecuteNonQuery()

			'Add each item that is found in our Datagridview
			For Each row As DataGridViewRow In RepairItems_DataGridView.Rows
				cmd.CommandText = "INSERT INTO " & TABLE_RMAREPAIRITEMS & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_DESCRIPTION & "],[" & DB_HEADER_CHARGE & "]) " &
					"VALUES('" & rmaGUID.ToString & "','" & row.Cells(DB_HEADER_DESCRIPTION).Value.ToString & "','" & row.Cells(DB_HEADER_CHARGE).Value.ToString & "')"
				cmd.ExecuteNonQuery()
			Next

			'Next we need to remove all of the codelists that are associated with the record and add in the new list.
			cmd.CommandText = "DELETE FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & rmaGUID.ToString & "'  AND [" & DB_HEADER_EVALUATION & "] = '1'"
			cmd.ExecuteNonQuery()

			'Add each item that is found in our code Datagridview
			For Each row As DataGridViewRow In EvalCodeItems_DataGridView.Rows
				cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & row.Cells(DB_HEADER_CODE).Value.ToString & "'"
				Dim codeguid As Guid = cmd.ExecuteScalar

				cmd.CommandText = "INSERT INTO " & TABLE_RMACODELIST & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_RMACODESID & "],[" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_EVALUATION & "]) " &
					"VALUES('" & rmaGUID.ToString & "','" & codeguid.ToString & "','0','1')"
				cmd.ExecuteNonQuery()
			Next

			'Update our SFN display
			ServiceForm_TextBox.Text = ""

			'Clear our Serial Number
			SerialNumber_TextBox.Text = ""
		Catch ex As Exception
			MsgBox(ex.Message)
			Return False
		End Try

		Return True
	End Function

	Private Sub AddItem_Button_Click() Handles AddItem_Button.Click
		Dim errorMessage As String = ""
		Dim cost As Decimal

		'Check to see if we have text.
		If RepairItem_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a repair item to add." & vbNewLine
		End If

		'Check to see if we have cost.
		If Cost_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a cost for the repair item." & vbNewLine
		Else
			Try
				'try to convert the cost into a decimal.
				cost = Cost_TextBox.Text
			Catch ex As Exception
				errorMessage = errorMessage & "Please input a decimal cost value." & vbNewLine
			End Try
		End If

		'If we have any errors, display them and do not continue.
		If errorMessage.Length <> 0 Then
			MsgBox(errorMessage)
			Return
		End If

		'Add it to the Datatable.
		dt_repairItems.Rows.Add(RepairItem_TextBox.Text, Cost_TextBox.Text)

		'Clear both textboxes for the next item
		RepairItem_TextBox.Text = ""
		Cost_TextBox.Text = ""

		'Send the focus back to the repairItem textbox
		RepairItem_TextBox.Focus()
	End Sub

	Private Sub DeleteItem_Button_Click() Handles DeleteItem_Button.Click
		'First check to see if we have selected any of the rows in the datatable.
		If RepairItems_DataGridView.SelectedCells.Count = 1 Then
			'Search for the row that we are going to be deleting.
			Dim rowIndex As Integer = RepairItems_DataGridView.SelectedCells.Item(0).RowIndex
			Dim foundRows As DataRow() = dt_repairItems.Select("[" & DB_HEADER_DESCRIPTION & "] = '" & RepairItems_DataGridView.Rows(rowIndex).Cells(DB_HEADER_DESCRIPTION).Value & "' AND [" & DB_HEADER_CHARGE & "] = '" & RepairItems_DataGridView.Rows(rowIndex).Cells(DB_HEADER_CHARGE).Value & "'")

			'Delete the row in our datatable.
			For Each row As DataRow In foundRows
				row.Delete()
			Next
		End If
	End Sub

	Private Sub AddEvalCode_Button_Click() Handles AddEvalCode_Button.Click
		'Make sure that we have a code selected in our drop box.
		If EvalCode_ComboBox.Text.Length <> 0 Then
			'See if we have added it already to the datatable
			Dim rows As DataRow() = dt_evalCodeItems.Select("[" & DB_HEADER_CODE & "] = '" & EvalCode_ComboBox.Text & "'")

			'Only add if we have not already.
			If rows.Count = 0 Then
				Dim mycmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & EvalCode_ComboBox.Text & "'", myConn)
				Dim results As New DataTable
				results.Load(mycmd.ExecuteReader)

				'Add it to the Datatable.
				dt_evalCodeItems.Rows.Add(results(0)(DB_HEADER_CODE), results(0)(DB_HEADER_TYPE), results(0)(DB_HEADER_DESCRIPTION), results(0)(DB_HEADER_FIX))
			End If
		End If
	End Sub

	Private Sub DeleteCode_Button_Click() Handles DeleteEvalCode_Button.Click
		'First check to see if we have selected any of the rows in the datatable.
		If EvalCodeItems_DataGridView.SelectedCells.Count = 1 Then
			'Search for the row that we are going to be deleting.
			Dim rowIndex As Integer = EvalCodeItems_DataGridView.SelectedCells.Item(0).RowIndex
			Dim foundRows As DataRow() = dt_evalCodeItems.Select("[" & DB_HEADER_CODE & "] = '" & EvalCodeItems_DataGridView.Rows(rowIndex).Cells(DB_HEADER_CODE).Value & "'")

			'Delete the row in our datatable.
			For Each row As DataRow In foundRows
				row.Delete()
			Next
		End If
	End Sub

	Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode.Equals(Keys.Delete) Then
			'If we press the Delete key, call our remove function relevant to the dataGridView that we have focused.
			If RepairItems_DataGridView.Focused = True Then
				DeleteItem_Button_Click()
			ElseIf EvalCodeItems_DataGridView.Focused = True Then
				DeleteCode_Button_Click()
			End If

		ElseIf e.KeyCode.Equals(Keys.Enter) Then
			'If we press the Enter key, call our Add function only if we are focused on the right textboxes.
			If RepairItem_TextBox.Focused = True Or Cost_TextBox.Focused = True Then
				Call AddItem_Button_Click()
			End If
		End If
	End Sub

	Private Sub IP_Button_Click() Handles IP_Button.Click
		Dim WResponse As String = ""
		Dim result As String = ""
		Dim obj = Nothing

		'Check to see if our IP textbox is empty.
		If IP_TextBox.Text.Length = 0 Then
			MsgBox("Please specify IP Address to register")
			Return
		End If

		'Check to see if our serial number textbox is empty
		If SerialNumber_TextBox.Text.Length = 0 Then
			MsgBox("Please specify a Serial number to register")
			Return
		End If

		'Get the JSON from the remote server so we can see the current machine information.
		If jsonapi.GetMachineInfo(IP_TextBox.Text, WResponse) Then
			Cursor = Cursors.Default
			Try
				obj = JsonConvert.DeserializeObject(Of JSON_InfoResult)(WResponse)
			Catch
				MsgBox("Could not convert JSON result string")
				Return
			End Try

			'Check to see if the serial numbers match eachother
			If SerialNumber_TextBox.Text <> obj.serial.ToString Then
				MsgBox("Expected: " & SerialNumber_TextBox.Text & " - Received: " & obj.serial.ToString)
				Return
			End If

			'Update the information in the correct textboxes.
			SoftwareVer_TextBox.Text = obj.version.ToString.Substring(1)
			IOVer_TextBox.Text = obj.ioversion.Substring(1)
			BootVer_TextBox.Text = obj.blversion.Substring(1)
		Else
			MsgBox("Could not get JSON information from the machine")
			Return
		End If

	End Sub

	Private Sub Directory_Button_Click() Handles Directory_Button.Click
		'Check to see if the folder path is valid or not.
		If IO.Directory.Exists(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text) = False Then
			MsgBox(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text & " Service Folder was not found. Please update your settings to the correct " &
				   "location of the folder and make sure that it exists there.")
			Return
		End If

		'Open the folder location in a new window.
		Process.Start(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text)
	End Sub

	Private Sub Evaluation_Activated() Handles Me.Activated
		'Reload our codes in case the user decided to add new ones.
		EvalCode_ComboBox.Items.Clear()
		PopulateCodes(EvalCode_ComboBox)
		Status_ComboBox.Items.Clear()
		PopulateStatus(Status_ComboBox)
	End Sub

	Private Sub UpdateAndNext_Button_Click() Handles UpdateAndNext_Button.Click
		Dim serviceForm As String = ServiceForm_TextBox.Text

		'Try to update the RMA. If we fail, do not close and continue the process.
		If UpdateRMA() = False Then
			Return
		End If

		'Open up the next window with the service form that we are working with.
		MenuMain.ContinueWithForm(Me, Approval, serviceForm)
		Close()
	End Sub

	Private Sub GetLogs_Button_Click() Handles GetLogs_Button.Click
		Cursor = Cursors.WaitCursor

		Dim WResponse As String = ""
		Dim obj = Nothing

		'Check to see if our IP textbox is empty.
		If IP_TextBox.Text.Length = 0 Then
			MsgBox("Please specify IP Address to register")
			Return
		End If

		'Check to see if our serial number textbox is empty
		If SerialNumber_TextBox.Text.Length = 0 Then
			MsgBox("Please specify a Serial number to register")
			Return
		End If

		'Check to see if the folder path is valid or not.
		If IO.Directory.Exists(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text) = False Then
			MsgBox(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text & " Service Folder was not found. Please update your settings to the correct " &
				   "location of the folder and make sure that it exists there.")
			Return
		End If

		'Get the JSON from the remote server so we can see the current machine information.
		If jsonapi.GetMachineInfo(IP_TextBox.Text, WResponse) Then
			Try
				obj = JsonConvert.DeserializeObject(Of JSON_InfoResult)(WResponse)
			Catch
				MsgBox("Could not convert JSON result string")
				Return
			End Try

			'Check to see if the serial numbers match eachother
			If (SQL_API.DATABASE <> "Devel") Then
				If SerialNumber_TextBox.Text <> obj.serial.ToString Then
					MsgBox("Expected: " & SerialNumber_TextBox.Text & " - Received: " & obj.serial.ToString)
					Return
				End If
			End If

			jsonapi.SendDiagnostic(IP_TextBox.Text, WResponse)

			Dim retval As Boolean

			retval = DownloadFiles("belleville")
			If retval = False Then
                retval = DownloadFiles("aquametrix")
            End If
            'Chain any other passwords to try here.
            'If retval = False Then
            '	retval = DownloadFiles("password")
            'End If
        Else
			MsgBox("Could not get JSON information from the machine")
			Return
		End If
		Cursor = Cursors.Default
	End Sub

	Private Function DownloadFiles(ByRef password As String) As Boolean
		Dim client = New WebClient()

		Dim netCredential As NetworkCredential = New NetworkCredential("admin", password)

		client.Credentials = netCredential

		'Grab our logs.
		For Each file As String In runlogDownloadList
			Dim URL As String = "http://" & IP_TextBox.Text & "/download?name=" & file
			Dim saveLocation As String = My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text & "\" & file

			Try
				client.DownloadFile(URL, saveLocation)
			Catch ex As Exception
				' Move onto the next file.
			End Try
		Next

		Dim year As Integer = Date.Now.Year
		Dim month As Integer = Date.Now.Month
		For i As Integer = 1 To 12
			Dim URL As String = "http://" & IP_TextBox.Text & "/download?name=A" & year & month.ToString("D2") & ".bin"
			Dim saveLocation As String = My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text & "\A" & year & month.ToString("D2") & ".bin"

			Try
				client.DownloadFile(URL, saveLocation)
			Catch ex As Exception
				' Move onto the next file.
			End Try
			month -= 1

			If month = 0 Then
				month = 12
				year -= 1
			End If
		Next

		Return True
	End Function

	Private Sub Comment_Button_Click() Handles Comment_Button.Click
		Dim DoAddBoardComment As New AddBoardComment(thisSystemID)
		DoAddBoardComment.ShowDialog()

		If DoAddBoardComment.response.Length <> 0 Then
			TechnicianNotes_TextBox.AppendText(vbNewLine & DoAddBoardComment.response)

			UpdateRMA()
		End If
	End Sub

End Class