
Partial Class ContactUs
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ' Dim currentabsolute As String = System.Web.HttpContext.Current.Request.Url.AbsoluteUri

        If Me.TimssBll.iseTIMSS() Then
            paneltimss.Visible = True
            panelicils.Visible = False
        ElseIf Me.TimssBll.isICILS() Then
            panelicils.Visible = True
            paneltimss.Visible = False
        End If

    End Sub

End Class
