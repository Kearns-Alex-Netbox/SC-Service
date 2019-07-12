'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: Settings.vb
'
' Description: Settings that are set by the user. These settings are saved and used throughout the program.
'
' Settings:
'	Service Form Number = The current form number that we are using.
'	PDF File = The PDF file of the service form that we will use to print out to a printer.
'	Returns Directory = Folder location where we are storing all of our files for service form that we are working with.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class Settings
	Private Sub Settings_Load() Handles MyBase.Load
		Dim cmd As New SqlCommand("", myConn)

		'Get the current Service form number that is stored in the database.
		cmd.CommandText = "SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
		ServiceForm_TextBox.Text = cmd.ExecuteScalar
		PDFFile_TextBox.Text = My.Settings.PDFFilePath
		ReturnsDirectory_TextBox.Text = My.Settings.ServiceFoldersDir
	End Sub

	Private Sub Close_Button_Click() Handles Close_Button.Click
		Close()
	End Sub

	Private Sub Save_Button_Click() Handles Save_Button.Click
		Dim cmd As New SqlCommand("", myConn)
		Dim updateSFN As Boolean = False
		Dim sfnNumber As Integer = 0

		'Check to see if we have anything in our SFN to update.
		If NewSFN_TextBox.Text.Length <> 0 Then
			Try
				'Try casting the text into an Integer.
				sfnNumber = NewSFN_TextBox.Text
			Catch ex As Exception
				MsgBox("New Serial Form Number needs to be a positive whole integer.")
				Return
			End Try

			'Set our flag to update our SFN.
			updateSFN = True
		End If

		'Preform our updates
		If updateSFN = True Then
			Try
				cmd.CommandText = "UPDATE " & TABLE_PARAMETERS & " SET " & DB_HEADER_VALUESTRING & " = '" & sfnNumber & "' WHERE [" & DB_HEADER_ID & "] = 'SFN'"
				cmd.ExecuteNonQuery()
			Catch ex As Exception
				MsgBox(ex.Message)
				Return
			End Try
		End If

		'Once we are done with our updates, we need to refresh a few of the textboxes.
		cmd.CommandText = "SELECT [" & DB_HEADER_VALUESTRING & "] FROM " & TABLE_PARAMETERS & " WHERE [" & DB_HEADER_ID & "] = 'SFN'"
		ServiceForm_TextBox.Text = cmd.ExecuteScalar

		NewSFN_TextBox.Text = ""

		My.Settings.PDFFilePath = PDFFile_TextBox.Text
		My.Settings.ServiceFoldersDir = ReturnsDirectory_TextBox.Text

		My.Settings.Save()
		MsgBox("Settings Saved")
	End Sub

	Private Sub BrowsePDFFile_Button_Click() Handles BrowsePDFFile_Button.Click
		Dim selectFile As New SaveFileDialog
		selectFile.CheckFileExists = False
		selectFile.CheckPathExists = False
		selectFile.OverwritePrompt = False

		If My.Settings.PDFFilePath.Length = 0 Then
			selectFile.InitialDirectory = "C:\"
		Else
			selectFile.InitialDirectory = My.Settings.PDFFilePath.Substring(0, My.Settings.PDFFilePath.LastIndexOf("\") + 1)
		End If
		selectFile.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
		If selectFile.ShowDialog() = DialogResult.OK Then
			PDFFile_TextBox.Text = selectFile.FileName
		End If
	End Sub

	Private Sub ReturnServiceDirectory_Button_Click() Handles ReturnServiceDirectory_Button.Click
		Dim selectFolder As New SaveFileDialog
		selectFolder.ValidateNames = False
		selectFolder.CheckFileExists = False
		selectFolder.CheckPathExists = True
		selectFolder.OverwritePrompt = False

		If My.Settings.ServiceFoldersDir.Length = 0 Then
			selectFolder.InitialDirectory = "C:\"
		Else
			selectFolder.InitialDirectory = My.Settings.ServiceFoldersDir
		End If

		selectFolder.FileName = "THIS IS A PLACE HOLDER SO YOU CAN OPEN UP THE LOCATION"
		selectFolder.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
		If selectFolder.ShowDialog() = DialogResult.OK Then
			ReturnsDirectory_TextBox.Text = selectFolder.FileName.Substring(0, selectFolder.FileName.LastIndexOf("\"))
		End If
	End Sub
End Class