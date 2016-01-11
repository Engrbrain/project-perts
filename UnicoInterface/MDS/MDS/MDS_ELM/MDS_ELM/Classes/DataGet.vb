Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System
Imports System.Data

Public Class DataGet

    Private cn As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Private bn As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString_ETL")
    Private dn As String = System.Configuration.ConfigurationManager.AppSettings("UADConnectionString")
    Dim Client As String = System.Configuration.ConfigurationManager.AppSettings("Client")


   

    Public Function LoadHeaderData_FromELM(StartDate As String, EndDate As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "SELECT * FROM dbo.BillNotes where BillFromDate = " & "'" & StartDate & "'" & " and BillToDate =  " & "'" & EndDate & "'"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadELMCustomer() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select DISTINCT '(' + ClientCode + ') ' + ClientName As CustomerInfo, ClientName, ClientCode from Clients"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadELMDepot() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select DISTINCT DepotCode,DepotName from dbo.Depots"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadELMMaterial() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select DISTINCT BillItemCode, BillItemDescription from BillItems"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllSAPMaterial() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select a.MATNR, a.MAKTX from uap.MAKT a, uap.MARA b, uap.MVKE c where a.MATNR = b.MATNR and a.MATNR = c.MATNR and a.MANDT = " & Client & " and b.MANDT = " & Client & "and c.MANDT = " & Client & "and b.SPART = '70'"
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllSAPCustomer() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select DISTINCT '(' + KUNNR + ') ' + NAME1 As CustomerInfo, KUNNR, NAME1 from uap.KNA1 where KUNNR in (Select KUNNR from uap.KNB1 where MANDT = " & Client & " and BUKRS = '7000') and MANDT = " & Client
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Function GetAllSAPProfitCenters() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select PRCTR,LTEXT  from uap.CEPCT where LEFT (PRCTR,3) = 'YB7' and MANDT = " & Client
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetAllSASalesOffice() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select VKBUR,BEZEI from uap.TVKBT where  SPRAS = 'E' and  ISNUMERIC(VKBUR) = 0 and MANDT =" & Client
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function GetAllSAPStorageLocations() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select DISTINCT '(' + LGORT + ') ' + LGOBE As StorageLocationInfo, LGORT, LGOBE from uap.T001L where WERKS = '7000' and MANDT =" & Client
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllPersonnelInformation() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select DISTINCT '(' + PERNR + ') ' + SNAME As PersInfo, PERNR, SNAME from uap.PA0001 where BUKRS = '7000' and MANDT = " & Client
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllProfitCenterInformation() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select PRCTR,  PRCTR + ' - ' + ABTEI as ProfitCenter, ABTEI + ' - ' + VERAK as ProfitCenterDesc from uap.CEPC where MANDT = '374' and LEFT (PRCTR,3) = 'YB7'"
            ds = SqlHelper.ExecuteDataset(dn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    


    Public Function LoadAllSplitedDocuments(StartDate As String, EndDate As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select Distinct dbo.CD_BillDetails.DocumentNo + dbo.CD_BillDetails.DepotID  As 'NewDocumentID', dbo.CD_BillDetails.DepotID, dbo.BillNotes.DocumentNo from dbo.CD_BillDetails inner join dbo.BillNotes on dbo.BillNotes.DocumentNo = CD_BillDetails.DocumentNo where dbo.BillNotes.BillFromDate = " & "'" & StartDate & "'" & " and dbo.BillNotes.BillToDate =  " & "'" & EndDate & "'"

            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadMinimumFixedAmount(NewDocNo As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select * from dbo.CD_BillDetails where DocumentNo +DepotID =" & "'" & NewDocNo & "'"

            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadAllSplitedDocumentsByClient(StartDate As String, EndDate As String, ClientID As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "select Distinct dbo.CD_BillDetails.DocumentNo + dbo.CD_BillDetails.DepotID As 'NewDocumentID', dbo.BillNotes.DocumentNo from dbo.CD_BillDetails inner join dbo.BillNotes on dbo.BillNotes.DocumentNo = CD_BillDetails.DocumentNo where dbo.BillNotes.BillFromDate = " & "'" & StartDate & "'" & " and dbo.BillNotes.BillToDate =  " & "'" & EndDate & "'" & " and dbo.BillNotes.ClientID = " & "'" & ClientID & "'"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllLineItemsByDocumentNumber(DocumentNumber As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select * from CD_BillDetails a where a.DocumentNo + a.DepotID =" & "'" & DocumentNumber & "'"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Function GetAllBillingLines(DocumentNumber As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select SUM(a.RatedAmount) as RatedAmount, a.BillItem, a.DepotID from CDBP_BillDetails a where a.DocumentNo + a.DepotID =" & "'" & DocumentNumber & "'" & "group by a.BillItem,a.DepotID"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetAllLineItemsByDocumentNumberToView(DocumentNumber As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "Select Distinct a.DocumentNo+ a.DepotID as DocumentNumber, a.DocumentNo, a.ClientID, a.DepotID,a.RatedAmount,a.VolumeComputed,a.MinimumAmount,a.FixedAmount,a.AmountUsed from CD_BillDetails a where a.DocumentNo =" & "'" & DocumentNumber & "'"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function LoadHeaderData_FromELMByClient(StartDate As String, EndDate As String, ClientID As String) As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "SELECT * FROM BillNotes where BillFromDate = " & "'" & StartDate & "'" & " and BillToDate =  " & "'" & EndDate & "'" & " and ClientID = " & "'" & ClientID & "'"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function LoadClientData_FromELM() As DataView
        Dim ds As DataSet = Nothing
        Try
            ' Dim params() As SqlParameter = {New SqlParameter("@EmployeeNo", Period)}
            Dim strQuery As String = "SELECT * FROM Clients"
            ds = SqlHelper.ExecuteDataset(cn, CommandType.Text, strQuery)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetStorageLocatioBYDeportID(DeportID As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetStorageLocatioBYDeportID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GETALLCLIENTS() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GETALLCLIENTS")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetMaterialMaster(MATERIAL_CODE As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@MATERIAL_CODE", MATERIAL_CODE)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetMaterialMaster", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetCustomerCodeByClientID(ELM_CLIENT_ID As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@ELM_CLIENT_ID", ELM_CLIENT_ID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetCustomerCodeByClientID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetSplitingByCode(OLD_DOCUMENT_NUMBER As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@OLD_DOCUMENT_NUMBER", OLD_DOCUMENT_NUMBER)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetSplitingByCode", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetCheckData() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@OLD_DOCUMENT_NUMBER", OLD_DOCUMENT_NUMBER)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetCheckData")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetpersonnelIDByStaffName(DeportID As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetpersonnelIDByStaffName", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GetAllDeports() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllDeports")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetAllCustomerMapping() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllCustomerMapping")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllRegisteredUsers() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllRegisteredUsers")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllDepotMapping() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllDepotMapping")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetAllMaterialMapping() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllMaterialMapping")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllDepotManagers() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllDepotManagers")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllPendingSalesOrders() As DataSet
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllPendingSalesOrders")
            If ds Is Nothing Then
                Return Nothing
            End If
            ' Dim dv As DataView = New DataView(ds.Tables(0))
            Return ds
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetSalesOfficeByDeportID(DEPORT_ID As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@DEPORT_ID", DEPORT_ID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetSalesOfficeByDeportID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetAllDepotManagerByID(ID As Integer) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@ID", ID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllDepotManagerByID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllCustomersByID(ID As Integer) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@ID", ID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllCustomersByID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllUsersByID(ID As Integer) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@ID", ID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllUsersByID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllMaterialsByID(ID As Integer) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@ID", ID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllMaterialsByID", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GetAllSearchedSalesPersons(SearchParam As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@SearchParam", SearchParam)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllSearchedSalesPersons", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllSearchedCustomer(SearchParam As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@SearchParam", SearchParam)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllSearchedCustomer", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllSearchedUsers(SearchParam As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@SearchParam", SearchParam)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllSearchedUsers", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function ValidateUserOldPassword(EMAIL As String, USER_PASSWORD As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@EMAIL", EMAIL), _
                                            New SqlParameter("@USER_PASSWORD", USER_PASSWORD)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "ValidateUserOldPassword", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function

    Public Function GetAllSearchedDepot(SearchParam As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@SearchParam", SearchParam)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllSearchedCustomer", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetAllSearchedMaterial(SearchParam As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@SearchParam", SearchParam)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllSearchedMaterial", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetLastLineItemNumber(DocumentNumber As String) As DataView
        Try
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@DocumentNumber", DocumentNumber)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetLastLineItemNumber", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GetAllOffsetInformations() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@DeportID", DeportID)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllOffsetInformations")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetAllUserRoles() As DataView
        Try
            Dim ds As DataSet = Nothing
            ' Dim params() As SqlParameter = {New SqlParameter("@STAFFNAME_ELM", STAFFNAME_ELM)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllUserRoles")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetAllunMappedDeports() As DataView
        Try
            Dim ds As DataSet = Nothing
            ' Dim params() As SqlParameter = {New SqlParameter("@STAFFNAME_ELM", STAFFNAME_ELM)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllunMappedDeports")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function
    Public Function GetAllunMappedCustomers() As DataView
        Try
            Dim ds As DataSet = Nothing
            ' Dim params() As SqlParameter = {New SqlParameter("@STAFFNAME_ELM", STAFFNAME_ELM)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetAllunMappedCustomers")
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function




    


    


    Public Function GetLoginUser(EmailAddress As String, Password As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@EmailAddress", EmailAddress), _
                                            New SqlParameter("@Password", Password)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetLoginUser", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function



    Public Function GetLoginUserFullDetails(EmailAddress As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@EmailAddress", EmailAddress)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetLoginUserFullDetails", params)
            If ds Is Nothing Then
                Return Nothing
            End If
            Dim dv As DataView = New DataView(ds.Tables(0))
            Return dv
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function


    Public Function GetDashBoardReports() As DataSet
        Try

            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@EmailAddress", EmailAddress)}
            ds = SqlHelper.ExecuteDataset(bn, CommandType.StoredProcedure, "GetDashBoardReports")
            If ds Is Nothing Then
                Return Nothing
            End If
            'Dim dv As DataView = New DataView(ds.Tables(0))
            Return ds
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return Nothing
        End Try
    End Function


End Class

