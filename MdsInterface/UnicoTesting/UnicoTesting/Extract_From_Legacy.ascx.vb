Imports System.Data.OleDb
Public Class Extract_From_Legacy
    Inherits System.Web.UI.UserControl
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Page.IsPostBack = False Then
                Me.LoadDropDown()
                DefaultToTodaY()
                'CheckUserLogin()
                Me.lblMsg.Text = ""
                'Me.lblUserName.Text = "BODSUSER"
                Me.lblMsg.Text = ""
                HideTables()
                Me.tblSectionA.Visible = True

            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub



    Protected Sub DefaultToTodaY()
        Try
            Me.lblMsg.Text = ""
            Dim DisMonthString As String
            Dim DisMonth As Integer
            Dim DisYear As Integer = System.DateTime.Today.Year
            DisMonth = System.DateTime.Today.Month.ToString
            DisMonthString = DisMonth.ToString
            If Len(DisMonthString) = 1 Then
                DisMonthString = "0" & DisMonth
                DisMonth = CInt(DisMonthString)
            End If

            Me.ddlUnico_Period.SelectedValue = DisMonthString
            Me.ddlUnicoYear.SelectedValue = CInt(IIf(IsNumeric(DisYear), DisYear, 0))

            Me.ddlUnico_Period.Enabled = False
            Me.ddlUnicoYear.Enabled = False
            Me.lblPeriod.Text = DisYear & DisMonth
            Me.btnExtract.Enabled = False
            SqlDataSource1.DataBind()
            GridView1.DataBind()


            HideTables()
            Me.tblViewGrid.Visible = True
            Me.tblSectionA.Visible = True

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub CheckUserLogin()
        Try
            If (Session("Username") IsNot Nothing OrElse [String].Empty.Equals(Session("Username"))) Then

                Me.lblUserName.Text = Session("Username").ToString
            Else


                Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                Response.Redirect(LoginPage, False)
            End If






        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    



    Protected Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        Try
            Me.lblMsg.Text = ""
            Dim SelectedYear As String = Me.ddlUnicoYear.SelectedValue.ToString
            Dim SelectedPeriod As String = Me.ddlUnico_Period.SelectedValue.ToString

            Me.lblPeriod.Text = SelectedYear & SelectedPeriod

            SqlDataSource1.DataBind()
            GridView1.DataBind()
            InsertIntoMyTable()
            BindOriginalGL_TRS()




        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub BindBalancesSummary()
        Try
            Me.lblMsg.Text = ""
            Dim dv As DataView = mydata2.GetAllBalancesByReference

            If dv.Count > 0 Then
                With Me.dgBalances
                    .DataSource = dv
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub HideTables()

        Me.tblSectionA.Visible = False
        Me.tblViewGrid.Visible = False
        Me.tblViewBalances.Visible = False
        Me.tblViewBalancesDetails.Visible = False
        Me.tblViewSuccessFull.Visible = False

    End Sub
    Protected Sub LoadDropDown()
        Try
            With Me.ddlUnico_Period
                .DataSource = mydata2.GetALLUnicoPeriod
                .DataTextField = "DESCRIPTION"
                .DataValueField = "PERIOD"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlUnicoYear
                .DataSource = mydata2.GetALLUnicoYear
                .DataTextField = "YEAR"
                .DataValueField = "YEAR"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub InsertIntoMyTable()
        Try


            Me.lblMsg.Text = ""


            mydata.TRUNCATE_GL_TRS()
            mydata.TRUNCATE_UNICO_MAPPING_TABLE()
            mydata.TRUNCATE_UNICO_SUM_LOGIC()
            If GridView1.Rows.Count > 0 Then
                For i As Integer = 0 To Me.GridView1.Rows.Count - 1
                    If Len(GridView1.Rows(i).Cells(0).Text) = 6 Then

                      

                    If GetNewAccountNumber(GridView1.Rows(i).Cells(0).Text) = "1" Then

                    Else

                            Dim TRS_REF As String = GridView1.Rows(i).Cells(3).Text.ToString

                            Dim EntryDating As Date = GridView1.Rows(i).Cells(20).Text


                           

                            Dim ENTRY_DATE As String = EntryDating.ToString("yyyy/MM/dd")

                            ENTRY_DATE = ENTRY_DATE.Replace("/", "")



                            Dim GL_ACCOUNT As String = GetNewAccountNumber(GridView1.Rows(i).Cells(0).Text)
                            GL_ACCOUNT = "0000" & GL_ACCOUNT

                            Dim AMt As String = GridView1.Rows(i).Cells(12).Text.ToString
                            AMt = AMt.Replace("-", "")
                            Dim AMOUNT As Integer = CInt(IIf(IsNumeric(AMt), AMt, 0))
                            Dim theAmount As String = AMOUNT.ToString
                            theAmount = Left(AMOUNT, 3)
                        Dim DEBIT_CREDIT As String = String.Empty
                        Dim negativechecker As String = Left(AMOUNT, 1)
                            Dim Monat As String = Me.ddlUnico_Period.SelectedValue.ToString
                        If negativechecker = "-" Then
                            DEBIT_CREDIT = 50
                        Else
                            DEBIT_CREDIT = 40
                        End If
                            Dim PostingDating As String = BindPostingDateByVal(GridView1.Rows(i).Cells(5).Text)

                           
                            Dim nDV As DataView = mydata2.GetCurrentCLient()

                            Dim Client As String = nDV.Item(0).Item("CLIENT").ToString


                            Dim RepostCheacker As Integer = RepostChecker(TRS_REF, PostingDating, GL_ACCOUNT, theAmount, DEBIT_CREDIT, PostingDating, Client, Monat)

                        If RepostCheacker = 2 Then

                        ElseIf RepostCheacker = 1 Then


                            Dim sending As New TableObjects.GL_TRS
                            With sending
                                .ACCT_NO = GridView1.Rows(i).Cells(0).Text
                                .BATCH_NO = GridView1.Rows(i).Cells(7).Text
                                .COMPANY_ID = GridView1.Rows(i).Cells(16).Text
                                .CURRENCY_CODE = GridView1.Rows(i).Cells(17).Text
                                .DateOfExtract = System.DateTime.Now
                                .DEPT_ID = GridView1.Rows(i).Cells(1).Text
                                .EMPLOYEE_CODE = GridView1.Rows(i).Cells(15).Text
                                .ENTRY_DATE = GridView1.Rows(i).Cells(20).Text
                                    .EXCHANGE_RATE = CDbl(IIf(IsNumeric(GridView1.Rows(i).Cells(18).Text), GridView1.Rows(i).Cells(18).Text, 0))
                                .ExtractReference = (New Utilities).GenerateRefNo
                                '.ExtractReference = GridView1.Rows(i).Cells(8).Text
                                .LOCKED = GridView1.Rows(i).Cells(11).Text
                                .PERIOD = GridView1.Rows(i).Cells(5).Text
                                .PREV_PERIOD = GridView1.Rows(i).Cells(6).Text
                                .PROCESSED = GridView1.Rows(i).Cells(19).Text
                                .SEQ = CInt(IIf(IsNumeric(GridView1.Rows(i).Cells(13).Text), GridView1.Rows(i).Cells(13).Text, 0))
                                    .TRS_AMT = CDbl(IIf(IsNumeric(GridView1.Rows(i).Cells(12).Text), GridView1.Rows(i).Cells(12).Text, 0))
                                .TRS_DATE = GridView1.Rows(i).Cells(4).Text
                                .TRS_DESC = GridView1.Rows(i).Cells(2).Text
                                .TRS_ID = GridView1.Rows(i).Cells(10).Text
                                .TRS_PRT = GridView1.Rows(i).Cells(9).Text
                                .TRS_REF = GridView1.Rows(i).Cells(3).Text
                                .TRS_SYSTEM = GridView1.Rows(i).Cells(8).Text
                                .UNIT_CODE = GridView1.Rows(i).Cells(14).Text
                                .UseFlag = 1
                                .SAP_GL_ACCOUNT = GetNewAccountNumber(GridView1.Rows(i).Cells(0).Text)
                                .USERNAME = Me.lblUserName.Text.ToString

                            End With

                            Dim kk As Integer = mydata.InsertGL_TRS(sending)
                            If kk > 0 Then

                            End If

                        End If

                        End If
                    Else

                    End If
                Next

                BindBalancesSummary()
            End If

            HideTables()
            Me.tblViewGrid.Visible = True



        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub BindLoadedData()
        Try
            Me.lblMsg.Text = ""
            mydata.TRUNCATE_UNICO_MAPPING_TABLE()

            Dim ds As DataSet = mydata2.GetLegacyDataForLoad()
            If ds Is Nothing Then
                Me.lblMsg.Text = "Sorry there is a Problem connecting to SQL Database. Please contact SQL Database Administrator"
                Exit Sub
                HideTables()
                Me.tblViewGrid.Visible = True
            End If

           
            Dim nDv As DataView = New DataView(ds.Tables(1))




            If nDv Is Nothing Then
                Me.lblMsg.Text = "Connection to SQL Database failed. Please retry Loading"

            End If

            If nDv.Count = 0 Then
                Me.lblMsg.Text = "All Line Items Selected Does Not Balance. Please review Extraction  from the Sybase System"


            End If


            If nDv.Count > 0 Then



                For i As Integer = 0 To nDv.Count - 1
                    Dim theSend As New TableObjects.UNICO_MAPPING_TABLE
                    With theSend

                        .ACCT_NO = nDv.Item(i).Item("Acct_No").ToString
                        .BATCH_NO = nDv.Item(i).Item("Batch_No").ToString
                        .COMPANY_ID = nDv.Item(i).Item("Company_ID").ToString
                        .CURRENCY_CODE = nDv.Item(i).Item("Currency_code").ToString
                        .DateOfExtract = System.DateTime.Now
                        .TRS_REF = nDv.Item(i).Item("Trs_Ref").ToString
                        .DEPT_ID = nDv.Item(i).Item("DeptID").ToString
                        .EMPLOYEE_CODE = nDv.Item(i).Item("Employee_Code").ToString
                        .ENTRY_DATE = Me.BindPostingDate(.TRS_REF)
                        .EXCHANGE_RATE = CInt(IIf(IsNumeric(nDv.Item(i).Item("Exchange_Rate").ToString), nDv.Item(i).Item("Exchange_Rate").ToString, 0))
                        .ExtractReference = (New Utilities).GenerateRefNo
                        '.ExtractReference = GridView1.Rows(i).Cells(8).Text
                        .LOCKED = nDv.Item(i).Item("Locked").ToString
                        .PERIOD = Me.BindPeriod(.TRS_REF)
                        .PREV_PERIOD = nDv.Item(i).Item("Prev_Period").ToString
                        .PROCESSED = nDv.Item(i).Item("Processed").ToString
                        .SEQ = CInt(IIf(IsNumeric(nDv.Item(i).Item("Seq").ToString), nDv.Item(i).Item("Seq").ToString, 0))
                        Dim SignValue As String = Left(nDv.Item(i).Item("Trs_Amt").ToString, 1)
                        If SignValue = "-" Then
                            Dim Amount As Double = CDbl(IIf(IsNumeric(nDv.Item(i).Item("Trs_Amt")), nDv.Item(i).Item("Trs_Amt"), 0))
                            .TRS_AMT = CDbl(Math.Abs(Amount))
                        Else
                            Dim Amount As Double = CDbl(IIf(IsNumeric(nDv.Item(i).Item("Trs_Amt")), nDv.Item(i).Item("Trs_Amt"), 0))
                            .TRS_AMT = CDbl(Amount * -1)

                        End If
                        '.TRS_AMT = CDbl(IIf(IsNumeric(nDv.Item(i).Item("Trs_Amt")), nDv.Item(i).Item("Trs_Amt"), 0))
                        .TRS_DATE = nDv.Item(i).Item("Trs_Date").ToString
                        .TRS_DESC = nDv.Item(i).Item("Trs_Desc").ToString
                        .TRS_ID = nDv.Item(i).Item("TRS_ID").ToString
                        .TRS_PRT = nDv.Item(i).Item("TRS_PRT").ToString
                        .TRS_SYSTEM = nDv.Item(i).Item("Trs_system").ToString
                        .UNIT_CODE = nDv.Item(i).Item("Unit_Code").ToString
                        .UseFlag = 1
                        .SAP_GL_ACCOUNT = nDv.Item(i).Item("SAP_GL_ACCOUNT").ToString
                        .USERNAME = nDv.Item(i).Item("USERNAME").ToString
                        .DocumentNumber = nDv.Item(i).Item("DocumentNo").ToString
                    End With

                    Dim thete As Integer = mydata.InsertUNICO_MAPPING_TABLE(theSend)
                Next

            End If

            'This is to load into the bad table for data that did not balance

            Dim myDv As DataView = New DataView(ds.Tables(2))




            If myDv Is Nothing Then
                Me.lblMsg.Text = "Connection to SQL Database failed. Please retry Loading"

            End If

            If myDv.Count = 0 Then
                Me.lblMsg.Text = "Congratulations. All Items Extracted Balanced Successfully"
                Me.lblMsg.ForeColor = Drawing.Color.Green



            End If


            If myDv.Count > 0 Then



                For i As Integer = 0 To myDv.Count - 1
                    Dim SendBad As New TableObjects.UNICO_BAD_TABLE
                    With SendBad

                        .ACCT_NO = myDv.Item(i).Item("Acct_No").ToString
                        .BATCH_NO = myDv.Item(i).Item("Batch_No").ToString
                        .COMPANY_ID = myDv.Item(i).Item("Company_ID").ToString
                        .CURRENCY_CODE = myDv.Item(i).Item("Currency_code").ToString
                        .DateOfExtract = System.DateTime.Now
                        .DEPT_ID = myDv.Item(i).Item("DeptID").ToString
                        .EMPLOYEE_CODE = myDv.Item(i).Item("Employee_Code").ToString
                        .ENTRY_DATE = myDv.Item(i).Item("Entry_Date").ToString
                        .EXCHANGE_RATE = CInt(IIf(IsNumeric(myDv.Item(i).Item("Exchange_Rate").ToString), myDv.Item(i).Item("Exchange_Rate").ToString, 0))
                        .ExtractReference = (New Utilities).GenerateRefNo
                        '.ExtractReference = GridView1.Rows(i).Cells(8).Text
                        .LOCKED = myDv.Item(i).Item("Locked").ToString
                        .PERIOD = myDv.Item(i).Item("Period").ToString
                        .PREV_PERIOD = myDv.Item(i).Item("Prev_Period").ToString
                        .PROCESSED = myDv.Item(i).Item("Processed").ToString
                        .SEQ = CInt(IIf(IsNumeric(myDv.Item(i).Item("Seq").ToString), myDv.Item(i).Item("Seq").ToString, 0))
                        .TRS_AMT = CInt(IIf(IsNumeric(myDv.Item(i).Item("Trs_Amt")), myDv.Item(i).Item("Trs_Amt"), 0))
                        .TRS_DATE = myDv.Item(i).Item("Trs_Date").ToString
                        .TRS_DESC = myDv.Item(i).Item("Trs_Desc").ToString
                        .TRS_ID = myDv.Item(i).Item("TRS_ID").ToString
                        .TRS_PRT = myDv.Item(i).Item("TRS_PRT").ToString
                        .TRS_REF = myDv.Item(i).Item("Trs_Ref").ToString
                        .TRS_SYSTEM = myDv.Item(i).Item("Trs_system").ToString
                        .UNIT_CODE = myDv.Item(i).Item("Unit_Code").ToString
                        .UseFlag = 1
                        .SAP_GL_ACCOUNT = myDv.Item(i).Item("SAP_GL_ACCOUNT").ToString
                        .USERNAME = myDv.Item(i).Item("USERNAME").ToString
                        .DocumentNumber = myDv.Item(i).Item("DocumentNo").ToString
                    End With

                    Dim thete As Integer = mydata.InsertUNICO_BAD_TABLE(SendBad)
                Next

            End If
            mydata.TRUNCATE_UNICO_DOC_HEADER()
            Dim gnDv As DataView = New DataView(ds.Tables(0))

            If gnDv Is Nothing Then
                Me.lblMsg.Text = "Connection to SQL Database failed. Please retry Loading"
            End If

            If gnDv.Count = 0 Then
                Me.lblMsg.Text = "All Line Items Selected Does Not Balance. Please review Extraction  from the Sybase System"
            End If
            If gnDv.Count > 0 Then
                For i As Integer = 0 To gnDv.Count - 1
                    Dim Trs_Ref As String = gnDv.Item(i).Item("Trs_Ref").ToString
                    If Me.BindUserName(Trs_Ref) = "EXIT" Then

                    Else

                        Dim Header As New TableObjects.UNICO_DOC_HEADER

                        With Header
                            .DocumentNumber = gnDv.Item(i).Item("Id")
                            .USERNAME = Me.BindUserName(Trs_Ref)
                            .ENTRY_DATE = Me.BindEntryDate(Trs_Ref)
                            .TRS_REF = gnDv.Item(i).Item("Trs_Ref").ToString

                            .PERIOD = Me.BindPeriod(Trs_Ref)
                            .POSTINGDATE = Me.BindPostingDate(Trs_Ref)

                        End With

                        mydata.InsertUNICO_DOC_HEADER(Header)
                    End If

                  
                Next
               
            End If
            HideTables()
            Me.tblViewSuccessFull.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Private Function BindEntryDate(Trs_Ref As String) As String

        Dim EntryDate As String

        Try
            Dim dv As DataView = mydata2.GetHeaderInfo(Trs_Ref)


            EntryDate = dv.Item(0).Item("ENTRY_DATE")

            EntryDate = EntryDate.Replace("/", "")

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try



        Return EntryDate
    End Function

    Private Function BindUserName(Trs_Ref As String) As String
        Dim Username As String = String.Empty
        Try
            Dim dv As DataView = mydata2.GetHeaderInfo(Trs_Ref)

            If dv Is Nothing Then
                Username = "EXIT"
            End If

            If dv.Count = 0 Then
                Username = "EXIT"
            End If

            If dv.Count > 0 Then
                Username = dv.Item(0).Item("USERNAME").ToString
            End If


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try

        

        Return Username
    End Function

    Private Function BindPostingDate(Trs_Ref As String) As String
        Dim PostingDate As String = String.Empty
        Try
            Dim dv As DataView = mydata2.GetHeaderInfo(Trs_Ref)

            If dv Is Nothing Then
                PostingDate = "EXIT"
            End If

            If dv.Count = 0 Then
                PostingDate = "EXIT"
            End If

            If dv.Count > 0 Then
                Dim Period As String = String.Empty
                Dim Period_One As String = String.Empty
                Period = dv.Item(0).Item("PERIOD").ToString
                Period_One = Right(Period, 2)
                If Period_One = "02" Then
                    PostingDate = Period & "28"
                    Return PostingDate
                ElseIf Period_One = "04" Then
                    PostingDate = Period & "30"
                    Return PostingDate

                ElseIf Period_One = "06" Then
                    PostingDate = Period & "30"
                    Return PostingDate

                ElseIf Period_One = "09" Then
                    PostingDate = Period & "30"
                    Return PostingDate

                ElseIf Period_One = "11" Then
                    PostingDate = Period & "30"
                    Return PostingDate

                Else
                    PostingDate = Period & "31"
                    Return PostingDate

                End If

            End If
            Return PostingDate

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try



        Return PostingDate
    End Function

    Private Function BindPostingDateByVal(InputPostingDate As String) As String
        Dim PostingDate As String = String.Empty
        Try
           

            Dim Period As String = String.Empty
            Dim Period_One As String = String.Empty
            Period = InputPostingDate
            Period_One = Right(Period, 2)
            If Period_One = "02" Then
                PostingDate = Period & "28"
                Return PostingDate
            ElseIf Period_One = "04" Then
                PostingDate = Period & "30"
                Return PostingDate

            ElseIf Period_One = "06" Then
                PostingDate = Period & "30"
                Return PostingDate

            ElseIf Period_One = "09" Then
                PostingDate = Period & "30"
                Return PostingDate

            ElseIf Period_One = "11" Then
                PostingDate = Period & "30"
                Return PostingDate

            Else
                PostingDate = Period & "31"
                Return PostingDate

            End If


            Return PostingDate

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try



        Return PostingDate
    End Function
    Private Function BindPeriod(Trs_Ref As String) As String
        Dim Period As String = String.Empty
        Try
            Dim dv As DataView = mydata2.GetHeaderInfo(Trs_Ref)

            If dv Is Nothing Then
                Period = "EXIT"
                Return Period
            End If

            If dv.Count = 0 Then
                Period = "EXIT"
                Return Period
            End If

            If dv.Count > 0 Then

                Period = dv.Item(0).Item("PERIOD").ToString
                Period = Right(Period, 2)
                Return Period
            End If


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try



        Return Period
    End Function



    Protected Function GetNewAccountNumber(LegacyAccountNumber As String) As String
        Dim SapAccountNumber As String = String.Empty
        Me.lblMsg.Text = ""
        Try
            If Len(LegacyAccountNumber) = 6 Then

                Dim dv As DataView = mydata2.GetAllAccountMapping(LegacyAccountNumber)
                If dv Is Nothing Then
                    SapAccountNumber = "1"
                    Return SapAccountNumber
                    Exit Function
                End If

                If dv.Count = 0 Then
                    SapAccountNumber = "1"
                    Return SapAccountNumber
                    Exit Function
                End If

                If dv.Count > 0 Then
                    SapAccountNumber = dv.Item(0).Item("SAPCode").ToString
                    Return SapAccountNumber
                    Exit Function
                End If
            Else
                SapAccountNumber = "1"

            End If

          




            Return SapAccountNumber

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
        Return SapAccountNumber
    End Function

    Protected Function GetLegacyAccountNumber(SAPAccountNumber As String) As String
        Dim LegacyAccountNumber As String = String.Empty
        Me.lblMsg.Text = ""
        Try


            Dim dv As DataView = mydata2.GetAllReturnAccountMapping(SAPAccountNumber)
            If dv Is Nothing Then
                LegacyAccountNumber = "1"
                Return LegacyAccountNumber
                Exit Function
            End If

            If dv.Count = 0 Then
                LegacyAccountNumber = "1"
                Return LegacyAccountNumber
                Exit Function
            End If

            If dv.Count > 0 Then
                LegacyAccountNumber = dv.Item(0).Item("LegCode").ToString
                Return LegacyAccountNumber
                Exit Function
            End If
           






            Return LegacyAccountNumber

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
        Return SapAccountNumber
    End Function

    Protected Function RepostChecker(TRS_REF As String, ENTRY_DATE As String, GL_ACCOUNT As String, AMOUNT As String, DEBIT_CREDIT As String, POSTING_DATE As String, CLIENT As String, Monat As String) As Integer
        Dim ThisFlag As Integer
        Try
            Dim dv As DataView = mydata2.GetAllRepostings(TRS_REF, ENTRY_DATE, GL_ACCOUNT, AMOUNT, DEBIT_CREDIT, POSTING_DATE, CLIENT, Monat)

            If dv Is Nothing Then
                Me.lblMsg.Text = "Error Connecting to SAP, Please Ensure that SAP is running Correctly"
                ThisFlag = 1
                Return ThisFlag
            End If

            If dv.Count = 0 Then
                ThisFlag = 1
                Return ThisFlag
            End If

            If dv.Count > 0 Then
                ThisFlag = 2
                Return ThisFlag
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
        Return ThisFlag
    End Function

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Me.lblMsg.Text = ""
            HideTables()
            Me.tblSectionA.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnViewSum_Click(sender As Object, e As EventArgs) Handles btnViewSum.Click
        Try
            BindBalancesSummary()
            Me.lblMsg.Text = ""
            HideTables()
            Me.tblViewBalances.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Me.lblMsg.Text = ""
            BindLoadedData()


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.lblMsg.Text = ""
            HideTables()
            Me.tblViewBalances.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnBakkk_Click(sender As Object, e As EventArgs) Handles btnBakkk.Click
        Try
            Me.lblMsg.Text = ""
            HideTables()
            Me.tblViewGrid.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Me.lblMsg.Text = ""
            HideTables()
            Me.tblSectionA.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Private Sub dgBalances_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBalances.ItemCommand
        Try
            Me.lblMsg.Text = ""
            Me.dgBalances.SelectedIndex = e.Item.ItemIndex
            Dim TransRef As String = CType(Me.dgBalances.SelectedItem.FindControl("lblTRS_REF"), Label).Text

            If e.CommandArgument = "View" Then

                Dim dv As DataView = mydata2.GetAllBalancesDetails(TransRef)
                If dv Is Nothing Then
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Exit Sub
                End If
                If dv.Count > 0 Then
                    With Me.dgBalancesDetails
                        .DataSource = dv
                        .DataBind()
                    End With
                    HideTables()
                    Me.tblViewBalancesDetails.Visible = True


                End If
            End If


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub lnkChangeDate_Click(sender As Object, e As EventArgs) Handles lnkChangeDate.Click
        Try
            Me.lblMsg.Text = ""
            Me.ddlUnico_Period.Enabled = True
            Me.ddlUnicoYear.Enabled = True


            Me.btnExtract.Enabled = True

            HideTables()
            Me.tblSectionA.Visible = True


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnRemoveFromList_Click(sender As Object, e As EventArgs) Handles btnRemoveFromList.Click
        Try
            Me.lblMsg.Text = ""
            Dim dg As DataGridItem
            For Each dg In Me.dgViewGLTable.Items
                If CType(dg.FindControl("chkSelect"), CheckBox).Checked = True Then


                    Dim Trans_Ref As String = CType(dg.FindControl("lblTRS_REF"), Label).Text.ToString

                    mydata.DeleteGL_TRS(Trans_Ref)




                End If
            Next

            Me.BindOriginalGL_TRS()

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub BindOriginalGL_TRS()
        Try
            Me.lblMsg.Text = ""
            Dim dv As DataView = mydata2.GetAllGL_TRS()
            If dv Is Nothing Then
                Me.lblMsg.Text = "Sorry. Connection to Sybase ASE Database was not Successfull. Please contact Database Administrator"
                HideTables()
                Me.tblSectionA.Visible = True
            End If
            If dv.Count = 0 Then
                Me.lblMsg.Text = "Sorry there is no data extracted for period selected"
                HideTables()
                Me.tblSectionA.Visible = True
            End If
            If dv.Count > 0 Then
                With Me.dgViewGLTable
                    .DataSource = dv
                    .DataBind()

                End With
                HideTables()
                Me.tblViewGrid.Visible = True
                Me.tblViewGrid.Visible = True
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


End Class