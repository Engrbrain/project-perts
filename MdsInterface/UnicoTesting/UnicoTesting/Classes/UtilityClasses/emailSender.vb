Imports Microsoft.VisualBasic
Imports System
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Web

Imports System.Diagnostics
Imports System.IO

Public Class emailSender


    Public Function SendEmailToApplicant(ByVal email As String, ByVal subject As String, Optional ByVal msg As String = "") As Integer
        Try

            Dim message As New MailMessage
            'Dim smtpCredential As New System.Net.NetworkCredential("konaolapo@gmail.com", "green!sh")
            Dim pwd As String = ConfigurationManager.AppSettings("notificationPWD")
            Dim emailer As String = ConfigurationManager.AppSettings("notificationEMAIL")

            Dim smtpCredential As New System.Net.NetworkCredential(emailer, pwd)
            'Dim smtpCredential2 As New System.Net.NetworkCredential(emailer, pwd)
            'System.Configuration.ConfigurationManager.AppSettings("ConnectionString_Data")
            Dim fromAddress As MailAddress = New MailAddress(emailer, "iConcierge Express")
            Dim smtpObj As New SmtpClient
            Dim applicantEmail As String = email

            Dim context As HttpContext = HttpContext.Current



            'first we create the Plain Text part
            'Dim palinBody As String = "This is my plain text content, viewable by"
            'palinBody = palinBody & " those clients that don't support html"
            'Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(palinBody, Nothing, "text/plain")
            ''then we create the Html part
            ''to embed images, we need to use the prefix 'cid' in the img src value
            'Dim htmlBody As String = "<b> " & msg & "</b><DIV>&nbsp;</DIV>"
            'htmlBody += "<img alt="""" hspace=0 src=""cid:uniqueId"" align=baseline border = 0 > """
            'htmlBody += "<DIV>&nbsp;</DIV><b>This is the end of Mail...</b>"
            'Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(htmlBody, Nothing, "text/html")

            'create the AlternateView for embedded image


            'add the views
            'message.AlternateViews.Add(plainView)
            'message.AlternateViews.Add(htmlView)


            With message
                .To.Add(applicantEmail)
                .Subject = subject
                .Body = msg
                .Priority = MailPriority.High
                .From = fromAddress
            End With

            With smtpObj
                .Port = 587
                .UseDefaultCredentials = False
                .Credentials = smtpCredential
                .EnableSsl = False

                .Host = "mail.iconciergeexpress.com"
                '.SendAsync(message, True)
                .Send(message)



            End With
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)

            Return 0
        End Try
    End Function
    Public Function SendEmailToSupport(ByVal email As String, ByVal subject As String, Optional ByVal msg As String = "") As Integer
        Try

            Dim message As New MailMessage
            'Dim smtpCredential As New System.Net.NetworkCredential("konaolapo@gmail.com", "green!sh")
            Dim pwd As String = ConfigurationManager.AppSettings("notificationPWD")
            Dim emailer As String = ConfigurationManager.AppSettings("notificationEMAIL")
            Dim supportemail As String = ConfigurationManager.AppSettings("supportemail")
            Dim smtpCredential As New System.Net.NetworkCredential(emailer, pwd)
            'System.Configuration.ConfigurationManager.AppSettings("ConnectionString_Data")
            Dim fromAddress As MailAddress = New MailAddress(emailer, "bills & me")
            Dim smtpObj As New SmtpClient
            Dim applicantEmail As String = supportemail
            With message
                .To.Add(applicantEmail)
                .Subject = subject
                .Body = msg
                .Priority = MailPriority.High
                .From = fromAddress
            End With
            With smtpObj
                .UseDefaultCredentials = False
                .Credentials = smtpCredential
                .EnableSsl = True
                .Host = "smtp.gmail.com"
                '.SendAsync(message, True)
                .Send(message)
            End With
        Catch ex As Exception
            AppException.LogError(ex.Message, ex.StackTrace.ToString)
            Return 0
        End Try
    End Function
End Class
