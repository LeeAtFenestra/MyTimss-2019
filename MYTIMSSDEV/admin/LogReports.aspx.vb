Imports System.IO
Partial Class admin_LogReports
    Inherits BasePagePublic
    Protected Sub calStart_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calStart.SelectionChanged
        txtStartDate.Text = calStart.SelectedDate
        calStart.Visible = False
    End Sub
    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        cnAll.SelectParameters.Clear()
        'todo clear details
        Me.dvMessageDetails.DataSourceID = ""
        If String.IsNullOrEmpty(Me.txtStartDate.Text) Then
            Me.cnAll.SelectParameters.Add("StartDate", "1/1/1900")
        Else
            Me.cnAll.SelectParameters.Add("StartDate", Format(DateTime.Parse(Me.txtStartDate.Text), "MM/dd/yy"))
        End If
        If String.IsNullOrEmpty(Me.txtStartDate.Text) Then
            Me.cnAll.SelectParameters.Add("EndDate", "1/1/1900")
        ElseIf String.IsNullOrEmpty(Me.txtEndDate.Text) Then
            Me.cnAll.SelectParameters.Add("EndDate", Format(DateTime.Parse(Me.txtStartDate.Text), "MM/dd/yy"))
        Else
            Me.cnAll.SelectParameters.Add("EndDate", Format(DateTime.Parse(Me.txtEndDate.Text), "Short Date"))
        End If

        If Me.ddlRequestURL.SelectedValue = "All" Then
            Me.cnAll.SelectParameters.Add("RequestURL", "All")
        Else
            Me.cnAll.SelectParameters.Add("RequestURL", Me.ddlRequestURL.SelectedValue)
        End If
        If Me.ddlEventType.SelectedValue = "All" Then
            Me.cnAll.SelectParameters.Add("EventType", "All")
        Else
            Me.cnAll.SelectParameters.Add("EventType", Me.ddlEventType.SelectedValue)
        End If
        If ddlEventCode.SelectedValue = "All" Then
            Me.cnAll.SelectParameters.Add("EventCode", 0)
            Me.cnAll.SelectParameters.Add("EventDetailCode", 0)
        Else
            Dim aCode As String()
            aCode = Me.ddlEventCode.SelectedItem.Value.Split(" ")
            Me.cnAll.SelectParameters.Add("EventCode", Int32.Parse(aCode(0)))
            Me.cnAll.SelectParameters.Add("EventDetailCode", Int32.Parse(aCode(1)))
        End If
        dvMessageDetails.DataSourceID = Me.cnDetails.UniqueID
        gvwLog.DataSourceID = Me.cnAll.UniqueID
        gvwLog.DataBind()

    End Sub

    Protected Sub gvwLog_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.dvMessageDetails.DataSourceID = "cnDetails"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblRecordCount.Visible = False
        lblRecordCountDeleted.Visible = False

        If Not IsPostBack Then
            ddlEventType.Items.Clear()
            Me.ddlRequestURL.Items.Clear()

            ddlEventType.DataSourceID = "cnFilterEventType"
            Me.ddlEventType.Items.Insert(0, "All")
            Me.ddlEventType.AppendDataBoundItems = True

            Me.ddlRequestURL.Items.Insert(0, "All")
            Me.ddlRequestURL.DataSourceID = "cnFilterRequestURL"
            Me.ddlRequestURL.AppendDataBoundItems = True
            ddlEventCode.Items.Insert(0, "All")

            cnEventCodeDetails.SelectParameters.Clear()
            Me.cnEventCodeDetails.SelectParameters.Add("EventCode", 0)
            Me.cnEventCodeDetails.SelectParameters.Add("EventDetailCode", 0)
            ddlEventCode.DataSourceID = "cnEventCodeDetails"
            ddlEventCode.AppendDataBoundItems = True
            ddlEventCode.DataBind()

            Me.ddlDaysToKeep.Items.Insert(0, "All")

            dvMessageDetails.DataSourceID = ""
            gvwLog.DataSourceID = ""
            btnFilter.Enabled = True
            Me.ddlViews.Focus()
        End If
        Me.lblAnswer.Text = ""
    End Sub

    Protected Sub gvwLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwLog.PageIndexChanging
        gvwLog.PageIndex = e.NewPageIndex
    End Sub

    Protected Sub cnAll_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles cnAll.Selected
        lblRecordCount.Visible = True
        lblRecordCount.Font.Bold = True
        lblRecordCount.Text = ""
        For Each p As Object In e.Command.Parameters
            If p.ToString = "@RecordCount" Then
                lblRecordCount.Text = "Record Count: " & p.Value.ToString
            End If
        Next
    End Sub

    Protected Sub cnAll_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles cnAll.Selecting
        Dim prm As New System.Data.SqlClient.SqlParameter
        prm.Direction = Data.ParameterDirection.Output
        prm.ParameterName = "@RecordCount"
        prm.SqlDbType = Data.SqlDbType.Int
        e.Command.Parameters.Add(prm)
    End Sub

    Protected Sub ibtnSearchStart_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSearchStart.Click
        Me.calStart.Visible = Not Me.calStart.Visible
    End Sub

    Protected Sub btnViewAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewAll.Click
        'todo clear details
        cnAll.SelectParameters.Clear()
        Me.dvMessageDetails.DataSourceID = ""
        Me.cnAll.SelectParameters.Add("StartDate", "1/1/1900")
        Me.cnAll.SelectParameters.Add("EndDate", "1/1/1900")
        Me.cnAll.SelectParameters.Add("RequestURL", "All")
        Me.cnAll.SelectParameters.Add("EventType", "All")
        Me.cnAll.SelectParameters.Add("EventCode", 0)
        Me.cnAll.SelectParameters.Add("EventDetailCode", 0)
        dvMessageDetails.DataSourceID = Me.cnDetails.UniqueID
        gvwLog.DataSourceID = Me.cnAll.UniqueID
        gvwLog.DataBind()
    End Sub

    Protected Sub ddlViews_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlViews.SelectedIndexChanged
        Select Case Me.ddlViews.SelectedIndex
            Case 1
                Me.mvViewLogs.ActiveViewIndex = 0
            Case 2
                Me.lstTextLogs.Items.Clear()
                'load log file list
                Dim strPath As String = Server.MapPath("~/Output/ErrorLog")

                Dim strDirectories() As String = Directory.GetDirectories(strPath)

                Dim filesToTest() As String
                Dim fileNames(1) As String
                Dim intNum As Int16 = -1

                'go thru through the directories to populate the file list
                For Each strDir As String In strDirectories
                    Dim blnDelete As Boolean = False
                    filesToTest = Directory.GetFiles(strDir)
                    For Each strFile As String In filesToTest
                        If strFile.ToUpper.EndsWith("TXT") OrElse strFile.ToUpper.EndsWith("DOC") Then
                            intNum += 1
                            ReDim Preserve fileNames(intNum)
                            fileNames(intNum) = Right(strFile, strFile.Length - (InStr(strFile, "errorlog", CompareMethod.Text) + 8))
                        End If
                    Next
                Next
                lstTextLogs.DataSource = fileNames
                lstTextLogs.DataBind()

                Me.mvViewLogs.ActiveViewIndex = 2
            Case 3

                Me.mvViewLogs.ActiveViewIndex = 1


            Case 0
                Me.mvViewLogs.ActiveViewIndex = -1

        End Select

    End Sub

    Protected Sub btnDeleteSQLLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSQLLog.Click
        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("FAB_DefaultConnectionString").ConnectionString
        Dim cn As SqlConnection

        lblAnswer.ForeColor = Drawing.Color.Black

        If ddlDaysToKeep.SelectedValue = "All" AndAlso Me.txtStartDateDelete.Text = "" Then
            lblAnswer.ForeColor = Drawing.Color.Red
            lblAnswer.Text = "Please select number of days or a date range"
            Exit Sub
        End If

        Using cn
            cn = New SqlConnection(strConnectionString)
            cn.Open()
            Dim cmdDeleteLog As New SqlCommand()
            cmdDeleteLog.CommandType = CommandType.StoredProcedure
            cmdDeleteLog.CommandText = "FAB_DeleteHMLogs"
            cmdDeleteLog.Parameters.Clear()
            cmdDeleteLog.Connection = cn
            Using cmdDeleteLog
                If ddlDaysToKeep.SelectedValue <> "All" Then
                    cmdDeleteLog.Parameters.AddWithValue("NoDays", Me.ddlDaysToKeep.SelectedValue)
                    cmdDeleteLog.Parameters.AddWithValue("StartDate", "1/1/1900")
                    cmdDeleteLog.Parameters.AddWithValue("EndDate", "1/1/1900")
                Else
                    If Me.txtStartDateDelete.Text <> "" Then
                        If Me.txtEndDateDelete.Text <> "" Then
                            cmdDeleteLog.Parameters.AddWithValue("NoDays", 0)
                            cmdDeleteLog.Parameters.AddWithValue("StartDate", txtStartDateDelete.Text)
                            cmdDeleteLog.Parameters.AddWithValue("EndDate", txtEndDateDelete.Text)
                        Else
                            cmdDeleteLog.Parameters.AddWithValue("NoDays", 0)
                            cmdDeleteLog.Parameters.AddWithValue("StartDate", txtStartDateDelete.Text)
                            cmdDeleteLog.Parameters.AddWithValue("EndDate", txtStartDateDelete.Text)
                        End If
                    End If
                End If

                'create output parameter
                Dim prm As New SqlParameter("Count", SqlDbType.BigInt, Nothing, ParameterDirection.Output, True, Nothing, Nothing, Nothing, DataRowVersion.Original, Nothing)
                cmdDeleteLog.Parameters.Add(prm)

                Try
                    cmdDeleteLog.ExecuteNonQuery()
                Catch ex As Exception
                    Me.lblAnswer.Text = "Problem Deleting Logs. <br/>" & ex.Message
                    Exit Sub
                End Try
                lblAnswer.Visible = True
                lblAnswer.Font.Bold = True
                lblAnswer.Text = ""
                If Not String.IsNullOrEmpty(prm.Value.ToString) Then
                    lblRecordCountDeleted.Text = "Number of deleted records: " & prm.Value.ToString
                End If
                Me.txtEndDateDelete.Text = ""
                Me.txtStartDateDelete.Text = ""
                Me.ddlDaysToKeep.SelectedIndex = -1
                cmdDeleteLog = Nothing
            End Using
            cn.Close()
            cn = Nothing
        End Using
        Me.lblAnswer.Text = "Logs Deleted Successfully"

    End Sub

    Protected Sub btnDeleteTextLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteTextLog.Click

        Dim strPath As String = Server.MapPath("~/Output/ErrorLog")

        Dim strDirectories() As String = Directory.GetDirectories(strPath)

        Dim filesToTest() As String

        Dim filesToKeep() As String
        Dim filesToDelete() As String

        Dim strFile As String

        Dim intToday As Int16 = DateTime.Now.Day
        Dim dtEndDate As DateTime
        If ddlDaysToKeep.SelectedValue <> "All" Then
            dtEndDate = DateTime.Now.AddDays(7)
            'delete all records except for these
            'populate an array with all the days/files to keep
            For intDays As Int16 = 0 To (Int16.Parse(ddlDaysToKeep.SelectedValue) - (2 * Int16.Parse(ddlDaysToKeep.SelectedValue))) + 1 Step -1
                Dim datDate As DateTime = DateTime.Now.AddDays(intDays)
                ReDim Preserve filesToKeep((intDays + -(2 * intDays)))
                Dim strDate As String = datDate.Month
                If strDate.Length = 1 Then
                    strDate = "0" & strDate
                End If
                Dim strDay As String = datDate.Day
                If strDay.Length = 1 Then
                    strDay = "0" & strDay
                End If
                filesToKeep((intDays + -(2 * intDays))) = datDate.Year & "-" & strDate & "-" & strDay & ".txt"
            Next
            'go thru through the directories
            Try
                For Each strDir As String In strDirectories
                    Dim blnDelete As Boolean = False
                    filesToTest = Directory.GetFiles(strDir)
                    For Each strFile In filesToTest
                        'go through the files in the directory
                        For Each strFileToKeep As String In filesToKeep
                            If (Right(strFile, strFile.Length - InStrRev(strFile, "\"))) = (strFileToKeep) Then
                                'keep it
                                blnDelete = False
                                Exit For
                            Else
                                'delete it
                                blnDelete = True
                            End If
                        Next
                        If blnDelete = True Then
                            System.IO.File.Delete(strFile)
                        End If
                        blnDelete = False
                    Next
                    'delete directory if it's empty
                    If filesToTest.Length = 0 Then
                        Directory.Delete(strDir)
                    End If
                Next
            Catch ex As System.UnauthorizedAccessException
                Me.lblAnswer.Text = "Unauthorized access attempted."
            End Try
            'if no error
            Me.lblAnswer.Text = "Logs Deleted Successfully"
        ElseIf Not String.IsNullOrEmpty(Me.txtStartDateDelete.Text) Then
            Dim dtStartDate As DateTime = Me.txtStartDateDelete.Text
            If Me.txtStartDateDelete.Text > Today.Date Then
                Me.lblAnswer.Text = "Please select a date that is less than today"
                txtStartDateDelete.Focus()
                Exit Sub
            End If
            If Not String.IsNullOrEmpty(Me.txtEndDateDelete.Text) Then
                dtEndDate = Me.txtEndDateDelete.Text
            Else
                dtEndDate = Me.txtStartDateDelete.Text
            End If
            Dim intNoDaysToKeep As TimeSpan = dtEndDate.Subtract(dtStartDate)
            'populate an array with all the days/files to keep
            For intDays As Int16 = 0 To (intNoDaysToKeep.TotalDays) Step 1
                Dim dtStart As DateTime
                ReDim Preserve filesToDelete(intDays)
                dtStart = dtStartDate.AddDays(intDays)
                Dim strDate As String = dtStart.Month
                If strDate.Length = 1 Then
                    strDate = "0" & strDate
                End If
                Dim strDay As String = dtStart.Day
                If strDay.Length = 1 Then
                    strDay = "0" & strDay
                End If
                filesToDelete(intDays) = dtStart.Year & "-" & strDate & "-" & strDay & ".txt"
            Next
            'go thru through the directories
            Try
                For Each strDir As String In strDirectories
                    Dim blnDelete As Boolean = False
                    filesToTest = Directory.GetFiles(strDir)
                    For Each strFile In filesToTest
                        'go through the files in the directory
                        For Each strFileToDelete As String In filesToDelete
                            If (Right(strFile, strFile.Length - InStrRev(strFile, "\"))) = (strFileToDelete) Then
                                'delete it
                                blnDelete = True
                                Exit For
                            Else
                                'delete it
                                blnDelete = False
                            End If
                        Next
                        If blnDelete = True Then
                            System.IO.File.Delete(strFile)
                        End If
                        blnDelete = False
                    Next
                    'delete directory if it's empty
                    If filesToTest.Length = 0 Then
                        Directory.Delete(strDir)
                    End If
                Next
            Catch ex As System.UnauthorizedAccessException
                Me.lblAnswer.Text = "Unauthorized access attempted."
            End Try
            'if no error
            Me.lblAnswer.Text = "Logs Deleted Successfully"
        Else
            Me.lblAnswer.Text = "Please select a range or amount of days then click delete"
        End If
        Me.ddlDaysToKeep.SelectedIndex = -1
        Me.txtStartDateDelete.Text = ""
        Me.txtEndDateDelete.Text = ""

    End Sub

    Protected Sub ibtnStartDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnStartDelete.Click
        Me.calStartDelete.Visible = Not Me.calStartDelete.Visible
    End Sub

    Protected Sub ibtnEndDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEndDelete.Click
        Me.calEndDelete.Visible = Not Me.calEndDelete.Visible
    End Sub

    Protected Sub calStartDelete_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calStartDelete.SelectionChanged
        txtStartDateDelete.Text = calStartDelete.SelectedDate
        calStartDelete.Visible = False
    End Sub

    Protected Sub calEndDelete_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEndDelete.SelectionChanged
        txtEndDateDelete.Text = calEndDelete.SelectedDate
        calEndDelete.Visible = False
    End Sub

    Protected Sub calEnd_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEnd.SelectionChanged
        txtEndDate.Text = calEnd.SelectedDate
        calEnd.Visible = False
    End Sub

    Protected Sub ibtnSearchEnd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnSearchEnd.Click
        Me.calEnd.Visible = Not Me.calEnd.Visible
    End Sub

    Protected Sub lstTextLogs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTextLogs.SelectedIndexChanged
        Dim strdoc As String
        strdoc = lstTextLogs.SelectedValue

        Dim strIFrame As String = "<iframe src=""../../Output/errorlog/" & strdoc & """ width=""700px"" id=""MyFrame"" frameborder=""0"" style=""height: 800px; vertical-align: top; text-align: left;""></iframe> "
        Dim IFrame As New System.Web.UI.WebControls.Literal
        IFrame.Text = strIFrame
        Me.pnlDoc.Controls.Add(IFrame)
        Me.pnlDoc.Visible = True
    End Sub

End Class
