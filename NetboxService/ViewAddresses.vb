'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: ViewAddresses.vb
'
' Description: Shows all of the Addresses that we have in our database with RMAs. Depending on how this form is created the user 
'	can have the ability to create a new Address.
'
' New: Creates a new Address.
' Edit / Select: Dual Purpose. Takes the selected entry and then either Edits it or sends it back to the form that called it.
'
' Double click: The same as the Edit / Select button.
'
' Responses: The string that is used once this form is closed.
'		- New: Signals that we have chosen to create a new Address.
'		- CANCEL: Signals that we have decided not to choose an option.
'		- [Service Form]: Signals that we have chosen an existing Address to edit or use.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class ViewAddresses
	Dim isUsable As Boolean
	Public thisGUID As String = ""
	Dim canCreateNew As Boolean

	Public Sub New(ByRef usable As Boolean, ByRef newOption As Boolean)

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		isUsable = usable
		canCreateNew = newOption
	End Sub

	Private Sub ViewAddresses_Load() Handles MyBase.Load
		UpdateTable()

		If isUsable = True Then
			New_Button.Enabled = False
			New_Button.Visible = False
			Select_Button.Text = "Select"
		End If
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		thisGUID = ""
		Close()
	End Sub

	Private Sub New_Button_Click() Handles New_Button.Click
		Dim id As String = "NEW"

		'Open up our edit interface
		Dim editAddressDialog As New EditAddress(id)
		editAddressDialog.ShowDialog()
	End Sub

	Private Sub Addresses_DataGridView_CellMouseDoubleClick() Handles Addresses_DataGridView.CellMouseDoubleClick
		Try
			Select_Button_Click()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Private Sub Select_Button_Click() Handles Select_Button.Click
		Dim id As String = ""
		If Addresses_DataGridView.SelectedCells.Count <> 1 Then
			MsgBox("Please select one record to use.")
			Return
		Else
			If isUsable = False Then
				'Return the GUID of the record.
				If Addresses_DataGridView.Rows(Addresses_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_COMPANY).Value.ToString = "NEW" Then
					id = "NEW"
				Else
					id = Addresses_DataGridView.Rows(Addresses_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_ID).Value.ToString
				End If

				'Open up our edit interface
				Dim editAddressDialog As New EditAddress(id)
				editAddressDialog.ShowDialog()
			Else
				thisGUID = Addresses_DataGridView.Rows(Addresses_DataGridView.SelectedCells.Item(0).RowIndex).Cells(DB_HEADER_ID).Value.ToString

				Close()
			End If
		End If
	End Sub

	Public Sub UpdateTable()
		Dim cmd As New SqlCommand("", myConn)

		'Grab the columns that we want to populate the table with.
		cmd.CommandText = "SELECT [" & DB_HEADER_COMPANY & "],[" & DB_HEADER_ADDRESS & "],[" & DB_HEADER_CITY & "],[" & DB_HEADER_STATE & "],[" & DB_HEADER_ZIPCODE & "],[" & DB_HEADER_COUNTRY & "],[" & DB_HEADER_PHONE & "],[" & DB_HEADER_CONTACTNAME & "],[" & DB_HEADER_CONTACTEMAIL & "],[" & DB_HEADER_CONTACTPHONE & "],[" & DB_HEADER_ID & "] FROM " & TABLE_RMAADDRESSES & " ORDER BY [" & DB_HEADER_COMPANY & "]"

		Dim dtaddresses = New DataTable

		dtaddresses.Load(cmd.ExecuteReader())
		If canCreateNew = True Then
			dtaddresses.Rows.Add("NEW")
		End If

		Addresses_DataGridView.DataSource = Nothing
		Addresses_DataGridView.DataSource = dtaddresses

		'Hide the GUID column so the user does not see it but we have quick access on our end.
		Addresses_DataGridView.Columns(DB_HEADER_ID).Visible = False
		Addresses_DataGridView.ClearSelection()
	End Sub

End Class