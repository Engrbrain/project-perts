Imports System.Data
Imports System
Imports System.Data.SqlClient
Public Class Utilities
    Public Function GenerateRefNo() As String
        'Function to generate a unique number
        Dim Pin As String = String.Empty
        Dim InstantTimeTicks As Integer
        Dim InstantTimeSeconds As Integer
        InstantTimeTicks = CInt(Left((Right((System.DateTime.Now.Ticks.ToString), 7)), 4))
        InstantTimeSeconds = CInt(Left((Right((System.Environment.TickCount.ToString), 7)), 4))

        Dim intSeed As Integer = CInt(Right((InstantTimeTicks * InstantTimeSeconds), 7))

        Dim randObj As New Random(intSeed)
        Try
            Dim lower As Long = 1000001
            Dim upper As Long = 9999999
            Pin = Right(CStr(randObj.Next(lower, upper)), 3) + Left(CStr(randObj.Next(lower, upper)), 2) + Left(CStr(randObj.Next(lower, upper)), 2) + CStr(randObj.Next(lower, upper)) ' + ControlChars.Tab + CStr(pinvalue)

        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
        End Try
        Return Pin
    End Function
    Public Function GenerateTranxCodeNo() As String
        'Function to generate a unique number
        Dim Pin As String = String.Empty
        Dim InstantTimeTicks As Integer
        Dim InstantTimeSeconds As Integer
        InstantTimeTicks = CInt(Left((Right((System.DateTime.Now.Ticks.ToString), 7)), 4))
        InstantTimeSeconds = CInt(Left((Right((System.Environment.TickCount.ToString), 7)), 4))

        Dim intSeed As Integer = CInt(Right((InstantTimeTicks * InstantTimeSeconds), 7))

        Dim randObj As New Random(intSeed)
        Try
            Dim lower As Long = 1000001
            Dim upper As Long = 9999999
            Pin = Left(CStr(randObj.Next(lower, upper)), 2) + Left(CStr(randObj.Next(lower, upper)), 7) + Right(CStr(randObj.Next(lower, upper)), 2) + Left(CStr(randObj.Next(lower, upper)), 3) ' + ControlChars.Tab + CStr(pinvalue)

        Catch ex As Exception
            'AppException.LogError(ex.Message, ex.StackTrace.ToString)
        End Try
        Return Pin
    End Function
    Public Function ReturnMyPinString(ByVal refNumber As String, ByVal RefID As Integer) As String
        'For Uniqueness sake, insert into a table get the ID of the table for this applicant
        'Add the ID to the Reference No Generate to make the reference no unique fr this guy
        Try

            Dim Reference_Code As String = String.Empty
            If RefID > 0 Then
                Dim lnRefID As Integer = Len(CStr(RefID))
                Reference_Code = Left(refNumber, Len(refNumber) - lnRefID)

                Reference_Code = Trim(Reference_Code & CStr(RefID))
            Else
                Dim sec As Integer = System.DateTime.Now.ToString("ss")
                Dim tik As Integer = Left(System.Environment.TickCount, 3)
                tik = Trim(tik & sec)
                Reference_Code = Left(refNumber, (Len(refNumber) - Len(tik)))
                Reference_Code = Trim(Reference_Code & CStr(tik))
            End If

            Return Reference_Code
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Function generatedApplicantRef() As String
        Dim Pin As String = String.Empty
        Dim InstantTimeTicks As Integer
        Dim InstantTimeSeconds As Integer
        InstantTimeTicks = CInt(Left((Right((System.DateTime.Now.Ticks.ToString), 7)), 4))
        InstantTimeSeconds = CInt(Left((Right((System.Environment.TickCount.ToString), 7)), 4))

        Dim intSeed As Integer = CInt(Right((InstantTimeTicks * InstantTimeSeconds), 7))

        Dim randObj As New Random(intSeed)

        Try
            Dim lower As Long = 10001
            Dim upper As Long = 9999999
            Pin = Right(CStr(randObj.Next(lower, upper)), 3) + Left(CStr(randObj.Next(lower, upper)), 2) + Left(CStr(randObj.Next(lower, upper)), 2) + CStr(randObj.Next(lower, upper)) ' + ControlChars.Tab + CStr(pinvalue)

        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
        End Try

        Return Pin

    End Function
    Public Function generatedApplicantTransCode() As String
        Dim Pin As String = String.Empty
        Dim InstantTimeTicks As Integer
        Dim InstantTimeSeconds As Integer
        InstantTimeTicks = CInt(Left((Right((System.DateTime.Now.Ticks.ToString), 6)), 3))
        InstantTimeSeconds = CInt(Left((Right((System.Environment.TickCount.ToString), 6)), 3))

        Dim intSeed As Integer = CInt(Right((InstantTimeTicks * InstantTimeSeconds), 6))

        Dim randObj As New Random(intSeed)

        Try
            Dim lower As Long = 10001
            Dim upper As Long = 9999999
            Pin = Right(CStr(randObj.Next(lower, upper)), 3) + Left(CStr(randObj.Next(lower, upper)), 2) + Left(CStr(randObj.Next(lower, upper)), 2) + CStr(randObj.Next(lower, upper)) ' + ControlChars.Tab + CStr(pinvalue)

        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
        End Try

        Return Pin
    End Function

    Public Function GenerateApplicationNo() As String
        'Function to generate a unique number
        Dim Pin As String = String.Empty
        Dim InstantTimeTicks As Integer
        Dim InstantTimeSeconds As Integer
        InstantTimeTicks = CInt(Left((Right((System.DateTime.Now.Ticks.ToString), 5)), 1))
        InstantTimeSeconds = CInt(Left((Right((System.Environment.TickCount.ToString), 5)), 1))

        Dim intSeed As Integer = CInt(Right((InstantTimeTicks * InstantTimeSeconds), 5))

        Dim randObj As New Random(intSeed)

        Try
            Dim lower As Long = 10001
            Dim upper As Long = 9999999
            Pin = Right(CStr(randObj.Next(lower, upper)), 3) + Left(CStr(randObj.Next(lower, upper)), 2) + Left(CStr(randObj.Next(lower, upper)), 2) + CStr(randObj.Next(lower, upper)) ' + ControlChars.Tab + CStr(pinvalue)

        Catch ex As Exception
            ' navywebDAL.AppException.LogError(ex.Message, ex.StackTrace.ToString)
        End Try

        Return Pin

    End Function

    Public Shared Function GetUserMesaage(ByVal strMsg As String) As String
        GetUserMesaage = strMsg
        Return GetUserMesaage
    End Function

    Public Shared DisplayMessages As String
    Public Shared StatusFlag As Integer
    Public Shared kount As Integer
    Public Shared kount2 As Integer

