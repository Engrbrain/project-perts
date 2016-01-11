Public Class ViewCurrentLoad
    Inherits System.Web.UI.UserControl

    Private mydata As New DataWriter
    Private mydata2 As New DataGet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Page.IsPostBack = False Then

                BindGrid()
              
            End If

        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub


    
    Protected Sub BindGrid()
        Try
            Dim ds As DataSet = mydata2.GetAllPendingSalesOrders()
            Dim condv As DataView = New DataView(ds.Tables(0))
            Dim headv As DataView = New DataView(ds.Tables(1))
            Dim itemdv As DataView = New DataView(ds.Tables(2))
            Dim partdv As DataView = New DataView(ds.Tables(3))
            Dim schedv As DataView = New DataView(ds.Tables(4))

            If headv.Count = 0 Then
                Me.lblHeader.Text = "You dont have any pending header document, Click on the Extract and load button to continue your load"
                With Me.dgSalesHeader
                    .DataSource = headv
                    .DataBind()

                End With
            Else
                With Me.dgSalesHeader
                    .DataSource = headv
                    .DataBind()

                End With
            End If

            If condv.Count = 0 Then
                Me.lblCondition.Text = "You dont have any pending Condition Record document, Click on the Extract and load button to continue your load"
                With Me.dgConditionRecord
                    .DataSource = condv
                    .DataBind()

                End With
            Else
                With Me.dgConditionRecord
                    .DataSource = condv
                    .DataBind()

                End With
            End If

            If itemdv.Count = 0 Then
                Me.lblItem.Text = "You dont have any pending Sales Order Line Item document, Click on the Extract and load button to continue your load"
                With Me.dgSalesLineItem
                    .DataSource = itemdv
                    .DataBind()

                End With
            Else
                With Me.dgSalesLineItem
                    .DataSource = itemdv
                    .DataBind()

                End With

            End If

            If partdv.Count = 0 Then
                Me.lblPartner.Text = "You dont have any pending Partner document, Click on the Extract and load button to continue your load"
                With Me.dgSalesPartner
                    .DataSource = partdv
                    .DataBind()

                End With
            Else
                With Me.dgSalesPartner
                    .DataSource = partdv
                    .DataBind()

                End With

            End If
          


        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub

    
    Protected Sub btnClearCurrentLoad_Click(sender As Object, e As EventArgs) Handles btnClearCurrentLoad.Click
        Try
            mydata.DeleteAllSalesOrders()
            BindGrid()
        Catch ex As Exception
            AppException.LogError(ex.Message.ToString, ex.StackTrace.ToString)
        End Try
    End Sub
End Class