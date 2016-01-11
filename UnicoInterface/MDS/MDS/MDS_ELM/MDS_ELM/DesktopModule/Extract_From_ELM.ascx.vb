Imports System.Data.OleDb
Imports System.Data
Imports System.Data.SqlClient
Public Class Extract_From_ELM
    Inherits System.Web.UI.UserControl
    Private mydata As New DataWriter
    Private mydata2 As New DataGet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                LoadClientDropDown()
                HideTables()
                Me.tblSectionA.Visible = True
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    
    Protected Sub LoadClientDropDown()
        Try
            With Me.ddlClient
                .DataSource = mydata2.GETALLCLIENTS
                .DataTextField = "ELM_CLIENT_NAME"
                .DataValueField = "ELM_CLIENT_ID"
                .DataBind()
                .Items.Insert(0, New ListItem("--- Please Select ---", 0))
                .SelectedIndex = 0
            End With
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click

        Try

            Dim Sdate As Date = CDate(Me.txtEndDate.Text.ToString)
            Dim yDate As Integer = Sdate.Year
            Dim mDate As Integer = Sdate.Month
            Dim MMDate As String = mDate
            If Len(MMDate) = 1 Then
                MMDate = "0" & MMDate
            Else
                MMDate = MMDate
            End If
            Dim dDate As Integer = Sdate.Day
            Dim SalesOrderDate As String
            SalesOrderDate = Me.GetLastDayOfMonth(mDate) & "/" & MMDate & "/" & yDate

            Session("_SalesOrderDate") = SalesOrderDate
            mydata.TruncateTempTable()
            Dim ClientDv As DataView = mydata2.GetAllunMappedCustomers
            Dim DepotDV As DataView = mydata2.GetAllunMappedDeports

            If ClientDv.Count <> 0 Then
                With Me.dgViewUnMappedCustomers
                    .DataSource = ClientDv
                    .DataBind()
                End With

            ElseIf ClientDv.Count = 0 Then
                Me.lblUnMappedCustomers.ForeColor = Drawing.Color.Green
                Me.lblUnMappedCustomers.Text = "Congratulations, you do not have any unmapped Customers. Please Remember to map all Newly created Customers in ELM to their Corresponding SAP Numbers"

            ElseIf ClientDv Is Nothing Then
                Me.lblUnMappedCustomers.ForeColor = Drawing.Color.Red
                Me.lblUnMappedCustomers.Text = "Cannot Connect to Solution database. Please contact Database Administrator"
            End If
            If DepotDV.Count <> 0 Then
                With Me.dgViewUnMappedDeports
                    .DataSource = DepotDV
                    .DataBind()
                End With

            ElseIf DepotDV.Count = 0 Then
                Me.lblUnmappedDepots.ForeColor = Drawing.Color.Green
                Me.lblUnmappedDepots.Text = "Congratulations, you do not have any unmapped Depots. Please Remember to map all Newly created Depots in ELM to their Corresponding SAP Storage Locations"

            ElseIf DepotDV Is Nothing Then
                Me.lblUnmappedDepots.ForeColor = Drawing.Color.Red
                Me.lblUnmappedDepots.Text = "Cannot Connect to Solution database. Please contact Database Administrator"
            End If




            Dim StartDate As String = Me.txtStartDate.Text.ToString & " 00:00:00.000"
            Dim EndDate As String = Me.txtEndDate.Text.ToString & " 23:59:59.900"
            Dim ClientID As String = Me.ddlClient.SelectedValue.ToString

            'Get Distinct Documents By Date and Insert into a Temporary table that i will Truncate Later
            Dim TempDv As DataView
            If Me.ddlClient.SelectedIndex <> 0 Then

                TempDv = mydata2.LoadAllSplitedDocumentsByClient(StartDate, EndDate, ClientID)
                If TempDv.Count > 0 Then
                    For i As Integer = 0 To TempDv.Count - 1



                        Dim TempInsert As New TableObjects.TEMP_ORDER_SPLITING
                        With TempInsert
                            .NEW_DOCUMENT_NUMBER = TempDv.Item(i).Item("NewDocumentID")
                            .OLD_DOCUMENT_NUMBER = TempDv.Item(i).Item("DocumentNo")
                        End With
                        mydata.InsertTEMP_ORDER_SPLITING(TempInsert)


                    Next

                Else
                    Me.lblMsg.Text = "Please make sure you select the Appropraite date Range as Represented in the ELM Invoice. This can be deduced from the Document Number for that Month"
                    Exit Sub
                End If
            Else
                TempDv = mydata2.LoadAllSplitedDocuments(StartDate, EndDate)
                If TempDv.Count > 0 Then
                    For i As Integer = 0 To TempDv.Count - 1



                        Dim TempInsert As New TableObjects.TEMP_ORDER_SPLITING
                        With TempInsert
                            .NEW_DOCUMENT_NUMBER = TempDv.Item(i).Item("NewDocumentID")
                            .OLD_DOCUMENT_NUMBER = TempDv.Item(i).Item("DocumentNo")
                            .DEPOTID = TempDv.Item(i).Item("DepotID")
                        End With
                        mydata.InsertTEMP_ORDER_SPLITING(TempInsert)


                    Next
                End If

            End If

            If Me.ddlClient.SelectedIndex <> 0 Then

                Dim dv As DataView = mydata2.LoadHeaderData_FromELMByClient(StartDate, EndDate, ClientID)
                Session("_BillNote") = dv

                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Cannot  Connect to ELM at this Time, Please contact your Database Administrator. Err01"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, There is no billing Document, for the Period Selected"
                    Exit Sub
                End If

                If dv.Count > 0 Then
                    With Me.dgViewBillNotes
                        .DataSource = dv
                        .DataBind()
                    End With

                End If
            Else
                Dim dv As DataView = mydata2.LoadHeaderData_FromELM(StartDate, EndDate)
                Session("_BillNote") = dv

                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Cannot  Connect to ELM at this Time, Please contact your Database Administrator. Err01"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, There is no billing Document, for the Period Selected"
                    Exit Sub
                End If

                If dv.Count > 0 Then
                    With Me.dgViewBillNotes
                        .DataSource = dv
                        .DataBind()
                    End With

                End If
            End If

            If ClientDv.Count <> 0 Then
                Me.HideTables()
                Me.tblViewUnMapped.Visible = True

            ElseIf DepotDV.Count <> 0 Then
                Me.HideTables()
                Me.tblViewUnMapped.Visible = True

            Else
                Me.HideTables()
                Me.tblViewExtract.Visible = True

            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try

    End Sub

    Protected Sub HideTables()
        Try
            Me.tblSectionA.Visible = False
            Me.tblViewExtract.Visible = False
            Me.tblViewLineItems.Visible = False
            Me.tblSuccess.Visible = False
            Me.tblViewUnMapped.Visible = False

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Private Function GetLastDayOfMonth(Month As Integer)
        Try
            Dim LastDayOfMonth As Integer = 31
            Select Case Month
                Case 4, 6, 9, 11
                    LastDayOfMonth = 30

                Case 2
                    LastDayOfMonth = 28

                Case 1, 3, 5, 7, 8, 10, 12
                    LastDayOfMonth = 31

            End Select
            Return LastDayOfMonth
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Function

    Private Sub dgViewBillNotes_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgViewBillNotes.ItemCommand
        Try
            Me.dgViewBillNotes.SelectedIndex = e.Item.ItemIndex
            Dim xx As String = CType(Me.dgViewBillNotes.SelectedItem.FindControl("lblDocumentNo"), Label).Text




            Dim DocumentNo As String = xx.ToString

            Dim dv As DataView = mydata2.GetAllLineItemsByDocumentNumberToView(DocumentNo)

            If dv Is Nothing Then
                Me.lblMsg.Text = "Sorry, Cannot  Connect to ELM at this Time, Please contact your Database Administrator. Err01"
                Exit Sub
            End If
            If dv.Count = 0 Then
                Me.lblMsg.Text = "Sorry, There is no billing Document, for the Period Selected"
                Exit Sub
            End If

            If dv.Count > 0 Then
                With Me.dgViewLineItems
                    .DataSource = dv
                    .DataBind()
                    HideTables()
                    Me.tblViewLineItems.Visible = True
                End With
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
       
    End Sub

    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            Dim gfDV As DataView = mydata2.GetCheckData
            If gfDV.Count > 0 Then
                Me.lblMsg.Text = "Oopps! There is a Previous Load that has not been loaded into SAP yet. This Load will be completed by " & System.DateTime.Today.AddMinutes(6)
                Exit Sub
            End If
            Dim UniqueRef As String = (New Utilities).GenerateRefNo
            Dim dv As DataView = Session("_BillNote")
            For i As Integer = 0 To dv.Count - 1

                Dim ClientID As String = dv.Item(i).Item("ClientID").ToString
                Dim cDV As DataView = mydata2.GetCustomerCodeByClientID(ClientID)

                If cDV.Count = 0 Then

                Else




                    ' Posting The Header Document into the staging Header Document Table

                    Dim DocumentNumber As String = dv.Item(i).Item("DocumentNo").ToString
                    'Perform Document Splitting
                    Dim TempDv As DataView = mydata2.GetSplitingByCode(DocumentNumber)

                    If TempDv.Count <> 0 Then
                        For T As Integer = 0 To TempDv.Count - 1
                            Dim SalesOrderNumber As String = Left((New Utilities).GenerateRefNo, 10)
                            Dim SoDeportID As String = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString

                            Dim SoDeportArray As String() = SoDeportID.Split(New Char() {"]"c})

                            SoDeportID = SoDeportArray(1).ToString


                            Dim soDV As DataView = mydata2.GetSalesOfficeByDeportID(SoDeportID)
                            Dim SalesOffice As String
                            Dim ProfitCenter As String
                            If soDV.Count > 0 Then
                                SalesOffice = soDV.Item(0).Item("SALES_OFFICE").ToString
                                ProfitCenter = soDV.Item(0).Item("PROFIT_CENTER").ToString

                            Else
                                SalesOffice = "100"
                            End If

                            'Insert the Head First
                            Dim sending As New TableObjects.SALES_ORDER_HEADER
                            With sending
                                .DIST_CHANNEL = "70"
                                .DIVISION = "70"
                                .DOCUMENT_TYPE = "SPO"
                                .INCOTERMS1 = "EXW"
                                .INCOTERMS2 = "EX WORKS"
                                .IS_LOADED = 0
                                .PURCHASE_DATE = Session("_SalesOrderDate")
                                .REQ_DATE_H = Session("_SalesOrderDate")
                                .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                .SALES_ORGANIZATION = "7000"
                                .TIMESTAMP = System.DateTime.Now.ToString
                                .UNIQUEREF = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString
                                Dim NewDocNo As String = .UNIQUEREF

                                Dim minDV As DataView = mydata2.LoadMinimumFixedAmount(NewDocNo)
                                .MINIMUMAMOUNT = CDbl(minDV.Item(0).Item("MinimumAmount"))
                                .RATEDAMOUNT = CDbl(minDV.Item(0).Item("RatedAmount"))
                                .OFFSET = CDbl(CDbl(.RATEDAMOUNT) - CDbl(.MINIMUMAMOUNT))
                                .SALES_OFFICE = SalesOffice.ToString


                            End With
                            Dim SuccessChecker As Integer = mydata.InsertSALES_ORDER_HEADER(sending)

                            'Inserting into Header end Here




                            If SuccessChecker > 0 Then
                                ' Inserting into Partner Table


                                Dim CustomerCode As String = cDV.Item(0).Item("SAP_CLIENT_ID").ToString

                                Dim PartnerType() As String = {"AG", "WE", "VE"}

                                For Each Partner As String In PartnerType

                                    If Partner = "AG" Then
                                        Dim sendingPartnerItem As New TableObjects.SALES_ORDER_PARTNER


                                        With sendingPartnerItem
                                            .COUNTRY = "NG"
                                            .IS_LOADED = 0
                                            .PARTNER_NUMBER = CustomerCode.ToString
                                            .PARTNER_ROLES = "AG"
                                            .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                            .TIMESTAMP = System.DateTime.Now
                                            .UNIQUEREF = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString

                                        End With

                                        mydata.InsertSALES_ORDER_PARTNER(sendingPartnerItem)
                                    End If

                                    If Partner = "WE" Then
                                        Dim sendingPartnerItem As New TableObjects.SALES_ORDER_PARTNER


                                        With sendingPartnerItem
                                            .COUNTRY = "NG"
                                            .IS_LOADED = 0
                                            .PARTNER_NUMBER = CustomerCode.ToString
                                            .PARTNER_ROLES = "WE"
                                            .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                            .TIMESTAMP = System.DateTime.Now
                                            .UNIQUEREF = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString

                                        End With

                                        mydata.InsertSALES_ORDER_PARTNER(sendingPartnerItem)
                                    End If

                                    If Partner = "VE" Then
                                        Dim DeportID As String = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString

                                        Dim DeportArray As String() = DeportID.Split(New Char() {"]"c})

                                        DeportID = DeportArray(1).ToString


                                        Dim personnelNumber As String
                                        Dim pDV As DataView = mydata2.GetpersonnelIDByStaffName(DeportID)
                                        If pDV.Count = 0 Then
                                            personnelNumber = "10001453"
                                        Else
                                            personnelNumber = pDV.Item(0).Item("PERSONNELID").ToString
                                        End If


                                        Dim sendingPartnerItem As New TableObjects.SALES_ORDER_PARTNER


                                        With sendingPartnerItem
                                            .COUNTRY = "NG"
                                            .IS_LOADED = 0
                                            .PARTNER_NUMBER = personnelNumber.ToString
                                            .PARTNER_ROLES = "VE"
                                            .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                            .TIMESTAMP = System.DateTime.Now
                                            .UNIQUEREF = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString

                                        End With

                                        mydata.InsertSALES_ORDER_PARTNER(sendingPartnerItem)
                                    End If

                                Next

                                Dim counter As Integer = 0

                                ' If the Insertion of that document is successful at the header level then we have to insert the line 
                                'Items into the lineItems Table
                                Dim NewDocumentNumber As String = TempDv.Item(T).Item("NEW_DOCUMENT_NUMBER").ToString
                                Dim lDV As DataView = mydata2.GetAllLineItemsByDocumentNumber(NewDocumentNumber)
                                For j As Integer = 0 To lDV.Count - 1

                                    Dim bDV As DataView = mydata2.GetAllBillingLines(NewDocumentNumber)
                                    If bDV.Count = 0 Then


                                    Else
                                        For b As Integer = 0 To bDV.Count - 1


                                            'Check if the Material and the storage location exist in the system
                                            Dim MaterialCode As String = bDV.Item(b).Item("BillItem").ToString
                                            Dim DeportID As String = bDV.Item(b).Item("DepotID").ToString
                                            'Dim Description As String = lDV.Item(j).Item("ProductMeasure").ToString & " | " & lDV.Item(j).Item("ProductOrServiceDesc").ToString & " | Volume:" & lDV.Item(j).Item("VolumeComputed").ToString & ", " & lDV.Item(j).Item("VolumeType").ToString
                                            Dim uDV As DataView = mydata2.GetMaterialMaster(MaterialCode)
                                            If uDV.Count = 0 Then


                                            Else
                                                Dim pDV As DataView = mydata2.GetStorageLocatioBYDeportID(DeportID)
                                                If pDV.Count = 0 Then

                                                Else
                                                    'Load Line Items, Schedule Lines and Conditions
                                                    Dim kk As String
                                                    Dim LineItemNumbers As String = String.Empty
                                                    If counter = 0 Then
                                                        counter = counter + 1
                                                        kk = counter.ToString
                                                        kk = kk & "0"
                                                    Else
                                                        counter = counter + 1
                                                        kk = counter & "0"
                                                    End If
                                                    LineItemNumbers = kk.ToString
                                                    LineItemNumbers = LineItemNumbers.PadLeft(6, "0")

                                                    'kk = kk + 10


                                                    Dim SendingLineItem As New TableObjects.SALES_ORDER_ITEM
                                                    With SendingLineItem
                                                        .BILLDATE = Session("_SalesOrderDate")
                                                        .INCOTERM1 = "EXW"
                                                        .INCOTERM2 = "EX WORKS"
                                                        .IS_LOADED = 0
                                                        .MATERIAL = uDV.Item(0).Item("MATERIAL_CODE").ToString
                                                        .PLANT = "7000"
                                                        .PO_ITM_NO = NewDocumentNumber.ToString
                                                        .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                                        Dim kDV As DataView = mydata2.GetStorageLocatioBYDeportID(DeportID)
                                                        If kDV.Count = 0 Then
                                                            .STORAGE_LOCATION = ""

                                                        Else
                                                            .STORAGE_LOCATION = kDV.Item(0).Item("SAP_STORAGE_LOC_ID")

                                                        End If
                                                        .TIMESTAMP = System.DateTime.Now.ToString
                                                        .UNIQUEREF = NewDocumentNumber.ToString
                                                        .SALES_ORDER_LINE_ITEMS = LineItemNumbers.ToString
                                                        Dim prDV As DataView = mydata2.GetSalesOfficeByDeportID(DeportID)
                                                        If prDV.Count = 0 Then
                                                            .PROFIT_CENTER = ""

                                                        Else
                                                            .PROFIT_CENTER = prDV.Item(0).Item("PROFIT_CENTER")

                                                        End If


                                                    End With
                                                    Dim SuccessLine As Integer = mydata.InsertSALES_ORDER_ITEM(SendingLineItem)
                                                    If SuccessLine > 0 Then


                                                        ' Inserting into the Schedule Lines Table
                                                        Dim ScheduleLinesSending As New TableObjects.SALES_ORDER_SCHEDULELINES
                                                        With ScheduleLinesSending
                                                            .IS_LOADED = 0
                                                            .REQ_DATE = Session("_SalesOrderDate")
                                                            .REQ_QUANTITY = 1
                                                            .SALES_DOCUMENT_ITEM = LineItemNumbers.ToString
                                                            .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                                            .SCHEDULE_LINES = "01"
                                                            .TIMESTAMP = System.DateTime.Now.ToString
                                                            .UNIQUEREF = NewDocumentNumber.ToString

                                                        End With
                                                        mydata.InsertSALES_ORDER_SCHEDULELINE(ScheduleLinesSending)

                                                        ' Inserting into the Condition  Table

                                                        Dim Condition As New TableObjects.SALES_ORDER_CONDITION_ITEM
                                                        With Condition
                                                            .CONDITION_COUNTER = "0"
                                                            .CONDITION_ITEM_NUMBER = LineItemNumbers.ToString
                                                            .CONDITION_PRICING_UNIT = "1"
                                                            .CONDITION_RATE = bDV.Item(b).Item("RatedAmount")
                                                            .CONDITION_TYPE = "ZPR0"


                                                            Dim UnitOfMeasurment As String = uDV.Item(0).Item("UNIT_OF_MEASURE").ToString
                                                            .CONDITION_UNIT = NewDocumentNumber.ToString
                                                            .CURRENCY_KEY = "NGN"
                                                            .SALES_DOCUMENT_ITEM = LineItemNumbers.ToString
                                                            .SALES_DOCUMENT_NUMBER = SalesOrderNumber.ToString
                                                            .STEP_NUMBER = "17"

                                                        End With
                                                        mydata.InsertSALES_ORDER_CONDITION_ITEM(Condition)
                                                    End If
                                                End If
                                            End If
                                        Next
                                    End If

                                Next

                            End If
                        Next

                    End If





                End If
            Next
            'Load Offset of Minimum Fixed Charge into Minimum Fixed Charge Material
            Dim LineItem As String = String.Empty
            Dim LineItemNumber As Integer
            Dim OffDV As DataView = mydata2.GetAllOffsetInformations
            If OffDV.Count > 0 Then
                For p As Integer = 0 To OffDV.Count - 1
                    Dim SendingLineItem As New TableObjects.SALES_ORDER_ITEM
                    With SendingLineItem
                        .BILLDATE = Session("_SalesOrderDate")
                        .INCOTERM1 = "EXW"
                        .INCOTERM2 = "EX WORKS"
                        .IS_LOADED = 0
                        .MATERIAL = "FIXED MINIMUM CHAR"
                        .PLANT = "7000"
                        .PO_ITM_NO = OffDV.Item(p).Item("UniqueRef").ToString
                        .SALES_DOCUMENT_NUMBER = OffDV.Item(p).Item("SALES_DOCUMENT_NUMBER").ToString
                        Dim DocNumb As String = OffDV.Item(p).Item("UniqueRef").ToString


                        Dim DeportArray As String() = DocNumb.Split(New Char() {"]"c})

                        DocNumb = DeportArray(1).ToString


                        Dim kDV As DataView = mydata2.GetStorageLocatioBYDeportID(DocNumb)
                        If kDV.Count = 0 Then
                            .STORAGE_LOCATION = ""

                        Else
                            .STORAGE_LOCATION = kDV.Item(0).Item("SAP_STORAGE_LOC_ID")

                        End If
                        .TIMESTAMP = System.DateTime.Now.ToString
                        .UNIQUEREF = .PO_ITM_NO
                        Dim linDV As DataView = mydata2.GetLastLineItemNumber(OffDV.Item(p).Item("SALES_DOCUMENT_NUMBER").ToString)
                        If linDV.Count = 0 Then
                            LineItem = 0

                        Else
                            LineItem = linDV.Item(0).Item("SALES_ORDER_LINE_ITEMS")
                        End If

                        LineItemNumber = CInt(IIf(IsNumeric(LineItem), LineItem, 0))
                        LineItemNumber = LineItemNumber + 10
                        LineItem = LineItemNumber.ToString
                        LineItem = LineItem.PadLeft(6, "0")
                        .SALES_ORDER_LINE_ITEMS = LineItem.ToString
                        Dim prDV As DataView = mydata2.GetSalesOfficeByDeportID(DocNumb)
                        If prDV.Count = 0 Then
                            .PROFIT_CENTER = ""

                        Else
                            .PROFIT_CENTER = prDV.Item(0).Item("PROFIT_CENTER")

                        End If


                    End With
                    Dim SuccessLine As Integer = mydata.InsertSALES_ORDER_ITEM(SendingLineItem)

                    'Inserting into Schedule Lines
                    Dim ScheduleLinesSending As New TableObjects.SALES_ORDER_SCHEDULELINES
                    With ScheduleLinesSending
                        .IS_LOADED = 0
                        .REQ_DATE = Session("_SalesOrderDate")
                        .REQ_QUANTITY = 1
                        .SALES_DOCUMENT_ITEM = LineItem.ToString
                        .SALES_DOCUMENT_NUMBER = OffDV.Item(p).Item("SALES_DOCUMENT_NUMBER").ToString
                        .SCHEDULE_LINES = "01"
                        .TIMESTAMP = System.DateTime.Now.ToString
                        .UNIQUEREF = OffDV.Item(p).Item("UniqueRef").ToString

                    End With
                    mydata.InsertSALES_ORDER_SCHEDULELINE(ScheduleLinesSending)


                    ' Inserting into the Condition  Table

                    Dim Condition As New TableObjects.SALES_ORDER_CONDITION_ITEM
                    With Condition
                        .CONDITION_COUNTER = "0"
                        .CONDITION_ITEM_NUMBER = LineItem.ToString
                        .CONDITION_PRICING_UNIT = "1"
                        Dim Amount As Double = OffDV.Item(p).Item("OFFSET")
                        Amount = Amount * -1
                        .CONDITION_RATE = Amount.ToString
                        .CONDITION_TYPE = "ZPR0"
                        Dim UnitOfMeasurment As String = "AU"
                        .CONDITION_UNIT = OffDV.Item(p).Item("UniqueRef").ToString
                        .CURRENCY_KEY = "NGN"
                        .SALES_DOCUMENT_ITEM = LineItem.ToString
                        .SALES_DOCUMENT_NUMBER = OffDV.Item(p).Item("SALES_DOCUMENT_NUMBER").ToString
                        .STEP_NUMBER = "17"

                    End With
                    mydata.InsertSALES_ORDER_CONDITION_ITEM(Condition)
                Next
            End If
            mydata.TruncateTempTable()
            mydata.DeleteUnwanted()
            HideTables()
            Me.tblSuccess.Visible = True


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnExtract3_Click(sender As Object, e As EventArgs) Handles btnExtract3.Click
        Try
            HideTables()
            Me.tblSectionA.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnExtract1_Click(sender As Object, e As EventArgs) Handles btnExtract1.Click
        Try
            HideTables()
            Me.tblViewExtract.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnExtract2_Click(sender As Object, e As EventArgs) Handles btnExtract2.Click
        Try
            HideTables()
            Me.tblSectionA.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnExtract4_Click(sender As Object, e As EventArgs) Handles btnExtract4.Click
        Try
            HideTables()
            Me.tblViewExtract.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnExtract5_Click(sender As Object, e As EventArgs) Handles btnExtract5.Click
        Try
            HideTables()
            Me.tblSectionA.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

   
End Class