Public Class FilterParameter


    Public Sub New(col As String, compop As String)
        FilterColumn = col
        ComparisonOperator = compop
    End Sub


    Public Sub New(col As String, val As String, compop As String)
        FilterColumn = col
        FilterValue = val
        ComparisonOperator = compop
    End Sub

    Public Sub New(col As String, val As String, compop As String, idx As Integer)
        FilterColumn = col
        FilterValue = val
        ComparisonOperator = compop
        Index = idx
    End Sub

    Private mFilterColumn As String
    Public Property FilterColumn() As String
        Get
            Return mFilterColumn
        End Get
        Set(ByVal value As String)
            mFilterColumn = value
        End Set
    End Property

    Private mFilterValue As String
    Public Property FilterValue() As String
        Get
            Return mFilterValue
        End Get
        Set(ByVal value As String)
            mFilterValue = value
        End Set
    End Property

    Private mComparisonOperator As String
    Public Property ComparisonOperator() As String
        Get
            Return mComparisonOperator
        End Get
        Set(ByVal value As String)
            mComparisonOperator = value
        End Set
    End Property


    Private mIndex As Integer = 0
    Public Property Index() As Integer
        Get
            Return mIndex
        End Get
        Set(ByVal value As Integer)
            mIndex = value
        End Set
    End Property


    Public ReadOnly Property FilterExpression() As String
        Get
            Dim result As String = ""
            If Not String.IsNullOrEmpty(Me.FilterColumn) Then
                If Me.ComparisonOperator.ToLower().Equals("contains") Then
                    result = " [" & Me.FilterColumn & "] like '%' + @" & Me.FilterColumn & Me.Index & "+ '%'"
                ElseIf Me.ComparisonOperator.ToLower().Equals("equals") Or Me.ComparisonOperator.ToLower().Equals("equal") Then
                    result = " [" & Me.FilterColumn & "] = @" & Me.FilterColumn & Me.Index & ""
                ElseIf Me.ComparisonOperator.ToLower().Equals("startswith") Then
                    result = " [" & Me.FilterColumn & "] like @" & Me.FilterColumn & Me.Index & "+ '%'"
                ElseIf Me.ComparisonOperator.ToLower().Equals("in") Then
                    result = " [" & Me.FilterColumn & "] in (" & Me.FilterValue & ")"
                ElseIf Me.ComparisonOperator.ToLower().Equals("notin") Then
                    result = " [" & Me.FilterColumn & "] not in (" & Me.FilterValue & ")"
                ElseIf Me.ComparisonOperator.ToLower().Equals("isnotnull") Then
                    result = " [" & Me.FilterColumn & "] is not null"
                ElseIf Me.ComparisonOperator.ToLower().Equals("isnull") Then
                    result = " [" & Me.FilterColumn & "] is null"
                ElseIf Me.ComparisonOperator.ToLower().Equals("endswith") Then
                    result = " [" & Me.FilterColumn & "] like '%' + @" & Me.FilterColumn & Me.Index & "+ ''"
                ElseIf Me.ComparisonOperator.ToLower().Equals("doesnotendwith") Then
                    result = " [" & Me.FilterColumn & "] not like '%' + @" & Me.FilterColumn & Me.Index & "+ ''"
                ElseIf Me.ComparisonOperator.ToLower().Equals("notequals") Or Me.ComparisonOperator.ToLower().Equals("notequal") Then
                    result = " [" & Me.FilterColumn & "] <> @" & Me.FilterColumn & Me.Index & ""
                ElseIf Me.ComparisonOperator.ToLower().Equals("newequals") Or Me.ComparisonOperator.ToLower().Equals("newequal") Then
                    result = " [" & Me.FilterColumn & "] = " & Me.Index & ""

                End If

            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property HasValueParameter() As Boolean
        Get
            Dim result As Boolean = False
            If Not String.IsNullOrEmpty(Me.FilterColumn) Then
                If Me.ComparisonOperator.ToLower().Equals("in") Or Me.ComparisonOperator.ToLower().Equals("isnotnull") Or Me.ComparisonOperator.ToLower().Equals("isnull") Then
                    result = False
                Else
                    result = True
                End If
            End If
            Return result
        End Get
    End Property

End Class
