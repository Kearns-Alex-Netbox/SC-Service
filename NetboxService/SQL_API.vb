'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: AQL_API.vb
'
' Description: SQL helper API for basic SQL functions.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Class SQL_API

	'Board Status'
	Const BS_BURN_IN As String = "Burn In"

	'System Status'
	Const SS_BURN_IN As String = "Burn In"

	'System Dates
	Const REGISTER_DATE As String = "RegisterDate"
	Const PARAMETER_DATE As String = "ParameterDate"
	Const BURN_IN_DATE As String = "BurnInDate"

	Dim myReader As SqlDataReader

	''' <summary>
	''' Either sets or returns the string value for username.
	''' </summary>
	''' <value>String value that you want to set username to.</value>
	''' <returns>Returns the string value that username is currently set to.</returns>
	''' <remarks>This is your username.</remarks>
	Public Property _Username() As String
		Get
			Return username.ToString
		End Get
		Set(ByVal value As String)
			username = value
		End Set
	End Property

	''' <summary>
	''' Either sets or returns the string value for password.
	''' </summary>
	''' <value>String value that you want to set password to.</value>
	''' <returns>Returns the string value that password is currently set to.</returns>
	''' <remarks>This is your passwrod.</remarks>
	Public Property _Password() As String
		Get
			Return password.ToString
		End Get
		Set(ByVal value As String)
			password = value
		End Set
	End Property

	''' <summary>
	''' Opens the connection to the database and saves the user who is logged in.
	''' </summary>
	''' <param name="myConn">The connection that you would like to make.</param>
	''' <param name="myCmd">The SQL command that will be used to make the requests.</param>
	''' <param name="LoginName">OUTPUT: String that will contain the username.</param>
	''' <param name="result">OUTPUT: Error string if somethign does not go as planned.</param>
	''' <returns>True: successful open and return username. False: unsuccessful, see result message for details.</returns>
	''' <remarks>This needs to be called before you make anyother commands.</remarks>
	Public Function OpenDatabase(ByRef myConn As SqlConnection, ByRef myCmd As SqlCommand, ByRef loginName As String, ByRef result As String) As Boolean
		myConn = New SqlConnection("server=tcp:nas1,1622;Database=" & CurrentDatabase & ";User ID=" & username & ";password= " & password & ";")
		Try
			myConn.Open()
			myCmd = myConn.CreateCommand

			'Get the logged in user name from Windows to the database.
			myCmd.CommandText = "SELECT ORIGINAL_LOGIN()"
			myReader = myCmd.ExecuteReader()
			If myReader.Read() Then
				'Check to see if we are returned a NULL value.
				If myReader.IsDBNull(0) Then
					result = "Login name returned NULL."
					Return False
				Else
					loginName = myReader.GetString(0)
				End If
			Else
				'If nothing is returned then it does not exist.
				result = "Login name does not exist."
				Return False
			End If

			myReader.Close()
			Return True
		Catch ex As Exception
			result = ex.Message
			Return False
		End Try
	End Function

	''' <summary>
	''' Closes the database connection that gets passed through.
	''' </summary>
	''' <param name="myConn">The connection that you want to close.</param>
	''' <param name="result">OUTPUT: Error result if somthing does not go right.</param>
	''' <returns>True: Successful close. False: unsuccessful close, see result message for information.</returns>
	''' <remarks>Make sure the connection is already open first before trying to close it.</remarks>
	Public Function CloseDatabase(ByRef myConn As SqlConnection, ByRef result As String) As Boolean
		Try
			If myConn.State <> ConnectionState.Closed Then
				myConn.Close()
			End If
			Return True
		Catch ex As Exception
			result = ex.Message
			Return False
		End Try
	End Function

	''' <summary>
	''' Adds a comment to the system serial number that is passed through.
	''' </summary>
	''' <param name="myCmd">The SQL Command that you will be using to make this action.</param>
	''' <param name="serialNumber">The serial number of the System that you want to add the comment to.</param>
	''' <param name="comment">The comment string that you want to add.</param>
	''' <param name="loginName">The user responsible for adding the comment.</param>
	''' <param name="record">OUTPUT: The ID associated with the System.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: Comment was added successfully. False: There was a problem, see result message.</returns>
	''' <remarks></remarks>
	Public Function AddSystemComment(ByRef myCmd As SqlCommand, ByRef serialNumber As String, ByRef comment As String, ByRef loginName As String,
									 ByRef record As Guid, ByRef result As String) As Boolean
		Try
			'Get the GUID from the passed in serial number.
			myCmd.CommandText = "SELECT systemid FROM system where [dbo.SystemStatus.id] != (Select id from SystemStatus where name = 'Scrapped') and SerialNumber = '" & serialNumber & "'"
			myReader = myCmd.ExecuteReader()
			If myReader.Read() Then
				'Check to see if we are returned a NULL value.
				If myReader.IsDBNull(0) Then
					result = "[AddSystemComment1] System serial number '" & serialNumber & "' is NULL"
					myReader.Close()
					Return False
				Else
					record = myReader.GetGuid(0)
				End If
			Else
				'If nothing is returned then it does not exist.
				result = "[AddSystemComment2] System serial number '" & serialNumber & "' does not exist."
				myReader.Close()
				Return False
			End If
			myReader.Close()

			Dim fixedComment As String = comment
			'Replace any single qoutes with double single qoutes for SQL format.
			If comment.Contains("'"c) = True Then
				fixedComment = comment.Replace("'", "''")
			End If

			'Insert the comment corresponding to the serial number into the SystemAudit table form this user.
			myCmd.CommandText = "INSERT INTO dbo.SystemAudit(id,[dbo.System.systemid],Comment,LastUpdate, [User]) VALUES(NEWID(), '" _
					& record.ToString() & "','" & fixedComment & "',GETDATE(),'" & loginName & "')"
			myCmd.ExecuteNonQuery()
			Return True
		Catch ex As Exception
			result = "[AddSystemComment exception] " & ex.Message
			myReader.Close()
			Return False
		End Try
	End Function

	Public Function AddBoardComment(ByRef myCmd As SqlCommand, ByRef serialNumber As String, ByRef comment As String, ByRef loginName As String,
									ByRef record As Guid, ByRef result As String) As Boolean
		Try
			'Get the GUID from the passed in serial number.
			myCmd.CommandText = "SELECT boardid FROM dbo.Board WHERE SerialNumber = '" & serialNumber & "'"
			myReader = myCmd.ExecuteReader()
			If myReader.Read() Then
				'Check to see if we are returned a NULL value.
				If myReader.IsDBNull(0) Then
					result = "[AddBoardComment1] Board serial number '" & serialNumber & "' is NULL"
					myReader.Close()
					Return False
				Else
					record = myReader.GetGuid(0)
				End If
			Else
				'If nothing is returned then it does not exist.
				result = "[AddBoardComment2] Board serial number '" & serialNumber & "' does not exist."
				myReader.Close()
				Return False
			End If
			myReader.Close()

			Dim fixedComment As String = comment
			'Replace any single qoutes with double single qoutes for SQL format.
			If comment.Contains("'"c) = True Then
				fixedComment = comment.Replace("'", "''")
			End If

			'Insert the comment corresponding to the serial number into the BoardAudit table form this user.
			myCmd.CommandText = "INSERT INTO dbo.BoardAudit(id,[dbo.Board.boardid],Comment,LastUpdate, [User]) VALUES(NEWID(), '" _
					& record.ToString() & "','" & fixedComment & "',GETDATE(),'" & loginName & "')"
			myCmd.ExecuteNonQuery()
			Return True
		Catch ex As Exception
			result = "[AddBoardComment exception] " & ex.Message
			myReader.Close()
			Return False
		End Try
	End Function

	''' <summary>
	''' Updates the status of the system that we pass through.
	''' </summary>
	''' <param name="myCmd">The sql command that we will be using.</param>
	''' <param name="status">The new status that we are updating to the system.</param>
	''' <param name="systemSerialNumber">The system serial number that we are working with.</param>
	''' <param name="loginName">The user responsible for updating the status.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: Everything worked out. False: Something went wrong. see result for more details.</returns>
	''' <remarks></remarks>
	Public Function UpdateSystemStatus(ByRef myCmd As SqlCommand, ByRef myConn As SqlConnection, ByRef status As String, ByRef SystemSerialNumber As String, ByRef loginName As String,
									  ByRef result As String, ByRef trans As Boolean) As Boolean
		Dim systemStatus As String = ""
		Dim boardStatus As String = ""
		Dim record As Guid = Nothing
		Dim transaction As SqlTransaction = Nothing

		Try
			'Get system status that is passed through.
			If GetSystemStatus(myCmd, status, systemStatus, result) = False Then
				result = "[UpdateSystemStatus1] " & result
				Return False
			End If

			If trans = True Then
				transaction = myConn.BeginTransaction("Update system status")
				myCmd.Transaction = transaction
			End If

			myCmd.CommandText = "UPDATE dbo.System SET LastUpdate=GETDATE(), [dbo.SystemStatus.id]='" & systemStatus & "' WHERE SerialNumber = '" & SystemSerialNumber & "' and
[dbo.SystemStatus.id] != (Select id from SystemStatus where name = 'Scrapped')"
			myCmd.ExecuteNonQuery()

			If String.Compare(status, SS_SET_PARAMETERS) = 0 Then
				If GetBoardStatus(myCmd, myReader, BS_NETWORK_REGISTERED, boardStatus, result) = False Then
					RollBack(transaction, result)
					Return False
				End If
			ElseIf String.Compare(status, SS_BOARDS_REGISTERED) = 0 Then
				If GetBoardStatus(myCmd, myReader, BS_BOARD_REGISTERED, boardStatus, result) = False Then
					RollBack(transaction, result)
					Return False
				End If
			Else
				If GetBoardStatus(myCmd, myReader, status, boardStatus, result) = False Then
					RollBack(transaction, result)
					Return False
				End If
			End If

			'---------------------------'
			'   M O T H E R B O A R D   '
			'---------------------------'

			'Grab the motherboard id associated with the passed in serial number.
			If GetBoardGUIDBySystemSerialNumber(myCmd, myReader, SystemSerialNumber, "Motherboard", record, result) = False Then
				RollBack(transaction, result)
				result = "[UpdateSystemStatus2] " & result
				Return False
			End If

			If record <> Guid.Empty Then
				'Update the status.
				myCmd.CommandText = "UPDATE dbo.Board SET [dbo.BoardStatus.id]='" & boardStatus & "' WHERE boardid = '" & record.ToString & "'"
				myCmd.ExecuteNonQuery()

				'Insert the comment.
				myCmd.CommandText = "INSERT INTO dbo.BoardAudit(id,[dbo.Board.boardid],Comment,LastUpdate, [User]) VALUES(NEWID(), '" _
						& record.ToString() & "','" & "Board in: " & status & " due to System Status update.',GETDATE(),'" & loginName & "')"
				myCmd.ExecuteNonQuery()
			End If

			'-------------------------'
			'   M A S T E R   C P U   '
			'-------------------------'

			'Grab the Main CPU id associated with the passed in serial number.
			If GetBoardGUIDBySystemSerialNumber(myCmd, myReader, SystemSerialNumber, "MainCPU", record, result) = False Then
				RollBack(transaction, result)
				result = "[UpdateSystemStatus3] " & result
				Return False
			End If

			If record <> Guid.Empty Then
				'Update the status.
				myCmd.CommandText = "UPDATE dbo.Board SET [dbo.BoardStatus.id]='" & boardStatus & "' WHERE boardid = '" & record.ToString & "'"
				myCmd.ExecuteNonQuery()

				'Insert the comment.
				myCmd.CommandText = "INSERT INTO dbo.BoardAudit(id,[dbo.Board.boardid],Comment,LastUpdate, [User]) VALUES(NEWID(), '" _
						& record.ToString() & "','" & "Board in: " & status & " due to System Status update.',GETDATE(),'" & loginName & "')"
				myCmd.ExecuteNonQuery()
			End If

			'---------------------'
			'   S L O T   2 - 7   '
			'---------------------'

			For i = 2 To 7
				'Grab the board id for the slot we are dealing with using 'i' to cycle through each slot number.
				If GetBoardGUIDBySystemSerialNumber(myCmd, myReader, SystemSerialNumber, "Slot" & i, record, result) = False Then
					RollBack(transaction, result)
					result = "[UpdateSystemStatus4] " & result
					Return False
				End If

				'Check to see if our record got an id back or if it is empty.
				If record <> Guid.Empty Then
					'Update our board status.
					myCmd.CommandText = "UPDATE dbo.Board SET LastUpdate=GETDATE(), [dbo.BoardStatus.id]='" & boardStatus & "' WHERE boardid = '" & record.ToString & "'"
					myCmd.ExecuteNonQuery()

					'Insert the comment.
					myCmd.CommandText = "INSERT INTO dbo.BoardAudit(id,[dbo.Board.boardid],Comment,LastUpdate, [User]) VALUES(NEWID(), '" _
							& record.ToString() & "','" & "Board in: " & status & " due to System Status update.',GETDATE(),'" & loginName & "')"
					myCmd.ExecuteNonQuery()
				End If
			Next i
			If trans = True Then
				transaction.Commit()
			End If
			Return True
		Catch ex As Exception
			RollBack(transaction, result)
			result = "[UpdateSystemStatus exception] " & ex.Message
			Return False
		End Try
	End Function

	''' <summary>
	''' Updates the status of the board that we pass through.
	''' </summary>
	''' <param name="myCmd">The sql command that we will be using.</param>
	''' <param name="status">The new status that we are updating to the board.</param>
	''' <param name="boardSerialNumber">The board serial number that we are working with.</param>
	''' <param name="loginName">The user responsible for updating the status.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: Everything worked out. False: Something went wrong. see result for more details.</returns>
	''' <remarks></remarks>
	Public Function UpdateBoardStatus(ByRef myCmd As SqlCommand, ByRef status As String, ByRef boardSerialNumber As String, ByRef loginName As String,
									  ByRef result As String) As Boolean
		Dim boardStatus As String = ""
		Dim record As Guid = Nothing
		Try
			'Get board status that is passed through.
			If GetBoardStatus(myCmd, myReader, status, boardStatus, result) = False Then
				result = "[UpdateBoardStatus1] " & result
				Return False
			End If

			myCmd.CommandText = "UPDATE dbo.Board SET LastUpdate=GETDATE(), [dbo.BoardStatus.id]='" & boardStatus & "' WHERE SerialNumber = '" & boardSerialNumber & "'"
			myCmd.ExecuteNonQuery()

			If AddBoardComment(myCmd, boardSerialNumber, "Status updated to " & status & ".", loginName, record, result) = False Then
				result = "[UpdateBoardStatus1] " & result
				Return False
			End If

			Return True
		Catch ex As Exception
			result = "[UpdateBoardStatus exception] " & ex.Message
			Return False
		End Try
	End Function

	''' <summary>
	''' Gets the board status from the SQL reader that is passed through. Closes the passed reader before exit.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="status">Status that we are looking for.</param>
	''' <param name="boardStatus">OUTPUT: String that will hold the GUID of the status.</param>
	''' <param name="result">OUTPUT: Error message that will give us some insight as to what went wrong.</param>
	''' <returns>True: Everything worked out, returns our boardStatus. False: Somethign went wrong, see result for insight.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardStatus(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef status As String, ByRef boardStatus As String,
								   ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT id FROM dbo.boardStatus WHERE name = '" & status & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetBoardStatus1] Board status name '" & status & "' is NULL."
				myReader.Close()
				Return False
			Else
				boardStatus = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardStatus2] Board status name '" & status & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the Board Serial Number using the passed in Board ID and System serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="systemSerialNumber">The system serial number that we are working with.</param>
	''' <param name="board">Board ID that we want the serial number from.</param>
	''' <param name="record">OUTPUT: The ID associated with the board.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the Board Serial Number. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardGUIDBySystemSerialNumber(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef systemSerialNumber As String,
													 ByRef board As String, ByRef record As Guid, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT [" & board & ".boardid] FROM dbo.System WHERE SerialNumber = '" & systemSerialNumber & "' and [dbo.SystemStatus.id] != (Select id from SystemStatus where name = 'Scrapped')"
		myReader = myCmd.ExecuteReader()
		If myReader.Read() Then
			'Check to see if the Reader is empty/NULL or not.
			If myReader.IsDBNull(0) Then
				record = Guid.Empty
			Else
				'If not, set our record GUID to whatever was passed back to us.
				record = myReader.GetGuid(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardGUIDBySystemSerialNumber1] Board '" & board & "'/SerialNumber '" & systemSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the board serial number associated with the passed GUID.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardGUID">The GUID that we are working with.</param>
	''' <param name="serialNumber">OUTPUT: The serial number associated with the board GUID.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the Board Serial Number. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardSerialNumberByGUID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardGUID As String,
											   ByRef serialNumber As String, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT SerialNumber FROM dbo.Board WHERE boardid = '" & boardGUID & "'"
		myReader = myCmd.ExecuteReader()
		If myReader.Read() Then
			'Check to see if the Reader is empty/NULL or not.
			If myReader.IsDBNull(0) Then
				serialNumber = ""
			Else
				'If not, set our string to whatever was passed back to us.
				serialNumber = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardSerialNumberByGUID1] Board GUID '" & boardGUID & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the MAC Address associated with the passed in board serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The board serial number that we are checking for.</param>
	''' <param name="MACAddress">OUTPUT: The MAC Address that is associated with the System serial number.</param>
	''' <param name="result">OUTPUT: Error message that gives us a hint on what went wrong.</param>
	''' <returns>True: The record exists, returns MAC Address. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetMACAddress(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String, ByRef MACAddress As String,
								  ByRef result As String) As Boolean

		myCmd.CommandText = "SELECT MACAddress FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetMACAddress1] MAC Address for '" & boardSerialNumber & "' is NULL."
				myReader.Close()
				MACAddress = ""
				Return False
			Else
				MACAddress = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetMACAddress2] System serial number '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Get the system version of the passed system serail number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="systemSerialNumber">The system serial number that we are working with.</param>
	''' <param name="record">OUTPUT: The ID associated with the board.</param>
	''' <param name="version">OUTPUT: The version of the system.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the version. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetSystemVersionByID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef systemSerialNumber As String,
									 ByRef record As Guid, ByRef version As String, ByRef result As String) As Boolean
		Dim CPUguid As String = ""

		'First grab the GUID of the CPU Serial Board associated with the system.
		myCmd.CommandText = "SELECT [MainCPU.boardid] FROM dbo.System WHERE SerialNumber = '" & systemSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetSystemVersion1] CPU Board ID number for '" & systemSerialNumber & "' is NULL."
				myReader.Close()
				Return False
			Else
				CPUguid = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemVersion2] System serial number '" & systemSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		'Second grab the Version for the board with the GIUD we got from the first step.
		myCmd.CommandText = "SELECT SoftwareVersion FROM dbo.Board WHERE boardid = '" & CPUguid & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				version = ""
				myReader.Close()
				Return False
			Else
				version = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemVersion3] CPU Version for '" & systemSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets the Software Version of the passed in board Serial Number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The system serial number that we are working with.</param>
	''' <param name="version">OUTPUT: The version of the power A to D.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the version. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardSoftwareVersion(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String,
											ByRef version As String, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT SoftwareVersion FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				version = ""
				myReader.Close()
			Else
				version = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardVersion1] Board Version for '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets all of the system audit records associated with the passed system serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="SystemID">The system serial number that we are working with.</param>
	''' <param name="RichText">The listbox that we want to put all of the information into.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the date associated with the date field. False: The record does not exists, see result for details.</returns>
	''' <remarks></remarks>
	Public Function GetSystemAuditByID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef systemID As String, ByRef RichText As RichTextBox, ByRef result As String) As Boolean
		Dim record As Guid = Nothing

		'Next we grab the information we want form the Audit table.
		myCmd.CommandText = "SELECT LastUpdate, [User], Comment FROM dbo.SystemAudit WHERE [dbo.System.systemid] = '" & systemID & "' ORDER BY LastUpdate DESC"
		myReader = myCmd.ExecuteReader()

		Do While myReader.HasRows

			Do While myReader.Read()
				RichText.Text = RichText.Text & myReader.GetDateTime(0).ToString & " | " & myReader.GetString(1) & vbNewLine
				RichText.Text = RichText.Text & myReader.GetString(2) & vbNewLine
				RichText.Text = RichText.Text & vbNewLine
			Loop
			myReader.NextResult()
		Loop

		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the Current type of the System.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="type">OUTPUT: The current status of the system</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the status. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetSystemCurrentTypeByID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef systemID As String,
										 ByRef type As String, ByRef result As String) As Boolean
		Dim systemTypeGUID As String = ""

		'First grab the GUID of the System Type GUID associated with the system.
		myCmd.CommandText = "SELECT [dbo.SystemType.id] FROM dbo.System WHERE systemid = '" & systemID & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetSystemCurrentType1] System Type for '" & systemID & "' is NULL."
				myReader.Close()
				Return False
			Else
				systemTypeGUID = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemCurrentType2] System serial number '" & systemID & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		'Second grab the System type for the system with the GIUD we got from the first step.
		myCmd.CommandText = "SELECT name FROM dbo.SystemType WHERE id = '" & systemTypeGUID & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				type = ""
				myReader.Close()
				Return False
			Else
				type = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemCurrentType3] System Type does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets the System GUID of the passed in serial number. Closes the passed reader before exit.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="systemSerialNumber">The system serial number that we are checking for.</param>
	''' <param name="record">OUTPUT: GUID variable that will hold the returned GUID.</param>
	''' <param name="result">OUTPUT: Error report to show us what went wrong.</param>
	''' <returns>True: System exists and record is returned. False: Either the System is NULL or does not exist, see results.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetSystemGUID(ByRef myCmd As SqlCommand, ByRef systemSerialNumber As String, ByRef record As Guid,
								  ByRef result As String) As Boolean
		Dim myReader As SqlDataReader
		myCmd.CommandText = "SELECT systemid FROM dbo.System WHERE SerialNumber = '" & systemSerialNumber & "' and 
