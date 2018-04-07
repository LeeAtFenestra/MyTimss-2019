Imports System.Web.UI.WebControls
Public Class SortParameter

    Public Sub New(col As String, direction As SortDirection)
        SortColumn = col
        SortDirection = direction
    End Sub

    Private mSortColumn As String
    Public Property SortColumn() As String
        Get
            Return mSortColumn
        End Get
        Set(ByVal value As String)
            mSortColumn = value
        End Set
    End Property

    Private mSortDirection As SortDirection
    Public Property SortDirection() As SortDirection
        Get
            Return mSortDirection
        End Get
        Set(ByVal value As SortDirection)
            mSortDirection = value
        End Set
    End Property

    Public ReadOnly Property SortDirectionSQL() As String
        Get
            Return IIf(SortDirection = Web.UI.WebControls.SortDirection.Ascending, "Asc", "Desc")
        End Get
    End Property

    Public ReadOnly Property SortExpression() As String
        Get
            If Not String.IsNullOrEmpty(Me.SortColumn()) Then
                Return " [" & Me.SortColumn() & "] " & Me.SortDirectionSQL()
            Else
                Return " "
            End If
        End Get
    End Property

End Class
