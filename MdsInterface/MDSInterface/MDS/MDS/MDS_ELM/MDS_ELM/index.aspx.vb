Public Class index
    Inherits System.Web.UI.Page
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            CheckUserLogin()
            BindDashBoard()
        End If
    End Sub


    Protected Sub CheckUserLogin()
        Try

            If (Session("Username") IsNot Nothing OrElse [String].Empty.Equals(Session("Username"))) Then
                Dim Username As String = Session("Username").ToString
                Dim dv As DataView = mydata2.GetLoginUserFullDetails(Username)
                Dim rolefk As Integer = CInt(IIf(IsNumeric(dv.Item(0).Item("ROLEFK")), dv.Item(0).Item("ROLEFK"), 1))
                If rolefk = 1 Then
                    Me.navCreateUser.Visible = True
                    Me.navCustomerMapping.Visible = True
                    Me.navExtract.Visible = True
                    Me.navLogin.Visible = False
                    Me.navMaterialMapping.Visible = True
                    Me.navStaffMapping.Visible = True
                    Me.navStorageLocationMapping.Visible = True
                    Me.navloadBODS.Visible = True
                    Me.navChangePassword.Visible = True
                    Me.navViewPending.Visible = True

                End If

                If rolefk = 2 Then
                    Me.navCreateUser.Visible = True
                    Me.navCustomerMapping.Visible = True
                    Me.navExtract.Visible = True
                    Me.navLogin.Visible = False
                    Me.navMaterialMapping.Visible = True
                    Me.navStaffMapping.Visible = True
                    Me.navStorageLocationMapping.Visible = True
                    Me.navloadBODS.Visible = False
                    Me.navChangePassword.Visible = True
                    Me.navViewPending.Visible = True

                End If

                If rolefk = 3 Then
                    Me.navCreateUser.Visible = True
                    Me.navCustomerMapping.Visible = True
                    Me.navExtract.Visible = False
                    Me.navLogin.Visible = False
                    Me.navMaterialMapping.Visible = True
                    Me.navStaffMapping.Visible = True
                    Me.navStorageLocationMapping.Visible = True
                    Me.navloadBODS.Visible = False
                    Me.navChangePassword.Visible = True
                    Me.navViewPending.Visible = True

                End If

                If rolefk = 4 Then
                    Me.navCreateUser.Visible = False
                    Me.navCustomerMapping.Visible = False
                    Me.navExtract.Visible = True
                    Me.navLogin.Visible = False
                    Me.navMaterialMapping.Visible = False
                    Me.navStaffMapping.Visible = False
                    Me.navStorageLocationMapping.Visible = False
                    Me.navloadBODS.Visible = False
                    Me.navChangePassword.Visible = True
                    Me.navViewPending.Visible = False
                End If

            Else
                Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                Response.Redirect(LoginPage, False)

            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub BindDashBoard()
        Try
            Dim ds As DataSet = mydata2.GetDashBoardReports()
            Dim dv As DataView = New DataView(ds.Tables(0))
            Dim dv1 As DataView = New DataView(ds.Tables(1))
            Dim dv2 As DataView = New DataView(ds.Tables(2))
            Dim dv3 As DataView = New DataView(ds.Tables(3))

            Dim NumberOfClients As String = dv.Item(0).Item("NumberOfClients").ToString
            Dim NumberOfBillItems As String = dv1.Item(0).Item("NumberOfBillItems").ToString
            Dim NumberOfDeports As String = dv2.Item(0).Item("NumberOfDeports").ToString
            Dim NumberOfSalesOrders As String = dv3.Item(0).Item("NumberOfSalesOrders").ToString

            Me.lblNumberOfBillItems.Text = NumberOfBillItems.ToString
            Me.lblLoadedSalesOrders.Text = NumberOfSalesOrders.ToString
            Me.lblNumberOfClient.Text = NumberOfClients.ToString
            Me.lblNumberofDeports.Text = NumberOfDeports.ToString



        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

End Class