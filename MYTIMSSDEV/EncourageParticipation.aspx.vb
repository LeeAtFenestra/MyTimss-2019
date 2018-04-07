
Partial Class EncourageParticipation
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        panelgrade12.Visible = Me.TimssBll.HasGradeId12()
    End Sub
End Class