#Region "Kayode Class"
    Public Class StatusReponse
        Private _ID As Integer = 0
        Private _cardserial As String = String.Empty
        Private _cardnumber As String = String.Empty
        Private _cardexpiry As String = String.Empty
        Private _cardpin As String = String.Empty
        Private _usedate As String = String.Empty
        Private _usestatus As String = String.Empty
        Private _batchnumber As String = String.Empty
        Private _audit As String = String.Empty
        Private _isvisible As String = String.Empty
        Private _cardvalue As String = String.Empty
        Private _tranxcode As String = String.Empty
        Private _walletid As String = String.Empty
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Public Property Cardserial() As String
            Get
                Return _cardserial
            End Get
            Set(ByVal value As String)
                _cardserial = value
            End Set
        End Property

        Public Property Cardnumber() As String
            Get
                Return _cardnumber
            End Get
            Set(ByVal value As String)
                _cardnumber = value
            End Set
        End Property

        Public Property Cardpin() As String
            Get
                Return _cardpin
            End Get
            Set(ByVal value As String)
                _cardpin = value
            End Set
        End Property
        Public Property Cardexpiry() As String
            Get
                Return _cardexpiry
            End Get
            Set(ByVal value As String)
                _cardexpiry = value
            End Set
        End Property
        Public Property Usedate() As String
            Get
                Return _usedate
            End Get
            Set(ByVal value As String)
                _usedate = value
            End Set
        End Property
        Public Property Usestatus() As String
            Get
                Return _usestatus
            End Get
            Set(ByVal value As String)
                _usestatus = value
            End Set
        End Property
        Public Property Batchnumber() As String
            Get
                Return _batchnumber
            End Get
            Set(ByVal value As String)
                _batchnumber = value
            End Set
        End Property
        Public Property Audit() As String
            Get
                Return _audit
            End Get
            Set(ByVal value As String)
                _audit = value
            End Set
        End Property
        Public Property Isvisible() As String
            Get
                Return _isvisible
            End Get
            Set(ByVal value As String)
                _isvisible = value
            End Set
        End Property
        Public Property Cardvalue() As String
            Get
                Return _cardvalue
            End Get
            Set(ByVal value As String)
                _cardvalue = value
            End Set
        End Property
        Public Property Tranxcode() As String
            Get
                Return _tranxcode
            End Get
            Set(ByVal value As String)
                _tranxcode = value
            End Set
        End Property
        Public Property Walletid() As String
            Get
                Return _walletid
            End Get
            Set(ByVal value As String)
                _walletid = value
            End Set
        End Property

    End Class
    Public Sub New()
    End Sub
