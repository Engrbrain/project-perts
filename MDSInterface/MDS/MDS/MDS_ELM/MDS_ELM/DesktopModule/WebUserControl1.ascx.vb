Public Class WebUserControl1
    Inherits System.Web.UI.UserControl
    Private mydata As New DataWriter
    Private mydata2 As New DataGet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dv As DataView = mydata2.GetAllunMappedCustomers
        With GridView1
            .DataSource = dv
            .DataBind()
        End With

    End Sub

End Class