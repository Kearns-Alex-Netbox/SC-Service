'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ViewBillTypes.vb
'
' Description: Shows all of the Bill Types that we have in our database with RMAs with the ability to edit or add new entries.
'
' New: Create a new Bill Type.
' Select: Edit the selected entry or create a new one depending on which row is selected.
'
' Double click: The same as the Select Function.
'
' Responses: The string that passed to the editor window.
'		- NEW: Signals that we have chosen to create a new Bill Type.
'		- [Service Form]: Signals that we have chosen an existing Bill Type to edit.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class ViewBillTypes

	Private Sub ViewBillTypes_Load() Handles MyBase.Load
		UpdateTable()
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub New_Button_Click() Handles New_Button.Click
		'Open up our edit interface
		Dim editBillingDialog As New EditBillingType("NEW")
		editBillingDialog.ShowDialog()
	End Sub

	Private Sub BillTypes_DataGridView_CellMouseDoubleClick() Handles BillTypes_DataGridView.CellMouseDoubleClick
		Try
			Select_Button_Click()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub Select_Button_Click() Handles Select_Button.Click
		Dim name As String = ""
		If BillTypes_DataGridView.SelectedCells.Count <> 1 Then
			MsgBox("Please select one record to use.")
			Return
		Else
			'Get the "Bill Type" string
			name = BillTypes_DataGridView.Rows(BillTypes_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_BILLTYPE).Value.ToString

			'Open up our edit interface
			Dim editBillingDialog As New EditBillingType(name)
			editBillingDialog.ShowDialog()
		End If
	End Sub

	Public Sub UpdateTable()
		Dim cmd As New SqlCommand("", myConn)

		'Get the columns that we want to fill our table with.
		cmd.CommandText = "SELECT [" & DB_HEADER_BILLTYPE & "],[" & DB_HEADER_NEEDSADDRESS & "],[" & DB_HEADER_SAMEASSHIPPING & "] FROM " & TABLE_RMABILLTYPE

		Dim dtBillTypes = New DataTable

		dtBillTypes.Load(cmd.ExecuteReader())
		dtBillTypes.Rows.Add("NEW")

		BillTypes_DataGridView.DataSource = Nothing
		BillTypes_DataGridView.DataSource = dtBillTypes
		BillTypes_DataGridView.ClearSelection()
	End Sub

End Class