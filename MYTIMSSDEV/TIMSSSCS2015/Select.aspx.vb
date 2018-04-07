Imports Westat.TIMSS.BLL

Partial Class TIMSSSCS2015_Select
    Inherits BasePagePublic


    Private mProf As ProfileCommon = ProfileCommon.GetUserProfile()
    Private mUser As MembershipUser = Membership.GetUser()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub


    Private Sub BindData()
        Me.DropDownWorkArea.DataSource = TimssBll.GetWorkAreaNameValuePairList()
        Me.DropDownWorkArea.DataBind()
        TimssBll.SetDropDownListSelectedValue(DropDownWorkArea, mProf.LastArea)
    End Sub

    Protected Sub ButtonGo_Click(sender As Object, e As System.EventArgs) Handles ButtonGo.Click
        mProf.LastArea = Me.DropDownWorkArea.SelectedValue
        mProf.Save()
        Response.Redirect("default.aspx")
    End Sub

End Class
