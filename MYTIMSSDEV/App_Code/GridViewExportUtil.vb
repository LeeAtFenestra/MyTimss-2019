Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.Control
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
'Imports System.IO
'Imports System.Data
Imports ClosedXML.Excel

Public Class GridViewExportUtil

    Public Shared Sub Export2(fileName As String, gv As GridView, skipcolumn1 As Boolean)

        Dim startcolumn As Integer = IIf(skipcolumn1, 1, 0)

        Dim dt As New DataTable(fileName.Replace(".xlsx", ""))

        If Not gv.HeaderRow Is Nothing Then

            For i As Integer = startcolumn To gv.HeaderRow.Cells.Count - 1
                Dim cell As TableCell = gv.HeaderRow.Cells(i)
                'For Each cell As TableCell In gv.HeaderRow.Cells
                If cell.Controls.Count > 0 Then
                    Dim button As LinkButton = DirectCast(cell.Controls(0), LinkButton)
                    If Not (button Is Nothing) Then
                        dt.Columns.Add(button.Text)
                    Else
                        dt.Columns.Add(cell.Text.Replace("&nbsp;", " "))
                    End If
                Else
                    dt.Columns.Add(cell.Text.Replace("&nbsp;", " "))
                End If
            Next

        End If
        For Each row As GridViewRow In gv.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - (1 + startcolumn)
                Dim cell As TableCell = row.Cells(i + startcolumn)
                If cell.Controls.Count > 0 Then
                    Dim cntr As Control = cell.Controls(0)
                    If TypeOf cntr Is DataBoundLiteralControl Then
                        dt.Rows(dt.Rows.Count - 1)(i) = HttpContext.Current.Server.HtmlDecode(CType(cntr, DataBoundLiteralControl).Text) '.ToString.Replace("&nbsp;", " ")
                    Else
                        dt.Rows(dt.Rows.Count - 1)(i) = HttpContext.Current.Server.HtmlDecode(cell.Text) '.Replace("&nbsp;", " ")
                    End If
                Else
                    dt.Rows(dt.Rows.Count - 1)(i) = HttpContext.Current.Server.HtmlDecode(cell.Text) '.Replace("&nbsp;", " ")
                End If
                'dt.Rows(dt.Rows.Count - 1)(i) = cell.Text.Replace("&nbsp;", " ")
            Next
        Next

        If dt.Rows.Count > 0 Then

            Using wb As New XLWorkbook()
                wb.Worksheets.Add(dt)

                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.Buffer = True
                HttpContext.Current.Response.Charset = ""
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & fileName)
                Using MyMemoryStream As New MemoryStream()

                    wb.SaveAs(MyMemoryStream)
                    MyMemoryStream.WriteTo(HttpContext.Current.Response.OutputStream)
                    HttpContext.Current.Response.Flush()
                    HttpContext.Current.Response.[End]()
                End Using
            End Using

        End If

    End Sub

    Public Shared Sub Export(fileName As String, gv As GridView)
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
        'HttpContext.Current.Response.ContentType = "application/ms-excel"
        'HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"""
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

        Using sw As New StringWriter()
            Using htw As New HtmlTextWriter(sw)
                '  Create a form to contain the grid
                Dim table As New Table()
                table.BorderColor = Drawing.Color.Black
                table.BorderStyle = BorderStyle.Solid
                '  add the header row to the table
                If gv.HeaderRow IsNot Nothing Then
                    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow)
                    table.Rows.Add(gv.HeaderRow)
                    table.Rows(table.Rows.Count - 1).BorderColor = Drawing.Color.Black
                    table.Rows(table.Rows.Count - 1).BorderStyle = BorderStyle.Solid
                End If

                '  add each of the data rows to the table
                For Each row As GridViewRow In gv.Rows
                    GridViewExportUtil.PrepareControlForExport(row)
                    table.Rows.Add(row)
                    table.Rows(table.Rows.Count - 1).BorderColor = Drawing.Color.Black
                    table.Rows(table.Rows.Count - 1).BorderStyle = BorderStyle.Solid
                Next

                '  add the footer row to the table
                If gv.FooterRow IsNot Nothing Then
                    GridViewExportUtil.PrepareControlForExport(gv.FooterRow)
                    table.Rows.Add(gv.FooterRow)
                    table.Rows(table.Rows.Count - 1).BorderColor = Drawing.Color.Black
                    table.Rows(table.Rows.Count - 1).BorderStyle = BorderStyle.Solid
                End If

                '  render the table into the htmlwriter
                table.RenderControl(htw)

                '  render the htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString())
                HttpContext.Current.Response.[End]()
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Replace any of the contained controls with literals
    ''' </summary>
    ''' <param name="control"></param>
    Private Shared Sub PrepareControlForExport(control As Control)
        For i As Integer = 0 To control.Controls.Count - 1
            Dim current As Control = control.Controls(i)
            If TypeOf current Is LinkButton Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, LinkButton).Text))
            ElseIf TypeOf current Is ImageButton Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, ImageButton).AlternateText))
            ElseIf TypeOf current Is HyperLink Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, HyperLink).Text))
            ElseIf TypeOf current Is DropDownList Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(TryCast(current, DropDownList).SelectedItem.Text))
            ElseIf TypeOf current Is CheckBox Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(If(TryCast(current, CheckBox).Checked, "True", "False")))
            End If

            If current.HasControls() Then
                GridViewExportUtil.PrepareControlForExport(current)
            End If
        Next
    End Sub
End Class

