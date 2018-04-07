<Serializable()> Public Class NameValuePair

#Region "Constructors"

    Public Sub New(name As String, value As String)
        Me.Name = name
        Me.Value = value
    End Sub

    Public Sub New(bytes As Byte(), name As String)
        Me.Name = name
        Me.Bytes = bytes
    End Sub

#End Region

#Region "Properties"

    Private mName As String
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    Private mValue As String
    Public Property Value() As String
        Get
            Return mValue
        End Get
        Set(ByVal value As String)
            mValue = value
        End Set
    End Property

    Private mBytes As Byte()
    Public Property Bytes() As Byte()
        Get
            Return mBytes
        End Get
        Set(ByVal value As Byte())
            mBytes = value
        End Set
    End Property

#End Region


End Class
