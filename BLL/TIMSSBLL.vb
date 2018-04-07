Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Web.UI
Imports Westat.TIMSS.HL
Imports System.Web.Security
Imports System.Net.Mail
Imports ClosedXML.Excel
Imports System.Text.RegularExpressions
Imports System.Collections.Specialized
Imports System.Configuration

Public Class TIMSSBLL

    Private mProf As ProfileCommon = ProfileCommon.GetUserProfile()
    Private mUser As MembershipUser = Membership.GetUser()
    Private mTIMSSDal As DAL.TIMSSDAL

    Public Sub New()
        mTIMSSDal = New DAL.TIMSSDAL()
    End Sub

    Public Function getProjectNameFromMyNAEPREGID(MyNAEPRegID As String) As String
        Return mTIMSSDal.getProjectNameFromMyNAEPRegID(MyNAEPRegID)
    End Function

    Public Function getSMPGRDFromMyNAEPREGID(MyNAEPRegID As String) As String
        Return mTIMSSDal.getSMPGRDFromMyNAEPREGID(MyNAEPRegID)
    End Function

    Public Function GetPageSizeNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetPageSizeNameValuePairArrayList()
    End Function
    Public Function GetDispCodesNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetDispCodesNameValuePairArrayList(False)
    End Function

    Public Function GetDispCodesNameValuePairArrayList(displayCodeInText As Boolean) As ArrayList
        Return mTIMSSDal.GetDispCodesNameValuePairArrayList(displayCodeInText)
    End Function

    Public Function GetParticipateIfNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetParticipateIfNameValuePairArrayList()
    End Function

    Public Function GetRespondentAttitudeNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetRespondentAttitudeNameValuePairArrayList()
    End Function
    Public Function GetEROCCodeNameValuePairArrayList(displayCodeInText As Boolean) As ArrayList
        Return mTIMSSDal.GetEROCCodeNameValuePairArrayList(displayCodeInText)
    End Function

    Public Function GetDateRangeNameValuePairArrayList(AddNoneOption As Boolean, StartDate As Date, EndDate As Date) As ArrayList
        Return mTIMSSDal.GetDateRangeNameValuePairArrayList(AddNoneOption, StartDate, EndDate)
    End Function


    Public Function GetSampleListAvailableNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetSampleListAvailableNameValuePairArrayList()
    End Function

    Public Function GetStudentSampledNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetStudentSampledNameValuePairArrayList()
    End Function

    Public Function GetParentLetterTypeNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetParentLetterTypeNameValuePairArrayList()
    End Function

    Public Function GetParentLetterLngNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetParentLetterLngNameValuePairArrayList()
    End Function

    Public Function GetPreassessmentNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetPreassessmentNameValuePairArrayList()
    End Function

    Public Function GetAssessmentNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetAssessmentNameValuePairArrayList()
    End Function


    Public Function GetMarkNCSNameValuePairArrayList() As ArrayList
        Return mTIMSSDal.GetMarkNCSNameValuePairArrayList()
    End Function

    Public Sub SaveDistrictEditChanges(controls As ControlCollection, leadid As String)
        Dim args As New SaveChangesArgsBase(controls, leadid)
        mTIMSSDal.SaveDistrictEditChanges(args)
    End Sub

    Public Sub SaveSchoolEditChanges(controls As ControlCollection, frame_n_ As String, gradeid As String, grade As Integer)
        Dim args As New SaveSchoolEditChangesArgs(controls, frame_n_, gradeid)
        mTIMSSDal.SaveSchoolEditChanges(args)

        If grade = 4 Then
            Dim g4args As New SaveChangesArgsBase(controls, gradeid, "dbG4_")
            mTIMSSDal.HandleTableUpdate(g4args.DbControls, g4args.PrimaryKey, "tblscsgrade", "ID")
        End If

        If grade = 8 Then
            Dim g8args As New SaveChangesArgsBase(controls, gradeid, "dbG8_")
            mTIMSSDal.HandleTableUpdate(g8args.DbControls, g8args.PrimaryKey, "tblscsgrade", "ID")
        End If

        If grade = 12 Then
            Dim g12args As New SaveChangesArgsBase(controls, gradeid, "dbG12_")
            mTIMSSDal.HandleTableUpdate(g12args.DbControls, g12args.PrimaryKey, "tblscsgrade", "ID")
        End If

    End Sub

    'Public Sub SaveSchoolErocChanges(controls As ControlCollection, gradeid As String, disp As String, erocid As Integer)
    '    Dim args As New SaveChangesArgsBase(controls, erocid)
    '    mTIMSSDal.SaveSchoolErocChanges(args, gradeid, disp, Me.mUser.ProviderUserKey)
    'End Sub

    Public Sub SaveSchoolErocChanges(controls As ControlCollection, gradeid As String, erocid As Integer)
        Dim args As New SaveChangesArgsBase(controls, erocid)
        mTIMSSDal.SaveSchoolErocChanges(args, gradeid, Me.mUser.ProviderUserKey)
    End Sub

    Public Function GetDistrictListDataTable(args As GetDistrictListSqlDataSourceArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetDistrictListDataTable(args)
    End Function

    Public Function GetSchoolListDataTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetSchoolListDataTable(args)
    End Function

    Public Function GetSchoolsEROCDataTable(gradeid As String) As DataTable
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        args.SortParameters.Add(New HL.SortParameter("DateContacted", SortDirection.Descending))
        args.FilterParameters.Add(New HL.FilterParameter("id", gradeid, "equals"))
        Return mTIMSSDal.GetSchoolsEROCDataTable(args)
    End Function

    Public Sub HandleGridViewSortImageDisplay(ByVal e As GridViewRowEventArgs, GridViewSortColumn As String, GridViewSortDirection As SortDirection)
        If Not (e.Row Is Nothing) AndAlso e.Row.RowType = DataControlRowType.Header Then
            For Each cell As TableCell In e.Row.Cells
                If cell.HasControls Then
                    Dim button As LinkButton = DirectCast(cell.Controls(0), LinkButton)
                    If Not (button Is Nothing) Then
                        If GridViewSortColumn = button.CommandArgument Then
                            Dim image As Image = New Image
                            If GridViewSortDirection = SortDirection.Ascending Then
                                'image.ImageUrl = e.Row.Page.ResolveClientUrl("~/common/images/buttons/Asc.gif")
                                image.ImageUrl = "~/common/images/buttons/Asc.gif"
                                cell.Controls.Add(image)
                            Else
                                'image.ImageUrl = e.Row.Page.ResolveClientUrl("~/common/images/buttons/Desc.gif")
                                image.ImageUrl = "~/common/images/buttons/Desc.gif"
                                cell.Controls.Add(image)
                            End If
                            button.CssClass = "fldheaderlinkon"
                            cell.CssClass = "fldheaderon"

                        Else
                            button.CssClass = "fldheaderlink"
                            cell.CssClass = "fldheader"
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Public Function HandleGridViewSortDirectionToggling(e As GridViewSortEventArgs, GridViewSortColumn As String, GridViewSortDirection As SortDirection) As SortDirection
        If GridViewSortColumn = e.SortExpression Then
            If GridViewSortDirection = SortDirection.Ascending Then
                Return SortDirection.Descending
            Else
                Return SortDirection.Ascending
            End If
        Else
            Return SortDirection.Ascending
        End If
    End Function

    Public Function GetSchoolDetailsDataRow(GradelId As String) As DataRow
        Dim args As New HL.SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("id", GradelId, "equals"))
        Return mTIMSSDal.GetSchoolDetailsDataRow(args)
    End Function

    Public Function GetDistrictDetailsDataRow(leaid As String) As DataRow
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("leaid", leaid, "equals"))
        Return mTIMSSDal.GetDistrictDetailsDataRow(args)
    End Function

    Public Function GetDistrictPersonnelDataTable(leaid As String) As DataTable

        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        args.SortParameters.Add(New HL.SortParameter("fname", SortDirection.Ascending))
        args.SortParameters.Add(New HL.SortParameter("lname", SortDirection.Ascending))
        args.FilterParameters.Add(New HL.FilterParameter("leaid", leaid, "equals"))

        Return mTIMSSDal.GetDistrictPersonnelDataTable(args)
    End Function

    Public Sub SCR_SavePrincipalInformation(timssid As String, firstname As String, lastname As String, email As String)
        mTIMSSDal.SCR_SavePrincipalInformation(timssid, firstname, lastname, email)
    End Sub

    Public Sub SCR_UpdateRespondedFlag(timssid As String, responded As String)
        mTIMSSDal.SCR_UpdateRespondedFlag(timssid, responded)
    End Sub

    Public Sub SCR_UpdateEmailSentDT(timssid As String)
        mTIMSSDal.SCR_UpdateEmailSentDT(timssid)
    End Sub

    Public Sub SaveDistrictPersonnelEditChanges(controls As ControlCollection, pid As String, fldname As String, leaid As String, email As String)
        Dim args As New SaveChangesArgsBase(controls, pid)
        mTIMSSDal.SaveDistrictPersonnelEditChanges(args, fldname, leaid, email)
    End Sub

    Public Function GetSchoolPersonnelDataRow(pid As Integer) As DataRow
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("pid", pid, "equals"))
        Return mTIMSSDal.GetSchoolPersonnelDataRow(args)
    End Function

    Public Function GetDistrictPersonnelDataRow(pid As Integer) As DataRow
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("pid", pid, "equals"))
        Return mTIMSSDal.GetDistrictPersonnelDataRow(args)
    End Function


    Public Function GetSchoolPersonnelDataTable(frame_n_ As String) As DataTable
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        args.SortParameters.Add(New HL.SortParameter("fname", SortDirection.Ascending))
        args.SortParameters.Add(New HL.SortParameter("lname", SortDirection.Ascending))
        args.FilterParameters.Add(New FilterParameter("frame_n_", frame_n_, "equals"))
        Return mTIMSSDal.GetSchoolPersonnelDataTable(args)
    End Function

    Public Function GetFrameN(gradeid As String) As String
        Return mTIMSSDal.GetFrameN(gradeid)
    End Function

    Public Sub SaveSchoolPersonnelEditChanges(controls As ControlCollection, pid As String, fldname As String, frame_n_ As String, email As String)
        Dim args As New SaveChangesArgsBase(controls, pid)
        mTIMSSDal.SaveSchoolPersonnelEditChanges(args, fldname, frame_n_, email)
    End Sub

    Public Function GetWorkAreaNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetWorkAreaNameValuePairList(args)
    End Function

    Public Shared ReadOnly Property IsTestAdministrator() As Boolean
        Get
            Return Roles.IsUserInRole("Test Administrator")
        End Get
    End Property

    Public Shared ReadOnly Property IsNAEPStateCoordinator() As Boolean
        Get
            Return Roles.IsUserInRole("NAEP State Coordinator")
        End Get
    End Property

    Public Shared ReadOnly Property IsHomeOffice() As Boolean
        Get
            Return Roles.IsUserInRole("Home Office")
        End Get
    End Property

    Public Shared ReadOnly Property IsFieldManager() As Boolean
        Get
            Return Roles.IsUserInRole("Field Manager")
        End Get
    End Property

    Public Shared ReadOnly Property IsFieldDirector() As Boolean
        Get
            Return Roles.IsUserInRole("Field Director")
        End Get
    End Property

    Public Shared ReadOnly Property IsAdmin() As Boolean
        Get
            Return Roles.IsUserInRole("Admin") Or Roles.IsUserInRole("WesAdmin")
        End Get
    End Property

    Public Shared ReadOnly Property CanFilterByArea() As Boolean
        Get
            Return IsAdmin() Or IsHomeOffice() Or IsFieldManager()
        End Get
    End Property

    Public Shared Function UsernameCanAccessSCS(username As String) As Boolean
        Return Roles.IsUserInRole(username, "Admin") Or Roles.IsUserInRole(username, "WesAdmin") Or Roles.IsUserInRole(username, "WebsiteSCS")
    End Function


    Public Shared ReadOnly Property CanAccessSCS() As Boolean
        Get
            Return IsAdmin() Or Roles.IsUserInRole("WebsiteSCS")
        End Get
    End Property
    Public Shared ReadOnly Property CanAccessEfile() As Boolean
        Get
            Return IsAdmin() Or Roles.IsUserInRole("WebsiteEfile")
        End Get
    End Property

    Public Shared Function Fullname() As String
        Dim prof As ProfileCommon = ProfileCommon.GetUserProfile()
        Return prof.FirstName & " " & prof.LastName
    End Function

    Public Shared Function MyRoles() As String
        Dim result As String = ""
        Dim u As MembershipUser = Membership.GetUser
        Dim rolelist As String() = Roles.GetRolesForUser(u.UserName)
        For Each role As String In rolelist
            result = result & IIf(String.IsNullOrEmpty(result), "", "<br />") & role
        Next
        Return "<span>" & result & "</span>"

    End Function

    Public Function GetAccountDetailsDataTable(args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("ApplicationName", Membership.ApplicationName, "equals"))
        Return mTIMSSDal.GetAccountDetailsDataTable(args)
    End Function

    Public Shared Sub SetDropDownListSelectedValue(control As DropDownList, value As String)
        control.SelectedIndex = -1
        For Each item As ListItem In control.Items
            If item.Value.Equals(value) Then
                item.Selected = True
            End If
        Next
    End Sub

    Public Function CalcDaysTillPasswordExpires(CreateDate As DateTime, DaysPasswordIsValid As Integer) As Integer
        'Dim ExpirationDate As DateTime = CreateDate.AddDays(DaysPasswordIsValid)
        'Dim timeremaining As TimeSpan = ExpirationDate.Subtract(DateTime.Now)
        'Return timeremaining.Days
        Return TIMSSBLLService.CalcDaysTillPasswordExpires(CreateDate, DaysPasswordIsValid)
    End Function


    Public Function GetREPSBGRPNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetREPSBGRPNameValuePairList()
    End Function

    Private Sub InitSelectFromDatabaseArgss(args As SelectFromDatabaseArgs)

        args.WINSID = mProf.WINSID
        args.REPSBGRP = mProf.REPSBGRP
        args.IsFieldDirector = IsFieldDirector
        args.IsFieldManager = IsFieldManager
        args.IsNaepStateCoordinator = IsNAEPStateCoordinator
        args.IsTestAdministrator = IsTestAdministrator
        args.IsHomeOffice = IsHomeOffice
        args.IsAdmin = IsAdmin
        args.IsTudaCoordinator = IsTudaCoordinator
        args.IsMyTIMSSUser = IsMyTIMSSUser
        args.TUA_LEA = mProf.TUA_LEA
    End Sub

    Public Function GetFirstLetterOfSchoolNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        'Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetFirstLetterOfSchoolNameValuePairList(args)
    End Function

    Public Function GetFirstLetterOfDistrictNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetFirstLetterOfDistrictNameValuePairList(args)
    End Function

    Public Function GetStaffWINSIDNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetStaffWINSIDNameValuePairList()
    End Function

    Public Function GetEmailTemplateDataRow(templatename As String) As DataRow
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("EmailTemplateName", templatename, "equals"))
        Return mTIMSSDal.GetEmailTemplateDataRow(args)
    End Function

    Public Sub SaveEmailTemplateEditChanges(controls As ControlCollection, emailtemplatename As String)
        Dim args As New SaveChangesArgsBase(controls, emailtemplatename)
        mTIMSSDal.SaveEmailTemplateEditChanges(args)
    End Sub

    Public Sub UpdateConfirmationEmailSent(pID As Integer)
        mTIMSSDal.UpdateConfirmationEmailSent(pID)
    End Sub



    Public Sub SendEmail_SCR_Yand4(schoolname As String, timssid As String, attachmentfilepath As String, principallastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>Dear Principal " & principallastname & ",</p>" &
                             "<p>Thank you for confirming you are the current principal at " & schoolname & " and providing updated contact information. Your school's confidential TIMSS school report is attached to this email.</p>" &
                             "<p>The attached school report includes links to PDFs containing the associated 2015 TIMSS restricted mathematics and science items, so that you may see the items listed in the report tables.  Because of the restricted nature of these items, the PDFs are located on the MyTIMSS web site, meaning that you will need to log-in to access the files.</p>" &
                             "<p>When you click on a link that goes to either the mathematics or science restricted items PDFs, either in the main report or one of the item names in the report tables, you will be directed to the log-in screen for MyTIMSS.  Please use the following username and password when you log in:</p>" &
                             "<p><b>Username:</b>&nbsp;prin2015</p>" &
                             "<p><b>Password:</b>&nbsp;$EMUNQ3hw7</p>" &
                             "<p>Once logged in, you will then be directed to the PDF associated with the link you clicked.  As long as you keep your web browser open, you will not have to log in again when you click on another link to those PDFs.</p>" &
                             "<p>You can also see the TIMSS 2015 report for the United States, which was released in November 2016, at <a href=""https://nces.ed.gov/timss/timss2015/"">https://nces.ed.gov/timss/timss2015/</a>.  The international reports are available at <a href=""https://timssandpirls.bc.edu/timss2015/"">https://timssandpirls.bc.edu/timss2015/</a>.</p>" &
                             "<p>Again, many thanks for your students’ and school staff’s time and effort in participating in TIMSS 2015.  We hope that you will find the school report useful and informative. If you have any questions, please contact <b>TIMSSreport@westat.com</b>.</p>" &
                             "<p>Sincerely,</p>" &
                             "<p>The TIMSS 2015 Team</p>"

        mail.Attachments.Add(New Attachment(attachmentfilepath))

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)

        SCR_UpdateEmailSentDT(timssid)
    End Sub 'SendEmail

    Public Sub SendEmail_SCR_Yand8(schoolname As String, timssid As String, attachmentfilepath As String, principallastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>Dear Principal " & principallastname & ",</p>" &
                             "<p>Thank you for confirming you are the current principal at " & schoolname & " and providing updated contact information. Your school's confidential TIMSS school report is attached to this email.</p>" &
                             "<p>The attached school report includes links to PDFs containing the associated 2015 TIMSS restricted mathematics and science items, so that you may see the items listed in the report tables.  Because of the restricted nature of these items, the PDFs are located on the MyTIMSS web site, meaning that you will need to log-in to access the files.</p>" &
                             "<p>When you click on a link that goes to either the mathematics or science restricted items PDFs, either in the main report or one of the item names in the report tables, you will be directed to the log-in screen for MyTIMSS.  Please use the following username and password when you log in:</p>" &
                             "<p><b>Username:</b>&nbsp;prin2015</p>" &
                             "<p><b>Password:</b>&nbsp;$EMUNQ3hw7</p>" &
                             "<p>Once logged in, you will then be directed to the PDF associated with the link you clicked.  As long as you keep your web browser open, you will not have to log in again when you click on another link to those PDFs.</p>" &
                             "<p>You can also see the TIMSS 2015 report for the United States, which was released in November 2016, at <a href=""https://nces.ed.gov/timss/timss2015/"">https://nces.ed.gov/timss/timss2015/</a>.  The international reports are available at <a href=""https://timssandpirls.bc.edu/timss2015/"">https://timssandpirls.bc.edu/timss2015/</a>.</p>" &
                             "<p>Again, many thanks for your students’ and school staff’s time and effort in participating in TIMSS 2015.  We hope that you will find the school report useful and informative. If you have any questions, please contact <b>TIMSSreport@westat.com</b>.</p>" &
                             "<p>Sincerely,</p>" &
                             "<p>The TIMSS 2015 Team</p>"

        mail.Attachments.Add(New Attachment(attachmentfilepath))

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)

        SCR_UpdateEmailSentDT(timssid)
    End Sub 'SendEmail

    Public Sub SendEmail_SCR_YandAdv(schoolname As String, timssid As String, attachmentfilepath As String, principallastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>Dear Principal " & principallastname & ",</p>" &
                             "<p>Thank you for confirming you are the current principal at " & schoolname & " and providing updated contact information. Your school's confidential TIMSS Advanced school report is attached to this email.</p>" &
                             "<p>The attached school report includes links to PDFs containing the associated 2015 TIMSS restricted mathematics and science items, so that you may see the items listed in the report tables.  Because of the restricted nature of these items, the PDFs are located on the MyTIMSS web site, meaning that you will need to log-in to access the files.</p>" &
                             "<p>When you click on a link that goes to either the mathematics or science restricted items PDFs, either in the main report or one of the item names in the report tables, you will be directed to the log-in screen for MyTIMSS.  Please use the following username and password when you log in:</p>" &
                             "<p><b>Username:</b>&nbsp;prin2015</p>" &
                             "<p><b>Password:</b>&nbsp;$EMUNQ3hw7</p>" &
                             "<p>Once logged in, you will then be directed to the PDF associated with the link you clicked.  As long as you keep your web browser open, you will not have to log in again when you click on another link to those PDFs.</p>" &
                             "<p>You can also see the TIMSS Advanced 2015 report for the United States, which was released in November 2016, at <a href=""https://nces.ed.gov/timss/timss2015/"">https://nces.ed.gov/timss/timss2015/</a>.  The international reports are available at <a href=""https://timssandpirls.bc.edu/timss2015/advanced/"">https://timssandpirls.bc.edu/timss2015/advanced/</a>.</p>" &
                             "<p>Again, many thanks for your students’ and school staff’s time and effort in participating in TIMSS Advanced 2015.  We hope that you will find the school report useful and informative. If you have any questions, please contact <b>TIMSSreport@westat.com</b>.</p>" &
                             "<p>Sincerely,</p>" &
                             "<p>The TIMSS Advanced 2015 Team</p>"

        mail.Attachments.Add(New Attachment(attachmentfilepath))

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)

        SCR_UpdateEmailSentDT(timssid)
    End Sub 'SendEmail

    Public Sub SendEmail_SCR_Nand4(schoolname As String, timssid As String, principallastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>Dear Principal " & principallastname & ",</p>" &
                             "<p>Thank you for confirming you are the current principal at " & schoolname & " and providing updated contact information.</p>" &
                             "<p>Your school has elected not to receive a report.</p>" &
                             "<p>Again, many thanks for your students’ and school staff’s time and effort in participating in TIMSS 2015. If you have any questions, please contact <b>TIMSSreport@westat.com</b>.</p>" &
                             "<p>Sincerely,</p>" &
                             "<p>The TIMSS 2015 Team</p>"

        'NO ATTACHMENT

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)

        SCR_UpdateEmailSentDT(timssid)
    End Sub 'SendEmail

    Public Sub SendEmail_SCR_Nand8(schoolname As String, timssid As String, principallastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>Dear Principal " & principallastname & ",</p>" &
                             "<p>Thank you for confirming you are the current principal at " & schoolname & " and providing updated contact information.</p>" &
                             "<p>Your school has elected not to receive a report.</p>" &
                             "<p>Again, many thanks for your students’ and school staff’s time and effort in participating in TIMSS 2015. If you have any questions, please contact <b>TIMSSreport@westat.com</b>.</p>" &
                             "<p>Sincerely,</p>" &
                             "The TIMSS 2015 Team"
        'NO ATTACHMENT

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)

        SCR_UpdateEmailSentDT(timssid)
    End Sub 'SendEmail

    Public Sub SendEmail_SCR_NandAdv(schoolname As String, timssid As String, principallastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>Dear Principal " & principallastname & ",</p>" &
                             "<p>Thank you for confirming you are the current principal at " & schoolname & " and providing updated contact information.</p>" &
                             "<p>Your school has elected not to receive a report.</p>" &
                             "<p>Again, many thanks for your students’ and school staff’s time and effort in participating in TIMSS Advanced 2015. If you have any questions, please contact <b>TIMSSreport@westat.com</b>.</p>" &
                             "<p>Sincerely,</p>" &
                             "<p>The TIMSS Advanced 2015 Team</p>"
        'NO ATTACHMENT

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)

        SCR_UpdateEmailSentDT(timssid)
    End Sub 'SendEmail

    Public Sub SendEmail_SCR_Summary(schoolname As String, principalname As String, principalemail As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<p>The principal on record, <b>" & principalname & " (" & principalemail & ")</b>, has just specified they are no longer the principal of <b>" & schoolname & "</b> in the School Report Dissemination process.</p>"

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_TIMMSRegistrationG4(attachmentfilepath As String, schoolname As String, firstandlastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3"">Dear " & firstandlastname & ",<br><br>" &
                                "Thank you for creating a MyTIMSS account for the Trends in International Mathematics and Science Study (TIMSS). https://www.MyTIMSS.com is a secure website for sharing information and files related to your school’s participation in the TIMSS 2018 Field Test. The website’s <b>""What You Need To Do""</b> menu will guide you through the upcoming TIMSS activities.<br><br>" &
                                "The first step is to complete the <b>Provide School Information</b> page. If you have not already done so, please go to https://www.MyTIMSS.com/ProvideSchoolInformation.aspx. <br><br>" &
                                "In January, you will need to provide a complete and current list of <b>all of your school’s fourth-grade classes</b> for the TIMSS team to randomly select two classes to participate in the TIMSS Field Test. You will then be asked to submit lists of <b>all students in the selected classes</b>. TIMSS also needs a list of <b>all of your school’s fourth-grade teachers</b> to assign teacher questionnaires that will be used to link information about science teachers to the assessed students. We will email you in early January with instructions about how to submit the class, student, and teacher lists through https://www.MyTIMSS.com. <br><br>" &
                                "Please see the <b>attached</b> Summary of School Activities for a complete list of activities associated with your school’s participation in the TIMSS 2018 Field Test.<br><br>" &
                                "Thank you very much for participating in TIMSS, and for playing a key role in making the assessment a success in <b>" & schoolname & "</b>!<br><br>" &
                                "Sincerely,<br><br>" &
                                "The TIMSS Team<br><br></font>" &
                                "<font size=""2""><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></font>"

        mail.Attachments.Add(New Attachment(attachmentfilepath))

        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_TIMMSRegistrationG4IN(schoolname As String, firstandlastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3"">Dear " & firstandlastname & ",<br><br>" &
                                "Thank you for creating a MyTIMSS account for the Trends in International Mathematics and Science Study (TIMSS). https://www.MyTIMSS.com is a secure website for sharing information and files related to your school’s participation in the TIMSS 2018 Field Test. The website’s <b>""What You Need To Do""</b> menu will guide you through the upcoming TIMSS activities.<br><br>" &
                                "The first step is to complete the <b>Provide School Information</b> page. If you have not already done so, please go to https://www.MyTIMSS.com/ProvideSchoolInformation.aspx. <br><br>" &
                                "In January, you will need to provide a complete and current list of <b>all of your school’s fourth-grade classes</b> for the TIMSS team to randomly select two classes to participate in the TIMSS Field Test. You will then be asked to submit lists of <b>all students in the selected classes</b>. TIMSS also needs a list of <b>all of your school’s fourth-grade teachers</b> to assign teacher questionnaires that will be used to link information about science teachers to the assessed students. We will email you in early January with instructions about how to submit the class, student, and teacher lists through https://www.MyTIMSS.com. <br><br>" &
                                "Thank you very much for participating in TIMSS, and for playing a key role in making the assessment a success in <b>" & schoolname & "</b>!<br><br>" &
                                "Sincerely,<br><br>" &
                                "The TIMSS Team<br><br></font>" &
                                "<font size=""2""><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></font>"


        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_TIMMSRegistrationG8(attachmentfilepath As String, schoolname As String, firstandlastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3"">Dear " & firstandlastname & ",<br><br>" &
                                "Thank you for creating a MyTIMSS account for the Trends in International Mathematics and Science Study (TIMSS) on https://www.MyTIMSS.com. MyTIMSS is a secure website for sharing information and files related to your school’s participation in the TIMSS 2018 Field Test. The website’s <b>""What You Need to Do""</b> menu will guide you through the upcoming TIMSS activities.<br><br>" &
                                "The first step is to complete the <b>Provide School Information</b> page. If you have not already done so, please go to https://www.MyTIMSS.com/ProvideSchoolInformation.aspx. <br><br>" &
                                "In January, you will need to provide a complete and current list of <b>all of your school’s eighth-grade mathematics classes</b> for the TIMSS team to randomly select two classes to participate in the TIMSS Field Test. You will then be asked to submit lists of <b>all students in the selected classes</b>. TIMSS also needs a list of <b>all of your school’s eighth-grade science teachers</b> and the courses they teach to assign teacher questionnaires that will be used to link information about science teachers to the assessed students. We will email you in early January with instructions about how to submit the class list, student list, and teacher list through https://www.MyTIMSS.com. <br><br>" &
                                "Please see the <b>attached</b> Summary of School Activities for a complete list of activities associated with your school’s participation in the TIMSS 2018 Field Test.<br><br>" &
                                "Thank you very much for participating in TIMSS, and for playing a key role in making the assessment a success in " & schoolname & "!<br><br>" &
                                "Sincerely,<br><br>" &
                                "The TIMSS Team<br><br></font>" &
                                "<font size=""2""><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></font>"

        mail.Attachments.Add(New Attachment(attachmentfilepath))

        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_TIMMSRegistrationG8IN(schoolname As String, firstandlastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3"">Dear " & firstandlastname & ",<br><br>" &
                                "Thank you for creating a MyTIMSS account for the Trends in International Mathematics and Science Study (TIMSS) on https://www.MyTIMSS.com. MyTIMSS is a secure website for sharing information and files related to your school’s participation in the TIMSS 2018 Field Test. The website’s <b>""What You Need to Do""</b> menu will guide you through the upcoming TIMSS activities.<br><br>" &
                                "The first step is to complete the <b>Provide School Information</b> page. If you have not already done so, please go to https://www.MyTIMSS.com/ProvideSchoolInformation.aspx. <br><br>" &
                                "In January, you will need to provide a complete and current list of <b>all of your school’s eighth-grade mathematics classes</b> for the TIMSS team to randomly select two classes to participate in the TIMSS Field Test. You will then be asked to submit lists of <b>all students in the selected classes</b>. TIMSS also needs a list of <b>all of your school’s eighth-grade science teachers</b> and the courses they teach to assign teacher questionnaires that will be used to link information about science teachers to the assessed students. We will email you in early January with instructions about how to submit the class list, student list, and teacher list through https://www.MyTIMSS.com. <br><br>" &
                                "Thank you very much for participating in TIMSS, and for playing a key role in making the assessment a success in " & schoolname & "!<br><br>" &
                                "Sincerely,<br><br>" &
                                "The TIMSS Team<br><br></font>" &
                                "<font size=""2""><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></font>"

        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_ICILSRegistration(attachmentfilepath As String, schoolname As String, firstandlastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3"">Dear " & firstandlastname & ",<br><br>" &
                              "Thank you for creating a MyICILS account for the International Computer and Information Literacy Study (ICILS). https://www.MyICILS.com is a secure website for sharing information and files related to your school's participation in ICILS 2018. The website’s <b>""What You Need to Do""</b> menu will guide you through the upcoming ICILS activities.<br><br>" &
                              "The first step is to complete the <b>Provide School Information</b> page. If you have not already done so, please go to https://www.MyICILS.com/ProvideSchoolInformation.aspx. <br><br>" &
                              "In January, you will need to provide a complete and current list of <b>all of your school's eighth-grade students</b> for the ICILS team to randomly select up to 30 students to participate in the ICILS field test, and a list of <b>all of your school’s eighth-grade teachers</b> for random selection of up to 20 teachers to complete a teacher questionnaire. We will email you in January with instructions about how to submit the student list and teacher list through https://www.MyICILS.com. <br><br>" &
                              "Please see the <b>attached</b> Summary Of School Activities for a complete list of activities associated with your school's participation in ICILS 2018.<br><br>" &
                              "Thank you very much for participating in ICILS, and for playing a key role in making the assessment a success in <b>" & schoolname & "</b>!<br><br>" &
                              "Sincerely,<br><br>" &
                              "The ICILS Team<br><br></font>" &
                              "<font size=""2""><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></font>"

        mail.Attachments.Add(New Attachment(attachmentfilepath))

        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_ICILSRegistrationIN(schoolname As String, firstandlastname As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3"">Dear " & firstandlastname & ",<br><br>" &
                              "Thank you for creating a MyICILS account for the International Computer and Information Literacy Study (ICILS). https://www.MyICILS.com is a secure website for sharing information and files related to your school's participation in ICILS 2018. The website’s <b>""What You Need to Do""</b> menu will guide you through the upcoming ICILS activities.<br><br>" &
                              "The first step is to complete the <b>Provide School Information</b> page. If you have not already done so, please go to https://www.MyICILS.com/ProvideSchoolInformation.aspx. <br><br>" &
                              "In January, you will need to provide a complete and current list of <b>all of your school's eighth-grade students</b> for the ICILS team to randomly select up to 30 students to participate in the ICILS field test, and a list of <b>all of your school’s eighth-grade teachers</b> for random selection of up to 20 teachers to complete a teacher questionnaire. We will email you in January with instructions about how to submit the student list and teacher list through https://www.MyICILS.com. <br><br>" &
                              "Thank you very much for participating in ICILS, and for playing a key role in making the assessment a success in <b>" & schoolname & "</b>!<br><br>" &
                              "Sincerely,<br><br>" &
                              "The ICILS Team<br><br></font>" &
                              "<font size=""2""><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></font>"

        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_ICILSRegistrationLoginInfo(numofdays As String, username As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body As String = "<font size=""3""><p>Thank you for creating a MyICILS account for the International Computer and Information Literacy Study (ICILS). To access MyICILS, go to https://www.MyICILS.com and enter your username and password.</p>" &
                             "<p style=""margin-left:2em;""><b>Username: </b> " & username & "</p>" &
                             "<p style=""margin-left:2em;""><b>Password information:</b> You created your own password upon registering. Use the <b>""Forgot Password""</b> links on MyICILS login page if needed. You will be prompted to change your password after " & numofdays & " days.</p>" &
                             "<p>MyICILS is a secure site that contains confidential information, so it is important to remember your username and password. If you choose to write down this information, store it in a secure place.</p>" &
                             "<p>If you have questions or need help accessing the website, please contact the ICILS Help Desk at ICILS@westat.com or at 1-855-445-5604 Monday through Friday between 9 a.m. and 5:00 p.m. ET.<p>" &
                             "<p>Sincerely,</p>" &
                             "<p>The ICILS Team</p></font>" &
                             "<font size=""2""><p><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></p></font>"

        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail_TIMSSRegistrationLoginInfo(numofdays As String, username As String, emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        Dim body = "<font size=""3""><p>Thank you for creating a MyTIMSS account for the Trends in International Mathematics and Science Study (TIMSS). To access MyTIMSS, go to https://www.MyTIMSS.com and enter your username and password.</p>" &
                   "<p style=""margin-left:2em;""><b>Username: </b>" & username & "</p>" &
                   "<p style=""margin-left:2em;""><b>Password information:</b> You created your own password upon registering. Use the <b>""Forgot Password""</b> links on MyTIMSS login page if needed. You will be prompted to change your password after " & numofdays & " days.</p>" &
                   "<p>MyTIMSS is a secure site that contains confidential information, so it is important to remember your username and password. If you choose to write down this information, store it in a secure place.</p>" &
                   "<p>If you have questions or need help accessing the website, please contact the TIMSS Help Desk at TIMSS@westat.com or at 1-855-445-5604 Monday through Friday between 9 a.m. and 5:00 p.m. ET.<p>" &
                   "<p>Sincerely,</p>" &
                   "<p>The TIMSS Team</p></font>" &
                   "<font size=""2""><p><i>NCES is authorized to conduct this study under the Education Sciences Reform Act of 2002 (ESRA 2002, 20 U.S.C. §9543). All of the information provided by your staff and students may only be used for statistical purposes and may not be disclosed, or used, in identifiable form for any other purpose except as required by law (20 U.S.C. §9573 and 6 U.S.C. §151).</i></p></font>"


        'emailto = "danielbarnhouse@westat.com"
        'emailcc = ""

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject
        mail.Body = body
        mail.IsBodyHtml = IsBodyHtml
        mail.From = New MailAddress(emailfrom)
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Sub SendEmail(emailfrom As String, emailto As String, emailcc As String, emailbcc As String, emailsubject As String, emailbody As String, IsBodyHtml As Boolean, replyto As String)
        Dim mail As New MailMessage()

        mail.From = New MailAddress(emailfrom)

        If Not String.IsNullOrEmpty(replyto) Then
            mail.ReplyToList.Add(replyto)
        End If

        'mail.To.Add(emailto)
        If Not String.IsNullOrEmpty(emailto) Then
            Dim lst As String() = emailto.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.To.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailcc) Then
            Dim lst As String() = emailcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
        End If

        If Not String.IsNullOrEmpty(emailbcc) Then
            Dim lst As String() = emailbcc.Split(";")
            For Each e As String In lst
                If Not String.IsNullOrEmpty(e) Then
                    mail.CC.Add(e)
                End If
            Next
            'mail.Bcc.Add(emailbcc)
        End If

        mail.Subject = emailsubject

        mail.Body = emailbody
        mail.IsBodyHtml = IsBodyHtml
        'send the message
        Dim smtp As New SmtpClient()
        smtp.Send(mail)
    End Sub 'SendEmail

    Public Function GetReportDistrictRecruitmentDataTable(args As GetDistrictListSqlDataSourceArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetReportDistrictRecruitmentDataTable(args)
    End Function

    Public Function GetReportSchoolRecruitmentDataTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetReportSchoolRecruitmentDataTable(args)
    End Function

    Public Function GetRecruitmentReportSchoolTypeNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetRecruitmentReportSchoolTypeNameValuePairList(args)
    End Function

    Public Function GetRecruitmentReportFilterNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetRecruitmentReportFilterNameValuePairList(args)
    End Function

    Public Function GeExportReportOptionsNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GeExportReportOptionsNameValuePairList(args)
    End Function

    Public Function GetAssessmentDateList(state As String, region As String) As List(Of AssessmentDate)
        Dim args As New GetSchoolListSqlDataSourceArgs
        InitSelectFromDatabaseArgss(args)

        'args.FilterParameters.Add(New FilterParameter("s_state", state, "equals"))
        If Not String.IsNullOrEmpty(state) Then
            args.FilterParameters.Add(New FilterParameter("REPSBGRP", state, "equals"))
        End If

        If Not String.IsNullOrEmpty(region) Then
            args.FilterParameters.Add(New FilterParameter("testregion", region, "equals"))
        End If

        args.SortParameters.Add(New SortParameter("ScheDate", SortDirection.Ascending))
        Return mTIMSSDal.GetAssessmentDateList(args)
    End Function


    Public Function GetAssessmentDateFromList(lst As List(Of AssessmentDate), targetDate As DateTime) As AssessmentDate
        Return mTIMSSDal.GetAssessmentDateFromList(lst, targetDate)
    End Function

    Public Function GetAssessmentDateListTestRegionNameValuePairList(REPSBGRP As String) As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        'args.FilterParameters.Add(New FilterParameter("S_State", S_State, "equals"))
        If Not String.IsNullOrEmpty(REPSBGRP) Then
            args.FilterParameters.Add(New FilterParameter("REPSBGRP", REPSBGRP, "equals"))
        End If
        Return mTIMSSDal.GetAssessmentDateListTestRegionNameValuePairList(args)
    End Function

    Public Function GetStateNameValuePairList(args As SelectFromDatabaseArgs, showalloption As Boolean) As List(Of NameValuePair)
        If args Is Nothing Then
            args = New SelectFromDatabaseArgs
        End If
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetStateNameValuePairList(args, showalloption)
    End Function

    Public Function GetREPSBGRPValuePairList(args As SelectFromDatabaseArgs, showalloption As Boolean) As List(Of NameValuePair)
        If args Is Nothing Then
            args = New SelectFromDatabaseArgs
        End If
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetREPSBGRPValuePairList(args, showalloption)
    End Function


    Public Function GetREPSValuePairList(args As SelectFromDatabaseArgs, showalloption As Boolean) As List(Of NameValuePair)
        If args Is Nothing Then
            args = New SelectFromDatabaseArgs
        End If
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetREPSValuePairList(args, showalloption)
    End Function

    Public Function GetAssessmentDateListStateNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        'Return mTIMSSDal.GetAssessmentDateListStateNameValuePairList(args)
        args.FilterParameters.Add(New FilterParameter("ScheDate", "isnotnull"))
        Return GetStateNameValuePairList(args, True)
    End Function
    Public Function GetAssessmentDateListREPSBGRPValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        'Return mTIMSSDal.GetAssessmentDateListStateNameValuePairList(args)
        args.FilterParameters.Add(New FilterParameter("ScheDate", "isnotnull"))
        Return GetREPSBGRPValuePairList(args, True)
    End Function

    Public Shared ReadOnly Property IsDevelopmentWebsite() As Boolean
        Get
            Return System.Web.HttpContext.Current.Request.ServerVariables("HTTP_HOST").Equals("mytimssdev.westat.com", StringComparison.CurrentCultureIgnoreCase) Or _
                System.Web.HttpContext.Current.Request.ServerVariables("HTTP_HOST").StartsWith("localhost", StringComparison.CurrentCultureIgnoreCase)
        End Get
    End Property

    Public Shared ReadOnly Property IsTestingWebsite() As Boolean
        Get
            Return System.Web.HttpContext.Current.Request.ServerVariables("HTTP_HOST").Equals("mytimsstst.wesdemo.com", StringComparison.CurrentCultureIgnoreCase)
        End Get
    End Property

    Public Shared ReadOnly Property IsTrainingWebsite() As Boolean
        Get
            Return System.Web.HttpContext.Current.Request.ServerVariables("HTTP_HOST").Equals("mytimssdemo.wesdemo.com", StringComparison.CurrentCultureIgnoreCase)
        End Get
    End Property

    Public Shared ReadOnly Property IsProductionWebsite() As Boolean
        Get
            Return System.Web.HttpContext.Current.Request.ServerVariables("HTTP_HOST").Equals("www.mytimss.com", StringComparison.CurrentCultureIgnoreCase)
        End Get
    End Property

    Public Function GetTerritoryNameValuePairList() As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetTerritoryNameValuePairList(args)
    End Function

    Public Function GetREPSValuePairListForTerritory(Territory As String, showalloption As Boolean) As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)

        If Not String.IsNullOrEmpty(Territory) Then
            args.FilterParameters.Add(New FilterParameter("fldTerritoryCode", Territory, "equals"))
        End If

        Return GetREPSValuePairList(args, showalloption)
    End Function

    Public Function GetStateNameValuePairListForTerritory(Territory As String, showalloption As Boolean) As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)

        If Not String.IsNullOrEmpty(Territory) Then
            args.FilterParameters.Add(New FilterParameter("fldTerritoryCode", Territory, "equals"))
        End If

        Return GetStateNameValuePairList(args, showalloption)
    End Function

    Public Sub SaveProvideSchoolInformationEditChanges(controls As ControlCollection, frame_n_ As String, principalid As String, principalemail As String, coordinatorid As String, coordinatoremail As String, id4 As String, id8 As String, id12 As String, updateprincipal As Boolean, updatecoordinator As Boolean)
        Dim args As New SaveProvideSchoolInformationEditChangesArgs(controls, frame_n_, principalid, coordinatorid)
        Dim principalargs As New SaveChangesArgsBase(controls, principalid, "dbprincipal_")
        Dim coordinatorargs As New SaveChangesArgsBase(controls, coordinatorid, "dbcoordinator_")

        mTIMSSDal.SaveProvideSchoolInformationEditChanges(args)

        If updateprincipal Then mTIMSSDal.SaveSchoolPersonnelEditChanges(principalargs, "principalid", frame_n_, principalemail)

        If updatecoordinator Then mTIMSSDal.SaveSchoolPersonnelEditChanges(coordinatorargs, "coordinatorid", frame_n_, coordinatoremail)

        If Not String.IsNullOrEmpty(id4) Then
            Dim g4args As New SaveChangesArgsBase(controls, id4, "dbG4_")
            mTIMSSDal.HandleTableUpdate(g4args.DbControls, g4args.PrimaryKey, "tblscsgrade", "ID")
        End If

        If Not String.IsNullOrEmpty(id8) Then
            Dim g8args As New SaveChangesArgsBase(controls, id8, "dbG8_")
            mTIMSSDal.HandleTableUpdate(g8args.DbControls, g8args.PrimaryKey, "tblscsgrade", "ID")
        End If

        If Not String.IsNullOrEmpty(id12) Then
            Dim g12args As New SaveChangesArgsBase(controls, id12, "dbG12_")
            mTIMSSDal.HandleTableUpdate(g12args.DbControls, g12args.PrimaryKey, "tblscsgrade", "ID")
        End If

        mTIMSSDal.SavePSISubmittedBy(frame_n_, Me.mUser.ProviderUserKey)

    End Sub

    Public Function GetDistinctSchoolListDataTable() As DataTable
        Dim args As New GetSchoolListSqlDataSourceArgs
        args.SortParameters.Add(New SortParameter("s_name", SortDirection.Ascending))
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetDistinctSchoolListDataTable(args)
    End Function

    Public Function getSchoolRecordByConfID(confid As String) As DataRow
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.getSchoolRecordByConfID(args, confid)
    End Function


    Public Function GetPSISchoolDetailsDataRow(frame_n_ As String) As DataRow
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetPSISchoolDetailsDataRow(args, frame_n_)
    End Function

    Public Function GetMyTIMSSRegistrationDataRow(MyNAEPREGID As String) As DataRow
        Return mTIMSSDal.GetMyTIMSSRegistrationDataRow(MyNAEPREGID)
    End Function

    Public Function GetRegistrationIDNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetRegistrationIDNameValuePairList()
    End Function

    Public Sub SaveSchoolPersonnelEditChangesDuringRegistration(NameValuePairList As List(Of NameValuePair), iscoordinator As Boolean, frame_n_ As String, email As String)
        mTIMSSDal.SaveSchoolPersonnelEditChanges(NameValuePairList, Nothing, IIf(iscoordinator, "coordinatorid", Nothing), frame_n_, email)
    End Sub

    Public Property Frame_N_() As String
        Get
            If IsMyTIMSSUser() Then
                Return mProf.Frame_N_
            Else
                Return System.Web.HttpContext.Current.Server.HtmlEncode(System.Web.HttpContext.Current.Session("Frame_N_"))
            End If
        End Get

        Set(ByVal value As String)
            If CanChangeSchools() Then
                System.Web.HttpContext.Current.Session("Frame_N_") = value
            End If
        End Set
    End Property

    Public Function GetFrameNForRegistrationID(MyNAEPREGID As String) As String
        Return mTIMSSDal.GetFrameNForRegistrationID(MyNAEPREGID)
    End Function

    Public Shared ReadOnly Property IsMyTIMSSUser() As Boolean
        Get
            Return Roles.IsUserInRole("MyTIMSSUser")
        End Get
    End Property

    Public Shared Sub CleanUpSessionVariables()
        System.Web.HttpContext.Current.Session.RemoveAll()

        'For Each var In System.Web.HttpContext.Current.Session.Keys = Nothing
        'Next
    End Sub

    Public Shared ReadOnly Property CanChangeSchools() As Boolean
        Get
            Return Westat.TIMSS.BLL.TIMSSBLL.IsAdmin() Or Westat.TIMSS.BLL.TIMSSBLL.IsFieldDirector Or Westat.TIMSS.BLL.TIMSSBLL.IsFieldManager Or Westat.TIMSS.BLL.TIMSSBLL.IsHomeOffice Or Westat.TIMSS.BLL.TIMSSBLL.IsNAEPStateCoordinator Or Westat.TIMSS.BLL.TIMSSBLL.IsTudaCoordinator Or Westat.TIMSS.BLL.TIMSSBLL.IsTestAdministrator Or Westat.TIMSS.BLL.TIMSSBLL.IsTestAdministratorTroubleShooter
        End Get
    End Property

    Public ReadOnly Property HasFrame_N_() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Frame_N_())
        End Get
    End Property


    Public Property ProjectID() As String
        Get
            If System.Web.HttpContext.Current.Session("ProjectID") = Nothing Then
                System.Web.HttpContext.Current.Session("ProjectID") = ""
            End If
            Return System.Web.HttpContext.Current.Session("ProjectID")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("ProjectID") = value
        End Set
    End Property

    Public Property GradeId4() As String
        Get
            If System.Web.HttpContext.Current.Session("GradeId4") = Nothing Then
                System.Web.HttpContext.Current.Session("GradeId4") = ""
            End If
            Return System.Web.HttpContext.Current.Session("GradeId4")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("GradeId4") = value
        End Set
    End Property
    Public ReadOnly Property HasGradeId4() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId4())
        End Get
    End Property


    Public Property GradeId8() As String
        Get
            If System.Web.HttpContext.Current.Session("GradeId8") = Nothing Then
                System.Web.HttpContext.Current.Session("GradeId8") = ""
            End If
            Return System.Web.HttpContext.Current.Session("GradeId8")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("GradeId8") = value
        End Set
    End Property
    Public ReadOnly Property HasGradeId8() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId8())
        End Get
    End Property

    Public Property GradeId12() As String
        Get
            If System.Web.HttpContext.Current.Session("GradeId12") = Nothing Then
                System.Web.HttpContext.Current.Session("GradeId12") = ""
            End If
            Return System.Web.HttpContext.Current.Session("GradeId12")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("GradeId12") = value
        End Set
    End Property
    Public ReadOnly Property HasGradeId12() As Boolean
        Get
            Return Not String.IsNullOrEmpty(GradeId12())
        End Get
    End Property

    Public Property SchoolName() As String
        Get
            If System.Web.HttpContext.Current.Session("SchoolName") = Nothing Then
                System.Web.HttpContext.Current.Session("SchoolName") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SchoolName")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SchoolName") = value
        End Set
    End Property

    Public ReadOnly Property HasSchoolName() As Boolean
        Get
            Return Not String.IsNullOrEmpty(SchoolName())
        End Get
    End Property

    Public Property SCHEDATE4() As String
        Get
            If System.Web.HttpContext.Current.Session("SCHEDATE4") = Nothing Then
                System.Web.HttpContext.Current.Session("SCHEDATE4") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SCHEDATE4")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SCHEDATE4") = value
        End Set
    End Property
    Public Property SCHEDATE4_2() As String
        Get
            If System.Web.HttpContext.Current.Session("SCHEDATE4_2") = Nothing Then
                System.Web.HttpContext.Current.Session("SCHEDATE4_2") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SCHEDATE4_2")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SCHEDATE4_2") = value
        End Set
    End Property


    Public Property SCHEDATE8() As String
        Get
            If System.Web.HttpContext.Current.Session("SCHEDATE8") = Nothing Then
                System.Web.HttpContext.Current.Session("SCHEDATE8") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SCHEDATE8")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SCHEDATE8") = value
        End Set
    End Property
    Public Property SCHEDATE8_2() As String
        Get
            If System.Web.HttpContext.Current.Session("SCHEDATE8_2") = Nothing Then
                System.Web.HttpContext.Current.Session("SCHEDATE8_2") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SCHEDATE8_2")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SCHEDATE8_2") = value
        End Set
    End Property

    Public Property SCHEDATE12() As String
        Get
            If System.Web.HttpContext.Current.Session("SCHEDATE12") = Nothing Then
                System.Web.HttpContext.Current.Session("SCHEDATE12") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SCHEDATE12")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SCHEDATE12") = value
        End Set
    End Property

    Public Property SchoolAddress() As String
        Get
            If System.Web.HttpContext.Current.Session("SchoolAddress") = Nothing Then
                System.Web.HttpContext.Current.Session("SchoolAddress") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SchoolAddress")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SchoolAddress") = value
        End Set
    End Property

    Public Property SchoolDistrict() As String
        Get
            If System.Web.HttpContext.Current.Session("SchoolDistrict") = Nothing Then
                System.Web.HttpContext.Current.Session("SchoolDistrict") = ""
            End If
            Return System.Web.HttpContext.Current.Session("SchoolDistrict")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("SchoolDistrict") = value
        End Set
    End Property


    Public Sub InitSchoolSessionInfo()

        Dim dr As DataRow = GetPSISchoolDetailsDataRow(Frame_N_)
        Dim gradecount As Integer = 0

        Me.GradeId4 = IIf(dr("id4") Is DBNull.Value, "", dr("id4"))
        Me.GradeId8 = IIf(dr("id8") Is DBNull.Value, "", dr("id8"))
        Me.GradeId12 = IIf(dr("id12") Is DBNull.Value, "", dr("id12"))
        Me.SchoolName = IIf(dr("s_name") Is DBNull.Value, "", dr("s_name"))

        Me.SCHEDATE4 = IIf(dr("SCHEDATE_4") Is DBNull.Value, "", dr("SCHEDATE_4"))
        Me.SCHEDATE8 = IIf(dr("SCHEDATE_8") Is DBNull.Value, "", dr("SCHEDATE_8"))
        Me.SCHEDATE12 = IIf(dr("SCHEDATE_12") Is DBNull.Value, "", dr("SCHEDATE_12"))
        Me.SCHEDATE4_2 = IIf(dr("SCHEDATE2_4") Is DBNull.Value, "", dr("SCHEDATE2_4"))
        Me.SCHEDATE8_2 = IIf(dr("SCHEDATE2_8") Is DBNull.Value, "", dr("SCHEDATE2_8"))

        Me.ProjectID = IIf(dr("fldProjectID") Is DBNull.Value, "", dr("fldProjectID"))

        Me.SchoolDistrict = IIf(dr("D_Name") Is DBNull.Value, "", dr("D_Name"))
        Me.SchoolAddress = IIf(dr("s_addr1") Is DBNull.Value, "", dr("s_addr1")) & ", " & IIf(dr("s_city") Is DBNull.Value, "", dr("s_city")) & ", " & IIf(dr("s_state") Is DBNull.Value, "", dr("s_state"))
    End Sub

    Public Function SchoolBannerText() As String
        Dim result As String
        result = Me.SchoolName()


        If HasGradeId4 Then
            result = result & "&nbsp;&nbsp;-&nbsp;&nbsp;" & Me.GradeId4 & "&nbsp;&nbsp;-&nbsp;&nbsp; Grade 4 Assessment Date: " & Me.SCHEDATE4 & " - " & Me.SCHEDATE4_2
        End If

        If HasGradeId8 Then
            If iseTIMSS(Me.GradeId8) = True Then
                result = result & "&nbsp;&nbsp;-&nbsp;&nbsp;" & Me.GradeId8 & "&nbsp;&nbsp;-&nbsp;&nbsp; Grade 8 Assessment Date: " & Me.SCHEDATE8 & " - " & Me.SCHEDATE8_2

            Else
                result = result & "&nbsp;&nbsp;-&nbsp;&nbsp;" & Me.GradeId8 & "&nbsp;&nbsp;-&nbsp;&nbsp; Grade 8 Assessment Date: " & Me.SCHEDATE8
            End If
        End If

        If HasGradeId12 Then
            result = result & "&nbsp;&nbsp;-&nbsp;&nbsp;" & Me.GradeId12 & "&nbsp;&nbsp;-&nbsp;&nbsp; Grade 12 Assessment Date: " & Me.SCHEDATE12
        End If


        ' result = result & "&nbsp;&nbsp;-&nbsp;&nbsp" & Me.SchoolDistrict


        'result = result & IIf(IsMyTIMSSUser() = False, "<br /><br />", "")
        Return result
    End Function

    Public Function GetTUA_LEANameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetTUA_LEANameValuePairList()
    End Function

    Public Shared ReadOnly Property IsTudaCoordinator() As Boolean
        Get
            Return Roles.IsUserInRole("Tuda Coordinator")
        End Get
    End Property

    Public Function GetAdvancedEligibilityNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetAdvancedEligibilityNameValuePairList()
    End Function

    Public Function GetAdvancedEligibilityDisplayText(value As Object) As String
        Dim lst As List(Of NameValuePair) = GetAdvancedEligibilityNameValuePairList()
        Dim result As String = ""
        If Not value Is DBNull.Value Then
            For Each nvp As NameValuePair In lst
                If nvp.Value.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase) Then
                    result = nvp.Name
                    Exit For
                End If
            Next
        End If
        Return result
    End Function

    Public Function FormatTextForHtmlDisplay(value As Object) As String
        Dim lst As List(Of NameValuePair) = GetAdvancedEligibilityNameValuePairList()
        Dim result As String = ""
        If Not value Is DBNull.Value Then
            result = value.ToString().Replace(vbCrLf, "<br />")
        End If
        Return result
    End Function
    Public Function GetClassListingForm(id As String) As DataTable
        Return mTIMSSDal.GetClassListingForm(id)
    End Function

    Public Function GetTIMSSG4IncompletePSI() As DataTable
        Return mTIMSSDal.GetTIMSSG4IncompletePSI()
    End Function
    Public Function GetTIMSSG8IncompletePSI() As DataTable
        Return mTIMSSDal.GetTIMSSG8IncompletePSI()
    End Function

    'Public Function GetScienceTeacherListingForm(id As String) As DataTable
    '    Return mTIMSSDal.GetScienceTeacherListingForm(id)
    'End Function

    Public Function GetTeacherNameListingForm(id As String) As DataTable
        Return mTIMSSDal.GetTeacherNameListingForm(id)
    End Function

    Public Function GetClassListingFormNameValuePairList(id As String) As List(Of NameValuePair)
        Return mTIMSSDal.GetClassListingFormNameValuePairList(id)
    End Function

    Public Function GetClassExclusionStatusText(id As Object) As String
        Dim result As String = ""
        If id IsNot DBNull.Value Then
            Dim lst As List(Of NameValuePair) = Me.GetClassExclusionStatusNameValuePairList()
            For Each nvp As NameValuePair In lst
                If nvp.Value.Equals(id.ToString(), StringComparison.CurrentCultureIgnoreCase) Then
                    result = nvp.Name
                    Exit For
                End If
            Next
        End If
        Return result
    End Function
    Public Function GetClassExclusionStatusNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetClassExclusionStatusNameValuePairList()
    End Function

    Public Function GetClassGroupNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetClassGroupNameValuePairList()
    End Function

    Public Function GetClassGroupText(id As Object) As String
        Dim result As String = ""
        If id IsNot DBNull.Value Then
            Dim lst As List(Of NameValuePair) = Me.GetClassGroupNameValuePairList()
            For Each nvp As NameValuePair In lst
                If nvp.Value.Equals(id.ToString(), StringComparison.CurrentCultureIgnoreCase) Then
                    result = nvp.Name
                    Exit For
                End If
            Next
        End If
        Return result
    End Function
    Public Sub DeleteClassListingFormItem(compositeid As String)
        Dim params As String() = compositeid.Split("|")
        Dim id As String = params(0)
        Dim ClassListingFormId As Integer = params(1)
        mTIMSSDal.DeleteClassListingFormItem(id, ClassListingFormId)
    End Sub

    Public Sub SaveClassListingFormItemEditChanges(controls As ControlCollection, ClassListingFormId As Integer)
        'Dim ClassListingFormId As HiddenField = controls.FindControl("ClassListingFormId")
        Dim args As New SaveChangesArgsBase(controls, ClassListingFormId)
        mTIMSSDal.HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblClassListingForm", "ClassListingFormId")
    End Sub


    Public Sub SaveClassListingFormItemInsert(parentcontrols As ControlCollection, prefix As String)
        Dim args As New SaveChangesArgsBase(parentcontrols, "", prefix)
        mTIMSSDal.HandleTableInsert(args.DbControls, "tblClassListingForm", False)

        For Each cntrl As Control In args.DbControls
            If cntrl.GetType() Is GetType(DropDownList) Then
                Dim d As DropDownList = cntrl
                d.SelectedIndex = -1
            ElseIf cntrl.GetType() Is GetType(HiddenField) Then
                Dim d As HiddenField = cntrl
                d.Value = ""
            Else
                Dim d As TextBox = cntrl
                d.Text = ""
            End If
        Next

    End Sub

    Public Sub DeleteScienceTeacherListingFormItem(compositeid As String)
        Dim params As String() = compositeid.Split("|")
        Dim id As String = params(0)
        Dim ClassListingFormId As Integer = params(1)
        mTIMSSDal.DeleteScienceTeacherListingFormItem(id, ClassListingFormId)
    End Sub
    Public Sub SaveScienceTeacherListingFormItemEditChanges(controls As ControlCollection, ScienceTeacherListingFormId As Integer)
        Dim args As New SaveChangesArgsBase(controls, ScienceTeacherListingFormId)
        mTIMSSDal.HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblScienceTeacherListingForm", "ScienceTeacherListingFormId")
    End Sub


    Public Sub SaveScienceTeacherListingFormItemInsert(parentcontrols As ControlCollection, prefix As String)
        Dim args As New SaveChangesArgsBase(parentcontrols, "", prefix)
        mTIMSSDal.HandleTableInsert(args.DbControls, "tblScienceTeacherListingForm", False)

        For Each cntrl As Control In args.DbControls
            If cntrl.GetType() Is GetType(DropDownList) Then
                Dim d As DropDownList = cntrl
                d.SelectedIndex = -1
            ElseIf cntrl.GetType() Is GetType(HiddenField) Then
                Dim d As HiddenField = cntrl
                d.Value = ""
            Else
                Dim d As TextBox = cntrl
                d.Text = ""
            End If
        Next

    End Sub

    Public Sub SaveClassListingFinished(id As String)

        Dim nvp As New List(Of NameValuePair)
        nvp.Add(New NameValuePair("ClassListSubmited", "getdate()"))
        nvp.Add(New NameValuePair("ClassListSubmitedBy", mUser.ProviderUserKey().ToString()))
        mTIMSSDal.HandleTableUpdate(nvp, id, "tblSCSGrade", "id")

    End Sub

    Public Function ProcessfileUploadForGrade(upload As FileUpload, id As String, frame_n_ As String, grade As Integer, projectid As Integer, HasColumnHeader As String, ByRef fileid As Integer, efiletypeid As Integer)
        Dim nvp As New List(Of NameValuePair)
        If upload.HasFile Then

            Dim dt As DateTime = DateTime.Now


            Dim extension As String = upload.PostedFile.FileName.Substring(upload.PostedFile.FileName.LastIndexOf(".") + 1)
            Dim basefilename As String = "Doc_" & Me.Frame_N_ & "_" & id
            Dim ServerRelativePath As String = "efile"
            Dim ServerFilePath As String = System.Web.HttpContext.Current.Server.MapPath("~/output/" & ServerRelativePath)
            Dim tempfilename As String = ServerFilePath & "\" & basefilename & "." & extension

            If (Not System.IO.Directory.Exists(ServerFilePath)) Then
                System.IO.Directory.CreateDirectory(ServerFilePath)
            End If

            Dim cnt As Integer = 1
            While System.IO.File.Exists(tempfilename)
                tempfilename = ServerFilePath & "\" & basefilename & "(" & cnt & ")" & "." & extension
                cnt = cnt + 1
            End While

            ' & "/" & filename
            nvp.Add(New NameValuePair("id", id))
            nvp.Add(New NameValuePair("SmpGrd", grade))
            nvp.Add(New NameValuePair("frame_n_", frame_n_))
            nvp.Add(New NameValuePair("fldProjectID", projectid))
            nvp.Add(New NameValuePair("UploadedBy", Me.mUser.ProviderUserKey.ToString()))
            nvp.Add(New NameValuePair("UserFilePath", upload.FileName))
            nvp.Add(New NameValuePair("Filesize", upload.PostedFile.ContentLength))
            nvp.Add(New NameValuePair("ContentType", upload.PostedFile.ContentType))
            nvp.Add(New NameValuePair("UploadDT", "getdate()"))
            nvp.Add(New NameValuePair("EfileTypeId", efiletypeid))
            'nvp.Add(New NameValuePair("ServerRelativePath", ServerRelativePath))

            nvp.Add(New NameValuePair(upload.FileBytes(), "FileData"))

            Try

                'save file details and raw bytes to database
                fileid = mTIMSSDal.HandleTableInsert(nvp, "tblEfileUploads", True)

                'save the temporarily
                upload.SaveAs(tempfilename)

                SetStatusCode(fileid, "File Uploaded", EFileStatusType.EFile, False)

                If grade <> 12 Then
                    ImportData(DateTime.Now, IIf(extension.ToLower().Equals("xlsx"), "Excel 12.0", "Excel 8.0"), tempfilename, HasColumnHeader, fileid, id, frame_n_, grade, projectid)
                End If

            Catch ex As Exception
                Throw ex
            Finally
                'always  delete file even if there is an error
                If System.IO.File.Exists(tempfilename) Then
                    System.IO.File.Delete(tempfilename)
                End If
            End Try


            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetEfileUploadsByGradeId(id As String, EfileTypeId As Integer) As DataTable
        Return mTIMSSDal.GetEfileUploadsByGradeId(id, EfileTypeId)
    End Function

    Public Function ProcessFile(ServerFilePath As String, fileid As Integer, id As String, frame_n_ As String, grade As Integer, projectid As Integer) As Boolean
        Dim result = False
        Dim nvp As New List(Of NameValuePair)
        Dim firstrow As Integer = 2
        Try
            Using wb As New XLWorkbook(ServerFilePath)
                'For Each ws2 In wb.Worksheets
                'System.Web.HttpContext.Current.Response.Write(ws2.Name & "<br />")
                'Next
                Dim rowindex As Integer = 1
                Dim cellindex As Integer = 1
                Dim ws As IXLWorksheet = wb.Worksheets(0)
                'wb.Worksheets(0).f()

                Dim firstrowobject As IXLRow = ws.Row(firstrow - 1)
                cellindex = 0

                For Each cell In firstrowobject.Cells()
                    nvp.Clear()
                    nvp.Add(New NameValuePair("fileid", fileid))
                    nvp.Add(New NameValuePair("ColumnSeq", cellindex))
                    nvp.Add(New NameValuePair("UserColumnLabel", IIf(String.IsNullOrEmpty(cell.Value), "C" & cellindex, cell.Value)))

                    mTIMSSDal.HandleTableInsert(nvp, "tblEfileUserColumns", False)

                    cellindex = cellindex + 1
                Next

                For Each row In ws.Rows(firstrow, ws.LastRowUsed().RowNumber())
                    cellindex = 0

                    nvp.Clear()

                    nvp.Add(New NameValuePair("fileid", fileid))
                    nvp.Add(New NameValuePair("row", rowindex - 1))
                    nvp.Add(New NameValuePair("id", id))
                    nvp.Add(New NameValuePair("SmpGrd", grade))
                    nvp.Add(New NameValuePair("frame_n_", frame_n_))
                    nvp.Add(New NameValuePair("fldProjectID", projectid))

                    For Each cell In row.Cells()
                        'cell.Value
                        'System.Web.HttpContext.Current.Response.Write("Row " & rowindex & ", Cell " & cellindex & ", value = " & cell.Value & "<br />")
                        nvp.Add(New NameValuePair("C" & cellindex, cell.Value))
                        cellindex = cellindex + 1
                    Next

                    mTIMSSDal.HandleTableInsert(nvp, "tblEfileStudentData", False)

                    rowindex = rowindex + 1
                Next
                result = True
            End Using
        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<font color='red'><b>" & ex.Message & "</b></font>")
            result = False
        End Try

        Return result
    End Function

    Private Sub ConnectToExcel(ByRef oledbConn As System.Data.OleDb.OleDbConnection, ExcelVersion As String, ServerFilePath As String, HasColumnHeader As String)

        Dim connectionstring As String = String.Empty

        If ExcelVersion = "Excel 12.0" Then

            connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ServerFilePath & _
               ";Extended Properties='Excel 12.0;HDR=" & HasColumnHeader & ";IMEX=1';"
        Else
            connectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ServerFilePath & _
             ";Extended Properties='Excel 8.0;HDR=" & HasColumnHeader & ";IMEX=1';"
        End If

        oledbConn = New System.Data.OleDb.OleDbConnection(connectionstring)
        oledbConn.Open()

    End Sub


    Private Function GetTable(ByRef oledbConn As System.Data.OleDb.OleDbConnection, ByRef ExcelObjectWorkSheetName As String) As Boolean

        Dim dt As System.Data.DataTable = Nothing

        Try


            dt = oledbConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, Nothing)

            ExcelObjectWorkSheetName = dt.Rows(0)("TABLE_NAME").ToString

        Catch ex As Exception

            Return False

        Finally

            'Clean up.

            If dt IsNot Nothing Then
                dt.Dispose()
            End If

        End Try

        Return True

    End Function

    Protected Function EFileRegExpPattern() As String

        Dim regexppattern As String = String.Empty
        Dim regexpindex As Integer

        ' None printable characters

        For regexpindex = 1 To 31
            If Len(regexppattern) > 0 Then
                regexppattern = regexppattern & "|"
            End If
            regexppattern = regexppattern & Chr(regexpindex)
        Next


        ' # sign

        regexppattern = regexppattern & "|"
        regexppattern = regexppattern & Chr(35)

        ' Less than sign

        regexppattern = regexppattern & "|"
        regexppattern = regexppattern & Chr(60)


        ' Greater than sign

        regexppattern = regexppattern & "|"
        regexppattern = regexppattern & Chr(62)

        ' E-File Tables

        regexppattern = regexppattern & "|" & _
        "tblEfileAccessLog|tblEfileCleanedStudentData|tblEfileDistricts|tblEfileGrades|tblEfileId|tblEfileNaepCodes|tblEfileNaepLabels|tblEfileResponseFreq|tblEfileSchools|tblEfileStates|tblEfileStatusLog|tblEfileStudentData_Deleted|tblEfileStudentData_Orphaned|tblEfileStudentData|tblEfileUploads|tblEfileUserColumns|tblEfileWarnings"

        Return regexppattern

    End Function

    Public Function CleanString(ByVal value As String, fileid As Integer, ByRef HasFilteredData As Boolean, Optional ByVal falgdatabase As Boolean = True, Optional ByVal isschoolidcolumn As Boolean = False) As String

        Dim tmpstr As String = String.Empty

        If String.IsNullOrEmpty(value) Then
            Return String.Empty
            Exit Function
        End If

        'begin transaction
        Dim ret As Boolean = False
        Dim starttransaction As Boolean = False

        'if connection object is null, then start its own transaction

        Try


            Dim pattern As String = EFileRegExpPattern()



            If isschoolidcolumn Then
                pattern = EFileRegExpPattern() & "|" & Chr(160)
            End If

            Dim regex As New Regex(pattern, RegexOptions.IgnoreCase)

            If falgdatabase AndAlso Not HasFilteredData AndAlso regex.IsMatch(value) Then
                Dim nvp As New List(Of NameValuePair)
                nvp.Add(New NameValuePair("HasFilteredData", 1))
                mTIMSSDal.HandleTableUpdate(nvp, fileid, "tblEfileUploads", "fileid")
                HasFilteredData = True
            End If

            tmpstr = Trim(regex.Replace(value, ""))

            regex = New Regex("\s{2,}", RegexOptions.IgnoreCase)
            tmpstr = Trim(regex.Replace(value, " "))

        Catch ex As Exception
            'catch exception other than validationexception, and will be catch by global exception handler.
            Throw New Exception(ex.Message)

        Finally
            'if this method start transaction, close the connection

        End Try

        Return tmpstr

    End Function
    Private Function ImportFileColumns(ByVal dt As DataTable, fileid As Integer, ByRef HasFilteredData As Boolean) As List(Of Integer)

        Dim totalfieldscount = dt.Columns.Count
        Dim usercoumnids As New List(Of Integer)
        Dim nvp As New List(Of NameValuePair)
        If totalfieldscount > 100 Then
            totalfieldscount = 100
        End If

        'begin transaction
        Dim ret As Boolean = False
        Dim starttransaction As Boolean = False


        Try
            Dim tmpname As String = String.Empty

            usercoumnids = New List(Of Integer)

            Dim totalblankcolumnheader As Integer = 0

            For i As Integer = 0 To totalfieldscount - 1
                Dim usercolumnid As Integer

                tmpname = CleanString(dt.Columns(i).ColumnName, fileid, HasFilteredData, , )

                If LCase(Left(tmpname, 1)) = "f" AndAlso IsNumeric(Mid(tmpname, 2)) Then
                    tmpname = "Column " & Mid(tmpname, 2) & ": No column header"
                    totalblankcolumnheader = totalblankcolumnheader + 1
                End If

                'usercolumnid = SiteProvider.EFile.AddUserColumn(FileID, i, tmpname, conn, transaction)

                nvp.Clear()
                nvp.Add(New NameValuePair("fileid", fileid))
                nvp.Add(New NameValuePair("ColumnSeq", i))
                nvp.Add(New NameValuePair("UserColumnLabel", tmpname))

                usercolumnid = mTIMSSDal.HandleTableInsert(nvp, "tblEfileUserColumns", True)

                usercoumnids.Add(usercolumnid)

                If totalblankcolumnheader > 15 Then
                    Throw New Exception("toomanyblankcolumns")
                    Exit For
                End If

            Next


        Catch ex As Exception
            'catch exception other than validationexception, and will be catch by global exception handler.
            Throw New Exception(ex.Message)

        Finally

        End Try

        Return usercoumnids
    End Function
    Private Function ProcessingExcel(ByRef oledbConn As System.Data.OleDb.OleDbConnection, ByRef usercolumnids As List(Of Integer), HasColumnHeader As String, fileid As Integer, id As String, frame_n_ As String, grade As Integer, projectid As Integer) As Boolean

        Dim table As String = String.Empty
        Dim totalrows As Integer = 0
        'Dim blnrownotempty As Boolean
        'Dim mastrowcount As Integer = 0

        Dim ds As DataSet = Nothing
        Dim dt As DataTable = Nothing

        Dim ret As Boolean = False
        Dim ExpectedRows As Integer
        Dim TotalColumns As Integer
        Dim RowCount As Integer
        Dim TotalSchools As Integer
        Dim HasFilteredData As Boolean
        'get worksheet table name
        Dim ExcelObjectWorkSheetName As String = Nothing
        Try

            GetTable(oledbConn, ExcelObjectWorkSheetName)

            'SetStatusCode("Selecting worksheet (" & Me.ExcelObjectWorkSheetName & ")", 1)

            table = Replace(ExcelObjectWorkSheetName, "''", "'")

            ' Create OleDbCommand object and select data from worksheet Sheet1
            Dim olecmd As System.Data.OleDb.OleDbCommand = _
             New System.Data.OleDb.OleDbCommand("SELECT * FROM [" & table & "]", oledbConn)

            ' Create new OleDbDataAdapter
            Dim oleda As System.Data.OleDb.OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter()

            oleda.SelectCommand = olecmd

            ' Create a DataSet which will hold the data extracted from the worksheet.
            ds = New DataSet()

            ' Fill the DataSet from the data extracted from the worksheet.
            oleda.Fill(ds, "StudentLists")

            dt = ds.Tables(0)



            usercolumnids = ImportFileColumns(dt, fileid, HasFilteredData)


            ExpectedRows = dt.Rows.Count

            'SetExpectedRows()

            TotalColumns = usercolumnids.Count

            RowCount = 0 'total row import
            TotalSchools = 0

            'If Not IsSync Then
            '    Return True
            '    GoTo UpdatetTotals
            'End If


            TotalSchools = 1

            Dim nvp As New List(Of NameValuePair)
            nvp.Add(New NameValuePair("HasColumnHeader", HasColumnHeader))
            nvp.Add(New NameValuePair("TotalColumns", TotalColumns))
            nvp.Add(New NameValuePair("ExpectedRows", ExpectedRows))
            nvp.Add(New NameValuePair("TableObject", table))
            nvp.Add(New NameValuePair("ExcelObjectWorksheetName", table))
            nvp.Add(New NameValuePair("IsSynchronous", 1))
            nvp.Add(New NameValuePair("TotalGrades", 1))
            mTIMSSDal.HandleTableUpdate(nvp, fileid, "tblEfileUploads", "fileid")

            CopyStudentRecord(ds.Tables(0), HasColumnHeader, fileid, id, frame_n_, grade, projectid, HasFilteredData)



