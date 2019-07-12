'Imports Microsoft.Office.Interop
Imports System.Data.SqlClient

Public Class GenerateReport

	Public Sub Generate_itemslistReport(ByRef ds As DataSet)
		Try
			Dim xlApp As New Object 'Excel.Application
			Dim xlWorkBook As Object 'Excel.Workbook
			Dim xlWorkSheet As Object 'Excel.Worksheet
			Dim misValue As Object = Reflection.Missing.Value
			Dim INDEX_row As Integer = 1
			Dim INDEX_column As Integer = 1

			xlApp = CreateObject("Excel.Application")

			xlWorkBook = xlApp.Workbooks.Add(misValue)
			xlWorkSheet = xlWorkBook.Sheets("sheet1")

			xlWorkSheet.PageSetup.CenterHeader = "RMA Report   " & Date.Now

			For Each dc As DataColumn In ds.Tables(0).Columns
				xlWorkSheet.Cells(1, INDEX_column) = dc.ColumnName
				INDEX_column += 1
			Next
			xlWorkSheet.Cells(1, INDEX_column) = "Notes:"


			INDEX_row += 1
			'Reset the Column index
			INDEX_column = 1

			For Each dr As DataRow In ds.Tables(0).Rows
				For Each dc As DataColumn In ds.Tables(0).Columns
					Dim value As String = dr(dc).ToString
					If dc.ColumnName.ToLower.Contains("date") = True And value.Length <> 0 Then
						value = value.Substring(0, value.IndexOf(" "))
					End If
					xlWorkSheet.Cells(INDEX_row, INDEX_column) = "'" & value.Trim
					INDEX_column += 1
				Next

				Dim test As String = dr(0).ToString

				Dim mycmd As New SqlCommand("SELECT [" & DB_HEADER_TECHNICIANNOTES & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & test & "'", myConn)

				Dim notes As String = mycmd.ExecuteScalar.ToString
				xlWorkSheet.Cells(INDEX_row, INDEX_column) = notes.Trim

				INDEX_row += 1
				'Reset the Column index
				INDEX_column = 1
			Next

			xlWorkSheet.Range("A1:X1").EntireColumn.AutoFit()
			xlWorkSheet.Range("R1").EntireColumn.WrapText = True
			xlWorkSheet.Range("R1").ColumnWidth = 90
			xlWorkSheet.Range("D1").EntireColumn.WrapText = True
			xlWorkSheet.Range("D1").ColumnWidth = 20
			xlWorkSheet.Range("A1").EntireRow.Font.Bold = True
			xlWorkSheet.Range("A1:X1").EntireColumn.HorizontalAlignment = -4131 ' Excel.XlHAlign.xlHAlignLeft

			xlApp.DisplayAlerts = False
			xlApp.Visible = True

			releaseObject(xlWorkSheet)
			releaseObject(xlWorkBook)
			releaseObject(xlApp)
		Catch ex As Exception
			MsgBox("File was not written: " & ex.Message)
		End Try
	End Sub

	Private Sub releaseObject(ByVal obj As Object)
		Try
			Runtime.InteropServices.Marshal.ReleaseComObject(obj)
			obj = Nothing
		Catch ex As Exception
			obj = Nothing
		Finally
			GC.Collect()
		End Try
	End Sub

End Class
