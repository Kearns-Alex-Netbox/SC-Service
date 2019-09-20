'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: RecordDetails.vb
'
' Description: RMA Details. All of the information regarding the RMA unit is found here. Each Tab is a different step. Details of the 
'	system as a whole are also displayed here. Each field is available for edit with the exception of the service form number. Extra
'	confirmatioin is used for changing the serial number or deleting the record.
'
'   Tab 1 Customer Info:
'   Tab 2 Evaluation:
'   Tab 3 Shipping/Billing:
'   Tab 4 Board Details: [Only shows when the RMA is a board and not a system.
'   Tab 5 System Details: Boards associated with the system can each be clicked to see more information and auidt records.
'   Tab 6 Extra Components: All of the extra little things that make a big difference from unit to unit
'
' Up/Down arrows: Moves through the resutls from the search table.
' Folder Directory: Opens up the folder location that is associated with this RMA unit to see any logs or pdfs.
' Enable Delete: Checkbox that is required to be checked before pressing the delete button to delete the record.
' Delete: Deletes the record and closes the window with a search refresh for the table.
' Update: Updates the current record with all of the new/old information provided. Sets a flag that will signal the search to refreash
'	once the window is closed.
'
' Special Keys:
'   enter = Addes the Repair Item to the data table if both item [String] and cost [Decimal] are filled out.
'	delete = deletes the selected row in the table depending on which table has the focus.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient
Imports System.Net
Imports Newtonsoft.Json

