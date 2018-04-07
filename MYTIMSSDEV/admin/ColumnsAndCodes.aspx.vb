
Partial Class admin_ColumnsAndCodes
    Inherits BasePagePublic


    Public Property LastVerifyColumnHeader() As String
        Get
            If ViewState("LastVerifyColumnHeader") = Nothing Then
                ViewState("LastVerifyColumnHeader") = ""
            End If
            Return ViewState("LastVerifyColumnHeader")
        End Get
        Set(ByVal value As String)
            ViewState("LastVerifyColumnHeader") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        
        RepeaterList.DataSource = TimssBll.GetColumnsAndCodes()
        RepeaterList.DataBind()

    End Sub

    Public Function HandleVerifyColumnHeaderVisibility(current As String) As Boolean
        If Me.LastVerifyColumnHeader.Equals(current) Then
            Return False
        Else
            Me.LastVerifyColumnHeader = current
            Return True
        End If
    End Function
End Class
