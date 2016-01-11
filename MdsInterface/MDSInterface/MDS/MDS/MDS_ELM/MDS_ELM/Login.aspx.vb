Public Class Login
    Inherits System.Web.UI.Page
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            Dim EmailAddress As String = Me.txtUsername.Text.ToString
            Dim Password As String = Me.txtPassword.Text.ToString
            Dim dv As DataView = mydata2.GetLoginUser(EmailAddress, Password)
            If dv Is Nothing Then
                Me.lblMsg.Text = "Sorry you cannot login at the moment. Please try again"
            End If

            If dv.Count = 0 Then
                Me.lblMsg.Text = "Username and Password does not match any database record. Please contact Admin"
            End If

            If dv.Count > 0 Then
                Dim siteurl As String = System.Configuration.ConfigurationManager.AppSettings("SiteURL")
                Response.Redirect(siteurl, False)
                Session("Username") = dv.Item(0).Item("EMAIL").ToString
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class