Public Class RecordDetails
	'Constants used for the board/system details.
	Const MOTHERBOARD As String = "MotherBoard"
	Const MAIN_CPU As String = "MainCPU"
	Const SLOT_2 As String = "Slot2"
	Const SLOT_3 As String = "Slot3"
	Const SLOT_4 As String = "Slot4"
	Const SLOT_5 As String = "Slot5"
	Const SLOT_6 As String = "Slot6"
	Const SLOT_7 As String = "Slot7"
	Const SLOT_8 As String = "Slot8"
	Const SLOT_9 As String = "Slot9"
	Const SLOT_10 As String = "Slot10"

	'Used to display the updated message.
	Const TIMEOUT As Integer = 5000    '1 second = 1000    1 minute = 60000    1/2 hour = 1800000    1 hour = 3600000

	Public changed As Boolean = False

	Dim thisCurrentRow As Integer
	Dim thisCurrentTable As DataTable
	Dim thisSFN As String
	Dim thisGUID As String
	Dim thisSerialNumber As String
	Dim dt_repairItems As New DataTable()
	Dim dt_customerCodeItems As New DataTable()
	Dim dt_evalCodeItems As New DataTable()
	Dim jsonapi As New JSON_API()
	Dim thissystemID As String = ""

	'Protection variable to prevent our combo box change event.
	Dim doNotUpdate As Boolean = True

	Public Sub New(ByRef currentRow As Integer, ByRef currentTable As DataTable)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		thisCurrentRow = currentRow
		thisCurrentTable = currentTable
	End Sub

	Private Sub RecordDetails_Load() Handles MyBase.Load
		KeyPreview = True

		'Populate all of our drop downs
		PopulateCodes(Code_ComboBox)
		PopulateStatus(Status_ComboBox)
		PopulateCodes(EvalCode_ComboBox)
		PopulateBillTypes(BillingType_ComboBox)

		'Set up our tables
		dt_customerCodeItems.Columns.Add(DB_HEADER_CODE)
		dt_customerCodeItems.Columns.Add(DB_HEADER_TYPE)
		dt_customerCodeItems.Columns.Add(DB_HEADER_DESCRIPTION)
		dt_customerCodeItems.Columns.Add(DB_HEADER_FIX)

		dt_evalCodeItems.Columns.Add(DB_HEADER_CODE)
		dt_evalCodeItems.Columns.Add(DB_HEADER_TYPE)
		dt_evalCodeItems.Columns.Add(DB_HEADER_DESCRIPTION)
		dt_evalCodeItems.Columns.Add(DB_HEADER_FIX)

		TabControl1.TabPages.Remove(Extras_TabPage)
		UpdateInformation()
	End Sub

	Private Sub UpdateInformation()
		Try
			'Check to see if this is the first row or not.
			If thisCurrentRow = 0 Then
				Up_Button.Enabled = False
			Else
				Up_Button.Enabled = True
			End If

			'Check to see if this is the last row or not.
			If thisCurrentRow = thisCurrentTable.Rows.Count - 1 Then
				Down_Button.Enabled = False
			Else
				Down_Button.Enabled = True
			End If

			thisSFN = thisCurrentTable.Rows(thisCurrentRow)(DB_HEADER_SERVICEFORM)

			'Declare all of the variables that we are going to use
			Dim mycmd As New SqlCommand("SELECT * FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & thisSFN & "'", myConn)
			Dim dt_rmaRecord = New DataTable()
			dt_rmaRecord.Load(mycmd.ExecuteReader)

			thisGUID = dt_rmaRecord(0)(DB_HEADER_ID).ToString
			Dim customer As String = dt_rmaRecord(0)(DB_HEADER_CUSTOMER).ToString
			thisSerialNumber = dt_rmaRecord(0)(DB_HEADER_SERIAL_NUMBER).ToString
			Dim statusGUID As String = dt_rmaRecord(0)(DB_HEADER_STATUSID).ToString
			Dim contactName As String = dt_rmaRecord(0)(DB_HEADER_CONTACTNAME).ToString
			Dim contactPhone As String = dt_rmaRecord(0)(DB_HEADER_CONTACTPHONE).ToString
			Dim contactEmail As String = dt_rmaRecord(0)(DB_HEADER_CONTACTEMAIL).ToString
			Dim rga As String = dt_rmaRecord(0)(DB_HEADER_C_RGA).ToString
			Dim invoice As String = dt_rmaRecord(0)(DB_HEADER_C_INVOICE).ToString
			'Service Form is already been passed in.
			Dim description As String = dt_rmaRecord(0)(DB_HEADER_DESCRIPTION).ToString
			Dim technician As String = dt_rmaRecord(0)(DB_HEADER_TECHNICIAN).ToString
			Dim softwareVersion As String = dt_rmaRecord(0)(DB_HEADER_SOFTWAREVERSION).ToString
			Dim ioVersion As String = dt_rmaRecord(0)(DB_HEADER_IOVERSION).ToString
			Dim bootVersion As String = dt_rmaRecord(0)(DB_HEADER_BOOTVERSION).ToString
			Dim shipGUID As String = dt_rmaRecord(0)(DB_HEADER_SHIPID).ToString
			Dim billTypeGUID As String = dt_rmaRecord(0)(DB_HEADER_BILLTYPEID).ToString
			Dim billGUID As String = dt_rmaRecord(0)(DB_HEADER_BILLID).ToString
			Dim rmaPO As String = dt_rmaRecord(0)(DB_HEADER_C_RMAPO).ToString
			Dim netboxInvoice As String = dt_rmaRecord(0)(DB_HEADER_NB_INVOICE).ToString
			Dim informationDate As String = dt_rmaRecord(0)(DB_HEADER_INFORMATIONDATE).ToString
			Dim ReceivedDate As String = dt_rmaRecord(0)(DB_HEADER_RECEIVEDDATE).ToString
			Dim evaluationDate As String = dt_rmaRecord(0)(DB_HEADER_EVALUATIONDATE).ToString
			Dim shipDate As String = dt_rmaRecord(0)(DB_HEADER_SHIPDATE).ToString
			Dim lastupdate As String = dt_rmaRecord(0)(DB_HEADER_LASTUPDATE).ToString
			Dim technicianNotes As String = dt_rmaRecord(0)(DB_HEADER_TECHNICIANNOTES).ToString
			Dim testedCheck As String = dt_rmaRecord(0)(DB_HEADER_TESTED).ToString
			Dim tested As Boolean = False

			If testedCheck.Length = 0 Then
				tested = False
			Else
				tested = dt_rmaRecord(0)(DB_HEADER_TESTED).ToString
			End If


			'Populate the header information
			ServiceForm_TextBox.Text = thisSFN
			SerialNumber_TextBox.Text = thisSerialNumber

			mycmd.CommandText = "SELECT [" & DB_HEADER_STATUS & "] FROM  " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_ID & "] = '" & statusGUID & "'"
			Dim status As String = mycmd.ExecuteScalar.ToString

			Status_ComboBox.SelectedIndex = Status_ComboBox.Items.IndexOf(status)

			'Only set date information if we have values.
			If lastupdate.Length <> 0 Then
				Dim useDate As Date = lastupdate
				LastDay_TextBox.Text = useDate.Day
				LastMonth_TextBox.Text = useDate.Month
				LastYear_TextBox.Text = useDate.Year
			End If

			'Populate the information tab

			'Only set date information if we have values.
			If informationDate.Length <> 0 Then
				Dim useDate As Date = informationDate
				InfoDay_TextBox.Text = useDate.Day
				InfoMonth_TextBox.Text = useDate.Month
				InfoYear_TextBox.Text = useDate.Year
			End If

			'Only set date information if we have values.
			If ReceivedDate.Length <> 0 Then
				Dim useDate As Date = ReceivedDate
				Receiveday_TextBox.Text = useDate.Day
				ReceiveMonth_TextBox.Text = useDate.Month
				ReceiveYear_TextBox.Text = useDate.Year
			End If

			Customer_TextBox.Text = customer
			ContactName_TextBox.Text = contactName
			ContactNumber_TextBox.Text = contactPhone
			ContactEmail_TextBox.Text = contactEmail
			RGANumber_TextBox.Text = rga
			InvoiceNumber_TextBox.Text = invoice
			Description_TextBox.Text = description
			Tested_CheckBox.Checked = tested

			dt_customerCodeItems.Rows.Clear()

			'Get all of the customer codes
			mycmd.CommandText = "SELECT * FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "' AND [" & DB_HEADER_CUSTOMER & "] = '1'"
			Dim dt_coderesults As New DataTable
			dt_coderesults.Load(mycmd.ExecuteReader)

			For Each row As DataRow In dt_coderesults.Rows
				Try
					mycmd.CommandText = "SELECT * FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_ID & "] = '" & row(DB_HEADER_RMACODESID).ToString & "'"
				Catch ex As Exception
					MsgBox(ex.Message)
				End Try

				Dim dt_codeInformation As New DataTable
				dt_codeInformation.Load(mycmd.ExecuteReader)

				If dt_codeInformation.Rows.Count <> 0 Then
					Try
						dt_customerCodeItems.Rows.Add(dt_codeInformation(0)(DB_HEADER_CODE).ToString, dt_codeInformation(0)(DB_HEADER_TYPE).ToString, dt_codeInformation(0)(DB_HEADER_DESCRIPTION).ToString, dt_codeInformation(0)(DB_HEADER_FIX).ToString)
					Catch ex As Exception
						MsgBox(ex.Message)
					End Try
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


			'Populate the evaluation tab

			'Only set date information if we have values.
			If evaluationDate.Length <> 0 Then
				Dim useDate As Date = evaluationDate
				EvaluationDay_TextBox.Text = useDate.Day
				EvaluationMonth_TextBox.Text = useDate.Month
				EvaluationYear_TextBox.Text = useDate.Year
			End If

			Technician_TextBox.Text = technician
			SoftwareVer_TextBox.Text = softwareVersion
			IOVer_TextBox.Text = ioVersion
			BootVer_TextBox.Text = bootVersion

			'Get all of the repair items that are associated with this RMA.
			dt_repairItems = New DataTable
			dt_repairItems.Columns.Add(DB_HEADER_DESCRIPTION)
			dt_repairItems.Columns.Add(DB_HEADER_CHARGE)

			RepairItems_DataGridView.DataSource = dt_repairItems

			mycmd.CommandText = "SELECT [" & DB_HEADER_DESCRIPTION & "],[" & DB_HEADER_CHARGE & "] FROM " & TABLE_RMAREPAIRITEMS & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "'"

			dt_repairItems.Load(mycmd.ExecuteReader())

			RepairItems_DataGridView.DataSource = Nothing
			RepairItems_DataGridView.DataSource = dt_repairItems
			RepairItems_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

			RepairItems_DataGridView.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
			RepairItems_DataGridView.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
			RepairItems_DataGridView.Columns(0).Width = 150
			RepairItems_DataGridView.ClearSelection()

			'Get all of the codes that are associated with this RMA.
			dt_evalCodeItems.Rows.Clear()

			mycmd.CommandText = "SELECT * FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "' AND [" & DB_HEADER_EVALUATION & "] = '1'"
			dt_coderesults = New DataTable
			dt_coderesults.Load(mycmd.ExecuteReader)

			For Each row As DataRow In dt_coderesults.Rows
				Try
					mycmd.CommandText = "SELECT * FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_ID & "] = '" & row(DB_HEADER_RMACODESID).ToString & "'"
				Catch ex As Exception
					MsgBox(ex.Message)
				End Try

				Dim dt_codeInformation As New DataTable
				dt_codeInformation.Load(mycmd.ExecuteReader)

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

			Dim newFont As New Font("Consolas", 9.75)
			CodeItems_DataGridView.DefaultCellStyle.Font = newFont
			CodeItems_DataGridView.ColumnHeadersDefaultCellStyle.Font = newFont
			CodeItems_DataGridView.RowHeadersDefaultCellStyle.Font = newFont

			RepairItems_DataGridView.DefaultCellStyle.Font = newFont
			RepairItems_DataGridView.ColumnHeadersDefaultCellStyle.Font = newFont
			RepairItems_DataGridView.RowHeadersDefaultCellStyle.Font = newFont

			EvalCodeItems_DataGridView.DefaultCellStyle.Font = newFont
			EvalCodeItems_DataGridView.ColumnHeadersDefaultCellStyle.Font = newFont
			EvalCodeItems_DataGridView.RowHeadersDefaultCellStyle.Font = newFont

			TechnicianNotes_TextBox.Text = technicianNotes

			'Populate the ship / bill tab

			'Set our doNotUpdate flag so we do not have our drop downs altering things as we change it.
			doNotUpdate = True

			'Only set date information if we have values.
			If shipDate.Length <> 0 Then
				Dim useDate As Date = shipDate
				ShipDay_TextBox.Text = useDate.Day
				ShipMonth_TextBox.Text = useDate.Month
				ShipYear_TextBox.Text = useDate.Year
			End If

			'Only add if we have a GUID for our shipping.
			If shipGUID.Length <> 0 Then
				mycmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & shipGUID & "'"
				Dim dt_Ship = New DataTable()
				dt_Ship.Load(mycmd.ExecuteReader())

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

			RMAPONumber_TextBox.Text = rmaPO
			NetBoxInvoice_TextBox.Text = netboxInvoice

			'Get the Billing type information
			'Only add if we have a GUID.
			If billTypeGUID.Length <> 0 Then
				mycmd.CommandText = "SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_ID & "] = '" & billTypeGUID & "'"
				Dim dt_BillingType = New DataTable()
				dt_BillingType.Load(mycmd.ExecuteReader())

				'Set the dropdown menu to this box
				BillingType_ComboBox.SelectedIndex = BillingType_ComboBox.Items.IndexOf(dt_BillingType(0)(DB_HEADER_BILLTYPE))

				'Determine how to set up the billing side of the information depending on billing type booleans.
				If dt_BillingType(0)(DB_HEADER_NEEDSADDRESS) = False Then
					'We do not need a billing address so disable each of the textboxes and autofill.
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
					'We need an address so enable all each of the textboxes and autofill.
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
			'Only add if we have a GUID.
			If billGUID.Length <> 0 Then
				mycmd.CommandText = "SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & billGUID & "'"
				Dim dt_Bill = New DataTable()
				dt_Bill.Load(mycmd.ExecuteReader())

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
			doNotUpdate = False


			'Populate the system details tab

			'Check to see if we are a valid System serial number.
			Dim instanceString = ""

			Try
				instanceString = instanceString & " and [" & DB_HEADER_INSTANCE & "] = '" & thisCurrentTable.Rows(thisCurrentRow)(DB_HEADER_INSTANCE) & "'"
			Catch ex As Exception

			End Try
			mycmd.CommandText = "SELECT * FROM " & TABLE_SYSTEM & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'" & instanceString
			Dim dt_results As New DataTable()
			dt_results.Load(mycmd.ExecuteReader())

			If dt_results.Rows.Count < 1 Then
				' We are not a system.
				thissystemID = ""

				'Check to see if we are a valid Board serial number.
				mycmd.CommandText = "SELECT * FROM " & TABLE_BOARD & " WHERE [" & DB_HEADER_SERIALNUMBER & "] = '" & SerialNumber_TextBox.Text & "'"
				dt_results = New DataTable()
				dt_results.Load(mycmd.ExecuteReader())

				If dt_results.Rows.Count < 1 Then
					'If we still do not have a valid serial number then we cannot add it to the RMA database.
					MsgBox("Serial Number [" & SerialNumber_TextBox.Text & "] does not exist in the PRODUCTION database." & vbNewLine)
				Else
					'First check to see if we have the tab page added to the control yet or if it has been reoved.
					If TabControl1.TabPages.IndexOf(BoardDetails_TabPage) = -1 Then
						TabControl1.TabPages.Insert(TabControl1.TabPages.IndexOf(SystemDetails_TabPage), BoardDetails_TabPage)
					End If
					SetBoardInfo(thisSerialNumber)

					Dim myReader As SqlDataReader = Nothing
					Dim boardGUID As String = ""
					sqlapi.GetBoardGUIDBySerialNumber(mycmd, myReader, thisSerialNumber, boardGUID, "")

					'The next step is to find out if the board is part of a system or not.
					mycmd.CommandText = "SELECT systemid FROM dbo.System WHERE ([MotherBoard.boardid] = '" & boardGUID & "' OR [MainCPU.boardid] = '" & boardGUID &
						"' OR [Slot2.boardid] = '" & boardGUID & "' OR [Slot3.boardid] = '" & boardGUID & "' OR [Slot4.boardid] = '" & boardGUID &
						"' OR [Slot5.boardid] = '" & boardGUID & "' OR [Slot6.boardid] = '" & boardGUID & "' OR [Slot7.boardid] = '" & boardGUID &
						"' OR [Slot8.boardid] = '" & boardGUID & "' OR [Slot9.boardid] = '" & boardGUID & "' OR [Slot10.boardid] = '" & boardGUID & "')"

					dt_results = New DataTable()
					dt_results.Load(mycmd.ExecuteReader())

					If dt_results.Rows.Count <> 0 Then
						'First check to see if we have the tab page added to the control yet or if it has been reoved.
						If TabControl1.TabPages.IndexOf(SystemDetails_TabPage) = -1 Then
							TabControl1.TabPages.Insert(TabControl1.TabPages.Count, SystemDetails_TabPage)
						End If
                        SetSystemInfo(mycmd.ExecuteScalar.ToString)

                        If TabControl1.TabPages.IndexOf(Extras_TabPage) = -1 Then
							'TabControl1.TabPages.Insert(TabControl1.TabPages.Count, Extras_TabPage)
						End If
						'SetExtraInfo(mycmd.ExecuteScalar.ToString)
					Else
                        'The RMA is a board with no system attached so remove the system detail page
                        TabControl1.TabPages.Remove(SystemDetails_TabPage)
						'TabControl1.TabPages.Remove(Extras_TabPage)
					End If
				End If
			Else
				'The RMA is a system so remove the board detail page
				TabControl1.TabPages.Remove(BoardDetails_TabPage)

				'First check to see if we have the tab page added to the control yet or if it has been reoved.
				If TabControl1.TabPages.IndexOf(SystemDetails_TabPage) = -1 Then
					TabControl1.TabPages.Insert(TabControl1.TabPages.Count, SystemDetails_TabPage)
				End If

				thissystemID = dt_results.Rows(0)("systemid").ToString()

				SetSystemInfo(thissystemID)

				If TabControl1.TabPages.IndexOf(Extras_TabPage) = -1 Then
					'TabControl1.TabPages.Insert(TabControl1.TabPages.Count, Extras_TabPage)
				End If
				'SetExtraInfo(thissystemID)
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try

	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub Delete_Button_Click() Handles Delete_Button.Click
		'Delete the RMA record
		Dim mycmd = New SqlCommand("DELETE FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_ID & "] = '" & thisGUID & "'", myConn)
		mycmd.ExecuteNonQuery()

		'Delete the RMA Repair Items associated with the record
		mycmd.CommandText = "DELETE FROM " & TABLE_RMAREPAIRITEMS & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "'"
		mycmd.ExecuteNonQuery()

		'Delete the codes associated with the record
		mycmd.CommandText = "DELETE FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "'"
		mycmd.ExecuteNonQuery()

		changed = True
		Close()
	End Sub

	Private Sub Update_Button_Click() Handles Update_Button.Click
		Dim hasErrors As Boolean = False
		Dim errorMessage As String = ""
		Dim commandString As String = "UPDATE " & TABLE_RMA & " SET "

		'First, check that the Serial Number exists.
		Dim cmd As New SqlCommand("", myConn)

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

		'Second, check to see if we have changed the serial number.
		If thisSerialNumber <> SerialNumber_TextBox.Text Then
			Select Case MsgBox("You are changing the Serial Number associated with form " & thisSFN & " from " & thisSerialNumber & " to " & SerialNumber_TextBox.Text & "." & vbNewLine &
									"Are you sure you want to change record serial number?", MsgBoxStyle.YesNo, "Confirmation")
				Case DialogResult.No
					SerialNumber_TextBox.Text = thisSerialNumber
					Return
			End Select
			thisSerialNumber = SerialNumber_TextBox.Text
		End If

		'Date logic checking
		Dim lastUpdate As New Date(Date.Now.Year, Date.Now.Month, Date.Now.Day)
		commandString = commandString & "[" & DB_HEADER_LASTUPDATE & "] = '" & lastUpdate & "'"

		Dim infoDate As Date = CheckDate(InfoDay_TextBox, InfoMonth_TextBox, InfoYear_TextBox, errorMessage)
		If infoDate <> Nothing Then
			commandString = commandString & ", [" & DB_HEADER_INFORMATIONDATE & "] = '" & infoDate & "'"
		Else
			commandString = commandString & ", [" & DB_HEADER_INFORMATIONDATE & "] = NULL"
		End If

		Dim Receivedate As Date = CheckDate(Receiveday_TextBox, ReceiveMonth_TextBox, ReceiveYear_TextBox, errorMessage)
		If Receivedate <> Nothing Then
			commandString = commandString & ", [" & DB_HEADER_RECEIVEDDATE & "] = '" & Receivedate & "'"
		Else
			commandString = commandString & ", [" & DB_HEADER_RECEIVEDDATE & "] = NULL"
		End If

		Dim evaluationDate As Date = CheckDate(EvaluationDay_TextBox, EvaluationMonth_TextBox, EvaluationYear_TextBox, errorMessage)
		If evaluationDate <> Nothing Then
			commandString = commandString & ", [" & DB_HEADER_EVALUATIONDATE & "] = '" & evaluationDate & "'"
		Else
			commandString = commandString & ", [" & DB_HEADER_EVALUATIONDATE & "] = NULL"
		End If

		Dim shipDate As Date = CheckDate(ShipDay_TextBox, ShipMonth_TextBox, ShipYear_TextBox, errorMessage)
		If shipDate <> Nothing Then
			commandString = commandString & ", [" & DB_HEADER_SHIPDATE & "] = '" & shipDate & "'"
		Else
			commandString = commandString & ", [" & DB_HEADER_SHIPDATE & "] = NULL"
		End If

		'Check to see that we did not get any errors.
		If errorMessage.Length <> 0 Then
			MsgBox(errorMessage)
			Return
		End If

		'Get the GUID of the status that we are cahnging to.
		cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_STATUS & "] = '" & Status_ComboBox.Text & "'"
		Dim statusGUID As Guid = cmd.ExecuteScalar

		'Add our information to the database as a new entry.
		Dim customer As String = Customer_TextBox.Text
		Dim contactName As String = ContactName_TextBox.Text
		Dim contactNumber As String = ContactNumber_TextBox.Text
		Dim contactEmail As String = ContactEmail_TextBox.Text
		Dim rgaNumber As String = RGANumber_TextBox.Text
		Dim invoiceNumber As String = InvoiceNumber_TextBox.Text
		Dim description As String = Description_TextBox.Text
		Dim tested As Boolean = Tested_CheckBox.Checked

		If description.Contains("'"c) = True Then
			description = description.Replace("'", "''")
		End If

		commandString = commandString & ", [" & DB_HEADER_STATUSID & "] = '" & statusGUID.ToString & "', [" & DB_HEADER_CUSTOMER & "] = '" & customer & "', " &
					"[" & DB_HEADER_SERIAL_NUMBER & "] = '" & thisSerialNumber & "', [" & DB_HEADER_CONTACTNAME & "] = '" & contactName & "', [" & DB_HEADER_CONTACTPHONE & "] = '" & contactNumber & "', " &
					"[" & DB_HEADER_CONTACTEMAIL & "] = '" & contactEmail & "', [" & DB_HEADER_C_RGA & "] = '" & rgaNumber & "', [" & DB_HEADER_C_INVOICE & "] = '" & invoiceNumber & "', " &
					"[" & DB_HEADER_DESCRIPTION & "] = '" & description & "', [" & DB_HEADER_TESTED & "] = '" & tested & "'"

		Dim technician As String = Technician_TextBox.Text
		Dim softwareVer As String = SoftwareVer_TextBox.Text
		Dim IOVer As String = IOVer_TextBox.Text
		Dim BootVer As String = BootVer_TextBox.Text
		Dim Notes As String = TechnicianNotes_TextBox.Text

		If Notes.Contains("'"c) = True Then
			Notes = Notes.Replace("'", "''")
		End If

		commandString = commandString & ", [" & DB_HEADER_TECHNICIAN & "] = '" & technician & "', " &
				"[" & DB_HEADER_SOFTWAREVERSION & "] = '" & softwareVer & "', [" & DB_HEADER_IOVERSION & "] = '" & IOVer & "', " &
				"[" & DB_HEADER_BOOTVERSION & "] = '" & BootVer & "', [" & DB_HEADER_TECHNICIANNOTES & "] = '" & Notes & "'"

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
			shipGUID = "NULL"
		Else
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
			cmd.CommandText = "SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_BILLTYPE & "] = '" & billingType & "'"
			Dim dt_BillingType = New DataTable()
			dt_BillingType.Load(cmd.ExecuteReader())

			billingTypeGUID = "'" & dt_BillingType(0)(DB_HEADER_ID).ToString & "'"

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


		commandString = commandString & ", [" & DB_HEADER_SHIPID & "] = " & shipGUID & ", " &
			"[" & DB_HEADER_BILLTYPEID & "] = " & billingTypeGUID & ", [" & DB_HEADER_BILLID & "] = " & billGUID & ", " &
			"[" & DB_HEADER_C_RMAPO & "] = '" & rmaPO & "', [" & DB_HEADER_NB_INVOICE & "] = '" & netboxInvoice & "' WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & thisSFN & "'"

		'Update our record information.
		cmd.CommandText = commandString
		cmd.ExecuteNonQuery()

		'Delete all of the previous repair items associated with this form.
		cmd.CommandText = "DELETE FROM " & TABLE_RMAREPAIRITEMS & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "'"
		cmd.ExecuteNonQuery()

		'Next we need to remove all of the codelists that are associated with the record and add in the new list.
		cmd.CommandText = "DELETE FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "' AND [" & DB_HEADER_CUSTOMER & "] = '1'"
		cmd.ExecuteNonQuery()

		'Next we need to remove all of the codelists that are associated with the record and add in the new list.
		cmd.CommandText = "DELETE FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & thisGUID & "'  AND [" & DB_HEADER_EVALUATION & "] = '1'"
		cmd.ExecuteNonQuery()

		'Add each item that is found in our Datagridview
		For Each row As DataGridViewRow In RepairItems_DataGridView.Rows
			cmd.CommandText = "INSERT INTO " & TABLE_RMAREPAIRITEMS & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_DESCRIPTION & "],[" & DB_HEADER_CHARGE & "]) " &
				"VALUES('" & thisGUID & "','" & row.Cells(DB_HEADER_DESCRIPTION).Value.ToString & "','" & row.Cells(DB_HEADER_CHARGE).Value.ToString & "')"
			cmd.ExecuteNonQuery()
		Next

		'Add each item that is found in our code Datagridview
		For Each row As DataGridViewRow In CodeItems_DataGridView.Rows
			cmd.CommandText = "Select [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & row.Cells(DB_HEADER_CODE).Value.ToString & "'"
			Dim codeguid As Guid = cmd.ExecuteScalar

			cmd.CommandText = "INSERT INTO " & TABLE_RMACODELIST & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_RMACODESID & "],[" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_EVALUATION & "]) " &
					"VALUES('" & thisGUID & "','" & codeguid.ToString & "','1','0')"
			cmd.ExecuteNonQuery()
		Next

		'Add each item that is found in our code Datagridview
		For Each row As DataGridViewRow In EvalCodeItems_DataGridView.Rows
			cmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & row.Cells(DB_HEADER_CODE).Value.ToString & "'"
			Dim codeguid As Guid = cmd.ExecuteScalar

			cmd.CommandText = "INSERT INTO " & TABLE_RMACODELIST & "([" & DB_HEADER_RMAID & "],[" & DB_HEADER_RMACODESID & "],[" & DB_HEADER_CUSTOMER & "],[" & DB_HEADER_EVALUATION & "]) " &
					"VALUES('" & thisGUID & "','" & codeguid.ToString & "','0','1')"
			cmd.ExecuteNonQuery()
		Next

		LastDay_TextBox.Text = Date.Now.Day
		LastMonth_TextBox.Text = Date.Now.Month
		LastYear_TextBox.Text = Date.Now.Year

		Updated_Label.Visible = True
		Timer1.Interval = TIMEOUT
		Timer1.Enabled = True
		Timer1.Start()

		changed = True
	End Sub

	Private Function CheckDate(ByRef day As TextBox, ByRef month As TextBox, ByRef year As TextBox, ByRef errorMessage As String) As Date
		'Check to see that we have text inside our dates textbox.
		If day.Text.Length <> 0 And month.Text.Length <> 0 And year.Text.Length <> 0 Then
			Try
				'First attempt to make the text into Integers.
				Dim thisyear As Integer = CInt(year.Text)
				Dim thismonth As Integer = CInt(month.Text)
				Dim thisday As Integer = CInt(day.Text)

				'Next do some basic number checking logic for the dates entered.
				If 13 <= thismonth Then
					errorMessage = errorMessage & "Month is greater than 12" & vbNewLine
					Return Nothing
				End If

				If 32 <= thisday Then
					errorMessage = errorMessage & "Day is greater than 31" & vbNewLine
					Return Nothing
				End If

				'Lastly, try to assign the date into the variable.
				Return New Date(thisyear, thismonth, thisday)
			Catch ex As Exception
				'Add any errors that we run into.
				errorMessage = errorMessage & "Please use legitimate numbers for the date." & vbNewLine
			End Try
		End If

		Return Nothing
	End Function

	Private Sub EnableDelete_CheckBox_CheckedChanged() Handles EnableDelete_CheckBox.CheckedChanged
		'Enable/Disable the delete button.
		Delete_Button.Enabled = EnableDelete_CheckBox.Checked
	End Sub


