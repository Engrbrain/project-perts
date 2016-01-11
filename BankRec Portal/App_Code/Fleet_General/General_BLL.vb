Imports Microsoft.VisualBasic
'Imports System.Net.Mail
Imports System.Web.Mail
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports System.Diagnostics
Imports OWC10
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Web.UI.UserControl
Imports DotNetNuke.Security.Roles
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.Common
'Imports CoreDAL =  General_DAL
'Imports Vladsm.Web.UI.WebControls

Public Class General_BLL
    Inherits Entities.Modules.PortalModuleBase
    'This sub accept a datagrid and exports it as excel
    Dim CoreDAL As New General_DAL
    Dim assIDToCompare As Integer
#Region "Generic"

    Public Function Get_Date() As String

        Return Format(CDate(DateAndTime.Now.ToShortDateString.ToUpper), "yyyy/MM/dd")

    End Function

    Public Function Get_Time() As String

        Return Format(DateAndTime.Now, "hh:mm:ss tt").ToUpper

    End Function
    Public Function Date_Unformatted() As String

        Return Format(CDate(DateAndTime.Now.ToShortDateString.ToUpper), "ddMMyyyy")

    End Function
    Public Function Time_Unformatted() As String

        Return Format(DateAndTime.Now, "hhmmss").ToUpper

    End Function

    Public Function ReadToGridGeneric(ByVal filename As String) As DataSet
        Try
            Dim _Context As HttpContext = HttpContext.Current
            Dim connectionstring As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & _Context.Server.MapPath("Upload/" + filename) & ";Extended Properties='Excel 8.0;HDR=YES;'"
            'Dim SelectString As String = "select  ISNULL(RegNo,' ') as RegNo, ISNULL(Surname,' ') as Surname, ISNULL(FirstName,' ') as Firstname, ISNULL(OtherName,' ') as Othername, ISNULL(Sex,' ') as Sex, ISNULL(DateofBirth,' ') as DateofBirth, ISNULL(Class,' ') as Class from [NewStudents$]"
            Dim factory As DbProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb")
            Dim adapter As DbDataAdapter = factory.CreateDataAdapter()
            Dim selectCommand As DbCommand = factory.CreateCommand()
            Dim connection As DbConnection = factory.CreateConnection()

            connection.ConnectionString = connectionstring
            connection.Open()


            Dim dt As System.Data.DataTable = connection.GetSchema("tables")
            Dim tableName As String = dt.Rows(0).Item("TABLE_NAME").ToString()
            connection.Close()

            selectCommand.CommandText = "SELECT * FROM [" + tableName + "]"
            selectCommand.Connection = connection
            adapter.SelectCommand = selectCommand
            Dim ds As DataSet = New DataSet
            adapter.Fill(ds)
            connection.Close()
            Return ds



            'Dim SelectString As String = "select  RegNo,  Surname, Firstname, Othername, Sex,  DateofBirth,  Class from [" & tableName & "$]"
            'Dim myCon As OleDbConnection = New OleDbConnection(connectionstring)
            'If myCon.State = ConnectionState.Closed Then
            '    myCon.Open()
            'End If
            'Dim myCmd As OleDbCommand = New OleDbCommand(SelectString, myCon)
            'Dim myReader As OleDbDataReader = myCmd.ExecuteReader
            'Dim ds As DataSet = convertDataReaderToDataSet_Ordinary(myReader)
            'myReader.Close()
            'Return ds
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        End Try
    End Function


    Public Shared Sub DataGridToExcel(ByVal dgExport As System.Web.UI.WebControls.DataGrid, ByVal response As HttpResponse)


        response.Clear()
        response.AddHeader("content-disposition", "attachment;filename=ReportToExcel.xls")
        response.Charset = ""
        response.Cache.SetCacheability(HttpCacheability.NoCache)
        response.ContentType = "application/vnd.xls"
        Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New HtmlTextWriter(stringWrite)

        dgExport.RenderControl(htmlWrite)
        response.Write(stringWrite.ToString())
        response.End()
    End Sub
    Public Shared Sub WriteLog(ByVal msg As String)
        If Not msg.ToString.Contains("Thread was being aborted") Then
            Dim context As HttpContext = HttpContext.Current
            Dim _path As String
            _path = "~/ErrorLog\ErrorLog.txt"
            Dim path As String = context.Server.MapPath(_path)
            Dim writer As New System.IO.StreamWriter(path, True)
            writer.WriteLine(msg + " " + DateTime.Now.ToString)
            writer.Close()
        End If


    End Sub
    Public Function generateRandom(ByVal StartValue As Integer, ByVal EndValue As Integer) As Integer
        Try
            Return CInt(Int((StartValue * Rnd()) + EndValue))
        Catch ex As Exception
            WriteLog(ex.Message + ex.StackTrace)
        End Try
    End Function
