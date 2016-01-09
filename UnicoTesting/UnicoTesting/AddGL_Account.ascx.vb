Public Class AddGL_Account
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
                Me.lblUserName = Session("Username")

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
            

            Dim Sending As New TableObjects.UNICO_GL_MAPPING

            With Sending
                .DATE_CREATED = System.DateTime.Now.ToString("MM/dd/yyyy")
                .LegacyDescription = Me.txtLegacyAccountDescription.Text.ToString
                .LegCode = Me.txtLegacyAccountNumber.Text.ToString
                .SAPCode = Me.txtSAPAccountNumber.Text.ToString
                .SAPDesc = Me.txtSAPAccountDescription.Text.ToString

            End With
            Dim Rdg As Integer = mydata.InsertUNICO_GL_MAPPING(Sending)
            If Rdg > 0 Then
                Me.lblMsg.Text = "GL Account Created Successfully"
                ClearControls()

            Else
                Me.lblMsg.Text = "Sorry - GL Account Cannot Be Created at the moment"
                ClearControls()
            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub ClearControls()
        Try

            Me.txtSAPAccountDescription.Text = ""
            Me.txtLegacyAccountDescription.Text = ""
            Me.txtSAPAccountNumber.Text = ""
            Me.txtLegacyAccountNumber.Text = ""



        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class