#Region "Information Methods"
	Private Sub AddCode_Button_Click() Handles AddCode_Button.Click
		'Make sure that we have a code selected in our drop box.
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
#End Region

#Region "Evaluation Methods"
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

	Private Sub DeleteEvalCode_Button_Click() Handles DeleteEvalCode_Button.Click
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
			If (CurrentDatabase.Contains("Devel") = False) Then
				If SerialNumber_TextBox.Text <> obj.serial.ToString Then
					MsgBox("Expected: " & SerialNumber_TextBox.Text & " - Received: " & obj.serial.ToString)
					Return
				End If
			End If

			jsonapi.SendDiagnostic(IP_TextBox.Text, WResponse)

			Dim retval As Boolean

			retval = DownloadFiles("belleville")
			If retval = False Then
				DownloadFiles("aquametrix")
			End If
			If retval = False Then
				DownloadFiles("bluesky")
				MsgBox("Download logs not supported at this moment")
			End If
			'Chain any other passwords to try here.
			'If retval = False Then
			'	DownloadFiles("password")
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
#End Region

#Region "Shipping / Billing Methods"
    Private Sub BillingType_ComboBox_SelectedIndexChanged() Handles BillingType_ComboBox.SelectedIndexChanged
		If doNotUpdate = False Then
			'Get the Billing type information
			Dim name As String = BillingType_ComboBox.Text

			If name = "" Then

			Else
				'Get the billing type information.
				Dim cmd As New SqlCommand("SELECT * FROM " & TABLE_RMABILLTYPE & " WHERE [" & DB_HEADER_BILLTYPE & "] = '" & name & "'", myConn)
				Dim dt_BillingType = New DataTable()
				dt_BillingType.Load(cmd.ExecuteReader())

				'Depending on what is selected, enable or disable the billing address.
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

			'Get all of the information from the results of the address that we have chosen and populate their locations.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & shipguid & "'", myConn)

			dt_Ship.Load(myCmd.ExecuteReader)

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

			'Get all of the information from the results of the address that we have chosen and populate their locations.
			Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMAADDRESSES & " WHERE [" & DB_HEADER_ID & "] = '" & billguid & "'", myConn)

			dt_Bill.Load(myCmd.ExecuteReader)

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
#End Region