#End Region
    Public Shared Function GetSwitchErrorString(ByVal success As String, ByVal SwitchType As Integer) As String
        Dim describ As String = String.Empty
        Try
            If success = "" Then
                Return "Connection timeout, Please try again later"
            End If
            If SwitchType = 1 Then
                If success = -1 Then
                    describ = "Invalid Parameters or Transaction Timeout"
                ElseIf success = 0 Then
                    describ = "Transaction Successful"
                ElseIf success = 1 Then
                    describ = "Destination Card not found"
                ElseIf success = 2 Then
                    describ = "Card Number not found"
                ElseIf success = 3 Then
                    describ = "Invalid Card pin"
                ElseIf success = 4 Then
                    describ = "Card Expiration not correct"
                ElseIf success = 5 Then
                    describ = "Insufficient Balance"
                ElseIf success = 6 Then
                    describ = "Spending Limit Exceeded"
                ElseIf success = 7 Then
                    describ = "Internal System Error Occured"
                ElseIf success = 8 Then
                    describ = "Financial Institution cannot be authorize transaction"
                ElseIf success = 9 Then
                    describ = "Pin trials Exceeded"
                ElseIf success = 10 Then
                    describ = "Card has been locked"
                ElseIf success = 11 Then
                    describ = "Card has Expired"
                ElseIf success = 12 Then
                    describ = "Payment Time out"
                ElseIf success = 13 Then
                    describ = "Destination Card has been locked"
                ElseIf success = 14 Then
                    describ = "Card has Expired"
                ElseIf success = 15 Then
                    describ = "Pin changed Required"
                ElseIf success = 16 Then
                    describ = "Invalid Amount"
                ElseIf success = 17 Then
                    describ = "Card has been disabled"
                ElseIf success = 18 Then
                    describ = "Unable to credit this account immediately,credit will be done later"
                ElseIf success = 19 Then
                    describ = "Transaction not permitted on terminal"
                ElseIf success = 20 Then
                    describ = "Exceeded withdrawer frequency"
                ElseIf success = 21 Then
                    describ = "Destination Card has expired"
                ElseIf success = 22 Then
                    describ = "Destination Card disabled"
                ElseIf success = 23 Then
                    describ = "Source Card disabled"
                ElseIf success = 24 Then
                    describ = "Invalid Bank Account"
                ElseIf success = 25 Then
                    describ = "Insufficient Balance"
                End If

            ElseIf SwitchType = 2 Then
                If success = "00" Then
                    describ = "Approved or completed successfully"
                ElseIf success = "01" Then
                    describ = "Refer to card issuer"
                ElseIf success = "02" Then
                    describ = "Refer to card issuer, special condition"
                ElseIf success = "03" Then
                    describ = "Invalid merchant"
                ElseIf success = "04" Then
                    describ = "Pick-up card"
                ElseIf success = "05" Then
                    describ = "Do not honor"
                ElseIf success = "06" Then
                    describ = "Error"
                ElseIf success = "07" Then
                    describ = "Pick-up card, special condition"
                ElseIf success = "08" Then
                    describ = "Honor with identification"
                ElseIf success = "09" Then
                    describ = "Request in progress"
                ElseIf success = "10" Then
                    describ = "Approved, partial"
                ElseIf success = "11" Then
                    describ = "Approved, VIP"
                ElseIf success = "12" Then
                    describ = "Invalid transaction"
                ElseIf success = "13" Then
                    describ = "Invalid amount"
                ElseIf success = "14" Then
                    describ = "Invalid card number"
                ElseIf success = "15" Then
                    describ = "No such issuer"
                ElseIf success = "16" Then
                    describ = "Approved, update track 3"
                ElseIf success = "17" Then
                    describ = "Customer cancellation"
                ElseIf success = "18" Then
                    describ = "Customer dispute"
                ElseIf success = "19" Then
                    describ = "Re-enter transaction"
                ElseIf success = "20" Then
                    describ = "Invalid response"
                ElseIf success = "21" Then
                    describ = "No action taken"
                ElseIf success = "22" Then
                    describ = "Suspected malfunction"
                ElseIf success = "23" Then
                    describ = "Unacceptable transaction fee"
                ElseIf success = "24" Then
                    describ = "File update not supported"
                ElseIf success = "25" Then
                    describ = "Unable to locate record"
                ElseIf success = "26" Then
                    describ = "Duplicate record"
                ElseIf success = "27" Then
                    describ = "File update field edit error"
                ElseIf success = "28" Then
                    describ = "File update file locked"
                ElseIf success = "29" Then
                    describ = "File update failed"
                ElseIf success = "30" Then
                    describ = "Format error"
                ElseIf success = "31" Then
                    describ = "Bank not supported"
                ElseIf success = "32" Then
                    describ = "Completed partially"
                ElseIf success = "33" Then
                    describ = "Expired card, pick-up"
                ElseIf success = "34" Then
                    describ = "Suspected fraud, pick-up"
                ElseIf success = "35" Then
                    describ = "Contact acquirer, pick-up"
                ElseIf success = "36" Then
                    describ = "Restricted card, pick-up"
                ElseIf success = "37" Then
                    describ = "Call acquirer security, pick-up"
                ElseIf success = "38" Then
                    describ = "PIN tries exceeded, pick-up"
                ElseIf success = "39" Then
                    describ = "No credit account"
                ElseIf success = "40" Then
                    describ = "Function not supported"
                ElseIf success = "41" Then
                    describ = "Lost card, pick-up"
                ElseIf success = "42" Then
                    describ = "No universal account"
                ElseIf success = "43" Then
                    describ = "Stolen card, pick-up"
                ElseIf success = "44" Then
                    describ = "No investment account"
                ElseIf success = "45" Then
                    describ = "Account closed"
                ElseIf success = "46" Then
                    describ = "Identification required"
                ElseIf success = "47" Then
                    describ = "Identification cross-check required"
                ElseIf success = "51" Then
                    describ = "Not sufficient funds"
                ElseIf success = "52" Then
                    describ = "No check account"
                ElseIf success = "53 Then" Then
                    describ = "No savings account"
                ElseIf success = "54" Then
                    describ = "Expired card"
                ElseIf success = "55" Then
                    describ = "Incorrect PIN"
                ElseIf success = "56" Then
                    describ = "No card record"
                ElseIf success = "57" Then
                    describ = "Transaction not permitted to cardholder"
                ElseIf success = "58" Then
                    describ = "Transaction not permitted on terminal"
                ElseIf success = "59" Then
                    describ = "Suspected fraud"
                ElseIf success = "60" Then
                    describ = "Contact acquirer"
                ElseIf success = "61" Then
                    describ = "Exceeds withdrawal limit"
                ElseIf success = "62" Then
                    describ = "Restricted card"
                ElseIf success = "63" Then
                    describ = "Security violation"
                ElseIf success = "64" Then
                    describ = "Original amount incorrect"
                ElseIf success = "65" Then
                    describ = "Exceeds withdrawal frequency"
                ElseIf success = "66" Then
                    describ = "Call acquirer security"
                ElseIf success = "67" Then
                    describ = "Hard capture"
                ElseIf success = "68" Then
                    describ = "Response received too late"
                ElseIf success = "69" Then
                    describ = "Advice received too late"
                ElseIf success = "75" Then
                    describ = "PIN tries exceeded"
                ElseIf success = "76" Then
                    describ = "Reserved for future Postilion use"
                ElseIf success = "77" Then
                    describ = "Intervene, bank approval required"
                ElseIf success = "78" Then
                    describ = "Intervene, bank approval required for partial amount"
                ElseIf success = "90" Then
                    describ = "Cut-off in progress"
                ElseIf success = "91" Then
                    describ = "Issuer or switch inoperative"
                ElseIf success = "92" Then
                    describ = "Routing error"
                ElseIf success = "93" Then
                    describ = "Violation of law"
                ElseIf success = "94" Then
                    describ = "Duplicate transaction"
                ElseIf success = "95" Then
                    describ = "Reconcile error"
                ElseIf success = "96" Then
                    describ = "System malfunction"
                ElseIf success = "97" Then
                    describ = "Reserved for future Postilion use"
                ElseIf success = "98" Then
                    describ = "Exceeds cash limit"
                ElseIf success = "W06" Then
                    describ = "Application Error"
                ElseIf success = "W09" Then
                    describ = "Request In Progress"
                ElseIf success = "W17" Then
                    describ = "Customer Cancellation"
                ElseIf success = "W56" Then
                    describ = "No Transaction Record"
                ElseIf success = "W57" Then
                    describ = "Merchant Deactivation"
                ElseIf success = "W63" Then
                    describ = "Security Violation"
                ElseIf success = "W94" Then
                    describ = "Duplicate Transaction Ref"
                ElseIf success = "X00" Then
                    describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
                ElseIf success = "X01" Then
                    describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
                ElseIf success = "X02" Then
                    describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
                ElseIf success = "X03" Then
                    describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
                ElseIf success = "X04" Then
                    describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
                ElseIf success = "X05" Then
                    describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
                Else
                    describ = "unknown errror "
                End If
            End If
        Catch ex As Exception
            describ = ex.Message
        End Try
        Return describ
    End Function
    
    Public Function QueryEtranzact4Response(ByRef TranxID As String) As String
        Try
            'http://demo.etranzact.com:8080/WebConnect/query.jws?wsdl
            Dim webPayFeedback As New NameValueCollection()
            Dim resp As String = String.Empty
            'Dim respCode As String = String.Empty
            'Dim respAmt As String = String.Empty
            'Dim ss As New EtranzactQuery.queryService


            'Dim TermID As String = System.Configuration.ConfigurationManager.AppSettings("TerminalID")
            ''Dim ISWMerchantID As String = System.Configuration.ConfigurationManager.AppSettings("ISWMerchantID")
            'resp = ss.query(TermID, TranxID)

            ''Split the response from WEBPAY

            'Dim xxxx As String = resp
            'If Len(xxxx) > 1 Then ' : exist in the string
            '    Dim yyyy As String() = xxxx.Split("&")
            '    If yyyy.Length > 1 Then
            '        Dim tltamount As String = yyyy(0).ToString
            '        resp = yyyy(3).ToString
            '    End If

            'End If

            Return resp
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return String.Empty
        End Try
        Return String.Empty
    End Function
    Public Shared Function GetInterSwitchErrorString(ByVal success As String) As String
        Dim describ As String = String.Empty
        Try
            If success = "00" Then
                describ = "Approved or completed successfully"
            ElseIf success = "01" Then
                describ = "Refer to card issuer"
            ElseIf success = "02" Then
                describ = "Refer to card issuer, special condition"
            ElseIf success = "03" Then
                describ = "Invalid merchant"
            ElseIf success = "04" Then
                describ = "Pick-up card"
            ElseIf success = "05" Then
                describ = "Do not honor"
            ElseIf success = "06" Then
                describ = "Error"
            ElseIf success = "07" Then
                describ = "Pick-up card, special condition"
            ElseIf success = "08" Then
                describ = "Honor with identification"
            ElseIf success = "09" Then
                describ = "Request in progress"
            ElseIf success = "10" Then
                describ = "Approved, partial"
            ElseIf success = "11" Then
                describ = "Approved, VIP"
            ElseIf success = "12" Then
                describ = "Invalid transaction"
            ElseIf success = "13" Then
                describ = "Invalid amount"
            ElseIf success = "14" Then
                describ = "Invalid card number"
            ElseIf success = "15" Then
                describ = "No such issuer"
            ElseIf success = "16" Then
                describ = "Approved, update track 3"
            ElseIf success = "17" Then
                describ = "Customer cancellation"
            ElseIf success = "18" Then
                describ = "Customer dispute"
            ElseIf success = "19" Then
                describ = "Re-enter transaction"
            ElseIf success = "20" Then
                describ = "Invalid response"
            ElseIf success = "21" Then
                describ = "No action taken"
            ElseIf success = "22" Then
                describ = "Suspected malfunction"
            ElseIf success = "23" Then
                describ = "Unacceptable transaction fee"
            ElseIf success = "24" Then
                describ = "File update not supported"
            ElseIf success = "25" Then
                describ = "Unable to locate record"
            ElseIf success = "26" Then
                describ = "Duplicate record"
            ElseIf success = "27" Then
                describ = "File update field edit error"
            ElseIf success = "28" Then
                describ = "File update file locked"
            ElseIf success = "29" Then
                describ = "File update failed"
            ElseIf success = "30" Then
                describ = "Format error"
            ElseIf success = "31" Then
                describ = "Bank not supported"
            ElseIf success = "32" Then
                describ = "Completed partially"
            ElseIf success = "33" Then
                describ = "Expired card, pick-up"
            ElseIf success = "34" Then
                describ = "Suspected fraud, pick-up"
            ElseIf success = "35" Then
                describ = "Contact acquirer, pick-up"
            ElseIf success = "36" Then
                describ = "Restricted card, pick-up"
            ElseIf success = "37" Then
                describ = "Call acquirer security, pick-up"
            ElseIf success = "38" Then
                describ = "PIN tries exceeded, pick-up"
            ElseIf success = "39" Then
                describ = "No credit account"
            ElseIf success = "40" Then
                describ = "Function not supported"
            ElseIf success = "41" Then
                describ = "Lost card, pick-up"
            ElseIf success = "42" Then
                describ = "No universal account"
            ElseIf success = "43" Then
                describ = "Stolen card, pick-up"
            ElseIf success = "44" Then
                describ = "No investment account"
            ElseIf success = "45" Then
                describ = "Account closed"
            ElseIf success = "46" Then
                describ = "Identification required"
            ElseIf success = "47" Then
                describ = "Identification cross-check required"
            ElseIf success = "51" Then
                describ = "Not sufficient funds"
            ElseIf success = "52" Then
                describ = "No check account"
            ElseIf success = "53 Then" Then
                describ = "No savings account"
            ElseIf success = "54" Then
                describ = "Expired card"
            ElseIf success = "55" Then
                describ = "Incorrect PIN"
            ElseIf success = "56" Then
                describ = "No card record"
            ElseIf success = "57" Then
                describ = "Transaction not permitted to cardholder"
            ElseIf success = "58" Then
                describ = "Transaction not permitted on terminal"
            ElseIf success = "59" Then
                describ = "Suspected fraud"
            ElseIf success = "60" Then
                describ = "Contact acquirer"
            ElseIf success = "61" Then
                describ = "Exceeds withdrawal limit"
            ElseIf success = "62" Then
                describ = "Restricted card"
            ElseIf success = "63" Then
                describ = "Security violation"
            ElseIf success = "64" Then
                describ = "Original amount incorrect"
            ElseIf success = "65" Then
                describ = "Exceeds withdrawal frequency"
            ElseIf success = "66" Then
                describ = "Call acquirer security"
            ElseIf success = "67" Then
                describ = "Hard capture"
            ElseIf success = "68" Then
                describ = "Response received too late"
            ElseIf success = "69" Then
                describ = "Advice received too late"
            ElseIf success = "75" Then
                describ = "PIN tries exceeded"
            ElseIf success = "76" Then
                describ = "Reserved for future Postilion use"
            ElseIf success = "77" Then
                describ = "Intervene, bank approval required"
            ElseIf success = "78" Then
                describ = "Intervene, bank approval required for partial amount"
            ElseIf success = "90" Then
                describ = "Cut-off in progress"
            ElseIf success = "91" Then
                describ = "Issuer or switch inoperative"
            ElseIf success = "92" Then
                describ = "Routing error"
            ElseIf success = "93" Then
                describ = "Violation of law"
            ElseIf success = "94" Then
                describ = "Duplicate transaction"
            ElseIf success = "95" Then
                describ = "Reconcile error"
            ElseIf success = "96" Then
                describ = "System malfunction"
            ElseIf success = "97" Then
                describ = "Reserved for future Postilion use"
            ElseIf success = "98" Then
                describ = "Exceeds cash limit"
            ElseIf success = "W06" Then
                describ = "Application Error"
            ElseIf success = "W09" Then
                describ = "Request In Progress"
            ElseIf success = "W17" Then
                describ = "Customer Cancellation"
            ElseIf success = "W56" Then
                describ = "No Transaction Record"
            ElseIf success = "W57" Then
                describ = "Merchant Deactivation"
            ElseIf success = "W63" Then
                describ = "Security Violation"
            ElseIf success = "W94" Then
                describ = "Duplicate Transaction Ref"
            ElseIf success = "X00" Then
                describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
            ElseIf success = "X01" Then
                describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
            ElseIf success = "X02" Then
                describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
            ElseIf success = "X03" Then
                describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
            ElseIf success = "X04" Then
                describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
            ElseIf success = "X05" Then
                describ = "Transaction could not be authorized. Please contact your bank or send an email to webpay.support@interswitchng.com"
            Else
                describ = "unknown errror "
            End If

        Catch ex As Exception
            describ = ex.Message
        End Try
        Return describ
    End Function
    Public Function ValidateDateInput(ByVal datevalue As String, ByRef msg As String) As Boolean
        '**********************This Guy Checks the Date Value Supplied as input****************
        '                   The Standard Format Considered id 'yyyy/MM/dd'
        '*************************************************************************************
        Try
            If Len(datevalue) <> 10 Then
                'Msg = "Date supplied is not correct!"
                msg = "Date supplied is not correct!"
                Return False
            End If
            Dim xtractIndex As Integer = datevalue.IndexOf("/")
            If xtractIndex = -1 Then
                'Msg = "Date is in wrong format!"
                msg = "Date supplied is not correct!"
                Return False
            ElseIf xtractIndex <> 4 Then
                'Msg = Date is in wrong format ' As in the year is not stated as required
                msg = "Date is in wrong format ' As in the year is not stated as required"
                Return False
            End If
            Dim xtractYear As String = datevalue.Substring(0, 4)
            If Not IsNumeric(xtractYear) Then
                msg = "error1"
                Return False
            End If
            Dim year As Integer = CInt(xtractYear)
            If year < 1900 Then
                'Msg = Date Supplied is not correct ' As in the year cannot be less than 1900 except very few cases not considered here!
                msg = "Date Supplied is not correct ' As in the year cannot be less than 1900 except very few cases not considered here!"
                Return False
            End If
            Dim xtractLastIndex As Integer = datevalue.LastIndexOf("/")
            If xtractLastIndex = -1 Then
                'Msg = "Date is in wrong format!"
                msg = "Date is in wrong format!"
                Return False
            ElseIf xtractLastIndex <> 7 Then
                'Msg = Date is in wrong format ' As in the year is not stated as required
                msg = "Date is in wrong format ' As in the year is not stated as required"
                Return False
            End If
            Dim xtractMonth As String = datevalue.Substring(5, 2)
            If Not IsNumeric(xtractMonth) Then
                msg = "error2"
                Return False
            End If
            Dim month As Integer = CInt(xtractMonth)
            If month < 1 Or month > 12 Then
                'Msg = "Date is in wrong format!"
                msg = "Date is in wrong format!"
                Return False
            End If
            Dim xtractDay As String = datevalue.Substring(8, 2)
            If Not IsNumeric(xtractDay) Then
                msg = "error3"
                Return False
            End If
            Dim day As Integer = CInt(xtractDay)
            If month = 4 Or month = 6 Or month = 9 Or month = 11 Then
                If day > 30 Then
                    'Msg = "Day cannot be more than 30!"
                    msg = "Day cannot be more than 30!"
                    Return False
                End If
            Else
                If day > 31 Then
                    'Msg = "Day cannot be more than 31!"
                    msg = "Day cannot be more than 31!"
                    Return False
                End If
            End If
            If year Mod 4 = 0 Then ' Leap Year here
                If month = 2 And day > 29 Then
                    'Msg = "For Leap year, day cannot be more than 29!"
                    msg = "For Leap year, day cannot be more than 29!"
                    Return False
                End If
            Else ' Not a leap year
                If month = 2 And day > 28 Then
                    'Msg = "For February, day cannot be more than 28!"
                    msg = "For February, day cannot be more than 28!"
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            msg = "error4"
            Return False
        End Try

    End Function

    Public Shared Function GetVISAErrorString(ByVal success As String) As String
        Dim describ As String = String.Empty
        Try
            If success = "" Then
                Return "Connection timeout, Please try again later"
            End If

            If success = "000" Then
                describ = "Approved, balances avialable"
            ElseIf success = "001" Then
                describ = "Approved, no balances avialable"
            ElseIf success = "003" Then
                describ = "Approved, additional Identification requested"
            ElseIf success = "007" Then
                describ = "Approved administrative transaction"
            ElseIf success = "-1" Then
                describ = "System Error"
            ElseIf success = "00" Then
                describ = "Authentication failed"
            ElseIf success = "02" Then
                describ = "Attempt Processing"
            ElseIf success = "05" Then
                describ = "Not enrolled or Element is Missing"
            ElseIf success = "050" Then
                describ = "General"
            ElseIf success = "051" Then
                describ = "Expired Card"
            ElseIf success = "052" Then
                describ = "Number of Pin Tries Exceeded"
            ElseIf success = "053" Then
                describ = "No sharing allowed"
            ElseIf success = "055" Then
                describ = "Invalid Transaction"
            ElseIf success = "056" Then
                describ = "Transaction not supported by institution"
            ElseIf success = "057" Then
                describ = "Lost or Stolen Card"
            ElseIf success = "058" Then
                describ = "Invalid card Status"
            ElseIf success = "059" Then
                describ = "Restricted Status"
            ElseIf success = "060" Then
                describ = "Account not Found"
            ElseIf success = "061" Then
                describ = "Wrong customer information for payment"
            ElseIf success = "062" Then
                describ = "Customer information format error"
            ElseIf success = "063" Then
                describ = "Prepaid Code not found"
            ElseIf success = "064" Then
                describ = "Bad Track Information"
            ElseIf success = "069" Then
                describ = "Bad Message Edit"
            ElseIf success = "074" Then
                describ = "Unable to Authorize"
            ElseIf success = "075" Then
                describ = "Invalid Credit Pan"
            ElseIf success = "076" Then
                describ = "Insufficient Funds"

            ElseIf success = "078" Then
                describ = "Duplicate Transaction Recieved"
            ElseIf success = "082" Then
                describ = "Maximum Number of times Used"
            ElseIf success = "085" Then
                describ = "Balance not allowed"
            ElseIf success = "095" Then
                describ = "Amount over maximum"
            ElseIf success = "100" Then
                describ = "Unable to process"
            ElseIf success = "101" Then
                describ = "Unable to authorize- call issuer"
            ElseIf success = "105" Then
                describ = "Card Not Supported"
            ElseIf success = "200" Then
                describ = "Invalid Account"
            ElseIf success = "201" Then
                describ = "Invalid Account"


            ElseIf success = "201" Then
                describ = "Incorrect Pin"
            ElseIf success = "205" Then
                describ = "Invalid advance amount"
            ElseIf success = "209" Then
                describ = "Invalid Transaction Code"
            ElseIf success = "210" Then
                describ = "Bad CAVV"
            ElseIf success = "211" Then
                describ = "Bad CVV2"
            ElseIf success = "212" Then
                describ = "Original Transaction not found for slip"
            ElseIf success = "213" Then
                describ = "Slip Already Recieved"
            ElseIf success = "800" Then
                describ = "Format Error"
            ElseIf success = "801" Then
                describ = "Original Transsaction not found"


            ElseIf success = "809" Then
                describ = "Invalid Close Transaction"
            ElseIf success = "810" Then
                describ = "Transaction Timeout"
            ElseIf success = "811" Then
                describ = "System Error"
            ElseIf success = "820" Then
                describ = "Invalid Terminal Identifier"
            ElseIf success = "880" Then
                describ = "The Last Batch Has Been sent; the downloading is successfully completed"
            ElseIf success = "881" Then
                describ = "The Previous Download stage was successfully completed; there are more data to be downloaded"
            ElseIf success = "882" Then
                describ = "The Terminal downloading is stopped. Call to the processing center is required"
            ElseIf success = "897" Then
                describ = "Cryptogram recieved in the transaction request is invalid"
            ElseIf success = "898" Then
                describ = "Invalid MAC Recieved in the transaction request"


            ElseIf success = "899" Then
                describ = "Sequence error-resync"
            ElseIf success = "900" Then
                describ = "Pin Tries Limit Exceeded"
            ElseIf success = "909" Then
                describ = "External Decline Special condition"
            ElseIf success = "959" Then
                describ = "Administrative Transaction not Supported"
          
            End If

        Catch ex As Exception
            describ = ex.Message
        End Try
        Return describ
    End Function

End Class

