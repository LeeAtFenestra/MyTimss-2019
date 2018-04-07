Public Class AssessmentDate

    Public Sub New(dt As DateTime)
        TheDate = dt
        Assessments = New List(Of DataRow)
    End Sub

    Private mTheDate As DateTime
    Public Property TheDate() As DateTime
        Get
            Return mTheDate
        End Get
        Set(ByVal value As DateTime)
            mTheDate = value
        End Set
    End Property

    Private mAssessments As List(Of DataRow)
    Public Property Assessments() As List(Of DataRow)
        Get
            Return mAssessments
        End Get
        Set(ByVal value As List(Of DataRow))
            mAssessments = value
        End Set
    End Property

End Class