#Region "System Details Methods"
    Private Sub SetSystemInfo(ByRef systemID As String)
		'Reset all of our boxes
		MotherboardSerialNumber.Text = ""
		Slot1SerialNumber.Text = ""
		Slot2SerialNumber.Text = ""
		Slot3SerialNumber.Text = ""
		Slot4SerialNumber.Text = ""
		Slot5SerialNumber.Text = ""
		Slot6SerialNumber.Text = ""
		Slot7SerialNumber.Text = ""
		Slot8SerialNumber.Text = ""
		Slot9SerialNumber.Text = ""
		Slot10SerialNumber.Text = ""

		BarcodeDate.Text = ""
		RegisterDate.Text = ""
		ParameterDate.Text = ""
		BurnInDate.Text = ""
		CheckoutDate.Text = ""
		ShipDate.Text = ""
		LastUpdate.Text = ""

		SystemType.Text = ""
		CPU_Version.Text = ""
		PWRAtoD_Version.Text = ""
		MACAddress.Text = ""

		SystemRTB_Results.Clear()

		Dim result As String = ""
		Dim record As Guid = Nothing
		Dim barcodeDateTime As DateTime = Nothing
		Dim registerDateTime As DateTime = Nothing
		Dim parameterDateTime As DateTime = Nothing
		Dim burnInDateTime As DateTime = Nothing
		Dim checkoutDateTime As DateTime = Nothing
		Dim shipDateTime As DateTime = Nothing
		Dim lastUpdateTime As DateTime = Nothing

		Dim mycmd As New SqlCommand("", myConn)
		Dim myreader As SqlDataReader = Nothing

		Dim hasCPU As Boolean = False
		Dim hasPWR As Boolean = False

		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, MOTHERBOARD, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, MotherboardSerialNumber.Text, result)
				If MotherboardSerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf MotherboardSerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, MAIN_CPU, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot1SerialNumber.Text, result)
				If Slot1SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot1SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_2, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot2SerialNumber.Text, result)
				If Slot2SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot2SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_3, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot3SerialNumber.Text, result)
				If Slot3SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot3SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_4, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot4SerialNumber.Text, result)
				If Slot4SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot4SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_5, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot5SerialNumber.Text, result)
				If Slot5SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot5SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_6, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot6SerialNumber.Text, result)
				If Slot6SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot6SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_7, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot7SerialNumber.Text, result)
				If Slot7SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot7SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_8, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot8SerialNumber.Text, result)
				If Slot8SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot8SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_9, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot9SerialNumber.Text, result)
				If Slot9SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot9SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If
		If sqlapi.GetBoardGUIDBySystemID(mycmd, myreader, systemID, SLOT_10, record, result) = True Then
			If record <> Guid.Empty Then
				sqlapi.GetBoardSerialNumberByGUID(mycmd, myreader, record.ToString, Slot10SerialNumber.Text, result)
				If Slot10SerialNumber.Text.Contains("CPU") = True Then
					hasCPU = True
				ElseIf Slot10SerialNumber.Text.Contains("PWR") = True Then
					hasPWR = True
				End If
			End If
		End If

		If hasCPU = True Then
			sqlapi.GetMACAddress(mycmd, myreader, Slot1SerialNumber.Text, MACAddress.Text, result)

			sqlapi.GetSystemVersionByID(mycmd, myreader, systemID, record, CPU_Version.Text, result)
		End If

		If hasPWR = True Then
			sqlapi.GetBoardSoftwareVersion(mycmd, myreader, Slot2SerialNumber.Text, PWRAtoD_Version.Text, result)
		End If


		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, BARCODE_DATE, barcodeDateTime, record, result)
		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, REGISTER_DATE, registerDateTime, record, result)
		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, PARAMETER_DATE, parameterDateTime, record, result)
		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, BURN_IN_DATE, burnInDateTime, record, result)
		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, CHECKOUT_DATE, checkoutDateTime, record, result)
		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, SHIP_DATE, shipDateTime, record, result)
		sqlapi.GetSystemDateByID(mycmd, myreader, systemID, LAST_UPDATE, lastUpdateTime, record, result)

		If barcodeDateTime <> Nothing Then
			BarcodeDate.Text = barcodeDateTime.ToString()
		End If
		If registerDateTime <> Nothing Then
			RegisterDate.Text = registerDateTime.ToString()
		End If
		If parameterDateTime <> Nothing Then
			ParameterDate.Text = parameterDateTime.ToString()
		End If
		If burnInDateTime <> Nothing Then
			BurnInDate.Text = burnInDateTime.ToString()
		End If
		If checkoutDateTime <> Nothing Then
			CheckoutDate.Text = checkoutDateTime.ToString()
		End If
		If shipDateTime <> Nothing Then
			ShipDate.Text = shipDateTime.ToString()
		End If
		If lastUpdateTime <> Nothing Then
			LastUpdate.Text = lastUpdateTime.ToString()
		End If


		'Get the system Audits.
		sqlapi.GetSystemAuditByID(mycmd, myreader, systemID, SystemRTB_Results, result)

		'Get the system type.
		sqlapi.GetSystemCurrentTypeByID(mycmd, myreader, systemID, SystemType.Text, result)

		Dim newFont = New Font(MotherBoardLabel.Font.Name, MotherBoardLabel.Font.Size, FontStyle.Underline Or FontStyle.Bold)

		'If we have a board, make the label special so we can click on it to see more information.
		If MotherboardSerialNumber.Text.Length <> 0 Then
			MotherBoardLabel.Font = newFont
		End If
		If Slot1SerialNumber.Text.Length <> 0 Then
			Slot1Label.Font = newFont
		End If
		If Slot2SerialNumber.Text.Length <> 0 Then
			Slot2Label.Font = newFont
		End If
		If Slot3SerialNumber.Text.Length <> 0 Then
			Slot3Label.Font = newFont
		End If
		If Slot4SerialNumber.Text.Length <> 0 Then
			Slot4Label.Font = newFont
		End If
		If Slot5SerialNumber.Text.Length <> 0 Then
			Slot5Label.Font = newFont
		End If
		If Slot6SerialNumber.Text.Length <> 0 Then
			Slot6Label.Font = newFont
		End If
		If Slot7SerialNumber.Text.Length <> 0 Then
			Slot7Label.Font = newFont
		End If
		If Slot8SerialNumber.Text.Length <> 0 Then
			Slot8Label.Font = newFont
		End If
		If Slot9SerialNumber.Text.Length <> 0 Then
			Slot9Label.Font = newFont
		End If
		If Slot10SerialNumber.Text.Length <> 0 Then
			Slot10Label.Font = newFont
		End If

		Model_Label.Text = SystemType.Text
	End Sub

	Private Sub MotherBoardLabel_Click() Handles MotherBoardLabel.Click
		If MotherboardSerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(MotherboardSerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot1Label_Click() Handles Slot1Label.Click
		If Slot1SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot1SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot2Label_Click() Handles Slot2Label.Click
		If Slot2SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot2SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot3Label_Click() Handles Slot3Label.Click
		If Slot3SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot3SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot4Label_Click() Handles Slot4Label.Click
		If Slot4SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot4SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot5Label_Click() Handles Slot5Label.Click
		If Slot5SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot5SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot6Label_Click() Handles Slot6Label.Click
		If Slot6SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot6SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot7Label_Click() Handles Slot7Label.Click
		If Slot7SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot7SerialNumber.Text)
			DoViewBoardInfo.Show()
		End If
	End Sub

	Private Sub Slot8Label_Click() Handles Slot8Label.Click
		If Slot8SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot8SerialNumber.Text)
			DoViewBoardInfo.ShowDialog()
		End If
	End Sub

	Private Sub Slot9Label_Click() Handles Slot9Label.Click
		If Slot9SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot9SerialNumber.Text)
			DoViewBoardInfo.ShowDialog()
		End If
	End Sub

	Private Sub Slot10Label_Click() Handles Slot10Label.Click
		If Slot10SerialNumber.Text.Length <> 0 Then
			Dim DoViewBoardInfo As New ViewBoardInfo(Slot10SerialNumber.Text)
			DoViewBoardInfo.ShowDialog()
		End If
	End Sub

	Private Sub SetBoardInfo(ByRef boardSerialNo As String)
		Dim result As String = ""
		Dim record As Guid = Nothing
		Dim mycmd As New SqlCommand("", myConn)
		Dim myreader As SqlDataReader = Nothing

		BootloaderVersion.Text = ""
		BoardVersion.Text = ""
		LastUpdate.Text = ""
		BoardStatus.Text = ""
		BoardStatus.Text = ""

		BoardRTB_Results.Clear()

		BoardType.Text = ""
		HardwareVersion.Text = ""
		MACAddress.Text = ""
		CPUID.Text = ""

		'Get all of the board's information.
		sqlapi.GetBoardBootloaderVersion(mycmd, myreader, boardSerialNo, BootloaderVersion.Text, result)

		sqlapi.GetBoardSoftwareVersion(mycmd, myreader, boardSerialNo, BoardVersion.Text, result)

		sqlapi.GetBoardLastUpdate(mycmd, myreader, boardSerialNo, LastUpdate.Text, record, result)

		sqlapi.GetBoardCurrentStatus(mycmd, myreader, boardSerialNo, BoardStatus.Text, result)

		sqlapi.GetBoardAudit(mycmd, myreader, boardSerialNo, BoardRTB_Results, result)

		sqlapi.GetBoardCurrentType(mycmd, myreader, boardSerialNo, record, BoardType.Text, result)

		sqlapi.GetBoardHardwareVersion(mycmd, myreader, boardSerialNo, HardwareVersion.Text, result)

		sqlapi.GetMACAddress(mycmd, myreader, boardSerialNo, MACAddress.Text, result)

		sqlapi.GetCPUID(mycmd, myreader, boardSerialNo, CPUID.Text, result)
	End Sub

	Private Sub SetExtraInfo(ByRef systemidString As String)
		Dim mycmd As New SqlCommand("", myConn)
		Dim myreader As SqlDataReader = Nothing

		Dim result As String = ""
		Try
			mycmd.CommandText = "SELECT ec.[Description], ec.[Qty], ec.[DateOfService] FROM SystemExtrasMap map JOIN ExtraComponents ec ON ec.id = map.[ExtraComponents.id]
