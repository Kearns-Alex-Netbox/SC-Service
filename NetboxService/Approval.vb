'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: Approval.vb
'
' Description: Approval Step in our RMA Process. After We put in the Serial Number that we are working with, the program will lookup and
'	see if we have any Service Forms associated with it. Any information that has been put in will be filled out automatically.
'
' Approval Date: The date that the approval was given.
' Status: What type of approval was given. This is taken from a list of 'approval' status's and can have more added.
' Notes: Any kind of notes that are needed along with the approval.
' Update: Update the record and then clears the form for the next record.
' Update + Next: Updates the record and then moves onto the next step [shipping and billing] with the same Service Form and Serial Number.
'
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class Approval

	Dim wait As Boolean = False

	Private Sub Approval_Load() Handles MyBase.Load
		'Get today's date and populate
		Date_DTP.Value = Date.Now

		PopulateStatus(Status_ComboBox)
	End Sub

	Private Sub PopulateStatus(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Select only the status's that are 'Approval' based.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_ISAPPROVAL & "] = '1' AND [" & DB_HEADER_STATUS & "] LIKE '%Approved - %' ORDER BY [" & DB_HEADER_STATUS & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_STATUS))
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

				If dt_results.Rows.Count <> 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					MsgBox("Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the PRODUCTION database.")
					Return
				End If
			End If

			'Next, check to see if we have any RMA records in the database.
			'If we have 1 or more, we need to go into a special form to choose which record we are dealing with.
			cmd.CommandText = "SELECT [" & DB_HEADER_SERVICEFORM & "], [" & DB_HEADER_CUSTOMER & "], [" & DB_HEADER_INFORMATIONDATE & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERIAL_NUMBER & "] = '" & SerialNumber_TextBox.Text & "' ORDER BY [" & DB_HEADER_SERVICEFORM & "] ASC"
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
		UpdateAndNext_Button.Enabled = True
		Status_ComboBox.Focus()
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
				ApprovalNotes_TextBox.Text = dt_results(0)(DB_HEADER_APPROVALNOTES).ToString

				'Get the current status using the GUID 
				cmd.CommandText = "SELECT [" & DB_HEADER_STATUS & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_ID & "] = '" & dt_results(0)(DB_HEADER_STATUSID).ToString & "'"
				Dim currentStatus As String = cmd.ExecuteScalar
				Status_ComboBox.SelectedIndex = Status_ComboBox.Items.IndexOf(currentStatus)

		End Select
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		'Try to update our record. If we fail, do not close the window so the user can fix any issues.
		If UpdateRMA() = False Then
			Return
		End If
	End Sub

	Private Function UpdateRMA() As Boolean
		Dim isBoard As Boolean = False
		Dim hasErrors As Boolean = False
		Dim errorMessage As String = ""

		'Serial Number logic checking
		Dim cmd As New SqlCommand("", myConn)

		'Check to make sure that we have a serial number to work with.
		If SerialNumber_TextBox.Text.Length = 0 Then
			errorMessage = errorMessage & "Please input a Serial Number." & vbNewLine
			'Else
			'	'Check to see if we are a valid serial number.
			'	cmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
			'	Dim dt_results As New DataTable()
			'	dt_results.Load(cmd.ExecuteReader())

			'	If dt_results.Rows.Count < 1 Then
			'		'Check to see if we are a valid Board serial number.
			'		cmd.CommandText = "SELECT * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
			'		dt_results = New DataTable()
			'		dt_results.Load(cmd.ExecuteReader())
			'		isBoard = True

			'		If dt_results.Rows.Count < 1 Then
			'			'If we still do not have a valid serial number then we cannot add it to the RMA database.
			'			errorMessage = errorMessage & "Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the PRODUCTION database." & vbNewLine
			'		End If
			'	End If
		End If

		If Status_ComboBox.Text = "" Then
			errorMessage = errorMessage & "Please select an Approval Status." & vbNewLine
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
		Dim approvalNotes As String = ApprovalNotes_TextBox.Text
		Dim lastUpdate As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)

		'Replace any single ['] quotes with double single [''] quotes for the SQL syntax.
		approvalNotes = approvalNotes.Replace("'", "''")

		'Get the GUID of the status that we are cahnging to.
		cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = '" & Status_ComboBox.Text & "'"
		Dim statusGUID As Guid = cmd.ExecuteScalar

		Try
			'Updating our record in the database.
			cmd.CommandText = "UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_STATUSID & "] = '" & statusGUID.ToString & "', [" & DB_HEADER_SERIAL_NUMBER & "] = '" & serialNumber & "', " &
				"[" & DB_HEADER_APPROVALNOTES & "] = '" & approvalNotes & "', [" & DB_HEADER_APPROVALDATE & "] = '" & infoDate & "', " &
				"[" & DB_HEADER_LASTUPDATE & "] = '" & lastUpdate & "' WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & sfn & "'"
			cmd.ExecuteNonQuery()

			If isBoard = False Then
				'Add system comment
				Dim systemGUID As Guid

				sqlapi.GetSystemGUID(cmd, serialNumber, systemGUID, "")
				sqlapi.AddSystemComment(cmd, serialNumber, "System approved for: " & Status_ComboBox.Text & ".", UserName, systemGUID, "")
			Else
				'Add board comment
				Dim boardGUID As Guid

				sqlapi.AddBoardComment(cmd, serialNumber, "Board approved for: " & Status_ComboBox.Text & ".", UserName, boardGUID, "")
			End If


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

	Private Sub Approval_Activated() Handles Me.Activated
		'Reload our status's in case the user decided to add new ones.
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
		MenuMain.ContinueWithForm(Me, ShipAndBill, serviceForm)
		Close()
	End Sub

End Class