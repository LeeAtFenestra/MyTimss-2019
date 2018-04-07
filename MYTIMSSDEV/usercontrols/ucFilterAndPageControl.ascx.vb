Imports Westat.TIMSS.BLL

Partial Class usercontrols_ucFilterAndPageControl
    Inherits System.Web.UI.UserControl

    Public Event OnPageSizeChanged As System.EventHandler
    Public Event OnFilterChanged As System.EventHandler
    Public Event OnCurrentPageChanged As System.EventHandler

    'Private mPageCount As Integer
    'Public Property PageCount() As Integer
    '    Get
    '        Return mPageCount
    '    End Get
    '    Set(ByVal value As Integer)
    '        mPageCount = value
    '    End Set
    'End Property

    Public WriteOnly Property RecordCount() As Integer
        Set(ByVal value As Integer)
            LabelNumberOfRecords.Text = value
        End Set
    End Property

    Public ReadOnly Property searchFLDUniqueID() As String
        Get
            Return searchFLD.UniqueID
        End Get
    End Property
    Public ReadOnly Property searchSTRUniqueID() As String
        Get
            Return searchSTR.UniqueID
        End Get
    End Property

    Public WriteOnly Property SearchFields() As ListItemCollection
        Set(ByVal value As ListItemCollection)
            Dim originalselectedvalue As String = searchFLD.SelectedValue
            searchFLD.DataSource = value
            searchFLD.DataBind()
            TIMSSBLL.SetDropDownListSelectedValue(searchFLD, originalselectedvalue)
            'Try
            '    searchFLD.SelectedValue = originalselectedvalue
            'Catch ex As Exception
            'End Try
        End Set
    End Property


    Public WriteOnly Property PageSizeDataSource() As ArrayList
        Set(ByVal value As ArrayList)
            'Dim originalselectedvalue As String = PageSize.SelectedValue
            PageSize.DataSource = value
            PageSize.DataBind()
            'PageSize.SelectedValue = originalselectedvalue
        End Set
    End Property

    Public Property PageSizeSelected() As Integer
        Get
            Return PageSize.SelectedValue
        End Get
        Set(ByVal value As Integer)

            TIMSSBLL.SetDropDownListSelectedValue(PageSize, value)
            'Try
            '    PageSize.SelectedValue = value
            'Catch ex As Exception

            'End Try
        End Set
    End Property


    Public WriteOnly Property PageCount() As Integer
        Set(ByVal value As Integer)
            'Dim originalselectedvalue As String = CurrentPage.SelectedValue
            LabelPageCount.Text = value
            CurrentPage.Items.Clear()
            For pg As Integer = 1 To value
                CurrentPage.Items.Add(New ListItem(pg, pg))
            Next
            'CurrentPage.SelectedValue = originalselectedvalue
        End Set
    End Property

    Public Property SelectedPage() As Integer
        Get
            If String.IsNullOrEmpty(CurrentPage.SelectedValue) Then
                Return 0
            Else
                Return CurrentPage.SelectedValue - 1
            End If
        End Get
        Set(ByVal value As Integer)
            TIMSSBLL.SetDropDownListSelectedValue(CurrentPage, value)
            'Try
            '    CurrentPage.SelectedValue = value
            'Catch ex As Exception

            'End Try
        End Set
    End Property

    Public ReadOnly Property HasFilter() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Me.searchSTR.Text)
        End Get
    End Property
    Public Property FilterColumn() As String
        Get
            If HasFilter() Then
                Return Me.searchFLD.SelectedValue
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            TIMSSBLL.SetDropDownListSelectedValue(searchFLD, value)
            'Try
            '    Me.searchFLD.SelectedValue = value
            'Catch ex As Exception

            'End Try
        End Set
    End Property

    Public Property FilterValue() As String
        Get
            If HasFilter() Then
                Return Me.searchSTR.Text
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Me.searchSTR.Text = value
        End Set
    End Property

    Public ReadOnly Property ComparisonOperator() As String
        Get
            If HasFilter() Then
                Return "contains"
            Else
                Return ""
            End If
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Sub PageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PageSize.SelectedIndexChanged

        Dim prof As ProfileCommon = ProfileCommon.GetUserProfile()
        prof.LastPageSize = Me.PageSizeSelected
        prof.Save()

        RaiseEvent OnPageSizeChanged(Me, New EventArgs())
    End Sub

    Sub CurrentPage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CurrentPage.SelectedIndexChanged
        RaiseEvent OnCurrentPageChanged(Me, New EventArgs())
    End Sub


    Protected Sub ButtonFind_Click(sender As Object, e As System.EventArgs) Handles ButtonFind.Click
        RaiseEvent OnFilterChanged(Me, New EventArgs())
    End Sub
End Class
