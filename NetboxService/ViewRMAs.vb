'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ViewRMAs.vb
'
' Description: Displays all of the RMAs in our database system. Here we have the ability to search through each column and even use a drop
'	down list of codes. Because of this the SQL command text is super long and conplex. In simple short terms, we create a new table
'	called TempTable and fill it with all of the information that we want. Two important columns are ServiceFormDup and ServiceFormCodeDup. 
'	These two control that no duplicates Of forms are displayed. An example would be if the customer and technician filed the same code for 
'	a broken screen. Instead Of having the same entry pop up twice and allow for some confusing edits to happen or false numbers, these two 
'	columns display a unique identifier To seperate them. We then look for rows that contain only 1 [or more than one for a double code 
'	search] and add that to the display table. After this SQL table has been formed, we then select the final columns that we want from the 
'	TempTable based On our search And the duplicates. The inside where clause is where all of our text searchs go to build up the temp 
'	table And the outside where clause is where our duplication limitation goes for our code search.
'
' Create Excel: Create a Spread sheet with the currently displayed results from the DataGridView.
' View Details: Veiw more details of the selected RMA.
' Double Click: Same action as View Details.
' Header DropDown: Allows us to search within the selected dropdown column name. If "Code" is selected then the associated Textbox will be
'	be disabled and no visable. The Dropdown for the code we are looking for will then be enabled and visable. When "Code" is not selected
'	the reverse happens.
'
' Special Keys:
'	enter = executes the search if either of the search text boxes are selected.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class ViewRMAs

