Imports Westat.TIMSS.BLL

Partial Class TIMSSEFILE2015_TIMSSEFILE2015
    Inherits System.Web.UI.MasterPage


    Private mTimssBll As TIMSSBLL
    Public ReadOnly Property TimssBll() As TIMSSBLL
        Get
            If mTimssBll Is Nothing Then
                mTimssBll = New TIMSSBLL()
            End If
            Return mTimssBll
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Response.Write(prof.FirstName())
            Me.DataBind()
        End If
    End Sub
    Public Function HighlightMenuItemCSS(targetList As String) As String
        Dim result As String = "menu"
        Dim targets As String() = targetList.Split("|")
        For Each target As String In targets
            If Request.ServerVariables("SCRIPT_NAME").ToLower().EndsWith(target.ToLower()) Then
                result = "menuOn"
                Exit For
            End If
        Next
        Return result
    End Function

    Sub HeadLoginStatus_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs)
        TimssBll.CleanUpSessionVariables()
    End Sub
End Class

