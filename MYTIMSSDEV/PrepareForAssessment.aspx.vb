Imports Westat.TIMSS.HL

Partial Class PrepareForAssessment
    Inherits BasePagePublic

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Date.Now < "1/31/2018" And Request.Url.ToString().ToLower().Contains("mytimsstst.wesdemo.com") = False And Request.Url.ToString().ToLower().Contains("localhost") = False Then
            '  panelgrade12.Visible = False
            panelgrade4eTIMSS.Visible = False
            panelgrade8ICILS.Visible = False
            panelgrade8eTIMSS.Visible = False
            lblNotAvailable.Visible = True
            Exit Sub
        Else
            panelgrade4eTIMSS.Visible = Me.TimssBll.HasGradeId4()
            panelgrade8ICILS.Visible = Me.TimssBll.HasGradeId8() And Me.TimssBll.isICILS()
            panelgrade8eTIMSS.Visible = Me.TimssBll.HasGradeId8() And Me.TimssBll.iseTIMSS()
            'panelgrade12.Visible = Me.TimssBll.HasGradeId12()
            lblNotAvailable.Visible = False
        End If
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        If Not TimssBll.HasFrame_N_() Then
            Return
        End If

        '-------------------------------------------

        If panelgrade4eTIMSS.Visible Then
            ' Me.PanelUpdateSTLFFileGrade4.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            Dim dr As DataRow = TimssBll.DocumentUploaded("STLF", TimssBll.GradeId4)
            Dim stlf1doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade4.Text = TimssBll.GradeId4
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade4.Visible = True
                LinkButtonViewFileSTLF1Grade4eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade4.NavigateUrl = stlfdoc
                ImageSTLF1Grade4eTIMSS.ImageUrl = TimssBll.GetIconForFile(stlf1doc)
            Else
                'HyperLinkDownloadSTLFGrade4.Visible = False
                LinkButtonViewFileSTLF1Grade4eTIMSS.Visible = False
            End If
            ImageSTLF1Grade4eTIMSS.Visible = LinkButtonViewFileSTLF1Grade4eTIMSS.Visible

            '-------------------------------------------

            ' Me.PanelUpdateSTLFFileGrade4.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STLF2", TimssBll.GradeId4)
            Dim stlf2doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade4.Text = TimssBll.GradeId4
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade4.Visible = True
                LinkButtonViewFileSTLF2Grade4eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade4.NavigateUrl = stlfdoc
                ImageSTLF2Grade4eTIMSS.ImageUrl = TimssBll.GetIconForFile(stlf2doc)
            Else
                'HyperLinkDownloadSTLFGrade4.Visible = False
                LinkButtonViewFileSTLF2Grade4eTIMSS.Visible = False
            End If
            ImageSTLF2Grade4eTIMSS.Visible = LinkButtonViewFileSTLF1Grade4eTIMSS.Visible

            '-------------------------------------------

            ' Me.PanelUpdateSTLFFileGrade4.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STF", TimssBll.GradeId4)
            Dim stf1doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade4.Text = TimssBll.GradeId4
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade4.Visible = True
                LinkButtonViewFileSTF1Grade4eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade4.NavigateUrl = stlfdoc
                ImageSTF1Grade4eTIMSS.ImageUrl = TimssBll.GetIconForFile(stf1doc)
            Else
                'HyperLinkDownloadSTLFGrade4.Visible = False
                LinkButtonViewFileSTF1Grade4eTIMSS.Visible = False
            End If

            ImageSTF1Grade4eTIMSS.Visible = LinkButtonViewFileSTF1Grade4eTIMSS.Visible

            '-------------------------------------------

            ' Me.PanelUpdateSTLFFileGrade4.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STF2", TimssBll.GradeId4)
            Dim stf2doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade4.Text = TimssBll.GradeId4
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade4.Visible = True
                LinkButtonViewFileSTF2Grade4eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade4.NavigateUrl = stlfdoc
                ImageSTF2Grade4eTIMSS.ImageUrl = TimssBll.GetIconForFile(stf2doc)
            Else
                'HyperLinkDownloadSTLFGrade4.Visible = False
                LinkButtonViewFileSTF2Grade4eTIMSS.Visible = False
            End If
            ImageSTF2Grade4eTIMSS.Visible = LinkButtonViewFileSTF1Grade4eTIMSS.Visible
        End If



        If panelgrade8eTIMSS.Visible Then
            ' Me.PanelUpdateSTLFFileGrade8.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            Dim dr As DataRow = TimssBll.DocumentUploaded("STLF", TimssBll.GradeId8)
            Dim stlf1doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade8.Text = TimssBll.GradeId8
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade8.Visible = True
                LinkButtonViewFileSTLF1Grade8eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade8.NavigateUrl = stlfdoc
                ImageSTLF1Grade8eTIMSS.ImageUrl = TimssBll.GetIconForFile(stlf1doc)
            Else
                'HyperLinkDownloadSTLFGrade8.Visible = False
                LinkButtonViewFileSTLF1Grade8eTIMSS.Visible = False
            End If
            ImageSTLF1Grade8eTIMSS.Visible = LinkButtonViewFileSTLF1Grade8eTIMSS.Visible

            '-------------------------------------------

            ' Me.PanelUpdateSTLFFileGrade8.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STLF2", TimssBll.GradeId8)
            Dim stlf2doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade8.Text = TimssBll.GradeId8
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade8.Visible = True
                LinkButtonViewFileSTLF2Grade8eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade8.NavigateUrl = stlfdoc
                ImageSTLF2Grade8eTIMSS.ImageUrl = TimssBll.GetIconForFile(stlf2doc)
            Else
                'HyperLinkDownloadSTLFGrade8.Visible = False
                LinkButtonViewFileSTLF2Grade8eTIMSS.Visible = False
            End If
            ImageSTLF2Grade8eTIMSS.Visible = LinkButtonViewFileSTLF1Grade8eTIMSS.Visible

            '-------------------------------------------

            ' Me.PanelUpdateSTLFFileGrade8.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STF", TimssBll.GradeId8)
            Dim stf1doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade8.Text = TimssBll.GradeId8
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade8.Visible = True
                LinkButtonViewFileSTF1Grade8eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade8.NavigateUrl = stlfdoc
                ImageSTF1Grade8eTIMSS.ImageUrl = TimssBll.GetIconForFile(stf1doc)
            Else
                'HyperLinkDownloadSTLFGrade8.Visible = False
                LinkButtonViewFileSTF1Grade8eTIMSS.Visible = False
            End If

            ImageSTF1Grade8eTIMSS.Visible = LinkButtonViewFileSTF1Grade8eTIMSS.Visible

            '-------------------------------------------

            ' Me.PanelUpdateSTLFFileGrade8.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STF2", TimssBll.GradeId8)
            Dim stf2doc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDGrade8.Text = TimssBll.GradeId8
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                'HyperLinkDownloadSTLFGrade8.Visible = True
                LinkButtonViewFileSTF2Grade8eTIMSS.Visible = True
                'HyperLinkDownloadSTLFGrade8.NavigateUrl = stlfdoc
                ImageSTF2Grade8eTIMSS.ImageUrl = TimssBll.GetIconForFile(stf2doc)
            Else
                'HyperLinkDownloadSTLFGrade8.Visible = False
                LinkButtonViewFileSTF2Grade8eTIMSS.Visible = False
            End If
            ImageSTF2Grade8eTIMSS.Visible = LinkButtonViewFileSTF1Grade8eTIMSS.Visible
        End If


        If panelgrade8ICILS.Visible Then
            ' Me.PanelUpdateTTFFileGrade8.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            Dim dr As DataRow = TimssBll.DocumentUploaded("TTF", TimssBll.GradeId8)
            Dim TTFdoc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            '  Me.TIMSSIDTTFGrade8.Text = TimssBll.GradeId8
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                LinkButtonViewFileTTFGrade8ICILS.Visible = True
                ImageTTFGrade8ICILS.ImageUrl = TimssBll.GetIconForFile(TTFdoc)
            Else
                LinkButtonViewFileTTFGrade8ICILS.Visible = False
            End If
            ImageTTFGrade8ICILS.Visible = LinkButtonViewFileTTFGrade8ICILS.Visible

            '--------------------------------------

            ' Me.PanelUpdateSTLFFileGrade8.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
            dr = TimssBll.DocumentUploaded("STF", TimssBll.GradeId8)
            Dim stfdoc As String = IIf(dr("Filename") Is DBNull.Value, "", dr("Filename"))
            ' Me.TIMSSIDSTLFGrade8.Text = TimssBll.GradeId8
            If dr("Uploaded") And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
                LinkButtonViewFileSTFGrade8ICILS.Visible = True
                ImageSTFGrade8ICILS.ImageUrl = TimssBll.GetIconForFile(stfdoc)
            Else
                LinkButtonViewFileSTFGrade8ICILS.Visible = False
            End If
            ImageSTFGrade8ICILS.Visible = LinkButtonViewFileSTFGrade8ICILS.Visible
        End If

        'If panelgrade12.Visible Then
        'Me.PanelUpdateSTLFFileGrade12.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanUploadSTLF()
        'Dim stlfdoc As String = TimssBll.GetSTLFFilePath(TimssBll.GradeId12)
        'Me.TIMSSIDGrade12.Text = TimssBll.GradeId12
        'If Not String.IsNullOrEmpty(stlfdoc) And Not Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator() And Not Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator() Then
        '    HyperLinkDownloadSTLFGrade12.Visible = True
        '    HyperLinkDownloadSTLFGrade12.NavigateUrl = stlfdoc
        '    ImageSTLFGrade12.ImageUrl = TimssBll.GetIconForFile(stlfdoc)
        'Else
        '    HyperLinkDownloadSTLFGrade12.Visible = False
        'End If
        'ImageSTLFGrade12.Visible = HyperLinkDownloadSTLFGrade12.Visible
        ' End If

    End Sub

    'Protected Sub ButtonUploadGrade4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadGrade4.Click

    '    If Not TimssBll.HasGradeId4 Then Return

    '    LabelFileUploadGrade4.Visible = False

    '    If FileUploadGrade4.HasFile Then
    '        If TimssBll.ProcessfileUploadForGrade("STLF", FileUploadGrade4, TimssBll.GradeId4) Then
    '            BindData()
    '        End If
    '    Else
    '        LabelFileUploadGrade4.Visible = True
    '        LabelFileUploadGrade4.Text = "You have not specified a file."

    '    End If
    'End Sub

    'Protected Sub ButtonUploadGrade8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadSTLFGrade8.Click

    '    If Not TimssBll.HasGradeId8 Then Return

    '    LabelFileUploadSTLFGrade8.Visible = False
    '    If FileUploadSTLFGrade8.HasFile Then
    '        If TimssBll.ProcessfileUploadForGrade("STLF", FileUploadSTLFGrade8, TimssBll.GradeId8) Then
    '            BindData()
    '        End If
    '    Else
    '        LabelFileUploadSTLFGrade8.Visible = True
    '        LabelFileUploadSTLFGrade8.Text = "You have not specified a file."
    '    End If
    'End Sub

    'Protected Sub ButtonUploadGrade12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadGrade12.Click

    '    If Not TimssBll.HasGradeId12 Then Return

    '    If FileUploadGrade12.HasFile Then
    '        If TimssBll.ProcessfileUploadForGrade("STLF", FileUploadGrade12, TimssBll.GradeId12) Then
    '            BindData()
    '        End If
    '    Else
    '        LabelFileUploadGrade12.Visible = True
    '        LabelFileUploadGrade12.Text = "You have not specified a file."
    '    End If
    'End Sub

    'Protected Sub ButtonRemoveGrade4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemoveGrade4.Click

    '    If Not TimssBll.HasGradeId4 Then Return
    '    TimssBll.RemoveGradeSTLFFile(TimssBll.GradeId4, "STLF", "Delete")
    '    BindData()

    'End Sub

    'Protected Sub ButtonRemoveGrade8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemoveSTLFGrade8.Click

    '    If Not TimssBll.HasGradeId8 Then Return
    '    TimssBll.RemoveGradeSTLFFile(TimssBll.GradeId8, "STLF", "Delete")
    '    BindData()

    'End Sub

    'Protected Sub ButtonRemoveGrade12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemoveGrade12.Click

    '    If Not TimssBll.HasGradeId12 Then Return
    '    TimssBll.RemoveGradeSTLFFile(TimssBll.GradeId12, "STLF", "Delete")
    '    BindData()

    'End Sub

    Protected Sub LinkButtonViewFileSTF1Grade4eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTF1Grade4eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STF", TimssBll.GradeId4, True)
    End Sub

    Protected Sub LinkButtonViewFileSTF2Grade4eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTF2Grade4eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STF2", TimssBll.GradeId4, True)
    End Sub

    Protected Sub LinkButtonViewFileSTLF1Grade4eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTLF1Grade4eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STLF", TimssBll.GradeId4, True)
    End Sub

    Protected Sub LinkButtonViewFileSTLF2Grade4eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTLF2Grade4eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STLF2", TimssBll.GradeId4, True)
    End Sub


    Protected Sub LinkButtonViewFileSTFGrade8ICILS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTFGrade8ICILS.Click
        Me.TimssBll.StreamDocumentToBrowser("STF", TimssBll.GradeId8, True)
    End Sub

    Protected Sub LinkButtonViewFileTTFGrade8ICILS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileTTFGrade8ICILS.Click
        Me.TimssBll.StreamDocumentToBrowser("TTF", TimssBll.GradeId8, True)
    End Sub



    'Protected Sub ButtonUploadTTFGrade8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadTTFGrade8.Click

    '    If Not TimssBll.HasGradeId8 Then Return

    '    LabelFileUploadTTFGrade8.Visible = False
    '    If FileUploadTTFGrade8.HasFile Then
    '        If TimssBll.ProcessfileUploadForGrade("TTF", FileUploadTTFGrade8, TimssBll.GradeId8) Then
    '            BindData()
    '        End If
    '    Else
    '        LabelFileUploadTTFGrade8.Visible = True
    '        LabelFileUploadTTFGrade8.Text = "You have not specified a file."
    '    End If
    'End Sub

    'Protected Sub ButtonRemoveTTFGrade8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemoveTTFGrade8.Click

    '    If Not TimssBll.HasGradeId8 Then Return
    '    TimssBll.RemoveGradeSTLFFile(TimssBll.GradeId8, "TTF", "Delete")
    '    BindData()

    'End Sub






    'Protected Sub ButtonUploadGrade8eTIMSS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadGrade8eTIMSS.Click

    '    If Not TimssBll.HasGradeId8 Then Return

    '    LabelFileUploadGrade8eTIMSS.Visible = False

    '    If FileUploadGrade8eTIMSS.HasFile Then
    '        If TimssBll.ProcessfileUploadForGrade("STLF", FileUploadGrade8eTIMSS, TimssBll.GradeId8) Then
    '            BindData()
    '        End If
    '    Else
    '        LabelFileUploadGrade8eTIMSS.Visible = True
    '        LabelFileUploadGrade8eTIMSS.Text = "You have not specified a file."

    '    End If
    'End Sub

    Protected Sub LinkButtonViewFileSTLF1Grade8eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTLF1Grade8eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STLF", TimssBll.GradeId8, True)
    End Sub
    Protected Sub LinkButtonViewFileSTLF2Grade8eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTLF2Grade8eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STLF2", TimssBll.GradeId8, True)
    End Sub

    Protected Sub LinkButtonViewFileSTF1Grade8eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTF1Grade8eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STF", TimssBll.GradeId8, True)
    End Sub
    Protected Sub LinkButtonViewFileSTF2Grade8eTIMSS_Click(sender As Object, e As System.EventArgs) Handles LinkButtonViewFileSTF2Grade8eTIMSS.Click
        Me.TimssBll.StreamDocumentToBrowser("STF2", TimssBll.GradeId8, True)
    End Sub

    'Protected Sub ButtonRemoveGrade8eTIMSS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRemoveGrade8eTIMSS.Click

    '    If Not TimssBll.HasGradeId8 Then Return
    '    TimssBll.RemoveGradeSTLFFile(TimssBll.GradeId8, "STLF", "Delete")
    '    BindData()

    'End Sub
End Class