UpdatetTotals:
            'UpdateTotals()

            ret = True

            'If IsSync Then

            '    If RowCount = 0 Then

            '        SetStatusCode("Upload/Import Process Failed: No readable data found!", 1)
            '        Throw New ValidationException("nodata")
            '        ret = False
            '    End If

            'End If



        Catch ex As Exception

            Throw ex

            ret = False

        Finally


            If oledbConn IsNot Nothing Then
                oledbConn.Close()
                oledbConn.Dispose()
            End If


        End Try

        Return ret


    End Function


    'upload efile excel file to the server
    Public Function ImportData(ByVal startdt As Date, ExcelVersion As String, ServerFilePath As String, HasColumnHeader As String, fileid As Integer, id As String, frame_n_ As String, grade As Integer, projectid As Integer) As Boolean

        Dim ret As Boolean
        Dim oledbConn As System.Data.OleDb.OleDbConnection = Nothing
        Dim usercolumnids As List(Of Integer) = Nothing

        Try

            SetStatusCode(fileid, "Data Import Pending", EFileStatusType.EFile, False)

            'ProcessingLog("ConnectToExcelFile", "start")

            ConnectToExcel(oledbConn, ExcelVersion, ServerFilePath, HasColumnHeader)

            'ProcessingLog("ConnectToExcelFile", "end")

            SetStatusCode(fileid, "Connection to excel file complete", EFileStatusType.EFile, False)

            'ProcessingLog("ProcessExcel", "start")

            ProcessingExcel(oledbConn, usercolumnids, HasColumnHeader, fileid, id, frame_n_, grade, projectid)

            'ProcessingLog("ProcessExcel", "End")



            SetStatusCode(fileid, "Data Import Successful", EFileStatusType.EFile, False)

            SetStatusCode(fileid, "Response Rate Insert Pending", EFileStatusType.EFile, False)

            mTIMSSDal.EfileImportProcessCalculateResponseFreq(usercolumnids, grade, fileid)

            SetStatusCode(fileid, "Response Rate Insert Completed", EFileStatusType.EFile, False)

            SetStatusCode(fileid, "Column Linking Pending", EFileStatusType.EFile, False)


            Dim nvp As New List(Of NameValuePair)
            nvp.Add(New NameValuePair("ProcessingTime", DateDiff(DateInterval.Second, startdt, Now)))
            nvp.Add(New NameValuePair("ProcessedDT", "getdate()"))
            mTIMSSDal.HandleTableUpdate(nvp, fileid, "tblEfileUploads", "fileid")

            'If IsSync Then


            '    SetStatusCode("Response Rate Insert Pending", 0)

            '    ProcessingLog("CalculateResponsFreqs", "start")

            '    CalculateResponsFreqs(usercolumnids)

            '    ProcessingLog("CalculateResponsFreqs", "end")

            '    SetStatusCode("Response Rate Insert Completed", 0)

            '    ProcessingLog("FlagOnlineProcessingComplete", "start")

            '    ImportProcessComplete(startdt)

            '    ProcessingLog("FlagOnlineProcessingComplete", "end")

            '    SetStatusCode("Column Linking Pending", 0)

            '    ProcessingLog("end", "")

            'Else

            '    ProcessingLog("FlagOnlineProcessingComplete", "start")

            '    ImportProcessComplete(startdt)

            '    ProcessingLog("FlagOnlineProcessingComplete", "end")

            '    SetStatusCode("Column Linking Pending", 0)

            'End If

        Catch ex As Exception

            ret = False
            Throw ex

        Finally

            If oledbConn IsNot Nothing Then
                oledbConn.Close()
                oledbConn.Dispose()
            End If

        End Try

        Return ret

    End Function


    Private Function CopyStudentRecord(ByRef dt As DataTable, HasColumnHeader As String, fileid As Integer, id As String, frame_n_ As String, grade As Integer, projectid As Integer, ByRef HasFilteredData As Boolean) As Boolean

        Dim mastrowcount As Integer
        Dim datalist As List(Of String) = Nothing
        Dim blnrownotempty As Boolean = False
        Dim data As String = String.Empty
        Dim totalrows As Integer
        Dim orphanids As List(Of String) = Nothing
        Dim schoolidcolumnseq As Integer = 0
        Dim totalcolumns As Integer = 0
        Dim nvp As New List(Of NameValuePair)


        If HasColumnHeader = "Yes" Then
            mastrowcount = 1
        End If

        'begin transaction
        Dim ret As Boolean = False
        Dim starttransaction As Boolean = False


        Try
            Dim tmpname As String = String.Empty

            For Each row As DataRow In dt.Rows

                'increment master row count
                mastrowcount = mastrowcount + 1

                'initialize row to not empty
                blnrownotempty = False

                datalist = New List(Of String)

                Dim intcolumncount As Integer = 0

                nvp.Clear()

                nvp.Add(New NameValuePair("fileid", fileid))
                nvp.Add(New NameValuePair("row", mastrowcount))
                nvp.Add(New NameValuePair("id", id))
                nvp.Add(New NameValuePair("SmpGrd", grade))
                nvp.Add(New NameValuePair("frame_n_", frame_n_))
                nvp.Add(New NameValuePair("fldProjectID", projectid))


                For Each column As DataColumn In dt.Columns

                    If intcolumncount > 99 Then
                        Exit For
                    End If

                    data = CleanString(row(column.ColumnName).ToString, fileid, HasFilteredData, , )

                    If Not String.IsNullOrEmpty(data) AndAlso Len(data) > 0 Then
                        blnrownotempty = True
                    End If
                    datalist.Add(data)

                    nvp.Add(New NameValuePair("C" & intcolumncount, data))
                    intcolumncount = intcolumncount + 1

                Next

                If blnrownotempty Then
                    totalrows = totalrows + 1
                    'ret = SiteProvider.EFile.CopyStudentRecord(fileid, School.SchoolID, mastrowcount, datalist, False, conn, transaction)
                    mTIMSSDal.HandleTableInsert(nvp, "tblEfileStudentData", False)
                End If


            Next


            nvp = New List(Of NameValuePair)
            nvp.Add(New NameValuePair("TotalRows", totalrows))

            mTIMSSDal.HandleTableUpdate(nvp, fileid, "tblEfileUploads", "fileid")

            'RowCount = totalrows

        Catch ex As Exception
            'catch exception other than validationexception, and will be catch by global exception handler.
            Throw New Exception(ex.Message)

        Finally
        End Try

        Return ret

    End Function

    Public Function GetEfileUserColumns(fileid As Integer) As DataTable
        Return mTIMSSDal.GetEfileUserColumns(fileid)
    End Function

    Public Function GetNaepColumnLabels(file As File) As List(Of NameValuePair)
        Return mTIMSSDal.GetNaepColumnLabels(file)
    End Function

    Public Function GetNaepCodes(NaepLabelId As Integer) As List(Of NameValuePair)
        Return mTIMSSDal.GetNaepCodes(NaepLabelId)
    End Function
    Public Function GetEfileResponseFreqs(UserColumnId As Integer) As DataTable
        Return mTIMSSDal.GetEfileResponseFreqs(UserColumnId)
    End Function

    Public Sub SaveEfileColumnMappingEditChanges(controls As ControlCollection, UserColumnId As Integer)
        Dim args As New SaveChangesArgsBase(controls, UserColumnId)
        mTIMSSDal.HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblEfileUserColumns", "UserColumnId")

        Dim nvp As New List(Of NameValuePair)
        nvp.Add(New NameValuePair("NaepCodeId", Nothing))
        nvp.Add(New NameValuePair("ClassListingFormId", Nothing))
        mTIMSSDal.HandleTableUpdate(nvp, UserColumnId, "tblEfileResponseFreq", "UserColumnId")
    End Sub

    Public Function GetEfileColumnsWithValuesToMap(FileId As Integer) As DataTable
        Return mTIMSSDal.GetEfileColumnsWithValuesToMap(FileId)
    End Function


    Public Sub SaveEfileValueMappingEditChanges(controls As ControlCollection, ResponseFreqId As Integer, prefix As String)
        Dim args As New SaveChangesArgsBase(controls, ResponseFreqId, prefix)
        mTIMSSDal.HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblEfileResponseFreq", "ResponseFreqId")

    End Sub

    Public Function GetFinalResponseFreqs(fileid As Integer) As DataTable
        Return mTIMSSDal.GetFinalResponseFreqs(fileid)
    End Function

    Public Function GetEfileDetailsDataRow(fileid As Integer) As DataRow
        Return mTIMSSDal.GetEfileDetailsDataRow(fileid)
    End Function


    Public Function CalcPercentage(ByVal Numer As Integer, ByVal Denom As Integer, ByVal Digits As Integer) As Double

        Dim result As Double

        If CDbl(Denom) > 0 Then
            If CDbl(Digits) > -1 Then
                result = Math.Round((CDbl(Numer) / CDbl(Denom)) * 100, Digits)
            Else
                result = (CDbl(Numer) / CDbl(Denom)) * 100
            End If
        Else
            result = 0
        End If

        Return result

    End Function


    Public Function GetEfileDetailsDataTable(args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        'args.FilterParameters.Add(New FilterParameter("ApplicationName", Membership.ApplicationName, "equals"))
        Return mTIMSSDal.GetEfileDetailsDataTable(args)
    End Function


    Public Function SetStatusCode(fileid As Integer, ByVal status As String, statustype As EFileStatusType, ByVal iserror As Boolean) As Integer
        Dim result As Boolean = False

        Dim nvp As New List(Of NameValuePair)

        nvp.Add(New NameValuePair("fileid", fileid))
        If status.Length > 3999 Then
            status = status.Substring(0, 3999)
        End If
        nvp.Add(New NameValuePair("status", status))
        nvp.Add(New NameValuePair("StatusType", IIf(statustype = EFileStatusType.EFile, "E-File", statustype.ToString)))
        nvp.Add(New NameValuePair("UID", mUser.ProviderUserKey.ToString()))
        nvp.Add(New NameValuePair("Firstname", mProf.FirstName))
        nvp.Add(New NameValuePair("Lastname", mProf.LastName))
        nvp.Add(New NameValuePair("IsError", IIf(iserror, 1, 0)))
        nvp.Add(New NameValuePair("StatusEditDT", "getdate()"))

        Return mTIMSSDal.HandleTableInsert(nvp, "tblEfileStatusLog", False)

    End Function


    Public Sub UserVerification(file As File, VerificationComments As String, isuserverified As Boolean)
        Dim result As Boolean = False

        If isuserverified Then

            'blnsamplemeth = True
            'blnreset = False

            SetStatusCode(file.FileId, "Verified By User", EFileStatusType.EFile, 0)
            SetStatusCode(file.FileId, "Ready For Parsing", EFileStatusType.DP, 0)


        Else
            'blnsamplemeth = False
            'blnreset = True

            SetStatusCode(file.FileId, "Verified as INCORRECT", EFileStatusType.EFile, 1)
            SetStatusCode(file.FileId, "No Parsing needed", EFileStatusType.DP, 0)


        End If

        Dim nvp As New List(Of NameValuePair)
        nvp.Add(New NameValuePair("VerificationDT", "getdate()"))
        nvp.Add(New NameValuePair("VerificationComments", VerificationComments))
        nvp.Add(New NameValuePair("HasVerificationComments", IIf(String.IsNullOrEmpty(VerificationComments), 0, 1)))
        mTIMSSDal.HandleTableUpdate(nvp, file.FileId, "tblEfileUploads", "fileid")


        nvp.Clear()
        nvp.Add(New NameValuePair("SLF_CompDate", "getdate()"))
        nvp.Add(New NameValuePair("SLF_DateReceived", file.UploadDT))
        nvp.Add(New NameValuePair("SLF_Name", mProf.LastName & ", " & mProf.FirstName))
        'nvp.Add(New NameValuePair("SLF_Phone", Nothing))
        nvp.Add(New NameValuePair("SLF_email", mUser.Email))
        'nvp.Add(New NameValuePair("SLF_DateCurrent", ""))
        nvp.Add(New NameValuePair("SLF_Receivedby", "WESTAT"))
        nvp.Add(New NameValuePair("SampMeth", 3))

        mTIMSSDal.HandleTableUpdate(nvp, file.ID, "tblSCSGrade", "id")


        SendUserConfirmationEmail(file, isuserverified)

    End Sub


    Private Sub SendUserConfirmationEmail(thefile As File, ByVal isuserveryified As Boolean)

        Dim msg As String = String.Empty


        msg = _
         "<div align=""center""><center><table border=""2"" cellpadding=""0"" cellspacing=""1"" width=""95%"">" & vbCrLf & _
         "   <tr class=""selectcolbyresp"">" & vbCrLf & _
         "       <td colspan=""2"" align=""center"">" & vbCrLf & _
         "           Verified File (" & System.Web.HttpContext.Current.Request.ServerVariables("HTTP_HOST") & ")" & vbCrLf & _
         "       </td>" & vbCrLf & _
         "   </tr>" & vbCrLf & _
         "   <tr class=""default"">" & vbCrLf & _
         "       <td width=""20%"">" & vbCrLf & _
         "               <b>Date/Time</b>" & vbCrLf & _
         "       </td>" & vbCrLf & _
         "       <td width=""80%"">" & vbCrLf & _
         "           " & Now() & vbCrLf & _
         "       </td>" & vbCrLf & _
         "   </tr>" & vbCrLf & _
         "   <tr class=""default"">" & vbCrLf & _
         "       <td>" & vbCrLf & _
         "               <b>Verified By</b>" & vbCrLf & _
         "       </td>" & vbCrLf & _
         "       <td>" & vbCrLf & _
         "           " & mUser.UserName & vbCrLf & _
         "       </td>" & vbCrLf & _
         "   </tr>" & vbCrLf

        msg = msg & _
        "   <tr class=""default"">" & vbCrLf & _
        "       <td>" & vbCrLf & _
        "               <b>Email</b>" & vbCrLf & _
        "       </td>" & vbCrLf & _
        "       <td>" & vbCrLf & _
        "           " & mUser.Email & vbCrLf & _
        "       </td>" & vbCrLf & _
        "   </tr>" & vbCrLf


        'msg = msg & _
        '"   <tr class=""default"">" & vbCrLf & _
        '"       <td>" & vbCrLf & _
        '"               <b>State</b>" & vbCrLf & _
        '"       </td>" & vbCrLf & _
        '"       <td>" & vbCrLf & _
        '"           " & thefile.StateName & " (" & thefile.FIPSST & ")" & vbCrLf & _
        '"       </td>" & vbCrLf & _
        '"   </tr>" & vbCrLf

        'If Not thefile.EFileType = EFileType.State Then

        '    msg = msg & _
        '    "   <tr class=""default"">" & vbCrLf & _
        '    "       <td>" & vbCrLf & _
        '    "               <b>District</b>" & vbCrLf & _
        '    "       </td>" & vbCrLf & _
        '    "       <td>" & vbCrLf & _
        '    "           " & thefile.DistrictName & " (" & thefile.LEAID & ")" & vbCrLf & _
        '    "       </td>" & vbCrLf & _
        '    "   </tr>" & vbCrLf

        'End If
        If thefile.IsSingleGradeFile Then

            msg = msg & _
            "   <tr class=""default"">" & vbCrLf & _
            "       <td>" & vbCrLf & _
            "               <b>Schoolname</b>" & vbCrLf & _
            "       </td>" & vbCrLf & _
            "       <td>" & vbCrLf & _
            "           " & thefile.SchoolName & vbCrLf & _
            "       </td>" & vbCrLf & _
            "   </tr>" & vbCrLf & _
            "   <tr class=""default"">" & vbCrLf & _
            "       <td>" & vbCrLf & _
            "               <b>School ID</b>" & vbCrLf & _
            "       </td>" & vbCrLf & _
            "       <td>" & vbCrLf & _
            "           " & thefile.Frame_N_ & vbCrLf & _
            "       </td>" & vbCrLf & _
            "   </tr>" & vbCrLf


        End If

        msg = msg & _
        "   <tr class=""default"">" & vbCrLf & _
        "       <td>" & vbCrLf & _
        "               <b>Uploaded</b>" & vbCrLf & _
        "       </td>" & vbCrLf & _
        "       <td>" & vbCrLf & _
        "           " & thefile.UploadDT & vbCrLf & _
        "       </td>" & vbCrLf & _
        "   </tr>" & vbCrLf & _
        "   <tr class=""default"" nowrap>" & vbCrLf & _
        "       <td>" & vbCrLf & _
        "               <b>Record Count</b>" & vbCrLf & _
        "       </td>" & vbCrLf & _
        "       <td>" & vbCrLf & _
        "           " & thefile.TotalRows & vbCrLf & _
        "       </td>" & vbCrLf & _
        "   </tr>" & vbCrLf

        msg = msg & _
        "</table></center></div>" & vbCrLf

        Dim IdLabel As String = String.Empty

        'If thefile.EFileType = EFileType.State Then
        '    IdLabel = "STATE: " & thefile.StateName
        'ElseIf thefile.EFileType = EFileType.District Then
        '    IdLabel = "DISTRICTID: " & thefile.LEAID
        'Else

        If thefile.EFileType = EFileType.Grade Then
            IdLabel = "ID: " & thefile.ID
        End If

        'Dim email As New Admin.BLL.NaepEmail
        'email.FromEmailAddress = Globals.Settings.EFileElement.EfileTechSupportEmail
        'email.ToEmailAddressList = Globals.Settings.EFileElement.EfileTechSupportEmail
        'email.BodyFormat = Admin.BLL.EmailBodyFormat.HTML
        'email.Body = msg
        'If Globals.Settings.Site.ToLower <> "development" Then
        '    email.EmailStatus = Admin.BLL.NaepEmailStatus.Sent
        'End If

        Dim emailSubject As String = ""
        If isuserveryified Then
            emailSubject = "MyTIMSS E-File: Verified Correct (" & IdLabel & ")"
        Else
            emailSubject = "MyTIMSS E-File: Verified INCORRECT (" & IdLabel & ")"
        End If

        SendEmail(ConfigurationManager.AppSettings("NotificationFromEmail"), ConfigurationManager.AppSettings("NotifyEmailList"), Nothing, Nothing, emailSubject, msg, True, Nothing)



    End Sub

    Public Function GetEfileStatusHistoryDataTable(fileid As Integer, type As String, args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("fileid", fileid, "equals"))
        args.FilterParameters.Add(New FilterParameter("StatusType", type, "equals"))
        Return mTIMSSDal.GetEfileStatusHistoryDataTable(args)
    End Function

    Public Function GetColumnsAndCodes() As DataTable
        Return mTIMSSDal.GetColumnsAndCodes()
    End Function

    Public Function GetClassListSubmitedDataRow(id As String) As DataRow
        Dim args As New SelectFromDatabaseArgs()
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New HL.FilterParameter("id", id, "equals"))
        Return mTIMSSDal.GetClassListSubmitedDataRow(args)
    End Function

    Public Sub UnlockClassListEditting(id As String)
        Dim nvp As New List(Of NameValuePair)
        nvp.Add(New NameValuePair("ClassListSubmited", Nothing))
        nvp.Add(New NameValuePair("ClassListSubmitedBy", Nothing))
        mTIMSSDal.HandleTableUpdate(nvp, id, "tblSCSGrade", "id")
    End Sub


    Public Function GetTestRegionNameValuePairList(S_State As String) As List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        If Not String.IsNullOrEmpty(S_State) Then
            args.FilterParameters.Add(New FilterParameter("S_State", S_State, "equals"))
        End If
        Return mTIMSSDal.GetTestRegionNameValuePairList(args)
    End Function

    Public Function GetSmpGrdNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetSmpGrdNameValuePairList()
    End Function

    Public Function GetDistinctEfileStatusNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetDistinctEfileStatusNameValuePairList()
    End Function

    Public Function GetDistincDPStatusNameValuePairList() As List(Of NameValuePair)
        Return mTIMSSDal.GetDistincDPStatusNameValuePairList()
    End Function

    Public Function GetClassListSubmittedDataTable(args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetClassListSubmittedDataTable(args)
    End Function

    Public Function GetEfilesReadyForParsingDataTable(args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        'args.FilterParameters.Add(New FilterParameter("DPStatusCode", "Ready For Parsing", "equals"))
        args.FilterParameters.Add(New FilterParameter("DPStatusCode", "'Ready For Parsing', 'Ready for OFF-LINE'", "in", 100))

        Return (mTIMSSDal.GetEfileDetailsDataTable(args))
    End Function

    Public Sub ParseEfile(fileid As Integer)
        mTIMSSDal.ParseEfile(fileid)
        SetStatusCode(fileid, "Ready for OFF-LINE", EFileStatusType.DP, 0)
    End Sub

    Public Function GetEfileCleanedStudetDataTable(fileid As Integer, args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        args.FilterParameters.Add(New FilterParameter("fileid", fileid, "equals"))
        Return mTIMSSDal.GetEfileCleanedStudetDataTable(args)
    End Function


    Public Function GetAssessmentDateTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        'Dim args As New GetSchoolListSqlDataSourceArgs
        InitSelectFromDatabaseArgss(args)


        'If Not String.IsNullOrEmpty(state) Then
        '    args.FilterParameters.Add(New FilterParameter("s_state", state, "equals"))
        'End If

        'If Not String.IsNullOrEmpty(region) Then
        '    args.FilterParameters.Add(New FilterParameter("testregion", region, "equals"))
        'End If

        'args.SortParameters.Add(New SortParameter("ScheDate", SortDirection.Ascending))
        Return mTIMSSDal.GetAssessmentDateTable(args)
    End Function

    Public Function GetSchoolCompletionReportDateTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetSchoolCompletionReportDateTable(args)
    End Function

    Public Function GetQuestionnaireStatusReportSchoolNameValuePairList(args As GetSchoolListSqlDataSourceArgs) As List(Of NameValuePair)
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetQuestionnaireStatusReportSchoolNameValuePairList(args)
    End Function

    Public Function GetQuestionnaireStatusReportSchoolDetailsValuePairList(args As GetSchoolListSqlDataSourceArgs) As List(Of NameValuePair)
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetQuestionnaireStatusReportSchoolDetailsValuePairList(args)
    End Function

    Public Shared ReadOnly Property IsTestAdministratorTroubleShooter() As Boolean
        Get
            Return Roles.IsUserInRole("Test Administrator Troubleshooter")
        End Get
    End Property

    Const stlfdocumentfolder As String = "stlf"
    'Public Function GetSTLFFilePath(id As String) As String
    '    Dim args As New GetSchoolListSqlDataSourceArgs
    '    args.FilterParameters.Add(New FilterParameter("id", id, "equals"))
    '    InitSelectFromDatabaseArgss(args)
    '    Dim result As String = mTIMSSDal.GetSTLFFilePath(args)
    '    'If Not String.IsNullOrEmpty(result) Then
    '    '    result = result.Replace(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "~/")
    '    'End If
    '    Return result
    '    'Return AddSTLFFilePath(result)
    '    'System.Web.HttpContext.Current.Server.
    'End Function

    Public Function ProcessfileUploadForGradeSTLF(upload As FileUpload, id As String, frame_n_ As String)
        Dim nvp As New List(Of NameValuePair)
        If upload.HasFile Then

            'Dim dt As DateTime = DateTime.Now


            'Dim extension As String = upload.PostedFile.FileName.Substring(upload.PostedFile.FileName.LastIndexOf(".") + 1)
            'Dim basefilename As String = "Doc_" & frame_n_ & "_" & id
            'Dim ServerRelativePath As String = stlfdocumentfolder
            'Dim ServerFilePath As String = System.Web.HttpContext.Current.Server.MapPath("~/output/" & ServerRelativePath)
            ''Dim filenamewithextension As String = basefilename & "." & extension
            'Dim filenamewithextension As String = basefilename & "_" & Random5Numbers() & "." & extension
            'Dim tempfilename As String = ServerFilePath & "\" & filenamewithextension


            'If (Not System.IO.Directory.Exists(ServerFilePath)) Then
            '    System.IO.Directory.CreateDirectory(ServerFilePath)
            'End If

            'Dim cnt As Integer = 1
            'While System.IO.File.Exists(tempfilename)
            '    'filenamewithextension = basefilename & "(" & cnt & ")" & "." & extension
            '    filenamewithextension = basefilename & "_" & Random5Numbers() & "." & extension
            '    tempfilename = ServerFilePath & "\" & filenamewithextension
            '    cnt = cnt + 1
            'End While
            
            nvp.Add(New NameValuePair("STLFUserFilePath", upload.FileName))
            nvp.Add(New NameValuePair("STLFFilesize", upload.PostedFile.ContentLength))
            nvp.Add(New NameValuePair("STLFContentType", upload.PostedFile.ContentType))
            nvp.Add(New NameValuePair(upload.FileBytes(), "STLFFileData"))
            
            Try
                'save the temporarily
                'upload.SaveAs(tempfilename)
                mTIMSSDal.HandleTableUpdate(nvp, id, "tblSCSGrade", "id")
            Catch ex As Exception
                Throw ex
            Finally
                'always  delete file even if there is an error
                'If System.IO.File.Exists(tempfilename) Then
                '    System.IO.File.Delete(tempfilename)
                'End If
            End Try

            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetIconForFile(filename As Object) As String
        Dim result As String = Nothing
        If Not filename Is DBNull.Value Then
            If filename.ToString().EndsWith(".xls") Or filename.ToString().EndsWith(".xlsx") Then
                result = "~/common/images/excel_icon.gif"
            ElseIf filename.ToString().EndsWith(".pdf") Then
                result = "~/common/images/pdf_icon.gif"
            ElseIf filename.ToString().EndsWith(".doc") Or filename.ToString().EndsWith(".docx") Then
                result = "~/common/images/word.gif"
            ElseIf filename.ToString().EndsWith(".zip") Then
                result = "~/common/images/zip.png"
            End If
        End If
        Return result
    End Function


    Public Function HasIconForFile(filename As Object) As Boolean
        Return Not String.IsNullOrEmpty(GetIconForFile(filename))
    End Function

    Public Function HasFilename(filename As Object) As Boolean
        Dim result As Boolean = False
        If Not filename Is DBNull.Value Then
            If Not String.IsNullOrEmpty(filename) Then
                result = True
            End If
        End If
        Return result
    End Function

    Public Function GetManageDocumentsDataTable(args As SelectFromDatabaseArgs) As DataTable
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.GetManageDocumentsDataTable(args)

    End Function

    Public Function ConvertFilePathToURL(filepath As Object) As String
        Dim result As String = Nothing
        If Not filepath Is DBNull.Value Then
            If Not String.IsNullOrEmpty(filepath) Then
                result = filepath.ToString().Replace(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "~/")
            End If
        End If
        Return result
    End Function

    'Public Function AddSTLFFilePath(filename As Object) As String
    '    Dim result As String = Nothing
    '    If Not filename Is DBNull.Value Then
    '        If Not String.IsNullOrEmpty(filename) Then
    '            result = "~/output/" & stlfdocumentfolder & "/" & filename
    '        End If
    '    End If
    '    Return result
    'End Function

    Public Function Random5Numbers() As String
        Return RandomNumber() & "" & RandomNumber() & "" & RandomNumber() & "" & RandomNumber() & "" & RandomNumber()
    End Function

    Public Function RandomNumber() As String
        Dim n As Integer = 9
        Return CInt(Math.Ceiling(Rnd() * n)) + 1
    End Function

    Public Function GetTIMSSStaffTRADataRow(winsid As String) As DataRow
        Return mTIMSSDal.GetTIMSSStaffTRADataRow(winsid)
    End Function
    Public Function HasWINSIDAlreadyBeenLinked(winsid As String, userid As String) As Boolean
        Return mTIMSSDal.HasWINSIDAlreadyBeenLinked(winsid, userid)
    End Function

    Public Shared ReadOnly Property IsSTLFUploader() As Boolean
        Get
            Return Roles.IsUserInRole("STLF Uploader")
        End Get
    End Property

    Public Shared ReadOnly Property CanUploadSTLF() As Boolean
        Get
            Return IsAdmin Or IsSTLFUploader()
        End Get
    End Property

    Public Sub RemoveGradeSTLFFile(id As String, which As String, action As String)
        'Dim nvp As New List(Of NameValuePair)
        'nvp.Add(New NameValuePair("STLFDocument", Nothing))
        'mTIMSSDal.HandleTableUpdate(nvp, id, "tblSCSGrade", "id")

        Dim nvp As New List(Of NameValuePair)
        nvp.Add(New NameValuePair("id", id))
        nvp.Add(New NameValuePair("which", which))
        nvp.Add(New NameValuePair("action", action))
        mTIMSSDal.HandleStoredProc(nvp, "usp_UpdateDocument")
    End Sub

    Public Sub StreamDocumentToBrowser(whichdocument As String, id As String)
        StreamDocumentToBrowser(whichdocument, id, False)
    End Sub

    Public Sub StreamDocumentToBrowser(whichdocument As String, id As String, showAll As Boolean)
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        Dim dr As DataRow = mTIMSSDal.GetDocumentsDataRowForStream(whichdocument, id, args, showAll)
        Dim contenttype As String = dr("ContentType")
        Dim filename As String = dr("Filename")

        If contenttype.Equals("application/pdf") Then
            filename = filename & ".pdf"
        ElseIf contenttype.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document") Then
            filename = filename & ".docx"
            'Else
            '    filename = Nothing
        End If

        System.Web.HttpContext.Current.Response.Buffer = False 'transmitfile self buffers
        System.Web.HttpContext.Current.Response.Clear()
        System.Web.HttpContext.Current.Response.ClearContent()
        System.Web.HttpContext.Current.Response.ClearHeaders()
        System.Web.HttpContext.Current.Response.ClearContent()

        System.Web.HttpContext.Current.Response.ContentType = contenttype

        If Not filename Is Nothing Then
            'System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + filename)
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename)
        End If

        System.Web.HttpContext.Current.Response.AddHeader("Content-Length", dr("FileSize"))
        System.Web.HttpContext.Current.Response.BinaryWrite(dr("FileData"))
        'System.Web.HttpContext.Current.Response.Flush()
        System.Web.HttpContext.Current.Response.End()
    End Sub


    Public Function ProcessfileUploadForGrade(which As String, upload As FileUpload, id As String)
        If upload.HasFile Then
            mTIMSSDal.ProcessfileUploadForGrade(which, id, mUser.ProviderUserKey.ToString(), upload.FileName, upload.PostedFile.ContentLength, upload.FileBytes(), upload.PostedFile.ContentType)
            Return True
        Else
            Return False
        End If
    End Function

    Public Function DocumentUploaded(whichdocument As String, id As String) As DataRow
        Dim args As New SelectFromDatabaseArgs
        InitSelectFromDatabaseArgss(args)
        Return mTIMSSDal.DocumentUploaded(whichdocument, id, args)
    End Function
    Public Function ProjectName(id As String) As String
        Dim result As String = ""

        If id.EndsWith("6") Then
            result = "ICILS"
        ElseIf id.EndsWith("7") Then
            result = "eTIMSS"
        ElseIf id.EndsWith("T") Then
            result = "eTIMSS"
        ElseIf id.EndsWith("I") Then
            result = "ICILS"
        Else
            result = "eTIMSS"
        End If
        Return result
    End Function
    Public Function ProjectName() As String
        Dim result As String = ""
        If Me.HasGradeId4 Then
            result = ProjectName(Me.GradeId4)
        ElseIf Me.HasGradeId8 Then
            result = ProjectName(Me.GradeId8)
        End If
        Return result
    End Function

    Public Function isICILS() As Boolean
        Return ProjectName().Equals("ICILS") Or ProjectName().Equals("ICILS 2018")
    End Function

    Public Function iseTIMSS() As Boolean
        Return ProjectName().Equals("eTIMSS") Or ProjectName().Equals("eTIMSS 2018")
    End Function

    Public Function isICILS(id As String) As Boolean
        Return ProjectName(id).Equals("ICILS")
    End Function

    Public Function iseTIMSS(id As String) As Boolean
        Return ProjectName(id).Equals("eTIMSS")
    End Function

    Public Function isICILS2018(id As String) As Boolean
        Return ProjectName(id).Equals("ICILS 2018")
    End Function

    Public Function iseTIMSS2018(id As String) As Boolean
        Return ProjectName(id).Equals("eTIMSS 2018")
    End Function


End Class

