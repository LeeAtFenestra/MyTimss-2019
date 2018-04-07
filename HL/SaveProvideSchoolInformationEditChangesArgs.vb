Imports System.Web.UI

Public Class SaveProvideSchoolInformationEditChangesArgs
    Inherits SaveChangesArgsBase

    Public Sub New(controls As ControlCollection, frame_n_ As String)
        MyBase.New(controls, frame_n_)
    End Sub

    Public Sub New(controls As ControlCollection, frame_n_ As String, Principalid As String, CoordinatorId As String)
        MyBase.New(controls, frame_n_)
        Me.PrincipalId = Principalid
        Me.CoordinatorId = CoordinatorId

        Dim ControlList As List(Of Control) = Me.PrincipalDbControls
        For Each ctrl As Control In controls
            ProcessControls(ControlList, "dbprincipal_", ctrl)
        Next

        ControlList = Me.CoordinatorDbControls
        For Each ctrl As Control In controls
            ProcessControls(ControlList, "dbcoordinator_", ctrl)
        Next

    End Sub

    Private mPrincipalDbControls As List(Of Control)
    Public Property PrincipalDbControls() As List(Of Control)
        Get
            If mPrincipalDbControls Is Nothing Then
                mPrincipalDbControls = New List(Of Control)
            End If
            Return mPrincipalDbControls
        End Get
        Set(ByVal value As List(Of Control))
            mPrincipalDbControls = value
        End Set
    End Property


    Public ReadOnly Property HasPrincipalId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(PrincipalId)
        End Get
    End Property

    Private mPrincipalId As String
    Public Property PrincipalId() As String
        Get
            Return mPrincipalId
        End Get
        Set(ByVal value As String)
            mPrincipalId = value
        End Set
    End Property


    Private mCoordinatorDbControls As List(Of Control)
    Public Property CoordinatorDbControls() As List(Of Control)
        Get
            If mCoordinatorDbControls Is Nothing Then
                mCoordinatorDbControls = New List(Of Control)
            End If
            Return mCoordinatorDbControls
        End Get
        Set(ByVal value As List(Of Control))
            mCoordinatorDbControls = value
        End Set
    End Property


    Public ReadOnly Property HasCoordinatorId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(CoordinatorId)
        End Get
    End Property

    Private mCoordinatorId As String
    Public Property CoordinatorId() As String
        Get
            Return mCoordinatorId
        End Get
        Set(ByVal value As String)
            mCoordinatorId = value
        End Set
    End Property
End Class