WHERE map.[SystemExtras.id] = (SELECT [SystemExtras.id] FROM dbo.System WHERE systemid = '" & systemidString & "') ORDER by ec.[Description]"
			Dim da_extras = New SqlDataAdapter(mycmd)
			Dim ds_extras = New DataSet()

			da_extras.Fill(ds_extras, "TABLE")

			If ds_extras.Tables(0).Rows.Count <> 0 Then
				mycmd.CommandText = "SELECT [Description] FROM SystemExtras WHERE [id] = (SELECT [SystemExtras.id] FROM dbo.System WHERE systemid = '" & systemidString & "')"

				L_Extra.Text = "Extras: " & mycmd.ExecuteScalar.ToString

				DGV_Extras.DataSource = ds_extras.Tables(0)
				DGV_Extras.Focus()

				DGV_Extras.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				DGV_Extras.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
				DGV_Extras.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
			Else
				TabControl1.TabPages.Remove(Extras_TabPage)
			End If
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub
#End Region

#Region "Populate Drop Downs"
	Private Sub PopulateCodes(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of our codes.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " ORDER BY [" & DB_HEADER_CODE & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_CODE))
		Next
	End Sub

	Private Sub PopulateStatus(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of our status's
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMASTATUS & " ORDER BY [" & DB_HEADER_ORDER & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_STATUS))
		Next
	End Sub

	Private Sub PopulateBillTypes(ByRef box As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of our bill types
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMABILLTYPE, myConn)

		resultTable.Load(myCmd.ExecuteReader)

		box.Items.Add("")
		For Each dr As DataRow In resultTable.Rows
			box.Items.Add(dr(DB_HEADER_BILLTYPE).ToString)
		Next
	End Sub
