Imports System.Web.UI

Public Class SaveSchoolEditChangesArgs
    Inherits SaveChangesArgsBase

    Public Sub New(controls As ControlCollection, frame_n_ As String)
        MyBase.New(controls, frame_n_)
    End Sub

    Public Sub New(controls As ControlCollection, frame_n_ As String, gradeid As String)
        MyBase.New(controls, frame_n_)
        Me.GradeId = gradeid

        Dim ControlList As List(Of Control) = Me.GradeDbControls
        For Each ctrl As Control In controls
            ProcessControls(ControlList, "dbgrade_", ctrl)
        Next
    End Sub

    Private mGradeDbControls As List(Of Control)
    Public Property GradeDbControls() As List(Of Control)
        Get
            If mGradeDbControls Is Nothing Then
                mGradeDbControls = New List(Of Control)
            End If
            Return mGradeDbControls
        End Get
        Set(ByVal value As List(Of Control))
            mGradeDbControls = value
        End Set
    End Property


    Public ReadOnly Property HasGradeId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId)
        End Get
    End Property

    Private mGradeId As String
    Public Property GradeId() As String
        Get
            Return mGradeId
        End Get
        Set(ByVal value As String)
            mGradeId = value
        End Set
    End Property
End Class
