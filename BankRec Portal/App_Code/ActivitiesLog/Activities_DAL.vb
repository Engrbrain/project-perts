Imports Microsoft.VisualBasic
Imports Microsoft.ApplicationBlocks.Data
Namespace YourCompany.Modules.ActivitiesReport
    Public Class Activities_DAL
        Inherits Entities.Modules.PortalModuleBase
        Dim cn As SqlConnection
        Public Sub New()
            cn = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("SiteSqlServer"))
        End Sub
        Public Function fetchLog_All(ByVal startdate As String, ByVal enddate As String) As DataSet
            Try
                Dim params() As SqlParameter = {New SqlParameter("@StartDate", startdate), New SqlParameter("@EndDate", enddate)}
                Dim ds As DataSet
                ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchActivityAll", params)
                Return ds
            Catch ex As Exception
                General_BLL.WriteLog(ex.Message + ex.StackTrace)
                Return Nothing
            End Try
        End Function
        Public Function fetchModules() As DataSet
            Try
                Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchModules")

            Catch ex As Exception
                General_BLL.WriteLog(ex.Message + ex.StackTrace)
                Return Nothing
            End Try
        End Function
        Public Function fetchActions() As DataSet
            Try
                Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchActions")

            Catch ex As Exception
                General_BLL.WriteLog(ex.Message + ex.StackTrace)
                Return Nothing
            End Try
        End Function
        'PSA_FetchActivity_Modules
        Public Function fetchLog_Modules(ByVal startdate As String, ByVal enddate As String, ByVal moduleID As Integer) As DataSet
            Try
                Dim params() As SqlParameter = {New SqlParameter("@StartDate", startdate), New SqlParameter("@EndDate", enddate), New SqlParameter("@ModuleId", moduleID)}
                Dim ds As DataSet
                ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchActivity_Modules", params)
                Return ds
            Catch ex As Exception
                General_BLL.WriteLog(ex.Message + ex.StackTrace)
                Return Nothing
            End Try
        End Function
        'PSA_FetchActivity_Actions
        Public Function fetchLog_Actions(ByVal startdate As String, ByVal enddate As String, ByVal ActionID As Integer) As DataSet
            Try
                Dim params() As SqlParameter = {New SqlParameter("@StartDate", startdate), New SqlParameter("@EndDate", enddate), New SqlParameter("@ActionID", ActionID)}
                Dim ds As DataSet
                ds = SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_FetchActivity_Actions", params)
                Return ds
            Catch ex As Exception
                General_BLL.WriteLog(ex.Message + ex.StackTrace)
                Return Nothing
            End Try
        End Function
        Public Function fetchDetail(ByVal actID As Integer) As DataSet
            Try
                Dim param As SqlParameter = New SqlParameter("@ActionID", actID)
                Return SqlHelper.ExecuteDataset(cn, CommandType.StoredProcedure, "APP_fetchActivityDetail", param)
            Catch ex As Exception
                General_BLL.WriteLog(ex.Message + ex.StackTrace)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace

