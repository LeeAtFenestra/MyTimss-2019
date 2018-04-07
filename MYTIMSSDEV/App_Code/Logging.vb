Imports Microsoft.VisualBasic
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports System.Text

Public Class Logging
    Private m_ErrorMsg As String
    Private m_ErrorType As String

    Public Enum EventPriority
        Low = 0
        Normal = 1
        High = 2
    End Enum
    Public Property ErrorMsg() As String
        Get
            Return m_ErrorMsg
        End Get
        Set(ByVal value As String)
            m_ErrorMsg = value
        End Set
    End Property

    Public Property ErrorType() As String
        Get
            Return m_ErrorType
        End Get
        Set(ByVal value As String)
            m_ErrorType = value
        End Set
    End Property

    Public Shared Sub LogErrorToFile(ByVal ex As Exception)
        Dim ErrorMsgSB As New StringBuilder

        With ErrorMsgSB
            .Append("ExceptionType:" & ex.Message + "" & vbCrLf)
            .Append(" SOURCE: " & ex.Source & "" & vbCrLf)
            .Append(" TARGETSITE: " & ex.TargetSite.ToString & vbCrLf)
            If ex.InnerException.ToString IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.ToString) Then
                .Append(" Inner Exception: " & ex.InnerException.ToString & vbCrLf)
            End If
            .Append(" STACKTRACE: " + ex.StackTrace.ToString)
        End With
        Logger.Write(ErrorMsgSB.ToString)
    End Sub
    Public Shared Sub LogErrorToDB(ByVal ex As Exception, ByVal ErrorPriority As Integer, ByVal EventID As Integer, ByVal Title As String, Optional ByVal Source As String = "")
        Dim logEntry As New LogEntry
        With logEntry
            .TimeStamp = DateTime.Now
            .Categories.Add("Error")
            If EventID <> Nothing AndAlso EventID > 0 Then
                .EventId = EventID
            End If
            .Title = Title
            .Priority = ErrorPriority
            If ex.InnerException IsNot Nothing Then
                .AddErrorMessage(ex.InnerException.Message.ToString)
                .Message = ex.InnerException.StackTrace.ToString
            Else
                .Message = ex.ToString
            End If
            .ExtendedProperties.Add("Source", Source)
            .Severity = Diagnostics.TraceEventType.Error
        End With
        Try
            Logger.Write(logEntry)
        Catch exLogger As Exception
        End Try
    End Sub
    Public Shared Sub LogInfoToDB(ByVal Message As String, ByVal InfoPriority As Integer, ByVal Title As String, ByVal Source As String, Optional ByVal EventID As Integer = 0)
        Dim cr As New Westat.FAB.Configuration.ConfigurationReader
        If cr.AuditLogging = "Yes" Then
            Dim logEntry As New LogEntry
            With logEntry
                .TimeStamp = DateTime.Now
                .Categories.Add("Audit")
                If EventID > 0 Then
                    .EventId = EventID
                End If
                .Title = Title
                .Priority = InfoPriority
                .Message = Message
                .ExtendedProperties.Add("Source", Source)
            End With
            Try
                Logger.Write(logEntry)
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class

