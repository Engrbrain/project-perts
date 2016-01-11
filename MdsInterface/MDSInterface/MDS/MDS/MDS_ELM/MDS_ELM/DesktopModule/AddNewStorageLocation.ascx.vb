Public Class AddNewStorageLocation
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
            With Me.ddlELMDepotCode
                .DataSource = mydata2.LoadELMDepot()
                .DataTextField = "DepotCode"
                .DataValueField = "DepotName"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlProfitCenterCode
                .DataSource = mydata2.GetAllSAPProfitCenters
                .DataTextField = "PRCTR"
                .DataValueField = "LTEXT"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlSAPSalesOfficeCode
                .DataSource = mydata2.GetAllSASalesOffice
                .DataTextField = "VKBUR"
                .DataValueField = "BEZEI"
                .DataBind()
                .Items.Insert(0, New ListItem("-- Please Select --", 0))
                .SelectedIndex = 0
            End With

            With Me.ddlSAPStorageLoactionCode
                .DataSource = mydata2.GetAllSAPStorageLocations
                .DataTextField = "StorageLocationInfo"
                .DataValueField = "LGORT"
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
            Me.lblMsg.Text = ""

            Me.btnSearch.Text = "Create Depot"
            Me.tblAddNewAccount.Visible = True
            Me.ddlELMDepotCode.SelectedIndex = 0
            Me.ddlProfitCenterCode.SelectedIndex = 0
            Me.ddlSAPSalesOfficeCode.SelectedIndex = 0
            Me.ddlSAPStorageLoactionCode.SelectedIndex = 0
            Me.txtELMDepotDescription.Text = ""
            Me.txtSAPSalesOfficeDescription.Text = ""
            Me.txtSAPStorageLocationDescription.Text = ""
            Me.txtProfitCenterDescription.Text = ""

            Me.ddlELMDepotCode.Enabled = True
            Me.ddlProfitCenterCode.Enabled = True
            Me.ddlSAPSalesOfficeCode.Enabled = True
            Me.ddlSAPStorageLoactionCode.Enabled = True
            Me.txtELMDepotDescription.ReadOnly = False
            Me.txtSAPSalesOfficeDescription.ReadOnly = False
            Me.txtSAPStorageLocationDescription.ReadOnly = False
            Me.txtProfitCenterDescription.ReadOnly = False
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim Sending As New TableObjects.STORAGE_LOC_MAPPING
            With Sending
                .DEPORT_ID = Me.ddlELMDepotCode.SelectedItem.Text.ToString
                .DEPORT_NAME = Me.txtELMDepotDescription.Text.ToString
                .SAP_STORAGE_LOC_ID = Me.ddlSAPStorageLoactionCode.SelectedValue
                .SAP_STORAGE_LOC_NAME = Me.txtSAPStorageLocationDescription.Text.ToString
                .IS_ACTIVE = 1


            End With

            Dim MainID As Integer = CInt(IIf(IsNumeric(Session("ID")), Session("ID"), 0))
            If Me.btnSearch.Text = "Create Depot" Then
                Dim insertSuccess As Integer = mydata.InsertSTORAGE_LOC_MAPPING(Sending)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Depot Mapped SuccessFully"
                Else
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Oopps there was an Error, Please Ensure that the ELM Depot you Selected Have not Been Mapped Before, If Yes then Edit the Previous Mapping"
                End If
            ElseIf Me.btnSearch.Text = "Edit Record" Then
                Dim insertSuccess As Integer = mydata.UpdateSTORAGE_LOC_MAPPING(Sending, MainID)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Customer Mapped SuccessFully"

                Else
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Oopps there was an Error, Record Cannot be updated at this time, please try again"
                End If
            End If


            Dim SalesOffice As New TableObjects.SALES_OFFICE_MAPPING
            With SalesOffice
                .DEPORT_ID = Me.ddlELMDepotCode.SelectedItem.Text.ToString
                .DEPORT_NAME = Me.ddlELMDepotCode.SelectedValue
                .PROFIT_CENTER = Me.ddlProfitCenterCode.SelectedItem.Text.ToString
                .SALES_OFFICE = Me.ddlSAPSalesOfficeCode.SelectedItem.Text.ToString


            End With

            Dim SalesOfficeID As Integer = CInt(IIf(IsNumeric(Session("SalesID")), Session("SalesID"), 0))
            If Me.btnSearch.Text = "Create Depot" Then
                Dim insertSuccess As Integer = mydata.InsertSALES_OFFICE_MAPPING(SalesOffice)
                If insertSuccess > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Successful : Depot Mapped SuccessFully"
                Else
                    Me.lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Oopps there was an Error, Please Ensure that the ELM Depot you Selected Have not Been Mapped Before, If Yes then Edit the Previous Mapping"
                End If
            ElseIf Me.btnSearch.Text = "Edit Record" Then
                Dim insertSuccess As Integer = mydata.UpdateSALES_OFFICE_MAPPING(SalesOffice, SalesOfficeID)
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

            Dim dv As DataView = mydata2.GetAllDepotMapping()
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
            Dim DeportID As String = CType(Me.dgSalesPerson.SelectedItem.FindControl("lblDEPORT_ID"), Label).Text
            Dim kdv As DataView = mydata2.GetSalesOfficeByDeportID(DeportID)
            Dim DecID As String = kdv.Item(0).Item("ID")
            Dim SalesID As Integer = CInt(IIf(IsNumeric(DecID), DecID, 0))
            Session("SalesID") = CInt(IIf(IsNumeric(SalesID), SalesID, 0))

            If e.CommandArgument = "Edit" Then

                Dim dv As DataView = mydata2.GetStorageLocatioBYDeportID(DeportID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archived"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.ddlELMDepotCode.SelectedItem.Text = dv.Item(0).Item("DEPORT_ID")
                    Me.ddlProfitCenterCode.SelectedItem.Text = kdv.Item(0).Item("PROFIT_CENTER")
                    Me.ddlSAPSalesOfficeCode.SelectedItem.Text = kdv.Item(0).Item("SALES_OFFICE")
                    Me.ddlSAPStorageLoactionCode.SelectedValue = dv.Item(0).Item("SAP_STORAGE_LOC_ID")

                    Me.txtELMDepotDescription.Text = dv.Item(0).Item("DEPORT_NAME")
                    Me.txtSAPStorageLocationDescription.Text = dv.Item(0).Item("SAP_STORAGE_LOC_NAME")

                    Me.txtProfitCenterDescription.Text = kdv.Item(0).Item("PROFIT_CENTER")
                    Me.txtSAPSalesOfficeDescription.Text = kdv.Item(0).Item("SALES_OFFICE")

                    Me.ddlELMDepotCode.Enabled = True
                    Me.ddlProfitCenterCode.Enabled = True
                    Me.ddlSAPSalesOfficeCode.Enabled = True
                    Me.ddlSAPStorageLoactionCode.Enabled = True

                    Me.txtELMDepotDescription.ReadOnly = False
                    Me.txtSAPStorageLocationDescription.ReadOnly = False

                    Me.txtProfitCenterDescription.ReadOnly = False
                    Me.txtSAPSalesOfficeDescription.ReadOnly = False


                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Text = "Edit Record"

                End If

            ElseIf e.CommandArgument = "View" Then
                Dim dv As DataView = mydata2.GetStorageLocatioBYDeportID(DeportID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archieved"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.ddlELMDepotCode.SelectedItem.Text = dv.Item(0).Item("DEPORT_ID")
                    Me.ddlProfitCenterCode.SelectedItem.Text = kdv.Item(0).Item("PROFIT_CENTER")
                    Me.ddlSAPSalesOfficeCode.SelectedItem.Text = kdv.Item(0).Item("SALES_OFFICE")
                    Me.ddlSAPStorageLoactionCode.SelectedValue = dv.Item(0).Item("SAP_STORAGE_LOC_ID")

                    Me.txtELMDepotDescription.Text = dv.Item(0).Item("DEPORT_NAME")
                    Me.txtSAPStorageLocationDescription.Text = dv.Item(0).Item("SAP_STORAGE_LOC_NAME")

                    Me.txtProfitCenterDescription.Text = kdv.Item(0).Item("PROFIT_CENTER")
                    Me.txtSAPSalesOfficeDescription.Text = kdv.Item(0).Item("SALES_OFFICE")

                    Me.ddlELMDepotCode.Enabled = False
                    Me.ddlProfitCenterCode.Enabled = False
                    Me.ddlSAPSalesOfficeCode.Enabled = False
                    Me.ddlSAPStorageLoactionCode.Enabled = False

                    Me.txtELMDepotDescription.ReadOnly = True
                    Me.txtSAPStorageLocationDescription.ReadOnly = True

                    Me.txtProfitCenterDescription.ReadOnly = True
                    Me.txtSAPSalesOfficeDescription.ReadOnly = True
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
            Dim dv As DataView = mydata2.GetAllSearchedDepot(SearchSTring)

            With Me.dgSalesPerson
                .DataSource = dv
                .DataBind()

            End With
            If dv.Count = 0 Then
                Me.lblMsg.Text = "Sorry, there are no Depot that Matches your search criteria, Please enter another value and try again"
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

                    Dim Deport_ID As String = CType(dg.FindControl("lblDEPORT_ID"), Label).Text.ToString

                    mydata.DeleteDepot(Deport_ID)
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
            Me.ddlELMDepotCode.Enabled = True
            Me.ddlProfitCenterCode.Enabled = True
            Me.ddlSAPSalesOfficeCode.Enabled = True
            Me.ddlSAPStorageLoactionCode.Enabled = True

            Me.txtELMDepotDescription.ReadOnly = False
            Me.txtSAPStorageLocationDescription.ReadOnly = False

            Me.txtProfitCenterDescription.ReadOnly = False
            Me.txtSAPSalesOfficeDescription.ReadOnly = False

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlELMDepotCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlELMDepotCode.SelectedIndexChanged
        Try
            Dim ELMDeportInfo As String = Me.ddlELMDepotCode.SelectedValue
           

            Me.txtELMDepotDescription.Text = ELMDeportInfo.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub ddlProfitCenterCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProfitCenterCode.SelectedIndexChanged
        Try
            Dim ProfitCenterInfo As String = Me.ddlProfitCenterCode.SelectedValue


            Me.txtProfitCenterDescription.Text = ProfitCenterInfo.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub ddlSAPSalesOfficeCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSAPSalesOfficeCode.SelectedIndexChanged
        Try
            Dim SAPSalesOfficeInfo As String = Me.ddlSAPSalesOfficeCode.SelectedValue


            Me.txtSAPSalesOfficeDescription.Text = SAPSalesOfficeInfo.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
    Protected Sub ddlSAPStorageLoactionCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSAPStorageLoactionCode.SelectedIndexChanged
        Try
            Dim StorageLocationInfo As String = Me.ddlSAPStorageLoactionCode.SelectedItem.Text.ToString
            Dim SoStorageLocationArray As String() = StorageLocationInfo.Split(New Char() {")"c})

            Dim ELMStorageLocation = Trim(SoStorageLocationArray(1).ToString)

            Me.txtSAPStorageLocationDescription.Text = ELMStorageLocation.ToString
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class