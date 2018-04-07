
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class SchoolActivities
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

    Public Property CurrentAlphaBarLetter() As String
        Get
            If ViewState("CurrentAlphaBarLetter") = Nothing Then
                ViewState("CurrentAlphaBarLetter") = ""
            End If
            Return ViewState("CurrentAlphaBarLetter")
        End Get
        Set(ByVal value As String)
            ViewState("CurrentAlphaBarLetter") = value
        End Set
    End Property


    Public ReadOnly Property HasCurrentAlphaBarLetter() As Boolean
        Get
            Return IIf(String.IsNullOrEmpty(Me.CurrentAlphaBarLetter()), False, True)
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            LoadGridViewColumns()

            If Not String.IsNullOrEmpty(Request.QueryString("al")) Then
                Me.CurrentAlphaBarLetter = Server.HtmlEncode(Request.QueryString("al"))
            End If

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
        Dim tmp As String = ""

        tmp = Me.DropDownListTerritory.SelectedValue
        Me.DropDownListTerritory.DataSource = TimssBll.GetTerritoryNameValuePairList()
        Me.DropDownListTerritory.DataBind()
        TimssBll.SetDropDownListSelectedValue(Me.DropDownListTerritory, tmp)

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("t")) Then
                TimssBll.SetDropDownListSelectedValue(Me.DropDownListTerritory, Server.HtmlEncode(Request.QueryString("t")))
            End If
        End If


        tmp = Me.DropDownListREPS.SelectedValue
        Me.DropDownListREPS.DataSource = TimssBll.GetREPSValuePairListForTerritory(Me.DropDownListTerritory.SelectedValue, True)
        Me.DropDownListREPS.DataBind()
        TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListREPS, tmp)

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("s")) Then
                TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListREPS, Server.HtmlEncode(Request.QueryString("s")))
            End If
        End If

        tmp = Me.DropDownListSchoolType.SelectedValue
        DropDownListSchoolType.DataSource = TimssBll.GetRecruitmentReportSchoolTypeNameValuePairList()
        DropDownListSchoolType.DataBind()
        TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListSchoolType, tmp)

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("st")) Then
                TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListSchoolType, Server.HtmlEncode(Request.QueryString("st")))
            End If
        End If

        tmp = Me.DropDownListRegion.SelectedValue
        Me.DropDownListRegion.DataSource = TimssBll.GetTestRegionNameValuePairList(Me.DropDownListREPS.SelectedValue)
        Me.DropDownListRegion.DataBind()
        TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListRegion, tmp)

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("r")) Then
                TIMSSBLL.SetDropDownListSelectedValue(Me.DropDownListRegion, Server.HtmlEncode(Request.QueryString("r")))
            End If
        End If

        args.SortParameters.Add(New SortParameter(Me.GridViewSortColumn, Me.GridViewSortDirection))

        If Me.HasCurrentAlphaBarLetter Then
            args.FilterParameters.Add(New FilterParameter("s_name", Me.CurrentAlphaBarLetter(), "startswith", 2))
        End If

        If Not String.IsNullOrEmpty(Me.DropDownListTerritory.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("fldTerritoryCode", Me.DropDownListTerritory.SelectedValue, "equals"))
        End If

        If Not String.IsNullOrEmpty(Me.DropDownListREPS.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("REPSBGRP", Me.DropDownListREPS.SelectedValue, "equals"))
        End If

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

        If Not String.IsNullOrEmpty(Me.DropDownListRegion.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("testregion", Me.DropDownListRegion.SelectedValue, "equals"))
        End If

        If (ucFilterAndPageControl1.HasFilter()) Then
            args.FilterParameters.Add(New FilterParameter(ucFilterAndPageControl1.FilterColumn, ucFilterAndPageControl1.FilterValue, ucFilterAndPageControl1.ComparisonOperator))
        End If

        Dim dt As DataTable = TimssBll.GetSchoolListDataTable(args)
        'Dim SqlDataSource1 As SqlDataSource = TimssBll.GetSchoolListSqlDataSource(args)
        'AddHandler SqlDataSource1.Selected, AddressOf SqlDataSource1_Selected

        'Me.Page.Controls.Add(SqlDataSource1)

        GridViewSchoolList.PageSize = Me.GridViewPageSize
        'GridViewSchoolList.DataSource = SqlDataSource1
        GridViewSchoolList.DataSource = dt
        GridViewSchoolList.DataBind()

        ucFilterAndPageControl1.RecordCount = dt.Rows.Count

        ucFilterAndPageControl1.PageSizeDataSource = TimssBll.GetPageSizeNameValuePairArrayList()
        ucFilterAndPageControl1.PageSizeSelected = GridViewPageSize
        ucFilterAndPageControl1.PageCount = GridViewSchoolList.PageCount
        ucFilterAndPageControl1.SelectedPage = GridViewSchoolList.PageIndex + 1

        RepeaterFirstLetter.DataSource = TimssBll.GetFirstLetterOfSchoolNameValuePairList(args)
        RepeaterFirstLetter.DataBind()

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


    Public Function AphaBarLinkCSS(currentletter As String) As String
        Return IIf(Me.CurrentAlphaBarLetter().Equals(currentletter), "alphbarlinkon", "alphbarlink")

    End Function

    Public Function AphaBarCSS(currentletter As String) As String
        Return IIf(Me.CurrentAlphaBarLetter().Equals(currentletter), "alphbaron", "alphbar")
    End Function

    Public Function AphaBarLinkTooltip(currentletter As String) As String
        Return IIf(String.IsNullOrEmpty(currentletter), "Display all School Names", "Display items in the [School Name] column starting with the letter '" & currentletter & "'")
    End Function

    Sub R1_ItemCommand(Sender As Object, e As RepeaterCommandEventArgs) Handles RepeaterFirstLetter.ItemCommand
        If e.CommandName.Equals("filterbyfirstletter") Then
            Me.CurrentAlphaBarLetter = e.CommandArgument
            Me.BindData()
        End If
    End Sub

    Sub CurrentPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListTerritory.SelectedIndexChanged, DropDownListREPS.SelectedIndexChanged, DropDownListSchoolType.SelectedIndexChanged, DropDownListRegion.SelectedIndexChanged
        BindData()
    End Sub

    Public Function HandleSTLFDocumentAccess(uploaded As Boolean) As Object
        If Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() Or Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
            Return False
        Else
            Return uploaded
        End If
    End Function

    Sub GridViewSchoolList_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewSchoolList.RowCommand
        If e.CommandName.Equals("Downloadfile") Then
            TimssBll.StreamDocumentToBrowser("STLF", e.CommandArgument, True)
        End If
    End Sub


End Class
