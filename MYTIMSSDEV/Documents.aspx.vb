
Partial Class Documents
    Inherits BasePagePublic


    Public Function HasGradeId4() As Boolean
        Return Me.TimssBll.HasGradeId4()
    End Function
    Public Function HasGradeId8() As Boolean
        Return Me.TimssBll.HasGradeId8()
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        '   Dim currentabsolute As String = System.Web.HttpContext.Current.Request.Url.AbsoluteUri


        If Me.TimssBll.isICILS() Then
            panelICILS.Visible = True

            panelGeneralInformation.Visible = False
            panelGeneralInformationICILS.Visible = True

            panelgrade4.Visible = False
            panelgrade8.Visible = False
            panelgrade12.Visible = False

            panelptgrade4.Visible = False
            panelptgrade8.Visible = False
            panelptgrade12.Visible = False

            lblProjectTitle.Text = "ICILS 2018"

        ElseIf Me.TimssBll.iseTIMSS() Then
            panelICILS.Visible = False

            panelGeneralInformation.Visible = True
            panelGeneralInformationICILS.Visible = False

            panelgrade4.Visible = Me.TimssBll.HasGradeId4()
            panelgrade8.Visible = Me.TimssBll.HasGradeId8()
            panelgrade12.Visible = Me.TimssBll.HasGradeId12()

            'panelgrade4.Visible = False
            ' panelgrade8.Visible = False
            ' panelgrade12.Visible = False

            panelptgrade4.Visible = Me.TimssBll.HasGradeId4()
            panelptgrade8.Visible = Me.TimssBll.HasGradeId8()
            panelptgrade12.Visible = Me.TimssBll.HasGradeId12()
        End If

    End Sub
End Class
