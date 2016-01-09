Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System
Imports System.Data
Public Class DataWriter
    Private cn As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString_ETL")
    Private bn As String = System.Configuration.ConfigurationManager.AppSettings("cnectionString_Prod")

    

    Public Function InsertSALES_ORDER_HEADER(ByVal AppObj As TableObjects.SALES_ORDER_HEADER) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DIST_CHANNEL", AppObj.DIST_CHANNEL), _
                                        New SqlParameter("@DIVISION", AppObj.DIVISION), _
                                        New SqlParameter("@DOCUMENT_TYPE", AppObj.DOCUMENT_TYPE), _
                                        New SqlParameter("@INCOTERMS1", AppObj.INCOTERMS1), _
                                        New SqlParameter("@INCOTERMS2", AppObj.INCOTERMS2), _
                                        New SqlParameter("@IS_LOADED", AppObj.IS_LOADED), _
                                        New SqlParameter("@PURCHASE_DATE", AppObj.PURCHASE_DATE), _
                                        New SqlParameter("@REQ_DATE_H", AppObj.REQ_DATE_H), _
                                        New SqlParameter("@SALES_DOCUMENT_NUMBER", AppObj.SALES_DOCUMENT_NUMBER), _
                                        New SqlParameter("@SALES_ORGANIZATION", AppObj.SALES_ORGANIZATION), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@MINIMUMAMOUNT", AppObj.MINIMUMAMOUNT), _
                                        New SqlParameter("@RATEDAMOUNT", AppObj.RATEDAMOUNT), _
                                        New SqlParameter("@OFFSET", AppObj.OFFSET), _
                                        New SqlParameter("@SALES_OFFICE", AppObj.SALES_OFFICE), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSALES_ORDER_HEADER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertUSER_REGISTER(ByVal AppObj As TableObjects.USER_REGISTER) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@EMAIL", AppObj.EMAIL), _
                                        New SqlParameter("@FIRST_NAME", AppObj.FIRST_NAME), _
                                        New SqlParameter("@LAST_NAME", AppObj.LAST_NAME), _
                                        New SqlParameter("@PERSONNEL_NUMBER", AppObj.PERSONNEL_NUMBER), _
                                        New SqlParameter("@ROLEFK", AppObj.ROLEFK), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF), _
                                        New SqlParameter("@USER_PASSWORD", AppObj.USER_PASSWORD)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertUSER_REGISTER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertDEPORTMANAGER(ByVal AppObj As TableObjects.DEPORTMANAGERS) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DEPORTID", AppObj.DEPORTID), _
                                        New SqlParameter("@DEPORTNAME", AppObj.DEPORTNAME), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE), _
                                        New SqlParameter("@MANAGERS_FIRST_NAME", AppObj.MANAGERS_FIRST_NAME), _
                                        New SqlParameter("@MANAGERS_LAST_NAME", AppObj.MANAGERS_LAST_NAME), _
                                        New SqlParameter("@PERSONNELID", AppObj.PERSONNELID), _
                                        New SqlParameter("@SAP_NAME", AppObj.SAP_NAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertDEPORTMANAGER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function DeleteDEPORTMANAGERS(ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID)}


        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteDEPORTMANAGERS", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function


    

    Function DeleteCustomer(ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID)}


        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteCustomer", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function DeleteUser(ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID)}


        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteUser", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function DeleteDepot(ByVal Deport_ID As String) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@Deport_ID", Deport_ID)}


        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteDepot", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function DeleteMaterials(ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID)}


        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteMaterials", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function UpdateDEPORTMANAGER(ByVal AppObj As TableObjects.DEPORTMANAGERS, ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID), _
                                        New SqlParameter("@DEPORTID", AppObj.DEPORTID), _
                                        New SqlParameter("@DEPORTNAME", AppObj.DEPORTNAME), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE), _
                                        New SqlParameter("@MANAGERS_FIRST_NAME", AppObj.MANAGERS_FIRST_NAME), _
                                        New SqlParameter("@MANAGERS_LAST_NAME", AppObj.MANAGERS_LAST_NAME), _
                                        New SqlParameter("@PERSONNELID", AppObj.PERSONNELID), _
                                        New SqlParameter("@SAP_NAME", AppObj.SAP_NAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateDEPORTMANAGER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function UpdateSTORAGE_LOC_MAPPING(ByVal AppObj As TableObjects.STORAGE_LOC_MAPPING, ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID), _
                                        New SqlParameter("@DEPORT_ID", AppObj.DEPORT_ID), _
                                        New SqlParameter("@DEPORT_NAME", AppObj.DEPORT_NAME), _
                                        New SqlParameter("@SAP_STORAGE_LOC_ID", AppObj.SAP_STORAGE_LOC_ID), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE), _
                                        New SqlParameter("@SAP_STORAGE_LOC_NAME", AppObj.SAP_STORAGE_LOC_NAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateSTORAGE_LOC_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function UpdateSALES_OFFICE_MAPPING(ByVal AppObj As TableObjects.SALES_OFFICE_MAPPING, ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID), _
                                        New SqlParameter("@DEPORT_ID", AppObj.DEPORT_ID), _
                                        New SqlParameter("@DEPORT_NAME", AppObj.DEPORT_NAME), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE), _
                                        New SqlParameter("@PROFIT_CENTER", AppObj.PROFIT_CENTER), _
                                        New SqlParameter("@SALES_OFFICE", AppObj.SALES_OFFICE)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateSALES_OFFICE_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function



    Public Function UpdateMATERIAL_MASTER(ByVal AppObj As TableObjects.MATERIAL_MASTER, ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID), _
                                        New SqlParameter("@DIVISION", AppObj.DIVISION), _
                                        New SqlParameter("@LEG_CODE", AppObj.LEG_CODE), _
                                        New SqlParameter("@LEG_DESC", AppObj.LEG_DESC), _
                                        New SqlParameter("@LEG_UOM", AppObj.LEG_UOM), _
                                        New SqlParameter("@MATERIAL_CODE", AppObj.MATERIAL_CODE), _
                                        New SqlParameter("@MATERIAL_DESC", AppObj.MATERIAL_DESC), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF), _
                                        New SqlParameter("@UNIT_OF_MEASURE", AppObj.UNIT_OF_MEASURE)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateMATERIAL_MASTER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
    Public Function UpdateCUSTOMER_MAPPING(ByVal AppObj As TableObjects.CUSTOMER_MAPPING, ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID), _
                                        New SqlParameter("@ELM_CLIENT_ID", AppObj.ELM_CLIENT_ID), _
                                        New SqlParameter("@ELM_CLIENT_NAME", AppObj.ELM_CLIENT_NAME), _
                                        New SqlParameter("@SAP_CLIENT_ID", AppObj.SAP_CLIENT_ID), _
                                        New SqlParameter("@SAP_CLIENT_NAME", AppObj.SAP_CLIENT_NAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateCUSTOMER_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function UpdateUSER_REGISTER(ByVal AppObj As TableObjects.USER_REGISTER, ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID), _
                                        New SqlParameter("@EMAIL", AppObj.EMAIL), _
                                        New SqlParameter("@FIRST_NAME", AppObj.FIRST_NAME), _
                                        New SqlParameter("@LAST_NAME", AppObj.LAST_NAME), _
                                        New SqlParameter("@PERSONNEL_NUMBER", AppObj.PERSONNEL_NUMBER), _
                                        New SqlParameter("@ROLEFK", AppObj.ROLEFK), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateUSER_REGISTER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function



    Public Function UpdatePassword(ByVal ID As Integer) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ID", ID)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdatePassword", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function



    Public Function UpdatePasswordByEmailAddress(ByVal EMAIL As String, ByVal USER_PASSWORD As String) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@EMAIL", EMAIL), _
                                       New SqlParameter("@USER_PASSWORD", USER_PASSWORD)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdatePasswordByEmailAddress", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertTEMP_ORDER_SPLITING(ByVal AppObj As TableObjects.TEMP_ORDER_SPLITING) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@NEW_DOCUMENT_NUMBER", AppObj.NEW_DOCUMENT_NUMBER), _
                                        New SqlParameter("@DEPOTID", AppObj.DEPOTID), _
                                        New SqlParameter("@OLD_DOCUMENT_NUMBER", AppObj.OLD_DOCUMENT_NUMBER)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertTEMP_ORDER_SPLITING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function TruncateTempTable() As Integer
        Dim i As Integer = 0


        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "TruncateTempTable")
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function


    Public Function DeleteAllSalesOrders() As Integer
        Dim i As Integer = 0


        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteAllSalesOrders")
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
    Public Function DeleteUnwanted() As Integer
        Dim i As Integer = 0


        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteUnwanted")
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSALES_ORDER_ITEM(ByVal AppObj As TableObjects.SALES_ORDER_ITEM) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@BILLDATE", AppObj.BILLDATE), _
                                        New SqlParameter("@INCOTERM1", AppObj.INCOTERM1), _
                                        New SqlParameter("@INCOTERM2", AppObj.INCOTERM2), _
                                        New SqlParameter("@IS_LOADED", AppObj.IS_LOADED), _
                                        New SqlParameter("@MATERIAL", AppObj.MATERIAL), _
                                        New SqlParameter("@PLANT", AppObj.PLANT), _
                                        New SqlParameter("@PO_ITM_NO", AppObj.PO_ITM_NO), _
                                        New SqlParameter("@SALES_DOCUMENT_NUMBER", AppObj.SALES_DOCUMENT_NUMBER), _
                                        New SqlParameter("@STORAGE_LOCATION", AppObj.STORAGE_LOCATION), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@SALES_ORDER_LINE_ITEMS", AppObj.SALES_ORDER_LINE_ITEMS), _
                                        New SqlParameter("@PROFIT_CENTER", AppObj.PROFIT_CENTER), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSALES_ORDER_ITEM", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSALES_ORDER_PARTNER(ByVal AppObj As TableObjects.SALES_ORDER_PARTNER) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@COUNTRY", AppObj.COUNTRY), _
                                        New SqlParameter("@IS_LOADED", AppObj.IS_LOADED), _
                                        New SqlParameter("@PARTNER_NUMBER", AppObj.PARTNER_NUMBER), _
                                        New SqlParameter("@PARTNER_ROLES", AppObj.PARTNER_ROLES), _
                                        New SqlParameter("@SALES_DOCUMENT_NUMBER", AppObj.SALES_DOCUMENT_NUMBER), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSALES_ORDER_PARTNER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSALES_OFFICE_MAPPING(ByVal AppObj As TableObjects.SALES_OFFICE_MAPPING) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DEPORT_ID", AppObj.DEPORT_ID), _
                                        New SqlParameter("@DEPORT_NAME", AppObj.DEPORT_NAME), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE), _
                                        New SqlParameter("@PROFIT_CENTER", AppObj.PROFIT_CENTER), _
                                        New SqlParameter("@SALES_OFFICE", AppObj.SALES_OFFICE)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSALES_OFFICE_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
    Public Function InsertMATERIAL_MASTER(ByVal AppObj As TableObjects.MATERIAL_MASTER) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DIVISION", AppObj.DIVISION), _
                                        New SqlParameter("@LEG_CODE", AppObj.LEG_CODE), _
                                        New SqlParameter("@LEG_DESC", AppObj.LEG_DESC), _
                                        New SqlParameter("@LEG_UOM", AppObj.LEG_UOM), _
                                        New SqlParameter("@MATERIAL_CODE", AppObj.MATERIAL_CODE), _
                                        New SqlParameter("@MATERIAL_DESC", AppObj.MATERIAL_DESC), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF), _
                                        New SqlParameter("@UNIT_OF_MEASURE", AppObj.UNIT_OF_MEASURE)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertMATERIAL_MASTER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertCUSTOMER_MAPPING(ByVal AppObj As TableObjects.CUSTOMER_MAPPING) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ELM_CLIENT_ID", AppObj.ELM_CLIENT_ID), _
                                        New SqlParameter("@ELM_CLIENT_NAME", AppObj.ELM_CLIENT_NAME), _
                                        New SqlParameter("@SAP_CLIENT_ID", AppObj.SAP_CLIENT_ID), _
                                        New SqlParameter("@SAP_CLIENT_NAME", AppObj.SAP_CLIENT_NAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertCUSTOMER_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSTORAGE_LOC_MAPPING(ByVal AppObj As TableObjects.STORAGE_LOC_MAPPING) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DEPORT_ID", AppObj.DEPORT_ID), _
                                        New SqlParameter("@DEPORT_NAME", AppObj.DEPORT_NAME), _
                                        New SqlParameter("@SAP_STORAGE_LOC_ID", AppObj.SAP_STORAGE_LOC_ID), _
                                        New SqlParameter("@SAP_STORAGE_LOC_NAME", AppObj.SAP_STORAGE_LOC_NAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSTORAGE_LOC_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSTAFFMAPPING(ByVal AppObj As TableObjects.STAFFMAPPING) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@SAP_PERSONNEL_NUMBER", AppObj.SAP_PERSONNEL_NUMBER), _
                                        New SqlParameter("@STAFFNAME_ELM", AppObj.STAFFNAME_ELM), _
                                        New SqlParameter("@STAFFNAME_SAP", AppObj.STAFFNAME_SAP)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSTAFFMAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSALES_ORDER_SCHEDULELINE(ByVal AppObj As TableObjects.SALES_ORDER_SCHEDULELINES) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@IS_LOADED", AppObj.IS_LOADED), _
                                        New SqlParameter("@REQ_DATE", AppObj.REQ_DATE), _
                                        New SqlParameter("@REQ_QUANTITY", AppObj.REQ_QUANTITY), _
                                        New SqlParameter("@SALES_DOCUMENT_ITEM", AppObj.SALES_DOCUMENT_ITEM), _
                                        New SqlParameter("@SALES_DOCUMENT_NUMBER", AppObj.SALES_DOCUMENT_NUMBER), _
                                        New SqlParameter("@SCHEDULE_LINES", AppObj.SCHEDULE_LINES), _
                                        New SqlParameter("@TIMESTAMP", AppObj.TIMESTAMP), _
                                        New SqlParameter("@UNIQUEREF", AppObj.UNIQUEREF)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSALES_ORDER_SCHEDULELINE", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSALES_ORDER_CONDITION_ITEM(ByVal AppObj As TableObjects.SALES_ORDER_CONDITION_ITEM) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@CONDITION_COUNTER", AppObj.CONDITION_COUNTER), _
                                        New SqlParameter("@CONDITION_ITEM_NUMBER", AppObj.CONDITION_ITEM_NUMBER), _
                                        New SqlParameter("@CONDITION_PRICING_UNIT", AppObj.CONDITION_PRICING_UNIT), _
                                        New SqlParameter("@CONDITION_RATE", AppObj.CONDITION_RATE), _
                                        New SqlParameter("@CONDITION_TYPE", AppObj.CONDITION_TYPE), _
                                        New SqlParameter("@CONDITION_UNIT", AppObj.CONDITION_UNIT), _
                                        New SqlParameter("@CURRENCY_KEY", AppObj.CURRENCY_KEY), _
                                        New SqlParameter("@SALES_DOCUMENT_ITEM", AppObj.SALES_DOCUMENT_ITEM), _
                                        New SqlParameter("@SALES_DOCUMENT_NUMBER", AppObj.SALES_DOCUMENT_NUMBER), _
                                        New SqlParameter("@STEP_NUMBER", AppObj.STEP_NUMBER)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSALES_ORDER_CONDITION_ITEM", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    

    Function DeleteGL_TRS(ByVal Trans_Ref As String) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4), _
                                        New SqlParameter("@Trans_Ref", Trans_Ref)}


        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "DeleteGL_TRS", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function


   
End Class
