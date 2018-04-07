Imports Westat.TIMSS.HL


Partial Class SubmitStudentList
    Inherits BasePagePublic

    Public ReadOnly Property EfileTypeId() As Integer
        Get
            Return 3
        End Get
    End Property

    Public Property theFile() As File
        Get
            Return ViewState("FileId")
        End Get
        Set(ByVal value As File)
            ViewState("FileId") = value
        End Set
    End Property

    'Public Property FileId() As Integer
    '    Get
    '        If ViewState("FileId") = Nothing Then
    '            ViewState("FileId") = -1
    '        End If
    '        Return ViewState("FileId")
    '    End Get
    '    Set(ByVal value As Integer)
    '        ViewState("FileId") = value
    '    End Set
    'End Property

    Public Property ColumnsToMap() As DataTable
        Get
            'If ViewState("ColumnsToMap") = Nothing Then
            '    ViewState("ColumnsToMap") = -1
            'End If
            Return ViewState("ColumnsToMap")
        End Get
        Set(ByVal value As DataTable)
            ViewState("ColumnsToMap") = value
        End Set
    End Property

    'Public Property ColumnsToMap() As List(Of NameValuePair)
    '    Get
    '        'If ViewState("ColumnsToMap") = Nothing Then
    '        '    ViewState("ColumnsToMap") = -1
    '        'End If
    '        Return ViewState("ColumnsToMap")
    '    End Get
    '    Set(ByVal value As List(Of NameValuePair))
    '        ViewState("ColumnsToMap") = value
    '    End Set
    'End Property
    Public Property LastColumnIndex() As Integer
        Get
            If ViewState("LastColumnIndex") = Nothing Then
                ViewState("LastColumnIndex") = 0
            End If
            Return ViewState("LastColumnIndex")
        End Get
        Set(ByVal value As Integer)
            ViewState("LastColumnIndex") = value
        End Set
    End Property


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


    'Public ReadOnly Property FileHasGrade4() As Boolean
    '    Get
    '        Return Me.theFile.FileHasGrade4
    '    End Get
    'End Property

    'Public ReadOnly Property FileHasGrade8() As Boolean
    '    Get
    '        Return Me.theFile.FileHasGrade8
    '    End Get
    'End Property

    'Public ReadOnly Property FileHasGrade12() As Boolean
    '    Get
    '        Return Me.theFile.FileHasGrade12
    '    End Get
    'End Property

    'Public Property FileHasGrade4() As Boolean
    '    Get
    '        If ViewState("FileHasGrade4") = Nothing Then
    '            ViewState("FileHasGrade4") = False
    '        End If
    '        Return ViewState("FileHasGrade4")
    '    End Get
    '    Set(ByVal value As Boolean)
    '        ViewState("FileHasGrade4") = value
    '    End Set
    'End Property

    'Public Property FileHasGrade8() As Boolean
    '    Get
    '        If ViewState("FileHasGrade8") = Nothing Then
    '            ViewState("FileHasGrade8") = False
    '        End If
    '        Return ViewState("FileHasGrade8")
    '    End Get
    '    Set(ByVal value As Boolean)
    '        ViewState("FileHasGrade8") = value
    '    End Set
    'End Property

    'Public Property FileHasGrade12() As Boolean
    '    Get
    '        If ViewState("FileHasGrade12") = Nothing Then
    '            ViewState("FileHasGrade12") = False
    '        End If
    '        Return ViewState("FileHasGrade12")
    '    End Get
    '    Set(ByVal value As Boolean)
    '        ViewState("FileHasGrade12") = value
    '    End Set
    'End Property


    'Public Property TotalRows() As Integer
    '    Get
    '        If ViewState("TotalRows") = Nothing Then
    '            ViewState("TotalRows") = 0
    '        End If
    '        Return ViewState("TotalRows")
    '    End Get
    '    Set(ByVal value As Integer)
    '        ViewState("TotalRows") = value
    '    End Set
    'End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        'If Date.Now < "1/31/2018" And Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") = False And Request.Url.ToString().ToLower().Contains("localhost") = False Then
        '    PanelGrade4.Visible = False
        '    PanelGrade8.Visible = False
        '    lblNotAvailable.Visible = True
        '    Exit Sub
        'End If

        If Not IsPostBack Then
            BindData()
            If Not String.IsNullOrEmpty(Request.QueryString("f")) Then
                BeginFileEdit(Request.QueryString("f"))
            End If

        End If
    End Sub

    Private Sub BindData()
        If Not TimssBll.HasFrame_N_() Then
            Return
        End If

        PanelGrade4.Visible = False
        PanelGrade8.Visible = False


        If TimssBll.HasGradeId4() Then
            If TimssBll.isICILS Then
                PanelGrade4.Visible = True
                RepeaterGrade4Uploads.DataSource = TimssBll.GetEfileUploadsByGradeId(TimssBll.GradeId4, Me.EfileTypeId)
                RepeaterGrade4Uploads.DataBind()
            Else
                Dim dr As DataRow = TimssBll.GetClassListSubmitedDataRow(TimssBll.GradeId4)
                If dr("ClassListSubmited") Is DBNull.Value Then
                    PanelGrade4.Visible = False
                    PanelGrade4ClassListNeeded.Visible = True
                Else
                    PanelGrade4.Visible = True
                    RepeaterGrade4Uploads.DataSource = TimssBll.GetEfileUploadsByGradeId(TimssBll.GradeId4, Me.EfileTypeId)
                    RepeaterGrade4Uploads.DataBind()
                End If
            End If
        End If

        If TimssBll.HasGradeId8 Then

            If TimssBll.isICILS Then
                PanelTIMSSUploadInstructions.Visible = TimssBll.iseTIMSS()
                PanelICILSUploadInstructions.Visible = TimssBll.isICILS()
                PanelGrade8.Visible = True
                RepeaterGrade8Uploads.DataSource = TimssBll.GetEfileUploadsByGradeId(TimssBll.GradeId8, Me.EfileTypeId)
                RepeaterGrade8Uploads.DataBind()
            Else
                PanelTIMSSUploadInstructions.Visible = TimssBll.iseTIMSS()
                PanelICILSUploadInstructions.Visible = TimssBll.isICILS()

                Dim dr As DataRow = TimssBll.GetClassListSubmitedDataRow(TimssBll.GradeId8)
                If dr("ClassListSubmited") Is DBNull.Value Then
                    PanelGrade8.Visible = False
                    PanelGrade8ClassListNeeded.Visible = True
                Else
                    PanelGrade8.Visible = True
                    RepeaterGrade8Uploads.DataSource = TimssBll.GetEfileUploadsByGradeId(TimssBll.GradeId8, Me.EfileTypeId)
                    RepeaterGrade8Uploads.DataBind()
                End If
            End If

        End If

        If TimssBll.HasGradeId12 Then
            PanelGrade12.Visible = True
            RepeaterGrade12Uploads.DataSource = TimssBll.GetEfileUploadsByGradeId(TimssBll.GradeId12, Me.EfileTypeId)
            RepeaterGrade12Uploads.DataBind()
        End If


    End Sub

    Protected Sub ButtonUploadGrade4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadGrade4.Click

        If Not TimssBll.HasGradeId4 Then Return

        LabelFileUploadGrade4.Visible = False

        If FileUploadGrade4.HasFile Then
            Dim tmpFileid As Integer
            If TimssBll.ProcessfileUploadForGrade(FileUploadGrade4, TimssBll.GradeId4, TimssBll.Frame_N_, 4, TimssBll.ProjectID, DropDownListColumnHeadersGrade4.SelectedValue, tmpFileid, Me.EfileTypeId) Then
                BindData()
                'Me.FileHasGrade4 = True
                'Me.FileHasGrade8 = False
                'Me.FileHasGrade12 = False
                BeginFileEdit(tmpFileid)
            End If
        Else
            LabelFileUploadGrade4.Visible = True
            LabelFileUploadGrade4.Text = "You have not specified a file."

        End If
    End Sub

    Protected Sub ButtonUploadGrade8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadGrade8.Click

        If Not TimssBll.HasGradeId8 Then Return

        LabelFileUploadGrade8.Visible = False
        If FileUploadGrade8.HasFile Then
            Dim tmpFileid As Integer
            If TimssBll.ProcessfileUploadForGrade(FileUploadGrade8, TimssBll.GradeId8, TimssBll.Frame_N_, 8, TimssBll.ProjectID, DropDownListColumnHeadersGrade8.SelectedValue, tmpFileid, Me.EfileTypeId) Then
                BindData()
                'Me.FileHasGrade4 = False
                'Me.FileHasGrade8 = True
                'Me.FileHasGrade12 = False
                BeginFileEdit(tmpFileid)
            End If
        Else
            LabelFileUploadGrade8.Visible = True
            LabelFileUploadGrade8.Text = "You have not specified a file."
        End If
    End Sub

    Protected Sub ButtonUploadGrade12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadGrade12.Click

        If Not TimssBll.HasGradeId12 Then Return

        'LabelFileUploadGrade12.Visible = False
        If FileUploadGrade12.HasFile Then
            Dim tmpFileid As Integer
            If TimssBll.ProcessfileUploadForGrade(FileUploadGrade12, TimssBll.GradeId12, TimssBll.Frame_N_, 12, TimssBll.ProjectID, DropDownListColumnHeadersGrade12.SelectedValue, tmpFileid, Me.EfileTypeId) Then
                BindData()
                'Me.FileHasGrade4 = False
                'Me.FileHasGrade8 = False
                'Me.FileHasGrade12 = True
                'BeginFileEdit(tmpFileid)
            End If
        Else
            LabelFileUploadGrade12.Visible = True
            LabelFileUploadGrade12.Text = "You have not specified a file."
        End If
    End Sub


    Sub RepeaterClassList_ItemCommand(Sender As Object, e As RepeaterCommandEventArgs) Handles RepeaterGrade4Uploads.ItemCommand, RepeaterGrade8Uploads.ItemCommand, RepeaterGrade12Uploads.ItemCommand

        If e.CommandName.Equals("editfile4", StringComparison.CurrentCultureIgnoreCase) Then
            'Me.FileHasGrade4 = True
            'Me.FileHasGrade8 = False
            'Me.FileHasGrade12 = False
            BeginFileEdit(e.CommandArgument)
        ElseIf e.CommandName.Equals("editfile8", StringComparison.CurrentCultureIgnoreCase) Then
            'Me.FileHasGrade4 = False
            'Me.FileHasGrade8 = True
            'Me.FileHasGrade12 = False
            BeginFileEdit(e.CommandArgument)
        ElseIf e.CommandName.Equals("editfile12", StringComparison.CurrentCultureIgnoreCase) Then
            'Me.FileHasGrade4 = False
            'Me.FileHasGrade8 = False
            'Me.FileHasGrade12 = True
            BeginFileEdit(e.CommandArgument)
        End If

    End Sub

    Private Sub BeginFileEdit(fileid As Integer)
        'Me.FileId = fileid
        Dim dr As DataRow = TimssBll.GetEfileDetailsDataRow(fileid)

        Me.theFile = New File(dr)

        If Me.theFile.SmpGrd = 12 Then
            Me.theFile = Nothing
            Return
        End If

        'Me.TotalRows = dr("TotalRows")
        Me.LastColumnIndex = 0
        Me.ColumnsToMap = Nothing
        Me.RepeaterIdentifyColumns.DataSource = TimssBll.GetEfileUserColumns(Me.theFile.FileId)
        Me.RepeaterIdentifyColumns.DataBind()
        PanelEditFile.Visible = True
        PanelColumns.Visible = True
        PanelValues.Visible = False
        PanelVerify.Visible = False
        PanelGrade4.Visible = False
        PanelGrade8.Visible = False
        PanelGrade12.Visible = False
    End Sub
    Protected Sub ButtonColumnsNext_Click(sender As Object, e As System.EventArgs) Handles ButtonColumnsNext.Click
        ProcessColumnRepeater(Me.RepeaterIdentifyColumns, "db_")

        Me.LastColumnIndex = 0
        Me.ColumnsToMap = TimssBll.GetEfileColumnsWithValuesToMap(Me.theFile.FileId)

        If Me.ColumnsToMap.Rows.Count > 0 Then
            UpdateValueMappingProgress()

            PanelColumns.Visible = False
            PanelValues.Visible = True

            Me.RepeaterIdentifyColumns.DataSource = Nothing
            Me.RepeaterIdentifyColumns.DataBind()

            Me.RepeaterMapValues.DataSource = TimssBll.GetEfileResponseFreqs(Me.ColumnsToMap.Rows(Me.LastColumnIndex)("UserColumnId"))
            Me.RepeaterMapValues.DataBind()
        Else
            PanelColumns.Visible = False
            PanelValues.Visible = False
            PanelVerify.Visible = True
            Me.LastColumnIndex -= 1
            Me.LastVerifyColumnHeader = Nothing

            Me.RepeaterMapValues.DataSource = Nothing
            Me.RepeaterMapValues.DataBind()

            Me.RepeaterVerify.DataSource = TimssBll.GetFinalResponseFreqs(Me.theFile.FileId)
            Me.RepeaterVerify.DataBind()
        End If

    End Sub

    Protected Sub ButtonColumnsPrevious_Click(sender As Object, e As System.EventArgs) Handles ButtonColumnsPrevious.Click
        ProcessColumnRepeater(Me.RepeaterIdentifyColumns, "db_")

        Me.RepeaterIdentifyColumns.DataSource = Nothing
        Me.RepeaterIdentifyColumns.DataBind()

        PanelColumns.Visible = False
        PanelValues.Visible = False
        Me.BindData()

    End Sub

    Private Sub ProcessColumnRepeater(rpt As Repeater, prefix As String)
        Dim blnEdit As Boolean = False
        Dim msg As String = ""

        For Each item As RepeaterItem In rpt.Items
            If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then
                Dim NaepLabelId As HiddenField = item.FindControl("NaepLabelId")
                Dim UserColumnId As HiddenField = item.FindControl("UserColumnId")
                Dim UserColumnLabel As HiddenField = item.FindControl("UserColumnLabel")

                Dim db_NaepLabelId As DropDownList = item.FindControl("db_NaepLabelId")
                If Not NaepLabelId.Value.Equals(db_NaepLabelId.SelectedValue) Then
                    TimssBll.SaveEfileColumnMappingEditChanges(item.Controls, UserColumnId.Value)

                    If String.IsNullOrEmpty(msg) Then
                        msg = "Identify Columns:<ul>"
                    End If
                    msg = msg & "<li>Column Header: " & UserColumnLabel.Value & " linked to Column Contains: " & db_NaepLabelId.SelectedItem.Text & "</li>"
                    blnEdit = True

                End If
            End If
        Next

        If blnEdit Then
            msg = msg & "</ul>"
            TimssBll.SetStatusCode(Me.theFile.FileId, msg, EFileStatusType.EFile, False)
        End If
    End Sub


    Protected Sub ButtonValuesNext_Click(sender As Object, e As System.EventArgs) Handles ButtonValuesNext.Click
        ProcessValueRepeater(Me.RepeaterMapValues)
        Me.LastColumnIndex += 1


        If Me.LastColumnIndex > (Me.ColumnsToMap.Rows.Count - 1) Then
            PanelColumns.Visible = False
            PanelValues.Visible = False
            PanelVerify.Visible = True
            Me.LastColumnIndex -= 1
            Me.LastVerifyColumnHeader = Nothing

            Me.RepeaterMapValues.DataSource = Nothing
            Me.RepeaterMapValues.DataBind()

            Me.RepeaterVerify.DataSource = TimssBll.GetFinalResponseFreqs(Me.theFile.FileId)
            Me.RepeaterVerify.DataBind()

            PanelVerifyInstructionseTIMSS.Visible = TimssBll.iseTIMSS()
            PanelVerifyInstructionsICILS.Visible = TimssBll.isICILS()

        Else

            UpdateValueMappingProgress()

            PanelColumns.Visible = False
            PanelValues.Visible = True
            Me.RepeaterMapValues.DataSource = TimssBll.GetEfileResponseFreqs(Me.ColumnsToMap.Rows(Me.LastColumnIndex)("UserColumnId"))
            Me.RepeaterMapValues.DataBind()
        End If

    End Sub

    Protected Sub ButtonValuesPrevious_Click(sender As Object, e As System.EventArgs) Handles ButtonValuesPrevious.Click

        ProcessValueRepeater(Me.RepeaterMapValues)
        Me.LastColumnIndex -= 1


        If Me.LastColumnIndex < 0 Then
            Me.RepeaterMapValues.DataSource = Nothing
            Me.RepeaterMapValues.DataBind()

            Me.RepeaterIdentifyColumns.DataSource = TimssBll.GetEfileUserColumns(Me.theFile.FileId)
            Me.RepeaterIdentifyColumns.DataBind()

            PanelColumns.Visible = True
            PanelValues.Visible = False
        Else
            UpdateValueMappingProgress()
            Me.RepeaterMapValues.DataSource = TimssBll.GetEfileResponseFreqs(Me.ColumnsToMap.Rows(Me.LastColumnIndex)("UserColumnId"))
            Me.RepeaterMapValues.DataBind()
        End If
    End Sub

    Public Function IsClassColumn() As Boolean
        Return Me.ColumnsToMap.Rows(Me.LastColumnIndex)("NaepLabel").ToString().Equals("class", StringComparison.CurrentCultureIgnoreCase)
    End Function

    Private Sub ProcessValueRepeater(rpt As Repeater)

        Dim blnEdit As Boolean = False
        Dim msg As String = ""
        Dim ColumnLabel As String = Me.ColumnsToMap.Rows(Me.LastColumnIndex)("NaepLabel")

        For Each item As RepeaterItem In rpt.Items
            If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then
                Dim ResponseFreqId As HiddenField = item.FindControl("ResponseFreqId")
                Dim NaepCodeId As HiddenField = item.FindControl("NaepCodeId")
                Dim db_NaepCodeId As DropDownList = item.FindControl("db_NaepCodeId")
                Dim currentresponsefreq As HiddenField = item.FindControl("currentresponsefreq")
                '     Dim ClassListingFormId As HiddenField = item.FindControl("ClassListingFormId")
                ' Dim dbcl_ClassListingFormId As DropDownList = item.FindControl("dbcl_ClassListingFormId")

                If IsClassColumn() Then
                    '  If Not ClassListingFormId.Value.Equals(dbcl_ClassListingFormId.SelectedValue) Then
                    'TimssBll.SaveEfileValueMappingEditChanges(item.Controls, ResponseFreqId.Value, "dbcl_")
                    '    If String.IsNullOrEmpty(msg) Then
                    '        msg = "Identify Values updated for: " & ColumnLabel & "<ul>"
                    '    End If
                    '    msg = msg & _
                    '    "<li>Value: (" & currentresponsefreq.Value & ") linked to Class: (" & dbcl_ClassListingFormId.SelectedItem.Text & ")</li>"
                    '    blnEdit = True
                    'End If
                Else

                    If Not NaepCodeId.Value.Equals(db_NaepCodeId.SelectedValue) Then
                        TimssBll.SaveEfileValueMappingEditChanges(item.Controls, ResponseFreqId.Value, "db_")
                        If String.IsNullOrEmpty(msg) Then
                            msg = "Identify Values updated for: " & ColumnLabel & "<ul>"
                        End If
                        msg = msg &
                        "<li>Value: (" & currentresponsefreq.Value & ") linked to TIMSS Code: (" & db_NaepCodeId.SelectedItem.Text & ")</li>"
                        blnEdit = True
                    End If

                End If
            End If
        Next

        If blnEdit Then
            msg = msg & "</ul>"
            TimssBll.SetStatusCode(Me.theFile.FileId, msg, EFileStatusType.EFile, False)
        End If
    End Sub

    Private Sub UpdateValueMappingProgress()
        PanelValuesInstructionseTIMSS.Visible = False
        PanelValuesInstructionsICILS.Visible = False

        If IIf(Me.LastColumnIndex = 0, True, False) Then
            PanelValuesInstructionseTIMSS.Visible = TimssBll.iseTIMSS()
            PanelValuesInstructionsICILS.Visible = TimssBll.isICILS()
        End If

        LabelValueMappingProgress.Text = "Column " & Me.LastColumnIndex + 1 & " of " & Me.ColumnsToMap.Rows.Count
        Dim label As String = Me.ColumnsToMap.Rows(Me.LastColumnIndex)("NaepLabel")
        LabelColumn.Text = label

        PanelDateFormat.Visible = label.Equals("Date of Birth", StringComparison.CurrentCultureIgnoreCase)
        If label.Equals("Date of Birth", StringComparison.CurrentCultureIgnoreCase) Then
            DropDownListDateFormat.DataSource = TimssBll.GetNaepCodes(Me.ColumnsToMap.Rows(Me.LastColumnIndex)("NaepLabelId"))
            DropDownListDateFormat.DataBind()
        Else
            DropDownListDateFormat.DataSource = Nothing
            DropDownListDateFormat.DataBind()
        End If
    End Sub

    Public Function HandleVerifyColumnHeaderVisibility(current As String) As Boolean
        If Me.LastVerifyColumnHeader.Equals(current) Then
            Return False
        Else
            Me.LastVerifyColumnHeader = current
            Return True
        End If
    End Function

    Protected Sub ButtonVerifySubmit_Click(sender As Object, e As System.EventArgs) Handles ButtonVerifySubmit.Click
        TimssBll.UserVerification(Me.theFile, Nothing, IIf(RadioButtonListVerifyChoice.SelectedValue = 1, True, False))

        PanelVerify.Visible = False
        PanelThanks.Visible = True

        If RadioButtonListVerifyChoice.SelectedValue = 1 Then
            If TimssBll.isICILS() Then
                PanelCorrectICILS.Visible = True
                PanelIncorrectICILS.Visible = False
                PanelCorrect.Visible = False
                PanelIncorrect.Visible = False
            Else
                PanelCorrectICILS.Visible = False
                PanelIncorrectICILS.Visible = False
                PanelCorrect.Visible = True
                PanelIncorrect.Visible = False
            End If

        Else
            If TimssBll.isICILS() Then
                PanelCorrectICILS.Visible = False
                PanelIncorrectICILS.Visible = True
                PanelCorrect.Visible = False
                PanelIncorrect.Visible = False
            Else
                PanelCorrectICILS.Visible = False
                PanelIncorrectICILS.Visible = False
                PanelCorrect.Visible = False
                PanelIncorrect.Visible = True
            End If

        End If
    End Sub

    Protected Sub ButtonVerifyPrevious_Click(sender As Object, e As System.EventArgs) Handles ButtonVerifyPrevious.Click
        If Me.LastColumnIndex > (Me.ColumnsToMap.Rows.Count - 1) Then
            UpdateValueMappingProgress()

            PanelValues.Visible = True
            PanelVerify.Visible = False

            Me.RepeaterVerify.DataSource = Nothing
            Me.RepeaterVerify.DataBind()

            Me.RepeaterMapValues.DataSource = TimssBll.GetEfileResponseFreqs(Me.ColumnsToMap.Rows(Me.LastColumnIndex)("UserColumnId"))
            Me.RepeaterMapValues.DataBind()
        Else

            PanelValues.Visible = False
            PanelVerify.Visible = False
            Me.RepeaterIdentifyColumns.DataSource = TimssBll.GetEfileUserColumns(Me.theFile.FileId)
            Me.RepeaterIdentifyColumns.DataBind()
            PanelEditFile.Visible = True
            PanelColumns.Visible = True
            PanelValues.Visible = False
            PanelVerify.Visible = False
            PanelGrade4.Visible = False
            PanelGrade8.Visible = False
            PanelGrade12.Visible = False

            Me.RepeaterVerify.DataSource = Nothing
            Me.RepeaterVerify.DataBind()

            Me.RepeaterMapValues.DataSource = Nothing
            Me.RepeaterMapValues.DataBind()

        End If

    End Sub

End Class
