Public Class FailedToLoad
    Inherits System.Web.UI.Page
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            CheckUserLogin()

        End If
    End Sub


    Protected Sub CheckUserLogin()
        Try
            If (Session("Username") IsNot Nothing OrElse [String].Empty.Equals(Session("Username"))) Then

                Me.lblUserName.Text = Session("Username").ToString

                Dim dv As DataView = mydata2.GetLoginUserFullDetails(Me.lblUserName.Text)
                Me.lblUserFullName.Text = dv.Item(0).Item("FIRST_NAME").ToString & " " & dv.Item(0).Item("LAST_NAME").ToString
            Else


                Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                Response.Redirect(LoginPage, False)
            End If






        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

End Class