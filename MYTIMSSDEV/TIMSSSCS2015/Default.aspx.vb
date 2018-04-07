
Partial Class TIMSSSCS2015_Default
    Inherits BasePagePublic

    Private Sub TIMSSSCS2015_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.Url.ToString().ToLower().Contains("mytimss.com") Or Request.Url.ToString().ToLower().Contains("mytimssdev.westat.com") Or Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") Or Request.Url.ToString().ToLower().Contains("mytimssdemo.wesdemo.com") Then
            divTIMSS.Visible = True
        ElseIf Request.Url.ToString().ToLower().Contains("myicils.com") Then
            divICILS.Visible = True
        End If


    End Sub
End Class
