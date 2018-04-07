
Partial Class admin_AccountEdit
    Inherits BasePagePublic
    Public ReadOnly Property Username() As String
        Get
            'Return Server.HtmlEncode(Request.QueryString("u"))
            Return Request.QueryString("u")
        End Get
    End Property

    Public ReadOnly Property HasUsername() As Boolean
        Get
            Return Not String.IsNullOrEmpty(Username())
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            DropDownListWINSID.Attributes.Add("onchange", "return ShowConfirm(this, '" & Me.HiddenFieldCaopyNameAndEmail.ClientID & "');")

            Dim allroles As String() = Roles.GetAllRoles()
            Dim myroles As String() = Nothing
            Dim u As MembershipUser

            If HasUsername() Then
                u = Membership.GetUser(Me.Username)
                myroles = Roles.GetRolesForUser(u.UserName)
                panelResetPassword.Visible = True
            Else
                panelResetPassword.Visible = False
            End If


            For Each r In allroles
                Dim item As New ListItem(r, r)

                If Not myroles Is Nothing Then
                    For Each role As String In myroles
                        If item.Value = role Then
                            item.Selected = True
                        End If
                    Next
                End If
                CheckBoxListRoles.Items.Add(item)
            Next

            DropDownListREPSBGRP.DataSource = TimssBll.GetREPSBGRPNameValuePairList()
            DropDownListREPSBGRP.DataBind()


            DropDownListWINSID.DataSource = TimssBll.GetStaffWINSIDNameValuePairList()
            DropDownListWINSID.DataBind()


            DropDownListSchoolList.DataSource = TimssBll.GetRegistrationIDNameValuePairList()
            DropDownListSchoolList.DataBind()


            DropDownListTUA_LEA.DataSource = TimssBll.GetTUA_LEANameValuePairList()
            DropDownListTUA_LEA.DataBind()


            'CheckBoxListRoles.DataSource = Roles.GetAllRoles()
            'CheckBoxListRoles.DataBind()

            If Not HasUsername() Then
                ButtonUpdate.Visible = False
                ButtonAddNew.Visible = True
                Me.TextBoxUserName.Enabled = True
                panelpassword.Visible = True
                Return
            Else
                Me.TextBoxUserName.Enabled = False
                ButtonUpdate.Visible = True
                ButtonAddNew.Visible = False
                panelpassword.Visible = False
            End If

            If Not u Is Nothing Then
                Me.TextBoxUserName.Text = u.UserName
                Me.TextBoxDescription.Text = u.Comment
                Me.Email.Text = u.Email
                Me.CheckBoxIsApproved.Checked = u.IsApproved
                Dim prof As ProfileCommon = ProfileCommon.GetUserProfile(u.UserName)
                Me.TextBoxFirstname.Text = prof.FirstName
                Me.TextBoxLastname.Text = prof.LastName
                Me.DropDownListWINSID.SelectedValue = prof.WINSID
                Me.DropDownListREPSBGRP.SelectedValue = prof.REPSBGRP
                Me.TextBoxTelephone.Text = prof.Telephone
                Me.TextBoxExtension.Text = prof.TelephoneExtension
                Me.DropDownListSchoolList.SelectedValue = prof.RegistrationId
                Me.TextBoxFrame_N_.Text = prof.Frame_N_
                Me.DropDownListTUA_LEA.SelectedValue = prof.TUA_LEA

                'Dim myroles As String() = Roles.GetRolesForUser(u.UserName)

                'For Each role As String In myroles
                '    For Each item As ListItem In CheckBoxListRoles.Items
                '        If item.Value = role Then
                '            item.Selected = True
                '        End If
                '    Next
                'Next

            End If
        End If
    End Sub


    Protected Sub ButtonUdate_Click(sender As Object, e As System.EventArgs) Handles ButtonUpdate.Click
        If Not HasUsername() Then
            Return
        End If

        Try
            Dim u As MembershipUser = Membership.GetUser(Me.Username)
            If Not u Is Nothing Then

                u.Comment = Me.TextBoxDescription.Text
                u.Email = Me.Email.Text
                u.IsApproved = Me.CheckBoxIsApproved.Checked
                Membership.UpdateUser(u)

                Dim prof As ProfileCommon = ProfileCommon.GetUserProfile(u.UserName)
                prof.FirstName = Me.TextBoxFirstname.Text
                prof.LastName = Me.TextBoxLastname.Text
                prof.WINSID = Me.DropDownListWINSID.SelectedValue
                prof.REPSBGRP = Me.DropDownListREPSBGRP.SelectedValue
                prof.Telephone = Me.TextBoxTelephone.Text
                prof.TelephoneExtension = Me.TextBoxExtension.Text
                prof.RegistrationId = Me.DropDownListSchoolList.SelectedValue
                prof.Frame_N_ = Me.TextBoxFrame_N_.Text
                prof.TUA_LEA = Me.DropDownListTUA_LEA.SelectedValue
                prof.Save()

                For Each item As ListItem In CheckBoxListRoles.Items
                    Dim alreadyinrole As Boolean = Roles.IsUserInRole(Me.Username, item.Value)
                    If alreadyinrole And item.Selected = False Then
                        Roles.RemoveUserFromRole(Me.Username, item.Value)
                    ElseIf Not alreadyinrole And item.Selected = True Then
                        Roles.AddUserToRole(Me.Username, item.Value)
                    End If
                Next
                Response.Redirect("accounts.aspx")
            End If
        Catch ex As Exception
            LabelError.Text = ex.Message
        End Try
        
    End Sub

    Protected Sub ButtonAddNew_Click(sender As Object, e As System.EventArgs) Handles ButtonAddNew.Click
        Try
            Dim uid As String = Me.TextBoxUserName.Text
            Dim u As MembershipUser = Membership.CreateUser(uid, Me.Password.Text, Me.Email.Text)
            u.Comment = Me.TextBoxDescription.Text
            u.IsApproved = Me.CheckBoxIsApproved.Checked
            Membership.UpdateUser(u)

            Dim prof As ProfileCommon = ProfileCommon.GetUserProfile(u.UserName)
            prof.FirstName = Me.TextBoxFirstname.Text
            prof.LastName = Me.TextBoxLastname.Text
            prof.WINSID = Me.DropDownListWINSID.Text
            prof.REPSBGRP = Me.DropDownListREPSBGRP.SelectedValue
            prof.Telephone = Me.TextBoxTelephone.Text
            prof.TelephoneExtension = Me.TextBoxExtension.Text
            prof.RegistrationId = Me.DropDownListSchoolList.SelectedValue
            prof.Frame_N_ = Me.TextBoxFrame_N_.Text
            prof.TUA_LEA = Me.DropDownListTUA_LEA.SelectedValue
            prof.Save()

            For Each item As ListItem In CheckBoxListRoles.Items
                Dim alreadyinrole As Boolean = Roles.IsUserInRole(uid, item.Value)
                If alreadyinrole And item.Selected = False Then
                    Roles.RemoveUserFromRole(uid, item.Value)
                ElseIf Not alreadyinrole And item.Selected = True Then
                    Roles.AddUserToRole(uid, item.Value)
                End If
            Next

            Response.Redirect("accounts.aspx")
        Catch ex As Exception
            LabelError.Text = ex.Message
        End Try

    End Sub

    Public Sub ResetPassword_OnClick(sender As Object, args As EventArgs)
        Dim newPassword As String
        Dim u = Membership.GetUser(Me.Username, False)

        If u Is Nothing Then
            Msg.Text = "Username " & Server.HtmlEncode(Me.Username) & " not found. Please check the value and re-enter."
            Return
        End If

        Try
            newPassword = u.ResetPassword()
        Catch e As MembershipPasswordException
            Msg.Text = "Invalid password answer. Please re-enter and try again."
            Return
        Catch e As Exception
            Msg.Text = e.Message
            Return
        End Try

        If Not newPassword Is Nothing Then
            Msg.Text = "Password reset, the new password is: " & Server.HtmlEncode(newPassword)
        Else
            Msg.Text = "Password reset failed. Please re-enter your values and try again."
        End If
    End Sub

    Public Sub LinkButtonLoginAs_OnClick(sender As Object, args As EventArgs)

        Dim u = Membership.GetUser(Me.Username, False)

        If u Is Nothing Then
            Msg.Text = "Username " & Server.HtmlEncode(Me.Username) & " not found. Please check the value and re-enter."
            Return
        End If

        FormsAuthentication.SetAuthCookie(u.UserName, True)
        Response.Redirect("~/")

    End Sub


    Sub SelectedSchool_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListSchoolList.SelectedIndexChanged
        Dim ddl As DropDownList = sender
        Me.TextBoxFrame_N_.Text = TimssBll.GetFrameNForRegistrationID(ddl.SelectedValue)
    End Sub


    Sub DropDownListWINSID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListWINSID.SelectedIndexChanged
        Dim ddl As DropDownList = sender
        Dim dr As DataRow = TimssBll.GetTIMSSStaffTRADataRow(ddl.SelectedValue)

        If Me.HiddenFieldCaopyNameAndEmail.Value.Equals("1") Then
            Me.TextBoxFirstname.Text = ""
            Me.TextBoxLastname.Text = ""
            Me.Email.Text = ""
            Me.LabelWinsIdError.Visible = False
            Me.HyperLinkViewWinsId.Visible = False
        End If

        If Not dr Is Nothing Then

            If Me.HiddenFieldCaopyNameAndEmail.Value.Equals("1") Then
                Me.TextBoxFirstname.Text = IIf(dr("fldfirstname") Is DBNull.Value, "", dr("fldfirstname"))
                Me.TextBoxLastname.Text = IIf(dr("fldLastName") Is DBNull.Value, "", dr("fldLastName"))
                Me.Email.Text = IIf(dr("Corporate_FOS_SMTPAlias") Is DBNull.Value, "", dr("Corporate_FOS_SMTPAlias"))
            End If

            If HasUsername Then
                Dim u = Membership.GetUser(Me.Username, False)
                Me.LabelWinsIdError.Visible = TimssBll.HasWINSIDAlreadyBeenLinked(ddl.SelectedValue, u.ProviderUserKey.ToString())
            Else
                Me.LabelWinsIdError.Visible = TimssBll.HasWINSIDAlreadyBeenLinked(ddl.SelectedValue, Nothing)
            End If
            Me.HyperLinkViewWinsId.Visible = Me.LabelWinsIdError.Visible
            Me.HyperLinkViewWinsId.NavigateUrl = "accounts.aspx?searchFLD=WINSID&searchSTR=" & ddl.SelectedValue
        Else
            Me.LabelWinsIdError.Visible = False
            Me.HyperLinkViewWinsId.Visible = False
        End If

    End Sub
End Class