#Region "RMA Items Variables"
	Dim da As New SqlDataAdapter
	Dim ds As New DataSet
	Dim RMA_myCmd = New SqlCommand("", myConn)

	Dim scrollValue As Integer

	Dim isSearched As Boolean = False
	Dim isSorted As Boolean = False

	'This is the SQL Command for the TempTalbe. It grabs all of the columns that we need along with two special columns for detecting duplicate records based on our search.
	Dim beginningTempTableCommand As String = "WITH TempTable AS " &
								"(SELECT" &
								"  rma.[" & DB_HEADER_SERVICEFORM & "]" &
								", rma.[" & DB_HEADER_SERIAL_NUMBER & "]" &
								", rma.[" & DB_HEADER_INSTANCE & "]" &
								", sType.[name] AS 'System Type'" &
								", status.[" & DB_HEADER_STATUS & "]" &
								", rma.[" & DB_HEADER_CUSTOMER & "]" &
								", rma.[" & DB_HEADER_C_RGA & "]" &
								", rma.[" & DB_HEADER_C_INVOICE & "]" &
								", rma.[" & DB_HEADER_TECHNICIAN & "]" &
								", rma.[" & DB_HEADER_SOFTWAREVERSION & "]" &
								", bill.[" & DB_HEADER_BILLTYPE & "]" &
								", rma.[" & DB_HEADER_C_RMAPO & "]" &
								", rma.[" & DB_HEADER_NB_INVOICE & "]" &
								", rma.[" & DB_HEADER_INFORMATIONDATE & "]" &
								", rma.[" & DB_HEADER_RECEIVEDDATE & "]" &
								", rma.[" & DB_HEADER_EVALUATIONDATE & "]" &
								", rma.[" & DB_HEADER_APPROVALDATE & "]" &
								", rma.[" & DB_HEADER_SHIPDATE & "]" &
								", rma.[" & DB_HEADER_LASTUPDATE & "]" &
								", codes.[" & DB_HEADER_CODE & "]" &
								", ServiceFormDup = ROW_NUMBER()OVER(PARTITION BY rma.[" & DB_HEADER_SERVICEFORM & "] ORDER BY rma.[" & DB_HEADER_SERVICEFORM & "] ASC)" &
								" FROM " & TABLE_RMA & " [rma]" &
								" LEFT JOIN System [sno] ON rma.[Serial Number] = sno.SerialNumber AND rma.[Instance] = sno.Instance" &
								" LEFT JOIN SystemType [sType] ON stype.id = [dbo.SystemType.id]" &
								" LEFT JOIN " & TABLE_RMASTATUS & " [status] ON rma.[" & DB_HEADER_STATUSID & "] = status.[" & DB_HEADER_ID & "]" &
								" LEFT JOIN " & TABLE_RMABILLTYPE & " [bill] ON rma.[" & DB_HEADER_BILLTYPEID & "] = bill.[" & DB_HEADER_ID & "]" &
								" LEFT JOIN " & TABLE_RMACODELIST & " [codelist] ON rma.[" & DB_HEADER_ID & "] = codelist.[" & DB_HEADER_RMAID & "] And codelist.[" & DB_HEADER_CUSTOMER & "] = 0" &
								" LEFT JOIN " & TABLE_RMACODES & " [codes] ON codelist.[" & DB_HEADER_RMACODESID & "] = codes.[" & DB_HEADER_ID & "] " '&
	'" LEFT JOIN SystemExtras [systemEx] ON sno.[SystemExtras.id] = systemEx.[id]"

	'", systemEx.[Description] AS 'Extras'" &
	'", rma.[" & DB_HEADER_CONTACTNAME & "]" &
	'", rma.[" & DB_HEADER_CONTACTPHONE & "]" &
	'", rma.[" & DB_HEADER_CONTACTEMAIL & "]" &
	'", rma.[" & DB_HEADER_IOVERSION & "]" &
	'", rma.[" & DB_HEADER_BOOTVERSION & "]" &

	'This is where the optional WHERE clause goes for generic searches
	Dim insideWhereClause As String = ""

	'This is the SQL command for grabbing the final columns from the TempTable so the user only sees the information they need.
	Dim tableCommand As String = "SELECT " &
								"  [" & DB_HEADER_SERVICEFORM & "]" &
								", [" & DB_HEADER_SERIAL_NUMBER & "]" &
								", [" & DB_HEADER_INSTANCE & "]" &
								", [" & HEADER_SYSTEMTYPE & "]" &
								", [" & DB_HEADER_STATUS & "]" &
								", [" & DB_HEADER_CUSTOMER & "]" &
								", [" & DB_HEADER_C_RGA & "]" &
								", [" & DB_HEADER_C_INVOICE & "]" &
								", [" & DB_HEADER_TECHNICIAN & "]" &
								", [" & DB_HEADER_SOFTWAREVERSION & "]" &
								", [" & DB_HEADER_BILLTYPE & "]" &
								", [" & DB_HEADER_C_RMAPO & "]" &
								", [" & DB_HEADER_NB_INVOICE & "]" &
								", [" & DB_HEADER_INFORMATIONDATE & "]" &
								", [" & DB_HEADER_RECEIVEDDATE & "]" &
								", [" & DB_HEADER_EVALUATIONDATE & "]" &
								", [" & DB_HEADER_APPROVALDATE & "]" &
								", [" & DB_HEADER_SHIPDATE & "]" &
								", [" & DB_HEADER_LASTUPDATE & "]"

	'", [Extras]" &
	'", [" & DB_HEADER_CONTACTNAME & "]" &
	'", [" & DB_HEADER_CONTACTPHONE & "]" &
	'", [" & DB_HEADER_CONTACTEMAIL & "]" &
	'", [" & DB_HEADER_IOVERSION & "]" &
	'", [" & DB_HEADER_BOOTVERSION & "]" &

	'This is the close part of the SQL command. It allows us to keep the tableCommand seperate incase we need to add more or use only it for SQL transactions.
	Dim endingCommand As String = ") " & tableCommand & " FROM TempTable "

	Dim entriesToShow As Integer = 250
	Dim numberOfRecords As Integer
	Dim sort As String = ""
	Dim Freeze As Integer = 1

	'Protection variable to prevent our combo box change event.
	Dim doNotUpdate As Boolean = True
