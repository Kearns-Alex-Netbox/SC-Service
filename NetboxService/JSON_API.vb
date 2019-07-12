Imports System.IO
Imports System.Text
Imports System.Net

Public Class JSON_API

	Public aquametrixType As Boolean = False

	Public Function GetMachineInfo(ByRef IPAddress As String, ByRef WebResp As String) As Boolean
		Dim uri As New Uri("http://" + IPAddress + "/process_json?name=machineinfo.json")
		Dim justURI As New Uri("http://" + IPAddress)
		Dim retval As Boolean

		retval = DoRequest(uri, justURI, "belleville", WebResp)
		If retval = True Then
			aquametrixType = False
		Else
			retval = DoRequest(uri, justURI, "aquametrix", WebResp)
			If retval = True Then
				aquametrixType = True
			End If
		End If
		Return retval
	End Function

	Public Function SendDiagnostic(ByRef IPAddress As String, ByRef WebResp As String) As Boolean
		Dim uri As New Uri("http://" + IPAddress + "/process_json?name=senddiagnostic.json")
		Dim justURI As New Uri("http://" + IPAddress)
		Dim retval As Boolean

		retval = DoRequest(uri, justURI, "belleville", WebResp)
		If retval = True Then
			aquametrixType = False
		Else
			retval = DoRequest(uri, justURI, "aquametrix", WebResp)
			If retval = True Then
				aquametrixType = True
			End If
		End If
		Return retval
	End Function

	Private Function DoRequest(ByRef URI As Uri, ByRef justURI As Uri, ByRef password As String, ByRef WebResp As String) As Boolean
		Dim response As WebResponse
		Dim request As WebRequest = WebRequest.Create(URI)
		Dim credentialCache As CredentialCache = New CredentialCache()
		Dim netCredential As NetworkCredential = New NetworkCredential("admin", password)

		credentialCache.Add(justURI, "Digest", netCredential)
		request.Credentials = credentialCache

		Try
			response = request.GetResponse()
		Catch ex As Exception
			Return False
		End Try

		Dim dataStream As Stream = response.GetResponseStream()
		Dim encode As Encoding = Encoding.GetEncoding("utf-8")
		Dim readStream As New StreamReader(dataStream, encode)
		Dim read(2048) As [Char]
		' Read up to 512 charcters 
		Dim count As Integer = readStream.Read(read, 0, 2048)

		readStream.Close()
		response.Close()

		' Convert into string from an array of Chars
		Dim str As New [String](read, 0, count)

		WebResp = str
		Return True
	End Function

End Class

'Machine Information response structure.
Public Class JSON_InfoResult
	Public cpuid As String
	Public model As String
	Public version As String
	Public serial As String
	Public cpuserial As String
	Public ioversion As String
	Public blversion As String
	Public macaddress As String
End Class