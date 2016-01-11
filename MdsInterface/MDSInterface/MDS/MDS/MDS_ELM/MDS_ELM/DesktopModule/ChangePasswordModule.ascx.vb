Public Class ChangePasswordModule
    Inherits System.Web.UI.UserControl

    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Page.IsPostBack = False Then


            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            If (Me.txtConfirmPassword.Text <> Me.txtNewPassword.Text) Then
                Me.lblMsg.ForeColor = Drawing.Color.Red
                Me.lblMsg.Text = "New Password and Confirm Password does not match"
            End If

            If (Session("Username") IsNot Nothing OrElse [String].Empty.Equals(Session("Username"))) Then

                Dim EMAIL As String = Session("Username").ToString
                Dim USER_PASSWORD As String = Me.txtOldPassword.Text.ToString
                Dim dv As DataView = mydata2.ValidateUserOldPassword(EMAIL, USER_PASSWORD)
                If dv.Count = 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Sorry, Your Password does not match this user, Please enter the right password and try again"
                End If
                If dv.Count > 0 Then
                    Dim NewPassword As String = Me.txtNewPassword.Text.ToString
                    Dim UpdatePassword As Integer = mydata.UpdatePasswordByEmailAddress(EMAIL, NewPassword)
                    If UpdatePassword > 0 Then
                        Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                        Response.Redirect(LoginPage, False)
                    End If
                End If

            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class