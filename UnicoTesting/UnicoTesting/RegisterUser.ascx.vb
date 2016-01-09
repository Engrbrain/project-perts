Public Class RegisterUser
    Inherits System.Web.UI.UserControl

    Private mydata As New DataWriter
    Private mydata2 As New DataGet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Page.IsPostBack = False Then
                CheckUserLogin()

            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub CheckUserLogin()
        Try
            Dim kk As String = Session("Username").ToString
            If Len(kk) > 1 Then
                If kk = "Admin" Then
                    Me.lblUserName = Session("Username")

                Else
                    Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                    Response.Redirect(LoginPage, False)
                End If


            Else
                Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                Response.Redirect(LoginPage, False)

            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Try
            If Me.txtConfirmPassword.Text <> Me.txtPassword.Text Then
                Me.lblMsg.Text = "Sorry, Password Does not match with Confirm Password Field"
                Me.ClearControls()
                Exit Sub
            End If

            Dim Sending As New TableObjects.USER_MANAGER

            With Sending
                .CREATIONDATE = System.DateTime.Now.ToString("MM/dd/yyyy")
                .EMAIL = Me.txtEmail.Text.ToString
                .FIRSTNAME = Me.txtFirstName.Text.ToString
                .IS_ACTIVE = 1
                .OTHERNAMES = Me.txtOtherNames.Text.ToString
                .PASSWORD = Me.txtPassword.Text.ToString
                .PHONENUMBER = Me.txtPhoneNumber.Text.ToString
                .REFNO = (New Utilities).GenerateRefNo
                .SURNAME = Me.txtSurname.Text.ToString
                .USERNAME = Me.txtUserName.Text.ToString

            End With
            Dim Rdg As Integer = mydata.InsertUSER_MANAGER(Sending)
            If Rdg > 0 Then
                Me.lblMsg.Text = "User Created Successfully"
                ClearControls()

            Else
                Me.lblMsg.Text = "Sorry - User Cannot Be Created at the moment"
                ClearControls()
            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub ClearControls()
        Try
            Me.txtConfirmPassword.Text = ""
            Me.txtEmail.Text = ""
            Me.txtFirstName.Text = ""
            Me.txtOtherNames.Text = ""
            Me.txtPassword.Text = ""
            Me.txtPhoneNumber.Text = ""
            Me.txtSurname.Text = ""
            Me.txtUserName.Text = ""


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class