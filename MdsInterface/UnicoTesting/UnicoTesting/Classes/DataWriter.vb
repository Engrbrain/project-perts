Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System
Imports System.Data
Public Class DataWriter
    Private cn As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Private bn As String = System.Configuration.ConfigurationManager.AppSettings("cnectionString_Prod")

    Public Function InsertGL_TRS(ByVal AppObj As TableObjects.GL_TRS) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ACCT_NO", AppObj.ACCT_NO), _
                                        New SqlParameter("@BATCH_NO", AppObj.BATCH_NO), _
                                        New SqlParameter("@COMPANY_ID", AppObj.COMPANY_ID), _
                                        New SqlParameter("@CURRENCY_CODE", AppObj.CURRENCY_CODE), _
                                        New SqlParameter("@DateOfExtract", AppObj.DateOfExtract), _
                                        New SqlParameter("@DEPT_ID", AppObj.DEPT_ID), _
                                        New SqlParameter("@EMPLOYEE_CODE", AppObj.EMPLOYEE_CODE), _
                                        New SqlParameter("@ENTRY_DATE", AppObj.ENTRY_DATE), _
                                        New SqlParameter("@EXCHANGE_RATE", AppObj.EXCHANGE_RATE), _
                                        New SqlParameter("@ExtractReference", AppObj.ExtractReference), _
                                        New SqlParameter("@LOCKED", AppObj.LOCKED), _
                                        New SqlParameter("@PERIOD", AppObj.PERIOD), _
                                        New SqlParameter("@PREV_PERIOD", AppObj.PREV_PERIOD), _
                                        New SqlParameter("@PROCESSED", AppObj.PROCESSED), _
                                        New SqlParameter("@SEQ", AppObj.SEQ), _
                                        New SqlParameter("@TRS_AMT", AppObj.TRS_AMT), _
                                        New SqlParameter("@TRS_DATE", AppObj.TRS_DATE), _
                                        New SqlParameter("@TRS_DESC", AppObj.TRS_DESC), _
                                        New SqlParameter("@TRS_ID", AppObj.TRS_ID), _
                                        New SqlParameter("@TRS_PRT", AppObj.TRS_PRT), _
                                        New SqlParameter("@TRS_REF", AppObj.TRS_REF), _
                                        New SqlParameter("@TRS_SYSTEM", AppObj.TRS_SYSTEM), _
                                        New SqlParameter("@UNIT_CODE", AppObj.UNIT_CODE), _
                                        New SqlParameter("@SAP_GL_ACCOUNT", AppObj.SAP_GL_ACCOUNT), _
                                        New SqlParameter("@USERNAME", AppObj.USERNAME), _
                                        New SqlParameter("@UseFlag", AppObj.UseFlag)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertGL_TRS", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertUNICO_MAPPING_TABLE(ByVal AppObj As TableObjects.UNICO_MAPPING_TABLE) As Integer
        Dim i As Integer


        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ACCT_NO", AppObj.ACCT_NO), _
                                        New SqlParameter("@BATCH_NO", AppObj.BATCH_NO), _
                                        New SqlParameter("@COMPANY_ID", AppObj.COMPANY_ID), _
                                        New SqlParameter("@CURRENCY_CODE", AppObj.CURRENCY_CODE), _
                                        New SqlParameter("@DateOfExtract", AppObj.DateOfExtract), _
                                        New SqlParameter("@DEPT_ID", AppObj.DEPT_ID), _
                                        New SqlParameter("@EMPLOYEE_CODE", AppObj.EMPLOYEE_CODE), _
                                        New SqlParameter("@ENTRY_DATE", AppObj.ENTRY_DATE), _
                                        New SqlParameter("@EXCHANGE_RATE", AppObj.EXCHANGE_RATE), _
                                        New SqlParameter("@ExtractReference", AppObj.ExtractReference), _
                                        New SqlParameter("@LOCKED", AppObj.LOCKED), _
                                        New SqlParameter("@PERIOD", AppObj.PERIOD), _
                                        New SqlParameter("@PREV_PERIOD", AppObj.PREV_PERIOD), _
                                        New SqlParameter("@PROCESSED", AppObj.PROCESSED), _
                                        New SqlParameter("@SEQ", AppObj.SEQ), _
                                        New SqlParameter("@TRS_AMT", AppObj.TRS_AMT), _
                                        New SqlParameter("@TRS_DATE", AppObj.TRS_DATE), _
                                        New SqlParameter("@TRS_DESC", AppObj.TRS_DESC), _
                                        New SqlParameter("@TRS_ID", AppObj.TRS_ID), _
                                        New SqlParameter("@TRS_PRT", AppObj.TRS_PRT), _
                                        New SqlParameter("@TRS_REF", AppObj.TRS_REF), _
                                        New SqlParameter("@TRS_SYSTEM", AppObj.TRS_SYSTEM), _
                                        New SqlParameter("@UNIT_CODE", AppObj.UNIT_CODE), _
                                        New SqlParameter("@SAP_GL_ACCOUNT", AppObj.SAP_GL_ACCOUNT), _
                                        New SqlParameter("@USERNAME", AppObj.USERNAME), _
                                        New SqlParameter("@DocumentNumber", AppObj.DocumentNumber), _
                                        New SqlParameter("@UseFlag", AppObj.UseFlag)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertUNICO_MAPPING_TABLE", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function



    Public Function InsertUNICO_BAD_TABLE(ByVal AppObj As TableObjects.UNICO_BAD_TABLE) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ACCT_NO", AppObj.ACCT_NO), _
                                        New SqlParameter("@BATCH_NO", AppObj.BATCH_NO), _
                                        New SqlParameter("@COMPANY_ID", AppObj.COMPANY_ID), _
                                        New SqlParameter("@CURRENCY_CODE", AppObj.CURRENCY_CODE), _
                                        New SqlParameter("@DateOfExtract", AppObj.DateOfExtract), _
                                        New SqlParameter("@DEPT_ID", AppObj.DEPT_ID), _
                                        New SqlParameter("@EMPLOYEE_CODE", AppObj.EMPLOYEE_CODE), _
                                        New SqlParameter("@ENTRY_DATE", AppObj.ENTRY_DATE), _
                                        New SqlParameter("@EXCHANGE_RATE", AppObj.EXCHANGE_RATE), _
                                        New SqlParameter("@ExtractReference", AppObj.ExtractReference), _
                                        New SqlParameter("@LOCKED", AppObj.LOCKED), _
                                        New SqlParameter("@PERIOD", AppObj.PERIOD), _
                                        New SqlParameter("@PREV_PERIOD", AppObj.PREV_PERIOD), _
                                        New SqlParameter("@PROCESSED", AppObj.PROCESSED), _
                                        New SqlParameter("@SEQ", AppObj.SEQ), _
                                        New SqlParameter("@TRS_AMT", AppObj.TRS_AMT), _
                                        New SqlParameter("@TRS_DATE", AppObj.TRS_DATE), _
                                        New SqlParameter("@TRS_DESC", AppObj.TRS_DESC), _
                                        New SqlParameter("@TRS_ID", AppObj.TRS_ID), _
                                        New SqlParameter("@TRS_PRT", AppObj.TRS_PRT), _
                                        New SqlParameter("@TRS_REF", AppObj.TRS_REF), _
                                        New SqlParameter("@TRS_SYSTEM", AppObj.TRS_SYSTEM), _
                                        New SqlParameter("@UNIT_CODE", AppObj.UNIT_CODE), _
                                        New SqlParameter("@SAP_GL_ACCOUNT", AppObj.SAP_GL_ACCOUNT), _
                                        New SqlParameter("@USERNAME", AppObj.USERNAME), _
                                        New SqlParameter("@DocumentNumber", AppObj.DocumentNumber), _
                                        New SqlParameter("@UseFlag", AppObj.UseFlag)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertUNICO_BAD_TABLE", params)
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

    Public Function InsertUSER_MANAGER(ByVal AppObj As TableObjects.USER_MANAGER) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@CREATIONDATE", AppObj.CREATIONDATE), _
                                        New SqlParameter("@EMAIL", AppObj.EMAIL), _
                                        New SqlParameter("@FIRSTNAME", AppObj.FIRSTNAME), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE), _
                                        New SqlParameter("@OTHERNAMES", AppObj.OTHERNAMES), _
                                        New SqlParameter("@PASSWORD", AppObj.PASSWORD), _
                                        New SqlParameter("@PHONENUMBER", AppObj.PHONENUMBER), _
                                        New SqlParameter("@REFNO", AppObj.REFNO), _
                                        New SqlParameter("@SURNAME", AppObj.SURNAME), _
                                        New SqlParameter("@USERNAME", AppObj.USERNAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertUSER_MANAGER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function


    Public Function InsertUNICO_GL_MAPPING(ByVal AppObj As TableObjects.UNICO_GL_MAPPING) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DATE_CREATED", AppObj.DATE_CREATED), _
                                        New SqlParameter("@LegacyDescription", AppObj.LegacyDescription), _
                                        New SqlParameter("@LegCode", AppObj.LegCode), _
                                        New SqlParameter("@SAPCode", AppObj.SAPCode), _
                                        New SqlParameter("@SAPDesc", AppObj.SAPDesc)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertUNICO_GL_MAPPING", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertSAPCLIENT(ByVal AppObj As TableObjects.SAPCLIENT) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@CLIENT", AppObj.CLIENT), _
                                        New SqlParameter("@DATE_ADDED", AppObj.DATE_ADDED), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertSAPCLIENT", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function


    Public Function InsertDOCUMENT_TYPE(ByVal AppObj As TableObjects.DOCUMENT_TYPE) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DOCUMENT_TYPE", AppObj.DOCUMENT_TYPE), _
                                        New SqlParameter("@DATE_ADDED", AppObj.DATE_ADDED), _
                                        New SqlParameter("@IS_ACTIVE", AppObj.IS_ACTIVE)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertDOCUMENT_TYPE", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
    Public Function InsertUNICO_DOC_HEADER(ByVal AppObj As TableObjects.UNICO_DOC_HEADER) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@DocumentNumber", AppObj.DocumentNumber), _
                                        New SqlParameter("@ENTRY_DATE", AppObj.ENTRY_DATE), _
                                        New SqlParameter("@TRS_REF", AppObj.TRS_REF), _
                                        New SqlParameter("@PERIOD", AppObj.PERIOD), _
                                        New SqlParameter("@POSTINGDATE", AppObj.POSTINGDATE), _
                                        New SqlParameter("@USERNAME", AppObj.USERNAME)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertUNICO_DOC_HEADER", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
    

    Public Function TRUNCATE_UNICO_MAPPING_TABLE() As Integer

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "TRUNCATE_UNICO_MAPPING_TABLE")

            Return 1
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function TRUNCATE_UNICO_DOC_HEADER() As Integer

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "TRUNCATE_UNICO_DOC_HEADER")

            Return 1
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function



    Public Function TRUNCATE_UNICO_SUM_LOGIC() As Integer

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "TRUNCATE_UNICO_SUM_LOGIC")

            Return 1
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function




    Public Function TRUNCATE_UNICO_BAD_TABLE() As Integer

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "TRUNCATE_UNICO_BAD_TABLE")

            Return 1
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
    Public Function TRUNCATE_GL_TRS() As Integer

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "TRUNCATE_GL_TRS")

            Return 1
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Public Function InsertACCOUNT_RECIEVABLE(ByVal AppObj As TableObjects.ACCOUNT_RECIEVABLE) As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ID", SqlDbType.Int, 4), _
                                        New SqlParameter("@ACCT_NO", AppObj.ACCT_NO), _
                                        New SqlParameter("@BATCH_NO", AppObj.BATCH_NO), _
                                        New SqlParameter("@COMPANY_ID", AppObj.COMPANY_ID), _
                                        New SqlParameter("@CURRENCY_CODE", AppObj.CURRENCY_CODE), _
                                        New SqlParameter("@DateOfExtract", AppObj.DateOfExtract), _
                                        New SqlParameter("@DEPT_ID", AppObj.DEPT_ID), _
                                        New SqlParameter("@EMPLOYEE_CODE", AppObj.EMPLOYEE_CODE), _
                                        New SqlParameter("@ENTRY_DATE", AppObj.ENTRY_DATE), _
                                        New SqlParameter("@EXCHANGE_RATE", AppObj.EXCHANGE_RATE), _
                                        New SqlParameter("@ExtractReference", AppObj.ExtractReference), _
                                        New SqlParameter("@LOCKED", AppObj.LOCKED), _
                                        New SqlParameter("@PERIOD", AppObj.PERIOD), _
                                        New SqlParameter("@PREV_PERIOD", AppObj.PREV_PERIOD), _
                                        New SqlParameter("@PROCESSED", AppObj.PROCESSED), _
                                        New SqlParameter("@SEQ", AppObj.SEQ), _
                                        New SqlParameter("@TRS_AMT", AppObj.TRS_AMT), _
                                        New SqlParameter("@TRS_DATE", AppObj.TRS_DATE), _
                                        New SqlParameter("@TRS_DESC", AppObj.TRS_DESC), _
                                        New SqlParameter("@TRS_ID", AppObj.TRS_ID), _
                                        New SqlParameter("@TRS_PRT", AppObj.TRS_PRT), _
                                        New SqlParameter("@TRS_REF", AppObj.TRS_REF), _
                                        New SqlParameter("@TRS_SYSTEM", AppObj.TRS_SYSTEM), _
                                        New SqlParameter("@UNIT_CODE", AppObj.UNIT_CODE), _
                                        New SqlParameter("@UseFlag", AppObj.UseFlag)}

        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "InsertACCOUNT_RECIEVABLE", params)
            i = CInt(IIf(IsNumeric(params(0).Value), params(0).Value, 0))
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function UpdateACCOUNTRECIEVABLE() As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@ReCID", SqlDbType.Int, 4)}
        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateACCOUNTRECIEVABLE", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function UpdateSAPCLIENT() As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4)}
        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateSAPCLIENT", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function UpdateDOCUMENT_TYPE() As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4)}
        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateDOCUMENT_TYPE", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function

    Function UpdateGL_TRS() As Integer
        Dim i As Integer

        Dim params() As SqlParameter = {New SqlParameter("@RecID", SqlDbType.Int, 4)}
        params(0).Direction = ParameterDirection.Output

        Try
            SqlHelper.ExecuteNonQuery(cn, CommandType.StoredProcedure, "UpdateGL_TRS", params)
            i = CInt(params(0).Value)
            Return i
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
   
End Class
