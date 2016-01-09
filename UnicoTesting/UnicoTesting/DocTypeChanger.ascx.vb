Public Class DocTypeChanger
    Inherits System.Web.UI.UserControl
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                CheckUserLogin()
                PreLoadClient()


            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub PreLoadClient()
        Try
            Dim dv As DataView = mydata2.GetCurrentDocumentType()

            If dv Is Nothing Then
                Me.lblMsg.Text = "Cannot Connect to SAP DB. Please contact Administrator"
            End If

            If dv.Count = 0 Then
                Me.lblMsg.Text = "There is no Document_Type configured for this Interface. Please set Interface"
            End If

            If dv.Count > 0 Then
                Me.txtPresentDocumentType.Text = dv.Item(0).Item("DOCUMENT_TYPE").ToString
            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


    Protected Sub CheckUserLogin()
        Try
            If (Session("Username") IsNot Nothing OrElse [String].Empty.Equals(Session("Username"))) Then


                Me.lblUserName = Session("Username")



            Else
                Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                Response.Redirect(LoginPage, False)

            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


    Protected Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        Try
            

            Dim sending As New TableObjects.DOCUMENT_TYPE

                With sending
                .DOCUMENT_TYPE = Me.txtNewDocumentType.Text.ToString
                    .DATE_ADDED = System.DateTime.Now.ToString("yyyy/MM/dd")
                    .IS_ACTIVE = 1

                End With

            mydata.UpdateDOCUMENT_TYPE()
            Dim kk As Integer = mydata.InsertDOCUMENT_TYPE(sending)

                If kk > 0 Then
                Me.lblMsg.Text = "New Document Type have been configured successfully for the interface"
                Else

                Me.lblMsg.Text = "Sorry New Document Type cannot be created, Please contact Administrator"
                End If



        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    

    
End Class