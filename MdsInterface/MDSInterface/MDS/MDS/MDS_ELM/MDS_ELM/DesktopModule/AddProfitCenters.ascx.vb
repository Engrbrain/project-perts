Public Class AddProfitCenters
    Inherits System.Web.UI.UserControl

    Private mydata As New DataWriter
    Private mydata2 As New DataGet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Page.IsPostBack = False Then
                CheckUserLogin()
                BindGrid()
                Hidetables()
                Me.tblDataGrid.Visible = True
                LoadDropDown()
            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub LoadDropDown()
        Try
            With Me.ddlProfitCenter
                .DataSource = mydata2.GetAllProfitCenterInformation
                .DataTextField = "ProfitCenter"
                .DataValueField = "PRCTR"
                .DataSourceID = "ProfitCenterDesc"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlDeport
                .DataSource = mydata2.GetAllDeports
                .DataTextField = "DEPORT_NAME"
                .DataValueField = "DEPORT_ID"
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
                Me.lblUsername = Session("Username")

            Else
                Dim LoginPage As String = System.Configuration.ConfigurationManager.AppSettings("LoginPage")
                Response.Redirect(LoginPage, False)

            End If
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub Hidetables()
        Try
            Me.tblDataGrid.Visible = False
            Me.tblAddNewAccount.Visible = False
            Me.tblDeleteCaution.Visible = False
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub ImgBAddNew_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBAddNew.Click
        Try
            Hidetables()
            Me.btnSearch.Text = "Create Profit Center"
            Me.tblAddNewAccount.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim Sending As New TableObjects.DEPORTMANAGERS
            With Sending
                .DEPORTID = Me.ddlDeport.SelectedValue.ToString
                .DEPORTNAME = Me.ddlDeport.SelectedItem.Text.ToString
                .IS_ACTIVE = CInt(IIf(IsNumeric(Me.ddlActive.SelectedValue), Me.ddlActive.SelectedValue, 0))
                .MANAGERS_FIRST_NAME = Me.txtFirstName.Text.ToString
                .MANAGERS_LAST_NAME = Me.txtLastName.Text.ToString
                .PERSONNELID = Me.ddlProfitCenter.SelectedValue
                .SAP_NAME = Me.txtProfitCenterInformation.Text.ToString


            End With

            Dim MainID As Integer = CInt(IIf(IsNumeric(Session("ID")), Session("ID"), 0))
            If Me.btnSearch.Text = "Create SalesPerson" Then
                mydata.InsertDEPORTMANAGER(Sending)
            ElseIf Me.btnSearch.Text = "Edit Record" Then
                mydata.UpdateDEPORTMANAGER(Sending, MainID)
            End If




            BindGrid()
            Hidetables()
            Me.tblDataGrid.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


    Protected Sub ImgBDelete_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBDelete.Click
        Try
            Hidetables()
            Me.tblDeleteCaution.Visible = True

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try

    End Sub
    Protected Sub BindGrid()
        Try

            Dim dv As DataView = mydata2.GetAllDepotManagers
            With Me.dgSalesPerson
                .DataSource = dv
                .DataBind()

            End With
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Private Sub dgSalesPerson_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesPerson.ItemCommand
        Try
            Me.dgSalesPerson.SelectedIndex = e.Item.ItemIndex
            Dim kk As String = CType(Me.dgSalesPerson.SelectedItem.FindControl("lblID"), Label).Text
            Dim RecID As Integer = CInt(IIf(IsNumeric(kk), kk, 0))
            Session("ID") = CInt(IIf(IsNumeric(RecID), RecID, 0))
            If e.CommandArgument = "Edit" Then

                Dim dv As DataView = mydata2.GetAllDepotManagerByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archieved"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.txtFirstName.Text = dv.Item(0).Item("MANAGERS_FIRST_NAME")
                    Me.txtLastName.Text = dv.Item(0).Item("MANAGERS_LAST_NAME")
                    Me.txtProfitCenterInformation.Text = dv.Item(0).Item("SAP_NAME")
                    Me.ddlActive.SelectedValue = CInt(IIf(IsNumeric(dv.Item(0).Item("IS_ACTIVE")), dv.Item(0).Item("IS_ACTIVE"), 1))
                    Me.ddlProfitCenter.SelectedValue = dv.Item(0).Item("PERSONNELID")
                    Me.ddlDeport.SelectedValue = dv.Item(0).Item("DEPORTID")

                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Text = "Edit Record"

                End If

            ElseIf e.CommandArgument = "View" Then
                Dim dv As DataView = mydata2.GetAllDepotManagerByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archieved"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.txtFirstName.Text = dv.Item(0).Item("MANAGERS_FIRST_NAME")
                    Me.txtFirstName.ReadOnly = True
                    Me.txtLastName.Text = dv.Item(0).Item("MANAGERS_LAST_NAME")
                    Me.txtLastName.ReadOnly = True
                    Me.txtProfitCenterInformation.Text = dv.Item(0).Item("SAP_NAME")
                    Me.txtLastName.ReadOnly = True
                    Me.ddlActive.SelectedValue = CInt(IIf(IsNumeric(dv.Item(0).Item("IS_ACTIVE")), dv.Item(0).Item("IS_ACTIVE"), 1))
                    Me.ddlActive.Enabled = False
                    Me.ddlProfitCenter.SelectedValue = dv.Item(0).Item("PERSONNELID")
                    Me.ddlProfitCenter.Enabled = False
                    Me.ddlDeport.SelectedValue = dv.Item(0).Item("DEPORTID")
                    Me.ddlDeport.Enabled = False
                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Enabled = False

                End If

            End If


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnSearching_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearching.Click
        Try
            Dim SearchSTring As String = Me.txtSearch.Text.ToString
            Dim dv As DataView = mydata2.GetAllSearchedSalesPersons(SearchSTring)
            With Me.dgSalesPerson
                .DataSource = dv
                .DataBind()

            End With
            Hidetables()
            Me.tblDataGrid.Visible = True

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnIAmSure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIAmSure.Click
        Try
            Dim dg As DataGridItem
            For Each dg In Me.dgSalesPerson.Items
                If CType(dg.FindControl("chkSelect"), CheckBox).Checked = True Then

                    Dim RecID As Integer = CType(dg.FindControl("lblID"), Label).Text
                    RecID = CInt(IIf(IsNumeric(RecID), RecID, 0))
                    mydata.DeleteDEPORTMANAGERS(RecID)


                End If
            Next
            BindGrid()
            Hidetables()
            Me.tblDataGrid.Visible = True

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Hidetables()
            Me.tblDataGrid.Visible = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Hidetables()
            Me.tblDataGrid.Visible = True
            Me.ddlActive.Enabled = True
            Me.ddlDeport.Enabled = True
            Me.ddlProfitCenter.Enabled = True
            Me.txtFirstName.ReadOnly = False
            Me.txtLastName.ReadOnly = False
            Me.btnSearch.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlProfitCenter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProfitCenter.SelectedIndexChanged
        Try
            Me.txtProfitCenterInformation.Text = Me.ddlProfitCenter.DataSourceID
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class