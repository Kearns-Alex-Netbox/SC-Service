'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ViewErrorCodes.vb
'
' Description: Shows all of the Codes that we have in our database with the ability to edit or add new entries.
'
' New: Create a new Code.
' Select: Edit the selected entry or create a new one depending on which row is selected.
'
' Double click: The same as the Select Function.
'
' Responses: The string that passed to the editor window.
'		- NEW: Signals that we have chosen to create a new Code.
'		- [Service Form]: Signals that we have chosen an existing Code to edit.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class ViewErrorCodes

	Private Sub EditErrorCodes_Load() Handles MyBase.Load
		UpdateTable()
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub New_Button_Click() Handles New_Button.Click
		'Open up our edit interface
		Dim editCodeDialog As New EditCode("NEW")
		editCodeDialog.ShowDialog()
	End Sub

	Private Sub Codes_DataGridView_CellMouseDoubleClick() Handles Codes_DataGridView.CellMouseDoubleClick
		Try
			Select_Button_Click()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub Select_Button_Click() Handles Select_Button.Click
		Dim code As String = ""
		If Codes_DataGridView.SelectedCells.Count <> 1 Then
			MsgBox("Please select one record to use.")
			Return
		Else
			'Get the "code" string
			code = Codes_DataGridView.Rows(Codes_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_CODE).Value.ToString

			'Open up our edit interface
			Dim editCodeDialog As New EditCode(code)
			editCodeDialog.ShowDialog()
		End If
	End Sub

	Public Sub UpdateTable()
		Dim cmd As New SqlCommand("", myConn)

		'Get teh columns that we want in our table.
		cmd.CommandText = "SELECT [" & DB_HEADER_CODE & "],[" & DB_HEADER_TYPE & "],[" & DB_HEADER_DESCRIPTION & "],[" & DB_HEADER_FIX & "] FROM " & TABLE_RMACODES & " ORDER BY [" & DB_HEADER_CODE & "] ASC"

		Dim dtCodes = New DataTable

		dtCodes.Load(cmd.ExecuteReader())
		Try
			dtCodes.Rows.Add("NEW")
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try


		Codes_DataGridView.DataSource = Nothing
		Codes_DataGridView.DataSource = dtCodes
		Codes_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

		Codes_DataGridView.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		Codes_DataGridView.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		Codes_DataGridView.Columns(2).Width = 300

		Codes_DataGridView.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		Codes_DataGridView.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		Codes_DataGridView.Columns(3).Width = 300
		Codes_DataGridView.ClearSelection()
	End Sub

End Class