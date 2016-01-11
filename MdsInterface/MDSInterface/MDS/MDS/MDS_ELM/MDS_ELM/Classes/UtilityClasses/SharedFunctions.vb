Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data.SqlClient

Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Diagnostics
Imports System.Net
Imports System.Xml


Imports System.Web.UI.WebControls
Imports Microsoft.VisualBasic

Imports System.Web.HttpServerUtility
Imports System.Web.HttpContext

Public Class SharedFunctions
    Public Shared Sub DataGridToExcel(ByVal dgExport As System.Web.UI.WebControls.DataGrid, ByVal response As HttpResponse, Optional ByVal filename As String = "Reports")
        ' Try
        'clean up the response.object
        Dim attachment As String = "attachment; filename=" & filename & ".xls"
        response.Clear()
        response.Charset = ""
        'set the response mime type for excel
        response.AddHeader("content-disposition", attachment)
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        'instantiate a datagrid
        Dim dg As New System.Web.UI.WebControls.DataGrid
        ' just set the input datagrid = to the new dg grid
        dg = dgExport



        ' I want to make sure there are no annoying gridlines
        'dg.GridLines = GridLines.None
        ' Make the header text bold
        dg.HeaderStyle.Font.Bold = True
        'dg.ItemStyle.CssClass = "{font-family: Verdana, Tahoma, Arial; font-size: 8pt; color: #000000}"
        'dg.HeaderStyle.CssClass = ""
        ' If needed, here's how to change colors/formatting at the component level
        'dg.HeaderStyle.ForeColor = System.Drawing.Color.Black
        'dg.ItemStyle.ForeColor = System.Drawing.Color.Black

        'bind the modified datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'output the html
        response.Write(stringWrite.ToString)
        response.End()
        'Catch ex As Exception
        '    debug.write(ex.Message)
        'End Try

    End Sub
    Public Shared Sub DataGridToExcelPosting(ByVal dgExport As System.Web.UI.WebControls.DataGrid, ByVal response As HttpResponse, Optional ByVal filename As String = "Payments")
        ' Try
        'clean up the response.object
        Dim attachment As String = "attachment; filename=" & filename & ".xls"
        response.Clear()
        response.Charset = ""
        'set the response mime type for excel
        response.AddHeader("content-disposition", attachment)
        response.ContentType = "application/vnd.ms-excel"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        'instantiate a datagrid
        Dim dg As New System.Web.UI.WebControls.DataGrid
        ' just set the input datagrid = to the new dg grid
        dg = dgExport



        ' I want to make sure there are no annoying gridlines
        'dg.GridLines = GridLines.None
        ' Make the header text bold
        dg.HeaderStyle.Font.Bold = True
        'dg.ItemStyle.CssClass = "{font-family: Verdana, Tahoma, Arial; font-size: 8pt; color: #000000}"
        'dg.HeaderStyle.CssClass = ""
        ' If needed, here's how to change colors/formatting at the component level
        'dg.HeaderStyle.ForeColor = System.Drawing.Color.Black
        'dg.ItemStyle.ForeColor = System.Drawing.Color.Black

        'bind the modified datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'output the html
        response.Write(stringWrite.ToString)
        response.End()
        'Catch ex As Exception
        '    debug.write(ex.Message)
        'End Try

    End Sub
    Public Shared Sub DataGridToPDF(ByVal dgExport As System.Web.UI.WebControls.DataGrid, ByVal response As HttpResponse, Optional ByVal filename As String = "Reports")
        ' Try
        'clean up the response.object
        Dim attachment As String = "attachment; filename=" & filename & ".pdf"
        response.Clear()
        response.Charset = ""
        'set the response mime type for excel
        response.AddHeader("content-disposition", attachment)
        response.ContentType = "application/pdf"
        'create a string writer
        Dim stringWrite As New System.IO.StringWriter
        'create an htmltextwriter which uses the stringwriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        'instantiate a datagrid
        Dim dg As New System.Web.UI.WebControls.DataGrid
        ' just set the input datagrid = to the new dg grid
        dg = dgExport



        ' I want to make sure there are no annoying gridlines
        'dg.GridLines = GridLines.None
        ' Make the header text bold
        dg.HeaderStyle.Font.Bold = True
        'dg.ItemStyle.CssClass = "{font-family: Verdana, Tahoma, Arial; font-size: 8pt; color: #000000}"
        'dg.HeaderStyle.CssClass = ""
        ' If needed, here's how to change colors/formatting at the component level
        'dg.HeaderStyle.ForeColor = System.Drawing.Color.Black
        'dg.ItemStyle.ForeColor = System.Drawing.Color.Black

        'bind the modified datagrid
        dg.DataBind()
        'tell the datagrid to render itself to our htmltextwriter
        dg.RenderControl(htmlWrite)
        'output the html
        response.Write(stringWrite.ToString)
        response.End()
        'Catch ex As Exception
        '    debug.write(ex.Message)
        'End Try

    End Sub
    Public Shared Function UploadFile(ByRef f As System.Web.UI.WebControls.FileUpload, ByRef message As String, ByRef ExcelfileName As String) As Boolean
        If Not f.HasFile Then
            message = "No file is selected."
            Return False
        End If

        Dim sessionid As String

        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current
        sessionid = context.Session.SessionID

        Dim fileExtension As String = System.IO.Path.GetExtension(f.FileName).ToLower()
        Dim allowedExtensions As String() = {".xls", ".xlsx"}
        Dim counter As Integer

        For counter = 0 To allowedExtensions.Length Step 1
            If fileExtension = allowedExtensions(counter) Then
                ' Dim myFile As System.Drawing.Bitmap = New System.Drawing.(120, 140)
                Dim myFile As String = String.Empty
                Try
                    Dim path As String = context.Server.MapPath("~/UploadedFiles/")
                    Dim filename As String = sessionid & Now.Millisecond.ToString & ".xls"
                    Dim fullpath As String = path & filename
                    f.PostedFile.SaveAs(fullpath)

                    ' myImage = New System.Drawing.Bitmap(fullpath)
                    myFile = fullpath
                    message = fullpath
                    ExcelfileName = filename
                    Return True
                Catch ex As Exception

                    message = "File Cannot Be Uplaoded  at the moment."
                    Return False
                Finally
                    'myImage.Dispose()
                End Try

            End If
        Next

        message = "Invalid File format! The File must be an excel file"
        Return False

    End Function
    Public Shared Function UploadFileNEW(ByRef f As System.Web.UI.WebControls.FileUpload, ByRef message As String, ByRef ExcelfileName As String, ByRef Extension As String, ByRef parth As String, ByRef FulPath As String) As Boolean
        If Not f.HasFile Then
            message = "No file is selected."
            Return False
        End If

        Dim sessionid As String

        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current
        sessionid = context.Session.SessionID

        Dim fileExtension As String = System.IO.Path.GetExtension(f.FileName).ToLower()
        Dim Ext As String = Path.GetExtension(f.PostedFile.FileName)
        Dim nAME As String = Path.GetFileName(f.PostedFile.FileName)
        Dim allowedExtensions As String() = {".xls", ".xlsx"}
        Dim counter As Integer

        For counter = 0 To allowedExtensions.Length Step 1
            If fileExtension = ".xlsx" Then
                message = "File Extension is not allowed, Make sure the file is saved as 97-2003 Worksheet"
                Return False
            End If
            If fileExtension = allowedExtensions(counter) Then
                Dim myFile As String = String.Empty
                Try
                    Dim path As String = context.Server.MapPath("~/UploadedFiles/")
                    Dim filename As String = sessionid & Now.Millisecond.ToString & ".xls"
                    Dim fullpath As String = path & filename
                    f.PostedFile.SaveAs(fullpath)

                    f.PostedFile.SaveAs(fullpath)
                    myFile = fullpath
                    message = fullpath
                    ExcelfileName = filename
                    Extension = Ext
                    parth = path
                    FulPath = fullpath
                    Return True
                Catch ex As Exception

                    message = "File Cannot Be Uplaoded  at the moment." & ex.Message
                    Return False
                Finally

                End Try

            End If
        Next

        message = "Invalid File format! The File must be an excel file"
        Return False

    End Function
    Public Shared Function getImage(ByVal strpath As String) As Byte()
        Try
            Dim htp As HttpContext = HttpContext.Current
            If Len(strpath) <> 0 Then
                Dim fil As FileStream = New FileStream(htp.Server.MapPath(strpath), FileMode.Open, FileAccess.ReadWrite)
                Dim filBty(fil.Length) As Byte
                fil.Read(filBty, 0, fil.Length)
                fil.Close()
                fil = Nothing
                Return filBty
            End If
        Catch ex As Exception

            Return Nothing
        End Try
        Return Nothing
    End Function
    'Public Shared Function GetConnectionString(ByRef msg As String, ByRef PlaceHolder1 As Control, ByRef ctl As UserControl) As String
    '    Try
    '        Dim strpath As String = CStr(ConfigurationManager.AppSettings("ConnectionString_Data_Land"))
    '        If strpath = String.Empty Then
    '            msg = "Connection String Not Set. Please Check the 'Web.Config' and Add the Key as 'ConnectionString_Data_Land'"
    '            Utilities.DisplayMessages = msg
    '            PlaceHolder1.Controls.Add(ctl)
    '            Return String.Empty
    '        End If
    '        If Not CheckIfDBHasBeenCreated(strpath) Then
    '            msg = "Database is not Created!, Please Use the Wizard to Create One"
    '            strpath = ""
    '        End If
    '        GetConnectionString = strpath
    '        Return GetConnectionString
    '    Catch ex As Exception
    '        Return String.Empty
    '    End Try
    '    Return String.Empty
    'End Function
    'Private Shared Function CheckIfDBHasBeenCreated(ByVal cn As String) As Boolean
    '    Try
    '        Dim serv As New StoredProcedureServiceImpl
    '        Dim ds As DataSet = Nothing
    '        With serv

    '            Dim mario As String = "GET_Fac"
    '            ds = .ExecuteProcedureNoParamsReturnDataSet(mario, cn)
    '        End With
    '        If ds Is Nothing Then
    '            'Me.lblerrormessage.Text = "Error Occured! "
    '            Return False
    '        End If
    '        'Dim dv As DataView = New DataView(ds.Tables(0))
    '        Return True
    '    Catch ex As Exception

    '        Return False
    '    End Try

    'End Function


    '#Region "Write To Web.Config"
    '    Public Shared Function AddKey(ByVal sConnectionString As String, ByRef msg As String) As Boolean

    '        Dim FileLocation As String = HttpContext.Current.Server.MapPath("Web.config")
    '        Dim NodeLocation As String = "configuration/appSettings"

    '        'Adds a key and value to the configuration file

    '        Dim strKey As String = "ConnectionString_Data_Land"
    '        Dim strValue As String = sConnectionString

    '        Dim xmlDoc As New XmlDocument


    '        'Change this to the location of your configuration file
    '        xmlDoc.Load(FileLocation)
    '        'Change this if node is different
    '        Dim appSettingsNode As XmlNode = xmlDoc.SelectSingleNode(NodeLocation)
    '        ' Dim nodeAppSettings As XmlNodeList = nodeList(0).ChildNodes
    '        Try
    '            'Check if the node exists before adding it
    '            If (KeyExists(strKey, strValue)) Then
    '                msg = "Done"
    '                Return True
    '                'Throw New ArgumentException("Key name: <" + strKey + "> already exists in the configuration.")
    '            End If

    '            Config.AddAppSetting(xmlDoc, strKey, strValue)
    '            'Save the file
    '            xmlDoc.Save(FileLocation)
    '            msg = "Done"
    '            Return True
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '    End Function

    '    'Determines if a key exists
    '    Private Shared Function KeyExists(ByVal strKey As String, ByVal strValue As String) As Boolean
    '        Try
    '            Dim FileLocation As String = HttpContext.Current.Server.MapPath("Web.config")
    '            Dim NodeLocation As String = "configuration/appSettings"
    '            Dim xmlDoc As New XmlDocument
    '            xmlDoc.Load(FileLocation)
    '            Dim appSettingsNode As XmlNode = xmlDoc.SelectSingleNode(NodeLocation)
    '            Dim childNode As XmlNode
    '            For Each childNode In appSettingsNode
    '                If Not IsNothing(childNode.Attributes) Then
    '                    If (childNode.Attributes("key").Value = strKey) Then
    '                        childNode.Attributes("key").Value = strKey
    '                        childNode.Attributes("value").Value = strValue
    '                        xmlDoc.Save(FileLocation)

    '                        Return True
    '                    End If
    '                End If

    '            Next
    '        Catch ex As Exception
    '            Return False
    '        End Try
    '        Return False
    '    End Function
    '    'Private Shared Function wRITEdoTHERfIE() As Boolean
    '    '    Try
    '    '        Dim fileMap As New ExeConfigurationFileMap()

    '    '        fileMap.ExeConfigFilename = "ConfigTest.exe.config"
    '    '        ' relative path names possible 
    '    '        ' Open another config file 

    '    '        Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None)



    '    '        ' read/write from it as usual 

    '    '        Dim mySection As ConfigurationSection = config.GetSection("mySection")

    '    '        config.SectionGroups.Clear()
    '    '        ' make changes to it 
    '    '        config.Save(ConfigurationSaveMode.Full)
    '    '        ' Save changes 
    '    '    Catch ex As Exception

    '    '    End Try

    '    ' Dim configData As New EditorFontData()

    '    'configData.Name = "Arial" 

    '    'configData.Size = 20 

    '    'configData.Style = 2 



    '    '    ' Write the new configuration data to the XML file 

    '    '    Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

    '    'config.Sections.Remove("EditorSettings") 

    '    'config.Sections.Add("EditorSettings", configData) 

    '    'config.Save() 
    '    'End Function

    '#End Region
End Class
