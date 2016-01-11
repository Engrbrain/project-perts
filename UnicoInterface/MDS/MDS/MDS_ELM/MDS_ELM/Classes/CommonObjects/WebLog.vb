Option Strict On
Imports System
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Web
Imports System.Diagnostics
Imports System.Web.Mail
Imports Microsoft.ApplicationBlocks
Imports System.Net.Mail




Public Class WebException


End Class




' Default exception to be thrown by the website, it will automatically
' log the contents of  the exception to the Windows NT/2000 Application Event Log.
' ---
Public Class AppException
    Inherits System.Exception


    ' Constructors
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    Public Sub New()
        LogError("An unexpected error occurred.")
    End Sub


    Public Sub New(ByVal message As String)
        LogError(message)
    End Sub


    Public Sub New( _
      ByVal message As String, _
      ByVal innerException As Exception)

        LogError(message)

        If Not (innerException Is Nothing) Then
            LogError(innerException.Message.ToString)
        End If

    End Sub



    ' Shared Methods
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    'Public Shared Sub LogError(ByVal Mesage As String)

    'End Sub

    Public Shared Sub LogError(ByVal message As String)
        Dim _HostingServerTimeDiff As Integer
        Dim _LocalDate As String
        Dim _localTime As String
        'Dim _localDateTime As String
        _HostingServerTimeDiff = CInt(System.Configuration.ConfigurationManager.AppSettings("HostingServerTimeDiff"))
        _LocalDate = DateTime.Now.AddHours(_HostingServerTimeDiff).ToString("yyyy/MM/dd")
        _localTime = DateTime.Now.AddHours(_HostingServerTimeDiff).Hour.ToString & ":" & _
          DateTime.Now.Minute.ToString & ":" & _
          DateTime.Now.Second.ToString
        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current

        ' Get location of ErrorLogFile from Web.config file
        Dim filePath As String = context.Server.MapPath( _
          CStr(System.Configuration.ConfigurationManager.AppSettings( _
             "ErrorLogFile")))

        ' Calculate GMT offset
        Dim gmtOffset As Integer = _
          DateTime.Compare(DateTime.Now, DateTime.UtcNow)

        Dim gmtPrefix As String
        If gmtOffset > 0 Then
            gmtPrefix = "+"
        Else
            gmtPrefix = ""
        End If

        ' Create DateTime string
        Dim errorDateTime As String = _LocalDate & " @ " & _localTime

        ' Write message to error file
        Try
            Dim sw As New System.IO.StreamWriter(filePath, True)
            'sw.WriteLine("## " & errorDateTime & " ## " & message & " ##" & " FileLocation: " & PagePath & " ##")
            sw.WriteLine("## " & errorDateTime & " ## " & message & " ##")

            'sw.WriteLine(message)
            'sw.WriteLine()
            sw.Close()
        Catch ex As Exception
            'Send Mail to administrator
            Try
                Dim xxx As New WebExceptionMail
                WebExceptionMail.AdministratorsEmail(ex.Message & "<BR> Thrown Exception Details: <BR>" & message)
            Catch ex2 As Exception
                'Leave this Error to God
                'Debug.Write(ex2.Message)
            End Try
        End Try

    End Sub

    Public Shared Sub LogError(ByVal message As String, ByVal PagePath As String)

        Dim _HostingServerTimeDiff As Integer
        Dim _LocalDate As String
        Dim _localTime As String
        'Dim _localDateTime As String
        _HostingServerTimeDiff = CInt(System.Configuration.ConfigurationManager.AppSettings("HostingServerTimeDiff"))
        _LocalDate = DateTime.Now.AddHours(_HostingServerTimeDiff).ToString("yyyy/MM/dd")
        _localTime = DateTime.Now.AddHours(_HostingServerTimeDiff).Hour.ToString & ":" & _
          DateTime.Now.Minute.ToString & ":" & _
          DateTime.Now.Second.ToString

        ' Get the current HTTPContext
        Dim context As HttpContext = HttpContext.Current

        ' Get location of ErrorLogFile from Web.config file
        Dim filePath As String = context.Server.MapPath( _
          CStr(System.Configuration.ConfigurationManager.AppSettings( _
             "ErrorLogFile")))

        ' Calculate GMT offset
        Dim gmtOffset As Integer = _
          DateTime.Compare(DateTime.Now, DateTime.UtcNow)

        Dim gmtPrefix As String
        If gmtOffset > 0 Then
            gmtPrefix = "+"
        Else
            gmtPrefix = ""
        End If

        ' Create DateTime string
        Dim errorDateTime As String = _LocalDate & " @ " & _localTime

        ' Write message to error file
        Try
            Dim sw As New System.IO.StreamWriter(filePath, True)
            Dim str_message As String
            str_message = "###" & vbCrLf & "[" & errorDateTime & "]" & vbCrLf & "[" & message & "]" & vbCrLf & "{Request Location}: " & PagePath & vbCrLf & "###"
            'sw.WriteLine("## " & errorDateTime & " ## " & message & " ##" & " Request Location: " & PagePath & " ##")
            sw.WriteLine(str_message)
            'sw.WriteLine(message)
            'sw.WriteLine()
            sw.Close()
        Catch ex As Exception
            'Send Mail to administrator
            Try
                Dim xxx As New WebExceptionMail
                WebExceptionMail.AdministratorsEmail(ex.Message & "<BR> Thrown Exception Details: <BR>" & message)
            Catch ex2 As Exception
                'Leave this Error to God
                'Debug.Write(ex2.Message)
            End Try
        End Try

    End Sub

