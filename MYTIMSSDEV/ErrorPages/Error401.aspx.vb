Partial Public Class Error401
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ' Error pages receive the path that caused the error on the query string: AspxErrorPath
        Dim errPath As String = "<i>No error path information is available.</i>"

        Dim o As Object = Request.QueryString("AspxErrorPath")
        If Not o Is Nothing Then
            errPath = CType(o, String)
        End If
        ErrorPathSPan.InnerHtml = errPath
    End Sub
End Class
