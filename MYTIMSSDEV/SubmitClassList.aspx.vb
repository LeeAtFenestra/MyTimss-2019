Partial Class SubmitClassList
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'If Date.Now < "1/31/2018" And Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") = False And Request.Url.ToString().ToLower().Contains("localhost") = False And Request.Url.ToString().ToLower().Contains("mytimssdev.westat.com") = False Then

        '    'divNotAvailable.Visible = True

        '    'divNotAvailable.Visible = True
        '    'lblNotAvailable.Visible = True

        '    PanelGrade4.Visible = False
        '    PanelGrade8.Visible = False
        '    PanelGrade12.Visible = False
        '    PanelSave.Visible = False

        '    lblNotAvailable.Visible = True
        '    Exit Sub
        'End If

        If Not IsPostBack Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

            BindData()
        End If
    End Sub

    Private Sub BindData()
        If Not TimssBll.HasFrame_N_() Then
            Return
        End If
        PanelGrade4.Visible = False
        PanelGrade8.Visible = False
        PanelGrade12.Visible = False
        PanelSave.Visible = (TimssBll.HasGradeId4() Or TimssBll.HasGradeId8())
        'PanelSave2.Visible = (TimssBll.HasGradeId8())

        Dim g4alldone As Boolean = True
        Dim g8alldone As Boolean = True

        If TimssBll.HasGradeId4() Then
            labelHeaderGrade.Text = "Submit Class List - Grade 4"
            PanelGrade4.Visible = True
            Dim dr As DataRow = TimssBll.GetClassListSubmitedDataRow(TimssBll.GradeId4)

            If dr("ClassListSubmited") Is DBNull.Value Then
                RepeaterClassList4.DataSource = TimssBll.GetClassListingForm(TimssBll.GradeId4)
                RepeaterClassList4.DataBind()

                dbG4_ClassExclusionStatus.DataSource = TimssBll.GetClassExclusionStatusNameValuePairList()
                dbG4_ClassExclusionStatus.DataBind()

                dbG4_ClassGroup.DataSource = TimssBll.GetClassGroupNameValuePairList()
                dbG4_ClassGroup.DataBind()

                dbG4_id.Value = TimssBll.GradeId4

                RepeaterClassList4ReadOnly.DataSource = Nothing
                RepeaterClassList4ReadOnly.DataBind()

                LabelClassListSubmittedBy.Text = ""

                trGrade4Add.Visible = True
                g4alldone = False
            Else
                RepeaterClassList4.DataSource = Nothing
                RepeaterClassList4.DataBind()


                dbG4_ClassExclusionStatus.DataSource = Nothing
                dbG4_ClassExclusionStatus.DataBind()

                dbG4_ClassGroup.DataSource = Nothing
                dbG4_ClassGroup.DataBind()

                RepeaterClassList4ReadOnly.DataSource = TimssBll.GetClassListingForm(TimssBll.GradeId4)
                RepeaterClassList4ReadOnly.DataBind()

                LabelClassListSubmittedBy.Text = "Submitted by " & dr("ClassListSubmitedByFirstName") & " " & dr("ClassListSubmitedByLastName") & " on " & dr("ClassListSubmited")

                trGrade4Add.Visible = False
                g4alldone = True
            End If



        End If

        If TimssBll.HasGradeId8 Then
            labelHeaderGrade.Text = "Submit Class List - Grade 8"
            PanelGrade8.Visible = True
            Dim dr As DataRow = TimssBll.GetClassListSubmitedDataRow(TimssBll.GradeId8)

            If dr("ClassListSubmited") Is DBNull.Value Then

                RepeaterClassList8.DataSource = TimssBll.GetClassListingForm(TimssBll.GradeId8)
                RepeaterClassList8.DataBind()

                'RepeaterScienceClassList8.DataSource = TimssBll.GetTeacherNameListingForm(TimssBll.GradeId8)
                'RepeaterScienceClassList8.DataBind()

                dbG8_ClassExclusionStatus.DataSource = TimssBll.GetClassExclusionStatusNameValuePairList()
                dbG8_ClassExclusionStatus.DataBind()

                dbG8_ClassGroup.DataSource = TimssBll.GetClassGroupNameValuePairList()
                dbG8_ClassGroup.DataBind()

                dbG8_id.Value = TimssBll.GradeId8
                'dbG8Science_id.Value = TimssBll.GradeId8

                RepeaterClassList8ReadOnly.DataSource = Nothing
                RepeaterClassList8ReadOnly.DataBind()

                'RepeaterScienceClassList8ReadOnly.DataSource = Nothing
                'RepeaterScienceClassList8ReadOnly.DataBind()


                trGrade8Add.Visible = True
                'trGrade8ScienceAdd.Visible = True
                g8alldone = False
            Else

                RepeaterClassList8.DataSource = Nothing
                RepeaterClassList8.DataBind()

                'RepeaterScienceClassList8.DataSource = Nothing
                'RepeaterScienceClassList8.DataBind()

                dbG8_ClassExclusionStatus.DataSource = Nothing
                dbG8_ClassExclusionStatus.DataBind()

                dbG8_ClassGroup.DataSource = Nothing
                dbG8_ClassGroup.DataBind()


                RepeaterClassList8ReadOnly.DataSource = TimssBll.GetClassListingForm(TimssBll.GradeId8)
                RepeaterClassList8ReadOnly.DataBind()

                'RepeaterScienceClassList8ReadOnly.DataSource = TimssBll.GetTeacherNameListingForm(TimssBll.GradeId8)
                'RepeaterScienceClassList8ReadOnly.DataBind()

                LabelClassListSubmittedBy.Text = "Submitted by " & dr("ClassListSubmitedByFirstName") & " " & dr("ClassListSubmitedByLastName") & " on " & dr("ClassListSubmited")


                trGrade8Add.Visible = False
                'trGrade8ScienceAdd.Visible = False
                g8alldone = True
            End If

        End If

        If TimssBll.HasGradeId12 Then
            labelHeaderGrade.Text = "Submit Class List - Grade 12"
            PanelGrade12.Visible = True
        End If

        If g8alldone And g4alldone Then
            ButtonSave.Visible = False
            ButtonFinished.Visible = False
            'ButtonSave2.Visible = False
        Else
            ButtonSave.Visible = True
            ButtonFinished.Visible = True
            'ButtonSave2.Visible = True
        End If
    End Sub


    Sub RepeaterClassList_ItemCommand(Sender As Object, e As RepeaterCommandEventArgs) Handles RepeaterClassList4.ItemCommand, RepeaterClassList8.ItemCommand ', RepeaterScienceClassList8.ItemCommand
        If e.CommandName.Equals("deleteclass", StringComparison.CurrentCultureIgnoreCase) Then
            'Response.Write(e.CommandArgument)
            TimssBll.DeleteClassListingFormItem(e.CommandArgument)
            BindData()
            LabelDeleteComplete.Visible = True
        ElseIf e.CommandName.Equals("deletescienceteacher", StringComparison.CurrentCultureIgnoreCase) Then
            TimssBll.DeleteScienceTeacherListingFormItem(e.CommandArgument)
            BindData()
        End If



    End Sub


    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click

        If TimssBll.HasGradeId4 Then
            ProcessRepeater(Me.RepeaterClassList4, "dbG4_")
        End If

        If TimssBll.HasGradeId8 Then
            ProcessRepeater(Me.RepeaterClassList8, "dbG8_")
            'ProcessScienceTeacherRepeater(Me.RepeaterScienceClassList8, "dbG8Science_")
        End If

        BindData()

        If ViewState("flgClassNameError") = False And ViewState("flgClassSizeError") = False And ViewState("flgRequiredFieldsError") = False Then
            LabelSaveComplete.Visible = True
        End If

    End Sub

    Protected Sub ButtonFinished_Click(sender As Object, e As System.EventArgs) Handles ButtonFinished.Click
        LabelFinished.Visible = True

        If TimssBll.HasGradeId4 Then
            ProcessRepeater(Me.RepeaterClassList4, "dbG4_")
            TimssBll.SaveClassListingFinished(TimssBll.GradeId4)
        End If

        If TimssBll.HasGradeId8 Then
            ProcessRepeater(Me.RepeaterClassList8, "dbG8_")
            'ProcessScienceTeacherRepeater(Me.RepeaterScienceClassList8, "dbG8Science_")
            TimssBll.SaveClassListingFinished(TimssBll.GradeId8)
        End If

        BindData()
    End Sub



    Private Sub ProcessRepeater(rpt As Repeater, prefix As String)

        '---all required fields check---------------------------
        Dim CountOfClassesMissingRequiredInfo As Integer = 0

        For Each item As RepeaterItem In rpt.Items
            If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then

                Dim req_ClassName As TextBox = item.FindControl("db_ClassName")
                Dim req_ClassGroup As DropDownList = item.FindControl("db_ClassGroup")
                Dim req_NumberOfStudents As TextBox = item.FindControl("db_NumberOfStudents")
                Dim req_NameOfTeacher As TextBox = item.FindControl("db_NameOfMathematicsTeacher" & IIf(prefix.Equals("dbG4_"), "G4", "G8"))
                Dim req_TeacherEmail As TextBox = item.FindControl("db_NameOfMathematicsTeacherEmail" & IIf(prefix.Equals("dbG4_"), "G4", "G8"))

                If Trim(req_ClassName.Text) = "" Or Trim(req_NumberOfStudents.Text) = "" Or Trim(req_NameOfTeacher.Text) = "" Or Trim(req_TeacherEmail.Text) = "" Or req_ClassGroup.SelectedIndex = 0 Then
                    CountOfClassesMissingRequiredInfo = CountOfClassesMissingRequiredInfo + 1
                End If
            End If
        Next

        If prefix.Equals("dbG4_") And Not String.IsNullOrEmpty(Trim(dbG4_ClassName.Text)) Then
            If dbG4_ClassName.Text = "" Or dbG4_NumberofStudents.Text = "" Or dbG4_NameOfMathematicsTeacherG4.Text = "" Or dbG4_NameOfMathematicsTeacherEmailG4.Text = "" Or dbG4_ClassGroup.SelectedIndex = 0 Then
                CountOfClassesMissingRequiredInfo = CountOfClassesMissingRequiredInfo + 1
            End If
        ElseIf prefix.Equals("dbG8_") And Not String.IsNullOrEmpty(Trim(dbG8_ClassName.Text)) Then
            If dbG8_ClassName.Text = "" Or dbG8_NumberofStudents.Text = "" Or dbG8_NameofMathematicsTeacherG8.Text = "" Or dbG8_NameofMathematicsTeacherEmailG8.Text = "" Or dbG8_ClassGroup.SelectedIndex = 0 Then
                CountOfClassesMissingRequiredInfo = CountOfClassesMissingRequiredInfo + 1
            End If
        End If

        '------------end required fields check-------------------

        '---------- no class size over 75------------------------
        Dim CountOfClassesOver75InList As Integer = 0

        For Each item As RepeaterItem In rpt.Items
            If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then
                Dim classsizetextbox As TextBox = item.FindControl("db_NumberOfStudents")
                If Not String.IsNullOrEmpty(classsizetextbox.Text) Then
                    If Integer.Parse(classsizetextbox.Text) > 75 Then
                        CountOfClassesOver75InList = CountOfClassesOver75InList + 1
                    End If
                End If

            End If
        Next

        If (prefix.Equals("dbG4_") And Not String.IsNullOrEmpty(Me.dbG4_NumberofStudents.Text)) Then
            If Integer.Parse(Me.dbG4_NumberofStudents.Text) > 75 Then
                CountOfClassesOver75InList = CountOfClassesOver75InList + 1
            End If
        ElseIf (prefix.Equals("dbG8_") And Not String.IsNullOrEmpty(Me.dbG8_NumberofStudents.Text)) Then
            If Integer.Parse(Me.dbG8_NumberofStudents.Text) > 75 Then
                CountOfClassesOver75InList = CountOfClassesOver75InList + 1
            End If
        End If
        '-------end class size check----------------------------------------------

        '-----gets list of class names to determine if class name is distinct--------    
        Dim classlist As List(Of String) = New List(Of String)
        Dim isClassNameDistinct As Boolean = Nothing

        For Each item As RepeaterItem In rpt.Items
            If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then
                Dim classnametextbox As TextBox = item.FindControl("db_ClassName")
                classlist.Add(Trim(classnametextbox.Text.ToLower()))
            End If
        Next

        If (prefix.Equals("dbG4_") And Not String.IsNullOrEmpty(Me.dbG4_ClassName.Text)) _
            Or
            (prefix.Equals("dbG8_") And Not String.IsNullOrEmpty(Me.dbG8_ClassName.Text)) Then

            If prefix.Equals("dbG4_") Then
                classlist.Add(Trim(Me.dbG4_ClassName.Text.ToLower()))
            ElseIf prefix.Equals("dbG8_") Then
                classlist.Add(Trim(Me.dbG8_ClassName.Text.ToLower()))
            End If
        End If

        If classlist.Distinct().Count() = classlist.Count() Then
            isClassNameDistinct = True
        Else
            isClassNameDistinct = False
        End If
        '--------------end class name isClassNameDistinct check-----------------------------

        '-----save---------------------------------
        For Each item As RepeaterItem In rpt.Items
            If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then
                If (isClassNameDistinct = True And CountOfClassesOver75InList = 0 And CountOfClassesMissingRequiredInfo = 0) Then
                    ViewState("flgClassNameError") = False
                    ViewState("flgClassSizeError") = False
                    ViewState("flgRequiredFieldsError") = False
                    Dim ClassListingFormId As HiddenField = item.FindControl("ClassListingFormId")
                    TimssBll.SaveClassListingFormItemEditChanges(item.Controls, ClassListingFormId.Value)

                ElseIf (isClassNameDistinct = False Or CountOfClassesOver75InList > 0 Or CountOfClassesMissingRequiredInfo > 0) Then
                    lblErrorHeader.Visible = True
                    lblErrorHeader.Text = "There were error(s) on your submission so <b>no data has been saved</b>.<br>Please correct the error(s) below and re-enter/retry again:"

                    If isClassNameDistinct = False Then
                        ViewState("flgClassNameError") = True
                        LabelError.Visible = True
                        lblClassNameError.Visible = True
                    Else
                        lblClassNameError.Visible = False
                    End If

                    If CountOfClassesOver75InList > 0 Then
                        ViewState("flgClassSizeError") = True
                        LabelError.Visible = True
                        lblClassSizeError.Visible = True
                    Else
                        lblClassSizeError.Visible = False
                    End If

                    If CountOfClassesMissingRequiredInfo > 0 Then
                        ViewState("flgRequiredFieldsError") = True
                        LabelError.Visible = True
                        lblRequiredFields.Visible = True
                    Else
                        lblRequiredFields.Visible = False
                    End If
                End If
            End If
        Next

        If (prefix.Equals("dbG4_") And Not String.IsNullOrEmpty(Me.dbG4_ClassName.Text)) _
            Or
            (prefix.Equals("dbG8_") And Not String.IsNullOrEmpty(Me.dbG8_ClassName.Text)) Then

            If (isClassNameDistinct = True And CountOfClassesOver75InList = 0 And CountOfClassesMissingRequiredInfo = 0) Then
                ViewState("flgClassNameError") = False
                ViewState("flgClassSizeError") = False
                ViewState("flgRequiredFieldsError") = False
                TimssBll.SaveClassListingFormItemInsert(Me.Controls, prefix)

            ElseIf (isClassNameDistinct = False Or CountOfClassesOver75InList > 0 Or CountOfClassesMissingRequiredInfo > 0) Then
                lblErrorHeader.Visible = True
                lblErrorHeader.Text = "There were error(s) on your submission so <b>no data has been saved</b>.<br>Please correct the error(s) below and re-enter/retry again:"

                If isClassNameDistinct = False Then
                    ViewState("flgClassNameError") = True
                    LabelError.Visible = True
                    lblClassNameError.Visible = True
                Else
                    lblClassNameError.Visible = False
                End If

                If CountOfClassesOver75InList > 0 Then
                    ViewState("flgClassSizeError") = True
                    LabelError.Visible = True
                    lblClassSizeError.Visible = True
                Else
                    lblClassSizeError.Visible = False
                End If

                If CountOfClassesMissingRequiredInfo > 0 Then
                    ViewState("flgRequiredFieldsError") = True
                    LabelError.Visible = True
                    lblRequiredFields.Visible = True
                Else
                    lblRequiredFields.Visible = False
                End If
            End If
        End If
        '------end save----------------------------------------

    End Sub

    'Private Sub ProcessScienceTeacherRepeater(rpt As Repeater, prefix As String)
    '    For Each item As RepeaterItem In rpt.Items
    '        If item.ItemType = ListItemType.AlternatingItem Or item.ItemType = ListItemType.Item Then
    '            Dim ScienceTeacherListingFormId As HiddenField = item.FindControl("ScienceTeacherListingFormId")
    '            TimssBll.SaveScienceTeacherListingFormItemEditChanges(item.Controls, ScienceTeacherListingFormId.Value)
    '        End If
    '    Next
    '    If (prefix.Equals("dbG8Science_") And Not String.IsNullOrEmpty(Me.dbG8Science_ClassName.Text)) Then
    '        TimssBll.SaveScienceTeacherListingFormItemInsert(Me.Controls, prefix)

    '    End If
    'End Sub
End Class
