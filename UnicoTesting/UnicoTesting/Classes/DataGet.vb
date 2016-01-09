Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System
Imports System.Data

Public Class DataGet

    Private cn As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")

    Private dn As String = System.Configuration.ConfigurationManager.AppSettings("UADConnectionString")
    Private _PortalSettingCacheTime As Integer = IIf(IsNumeric(Configuration.ConfigurationManager.AppSettings("_PortalSettingCacheTime")), Configuration.ConfigurationManager.AppSettings("_PortalSettingCacheTime"), 1000)

    

    Public Function gettestdata() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "gettestdata")
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



    Public Function GetAllBalancesByReference() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllBalancesByReference")
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


    Public Function GetCurrentCLient() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetCurrentCLient")
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



    Public Function GetCurrentDocumentType() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetCurrentDocumentType")
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
    Public Function GetBalancesSummary() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetBalancesSummary")
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

    Public Function GetLegacyDataForLoad() As DataSet
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetLegacyDataForLoad")
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



    Public Function GetAllGL_TRS() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllGL_TRS")
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
    Public Function GetAllAccRecievable(StartDate As Date, EndDate As Date) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@StartDate", StartDate), _
                                            New SqlParameter("@EndDate", EndDate)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllAccRecievable", params)
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

    Public Function GetAllRepostings(TRS_REF As String, ENTRY_DATE As String, GL_ACCOUNT As String, AMOUNT As Double, DEBIT_CREDIT As String, POSTING_DATE As String, CLIENT As String, MONAT As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@TRS_REF", TRS_REF), _
                                           New SqlParameter("@ENTRY_DATE", ENTRY_DATE), _
                                           New SqlParameter("@GL_ACCOUNT", GL_ACCOUNT), _
                                           New SqlParameter("@AMOUNT", AMOUNT), _
                                           New SqlParameter("@DEBIT_CREDIT", DEBIT_CREDIT), _
                                           New SqlParameter("@POSTING_DATE", POSTING_DATE), _
                                            New SqlParameter("@MONAT", MONAT), _
                                            New SqlParameter("@CLIENT", CLIENT)}
            ds = SqlHelper.ExecuteDataset(dn, CommandType.StoredProcedure, "GetAllRepostings", params)
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

    Public Function GetAllAccountMapping(LegacyAccount As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@LegacyAccount", LegacyAccount)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllAccountMapping", params)
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


    Public Function GetHeaderInfo(ByVal TRS_REF As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@TRS_REF", TRS_REF)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetHeaderInfo", params)
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



    Public Function GetAllReturnAccountMapping(ByVal SAPAccount As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@SAPAccount", SAPAccount)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllReturnAccountMapping", params)
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

    Public Function GetLoginUser(UserName As String, Password As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@UserName", UserName), _
                                            New SqlParameter("@Password", Password)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetLoginUser", params)
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


    Public Function GetAllSybaseInterfaceReport(UserName As String, Month As String, Year As String, CLIENT As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@UserName", UserName), _
                                            New SqlParameter("@Month", Month), _
                                            New SqlParameter("@Year", Year), _
                                            New SqlParameter("@CLIENT", CLIENT)}
            ds = SqlHelper.ExecuteDataset(dn, CommandType.StoredProcedure, "GetAllSybaseInterfaceReport", params)
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

    Public Function GETALLSYBASETECHREPORT(Client As String, StartDate As String, EndDate As String) As DataView
        Try



            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@Client", Client), _
                                            New SqlParameter("@StartDate", StartDate), _
                                            New SqlParameter("@EndDate", EndDate)}
            ds = SqlHelper.ExecuteDataset(dn, CommandType.StoredProcedure, "GETALLSYBASETECHREPORT", params)
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



    Public Function GetAllUnBalancedData(Period As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@Period", Period)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllUnBalancedData", params)
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


    Public Function GetAllSybaseInterfaceReportLineItems(BELNR As String, CLIENT As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@BELNR", BELNR), _
                                            New SqlParameter("@CLIENT", CLIENT)}
            ds = SqlHelper.ExecuteDataset(dn, CommandType.StoredProcedure, "GetAllSybaseInterfaceReportLineItems", params)
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



    Public Function GETALLSYBASETECHREPORTBYDOCNUMBER(Client As String, DOCNUM As String) As DataView
        Try
       
            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@Client", Client), _
                                            New SqlParameter("@DOCNUM", DOCNUM)}
            ds = SqlHelper.ExecuteDataset(dn, CommandType.StoredProcedure, "GETALLSYBASETECHREPORTBYDOCNUMBER", params)
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


    Public Function GetAllBalancesDetails(TRS_REF As String) As DataView
        Try

            Dim ds As DataSet = Nothing
            Dim params() As SqlParameter = {New SqlParameter("@TRS_REF", TRS_REF)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetAllBalancesDetails", params)
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

    Public Function GETALLACCOUNTRECIEVABLES() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GETALLACCOUNTRECIEVABLES")
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



    Public Function GetALLUnicoYear() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetALLUnicoYear")
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

    Public Function GetALLUnicoPeriod() As DataView
        Try
            Dim ds As DataSet = Nothing
            'Dim params() As SqlParameter = {New SqlParameter("@gettestdata", gettestdata)}
            ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "GetALLUnicoPeriod")
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
   
End Class