#End Region

#Region "Navigation Buttons"
	Private Sub Up_Button_Click() Handles Up_Button.Click
		Updated_Label.Visible = False
		Timer1.Stop()
		Timer1.Enabled = False
		thisCurrentRow = thisCurrentRow - 1
		UpdateInformation()
	End Sub

	Private Sub Down_Button_Click() Handles Down_Button.Click
		Updated_Label.Visible = False
		Timer1.Stop()
		Timer1.Enabled = False
		thisCurrentRow = thisCurrentRow + 1
		UpdateInformation()
	End Sub
#End Region

	Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode.Equals(Keys.Delete) Then
			'If we press the Delete key, call our remove function depending on which table has focus.
			If RepairItems_DataGridView.Focused = True Then
				DeleteItem_Button_Click()
			ElseIf EvalCodeItems_DataGridView.Focused = True Then
				DeleteEvalCode_Button_Click()
			ElseIf CodeItems_DataGridView.Focused = True Then
				DeleteCode_Button_Click()
			End If

		ElseIf e.KeyCode.Equals(Keys.Enter) Then
			'If we press the Enter key, call our Add function only if we are focused on the right textboxes.
			If RepairItem_TextBox.Focused = True Or Cost_TextBox.Focused = True Then
				Call AddItem_Button_Click()
			End If
		End If
	End Sub

	Private Sub Timer1_Tick() Handles Timer1.Tick
		Updated_Label.Visible = False
		Timer1.Stop()
		Timer1.Enabled = False
	End Sub

	Private Sub RecordDetails_Closing() Handles Me.Closing
		Timer1.Enabled = False
		Timer1.Stop()
	End Sub

	Private Sub Directory_Button_Click() Handles Directory_Button.Click
		'Check to see if the folder path is valid or not.
		If IO.Directory.Exists(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text) = False Then
			MsgBox(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text & " Service Folder was not found. Please update your settings to the correct " &
				   "location of the folder and make sure that it exists there.")
			Return
		End If

		'Open the folder location in another window.
		Process.Start(My.Settings.ServiceFoldersDir & "\" & ServiceForm_TextBox.Text)
	End Sub

	Private Sub RecordDetails_Activated() Handles Me.Activated
		'Reload our codes in case the user decided to add new ones.
		doNotUpdate = True

		Code_ComboBox.Items.Clear()
		EvalCode_ComboBox.Items.Clear()
		BillingType_ComboBox.Items.Clear()

		PopulateCodes(Code_ComboBox)
		PopulateCodes(EvalCode_ComboBox)
		PopulateBillTypes(BillingType_ComboBox)

		doNotUpdate = False
	End Sub

	Private Sub Comment_Button_Click() Handles Comment_Button.Click
		Dim DoAddBoardComment As New AddBoardComment(thissystemID)
		DoAddBoardComment.ShowDialog()

		If DoAddBoardComment.response.Length <> 0 Then
			TechnicianNotes_TextBox.AppendText(vbNewLine & DoAddBoardComment.response)

			' clear the audit box so we can refresh it with all of the correct information
			SystemRTB_Results.Clear()
			SetSystemInfo(thissystemID)

			Dim sfn As Integer = ServiceForm_TextBox.Text
			Dim Notes As String = TechnicianNotes_TextBox.Text

			'Replace any single ['] quotes with double single [''] quotes for the SQL syntax.
			If Notes.Contains("'"c) = True Then
				Notes = Notes.Replace("'", "''")
			End If

			Try
				'Updating our record in the database.
				Dim mycmd As New SqlCommand("UPDATE " & TABLE_RMA & " SET [" & DB_HEADER_TECHNICIANNOTES & "] = '" & Notes & "' WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & sfn & "'", myConn)
				mycmd.ExecuteNonQuery()

			Catch ex As Exception
				MsgBox(ex.Message)
				Return
			End Try
		End If
	End Sub
End Class