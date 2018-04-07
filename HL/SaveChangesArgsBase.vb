Imports System.Web.UI

Public Class SaveChangesArgsBase

    'Public Sub New(controls As ControlCollection, pk As String)
    '    Dim ControlList As List(Of Control) = Me.DbControls
    '    For Each ctrl As Control In controls
    '        ProcessControls(ControlList, "db_", ctrl)
    '    Next
    '    Me.PrimaryKey = pk
    'End Sub

    Public Sub New(controls As ControlCollection, pk As String)
        Me.New(controls, pk, "db_")
    End Sub

    Public Sub New(controls As ControlCollection, pk As String, prefix As String)
        Dim ControlList As List(Of Control) = Me.DbControls
        For Each ctrl As Control In controls
            ProcessControls(ControlList, prefix, ctrl)
        Next
        Me.PrimaryKey = pk
    End Sub
    Public ReadOnly Property HasPrimaryKey() As Boolean
        Get
            Return Not String.IsNullOrEmpty(PrimaryKey)
        End Get
    End Property

    Private mPrimaryKey As String
    Public Property PrimaryKey() As String
        Get
            Return mPrimaryKey
        End Get
        Set(ByVal value As String)
            mPrimaryKey = value
        End Set
    End Property

    Private mDbControls As List(Of Control)
    Public Property DbControls() As List(Of Control)
        Get
            If mDbControls Is Nothing Then
                mDbControls = New List(Of Control)
            End If
            Return mDbControls
        End Get
        Set(ByVal value As List(Of Control))
            mDbControls = value
        End Set
    End Property

    'Public Sub ProcessControls(ControlList As List(Of Control), parent As Control)
    '    ProcessControls(ControlList, "db_", parent)
    '    'If Not parent.ID Is Nothing Then
    '    '    If parent.ID.StartsWith("db_") Then
    '    '        DbControls.Add(parent)
    '    '    End If
    '    'End If
    '    'For Each ctrl As Control In parent.Controls
    '    '    If Not ctrl.ID Is Nothing Then
    '    '        If ctrl.ID.StartsWith("db_") Then
    '    '            DbControls.Add(ctrl)
    '    '        End If
    '    '    End If
    '    '    If ctrl.HasControls Then
    '    '        ProcessControls(ctrl)
    '    '    End If
    '    'Next
    'End Sub


    Public Sub ProcessControls(ControlList As List(Of Control), prefix As String, parent As Control)

        If Not parent.ID Is Nothing Then
            If parent.ID.StartsWith(prefix) Then
                ControlList.Add(parent)
            End If
        End If
        For Each ctrl As Control In parent.Controls
            If Not ctrl.ID Is Nothing Then
                If ctrl.ID.StartsWith(prefix) Then
                    ControlList.Add(ctrl)
                End If
            End If
            If ctrl.HasControls Then
                ProcessControls(ControlList, prefix, ctrl)
            End If
        Next
    End Sub

    'Private Sub ProcessControls(ControlList As List(Of Control), prefix As String, parent As Control)
    '    If Not parent.ID Is Nothing Then
    '        If parent.ID.StartsWith(prefix) Then
    '            DbControls.Add(parent)
    '        End If
    '    End If
    '    For Each ctrl As Control In parent.Controls
    '        If Not ctrl.ID Is Nothing Then
    '            If ctrl.ID.StartsWith(prefix) Then
    '                DbControls.Add(ctrl)
    '            End If
    '        End If
    '        If ctrl.HasControls Then
    '            ProcessControls(ctrl)
    '        End If
    '    Next
    'End Sub
End Class
