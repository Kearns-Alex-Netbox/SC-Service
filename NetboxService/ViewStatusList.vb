'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ViewStatusList.vb
'
' Description: Shows all of the Status' that we have in our database with the ability to edit or add new entries in regards to Approval 
'	and Evaluation.
'
' New: Create a new Status.
' Select: Edit the selected entry or create a new one depending on which row is selected.
'
' Double click: The same as the Select Function.
'
' Responses: The string that passed to the editor window.
'		- NEW: Signals that we have chosen to create a new Status.
'		- [Status]: Signals that we have chosen an existing Status to edit.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class ViewStatusList

	Private Sub ViewStatusList_Load() Handles MyBase.Load
		UpdateTable()
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub New_Button_Click() Handles New_Button.Click
		'Open up our edit interface
		Dim editStatusDialog As New EditStatus("NEW")
		editStatusDialog.ShowDialog()
	End Sub

	Private Sub Status_DataGridView_CellMouseDoubleClick() Handles Status_DataGridView.CellMouseDoubleClick
		Try
			Select_Button_Click()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub Select_Button_Click() Handles Select_Button.Click
		Dim code As String = ""
		If Status_DataGridView.SelectedCells.Count <> 1 Then
			MsgBox("Please select one record to use.")
			Return
		Else
			'Get the "code" string
			code = Status_DataGridView.Rows(Status_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_STATUS).Value.ToString

			'Open up our edit interface
			Dim editStatusDialog As New EditStatus(code)
			editStatusDialog.ShowDialog()
		End If
	End Sub

	Public Sub UpdateTable()
		Dim cmd As New SqlCommand("", myConn)

		'Get the columns that we want in our table.
		cmd.CommandText = "SELECT [" & DB_HEADER_STATUS & "],[" & DB_HEADER_ID & "] FROM " & TABLE_RMASTATUS & " WHERE [" & DB_HEADER_ISAPPROVAL & "] = '1' ORDER BY [" & DB_HEADER_STATUS & "] ASC"

		Dim dtCodes = New DataTable

		dtCodes.Load(cmd.ExecuteReader())
		Try
			dtCodes.Rows.Add("NEW")
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try


		Status_DataGridView.DataSource = Nothing
		Status_DataGridView.DataSource = dtCodes
		Status_DataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)
		Status_DataGridView.Columns(DB_HEADER_ID).Visible = False
		Status_DataGridView.ClearSelection()
	End Sub

End Class