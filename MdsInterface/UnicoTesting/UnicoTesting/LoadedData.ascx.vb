Public Class LoadedData
    Inherits System.Web.UI.UserControl
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Page.IsPostBack = False Then
                LoadDropDown()
                'CheckUserLogin()
                HideTables()
                Me.tblSectionA.Visible = True

            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Private Sub dgBalances_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgBalances.ItemCommand
        Try
            Me.dgBalances.SelectedIndex = e.Item.ItemIndex
            Dim DocNumber As String = CType(Me.dgBalances.SelectedItem.FindControl("lblBELNR"), Label).Text
            Dim nDV As DataView = mydata2.GetCurrentCLient()

            Dim Client As String = nDV.Item(0).Item("CLIENT").ToString



            If e.CommandArgument = "View" Then

                Dim dv As DataView = mydata2.GetAllSybaseInterfaceReportLineItems(DocNumber, Client)
                If dv Is Nothing Then
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Exit Sub
                End If
                If dv.Count > 0 Then
                    With Me.dgBalancesDetails
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
            Dim Month As String = Me.ddlUnico_Period.SelectedValue
            Dim Year As String = Me.ddlUnicoYear.SelectedValue
            Dim Username As String = "BODSUSER"

            Dim nDV As DataView = mydata2.GetCurrentCLient()

            Dim Client As String = nDV.Item(0).Item("CLIENT").ToString

            Dim dv As DataView = mydata2.GetAllSybaseInterfaceReport(Username, Month, Year, Client)

            If dv Is Nothing Then
                Me.lblMsg.Text = "Your Loaded Data cannot be Binded at this time, Please try again"
            End If

            If dv.Count = 0 Then
                Me.lblMsg.Text = "Sorry, There are no postings into SAP with the selected Date Range"
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
    Protected Sub LoadDropDown()
        Try
            With Me.ddlUnico_Period
                .DataSource = mydata2.GetALLUnicoPeriod
                .DataTextField = "DESCRIPTION"
                .DataValueField = "PERIOD"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlUnicoYear
                .DataSource = mydata2.GetALLUnicoYear
                .DataTextField = "YEAR"
                .DataValueField = "YEAR"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With
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