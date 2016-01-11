Public Class UnBalancedData
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

    Protected Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        Try
            Dim SelectedYear As String = Me.ddlUnicoYear.SelectedValue.ToString
            Dim SelectedPeriod As String = Me.ddlUnico_Period.SelectedValue.ToString

            Dim Period As String = SelectedYear & SelectedPeriod
            Dim Username As String = Me.lblUserName.Text.ToString
            Dim nDV As DataView = mydata2.GetCurrentCLient()

            Dim Client As String = nDV.Item(0).Item("CLIENT").ToString

            Dim dv As DataView = mydata2.GetAllUnBalancedData(Period)

            If dv Is Nothing Then
                Me.lblMsg.Text = "Failure Connecting to SQL Database. Please contact Database Administrator"
            End If

            If dv.Count = 0 Then
                Me.lblMsg.Text = "Congratulations, All Data Loaded for this period meet the interface Requirement and ready to be loaded into SAP, Please check the technical report to ensure that all data was loaded successfully"
                Me.lblMsg.ForeColor = Drawing.Color.Green
            End If

            If dv.Count > 0 Then
                With Me.dgViewGLTable
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