#Region "PSA BLL"
    Public Function ValidateGrid4CheckBoxes(ByVal datagrid1 As DataGrid) As Boolean

        Dim rItem As DataGridItem
        ' Dim itemID As Integer
        Dim indexI As Integer = 0

        Dim ret As Boolean = False
        Dim count As Integer = 0
        'Dim xLog As String
        '''''''''''''''''''''''



        Try
            For Each rItem In datagrid1.Items
                If CType(datagrid1.Items(indexI).FindControl("chkSelect"), CheckBox).Checked Then
                    count += 1
                End If
                indexI += 1
            Next
            If count = 0 Then
                ret = False
            ElseIf count > 0 Then
                ret = True
            End If
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            ret = False
        End Try
        Return ret
    End Function


#End Region



    Public Function SendMail(ByVal _body As String, ByVal _to As String, ByVal _subject As String, ByVal _from As String) As Boolean
        Try
            ' Dim mailserver, userid, password As String
            'Dim ds As DataSet = (New General_DAL).fetch4mail()
            'If Not ds Is Nothing And ds.Tables(0).Rows.Count > 0 Then
            '    mailserver = CType(ds.Tables(0).Rows(0).Item("mailserver"), String)
            '    userid = CType(ds.Tables(0).Rows(0).Item("userid"), String)
            '    password = DecodePwd(CType(ds.Tables(0).Rows(0).Item("password"), String))
            'End If
            'ds.Dispose()
            'Dim mailfrom As MailAddress = New MailAddress(_from)
            'Dim mailto As MailAddress = New MailAddress(_to)

            Dim MMsg As MailMessage = New MailMessage '(mailfrom, mailto)

            MMsg.From = _from
            MMsg.To = _to
            MMsg.Subject = _subject
            MMsg.Body = _body
            MMsg.Priority = MailPriority.High
            MMsg.BodyFormat = MailFormat.Html



            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "192.168.1.120")

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25)

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2)

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1)

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "sadeniyi")

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "nkanagr")

            SmtpMail.SmtpServer = "192.168.1.120"
            SmtpMail.Send(MMsg)



            Return True
        Catch ex As Exception
            WriteLog(ex.Message + "SendMail Module")
            Return False
        End Try

    End Function


    Public Function SendMail_Two(ByVal _body As String, ByVal _to As String, ByVal _subject As String, ByVal _from As String, ByVal BCC As String, ByVal CC As String, ByVal attach As String) As Boolean
        Try
            Dim MMsg As MailMessage = New MailMessage

            MMsg.From = _from
            MMsg.To = _to
            MMsg.Subject = _subject
            MMsg.Body = _body
            If BCC.Trim <> String.Empty Then
                MMsg.Bcc = BCC.ToString
            End If
            If CC.Trim <> String.Empty Then
                MMsg.Cc = CC.ToString
            End If
            MMsg.Priority = MailPriority.High
            MMsg.BodyFormat = MailFormat.Html
            Dim sAttach As String()
            sAttach = attach.Split(";")
            Dim i As Integer
            For i = 1 To sAttach.Length
                MMsg.Attachments.Add(New MailAttachment(sAttach(i - 1)))
            Next


            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "192.168.1.120")

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25)

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2)

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1)

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "sadeniyi")

            MMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "nkanagr")

            SmtpMail.SmtpServer = "192.168.1.120"
            SmtpMail.Send(MMsg)



            Return True
        Catch ex As Exception
            WriteLog(ex.Message + "SendMail Module")
            Return False
        End Try

    End Function

    Public Shared Function ReturnPicturePath(ByVal filePath As String) As String
        Dim _appPhotoImage As String = "ApplicantImages"
        Dim _context As HttpContext = HttpContext.Current
        Dim _forWriting As String

        _forWriting = _appPhotoImage



        Dim mpath As String = _context.Server.MapPath(_appPhotoImage) & "\" & filePath & ".jpg"

        'Dim _Status As Boolean = False
        'Dim binWriter As New BinaryWriter(File.Open(mpath, FileMode.Create))
        'Try
        '    binWriter.Write(picByte)
        '    _Status = True
        'Catch ex As Exception
        '    _Status = False
        '    WriteLog(ex.Message)
        'Finally
        '    binWriter.Close()
        'End Try

        'If _Status Then

        Return "~/" & _forWriting & "\" & filePath & ".jpg"

        'End If
    End Function

#End Region

    Public Function getpayresponse(ByVal resp As String, ByVal paymenttype As Integer, ByRef respdesc As String) As String
        Dim respcodea As String

        Try
            Dim str() As String = resp.Split("&")
            Dim i As Integer
            For i = 1 To str.Length
                Dim hold() As String = str(i - 1).Split("=")
                Select Case paymenttype
                    Case 1
                        If hold(0).ToString.ToUpper = "TCODE" Then
                            respcodea = hold(1).ToString
                        End If
                        If hold(0).ToString.ToUpper = "TVAL" Then
                            respdesc = hold(1).ToString
                        End If
                    Case 2
                        If hold(0).ToString.ToLower = "rspcode" Then
                            respcodea = hold(1).ToString
                        End If
                        If hold(0).ToString.ToLower = "rspdesc" Then
                            respdesc = hold(1).ToString
                        End If
                End Select
            Next
            Return respcodea
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
        End Try
    End Function

End Class

Public Class convertdataview2dataset
    Public Shared Function ConvertDataViewToDataset(ByVal dv As DataView) As DataSet
        'Convert dataview To Dataset
        Dim pdfDt As DataTable
        'Dim _newDs As DataView
        Dim drv As DataRowView
        Dim mynewDs As DataSet
        Try
            'Close The Structure of the View
            pdfDt = dv.Table.Clone

            pdfDt.TableName = "Row"
            'Populate the table with the rows in the view

            For Each drv In dv
                pdfDt.ImportRow(drv.Row)
            Next
            mynewDs = New DataSet(dv.Table.TableName)
            mynewDs.Tables.Add(pdfDt)
        Catch ex As Exception
            mynewDs = Nothing
        End Try

        Return mynewDs

    End Function

End Class

Public Class ExcelUtil

    Public Shared Sub GridviewDataToExcel(ByVal gv As System.Web.UI.WebControls.GridView, ByVal gv1 As System.Web.UI.WebControls.GridView, ByVal res As System.Web.HttpResponse)
        Try
            res.Clear()
            res.AddHeader("content-disposition", "attachment;filename=Sheet1.xls")
            res.Charset = ""
            res.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
            res.ContentType = "application/vnd.xls"
            Dim stringWrite As System.IO.StringWriter = New System.IO.StringWriter
            Dim htmlWrite As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(stringWrite)

            Dim frm As HtmlForm = New HtmlForm
            gv.Controls.Add(frm)
            frm.Attributes("runat") = "server"
            frm.Controls.Add(gv1)
            frm.RenderControl(htmlWrite)

            '  gv.RenderControl(htmlWrite)

            res.Write(stringWrite.ToString)
            res.End()
        Catch ex As Exception
            'Throw New Exception("Cannot Export Gridview Data to Excel: " + ex.Message)
            General_BLL.WriteLog(ex.Message & " : " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Function ExcelToDataSet(ByVal filename As String) As DataSet
        Try
            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" _
            & "Data Source=" & filename _
            & ";" & "Extended Properties=Excel 8.0;"
            Dim objConn As New OleDbConnection(sConnectionString)
            objConn.Open()
            Dim dt As System.Data.DataTable = objConn.GetSchema("tables")
            Dim tableName As String = dt.Rows(0)("TABLE_NAME").ToString
            objConn.Close()
            Dim objAdapter1 As New OleDbDataAdapter()
            Dim objCmdSelect As New OleDbCommand("SELECT * FROM [" + tableName + "]", objConn)
            objAdapter1.SelectCommand = objCmdSelect
            Dim ds As DataSet = New DataSet
            objAdapter1.Fill(ds)
            Return ds
        Catch ex As Exception
            General_BLL.WriteLog("Cannot Import Excel Data to DataSet: " + ex.Message)


        End Try
    End Function

    Public Shared Function convertDataReaderToDataSet(ByVal reader As SqlClient.SqlDataReader) As DataSet
        Try
            Dim dataSet As DataSet = New DataSet
            Do
                Dim schemaTable As DataTable = reader.GetSchemaTable
                Dim dataTable As DataTable = New DataTable
                If Not (schemaTable Is Nothing) Then
                    Dim i As Integer = 0
                    While i < schemaTable.Rows.Count
                        Dim dataRow As DataRow = schemaTable.Rows(i)
                        Dim columnName As String = CType(dataRow("ColumnName"), String)
                        Dim column As DataColumn = New DataColumn(columnName, CType(dataRow("DataType"), Type))
                        dataTable.Columns.Add(column)
                        System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
                    End While
                    dataSet.Tables.Add(dataTable)
                    While reader.Read
                        Dim dataRow As DataRow = dataTable.NewRow
                        Dim k As Integer = 0
                        While k < reader.FieldCount
                            dataRow(k) = reader.GetValue(k)
                            System.Math.Min(System.Threading.Interlocked.Increment(k), k - 1)
                        End While
                        dataTable.Rows.Add(dataRow)
                    End While
                Else
                    Dim column As DataColumn = New DataColumn("RowsAffected")
                    dataTable.Columns.Add(column)
                    dataSet.Tables.Add(dataTable)
                    Dim dataRow As DataRow = dataTable.NewRow
                    dataRow(0) = reader.RecordsAffected
                    dataTable.Rows.Add(dataRow)
                End If
            Loop While reader.NextResult
            Return dataSet
        Catch ex As Exception
            General_BLL.WriteLog(ex.Message + ex.StackTrace)
            Return Nothing
        End Try


    End Function

End Class

