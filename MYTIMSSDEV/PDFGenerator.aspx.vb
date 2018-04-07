Imports Winnovative.WnvHtmlConvert
Imports System.IO

Partial Class PDFGenerator
    Inherits BasePagePublic

    Public ReadOnly Property IsAttachment() As Boolean
        Get
            Return IIf(String.IsNullOrEmpty(Request.QueryString("inline")), True, False)
        End Get
    End Property

    Public ReadOnly Property Filename() As String
        Get
            Return Server.HtmlEncode(IIf(String.IsNullOrEmpty(Request.QueryString("filename")), "document.pdf", Request.QueryString("filename")))
        End Get
    End Property

    Public ReadOnly Property Url() As String
        Get
            Return IIf(String.IsNullOrEmpty(Request.QueryString("Url")), "", Request.QueryString("Url"))
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim hw As New StringWriter()
        Server.Execute(Me.Url, hw)
        Dim html As String = hw.GetStringBuilder().ToString()
        hw.Close()

        Dim pdfConverter As PdfConverter = New PdfConverter()
        'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
        'pdfConverter.PdfDocumentOptions.LeftMargin = 5
        'pdfConverter.PdfDocumentOptions.RightMargin = 5
        'pdfConverter.PdfDocumentOptions.TopMargin = 5
        'pdfConverter.PdfDocumentOptions.ShowHeader = False
        'pdfConverter.PdfDocumentOptions.ShowFooter = False

        ' set the demo license key
        'pdfConverter.LicenseKey = "Q2hzY3Jjc2N3bXNjcHJtcnFtenp6eg=="
        ' set license key
        pdfConverter.LicenseKey = "+NPK2MnYzdjP1sjYy8nWycrWwcHBwQ=="

        Dim baseUrl As String = HttpContext.Current.Request.Url.AbsoluteUri
        'Dim pdfDocumentBytes As Byte() = pdfConverter.GetPdfBytesFromUrl(Me.Url)
        Dim pdfDocumentBytes As Byte() = pdfConverter.GetPdfBytesFromHtmlString(html, baseUrl)

        If Me.IsAttachment Then
            Response.AddHeader("Content-Disposition", [String].Format("attachment; filename={0}; size={1}", Filename(), pdfDocumentBytes.Length.ToString()))
        Else
            Response.AddHeader("Content-Disposition", [String].Format("inline; filename={0}; size={1}", Filename(), pdfDocumentBytes.Length.ToString()))
        End If
        Response.BinaryWrite(pdfDocumentBytes)
        Response.End()
    End Sub
End Class
