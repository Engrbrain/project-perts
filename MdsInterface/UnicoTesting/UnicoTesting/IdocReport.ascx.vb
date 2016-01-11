Public Class IdocReport
    Inherits System.Web.UI.UserControl
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                'CheckUserLogin()
                HideTables()
                Me.tblSectionA.Visible = True

            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Public Function GetImageURL(Status As String) As String
        Dim ImageUrl As String = String.Empty
        Try
            If Status = "S" Then
                ImageUrl = "images\Branding\greenIndicator.png"
                Return ImageUrl

            ElseIf Status = "E" Then
                ImageUrl = "images\Branding\redIndicator.png"
                Return ImageUrl
            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
        Return ImageUrl
    End Function
    Private Sub dgBalances_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBalances.ItemCommand
        Try
            Me.dgBalances.SelectedIndex = e.Item.ItemIndex
            Dim DocNumber As String = CType(Me.dgBalances.SelectedItem.FindControl("lblDOCNUM"), Label).Text
            Dim nDV As DataView = mydata2.GetCurrentCLient()

            Dim Client As String = nDV.Item(0).Item("CLIENT").ToString



            If e.CommandArgument = "View" Then

                Dim dv As DataView = mydata2.GETALLSYBASETECHREPORTBYDOCNUMBER(Client, DocNumber)
                If dv Is Nothing Then
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Exit Sub
                End If
                If dv.Count > 0 Then
                    With Me.dgViewAll
                        .DataSource = dv
                        .DataBind()
                    End With
                    HideTables()
                    Me.tblViewBalancesDetails.Visible = True


                End If
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
            

            Dim nDV As DataView = mydata2.GetCurrentCLient()

            Dim Client As String = nDV.Item(0).Item("CLIENT").ToString

            Dim StartDate As String = Me.txtStartDate.Text.ToString.Replace("/", "")
            Dim EndDate As String = Me.txtEndDate.Text.ToString.Replace("/", "")

            Dim dv As DataView = mydata2.GETALLSYBASETECHREPORT(Client, StartDate, EndDate)

            If dv Is Nothing Then
                Me.lblMsg.Text = "Unable to connect to SAP, Please contact Administrator"
            End If

            If dv.Count = 0 Then
                Me.lblMsg.Text = "No Idoc have been loaded into SAP, By the BODS Scheduled Job for the selected Range and Client"
            End If

            If dv.Count > 0 Then
                With Me.dgBalances
                    .DataSource = dv
                    .DataBind()
                End With

                HideTables()
                Me.tblViewBalances.Visible = True

            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub HideTables()
        Try
            Me.tblSectionA.Visible = False
            Me.tblViewBalances.Visible = False
            Me.tblViewBalancesDetails.Visible = False
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnBakkk0_Click(sender As Object, e As EventArgs) Handles btnBakkk0.Click
        Try
            HideTables()
            Me.tblViewBalances.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnBakkk_Click(sender As Object, e As EventArgs) Handles btnBakkk.Click
        Try
            HideTables()
            Me.tblSectionA.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class