End Class

Public Class WebExceptionMail

    Public Shared Sub AdministratorsEmail(ByVal _Message As String)

        Dim mail As New System.Net.Mail.MailMessage
        Dim _ExchangeAddress As String = System.Configuration.ConfigurationManager.AppSettings("MailServerAddress")
        Dim _SystemEmail As String = System.Configuration.ConfigurationManager.AppSettings("applicationEmailSource")
        Dim _SiteAdminMail As String = System.Configuration.ConfigurationManager.AppSettings("applicationErrorEmailContact")
        Dim _FullName As String = System.Configuration.ConfigurationManager.AppSettings("applicationName")
        Dim _Title As String = System.Configuration.ConfigurationManager.AppSettings("applicationErrorSubject")
        Dim _to As MailAddress = New MailAddress(_SiteAdminMail)
        Dim _From As MailAddress = New MailAddress(_SystemEmail)
        Dim _Body As String = "Date and Time: " & Date.Now.ToLongDateString & "  " & Date.Now.ToShortTimeString & "<BR><BR>" & "Application Name: " & "<STRONG>" & _FullName & "</STRONG>" & "<BR><BR>" & "Message Title: " & _Title & "<BR><BR>" & "Message Body: " & _Message
        'mail.To(0) = _to
        'mail.From = _From
        'mail.IsBodyHtml = True 'MailFormat.Html
        'mail.Subject = _Title

        'mail.Body = "Date and Time: " & Date.Now.ToLongDateString & "  " & Date.Now.ToShortTimeString & "<BR><BR>" & "Application Name: " & "<STRONG>" & _FullName & "</STRONG>" & "<BR><BR>" & "Message Title: " & _Title & "<BR><BR>" & "Message Body: " & _Message
        Try
            '(1) Create the MailMessage instance
            Dim mm As New System.Net.Mail.MailMessage(_SystemEmail, _SiteAdminMail)

            '(2) Assign the MailMessage's properties
            mm.Subject = _Title
            mm.Body = _Body
            mm.IsBodyHtml = True

            '(3) Create the SmtpClient object
            Dim smtp As New SmtpClient(_ExchangeAddress)

            '(4) Send the MailMessage (will use the Web.config settings)
            smtp.Send(mm)
        Catch ex As Exception
            'Debug.Write(ex.Message)
            ' Exit Sub
        End Try

        'Try
        '    '(1) Create the MailMessage instance
        '    Dim mm As New System.Net.Mail.MailMessage(_SystemEmail, _SiteAdminMail)

        '    '(2) Assign the MailMessage's properties
        '    mm.Subject = _Title
        '    mm.Body = _Body
        '    mm.IsBodyHtml = True

        '    '(3) Create the SmtpClient object
        '    Dim smtp As New SmtpClient

        '    '(4) Send the MailMessage (will use the Web.config settings)
        '    smtp.Send(mm)
        'Catch ex As Exception

        'End Try
    End Sub

End Class

Public Class SiteUserLogs
    Private connectionString As String
    Public Sub New()
        connectionString = System.Configuration.ConfigurationManager.AppSettings("connectionString")
    End Sub



End Class

