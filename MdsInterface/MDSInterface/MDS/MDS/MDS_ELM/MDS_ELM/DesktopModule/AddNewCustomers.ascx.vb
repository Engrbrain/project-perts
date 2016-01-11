Public Class AddNewCustomers
    Inherits System.Web.UI.UserControl

    Private mydata As New DataWriter
    Private mydata2 As New DataGet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Page.IsPostBack = False Then

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
            With Me.ddlELMCustomerCode
                .DataSource = mydata2.LoadELMCustomer()
                .DataTextField = "CustomerInfo"
                .DataValueField = "ClientCode"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlSAPCustomerCode
                .DataSource = mydata2.GetAllSAPCustomer
                .DataTextField = "CustomerInfo"
                .DataValueField = "KUNNR"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With
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
            Me.btnSearch.Text = "Create Customer"
            Me.tblAddNewAccount.Visible = True
            Me.ddlELMCustomerCode.SelectedIndex = 0
            Me.ddlSAPCustomerCode.SelectedIndex = 0
            Me.txtELMFullName.Text = ""
            Me.txtSAPFullName.Text = ""
            Me.txtOtherInformation.Text = ""

            Me.ddlELMCustomerCode.Enabled = True
            Me.ddlSAPCustomerCode.Enabled = True
            Me.txtELMFullName.ReadOnly = False
            Me.txtSAPFullName.ReadOnly = False
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim Sending As New TableObjects.CUSTOMER_MAPPING
            With Sending
                .ELM_CLIENT_ID = Me.ddlELMCustomerCode.SelectedValue.ToString
                .ELM_CLIENT_NAME = Me.txtELMFullName.Text.ToString
                .SAP_CLIENT_ID = Me.ddlSAPCustomerCode.SelectedValue.ToString
                .SAP_CLIENT_NAME = Me.txtSAPFullName.Text.ToString


            End With

            Dim MainID As Integer = CInt(IIf(IsNumeric(Session("ID")), Session("ID"), 0))
            If Me.btnSearch.Text = "Create Customer" Then
                Dim insertSuccess As Integer = mydata.InsertCUSTOMER_MAPPING(Sending)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Customer Mapped SuccessFully"
                Else
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Oopps there was an Error, Please Ensure that the ELM Customer you Selected Have not Been Mapped Before, If Yes then Edit the Previous Mapping"
                End If
            ElseIf Me.btnSearch.Text = "Edit Record" Then
                Dim insertSuccess As Integer = mydata.UpdateCUSTOMER_MAPPING(Sending, MainID)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Customer Mapped SuccessFully"

                Else
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Oopps there was an Error, Record Cannot be updated at this time, please try again"
                End If
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

            Dim dv As DataView = mydata2.GetAllCustomerMapping()
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

                Dim dv As DataView = mydata2.GetAllCustomersByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archived"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.txtELMFullName.Text = dv.Item(0).Item("ELM_CLIENT_NAME")
                    Me.txtSAPFullName.Text = dv.Item(0).Item("SAP_CLIENT_NAME")
                    Me.ddlELMCustomerCode.SelectedValue = dv.Item(0).Item("ELM_CLIENT_ID")
                    Me.ddlSAPCustomerCode.SelectedValue = "00" & dv.Item(0).Item("SAP_CLIENT_ID")

                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Text = "Edit Record"

                End If

            ElseIf e.CommandArgument = "View" Then
                Dim dv As DataView = mydata2.GetAllCustomersByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archieved"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.txtELMFullName.Text = dv.Item(0).Item("ELM_CLIENT_NAME")
                    Me.txtELMFullName.ReadOnly = True
                    Me.txtSAPFullName.Text = dv.Item(0).Item("SAP_CLIENT_NAME")
                    Me.txtSAPFullName.ReadOnly = True
                    Me.ddlELMCustomerCode.SelectedValue = dv.Item(0).Item("ELM_CLIENT_ID")
                    Me.ddlELMCustomerCode.Enabled = False
                    Me.ddlSAPCustomerCode.SelectedValue = "00" & dv.Item(0).Item("SAP_CLIENT_ID")
                    Me.ddlSAPCustomerCode.Enabled = False

                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Enabled = False

                    Me.ddlELMCustomerCode.Enabled = True
                    Me.ddlSAPCustomerCode.Enabled = True
                    Me.txtELMFullName.ReadOnly = False
                    Me.txtSAPFullName.ReadOnly = False
                End If

            End If


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnSearching_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearching.Click
        Try
            Dim SearchSTring As String = Me.txtSearch.Text.ToString
            Dim dv As DataView = mydata2.GetAllSearchedCustomer(SearchSTring)

            With Me.dgSalesPerson
                .DataSource = dv
                .DataBind()

            End With
            If dv.Count = 0 Then
                Me.lblMsg.Text = "Sorry, there are no customers that Matches your search criteria, Please enter another value and try again"
            End If
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
                    mydata.DeleteCustomer(RecID)
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Record Deleted Successfully"

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
            Me.ddlELMCustomerCode.Enabled = True
            Me.ddlSAPCustomerCode.Enabled = True
            Me.txtELMFullName.ReadOnly = False
            Me.txtSAPFullName.ReadOnly = False
            Me.btnSearch.Enabled = True

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlELMCustomerCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlELMCustomerCode.SelectedIndexChanged
        Try
            Dim ELMCustomerInfo As String = Me.ddlELMCustomerCode.SelectedItem.Text.ToString
            Dim SoCustomerArray As String() = ELMCustomerInfo.Split(New Char() {")"c})

            Dim ELMCustomerName = Trim(SoCustomerArray(1).ToString)

            Me.txtELMFullName.Text = ELMCustomerName.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub ddlSAPCustomerCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSAPCustomerCode.SelectedIndexChanged
        Try
            Dim SAPCustomerInfo As String = Me.ddlSAPCustomerCode.SelectedItem.Text.ToString
            Dim SoCustomerArray As String() = SAPCustomerInfo.Split(New Char() {")"c})

            Dim SAPCustomerName = Trim(SoCustomerArray(1).ToString)

            Me.txtSAPFullName.Text = SAPCustomerName.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class