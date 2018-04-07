Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class TIMSSSCS2015_ReportQuestionnaireStatus
    Inherits BasePagePublic
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            Dim tmp As String = ""

            tmp = Me.DropDownListState.SelectedValue
            Me.DropDownListState.DataSource = TimssBll.GetAssessmentDateListStateNameValuePairList()
            Me.DropDownListState.DataBind()
            TimssBll.SetDropDownListSelectedValue(Me.DropDownListState, tmp)


            If Not IsPostBack Then
                If Not String.IsNullOrEmpty(Request.QueryString("s")) Then
                    TimssBll.SetDropDownListSelectedValue(Me.DropDownListState, Server.HtmlEncode(Request.QueryString("s")))
                End If
            End If

            tmp = Me.DropDownListRegion.SelectedValue
            Me.DropDownListRegion.DataSource = TimssBll.GetAssessmentDateListTestRegionNameValuePairList(Me.DropDownListState.SelectedValue)
            Me.DropDownListRegion.DataBind()
            TimssBll.SetDropDownListSelectedValue(Me.DropDownListRegion, tmp)


            If Not IsPostBack Then
                If Not String.IsNullOrEmpty(Request.QueryString("r")) Then
                    TimssBll.SetDropDownListSelectedValue(Me.DropDownListRegion, Server.HtmlEncode(Request.QueryString("r")))
                End If
            End If

            'DropDownListExport.DataSource = TimssBll.GeExportReportOptionsNameValuePairList()
            'DropDownListExport.DataBind()


            BindData()
        End If
    End Sub

    Private Sub BindData()

        Dim args As New GetSchoolListSqlDataSourceArgs

        args.SortParameters.Add(New SortParameter("s_name", SortDirection.Ascending))

        If Not String.IsNullOrEmpty(DropDownListState.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("s_state", DropDownListState.SelectedValue, "equals"))
        End If

        If Not String.IsNullOrEmpty(DropDownListRegion.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("testregion", DropDownListRegion.SelectedValue, "equals"))
        End If

        Me.ListBoxSchoolList.DataSource = TimssBll.GetQuestionnaireStatusReportSchoolNameValuePairList(args)
        Me.ListBoxSchoolList.DataBind()

    End Sub

    Sub DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListState.SelectedIndexChanged, DropDownListRegion.SelectedIndexChanged
        Me.BindData()
    End Sub

    Sub ListBoxSchoolList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxSchoolList.SelectedIndexChanged
        Dim args As New GetSchoolListSqlDataSourceArgs
        
        args.FilterParameters.Add(New FilterParameter("id", ListBoxSchoolList.SelectedValue, "equals"))

        Me.RepeaterSchoolList.DataSource = TimssBll.GetQuestionnaireStatusReportSchoolDetailsValuePairList(args)
        Me.RepeaterSchoolList.DataBind()
    End Sub


End Class
