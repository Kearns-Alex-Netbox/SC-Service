'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: SelectRecord.vb
'
' Description: Shows all of the service forms that are associated with the serial number. Depending on how this form is created the user 
'	can have the ability to use a new service form number.
'
' New: Uses a new form number with the current serial number.
' Select: Slects the table entry that is selected. If it is an existing form number, that number is then passed back and used for whatever
'	form called it.
'
' Double click: The same as the select button.
'
' Responses: The string that is used once this form is closed.
'		- New: Signals that we have chosen to use a new service form number.
'		- CANCEL: Signals that we have decided not to choose an option.
'		- [Service Form]: Signals that we have chosen an existing service form number to use.
'-----------------------------------------------------------------------------------------------------------------------------------------
Public Class SelectRecord

	Dim dtResults As DataTable
	Public response As String = "CANCEL"
	Dim canCreateNew As Boolean

	Sub New(ByRef resultTable As DataTable, ByRef newOption As Boolean)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		dtResults = New DataTable()
		dtResults = resultTable.Copy
		canCreateNew = newOption
	End Sub

	Private Sub SelectRecord_Load() Handles MyBase.Load
		If canCreateNew = True Then
			dtResults.Rows.Add("NEW")
		Else
			New_Button.Enabled = False
		End If

		Records_DataGridView.DataSource = dtResults
		Records_DataGridView.ClearSelection()
	End Sub

	Private Sub Cancel_Button_Click() Handles Cancel_Button.Click
		response = "CANCEL"
		Close()
	End Sub

	Private Sub New_Button_Click() Handles New_Button.Click
		response = "NEW"
		Close()
	End Sub

	Private Sub Records_DataGridView_CellMouseDoubleClick() Handles Records_DataGridView.CellMouseDoubleClick
		Try
			Select_Button_Click()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub Select_Button_Click() Handles Select_Button.Click
		If Records_DataGridView.SelectedCells.Count <> 1 Then
			MsgBox("Please select one record to use.")
			Return
		Else
			'Return the GUID of the record.
			If Records_DataGridView.Rows(Records_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_SERVICEFORM).Value.ToString = "NEW" Then
				response = "NEW"
			Else
				response = Records_DataGridView.Rows(Records_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_SERVICEFORM).Value.ToString
			End If
		End If

		Close()
	End Sub

End Class