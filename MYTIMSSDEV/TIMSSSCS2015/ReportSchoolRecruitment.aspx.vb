
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class TIMSSSCS2015_ReportSchoolRecruitment
    Inherits BasePagePublic


    Public Property GridViewPageSize() As Integer
        Get
            If ViewState("GridViewPageSize") = Nothing Then
                ViewState("GridViewPageSize") = IIf(Profile.LastPageSize = 0, 30, Profile.LastPageSize)
            End If
            Return ViewState("GridViewPageSize")
        End Get
        Set(ByVal value As Integer)
            ViewState("GridViewPageSize") = value
        End Set
    End Property

    Public Property GridViewSortColumn() As String
        Get
            If ViewState("GridViewSortColumn") = Nothing Then
                ViewState("GridViewSortColumn") = "S_Name"
            End If
            Return ViewState("GridViewSortColumn")
        End Get
        Set(ByVal value As String)
            ViewState("GridViewSortColumn") = value
        End Set
    End Property

    Public Property GridViewSortDirection() As SortDirection
        Get
            If ViewState("GridViewSortDirection") = Nothing Then
                ViewState("GridViewSortDirection") = SortDirection.Ascending
            End If
            Return ViewState("GridViewSortDirection")
        End Get
        Set(ByVal value As SortDirection)
            ViewState("GridViewSortDirection") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            LoadGridViewColumns()

            DropDownListFilter.DataSource = TimssBll.GetRecruitmentReportFilterNameValuePairList()
            DropDownListFilter.DataBind()

            DropDownListSchoolType.DataSource = TimssBll.GetRecruitmentReportSchoolTypeNameValuePairList()
            DropDownListSchoolType.DataBind()

            DropDownListExport.DataSource = TimssBll.GeExportReportOptionsNameValuePairList()
            DropDownListExport.DataBind()

            If Not String.IsNullOrEmpty(Request.QueryString("sortfld")) Then
                Me.GridViewSortColumn = Server.HtmlEncode(Request.QueryString("sortfld"))
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("sortdir")) Then
                Me.GridViewSortDirection = Server.HtmlEncode(Request.QueryString("sortdir"))
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("ps")) Then
                Me.GridViewPageSize = Server.HtmlEncode(Request.QueryString("ps"))
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("pg")) Then
                Me.GridViewSchoolList.PageIndex = Server.HtmlEncode(Request.QueryString("pg"))
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("searchSTR")) Then
                Me.ucFilterAndPageControl1.FilterValue = Server.HtmlEncode(Request.QueryString("searchSTR"))
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("searchFLD")) Then
                Me.ucFilterAndPageControl1.FilterColumn = Server.HtmlEncode(Request.QueryString("searchFLD"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("st")) Then
                DropDownListSchoolType.SelectedValue = Server.HtmlEncode(Request.QueryString("st"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("f")) Then
                DropDownListFilter.SelectedValue = Server.HtmlEncode(Request.QueryString("f"))
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("showall")) Then
                Me.GridViewSchoolList.AllowPaging = False
                Me.GridViewSchoolList.AllowSorting = False
            End If

            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim args As New GetSchoolListSqlDataSourceArgs
        'args.SortColumn = Me.GridViewSortColumn
        'args.SortDirection = Me.GridViewSortDirection
        'args.FilterColumn = ucFilterAndPageControl1.FilterColumn
        'args.FilterValue = ucFilterAndPageControl1.FilterValue
        'args.ComparisonOperator = ucFilterAndPageControl1.ComparisonOperator

        args.SortParameters.Add(New SortParameter(Me.GridViewSortColumn, Me.GridViewSortDirection))

        'If Not String.IsNullOrEmpty(DropDownListSchoolType.SelectedValue) Then
        '    args.FilterParameters.Add(New FilterParameter("isPublic", DropDownListSchoolType.SelectedValue, "equals"))
        'End If
        Dim tmp As String
        If Not String.IsNullOrEmpty(Me.DropDownListSchoolType.SelectedValue) Then
            tmp = Me.DropDownListSchoolType.SelectedValue
            If tmp.EndsWith("I") Then
                tmp = tmp.Replace("I", "")
                args.FilterParameters.Add(New FilterParameter("isICILS", "1", "equals"))
            Else
                args.FilterParameters.Add(New FilterParameter("isICILS", "1", "notequals"))
            End If

            args.FilterParameters.Add(New FilterParameter("isPublic", tmp, "equals"))
        End If

        If Not String.IsNullOrEmpty(DropDownListFilter.SelectedValue) Then
            args.FilterShortcut = DropDownListFilter.SelectedValue
        End If

        If (ucFilterAndPageControl1.HasFilter()) Then
            args.FilterParameters.Add(New FilterParameter(ucFilterAndPageControl1.FilterColumn, ucFilterAndPageControl1.FilterValue, ucFilterAndPageControl1.ComparisonOperator))
        End If

        Dim dt As DataTable = TimssBll.GetReportSchoolRecruitmentDataTable(args)

        GridViewSchoolList.PageSize = Me.GridViewPageSize
        'GridViewSchoolList.DataSource = SqlDataSource1
        GridViewSchoolList.DataSource = dt
        GridViewSchoolList.DataBind()

        If TIMSSBLL.MyRoles().Contains("Home Office") Then
            GridViewSchoolList.Columns(20).Visible = True
            GridViewSchoolList.Columns(21).Visible = True
        Else
            GridViewSchoolList.Columns(20).Visible = False
            GridViewSchoolList.Columns(21).Visible = False
        End If



        ucFilterAndPageControl1.RecordCount = dt.Rows.Count

        ucFilterAndPageControl1.PageSizeDataSource = TimssBll.GetPageSizeNameValuePairArrayList()
        ucFilterAndPageControl1.PageSizeSelected = GridViewPageSize
        ucFilterAndPageControl1.PageCount = GridViewSchoolList.PageCount
        ucFilterAndPageControl1.SelectedPage = GridViewSchoolList.PageIndex + 1


    End Sub


    Private Sub LoadGridViewColumns()
        Dim lst As New ListItemCollection()
        For index As Integer = 0 To GridViewSchoolList.Columns.Count - 1
            Dim fld As DataControlField = GridViewSchoolList.Columns(index)
            If fld.SortExpression <> "" Then
                lst.Add(New ListItem(fld.HeaderText, fld.SortExpression))
            End If
        Next
        ucFilterAndPageControl1.SearchFields = lst
        'Response.Write(SqlDataSource1.SelectCommand)
    End Sub
    Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs)
        ucFilterAndPageControl1.RecordCount = e.AffectedRows
    End Sub

    Sub GridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewSchoolList.RowCreated
        TimssBll.HandleGridViewSortImageDisplay(e, GridViewSortColumn, GridViewSortDirection)
    End Sub

    Sub GridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewSchoolList.PageIndexChanging
        Me.GridViewSchoolList.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Sub GridView_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridViewSchoolList.Sorting
        GridViewSortDirection = TimssBll.HandleGridViewSortDirectionToggling(e, GridViewSortColumn, GridViewSortDirection)
        GridViewSortColumn = e.SortExpression
        BindData()
    End Sub


    Sub FilterAndPaging_OnPageSizeChanged(sender As Object, e As System.EventArgs) Handles ucFilterAndPageControl1.OnPageSizeChanged
        Me.GridViewPageSize = ucFilterAndPageControl1.PageSizeSelected()
        BindData()
    End Sub

    Sub FilterAndPaging_OnFilterChanged(sender As Object, e As System.EventArgs) Handles ucFilterAndPageControl1.OnFilterChanged
        BindData()
    End Sub

    Sub FilterAndPaging_OnCurrentPageChanged(sender As Object, e As System.EventArgs) Handles ucFilterAndPageControl1.OnCurrentPageChanged
        Me.GridViewSchoolList.PageIndex = ucFilterAndPageControl1.SelectedPage()
        BindData()
    End Sub

    Sub DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListFilter.SelectedIndexChanged, DropDownListSchoolType.SelectedIndexChanged, DropDownListExport.SelectedIndexChanged
        Me.BindData()
    End Sub
    Protected Sub btnExportfromDt_Click(sender As Object, e As EventArgs) Handles ImageButtonXportToExcel.Click
        ExportGridviewToExcel()
    End Sub

    Private Sub ExportGridviewToExcel()
        If DropDownListExport.SelectedValue.Equals("all", StringComparison.CurrentCultureIgnoreCase) Then
            GridViewSchoolList.AllowPaging = False
            GridViewSchoolList.AllowSorting = False
        End If
        Me.BindData()
        GridViewExportUtil.Export2("ReportSchoolRecruitment.xlsx", GridViewSchoolList, True)
    End Sub

End Class
