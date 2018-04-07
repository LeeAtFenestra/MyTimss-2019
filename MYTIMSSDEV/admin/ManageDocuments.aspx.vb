Imports Westat.TIMSS.HL
Imports Westat.FAB.MembershipProvider

Partial Class admin_ManageDocuments
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
                ViewState("GridViewSortColumn") = "s_name"
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
        LabelError.Visible = False
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
                Me.GridViewAccountList.PageIndex = Server.HtmlEncode(Request.QueryString("pg"))
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

        Dim dt As DataTable = TimssBll.GetManageDocumentsDataTable(args)
        GridViewAccountList.PageSize = Me.GridViewPageSize
        GridViewAccountList.DataSource = dt
        GridViewAccountList.DataBind()

        ucFilterAndPageControl1.RecordCount = dt.Rows.Count

        ucFilterAndPageControl1.PageSizeDataSource = TimssBll.GetPageSizeNameValuePairArrayList()
        ucFilterAndPageControl1.PageSizeSelected = GridViewPageSize
        ucFilterAndPageControl1.PageCount = GridViewAccountList.PageCount
        ucFilterAndPageControl1.SelectedPage = GridViewAccountList.PageIndex + 1
    End Sub

    Sub ContactsGridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles GridViewAccountList.RowCommand
        If e.CommandName.Equals("DownloadfileSTLF") Then
            TimssBll.StreamDocumentToBrowser("STLF1", e.CommandArgument, True)
        ElseIf e.CommandName.Equals("DownloadfileTTF") Then
            TimssBll.StreamDocumentToBrowser("TTF", e.CommandArgument, True)
        ElseIf e.CommandName.Equals("DownloadfileSTF") Then
            TimssBll.StreamDocumentToBrowser("STF1", e.CommandArgument, True)
        ElseIf e.CommandName.Equals("DownloadfileSTF2") Then
            TimssBll.StreamDocumentToBrowser("STF2", e.CommandArgument, True)
        ElseIf e.CommandName.Equals("DownloadfileSTLF2") Then
            TimssBll.StreamDocumentToBrowser("STLF2", e.CommandArgument, True)
        End If
    End Sub

    Private Sub LoadGridViewColumns()
        Dim lst As New ListItemCollection()
        For index As Integer = 0 To GridViewAccountList.Columns.Count - 1
            Dim fld As DataControlField = GridViewAccountList.Columns(index)
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

    Sub GridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridViewAccountList.RowCreated
        TimssBll.HandleGridViewSortImageDisplay(e, GridViewSortColumn, GridViewSortDirection)
    End Sub

    Sub GridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewAccountList.PageIndexChanging
        Me.GridViewAccountList.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Sub GridView_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridViewAccountList.Sorting
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
        Me.GridViewAccountList.PageIndex = ucFilterAndPageControl1.SelectedPage()
        BindData()
    End Sub


    Protected Sub ButtonUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpload.Click

        Dim updated As Boolean = False

        Try


            If FileUpload_File1.HasFile Then
                Dim filename As String = System.IO.Path.GetFileName(FileUpload_File1.FileName)
                Dim startindex As Integer = filename.IndexOf("_") + 1
                Dim endindex As Integer = filename.IndexOf(".")
                Dim IEAid As String = filename.Substring(startindex, endindex - startindex)
                Dim filetype As String = filename.Substring(0, startindex - 1)

                If TimssBll.ProcessfileUploadForGrade(filetype, FileUpload_File1, IEAid) Then
                    updated = True
                End If
            End If

            If FileUpload_File2.HasFile Then
                Dim filename As String = System.IO.Path.GetFileName(FileUpload_File2.FileName)
                Dim startindex As Integer = filename.IndexOf("_") + 1
                Dim endindex As Integer = filename.IndexOf(".")
                Dim IEAid As String = filename.Substring(startindex, endindex - startindex)
                Dim filetype As String = filename.Substring(0, startindex - 1)

                If TimssBll.ProcessfileUploadForGrade(filetype, FileUpload_File2, IEAid) Then
                    updated = True
                End If
            End If

            If FileUpload_File3.HasFile Then
                Dim filename As String = System.IO.Path.GetFileName(FileUpload_File3.FileName)
                Dim startindex As Integer = filename.IndexOf("_") + 1
                Dim endindex As Integer = filename.IndexOf(".")
                Dim IEAid As String = filename.Substring(startindex, endindex - startindex)
                Dim filetype As String = filename.Substring(0, startindex - 1)

                If TimssBll.ProcessfileUploadForGrade(filetype, FileUpload_File3, IEAid) Then
                    updated = True
                End If
            End If

            If FileUpload_File4.HasFile Then
                Dim filename As String = System.IO.Path.GetFileName(FileUpload_File4.FileName)
                Dim startindex As Integer = filename.IndexOf("_") + 1
                Dim endindex As Integer = filename.IndexOf(".")
                Dim IEAid As String = filename.Substring(startindex, endindex - startindex)
                Dim filetype As String = filename.Substring(0, startindex - 1)

                If TimssBll.ProcessfileUploadForGrade(filetype, FileUpload_File4, IEAid) Then
                    updated = True
                End If
            End If

            If FileUpload_File5.HasFile Then
                Dim filename As String = System.IO.Path.GetFileName(FileUpload_File5.FileName)
                Dim startindex As Integer = filename.IndexOf("_") + 1
                Dim endindex As Integer = filename.IndexOf(".")
                Dim IEAid As String = filename.Substring(startindex, endindex - startindex)
                Dim filetype As String = filename.Substring(0, startindex - 1)

                If TimssBll.ProcessfileUploadForGrade(filetype, FileUpload_File5, IEAid) Then
                    updated = True
                End If
            End If
        Catch ex As Exception
            LabelError.Text = "There was an error uploading your file. Please check the file path format and try again."
            LabelError.Visible = True
            Exit Sub
        End Try

        For Each item As GridViewRow In Me.GridViewAccountList.Rows
            Dim Frame_N_ As HiddenField = item.FindControl("Frame_N_")
            Dim id As HiddenField = item.FindControl("id")
            Dim CheckBoxRemoveSTF As CheckBox = item.FindControl("CheckBoxRemoveSTF")
            Dim CheckBoxRemoveSTLF As CheckBox = item.FindControl("CheckBoxRemoveSTLF")
            Dim CheckBoxRemoveSTF2 As CheckBox = item.FindControl("CheckBoxRemoveSTF2")
            Dim CheckBoxRemoveSTLF2 As CheckBox = item.FindControl("CheckBoxRemoveSTLF2")
            Dim CheckBoxRemoveTTF As CheckBox = item.FindControl("CheckBoxRemoveTTF")



            If Not CheckBoxRemoveSTLF Is Nothing Then
                If CheckBoxRemoveSTLF.Checked Then
                    TimssBll.RemoveGradeSTLFFile(id.Value, "STLF", "Delete")
                    updated = True
                End If
            End If

            If Not CheckBoxRemoveSTF Is Nothing Then
                If CheckBoxRemoveSTF.Checked Then
                    TimssBll.RemoveGradeSTLFFile(id.Value, "STF", "Delete")
                    updated = True
                End If
            End If

            If Not CheckBoxRemoveTTF Is Nothing Then
                If CheckBoxRemoveTTF.Checked Then
                    TimssBll.RemoveGradeSTLFFile(id.Value, "TTF", "Delete")
                    updated = True
                End If
            End If

            If Not CheckBoxRemoveSTLF2 Is Nothing Then
                If CheckBoxRemoveSTLF2.Checked Then
                    TimssBll.RemoveGradeSTLFFile(id.Value, "STLF2", "Delete")
                    updated = True
                End If
            End If

            If Not CheckBoxRemoveSTF2 Is Nothing Then
                If CheckBoxRemoveSTF2.Checked Then
                    TimssBll.RemoveGradeSTLFFile(id.Value, "STF2", "Delete")
                    updated = True
                End If
            End If
        Next

        If updated Then
            BindData()
        End If

    End Sub

End Class
