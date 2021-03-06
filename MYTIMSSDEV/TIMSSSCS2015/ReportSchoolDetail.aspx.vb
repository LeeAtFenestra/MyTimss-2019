﻿Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class TIMSSSCS2015_ReportSchoolDetail
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

            Dim tmp As String = ""

            tmp = Me.DropDownListState.SelectedValue
            Me.DropDownListState.DataSource = TimssBll.GetAssessmentDateListStateNameValuePairList()
            Me.DropDownListState.DataBind()
            TimssBll.SetDropDownListSelectedValue(Me.DropDownListState, tmp)

            DropDownListSchoolType.DataSource = TimssBll.GetRecruitmentReportSchoolTypeNameValuePairList()
            DropDownListSchoolType.DataBind()

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

        If Not String.IsNullOrEmpty(DropDownListState.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("s_state", DropDownListState.SelectedValue, "equals"))
        End If

        If Not String.IsNullOrEmpty(DropDownListRegion.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("testregion", DropDownListRegion.SelectedValue, "equals"))
        End If


        If (ucFilterAndPageControl1.HasFilter()) Then
            args.FilterParameters.Add(New FilterParameter(ucFilterAndPageControl1.FilterColumn, ucFilterAndPageControl1.FilterValue, ucFilterAndPageControl1.ComparisonOperator))
        End If

        Dim dt As DataTable = TimssBll.GetAssessmentDateTable(args)

        GridViewSchoolList.PageSize = Me.GridViewPageSize
        'GridViewSchoolList.DataSource = SqlDataSource1
        GridViewSchoolList.DataSource = dt
        GridViewSchoolList.DataBind()



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

    Sub DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListSchoolType.SelectedIndexChanged, DropDownListState.SelectedIndexChanged, DropDownListRegion.SelectedIndexChanged, DropDownListExport.SelectedIndexChanged
        Me.BindData()
        Me.DropDownListRegion.DataBind()
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
        GridViewExportUtil.Export2("ReportSchoolDetail.xlsx", GridViewSchoolList, True)
    End Sub

End Class
