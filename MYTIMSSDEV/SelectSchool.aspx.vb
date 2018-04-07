
Partial Class SelectSchool
    Inherits BasePagePublic

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Westat.TIMSS.BLL.TIMSSBLL.CanChangeSchools() Then
                'Dim ddl As DropDownList = Me.SelectedSchool

                Me.SelectedSchool.Visible = True
                Me.SelectedSchool.DataSource = TimssBll.GetDistinctSchoolListDataTable()
                Me.SelectedSchool.DataBind()
                'Response.Write(Me.SelectedSchool.Items.Count & " Schools...")
                If Not TimssBll.HasFrame_N_ Then
                    TimssBll.Frame_N_ = Me.SelectedSchool.SelectedValue
                Else
                    Me.SelectedSchool.SelectedValue = TimssBll.Frame_N_
                End If

            End If
        End If
        
    End Sub

    Protected Sub ButtonSelectSchool_Click(sender As Object, e As System.EventArgs) Handles ButtonSelectSchool.Click
        TimssBll.Frame_N_ = Me.SelectedSchool.SelectedValue
        TimssBll.InitSchoolSessionInfo()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.opener.location.href=window.opener.location.href;window.close();</script>")
    End Sub
End Class
