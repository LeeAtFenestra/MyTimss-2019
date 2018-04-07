
Partial Class admin_LoginActivity
    Inherits BasePagePublic

    Private Function GetXMLMessage(ByVal LogID As String) As String
        Dim SQLQ As New StringBuilder

        SQLQ.Append("SELECT Message FROM Log Where LogID='")
        SQLQ.Append(LogID)
        SQLQ.Append("'")

        Dim db As Database = DatabaseFactory.CreateDatabase()
        Try
            Dim dbCommand As Common.DbCommand = db.GetSqlStringCommand(SQLQ.ToString)
            Dim Message As String
            Message = ""
            Dim reader As IDataReader = db.ExecuteReader(dbCommand)
            reader.Read()
            Message = reader("Message").ToString
            Return Message
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dvwLoginActivityDetail.Visible = False
    End Sub

    Protected Sub gvwLoginLookup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwLoginLookup.SelectedIndexChanged
        dsXmlLoginInfo.Data = GetXMLMessage(gvwLoginLookup.Rows(gvwLoginLookup.SelectedIndex).Cells(1).Text.ToString)
        dvwLoginActivityDetail.Visible = True
    End Sub

    Protected Sub gvwLoginLookup_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwLoginLookup.PageIndexChanged
        dvwLoginActivityDetail.Visible = False
    End Sub

    Protected Sub rbViewAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbViewAll.CheckedChanged
        mvwLoginSearch.Visible = False
        gvwLoginLookup.Visible = True
        Dim strSearchSQL As String = "SELECT [LogID], [EventID], [Title], [Timestamp] FROM [Log] WHERE ([Title] LIKE '%LoginActivity%')"
        dsSqlLoginLookup.SelectCommand = strSearchSQL
    End Sub

    Protected Sub rbFilterByLoginStatus_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbFilterByLoginStatus.CheckedChanged
        mvwLoginSearch.ActiveViewIndex = 0
        mvwLoginSearch.Visible = True
        gvwLoginLookup.Visible = False
    End Sub

    Protected Sub rbAdvFilter_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbAdvFilter.CheckedChanged
        mvwLoginSearch.ActiveViewIndex = 1
        mvwLoginSearch.Visible = True
        gvwLoginLookup.Visible = False
    End Sub

    Protected Sub ddlLoginStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLoginStatus.SelectedIndexChanged
        Dim strSearchSQL As String
        gvwLoginLookup.Visible = True
        strSearchSQL = "SELECT [LogID], [EventID], [Title], [Timestamp] FROM [Log] WHERE ([Title] LIKE '%" + ddlLoginStatus.Text + "%')"
        dsSqlLoginLookup.SelectCommand = strSearchSQL
    End Sub

    Protected Sub calStartDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calStartDate.SelectionChanged
        txtBeginDate.Text = calStartDate.SelectedDate
        calStartDate.Visible = False
    End Sub

    Protected Sub ibtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSearch.Click
        If calStartDate.Visible = True Then
            calStartDate.Visible = False
        Else
            calStartDate.Visible = True
        End If
    End Sub

    Protected Sub ibtnEndDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEndDate.Click
        If calEndDate.Visible = True Then
            calEndDate.Visible = False
        Else
            calEndDate.Visible = True
        End If
    End Sub

    Protected Sub calEndDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEndDate.SelectionChanged
        txtEndDate.Text = calEndDate.SelectedDate
        calEndDate.Visible = False
    End Sub

    Protected Sub btnSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
        Dim strSearchSQL As String
        Dim strCrit As String

        strCrit = ""
        If txtBeginDate.Text <> "" Then
            If strCrit <> "" Then
                strCrit = strCrit & " AND "
            End If

            strCrit = strCrit & "[Timestamp ] >= '" & txtBeginDate.Text & "'"
        End If
        If txtEndDate.Text <> "" Then
            If strCrit <> "" Then
                strCrit = strCrit & " AND "
            End If

            strCrit = strCrit & "[Timestamp ]<= '" & txtEndDate.Text & "'"
        End If
        gvwLoginLookup.Visible = True
        strSearchSQL = "SELECT [LogID], [EventID], [Title], [Timestamp] FROM [Log] WHERE ([Title] LIKE '%" + ddlLoginStatus1.Text + "%') AND (" & strCrit & ")"
        dsSqlLoginLookup.SelectCommand = strSearchSQL
    End Sub
End Class
