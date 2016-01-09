Public Class SettingsPanel
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
            Dim dv As DataView = mydata2.GetCurrentCLient()

            If dv Is Nothing Then
                Me.lblMsg.Text = "Cannot Connect to SAP DB. Please contact Administrator"
            End If

            If dv.Count = 0 Then
                Me.lblMsg.Text = "There is no client configured for this Interface. Please set Interface"
            End If

            If dv.Count > 0 Then
                Me.txtPresentClient.Text = dv.Item(0).Item("CLIENT").ToString
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
            Dim Client As String = Me.txtNewClient.Text.ToString
            If ValClient(Client) = 0 Then

                Exit Sub

            Else
                Dim sending As New TableObjects.SAPCLIENT

                With sending
                    .CLIENT = Me.txtNewClient.Text.ToString
                    .DATE_ADDED = System.DateTime.Now.ToString("yyyy/MM/dd")
                    .IS_ACTIVE = 1

                End With

                mydata.UpdateSAPCLIENT()
                Dim kk As Integer = mydata.InsertSAPCLIENT(sending)

                If kk > 0 Then
                    Me.lblMsg.Text = "New Client have been configured successfully for the interface"
                Else

                    Me.lblMsg.Text = "Sorry New Client cannot be created, Please contact Administrator"
                End If
            End If
           

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Function CheckForAlphaCharacters(ByVal StringToCheck As String)


        For i = 0 To StringToCheck.Length - 1
            If Char.IsLetter(StringToCheck.Chars(i)) Then
                Return True
            End If
        Next

        Return False

    End Function

    Protected Function ValClient(Client As String) As Integer
        Dim ValIndicator As Integer
        Try
            If Len(Client) > 3 Then
                ValIndicator = 0
                Me.lblMsg.Text = "Sorry Client cannot Exceed a 3 Digit Number"
                Return ValIndicator

            ElseIf Client = String.Empty Then
                ValIndicator = 0
                Me.lblMsg.Text = "Sorry you have not inputed any value as Client"
                Return ValIndicator

            ElseIf Client = "" Then
                ValIndicator = 0
                Me.lblMsg.Text = "Sorry you have not inputed any value as Client"
                Return ValIndicator

            ElseIf CheckForAlphaCharacters(Client) Then
                ValIndicator = 0
                Me.lblMsg.Text = "Sorry your client cannot contain an Alphabet"
                Return ValIndicator

            Else
                ValIndicator = 1
                Me.lblMsg.Text = ""
                Return ValIndicator
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
        Return ValIndicator
    End Function
End Class