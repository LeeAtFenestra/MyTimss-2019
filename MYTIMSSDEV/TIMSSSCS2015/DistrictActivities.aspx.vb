
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data
Imports System.IO

Partial Class DistrictActivities
    Inherits BasePagePublic


    Private mProf As ProfileCommon = ProfileCommon.GetUserProfile()

    Public Property GridViewPageSize() As Integer
        Get
            If ViewState("GridViewPageSize") = Nothing Then
                ViewState("GridViewPageSize") = IIf(mProf.LastPageSize = 0, 30, mProf.LastPageSize)
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
                ViewState("GridViewSortColumn") = "D_Name"
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
            'Response.Write(Request.Url.PathAndQuery)
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
                Me.GridViewDistrictList.PageIndex = Server.HtmlEncode(Request.QueryString("pg"))
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

        Dim args As New GetDistrictListSqlDataSourceArgs()

        'args.SortColumn = Me.GridViewSortColumn
        'args.SortDirection = Me.GridViewSortDirection
        'args.FilterColumn = ucFilterAndPageControl1.FilterColumn
        'args.FilterValue = ucFilterAndPageControl1.FilterValue
        'args.ComparisonOperator = ucFilterAndPageControl1.ComparisonOperator

        args.SortParameters.Add(New SortParameter(Me.GridViewSortColumn, Me.GridViewSortDirection))

        ucFilterAndPageControl1.RecordCount = 0

        If Me.HasCurrentAlphaBarLetter Then
            args.FilterParameters.Add(New FilterParameter("d_name", Me.CurrentAlphaBarLetter(), "startswith", 2))
        End If

        If (ucFilterAndPageControl1.HasFilter()) Then
            args.FilterParameters.Add(New FilterParameter(ucFilterAndPageControl1.FilterColumn, ucFilterAndPageControl1.FilterValue, ucFilterAndPageControl1.ComparisonOperator))
        End If

        Dim dt As DataTable = TimssBll.GetDistrictListDataTable(args)

        'Dim SqlDataSource1 As SqlDataSource = timssbll.GetDistrictListSqlDataSource(args)
        'Me.Page.Controls.Add(SqlDataSource1)
        'AddHandler SqlDataSource1.Selected, AddressOf SqlDataSource1_Selected

        GridViewDistrictList.PageSize = Me.GridViewPageSize
        'GridViewDistrictList.DataSource = SqlDataSource1
        GridViewDistrictList.DataSource = dt
        GridViewDistrictList.DataBind()

        ucFilterAndPageControl1.RecordCount = dt.Rows.Count

        ucFilterAndPageControl1.PageSizeDataSource = TimssBll.GetPageSizeNameValuePairArrayList()
        ucFilterAndPageControl1.PageSizeSelected = GridViewPageSize
        ucFilterAndPageControl1.PageCount = GridViewDistrictList.PageCount
        ucFilterAndPageControl1.SelectedPage = GridViewDistrictList.PageIndex + 1

        RepeaterFirstLetter.DataSource = TimssBll.GetFirstLetterOfDistrictNameValuePairList()
        RepeaterFirstLetter.DataBind()

    End Sub


    Private Sub LoadGridViewColumns()
        Dim lst As New ListItemCollection()
        For index As Integer = 0 To GridViewDistrictList.Columns.Count - 1
            Dim fld As DataControlField = GridViewDistrictList.Columns(index)
            If fld.SortExpression <> "" Then
                lst.Add(New ListItem(fld.HeaderText, fld.SortExpression))
            End If
        Next
        ucFilterAndPageControl1.SearchFields = lst
    End Sub

    Sub SqlDataSource1_Selected(sender As Object, e As SqlDataSourceStatusEventArgs)
        ucFilterAndPageControl1.RecordCount = e.AffectedRows
    End Sub

    Sub GridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewDistrictList.RowCreated
        TimssBll.HandleGridViewSortImageDisplay(e, GridViewSortColumn, GridViewSortDirection)
    End Sub

    Sub GridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewDistrictList.PageIndexChanging
        Me.GridViewDistrictList.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Sub GridView_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridViewDistrictList.Sorting
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
        Me.GridViewDistrictList.PageIndex = ucFilterAndPageControl1.SelectedPage()
        BindData()

    End Sub


    Public Function AphaBarLinkCSS(currentletter As String) As String
        Return IIf(Me.CurrentAlphaBarLetter().Equals(currentletter), "alphbarlinkon", "alphbarlink")

    End Function

    Public Function AphaBarCSS(currentletter As String) As String
        Return IIf(Me.CurrentAlphaBarLetter().Equals(currentletter), "alphbaron", "alphbar")
    End Function

    Public Function AphaBarLinkTooltip(currentletter As String) As String
        Return IIf(String.IsNullOrEmpty(currentletter), "Display all District Names", "Display items in the [District Name] column starting with the letter '" & currentletter & "'")
    End Function

    Sub R1_ItemCommand(Sender As Object, e As RepeaterCommandEventArgs) Handles RepeaterFirstLetter.ItemCommand
        If e.CommandName.Equals("filterbyfirstletter") Then
            Me.CurrentAlphaBarLetter = e.CommandArgument
            Me.BindData()
        End If
    End Sub

End Class
