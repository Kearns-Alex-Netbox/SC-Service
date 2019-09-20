'-----------------------------------------------------------------------------------------------------------------------------------------
' Module: Variables.vb
'
' Description: Contains all of the common variables that are used in the entire project more than once as well as some debugging tools.
'
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'
' Module: Login.vb
'
' Description: This is the opening window of the program. The user must have a valid user name and password to log in.
'
' Special Keys:
'   enter = enters the user name and password and trys to log the user in.
'-----------------------------------------------------------------------------------------------------------------------------------------
Imports System.Data.SqlClient

Public Module Variables
	Public UserName As String = ""
	Public Password As String = ""

	Public		 Databases()			   As	  String  = { "Production",
															  "Devel", 
															  "BlueSkyProduction", 
															  "BlueSkyDevel" }

	Public Const NB_PRODUCTION			   As	  Integer = 0
	Public Const NB_DEVEL				   As	  Integer = 1
	Public Const BSG_PRODUCTION			   As	  Integer = 2
	Public Const BSG_DEVEL				   As	  Integer = 3

	public		 CurrentIndex			   As	  Integer = NB_PRODUCTION
	Public		 CurrentDatabase		   As	  String  = Databases(CurrentIndex)

	Public myConn As SqlConnection
	Public sqlapi As New SQL_API()

	Public WorkingServiceForm As Boolean = False

	Public runlogDownloadList() As String = {"startlog.txt",
											 "weblog.txt",
											 "smtpdiag.log",
											 "ditpdg0.log",
											 "boxstats.log",
											 "boxstat0.log",
											 "emailmsg.txt",
											 "emailmsg0.txt",
											 "mberror.log",
											 "mberror0.log",
											 "mbaccess.log",
											 "mbaccess0.log",
											 "error.log",
											 "error0.log",
											 "access.log",
											 "access0.log",
											 "sysacces.csv",
											 "sysaces0.csv",
											 "jscript.log",
											 "jscript0.log",
											 "runlog.txt",
											 "nbdiag.txt"}

	Public alarmDownloadList() As String = {}

	Public dataDownloadList() As String = {}

	'Board Status'
	Public Const BS_BOARD_CHECKED As String = "Board Checked"
	Public Const BS_BOARD_REGISTERED As String = "Board Registered"
	Public Const BS_NETWORK_REGISTERED As String = "Network Registered"
	Public Const BS_BURN_IN As String = "Burn In"
	Public Const BS_QC_CHECKOUT As String = "QC Checkout"
	Public Const BS_SHIPPED As String = "Ship"
	Public Const BS_REWORK As String = "Rework"

	'System Status'
	Public Const SS_BOARDS_REGISTERED As String = "Boards Registered"
	Public Const SS_NETWORK_REGISTERED As String = "Network Registered"
	Public Const SS_SET_PARAMETERS As String = "Set Parameters"
	Public Const SS_BURN_IN As String = "Burn In"
	Public Const SS_QC_CHECKOUT As String = "QC Checkout"

	'System Dates
	Public Const BARCODE_DATE As String = "BarcodeDate"
	Public Const REGISTER_DATE As String = "RegisterDate"
	Public Const PARAMETER_DATE As String = "ParameterDate"
	Public Const BURN_IN_DATE As String = "BurnInDate"
	Public Const CHECKOUT_DATE As String = "CheckoutDate"
	Public Const SHIP_DATE As String = "ShipDate"
	Public Const LAST_UPDATE As String = "LastUpdate"

	'Table names
	Public Const TABLE_PARAMETERS As String = "SystemParameters"
	Public Const TABLE_SYSTEM As String = "System"
	Public Const TABLE_SYSTEMTYPE As String = "SystemType"
	Public Const TABLE_BOARD As String = "Board"
	Public Const TABLE_RMA As String = "RMA"
	Public Const TABLE_RMAADDRESSES As String = "RMAAddresses"
	Public Const TABLE_RMAAUDIT As String = "RMAAudit"
	Public Const TABLE_RMABILLTYPE As String = "RMABillType"
	Public Const TABLE_RMACODES As String = "RMACodes"
	Public Const TABLE_RMAREPAIRITEMS As String = "RMARepairItems"
	Public Const TABLE_RMASTATUS As String = "RMAStatus"
	Public Const TABLE_RMACODELIST As String = "RMACodeList"

