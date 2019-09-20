'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: MenuMain.vb
'
' Description: Main Menu of the whole program. Side panel with buttons that open up forms inside the larger panel area. Top panel has 
'		special buttons that control all of the open forms. Only one form may be open at a time.
'
' Buttons:
'	Cascade = Takes all of the open forms and Cascades them from the top left cornor in order of last visited.
'	Tile = Takes all of the open forms and tiles them side by side to a more even size so that every form is on the screen.
'	Minimize = Minimizes all of the open forms.
'	Close = Closes all of the open forms.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class MenuMain

	Dim sqlapi As New SQL_API()
	Dim myCmd As New SqlCommand("", myConn)
	Dim loginName As String = ""

	Private Sub MenuMain_Load() Handles MyBase.Load
		CenterToParent()
		KeyPreview = True
		IsMdiContainer = True

		'Check to make sure that this parameter exists in our database.
		myCmd.CommandText = "SELECT * FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
		Dim resulttable As New DataTable
		resulttable.Load(myCmd.ExecuteReader)

		If resulttable.Rows.Count = 0 Then
			'Add in the SFN parameter into the table. 
			myCmd.CommandText = "INSERT INTO " & TABLE_PARAMETERS & "([" & DB_HEADER_ID & "],[" & DB_HEADER_VALUESTRING & "]) VALUES('SFN','17001')"
			myCmd.ExecuteNonQuery()
		End If
	End Sub



	Private Sub GeneralInfo_Button_Click() Handles GeneralInfo_Button.Click
		OpenForm(NewRMA)
	End Sub


	Private Sub ViewRMAs_Button_Click() Handles ViewRMAs_Button.Click
		OpenForm(ViewRMAs)
	End Sub

	Private Sub ViewErrorCodes_Button_Click() Handles ViewErrorCodes_Button.Click
		OpenForm(ViewErrorCodes)
	End Sub

	Private Sub ViewBillType_Button_Click() Handles ViewBillType_Button.Click
		OpenForm(ViewBillTypes)
	End Sub

	Private Sub ViewAddresses_Button_Click() Handles ViewAddresses_Button.Click
		Dim viewAddressesDialog As New ViewAddresses(False, True)
		OpenForm(viewAddressesDialog)
	End Sub

	Private Sub B_ViewStatus_Click(sender As Object, e As EventArgs) Handles B_ViewStatus.Click
		OpenForm(ViewStatusList)
	End Sub



	Private Sub Settings_Button_Click() Handles Settings_Button.Click
		Dim Settings As New Settings()
		Settings.ShowDialog()
	End Sub

	Private Sub Exit_Button_Click() Handles Exit_Button.Click
		Close()
	End Sub

#Region "Top Panel Buttons"
	Private Sub B_Cascade_Click() Handles B_Cascade.Click
		For Each form As Form In MdiChildren
			form.WindowState = FormWindowState.Normal
		Next
		LayoutMdi(MdiLayout.Cascade)
	End Sub

	Private Sub B_Tile_Click() Handles B_Tile.Click
		For Each form As Form In MdiChildren
			form.WindowState = FormWindowState.Normal
		Next
		LayoutMdi(MdiLayout.TileVertical)
	End Sub

	Private Sub B_Minimize_Click() Handles B_Minimize.Click
		For Each form As Form In MdiChildren
			form.WindowState = FormWindowState.Minimized
		Next
	End Sub

	Private Sub B_Close_Click() Handles B_Close.Click
		For Each form As Form In MdiChildren
			form.Close()
		Next
	End Sub
#End Region

	Private Sub MenuMain_FormClosed() Handles Me.FormClosed
		Dim result As String = ""
		sqlapi.CloseDatabase(myConn, result)
		Application.Exit()
	End Sub

	Public Function OpenForm(ByRef thisForm As Form) As Boolean
		'This index is used later for telling the child form who is their parent.
		Dim indexOfMenu As Integer = 0

		'Check the forms that are already open and see if our form is open.
		Dim frmCollection = Application.OpenForms
		For i = 0 To frmCollection.Count - 1
			If frmCollection.Item(i).Name = thisForm.Name Then
				'The form is already open so we switch the focus to it.
				frmCollection.Item(i).Activate()
				Return True
			End If
			If frmCollection.Item(i).Name = Name Then
				indexOfMenu = i
			End If
		Next i

		'Set up the form with specific parameters and open it up.
		thisForm.StartPosition = FormStartPosition.Manual
		thisForm.Left = 0
		thisForm.Top = 0
		thisForm.MdiParent = frmCollection.Item(indexOfMenu)
		thisForm.Show()
		thisForm.BringToFront()

		Return True
	End Function

	Private Sub Print_Button_Click()
		'Check to see if we have a PDF path.
		If My.Settings.PDFFilePath.Length = 0 Then
			MsgBox("File Path to the PDF has not been set up. Please check the path in the settings.")
			Return
		End If

		'Check to see if the file exists.
		If IO.File.Exists(My.Settings.PDFFilePath) = False Then
			MsgBox("File Path does not exist in settings. Please check the path in the settings.")
			Return
		End If

		'Set the printer we want to use
		Dim p As New PrintDialog
		p.UseEXDialog = True
		If p.ShowDialog = Windows.Forms.DialogResult.OK Then

			Dim processInfo As New ProcessStartInfo
			processInfo.UseShellExecute = True
			processInfo.Verb = "print"
			processInfo.WindowStyle = ProcessWindowStyle.Hidden

			'Here specify printer name
			processInfo.Arguments = p.PrinterSettings.PrinterName.ToString()

			'Here specify a document to be printed
			processInfo.FileName = My.Settings.PDFFilePath

			'Print the page.
			Process.Start(processInfo)
		End If
	End Sub

End Class