[dbo.SystemStatus.id] != (Select id from SystemStatus where name = 'Scrapped')"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetSystemGUID1] System serial number '" & systemSerialNumber & "' is NULL."
				myReader.Close()
				Return False
			Else
				record = myReader.GetGuid(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemGUID2] System serial number '" & systemSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the system status from the SQL reader that is passed throguh. Closes the passed reader before exit.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="status">Status that we are looking for.</param>
	''' <param name="systemStatus">OUTPUT: String that will hold the GUID of the status.</param>
	''' <param name="result">OUTPUT: Error message that will give us some insight as to what went wrong.</param>
	''' <returns>True: Everything worked out, returns our systemStatus. False: Somethign went wrong, see result for insight.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetSystemStatus(ByRef myCmd As SqlCommand, ByRef status As String, ByRef systemStatus As String,
									ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT id FROM dbo.SystemStatus WHERE name = '" & status & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetSystemStatus1] System status name '" & status & "' is NULL."
				myReader.Close()
				Return False
			Else
				systemStatus = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemStatus2] System status name '" & status & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets a passed date field associated with the passed system serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="systemID">The system serial number that we are working with.</param>
	''' <param name="dateFeild">The Date field that we are working with.</param>
	''' <param name="dateInformation">OUTPUT: The date for the date field.</param>
	''' <param name="record">OUTPUT: The ID associated with the board.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the date associated with the date field. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetSystemDateByID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef systemID As String,
							ByRef dateFeild As String, ByRef dateInformation As DateTime, ByRef record As Guid, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT " & dateFeild & " FROM dbo.System WHERE systemid = '" & systemID & "'"
		myReader = myCmd.ExecuteReader()
		If myReader.Read() Then
			'Check to see if the Reader is empty/NULL or not.
			If myReader.IsDBNull(0) Then
				dateInformation = Nothing
			Else
				'If not, set our information to whatever was passed back to us.
				dateInformation = myReader.GetDateTime(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetDate1] Date '" & dateFeild & "'/systemid '" & systemID & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the Bootloader version.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The system serial number that we are working with.</param>
	''' <param name="version">OUTPUT: The version of the Bootloader.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the version. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardBootloaderVersion(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String,
											  ByRef version As String, ByRef result As String) As Boolean
		'Grab the Version for the board.
		myCmd.CommandText = "SELECT BootloaderVersion FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				version = ""
				myReader.Close()
			Else
				version = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetSystemVersion1] Bootloader Version for '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets all of the board audit records associated with teh passed board serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The board serial number that we are working with.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the date associated with the date field. False: The record does not exists, see result for details.</returns>
	''' <remarks></remarks>
	Public Function GetBoardAudit(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String, ByRef RichText As RichTextBox, ByRef result As String) As Boolean
		Dim record As String = ""

		'First we have to get the System GUID that we are working with.
		If GetBoardGUIDBySerialNumber(myCmd, myReader, boardSerialNumber, record, result) = False Then
			Return False
		End If

		'Next we grab the information we want form the Audit table.
		myCmd.CommandText = "SELECT LastUpdate, [User], Comment FROM dbo.BoardAudit WHERE [dbo.Board.boardid] = '" & record.ToString() & "' ORDER BY LastUpdate DESC"
		myReader = myCmd.ExecuteReader()

		Do While myReader.HasRows

			Do While myReader.Read()
				RichText.Text = RichText.Text & myReader.GetDateTime(0).ToString & " | " & myReader.GetString(1) & vbNewLine
				RichText.Text = RichText.Text & myReader.GetString(2) & vbNewLine
				RichText.Text = RichText.Text & vbNewLine
			Loop
			myReader.NextResult()
		Loop

		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the board GUID associated with the passed Serial Number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="serialNumber">The serial number associated with the board GUID.</param>
	''' <param name="boardGUID">OUTPUT: The GUID that we are working with.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the Board Serial Number. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardGUIDBySerialNumber(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef serialNumber As String,
											   ByRef boardGUID As String, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT boardid FROM dbo.Board WHERE SerialNumber = '" & serialNumber & "'"
		myReader = myCmd.ExecuteReader()
		If myReader.Read() Then
			'Check to see if the Reader is empty/NULL or not.
			If myReader.IsDBNull(0) Then
				boardGUID = ""
			Else
				'If not, set our string to whatever was passed back to us.
				boardGUID = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardGUIDBySerialNumber1] Board GUID '" & serialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the last update field associated with the passed board serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The board serial number that we are working with.</param>
	''' <param name="dateInformation">OUTPUT: The date for the date field.</param>
	''' <param name="record">OUTPUT: The ID associated with the board.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the date associated with the date field. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardLastUpdate(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String,
									   ByRef dateInformation As String, ByRef record As Guid, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT LastUpdate FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()
		If myReader.Read() Then
			'Check to see if the Reader is empty/NULL or not.
			If myReader.IsDBNull(0) Then
				dateInformation = ""
			Else
				'If not, set our information to whatever was passed back to us.
				dateInformation = myReader.GetDateTime(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardDate1] BoardNumber '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Gets the Current type of the Board.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The board serial number that we are working with.</param>
	''' <param name="record">OUTPUT: The ID associated with the board.</param>
	''' <param name="type">OUTPUT: The current status of the system</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the status. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardCurrentType(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String,
										   ByRef record As Guid, ByRef type As String, ByRef result As String) As Boolean
		Dim boardTypeGUID As String = ""

		'First grab the GUID of the Board Type GUID associated with the board.
		myCmd.CommandText = "SELECT [dbo.BoardType.id] FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetBoardCurrentType1] board Type for '" & boardSerialNumber & "' is NULL."
				myReader.Close()
				Return False
			Else
				boardTypeGUID = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardCurrentType2] Board serial number '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		'Second grab the Type for the board with the GIUD we got from the first step.
		myCmd.CommandText = "SELECT name FROM dbo.BoardType WHERE id = '" & boardTypeGUID & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				type = ""
				myReader.Close()
				Return False
			Else
				type = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardCurrentType3] Board Type does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets the Current status of the Board.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The board serial number that we are working with.</param>
	''' <param name="stauts">OUTPUT: The current status of the system</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the status. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardCurrentStatus(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String,
										  ByRef stauts As String, ByRef result As String) As Boolean
		Dim systemStatusGUID As String = ""

		'First grab the GUID of the CPU Serial Board associated with the system.
		myCmd.CommandText = "SELECT [dbo.BoardStatus.id] FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetBoardCurrentStatus1] System Status for '" & boardSerialNumber & "' is NULL."
				myReader.Close()
				Return False
			Else
				systemStatusGUID = myReader.GetGuid(0).ToString
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardCurrentStatus2] System serial number '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		'Second grab the Version for the board with the GIUD we got from the first step.
		myCmd.CommandText = "SELECT name FROM dbo.BoardStatus WHERE id = '" & systemStatusGUID & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				stauts = ""
				myReader.Close()
				Return False
			Else
				stauts = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardCurrentStatus3] System Status does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets the Hardware Version of the passed in board Serial Number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The system serial number that we are working with.</param>
	''' <param name="version">OUTPUT: The version of the board.</param>
	''' <param name="result">OUTPUT: Error result when things do not work.</param>
	''' <returns>True: The record exists, returns the version. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetBoardHardwareVersion(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String,
											ByRef version As String, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT HardwareVersion FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				version = ""
				myReader.Close()
			Else
				version = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardHardwareVersion1] Board Version for '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()

		Return True
	End Function

	''' <summary>
	''' Gets the CPUID associated with the passed in Board serial number.
	''' </summary>
	''' <param name="myCmd">The sql Command that will be used.</param>
	''' <param name="myReader">SQL data reader that we are going to read from.</param>
	''' <param name="boardSerialNumber">The board serial number that we are checking for.</param>
	''' <param name="CPUID">OUTPUT: The MAC Address that is associated with the System serial number.</param>
	''' <param name="result">OUTPUT: Error message that gives us a hint on what went wrong.</param>
	''' <returns>True: The record exists, returns MAC Address. False: The record does not exists, see result for details.</returns>
	''' <remarks>Make sure the SQL reader that is being passed through is already set to the SQL command reader before calling this function.</remarks>
	Public Function GetCPUID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef boardSerialNumber As String, ByRef CPUID As String,
								  ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT CPUID FROM dbo.Board WHERE SerialNumber = '" & boardSerialNumber & "'"
		myReader = myCmd.ExecuteReader()

		If myReader.Read() Then
			'Check to see if we are returned a NULL value.
			If myReader.IsDBNull(0) Then
				result = "[GetCPUID1] CPUID for '" & boardSerialNumber & "' is NULL."
				myReader.Close()
				CPUID = ""
				Return False
			Else
				CPUID = myReader.GetString(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetCPUID2] System serial number '" & boardSerialNumber & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	Public Function GetBoardGUIDBySystemID(ByRef myCmd As SqlCommand, ByRef myReader As SqlDataReader, ByRef systemID As String,
													 ByRef board As String, ByRef record As Guid, ByRef result As String) As Boolean
		myCmd.CommandText = "SELECT [" & board & ".boardid] FROM dbo.System WHERE systemid = '" & systemID & "'"
		myReader = myCmd.ExecuteReader()
		If myReader.Read() Then
			'Check to see if the Reader is empty/NULL or not.
			If myReader.IsDBNull(0) Then
				record = Guid.Empty
			Else
				'If not, set our record GUID to whatever was passed back to us.
				record = myReader.GetGuid(0)
			End If
		Else
			'If nothing is returned then it does not exist.
			result = "[GetBoardGUIDBySystemSerialNumber1] Board '" & board & "'/SerialNumber '" & systemID & "' does not exist."
			myReader.Close()
			Return False
		End If
		myReader.Close()
		Return True
	End Function

	''' <summary>
	''' Roll back the transaction so we do not commit anything into the database.
	''' </summary>
	''' <param name="transaction">The transaction that we want to roll back.</param>
	''' <param name="result">OUTPUT: If there is an issue trying to roll back the transaction.</param>
	''' <remarks></remarks>
	Private Sub RollBack(ByRef transaction As SqlTransaction, ByRef result As String)
		Try
			'Attempt to roll back the transaction. 
			transaction.Rollback()
		Catch ex As Exception
			'Handles any errors that may have occurred on the server that would cause the rollback to fail, such as a closed connection.
			result = result & " :: " & ex.Message
		End Try
	End Sub

#Region "Search Functions with DGVs"

	Public Sub FirstPage(ByRef scrollValue As Integer, ByRef ds As DataSet, ByRef da As SqlDataAdapter, ByRef entriesToShow As Integer)
		scrollValue = 0
		ds.Clear()
		da.Fill(ds, scrollValue, entriesToShow, "Table")
	End Sub

	Public Sub PreviousPage(ByRef scrollValue As Integer, ByRef entriesToShow As Integer, ByRef ds As DataSet, ByRef da As SqlDataAdapter,
							ByRef firstButton As Button, ByRef previousButton As Button)
		scrollValue = scrollValue - entriesToShow
		If scrollValue <= 0 Then
			scrollValue = 0
			firstButton.Enabled = False
			previousButton.Enabled = False
		End If
		ds.Clear()
		da.Fill(ds, scrollValue, entriesToShow, "TABLE")
	End Sub

	Public Sub NextPage(ByRef scrollValue As Integer, ByRef entriesToShow As Integer, ByRef numberOfRecords As Integer, ByRef ds As DataSet,
						ByRef da As SqlDataAdapter, ByRef nextButton As Button, ByRef lastButton As Button)
		scrollValue = scrollValue + entriesToShow
		If scrollValue > numberOfRecords Then
			scrollValue = numberOfRecords - entriesToShow
			If scrollValue < 0 Then
				scrollValue = 0
			End If
			nextButton.Enabled = False
			lastButton.Enabled = False
		End If
		ds.Clear()
		da.Fill(ds, scrollValue, entriesToShow, "TABLE")

		'Check to see if we can keep scrolling or if we are at the end of the table.
		If scrollValue + entriesToShow >= numberOfRecords Then
			nextButton.Enabled = False
			lastButton.Enabled = False
		End If
	End Sub

	Public Sub LastPage(ByRef scrollValue As Integer, ByRef entriesToShow As Integer, ByRef numberOfRecords As Integer, ByRef ds As DataSet, ByRef da As SqlDataAdapter)
		scrollValue = numberOfRecords - entriesToShow
		ds.Clear()
		da.Fill(ds, scrollValue, entriesToShow, "TABLE")
	End Sub

	Public Sub Sort(ByRef command As String, ByRef myCmd As SqlCommand, ByRef ds As DataSet, ByRef da As SqlDataAdapter)
		Try
			myCmd.CommandText = command
			da = New SqlDataAdapter(myCmd)
			ds = New DataSet()
		Catch ex As Exception
			MsgBox(ex.Message)
		End Try
	End Sub

	Public Sub RetriveData(ByRef freeze As Integer, ByRef da As SqlDataAdapter, ByRef ds As DataSet, ByRef dgv As DataGridView, ByRef scrollValue As Integer, ByRef entriesToShow As Integer,
						   ByRef numberOfRecords As Integer, ByRef nextButton As Button, ByRef lastButton As Button, ByRef firstButton As Button, ByRef previousButton As Button)
		scrollValue = 0
		da.Fill(ds, scrollValue, entriesToShow, "TABLE")

		ds.Tables(0).Columns.Add(DB_HEADER_CODE)

		numberOfRecords = ds.Tables(0).Rows.Count

		Dim mycmd As New SqlCommand("", myConn)

		For Each row As DataRow In ds.Tables(0).Rows
			Dim formGUID As String

			mycmd.CommandText = "SELECT [" & DB_HEADER_ID & "] FROM " & TABLE_RMA & " WHERE [" & DB_HEADER_SERVICEFORM & "] = '" & row(DB_HEADER_SERVICEFORM).ToString & "'"
			formGUID = mycmd.ExecuteScalar.ToString

			mycmd.CommandText = "SELECT * FROM " & TABLE_RMACODELIST & " WHERE [" & DB_HEADER_RMAID & "] = '" & formGUID & "' AND [" & DB_HEADER_EVALUATION & "] = '1' ORDER BY [" & DB_HEADER_RMACODESID & "]"
			Dim dt_searchresults As New DataTable
			dt_searchresults.Load(mycmd.ExecuteReader)

			For Each reusltrow As DataRow In dt_searchresults.Rows
				mycmd.CommandText = "SELECT [" & DB_HEADER_CODE & "] FROM " & TABLE_RMACODES & " WHERE [" & DB_HEADER_ID & "] = '" & reusltrow(DB_HEADER_RMACODESID).ToString & "'"
				If row(DB_HEADER_CODE).ToString.Contains(mycmd.ExecuteScalar.ToString) = False Then
					row(DB_HEADER_CODE) = row(DB_HEADER_CODE) & mycmd.ExecuteScalar.ToString & ", "
				End If
			Next

			If row(DB_HEADER_CODE).ToString.Length <> 0 Then
				row(DB_HEADER_CODE) = row(DB_HEADER_CODE).ToString.Substring(0, row(DB_HEADER_CODE).ToString.Length - 2)
			End If
		Next

		ds.Tables(0).Columns(DB_HEADER_CODE).SetOrdinal(3)

		If numberOfRecords <= entriesToShow Then
			nextButton.Enabled = False
			lastButton.Enabled = False
		Else
			nextButton.Enabled = True
			lastButton.Enabled = True
		End If
		previousButton.Enabled = False
		firstButton.Enabled = False

		dgv.DataSource = Nothing
		dgv.DataSource = ds.Tables("TABLE")
		dgv.AutoResizeColumns(DataGridViewAutoSizeColumnMode.AllCells)

		dgv.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
		dgv.Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
		dgv.Columns(3).Width = 300

		For i = 0 To dgv.Columns.Count - 1
			dgv.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
		Next i

		dgv.Columns(freeze).Frozen = True
	End Sub

#End Region

End Class