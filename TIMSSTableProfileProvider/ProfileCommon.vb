Imports System
Imports System.Web.Profile
Imports System.Web.Security
Imports System.Web.Configuration

Public Class ProfileCommon
    Inherits System.Web.Profile.ProfileBase
    Public Shared Function GetUserProfile(ByVal username As String) As ProfileCommon
        Return TryCast(Create(username), ProfileCommon)
    End Function

    Public Shared Function GetUserProfile() As ProfileCommon
        If Membership.GetUser() Is Nothing Then
            Return Nothing
        Else
            Return TryCast(Create(Membership.GetUser().UserName), ProfileCommon)
        End If
    End Function

    <CustomProviderData("ProjectStaffId;nvarchar")> _
    Public Overridable Property ProjectStaffId() As String
        Get
            Return DirectCast(Me.GetPropertyValue("ProjectStaffId"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("ProjectStaffId", value)
        End Set
    End Property


    <CustomProviderData("Prefix;nvarchar")> _
    Public Overridable Property Prefix() As String
        Get
            Return DirectCast(Me.GetPropertyValue("Prefix"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("Prefix", value)
        End Set
    End Property



    <CustomProviderData("FirstName;nvarchar")> _
    Public Overridable Property FirstName() As String
        Get
            Return DirectCast(Me.GetPropertyValue("FirstName"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("FirstName", value)
        End Set
    End Property

    <CustomProviderData("MiddleName;nvarchar")> _
    Public Overridable Property MiddleName() As String
        Get
            Return DirectCast(Me.GetPropertyValue("MiddleName"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("MiddleName", value)
        End Set
    End Property

    <CustomProviderData("LastName;nvarchar")> _
    Public Overridable Property LastName() As String
        Get
            Return DirectCast(Me.GetPropertyValue("LastName"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("LastName", value)
        End Set
    End Property

    <CustomProviderData("WINSID;nvarchar")> _
    Public Overridable Property WINSID() As String
        Get
            Return DirectCast(Me.GetPropertyValue("WINSID"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("WINSID", value)
        End Set
    End Property

    <CustomProviderData("LastPageSize;int")> _
    Public Overridable Property LastPageSize() As Integer
        Get
            Return DirectCast(Me.GetPropertyValue("LastPageSize"), Integer)
        End Get
        Set(ByVal value As Integer)
            Me.SetPropertyValue("LastPageSize", value)
        End Set
    End Property

    <CustomProviderData("LastArea;nvarchar")> _
    Public Overridable Property LastArea() As String
        Get
            Return DirectCast(Me.GetPropertyValue("LastArea"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("LastArea", value)
        End Set
    End Property

    <CustomProviderData("LastRegion;nvarchar")> _
    Public Overridable Property LastRegion() As String
        Get
            Return DirectCast(Me.GetPropertyValue("LastRegion"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("LastRegion", value)
        End Set
    End Property



    <CustomProviderData("REPSBGRP;nvarchar")> _
    Public Overridable Property REPSBGRP() As String
        Get
            Return DirectCast(Me.GetPropertyValue("REPSBGRP"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("REPSBGRP", value)
        End Set
    End Property

    <CustomProviderData("Telephone;nvarchar")> _
    Public Overridable Property Telephone() As String
        Get
            Return DirectCast(Me.GetPropertyValue("Telephone"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("Telephone", value)
        End Set
    End Property

    <CustomProviderData("TelephoneExtension;nvarchar")> _
    Public Overridable Property TelephoneExtension() As String
        Get
            Return DirectCast(Me.GetPropertyValue("TelephoneExtension"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("TelephoneExtension", value)
        End Set
    End Property

    <CustomProviderData("RegistrationId;nvarchar")> _
    Public Overridable Property RegistrationId() As String
        Get
            Return DirectCast(Me.GetPropertyValue("RegistrationId"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("RegistrationId", value)
        End Set
    End Property

    <CustomProviderData("Frame_N_;char")> _
    Public Overridable Property Frame_N_() As String
        Get
            Return DirectCast(Me.GetPropertyValue("Frame_N_"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("Frame_N_", value)
        End Set
    End Property

    <CustomProviderData("TUA_LEA;char")> _
    Public Overridable Property TUA_LEA() As String
        Get
            Return DirectCast(Me.GetPropertyValue("TUA_LEA"), String)
        End Get
        Set(ByVal value As String)
            Me.SetPropertyValue("TUA_LEA", value)
        End Set
    End Property

End Class
