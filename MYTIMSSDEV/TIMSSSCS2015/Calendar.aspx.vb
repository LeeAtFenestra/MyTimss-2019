
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class Calendar
    Inherits BasePagePublic
    Private mAssessmentList As List(Of AssessmentDate)
    Public Property AssessmentList() As List(Of AssessmentDate)
        Get
            Return mAssessmentList
        End Get
        Set(ByVal value As List(Of AssessmentDate))
            mAssessmentList = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        BindData()
        LabelRunDate.Text = Date.Now.ToShortDateString()
        LabelRunTime.Text = Date.Now.ToShortTimeString()
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("d")) Then
                Me.CalendarAssesment.VisibleDate = Server.HtmlEncode(Request.QueryString("d"))
                Me.CalendarAssesment2.VisibleDate = "4/1/2018"
                Me.CalendarAssesment3.VisibleDate = "5/1/2018"
            Else
                Me.CalendarAssesment.VisibleDate = "3/1/2018"
                Me.CalendarAssesment2.VisibleDate = "4/1/2018"
                Me.CalendarAssesment3.VisibleDate = "5/1/2018"
            End If
        End If
    End Sub

    Private Sub BindData()
        Dim tmp As String = ""

        tmp = Me.DropDownListREPSBGRP.SelectedValue
        Me.DropDownListREPSBGRP.DataSource = TimssBll.GetAssessmentDateListREPSBGRPValuePairList()
        Me.DropDownListREPSBGRP.DataBind()
        TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListREPSBGRP, tmp)


        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("s")) Then
                TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListREPSBGRP, Server.HtmlEncode(Request.QueryString("s")))
            End If
        End If

        tmp = Me.DropDownListRegion.SelectedValue
        Me.DropDownListRegion.DataSource = TimssBll.GetAssessmentDateListTestRegionNameValuePairList(Me.DropDownListREPSBGRP.SelectedValue)
        Me.DropDownListRegion.DataBind()
        TimssBll.SetDropDownListSelectedValue(Me.DropDownListRegion, tmp)


        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("s")) Then
                TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListRegion, Server.HtmlEncode(Request.QueryString("s")))
            End If
        End If

        Me.AssessmentList = TimssBll.GetAssessmentDateList(Me.DropDownListREPSBGRP.SelectedValue, Me.DropDownListRegion.SelectedValue)


    End Sub

    Protected Sub Calendar1_DayRender(sender As Object, e As DayRenderEventArgs) Handles CalendarAssesment.DayRender, CalendarAssesment2.DayRender, CalendarAssesment3.DayRender
        Dim ad As AssessmentDate = TimssBll.GetAssessmentDateFromList(AssessmentList(), e.Day.Date)
        e.Day.IsSelectable = False
        e.Cell.Style.Item("text-align") = "left"
        e.Cell.Style.Item("vertical-align") = "top"
        If Not ad Is Nothing Then
            For Each dr As DataRow In ad.Assessments

                Dim hl As New HtmlAnchor()
                hl.HRef = "SchoolEdit.aspx?id=" & dr("id")
                hl.InnerHtml = "<br>" & dr("fldProjectDesc") & "-" & dr("s_name") & "(" & dr("id") & ")<br>"
                e.Cell.Controls.Add(hl)

                'Dim lb As New LinkButton()
                'lb.Text = "<br>" & dr("s_name") & "<br>"
                'lb.PostBackUrl = "SchoolEdit.aspx?id=" & dr("id")
                'lb.ID = "lbschooledit"
                'e.Cell.Controls.Add(lb)
            Next
        End If
    End Sub
End Class
