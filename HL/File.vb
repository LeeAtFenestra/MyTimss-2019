<Serializable()> Public Class File

#Region "Constructors"
    Public Sub New(dr As DataRow)
        Me.FileId = dr("FileId")
        Me.EFileType = dr("EFileTypeId")
        Me.DistrictName = dr("D_name")
        'Me.EFileType = HL.EFileType.Grade
        Me.Frame_N_ = dr("Frame_N_")
        Me.ID = dr("id")
        Me.LEAID = dr("LEAID")
        Me.SchoolName = dr("s_name")
        Me.TotalRows = dr("TotalRows")
        Me.UploadDT = dr("UploadDT")

        Me.SmpGrd = dr("SmpGrd")
        If Me.SmpGrd = 4 Then
            mFileHasGrade4 = True
        ElseIf dr("SmpGrd") = 8 Then
            mFileHasGrade8 = True
        ElseIf dr("SmpGrd") = 12 Then
            mFileHasGrade12 = True
        End If
    End Sub

#End Region

#Region "Properties"

    Private mSmpGrd As Integer = -1
    Public Property SmpGrd() As Integer
        Get
            Return mSmpGrd
        End Get
        Set(ByVal value As Integer)
            mSmpGrd = value
        End Set
    End Property

    Public ReadOnly Property SmpGrdString() As String
        Get
            Dim result As String = "undefined"
            If Me.SmpGrd = 4 Then
                result = "fourth"
            ElseIf Me.SmpGrd = 8 Then
                result = "eighth"
            ElseIf Me.SmpGrd = 12 Then
                result = "twelfth"
            End If
            Return result
        End Get
    End Property

    Private mFileId As Integer
    Public Property FileId() As Integer
        Get
            Return mFileId
        End Get
        Set(ByVal value As Integer)
            mFileId = value
        End Set
    End Property

    Private mFileHasGrade4 As Boolean = False
    Public ReadOnly Property FileHasGrade4() As Boolean
        Get
            Return mFileHasGrade4
        End Get
        'Set(ByVal value As Boolean)
        '    mFileHasGrade4 = value
        'End Set
    End Property

    Private mFileHasGrade8 As Boolean = False
    Public ReadOnly Property FileHasGrade8() As Boolean
        Get
            Return mFileHasGrade8
        End Get
        'Set(ByVal value As Boolean)
        '    mFileHasGrade8 = value
        'End Set
    End Property

    Private mFileHasGrade12 As Boolean = False
    Public ReadOnly Property FileHasGrade12() As Boolean
        Get
            Return mFileHasGrade12
        End Get
        'Set(ByVal value As Boolean)
        '    mFileHasGrade12 = value
        'End Set
    End Property

    Private mTotalRows As Integer
    Public Property TotalRows() As Integer
        Get
            Return mTotalRows
        End Get
        Set(ByVal value As Integer)
            mTotalRows = value
        End Set
    End Property

    Private mEFileType As EFileType
    Public Property EFileType() As EFileType
        Get
            Return mEFileType
        End Get
        Set(ByVal value As EFileType)
            mEFileType = value
        End Set
    End Property

    Private mFIPSST As String
    Public Property FIPSST() As String
        Get
            Return mFIPSST
        End Get
        Set(ByVal value As String)
            mFIPSST = value
        End Set
    End Property


    Private mStateName As String
    Public Property StateName() As String
        Get
            Return mStateName
        End Get
        Set(ByVal value As String)
            mStateName = value
        End Set
    End Property

    Private mLEAID As String
    Public Property LEAID() As String
        Get
            Return mLEAID
        End Get
        Set(ByVal value As String)
            mLEAID = value
        End Set
    End Property

    Private mDistrictName As String
    Public Property DistrictName() As String
        Get
            Return mDistrictName
        End Get
        Set(ByVal value As String)
            mDistrictName = value
        End Set
    End Property

    Private mSchoolName As String
    Public Property SchoolName() As String
        Get
            Return mSchoolName
        End Get
        Set(ByVal value As String)
            mSchoolName = value
        End Set
    End Property

    Private mFrame_N_ As String
    Public Property Frame_N_() As String
        Get
            Return mFrame_N_
        End Get
        Set(ByVal value As String)
            mFrame_N_ = value
        End Set
    End Property

    Private mID As String
    Public Property ID() As String
        Get
            Return mID
        End Get
        Set(ByVal value As String)
            mID = value
        End Set
    End Property


    Private mUploadDT As DateTime
    Public Property UploadDT() As DateTime
        Get
            Return mUploadDT
        End Get
        Set(ByVal value As DateTime)
            mUploadDT = value
        End Set
    End Property



#End Region

    Public Function IsSingleGradeFile() As Boolean

        If Me.EFileType = EFileType.Grade Then 'OrElse EFileType = EFileType.Grade_SubmitTeacherList Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function IsMultipleGradeFile() As Boolean

        'If Me.EFileType = EFileType.District OrElse _
        '    Me.EFileType = EFileType.State OrElse _
        '    Me.EFileType = EFileType.District_SubmitTeacherList OrElse _
        '    Me.EFileType = EFileType.State_SubmitTeacherList Then
        '    Return True
        'Else
        '    Return False
        'End If

        Return False
    End Function

    Public Function IsSubmitTeacherList() As Boolean

        If Me.EFileType = EFileType.Teacher Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function ProjectName() As String
        'Dim result As String = IIf(Me.ID.EndsWith("6"), "ICILS", "eTIMSS")
        Dim result As String = ""

        If Me.ID.EndsWith("6") Then
            result = "ICILS"
        ElseIf Me.ID.EndsWith("7") Then
            result = "eTIMSS"
        ElseIf Me.ID.EndsWith("T") Then
            result = "eTIMSS"
        ElseIf Me.ID.EndsWith("I") Then
            result = "ICILS"
        Else
            result = "eTIMSS"
        End If

        Return result
    End Function
    
    Public Function isICILS() As Boolean
        Return ProjectName().Equals("ICILS")
    End Function

    Public Function iseTIMSS() As Boolean
        Return ProjectName().Equals("eTIMSS")
    End Function

    'Public Function isICILS(id As String) As Boolean
    '    Return ProjectName(id).Equals("ICILS")
    'End Function

    'Public Function iseTIMSS(id As String) As Boolean
    '    Return ProjectName(id).Equals("eTIMSS")
    'End Function

End Class