#End Region

	Dim myCmd As New SqlCommand("", myConn)

	Private Sub ViewRMAs_Load() Handles MyBase.Load

		Dim result As String = ""

		Try
			'Get all of the RMA records and display them in our table.
			RMA_myCmd.CommandText = beginningTempTableCommand & endingCommand & "WHERE ServiceFormDup = 1 ORDER BY [" & DB_HEADER_SERVICEFORM & "]"
			da = New SqlDataAdapter(RMA_myCmd)
			ds = New DataSet()

			sqlapi.RetriveData(Freeze, da, ds, DGV_Items, scrollValue, entriesToShow, numberOfRecords, B_Next, B_Last, B_First, B_Previous)

			L_Results.Text = "Number of results: " & numberOfRecords

			'Get Drop Down Items.
			GetColumnDropDownItems(CB_Sort, ds)
			GetColumnDropDownItems(CB_Search, ds)
			GetColumnDropDownItems(CB_Search2, ds)
			GetCodeDropDownItems(Code_ComboBox)
			GetCodeDropDownItems(Code2_ComboBox)

			CB_Operand1.SelectedIndex = 0
			CB_Operand2.SelectedIndex = 0
			CB_Term_Operand.SelectedIndex = 0

			KeyPreview = True
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
		doNotUpdate = False
	End Sub

	Private Sub B_CreateExcel_Click() Handles B_CreateExcel.Click
		Dim report As New GenerateReport()

		report.Generate_itemslistReport(ds)
	End Sub

	Private Sub B_Close_Click() Handles B_Close.Click
		Close()
	End Sub

	Private Sub GetColumnDropDownItems(ByRef cb As ComboBox, ByRef ds As DataSet)
		For Each dc As DataColumn In ds.Tables(0).Columns
			cb.Items.Add(dc.ColumnName)
		Next

		If cb.Items.Count <> 0 Then
			cb.SelectedIndex = 0
		End If

		cb.DropDownHeight = 200
	End Sub

	Private Sub GetCodeDropDownItems(ByRef cb As ComboBox)
		Dim resultTable As New DataTable()

		'Get all of our codes from the database.
		Dim myCmd As New SqlCommand("SELECT * FROM " & TABLE_RMACODES & " ORDER BY [" & DB_HEADER_CODE & "] ASC", myConn)

		resultTable.Load(myCmd.ExecuteReader)

		For Each dr As DataRow In resultTable.Rows
			cb.Items.Add(dr(DB_HEADER_CODE))
		Next

		If cb.Items.Count <> 0 Then
			cb.SelectedIndex = 0
		End If

		cb.DropDownHeight = 200
	End Sub

