
Partial Class admin_MissingFiles
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'LabelFiles.Text = ""
        Dim dt As New DataTable("TheData")
        Dim dt2 As New DataTable("TheData2")
        Dim strConn As String = ConfigurationManager.ConnectionStrings("FAB_DefaultConnectionString").ConnectionString
        Dim efilelist As New List(Of DataRow)
        Dim stlflist As New List(Of DataRow)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.ServerFilePath, a.UploadDT from tblEfileUploads a order by a.UploadDT"

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            For Each row As DataRow In dt.Rows
                Dim ServerFilePath As String = row("ServerFilePath")
                If Not System.IO.File.Exists(ServerFilePath) Then
                    'dt.Rows.Remove(row)
                    'dt2.Rows.Add(row)
                    efilelist.Add(row)
                    'LabelFiles.Text = LabelFiles.Text & ServerFilePath & " (" & row("UploadDT") & ")" & "<br />"
                End If
            Next

            sql = "select a.STLFDocument from uv_Customize a where a.STLFUploaded = 'YES'"

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt2)
            For Each row As DataRow In dt2.Rows
                Dim ServerFilePath As String = Server.MapPath(row("STLFDocument"))
                If Not System.IO.File.Exists(ServerFilePath) Then
                    stlflist.Add(row)
                End If
            Next
        End Using

        LabelMissingEfiles.Text = efilelist.Count
        RepeaterEfileList.DataSource = efilelist
        RepeaterEfileList.DataBind()


        LabelMissingSTLF.Text = stlflist.Count
        RepeaterSTLFList.DataSource = stlflist
        RepeaterSTLFList.DataBind()
    End Sub
End Class
