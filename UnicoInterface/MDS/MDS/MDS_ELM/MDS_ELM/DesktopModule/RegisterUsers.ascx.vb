Public Class RegisterUsers
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
            With Me.ddlUserRoles
                .DataSource = mydata2.GetAllUserRoles
                .DataTextField = "ROLENAME"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("--- Please Select ---", 0))
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
            Me.btnSearch.Text = "Create User"
            Me.tblAddNewAccount.Visible = True
            Me.txtConfirmPassword.Text = ""
            Me.txtEmailAdress.Text = ""
            Me.txtFirstName.Text = ""
            Me.txtLastName.Text = ""
            Me.txtOtherInformation.Text = ""
            Me.txtPassword.Text = ""
            Me.txtPersonnelNumber.Text = ""

            Me.txtConfirmPassword.ReadOnly = False
            Me.txtEmailAdress.ReadOnly = False
            Me.txtFirstName.ReadOnly = False
            Me.txtLastName.ReadOnly = False
            Me.txtOtherInformation.ReadOnly = False
            Me.txtPassword.ReadOnly = False
            Me.txtPersonnelNumber.ReadOnly = False


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            If Me.txtPassword.Text.ToString <> Me.txtConfirmPassword.Text.ToString Then
                Me.lblMsg.Text = "Password and Confirm Password does not match, Please enter the correct password"
            Else
                Dim Sending As New TableObjects.USER_REGISTER

                With Sending
                    .EMAIL = Me.txtEmailAdress.Text.ToString
                    .FIRST_NAME = Me.txtFirstName.Text.ToString
                    .LAST_NAME = Me.txtLastName.Text.ToString
                    .PERSONNEL_NUMBER = Me.txtPersonnelNumber.Text.ToString
                    .ROLEFK = CInt(IIf(IsNumeric(Me.ddlUserRoles.SelectedValue), Me.ddlUserRoles.SelectedValue, 0))
                    .TIMESTAMP = System.DateTime.Now.ToString
                    .UNIQUEREF = (New Utilities).GenerateRefNo
                    .USER_PASSWORD = Me.txtPassword.Text.ToString

                End With
                Dim MainID As Integer = CInt(IIf(IsNumeric(Session("ID")), Session("ID"), 0))
                If Me.btnSearch.Text = "Create User" Then
                    Dim insertSuccess As Integer = mydata.InsertUSER_REGISTER(Sending)
                    If insertSuccess > 0 Then
                        Me.lblMsg.ForeColor = Drawing.Color.Green
                        Me.lblMsg.Text = "Successful : User Created SuccessFully"
                    Else
                        Me.lblMsg.ForeColor = Drawing.Color.Red
                        Me.lblMsg.Text = "Oopps there was an Error, Please Ensure that the ELM Customer you Selected Have not Been Mapped Before, If Yes then Edit the Previous Mapping"
                    End If
                ElseIf Me.btnSearch.Text = "Edit Record" Then
                    Dim insertSuccess As Integer = mydata.UpdateUSER_REGISTER(Sending, MainID)
                    If insertSuccess > 0 Then
                        Me.lblMsg.ForeColor = Drawing.Color.Green
                        Me.lblMsg.Text = "Successful : User Information Updated SuccessFully"

                    Else
                        Me.lblMsg.ForeColor = Drawing.Color.Red
                        Me.lblMsg.Text = "Oopps there was an Error, Record Cannot be updated at this time, please try again"
                    End If
                End If




                BindGrid()
                Hidetables()
                Me.tblDataGrid.Visible = True

            End If
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

            Dim dv As DataView = mydata2.GetAllRegisteredUsers()
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

                Dim dv As DataView = mydata2.GetAllUsersByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archived"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.txtConfirmPassword.Text = dv.Item(0).Item("USER_PASSWORD")
                    Me.txtEmailAdress.Text = dv.Item(0).Item("EMAIL")
                    Me.ddlUserRoles.SelectedValue = dv.Item(0).Item("ROLEFK")
                    Me.txtFirstName.Text = dv.Item(0).Item("FIRST_NAME")
                    Me.txtLastName.Text = dv.Item(0).Item("LAST_NAME")
                    Me.txtOtherInformation.Text = ""
                    Me.txtPassword.Text = dv.Item(0).Item("USER_PASSWORD")
                    Me.txtPersonnelNumber.Text = dv.Item(0).Item("PERSONNEL_NUMBER")

                    Me.txtConfirmPassword.ReadOnly = True
                    Me.txtEmailAdress.ReadOnly = False
                    Me.ddlUserRoles.Enabled = True
                    Me.txtFirstName.ReadOnly = False
                    Me.txtLastName.ReadOnly = False
                    Me.txtOtherInformation.ReadOnly = False
                    Me.txtPassword.ReadOnly = True
                    Me.txtPersonnelNumber.ReadOnly = False


                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Text = "Edit Record"

                End If

            ElseIf e.CommandArgument = "View" Then
                Dim dv As DataView = mydata2.GetAllUsersByID(RecID)
                If dv Is Nothing Then
                    Me.lblMsg.Text = "Sorry, Application cannot connect to database at this time. Please Contact Application Admin"
                    Exit Sub
                End If
                If dv.Count = 0 Then
                    Me.lblMsg.Text = "Sorry, Record have Either Been Deleted or Archieved"
                    Exit Sub
                End If
                If dv.Count > 0 Then

                    Me.txtConfirmPassword.Text = dv.Item(0).Item("USER_PASSWORD")
                    Me.txtEmailAdress.Text = dv.Item(0).Item("EMAIL")
                    Me.ddlUserRoles.SelectedValue = dv.Item(0).Item("ROLEFK")
                    Me.txtFirstName.Text = dv.Item(0).Item("FIRST_NAME")
                    Me.txtLastName.Text = dv.Item(0).Item("LAST_NAME")
                    Me.txtPassword.Text = dv.Item(0).Item("USER_PASSWORD")
                    Me.txtPersonnelNumber.Text = dv.Item(0).Item("PERSONNEL_NUMBER")

                    Me.txtConfirmPassword.ReadOnly = True
                    Me.txtEmailAdress.ReadOnly = True
                    Me.ddlUserRoles.Enabled = False
                    Me.txtFirstName.ReadOnly = True
                    Me.txtLastName.ReadOnly = True
                    Me.txtOtherInformation.ReadOnly = True
                    Me.txtPassword.ReadOnly = True
                    Me.txtPersonnelNumber.ReadOnly = True

                    Hidetables()
                    Me.tblAddNewAccount.Visible = True
                    Me.btnSearch.Enabled = False

                End If

            ElseIf e.CommandArgument = "Reset" Then

                Dim ResetPassword As Integer = mydata.UpdatePassword(RecID)
                If ResetPassword > 0 Then
                    Me.lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "Password Reset Successful"

                Else
                    Me.lblMsg.Text = "Password Reset cannot be done at this time, please try again later"
                End If

            End If


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    Protected Sub btnSearching_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearching.Click
        Try
            Dim SearchSTring As String = Me.txtSearch.Text.ToString
            Dim dv As DataView = mydata2.GetAllSearchedUsers(SearchSTring)

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
                    mydata.DeleteUser(RecID)
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
            Me.txtConfirmPassword.ReadOnly = True
            Me.txtEmailAdress.ReadOnly = False
            Me.ddlUserRoles.Enabled = True
            Me.txtFirstName.ReadOnly = False
            Me.txtLastName.ReadOnly = False
            Me.txtOtherInformation.ReadOnly = False
            Me.txtPassword.ReadOnly = True
            Me.txtPersonnelNumber.ReadOnly = False


        Catch ex As Exception

        End Try
    End Sub
   
End Class