#Region "RMA Items"
	Private Sub B_First_Click() Handles B_First.Click
		sqlapi.FirstPage(scrollValue, ds, da, entriesToShow)
		B_First.Enabled = False
		B_Previous.Enabled = False
		B_Next.Enabled = True
		B_Last.Enabled = True
	End Sub

	Private Sub B_Previous_Click() Handles B_Previous.Click
		sqlapi.PreviousPage(scrollValue, entriesToShow, ds, da, B_First, B_Previous)
		B_Next.Enabled = True
		B_Last.Enabled = True
	End Sub

	Private Sub B_Next_Click() Handles B_Next.Click
		sqlapi.NextPage(scrollValue, entriesToShow, numberOfRecords, ds, da, B_Next, B_Last)
		B_First.Enabled = True
		B_Previous.Enabled = True
	End Sub

	Private Sub B_Last_Click() Handles B_Last.Click
		sqlapi.LastPage(scrollValue, entriesToShow, numberOfRecords, ds, da)
		B_First.Enabled = True
		B_Previous.Enabled = True
		B_Next.Enabled = False
		B_Last.Enabled = False
	End Sub

	Private Sub B_ListAll_Click() Handles B_ListAll.Click
		isSearched = False
		isSorted = False

		RefreshList()
	End Sub

	Private Sub RefreshList()
		Cursor = Cursors.WaitCursor
		Try
			'Reset our inside where clause.
			insideWhereClause = ""

			'Get all of our data and populate our table.
			RMA_myCmd.CommandText = beginningTempTableCommand & endingCommand & "WHERE ServiceFormDup = 1 ORDER BY [" & DB_HEADER_SERVICEFORM & "]"
			da = New SqlDataAdapter(RMA_myCmd)
			ds = New DataSet()

			sqlapi.RetriveData(Freeze, da, ds, DGV_Items, scrollValue, entriesToShow, numberOfRecords, B_Next, B_Last, B_First, B_Previous)

			L_Results.Text = "Number of results: " & numberOfRecords
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
		Cursor = Cursors.Default
	End Sub

	Private Sub B_Search_Click() Handles B_Search.Click
		isSearched = True
		isSorted = False

		'Check to see if we have the Textbox visable and had text.
		If TB_Search.Text.Length = 0 And TB_Search.Visible = True Then
			MsgBox("Please put in a search inside the first box.")
			Return
		End If

		Cursor = Cursors.WaitCursor

		'Start by setting our endingWhere equal to the one that is mostly used.
		Dim endingWhere As String = "WHERE ServiceFormDup = 1"

		'First we need to build up our WHERE Clause.
		insideWhereClause = ""

		' set our conjunction for a double term search
		Dim conjunction As String = CB_Term_Operand.Text

		'Check to see if we are doing a code search or not.
		If Code_ComboBox.Visible = True Then
			'We need to get the guid of the code that is selected.
			myCmd.CommandText = "Select [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & Code_ComboBox.Text & "'"
			Dim codeguid As Guid = myCmd.ExecuteScalar

			insideWhereClause = " WHERE (codelist.[" & DB_HEADER_RMACODESID & "] = '" & codeguid.ToString & "'"

		Else
			Select Case CB_Operand1.Text
				Case "LIKE"
					If CB_Search.Text = "Customer" Or CB_Search.Text = "Serial Number" Or CB_Search.Text = "Instance" Then
						insideWhereClause = " WHERE (rma.[" & CB_Search.Text & "] " & CB_Operand1.Text & " '%" & TB_Search.Text & "%'"
					ElseIf CB_Search.Text = "Extras" Then
						insideWhereClause = " WHERE (systemEx.[Description] " & CB_Operand1.Text & " '%" & TB_Search.Text & "%'"
					Else
						insideWhereClause = " WHERE ([" & CB_Search.Text & "] " & CB_Operand1.Text & " '%" & TB_Search.Text & "%'"
					End If

				Case Else
					If CB_Search.Text = "Customer" Or CB_Search.Text = "Serial Number" Or CB_Search.Text = "Instance" Then
						insideWhereClause = " WHERE (rma.[" & CB_Search.Text & "] " & CB_Operand1.Text & " '" & TB_Search.Text & "'"
					ElseIf CB_Search.Text = "Extras" Then
						insideWhereClause = " WHERE (systemEx.[Description] " & CB_Operand1.Text & " '" & TB_Search.Text & "'"
					Else
						insideWhereClause = " WHERE ([" & CB_Search.Text & "] " & CB_Operand1.Text & " '" & TB_Search.Text & "'"
					End If
			End Select
		End If

		'Check to see if we are doing a code search or not.
		If Code2_ComboBox.Visible = True Then
			If Code2_ComboBox.Text = Code_ComboBox.Text Then
				MsgBox("Please select two different codes to search for.")
				Cursor = Cursors.Default
				Return
			End If

			If Code_ComboBox.Visible = True And conjunction.Equals("AND") Then
				conjunction = "OR"
				endingWhere = "WHERE ServiceFormDup = 2"
			End If

			'We need to get the guid of the code that is selected.
			myCmd.CommandText = "Select [" & DB_HEADER_ID & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_CODE & "] = '" & Code2_ComboBox.Text & "'"
			Dim codeguid As Guid = myCmd.ExecuteScalar

			insideWhereClause = insideWhereClause & " " & conjunction & " codelist.[" & DB_HEADER_RMACODESID & "] = '" & codeguid.ToString & "'"
		Else
			If TB_Search2.Text.Length <> 0 Then
				Select Case CB_Operand2.Text
					Case "LIKE"
						If CB_Search2.Text = "Customer" Or CB_Search2.Text = "Serial Number" Or CB_Search2.Text = "Instance" Then
							insideWhereClause = insideWhereClause & " " & conjunction & " rma.[" & CB_Search2.Text & "] " & CB_Operand2.Text & " '%" & TB_Search2.Text & "%'"
						ElseIf CB_Search2.Text = "Extras" Then
							insideWhereClause = insideWhereClause & " " & conjunction & " systemEx.[Description] " & CB_Operand2.Text & " '%" & TB_Search2.Text & "%'"
						Else
							insideWhereClause = insideWhereClause & " " & conjunction & " [" & CB_Search2.Text & "] " & CB_Operand2.Text & " '%" & TB_Search2.Text & "%'"
						End If

					Case Else
						If CB_Search2.Text = "Customer" Or CB_Search2.Text = "Serial Number" Or CB_Search2.Text = "Instance" Then
							insideWhereClause = insideWhereClause & " " & conjunction & " rma.[" & CB_Search2.Text & "] " & CB_Operand2.Text & " '" & TB_Search2.Text & "'"
						ElseIf CB_Search2.Text = "Extras" Then
							insideWhereClause = insideWhereClause & " " & conjunction & " systemEx.[Description] " & CB_Operand2.Text & " '" & TB_Search2.Text & "'"
						Else
							insideWhereClause = insideWhereClause & " " & conjunction & " [" & CB_Search2.Text & "] " & CB_Operand2.Text & " '" & TB_Search2.Text & "'"
						End If
				End Select
			End If
		End If

		insideWhereClause = insideWhereClause & ")"

		'Second we need to build up our sort clause.
		Dim orderByClause As String = " ORDER BY [" & CB_Sort.Text & "]"
		If RB_AscendingSort.Checked Then
			orderByClause = orderByClause & " ASC"
		Else
			orderByClause = orderByClause & " DESC"
		End If

		'Third we need to combine our strings to make our search and count commands
		RMA_myCmd.CommandText = beginningTempTableCommand & insideWhereClause & endingCommand & endingWhere & orderByClause

		'Lastly, we need to execute the commands and populate our datatables.
		da = New SqlDataAdapter(RMA_myCmd)
		ds = New DataSet()

		sqlapi.RetriveData(Freeze, da, ds, DGV_Items, scrollValue, entriesToShow, numberOfRecords, B_Next, B_Last, B_First, B_Previous)

		L_Results.Text = "Number of results: " & numberOfRecords
		Cursor = Cursors.Default
	End Sub

	Private Sub B_Sort_Click() Handles B_Sort.Click
		isSorted = True

		Cursor = Cursors.WaitCursor

		Dim orderByClause As String = "[" & CB_Sort.Text & "]"
		If RB_AscendingSort.Checked Then
			orderByClause = orderByClause & " ASC"
		Else
			orderByClause = orderByClause & " DESC"
		End If

		Dim tempTable As New DataTable()
		tempTable = ds.Tables(0)

		tempTable.DefaultView.Sort = orderByClause
		tempTable = tempTable.DefaultView.ToTable

		scrollValue = 0
		ds = New DataSet()
		ds.Tables.Add(tempTable)
		'da.Fill(ds, scrollValue, entriesToShow, "TABLE")


		DGV_Items.DataSource = Nothing
		DGV_Items.DataSource = ds.Tables(0)
		DGV_Items.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

		DGV_Items.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		DGV_Items.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		DGV_Items.Columns(3).Width = 300

		For i = 0 To DGV_Items.Columns.Count - 1
			DGV_Items.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
		Next i

		DGV_Items.Columns(Freeze).Frozen = True

		'Dim orderByClause As String = " ORDER BY [" & CB_Sort.Text & "]"
		'If RB_AscendingSort.Checked Then
		'	orderByClause = orderByClause & " ASC"
		'Else
		'	orderByClause = orderByClause & " DESC"
		'End If

		'Dim sendCommand As String = beginningTempTableCommand & insideWhereClause & endingCommand & "WHERE ServiceFormDup = 1" & orderByClause

		'sqlapi.Sort(sendCommand, RMA_myCmd, ds, da)

		'sqlapi.RetriveData(Freeze, da, ds, DGV_Items, scrollValue, entriesToShow, numberOfRecords, B_Next, B_Last, B_First, B_Previous)

		Cursor = Cursors.Default
	End Sub

	Private Sub TB_Search_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_Search.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Search_Click()
		End If
	End Sub

	Private Sub TB_Search2_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TB_Search2.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Search_Click()
		End If
	End Sub

	Private Sub CB_Display_SelectedValueChanged() Handles CB_Display.SelectedValueChanged
		entriesToShow = CInt(CB_Display.Text)
	End Sub

	Private Sub CB_Search_Click() Handles CB_Search.Click
		CB_Search.SelectedIndex = 0
	End Sub

	Private Sub CB_Sort_Click() Handles CB_Sort.Click
		CB_Sort.SelectedIndex = 0
	End Sub

	Private Sub DGV_Items_RowPostPaint(ByVal sender As Object, ByVal e As DataGridViewRowPostPaintEventArgs) Handles DGV_Items.RowPostPaint
		'Go through each row of the DGV and add the row number to the row header.
		Using b As SolidBrush = New SolidBrush(DGV_Items.RowHeadersDefaultCellStyle.ForeColor)
			e.Graphics.DrawString(e.RowIndex + 1 + scrollValue, DGV_Items.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
		End Using
	End Sub
#End Region

	Private Sub DGV_Items_CellMouseDoubleClick() Handles DGV_Items.CellMouseDoubleClick
		Try
			ViewDetails_Button_Click()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub ViewDetails_Button_Click() Handles ViewDetails_Button.Click
		Dim SFN As String = ""
		If DGV_Items.SelectedCells.Count <> 1 Then
			MsgBox("Please select one record to use.")
			Return
		Else
			'Get the Form number
			Dim currentRowIndex As Integer = DGV_Items.SelectedCells.Item(0).RowIndex
			Dim currentTable As DataTable = ds.Tables(0)

			'Open up our detailed record interface
			Dim recordDetailsDialog As New RecordDetails(currentRowIndex, currentTable)
			recordDetailsDialog.ShowDialog()

			If recordDetailsDialog.changed = True Then
				If isSearched = True Then
					B_Search_Click()
				End If
				If isSorted = True Then
					RefreshList()
					B_Sort_Click()
				End If
				If isSearched = False And isSorted = False Then
					RefreshList()
				End If
			End If
		End If
	End Sub

	Private Sub CB_Search_SelectedIndexChanged() Handles CB_Search.SelectionChangeCommitted
		If doNotUpdate = False Then
			If CB_Search.Text <> DB_HEADER_CODE Then
				Code_ComboBox.Visible = False
				Code_ComboBox.Enabled = False
				CB_Operand1.Visible = True
				CB_Operand1.Enabled = True
				TB_Search.Visible = True
				TB_Search.Enabled = True
			Else
				Code_ComboBox.Visible = True
				Code_ComboBox.Enabled = True
				CB_Operand1.Visible = False
				CB_Operand1.Enabled = False
				TB_Search.Visible = False
				TB_Search.Enabled = False
			End If
		End If
	End Sub

	Private Sub CB_Search2_SelectedIndexChanged() Handles CB_Search2.SelectionChangeCommitted
		If doNotUpdate = False Then
			If CB_Search2.Text <> DB_HEADER_CODE Then
				Code2_ComboBox.Visible = False
				Code2_ComboBox.Enabled = False
				CB_Operand2.Visible = True
				CB_Operand2.Enabled = True
				TB_Search2.Visible = True
				TB_Search2.Enabled = True
			Else
				Code2_ComboBox.Visible = True
				Code2_ComboBox.Enabled = True
				CB_Operand2.Visible = False
				CB_Operand2.Enabled = False
				TB_Search2.Visible = False
				TB_Search2.Enabled = False
			End If
		End If
	End Sub
End Class