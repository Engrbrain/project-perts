Public Class AddNewMaterial
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
            With Me.ddlELMMaterialCode
                .DataSource = mydata2.LoadELMMaterial()
                .DataTextField = "BillItemCode"
                .DataValueField = "BillItemDescription"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlSAPMaterialCode
                .DataSource = mydata2.GetAllSAPMaterial
                .DataTextField = "MATNR"
                .DataValueField = "MAKTX"
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
            Me.btnSearch.Text = "Create Material"
            Me.tblAddNewAccount.Visible = True
            Me.ddlELMMaterialCode.SelectedIndex = 0
            Me.ddlSAPMaterialCode.SelectedIndex = 0
            Me.txtELMDescription.Text = ""
            Me.txtSAPDescription.Text = ""
            Me.ddlDivision.SelectedIndex = 0
            Me.ddlActive.SelectedIndex = 0

            Me.ddlELMMaterialCode.Enabled = True
            Me.ddlSAPMaterialCode.Enabled = True
            Me.txtELMDescription.ReadOnly = False
            Me.txtSAPDescription.Text = False
            Me.ddlDivision.Enabled = True
            Me.ddlActive.Enabled = True
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    
    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim Sending As New TableObjects.MATERIAL_MASTER
            With Sending
                .DIVISION = 70
                .LEG_CODE = Me.ddlELMMaterialCode.SelectedItem.Text.ToString
                .LEG_DESC = Me.txtELMDescription.Text.ToString
                .LEG_UOM = "AU"
                .MATERIAL_CODE = Me.ddlSAPMaterialCode.SelectedItem.Text.ToString
                .MATERIAL_DESC = Me.txtSAPDescription.Text.ToString
                .TIMESTAMP = System.DateTime.Now
                .UNIQUEREF = (New Utilities).GenerateRefNo
                .UNIT_OF_MEASURE = "AU"



            End With

            Dim MainID As Integer = CInt(IIf(IsNumeric(Session("ID")), Session("ID"), 0))
            If Me.btnSearch.Text = "Create Material" Then
                Dim insertSuccess As Integer = mydata.InsertMATERIAL_MASTER(Sending)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Material Mapped SuccessFully"
                Else
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Oopps there was an Error, Please Ensure that the ELM Material you Selected Have not Been Mapped Before, If Yes then Edit the Previous Mapping"
                End If
            ElseIf Me.btnSearch.Text = "Edit Record" Then
                Dim insertSuccess As Integer = mydata.UpdateMATERIAL_MASTER(Sending, MainID)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Material Mapped SuccessFully"

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

            Dim dv As DataView = mydata2.GetAllMaterialMapping()
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

                Dim dv As DataView = mydata2.GetAllMaterialsByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archived"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.ddlActive.SelectedValue = "AU"
                    Me.ddlDivision.SelectedValue = "70"
                    Me.ddlELMMaterialCode.SelectedValue = dv.Item(0).Item("LEG_CODE")
                    Me.ddlSAPMaterialCode.SelectedValue = dv.Item(0).Item("MATERIAL_CODE")
                    Me.txtELMDescription.Text = dv.Item(0).Item("LEG_DESC")
                    Me.txtSAPDescription.Text = dv.Item(0).Item("MATERIAL_DESC")
                    Me.ddlELMMaterialCode.Enabled = True
                    Me.ddlSAPMaterialCode.Enabled = True
                    Me.txtELMDescription.ReadOnly = False
                    Me.txtSAPDescription.Text = False
                    Me.ddlDivision.Enabled = True
                    Me.ddlActive.Enabled = True

                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Text = "Edit Record"

                End If

            ElseIf e.CommandArgument = "View" Then
                Dim dv As DataView = mydata2.GetAllMaterialsByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archieved"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.ddlActive.SelectedValue = "AU"
                    Me.ddlActive.Enabled = False
                    Me.ddlDivision.SelectedValue = "70"
                    Me.ddlDivision.Enabled = False
                    Me.ddlELMMaterialCode.SelectedValue = dv.Item(0).Item("LEG_CODE")
                    Me.ddlELMMaterialCode.Enabled = False
                    Me.ddlSAPMaterialCode.SelectedValue = dv.Item(0).Item("MATERIAL_CODE")
                    Me.ddlSAPMaterialCode.Enabled = False
                    Me.txtELMDescription.Text = dv.Item(0).Item("LEG_DESC")
                    Me.txtELMDescription.ReadOnly = True
                    Me.txtSAPDescription.Text = dv.Item(0).Item("MATERIAL_DESC")
                    Me.txtSAPDescription.ReadOnly = True


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
            Dim dv As DataView = mydata2.GetAllSearchedMaterial(SearchSTring)

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
                    mydata.DeleteMaterials(RecID)
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
            Me.ddlELMMaterialCode.Enabled = True
            Me.ddlSAPMaterialCode.Enabled = True
            Me.txtELMDescription.ReadOnly = False
            Me.txtSAPDescription.ReadOnly = False
            Me.btnSearch.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlELMMaterialCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlELMMaterialCode.SelectedIndexChanged
        Try
            Dim ELMMaterialInfo As String = Me.ddlELMMaterialCode.SelectedValue
            

            Me.txtELMDescription.Text = ELMMaterialInfo.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub ddlSAPMaterialCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSAPMaterialCode.SelectedIndexChanged
        Try
            Dim SAPMaterialInfo As String = Me.ddlSAPMaterialCode.SelectedValue
            

            Me.txtSAPDescription.Text = SAPMaterialInfo.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class