Imports Westat.TIMSS.HL
Imports Westat.TIMSS.BLL

Partial Class admin_efileparser
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
                ViewState("GridViewSortColumn") = "UploadDT"
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
                Me.GridViewList.PageIndex = Server.HtmlEncode(Request.QueryString("pg"))
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

        Dim args As New SelectFromDatabaseArgs
        args.SortParameters.Add(New SortParameter(Me.GridViewSortColumn, Me.GridViewSortDirection))
        If (ucFilterAndPageControl1.HasFilter()) Then
            args.FilterParameters.Add(New FilterParameter(ucFilterAndPageControl1.FilterColumn, ucFilterAndPageControl1.FilterValue, ucFilterAndPageControl1.ComparisonOperator))
        End If

        Dim tmp As String = ""

        tmp = Me.DropDownListGrade.SelectedValue
        Me.DropDownListGrade.DataSource = TimssBll.GetSmpGrdNameValuePairList()
        Me.DropDownListGrade.DataBind()
        TimssBll.SetDropDownListSelectedValue(Me.DropDownListGrade, tmp)

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("g")) Then
                TimssBll.SetDropDownListSelectedValue(Me.DropDownListGrade, Server.HtmlEncode(Request.QueryString("g")))
            End If
        End If

        'tmp = Me.DropDownListDPStatus.SelectedValue
        'Me.DropDownListDPStatus.DataSource = TimssBll.GetDistincDPStatusNameValuePairList()
        'Me.DropDownListDPStatus.DataBind()
        'TimssBll.SetDropDownListSelectedValue(Me.DropDownListDPStatus, tmp)

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("dps")) Then
                TimssBll.SetDropDownListSelectedValue(Me.DropDownListDPStatus, Server.HtmlEncode(Request.QueryString("dps")))
            End If
        End If

        If Not String.IsNullOrEmpty(Me.DropDownListGrade.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("SmpGrd", Me.DropDownListGrade.SelectedValue, "equals"))
        End If

        If Not String.IsNullOrEmpty(Me.DropDownListDPStatus.SelectedValue) Then
            args.FilterParameters.Add(New FilterParameter("DPStatusCode", Me.DropDownListDPStatus.SelectedValue, "equals"))
        End If

        Dim dt As DataTable = TimssBll.GetEfilesReadyForParsingDataTable(args)
        GridViewList.PageSize = Me.GridViewPageSize
        GridViewList.DataSource = dt
        GridViewList.DataBind()

        ucFilterAndPageControl1.RecordCount = dt.Rows.Count

        ucFilterAndPageControl1.PageSizeDataSource = TimssBll.GetPageSizeNameValuePairArrayList()
        ucFilterAndPageControl1.PageSizeSelected = GridViewPageSize
        ucFilterAndPageControl1.PageCount = GridViewList.PageCount
        ucFilterAndPageControl1.SelectedPage = GridViewList.PageIndex + 1
    End Sub

    Sub ContactsGridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewList.RowCommand
        If e.CommandName.Equals("DownloadEfile") Then
            TimssBll.StreamDocumentToBrowser("Efile", e.CommandArgument, True)
        End If
    End Sub

    Private Sub LoadGridViewColumns()
        Dim lst As New ListItemCollection()
        For index As Integer = 0 To GridViewList.Columns.Count - 1
            Dim fld As DataControlField = GridViewList.Columns(index)
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

    Sub GridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewList.RowCreated
        TimssBll.HandleGridViewSortImageDisplay(e, GridViewSortColumn, GridViewSortDirection)
    End Sub

    Sub GridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewList.PageIndexChanging
        Me.GridViewList.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Sub GridView_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridViewList.Sorting
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
        Me.GridViewList.PageIndex = ucFilterAndPageControl1.SelectedPage()
        BindData()
    End Sub

    Sub CurrentPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListGrade.SelectedIndexChanged, DropDownListDPStatus.SelectedIndexChanged
        BindData()
    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonDoWork.Click
        ProcessGridView(Me.GridViewList)
    End Sub

    Private Sub ProcessGridView(rpt As GridView)
        For Each item As GridViewRow In rpt.Rows
            If item.RowType = DataControlRowType.DataRow Then
                Dim CheckBoxDoWork As CheckBox = item.FindControl("CheckBoxDoWork")
                If CheckBoxDoWork.Checked Then
                    Dim fileid As HiddenField = item.FindControl("fileid")
                    'Response.Write(fileid.Value & "<br />")
                    TimssBll.ParseEfile(fileid.Value)
                End If
            End If
        Next
        BindData()
    End Sub


End Class

