Public Class SelectFromDatabaseArgs

    Public Sub New()
        mFilterParameters = New List(Of FilterParameter)
        mSortParameters = New List(Of SortParameter)
    End Sub


    Private mFilterParameters As List(Of FilterParameter)
    Public Property FilterParameters() As List(Of FilterParameter)
        Get
            Return mFilterParameters
        End Get
        Set(ByVal value As List(Of FilterParameter))
            mFilterParameters = value
        End Set
    End Property

    Private mSortParameters As List(Of SortParameter)
    Public Property SortParameters() As List(Of SortParameter)
        Get
            Return mSortParameters
        End Get
        Set(ByVal value As List(Of SortParameter))
            mSortParameters = value
        End Set
    End Property

    Private mREPSBGRP As String
    Public Property REPSBGRP() As String
        Get
            Return mREPSBGRP
        End Get
        Set(ByVal value As String)
            mREPSBGRP = value
        End Set
    End Property

    Private mWINSID As String
    Public Property WINSID() As String
        Get
            Return mWINSID
        End Get
        Set(ByVal value As String)
            mWINSID = value
        End Set
    End Property

    Private mIsNaepStateCoordinator As Boolean = False
    Public Property IsNaepStateCoordinator() As Boolean
        Get
            Return mIsNaepStateCoordinator
        End Get
        Set(ByVal value As Boolean)
            mIsNaepStateCoordinator = value
        End Set
    End Property


    Private mIsTestAdministrator As Boolean = False
    Public Property IsTestAdministrator() As Boolean
        Get
            Return mIsTestAdministrator
        End Get
        Set(ByVal value As Boolean)
            mIsTestAdministrator = value
        End Set
    End Property

    Private mIsFieldManager As Boolean = False
    Public Property IsFieldManager() As Boolean
        Get
            Return mIsFieldManager
        End Get
        Set(ByVal value As Boolean)
            mIsFieldManager = value
        End Set
    End Property

    Private mIsFieldDirector As Boolean = False
    Public Property IsFieldDirector() As Boolean
        Get
            Return mIsFieldDirector
        End Get
        Set(ByVal value As Boolean)
            mIsFieldDirector = value
        End Set
    End Property

    Private mFilterShortcut As String
    Public Property FilterShortcut() As String
        Get
            Return mFilterShortcut
        End Get
        Set(ByVal value As String)
            mFilterShortcut = value
        End Set
    End Property

    Private mIsHomeOffice As Boolean
    Public Property IsHomeOffice() As Boolean
        Get
            Return mIsHomeOffice
        End Get
        Set(ByVal value As Boolean)
            mIsHomeOffice = value
        End Set
    End Property

    Private mIsAdmin As Boolean
    Public Property IsAdmin() As Boolean
        Get
            Return mIsAdmin
        End Get
        Set(ByVal value As Boolean)
            mIsAdmin = value
        End Set
    End Property

    Public Function ContainsThisFilterParameter(FilterColumn As String) As Boolean
        Dim result As Boolean = False
        For Each p As FilterParameter In Me.FilterParameters
            If p.FilterColumn.Equals(FilterColumn) Then
                result = True
                Exit For
            End If
        Next
        Return result
    End Function

    Public Sub RemoveFilterParameter(FilterColumn As String)
        Dim result As Boolean = False
        For index = Me.FilterParameters.Count - 1 To 0 Step -1
            'If Me.FilterParameters(index).FilterColumn.Equals(FilterColumn, StringComparison.CurrentCultureIgnoreCase) Then
            If Me.FilterParameters(index).FilterColumn.StartsWith(FilterColumn, StringComparison.CurrentCultureIgnoreCase) Then
                Me.FilterParameters.RemoveAt(index)
            End If
        Next
    End Sub

    Private mIsMyTIMSSUser As Boolean
    Public Property IsMyTIMSSUser() As Boolean
        Get
            Return mIsMyTIMSSUser
        End Get
        Set(ByVal value As Boolean)
            mIsMyTIMSSUser = value
        End Set
    End Property

    Private mIsTudaCoordinator As Boolean
    Public Property IsTudaCoordinator() As Boolean
        Get
            Return mIsTudaCoordinator
        End Get
        Set(ByVal value As Boolean)
            mIsTudaCoordinator = value
        End Set
    End Property

    Private mTUA_LEA As String
    Public Property TUA_LEA() As String
        Get
            Return mTUA_LEA
        End Get
        Set(ByVal value As String)
            mTUA_LEA = value
        End Set
    End Property


    Private mIsTestAdministratorTroubleShooter As Boolean = False
    Public Property IsTestAdministratorTroubleShooter() As Boolean
        Get
            Return mIsTestAdministratorTroubleShooter
        End Get
        Set(ByVal value As Boolean)
            mIsTestAdministrator = value
        End Set
    End Property
End Class