#Region "Database Headers"
	'Table Headers from RMAs
	Public Const DB_HEADER_ID As String = "id"                                  ' RMA | RMAAddresses |          | RMABillType |          |                | RMAStatus |             |
	Public Const DB_HEADER_CUSTOMER As String = "Customer"                      ' RMA | RMAAddresses |          |             |          |                |           | RMACodeList |
	Public Const DB_HEADER_SERIAL_NUMBER As String = "Serial Number"            ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_STATUSID As String = "Status.id"                     ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_CONTACTNAME As String = "Contact Name"               ' RMA | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_CONTACTPHONE As String = "Contact Phone"             ' RMA | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_CONTACTEMAIL As String = "Contact E-mail"            ' RMA | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_C_RGA As String = "Customer RGA"                     ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_C_INVOICE As String = "Customer Invoice"             ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_SERVICEFORM As String = "Service Form"               ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_DESCRIPTION As String = "Description"                ' RMA |              |          |             | RMACodes | RMARepairItems |           |             |
	Public Const DB_HEADER_TECHNICIAN As String = "Technician"                  ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_SOFTWAREVERSION As String = "Software Version"       ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_IOVERSION As String = "IO Version"                   ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_BOOTVERSION As String = "Boot Version"               ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_SHIPID As String = "Ship.id"                         ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_TECHNICIANNOTES As String = "Technician Notes"       ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_APPROVALNOTES As String = "Approval Notes"           ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_BILLTYPEID As String = "BillType.id"                 ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_BILLID As String = "Bill.id"                         ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_C_RMAPO As String = "Customer RMA PO"                ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_INFORMATIONDATE As String = "Information Date"       ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_RECEIVEDDATE As String = "Received Date"             ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_EVALUATIONDATE As String = "Evaluation Date"         ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_SHIPDATE As String = "Ship Date"                     ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_APPROVALDATE As String = "Approval Date"             ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_LASTUPDATE As String = "Last Update"                 ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_TESTED As String = "Tested"                          ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_NB_INVOICE As String = "Netbox Invoice"              ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_INSTANCE As String = "Instance"                      ' RMA |              |          |             |          |                |           |             |
	Public Const DB_HEADER_COMPANY As String = "Company"                        '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_ADDRESS As String = "Address"                        '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_CITY As String = "City"                              '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_STATE As String = "State"                            '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_ZIPCODE As String = "Zip Code"                       '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_COUNTRY As String = "Country"                        '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_PHONE As String = "Phone"                            '     | RMAAddresses |          |             |          |                |           |             |
	Public Const DB_HEADER_BILLTYPE As String = "Bill Type"                     '     |              |          | RMABillType |          |                |           |             |
	Public Const DB_HEADER_NEEDSADDRESS As String = "Needs Address"             '     |              |          | RMABillType |          |                |           |             |
	Public Const DB_HEADER_SAMEASSHIPPING As String = "Same as Shipping"        '     |              |          | RMABillType |          |                |           |             |
	Public Const DB_HEADER_CODE As String = "Code"                              '     |              |          |             | RMACodes |                |           |             |
	Public Const DB_HEADER_TYPE As String = "Type"                              '     |              |          |             | RMACodes |                |           |             |
	Public Const DB_HEADER_FIX As String = "Fix"                                '     |              |          |             | RMACodes |                |           |             |
	Public Const DB_HEADER_RMAID As String = "RMA.id"                           '     |              |          |             |          | RMARepairItems |           | RMACodeList |
	Public Const DB_HEADER_CHARGE As String = "Charge"                          '     |              |          |             |          | RMARepairItems |           |             |
	Public Const DB_HEADER_ISAPPROVAL As String = "Is Approval"                 '     |              |          |             |          |                | RMAStatus |             |
	Public Const DB_HEADER_STATUS As String = "Status"                          '     |              |          |             |          |                | RMAStatus |             |
	Public Const DB_HEADER_ORDER As String = "Order"							'     |              |          |             |          |                | RMAStatus |             |
	Public Const DB_HEADER_RMACODESID As String = "RMACodes.id"                 '     |              |          |             |          |                |           | RMACodeList |
	Public Const DB_HEADER_EVALUATION As String = "Evaluation"                  '     |              |          |             |          |                |           | RMACodeList |


	'Table headers from Production
	Public Const DB_HEADER_VALUESTRING As String = "valuestring"
	Public Const DB_HEADER_SERIALNUMBER As String = "SerialNumber"
	Public Const DB_HEADER_SYSTEMTYPEGUID As String = "dbo.SystemType.id"
	Public Const DB_HEADER_NAME As String = "name"

	'Table headers
	Public Const HEADER_SYSTEMTYPE As String = "System Type"
#End Region

End Module

Public Class Login
	Dim sqlapi As New SQL_API()
	Dim myCmd As SqlCommand
	Dim loginName As String = ""

	Private Sub Login_Load() Handles MyBase.Load
		'Set up the version and database type for our labels.
		L_Version.Text = "V:" & Application.ProductVersion
		
		' find out what database we are going to connect to
		Select Case CurrentDatabase
			Case Databases(NB_PRODUCTION)
				L_Database.Text = "NB"

			Case Databases(NB_DEVEL)
				L_Database.Text = "NB Devel"

			Case Databases(BSG_PRODUCTION)
				L_Database.Text = "BSG"

			Case Databases(BSG_DEVEL)
				L_Database.Text = "BSG Devel"

			Case Else
				L_Database.Text = "NOT KNOWN"

		End Select

		KeyPreview = True
	End Sub

	Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
		If e.KeyCode.Equals(Keys.Enter) Then
			Call B_Login_Click()
		End If
	End Sub

	Private Sub B_Login_Click() Handles B_Login.Click
		UserName = TB_User.Text
		Password = TB_Password.Text

		sqlapi._Username = UserName
		sqlapi._Password = Password
		Dim result As String = ""

		'Attempt to open a database connection.
		If sqlapi.OpenDatabase(myConn, myCmd, loginName, result) = False Then
			MsgBox(result)
			Return
		End If

		Dim DoMainForm As New MenuMain
		DoMainForm.Show()
		Close()
	End Sub

	Private Sub B_Exit_Click() Handles B_Exit.Click
		Application.Exit()
	End Sub

End Class