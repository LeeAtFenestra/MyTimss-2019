
Partial Class admin_ConfigureUser
    Inherits BasePagePublic
    Private _strRows(1, 0) As String
    Private _roleRowID As Int16

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Me.ddlView.Items.Add("Select One")
            'add the names of the views to the view drop down so a user can choose what they would like to do
            Dim vwView As View
            Dim strAnswer() As String = Nothing
            For Each vwView In Me.mvwMain.Views
                Dim strViewID As String = vwView.ID
                Dim strTemp As String = ""
                Dim intWord As String = -1
                Dim intLength As Int16 = strViewID.Length
                strAnswer = Nothing
                For intLoop As Int16 = 1 To intLength
                    strTemp = strViewID.Substring(intLoop - 1, 1)
                    If strTemp.ToUpper = strViewID.Substring(intLoop - 1, 1) Then
                        intWord += 1
                        ReDim Preserve strAnswer(intWord)
                        strAnswer(intWord) = strViewID.Substring(intLoop - 1, 1)
                    Else
                        If strAnswer Is Nothing Then
                            ReDim strAnswer(0)
                            strAnswer(0) = strViewID
                            Exit For
                        Else
                            strAnswer(intWord) = strAnswer(intWord).ToString & strViewID.Substring(intLoop - 1, 1)
                        End If
                    End If

                Next
                Me.ddlView.Items.Add(String.Join(" ", strAnswer))

                'test configuration to make sure you can do it before you let the user try
                If Membership.Provider.EnablePasswordRetrieval AndAlso Membership.Provider.RequiresQuestionAndAnswer = False Then
                    'display the reset password checkbox
                    chkResetPassword.Visible = True
                    lblResetPassword.Visible = True
                End If
            Next

            Cache.Remove("UsersDataSet")

            Me.ddlView.Focus()
        End If

        UpdateAddUserTable()
    End Sub
    Protected Sub btnUpdateRoles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateRoles.Click

        Dim blnSuccess As Boolean = False
        Dim strMessage As String = String.Empty
        'iterate through the controls in the checkedlistbox control
        Dim item As ListItem
        For Each item In Me.ckltRemoveUsersRoles.Items
            If item.Selected = True Then
                blnSuccess = AddUserToRole(Me.ddlUser.SelectedItem.ToString, item.Text)
                If blnSuccess = True Then
                    strMessage &= "<br>" & Me.ddlUser.SelectedItem.ToString & " was successfully added to the " & item.Text & " Role. <br>"
                Else
                    strMessage &= "<br> There was a problem adding " & Me.ddlUser.SelectedItem.ToString & " to the " & item.Text & " role.  <br> Please use the website administration tool to complete this task.<br><br>"
                End If
                'uncheck the box for next time around
                item.Selected = False
            Else
                'test to see if the user is already in the role and remove them if they are
                Dim strResult As String
                strResult = Me.RemoveUserFromRole(Me.ddlUser.SelectedItem.ToString, item.Text)
                If strResult = "" Then
                    'nothing, go to next control
                ElseIf strResult = "Exception" Then
                    strMessage &= "<br> There was a problem removing " & Me.ddlUser.SelectedItem.ToString & " from the " & item.Text & " role.  <br> Please use the website administration tool to complete this task.<br><br>"
                Else
                    strMessage &= "<br>" & Me.ddlUser.SelectedItem.ToString & " was successfully removed from the " & strResult & " Role. <br>"
                End If

            End If
        Next

        Me.lblAddUserToRoleResult.Text = "<br>" & strMessage
        ddlUser.SelectedIndex = ddlUser.Items.Count - 1

    End Sub
    Protected Sub ddlUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged
        'check the roles the user is already in

        Dim strRole As String = String.Empty
        Dim liItem As ListItem
        'unselect all items
        For Each liItem In Me.ckltRemoveUsersRoles.Items
            liItem.Selected = False
        Next
        For Each strRole In Roles.GetRolesForUser(Me.ddlUser.SelectedItem.ToString)
            For Each liItem In Me.ckltRemoveUsersRoles.Items
                If liItem.Text = strRole.ToString Then
                    liItem.Selected = True
                End If
            Next
        Next

        Me.lblAddUserToRoleResult.Text = ""

    End Sub
    Protected Sub ddlView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlView.SelectedIndexChanged
        'reset all result labels
        Me.lblDeleteResult.Text = ""
        Me.lblNewRoleResult.Text = ""
        Me.lblNewUserResult.Text = ""
        Me.lblAddUserToRoleResult.Text = ""
        Me.lblRemoveRoleResult.Text = ""
        Me.lblUpdateUserDetailsResult.Text = ""

        Dim liOne As ListItem = Me.ddlView.Items.FindByText("Select One")
        If liOne Is Nothing Then
            Me.mvwMain.ActiveViewIndex = Me.ddlView.SelectedIndex
        Else
            Me.mvwMain.ActiveViewIndex = Me.ddlView.SelectedIndex - 1
            Me.ddlView.Items.Remove("Select One")
        End If
        'maintain correct info by updating all sections.
        Select Case Me.ddlView.SelectedValue

            Case "Create A New User" '0
                Dim txtUserName As New TextBox
                txtUserName = Me.tableAddUser.Rows(0).Cells(1).Controls(0)
                txtUserName.Focus()
            Case "Update User Roles" '1
                'populate existing users
                ckltRemoveRoles.Items.Clear()
                ckltRemoveUsersRoles.Items.Clear()
                Dim ExistingUsers As MembershipUserCollection = Membership.GetAllUsers()
                ddlUser.DataSource = ExistingUsers
                ddlUser.DataBind()
                ddlUser.Items.Add("Select a User")
                ddlUser.SelectedIndex = ddlUser.Items.Count - 1
                ddlUser.Focus()
                PopulateckltRoleLists()
            Case "Remove A User" '2
                'populate the user list
                Dim AllUsers As MembershipUserCollection = Membership.GetAllUsers
                Me.ckltDeleteUsers.DataSource = AllUsers
                Me.ckltDeleteUsers.DataBind()
            Case "Update User" '3
                Dim ExistingUsers As MembershipUserCollection = Membership.GetAllUsers()
                ddlUserUpdateUserName.DataSource = ExistingUsers
                ddlUserUpdateUserName.DataBind()
                ddlUserUpdateUserName.Items.Add("Select a User")
                ddlUserUpdateUserName.SelectedIndex = ddlUserUpdateUserName.Items.Count - 1
                ddlUserUpdateUserName.Focus()

            Case "Create A New Role" '4
                txtNewRoleName.Focus()
            Case "Remove A Role" '5
                PopulateckltRoleLists()

            Case "View All Users" '6
                'populate the gridview
                gvwUser.EditIndex = -1
                Me.BindUserGrid()
        End Select
        Me.lblDeleteResult.Text = ""
        Me.lblNewUserResult.Text = ""
        Me.lblAddUserToRoleResult.Text = ""
        Me.lblNewRoleResult.Text = ""
        Me.lblRemoveRoleResult.Text = ""
    End Sub
    Protected Sub btnRemoveUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveUser.Click
        lblDeleteResult.Text = ""
        Dim liItem As ListItem
        For Each liItem In Me.ckltDeleteUsers.Items
            If liItem.Selected = True Then
                Try
                    Membership.DeleteUser(liItem.Value)
                    'delete the associated profile
                    ProfileManager.DeleteProfile(liItem.Value)
                    lblDeleteResult.Text = "<br>Deletion was successful."
                Catch ex As Exception
                    lblDeleteResult.Text = "<br>An exception occured; " & ex.Message
                End Try
            End If
        Next

        'populate the user list
        Dim AllUsers As MembershipUserCollection = Membership.GetAllUsers
        Me.ckltDeleteUsers.DataSource = AllUsers
        Me.ckltDeleteUsers.DataBind()
    End Sub
    Protected Sub btnNewRole_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewRole.Click
        If Roles.RoleExists(Me.txtNewRoleName.Text) Then
            'the role already exists
            Me.lblNewRoleResult.Text = "<br>" & Me.txtNewRoleName.Text & " already exists."
            Me.txtNewRoleName.Text = ""
        Else
            Try
                Roles.CreateRole(Me.txtNewRoleName.Text)
                Me.lblNewRoleResult.Text = "<br>" & Me.txtNewRoleName.Text & " was successfully added."
                Me.txtNewRoleName.Text = ""
            Catch ex As Exception
                Me.lblNewRoleResult.Text = "<br>There was an problem adding the new role, " & Me.txtNewRoleName.Text & ". <br> Please use the Web Site Administration Tool to add the new role."
                Me.txtNewRoleName.Text = ""
            End Try
        End If
        PopulateckltRoleLists()
    End Sub
    Protected Sub btnRemoveRoles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveRoles.Click
        Dim liItem As ListItem
        Dim blnError As Boolean = False
        Dim strMessage As String = ""
        For Each liItem In Me.ckltRemoveRoles.Items
            If liItem.Selected = True Then
                Try
                    Roles.DeleteRole(liItem.Text)
                    strMessage &= "<br>The " & liItem.Text & " role was successfully deleted."
                Catch ex As Exception
                    strMessage &= "<br>There was an problem deleting the role, " & liItem.Text & "." & "<br>" & ex.Message
                End Try
            End If
        Next
        Me.lblRemoveRoleResult.Text = strMessage
        PopulateckltRoleLists()
    End Sub
    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        lblNewUserResult.Text = CreateNewUser(Me.txtUserName.Text, Me.txtPassword.Text, txtEmail.Text, txtPassQues.Text, txtPassAns.Text)
        Me.txtUserName.Focus()
    End Sub
    Protected Sub btnUpdateEmailOrPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateEmailOrPassword.Click
        Dim strMessage As String = String.Empty
        Dim muUser As MembershipUser = Membership.GetUser(Me.ddlUserUpdateUserName.SelectedValue)
        Try
            If muUser.Email <> Me.txtUpdateUserEmail.Text Then
                muUser.Email = Me.txtUpdateUserEmail.Text
                Membership.UpdateUser(muUser)
                strMessage = muUser.ToString & "'s Email Address was updated successfully."
            End If

        Catch ex As Exception
            strMessage = "There was a problem updating " & muUser.ToString & "'s Email Address.<br>" & ex.Message
        End Try


        'if the configuration allows for password reset and password retrieval
        'this code assumes that the admin can retrieve the password, if not resetting it would do no good
        'b/c the password needs to be obtainable after it is reset

        'Note:  If Admins are resetting passwords to "Ugly Strings" some logic should be implemented to require the user the change that at first logon

        If chkResetPassword.Checked Then
            'provider is set up to do password Reset and Retrieval, and does not require the QuestionAndAnswer to do it
            Dim strOldPassword As String = String.Empty
            Dim strNewPassword As String = String.Empty
            Try
                If Membership.Provider.EnablePasswordReset Then
                    strOldPassword = muUser.GetPassword
                    muUser.ResetPassword()
                    strNewPassword = muUser.GetPassword.ToString()
                Else
                    'if ResetPassword = False then send user their old password
                    strNewPassword = muUser.GetPassword.ToString()
                End If
            Catch ex As System.Configuration.Provider.ProviderException
                Try
                    'try to reset their password back to the old one
                    muUser.ChangePassword(strNewPassword, strOldPassword)
                    strMessage &= "<Br> There was a problem resetting your password.  The password was reset to the old password."
                    Me.lblUpdateUserDetailsResult.Text = "<br>" & strMessage
                    Me.txtUpdateUserEmail.Text = ""
                    Me.ddlUserUpdateUserName.SelectedIndex = ddlUserUpdateUserName.Items.Count - 1
                    Exit Sub
                Catch ex1 As Exception
                    strMessage &= "<Br> There was a problem resetting your password.  Please contact the Web Site Administrator."
                    Me.lblUpdateUserDetailsResult.Text = "<br>" & strMessage
                    Me.txtUpdateUserEmail.Text = ""
                    Me.ddlUserUpdateUserName.SelectedIndex = ddlUserUpdateUserName.Items.Count - 1
                    Exit Sub
                End Try
            End Try
            'send the current password as an email
            Dim msgMailMessage As New System.Net.Mail.SmtpClient

            Dim s As New StringBuilder("This is your new Password for the ")
            s.Append(Roles.ApplicationName)
            s.Append(", ")
            s.Append(strNewPassword)
            s.Append(".")
            s.Append(vbCrLf)
            s.Append("Please try logging into the application again with your new password.")
            s.Append(vbCrLf)
            s.Append("If you have any difficulties please contact your Administrator.")
            s.Append(vbCrLf)
            s.Append("Thank You.")
            Try
                Dim cr As New Westat.FAB.Configuration.ConfigurationReader
                msgMailMessage.Send(cr.FromEmail.ToString, muUser.Email, "Your Password has been reset for the " & Roles.ApplicationName, s.ToString)
                strMessage &= "<br>" & muUser.ToString & "'s Password was reset and sent to " & muUser.Email
            Catch ex As System.Net.Mail.SmtpException
                muUser.ChangePassword(strNewPassword, strOldPassword)
                strMessage &= "<Br> There was a problem Emailing the new password.  The password was reset to the old password."
            Finally
                msgMailMessage = Nothing
            End Try

        End If
        Me.lblUpdateUserDetailsResult.Text = "<br>" & strMessage
        Me.txtUpdateUserEmail.Text = ""
        Me.ddlUserUpdateUserName.SelectedIndex = ddlUserUpdateUserName.Items.Count - 1
        Me.chkResetPassword.Checked = False
    End Sub
    Protected Sub ddlUserUpdateUserName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserUpdateUserName.SelectedIndexChanged
        Dim muUser As MembershipUser = Membership.GetUser(Me.ddlUserUpdateUserName.SelectedValue)
        Me.txtUpdateUserEmail.Text = muUser.Email
    End Sub
    Private Sub PopulateckltRoleLists()
        'repopulate the lists of roles
        ckltRemoveRoles.Items.Clear()
        ckltRemoveUsersRoles.Items.Clear()
        Dim strRole As String = String.Empty
        For Each strRole In Roles.GetAllRoles
            Me.ckltRemoveRoles.Items.Add(strRole.ToString)
            Me.ckltRemoveUsersRoles.Items.Add(strRole.ToString)
        Next
    End Sub
    Private Function CreateNewUser(ByVal UserName As String, ByVal Password As String, ByVal Email As String, ByVal Question As String, ByVal Answer As String) As String
        Dim strMessage As String = String.Empty
        Dim colUsers As MembershipUserCollection
        'test to see if user already exists
        colUsers = Membership.FindUsersByName(UserName)
        Dim status As MembershipCreateStatus
        If colUsers.Count = 0 Then
            Dim muNewUser As MembershipUser
            'by default create the user w/o a Question/Answer
            If Question = "" OrElse Answer = "" Then
                muNewUser = Membership.CreateUser(UserName, Password, Email)
            Else
                ' if Question AND Answer provided, use them
                muNewUser = Membership.CreateUser(UserName, Password, Email, Question, Answer, True, status)
            End If
            If muNewUser Is Nothing Then
                Select Case status
                    Case MembershipCreateStatus.DuplicateUserName
                        strMessage = "<br>Username already exists. Please enter a different user name."
                    Case MembershipCreateStatus.DuplicateEmail
                        strMessage = "<br>A username for that e-mail address already exists. Please enter a different e-mail address."
                    Case MembershipCreateStatus.InvalidPassword
                        strMessage = "<br>The password provided is invalid. Please enter a valid password value."
                    Case MembershipCreateStatus.InvalidEmail
                        strMessage = "<br>The e-mail address provided is invalid. Please check the value and try again."
                    Case MembershipCreateStatus.InvalidAnswer
                        strMessage = "<br>The password retrieval answer provided is invalid. Please check the value and try again."
                    Case MembershipCreateStatus.InvalidQuestion
                        strMessage = "<br>The password retrieval question provided is invalid. Please check the value and try again."
                    Case MembershipCreateStatus.InvalidUserName
                        strMessage = "<br>The user name provided is invalid. Please check the value and try again."
                    Case MembershipCreateStatus.ProviderError
                        strMessage = "<br>The authentication provider Returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator."
                    Case MembershipCreateStatus.UserRejected
                        strMessage = "<br>The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator."
                    Case Else
                        strMessage = "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator."
                End Select
                Return strMessage
            Else
                strMessage &= "<br>" & UserName & " was successfully created."
            End If
            'add the user to the specified roles
            Try
                Dim blnSuccess As Boolean
                'iterate through the checkboxes in the checked listbox control
                Dim liItem As ListItem
                Dim ckltRoleList As New CheckBoxList
                'get the checklistbox from the cell in the table
                ckltRoleList = tableAddUser.Rows.Item(_roleRowID).Cells(1).Controls(0)
                If Not ckltRoleList Is Nothing Then
                    For Each liItem In ckltRoleList.Items
                        If liItem.Selected = True Then
                            blnSuccess = AddUserToRole(UserName, liItem.Text)
                            If blnSuccess = True Then
                                strMessage &= "<br>" & UserName & " was successfully added to the " & liItem.Text & " Role. <br>"
                            Else
                                strMessage &= "<br> There was a problem adding " & UserName & " to the " & liItem.Text & " role.  <br> Please use the website administration tool to complete this task.<br><br>"
                            End If
                            'uncheck the box for next time around
                            liItem.Selected = False
                        End If
                    Next
                    Me.txtUserName.Text = ""
                    Me.txtPassword.Text = ""
                    Me.txtPassAns.Text = ""
                    Me.txtPassQues.Text = ""
                    Me.txtEmail.Text = ""
                Else
                    'there was a problem
                End If

            Catch ex As Exception
                strMessage &= "<br>An exception occured; " & ex.Message
            End Try
        Else
            'user already exists
            strMessage &= UserName & " already exists.<br>Please use the User Profile page to update this user's Profile."
            Return strMessage
        End If
        'create a new profile for the user
        strMessage &= "<br>" & Me.CreateProfile(UserName)
        Return strMessage
    End Function
    Private Function AddUserToRole(ByVal UserName As String, ByVal RoleName As String) As Boolean
        If Roles.IsUserInRole(UserName, RoleName) Then
            Return True
        End If
        Try
            Roles.AddUserToRole(UserName, RoleName)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function RemoveUserFromRole(ByVal UserName As String, ByVal RoleName As String) As String
        If Roles.IsUserInRole(UserName, RoleName) Then
            Try
                Roles.RemoveUserFromRole(UserName, RoleName)
                Return RoleName
            Catch ex As Exception
                Return ex.Message
            End Try
        Else
            Return ""
        End If
    End Function
    Private Function GetAllUsers() As DataSet
        Dim dsUsers As New DataSet
        Dim dtUsers As New DataTable("Users")
        Dim colColumn As New DataColumn
        Dim rowRow As DataRow

        'create the columns
        '1
        colColumn.ColumnName = "User Name"
        colColumn.ReadOnly = True
        dtUsers.Columns.Add(colColumn)
        '2
        colColumn = New DataColumn
        colColumn.ColumnName = "Creation Date"
        colColumn.ReadOnly = True
        colColumn.DataType = System.Type.GetType("System.String")
        dtUsers.Columns.Add(colColumn)
        '3
        colColumn = New DataColumn
        colColumn.ColumnName = "Last Login Date"
        colColumn.ReadOnly = True
        colColumn.DataType = System.Type.GetType("System.String")
        dtUsers.Columns.Add(colColumn)

        '4
        colColumn = New DataColumn
        colColumn.ColumnName = "Account LockOut"
        colColumn.DataType = System.Type.GetType("System.Boolean")
        colColumn.DefaultValue = False
        dtUsers.Columns.Add(colColumn)

        '5
        colColumn = New DataColumn
        colColumn.ColumnName = "Account Approved"
        colColumn.DataType = System.Type.GetType("System.Boolean")
        colColumn.DefaultValue = True
        dtUsers.Columns.Add(colColumn)

        'create a column for each existing Role
        Dim role As String
        For Each role In Roles.GetAllRoles
            colColumn = New DataColumn
            colColumn.ColumnName = role.ToString
            colColumn.DataType = System.Type.GetType("System.Boolean")
            colColumn.DefaultValue = False
            dtUsers.Columns.Add(colColumn)
        Next
        Dim user As MembershipUser
        Dim users As MembershipUserCollection = Membership.GetAllUsers
        'add a new RowRowfor each user
        For Each user In users
            rowRow = dtUsers.NewRow
            'add the first three columns
            rowRow.Item("User Name") = user.ToString
            rowRow.Item("Creation Date") = Format(user.CreationDate, "Short Date")
            rowRow.Item("Last Login Date") = Format(user.LastLoginDate, "Short Date")
            rowRow.Item("Account LockOut") = user.IsLockedOut
            rowRow.Item("Account Approved") = user.IsApproved

            For iCounter As Int16 = 5 To dtUsers.Columns.Count - 1
                'determine which roles the user is in and add that value to the table
                If dtUsers.Columns.Count <= 5 Then
                    Exit For
                End If
                If Roles.IsUserInRole(user.ToString, dtUsers.Columns(iCounter).ColumnName) Then
                    rowRow.Item(iCounter) = True
                Else
                    rowRow.Item(iCounter) = False
                End If
            Next
            dtUsers.Rows.Add(rowRow)
        Next
        '10 minutes
        Dim ts As New TimeSpan(600000000000)
        dsUsers.Tables.Add(dtUsers)

        Me.Cache.Add("UsersDataSet", dsUsers, Nothing, DateTime.MaxValue, ts, CacheItemPriority.Normal, Nothing)

        Return dsUsers

    End Function
    Private Sub BindUserGrid(Optional ByVal SortColumn As String = "", Optional ByVal sortDirection As System.Web.UI.WebControls.SortDirection = SortDirection.Ascending)

        Dim dsUsers As DataSet = Cache.Get("UsersDataSet")
        If dsUsers Is Nothing Then
            dsUsers = GetAllUsers()
        End If
        If Session("sortColumn") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("sortColumn").ToString) AndAlso Session("sortColumn") = SortColumn Then
            If Session("sortDirection") = WebControls.SortDirection.Ascending Then
                sortDirection = WebControls.SortDirection.Descending
                dsUsers.Tables(0).DefaultView.Sort = SortColumn & " Desc"
            Else
                sortDirection = WebControls.SortDirection.Ascending
                dsUsers.Tables(0).DefaultView.Sort = SortColumn

            End If
        Else
            dsUsers.Tables(0).DefaultView.Sort = SortColumn
        End If

        Session("sortColumn") = SortColumn
        Session("sortDirection") = sortDirection

        Me.gvwUser.DataSource = dsUsers.Tables(0).DefaultView

        Me.gvwUser.DataBind()
    End Sub
    Private Function CreateProfile(ByVal userName As String) As String
        Dim newProfile As System.Web.Profile.ProfileBase
        Dim strErrorMessage As String = String.Empty
        Try
            newProfile = ProfileBase.Create(userName)
        Catch ex As Exception
            'could be an error if the profile already exists for example
            strErrorMessage = "There was a problem creating the User's Profile"
            Return strErrorMessage
            Exit Function
        End Try
        Dim profileProperty As System.Configuration.SettingsProperty
        'iterate through the profile properties
        For Each profileProperty In ProfileBase.Properties
            'find the textboxes that corresponds to the profile property
            Dim txtProfile As New TextBox
            Dim ddlProfile As New DropDownList

            'iterate through the array of row names and row indexes created when the table was created
            'to find the index of the row we are looking for based on the name of the property/row
            Dim rowIndex As Int16
            For intCounter As Int16 = 0 To _strRows.GetUpperBound(1)
                If _strRows(0, intCounter) = profileProperty.Name Then
                    rowIndex = _strRows(1, intCounter)
                    Exit For
                End If
            Next
            If profileProperty.Name <> "State" Then
                txtProfile = tableAddUser.Rows.Item(rowIndex).Cells(1).Controls(0)
                Try
                    'add the information to the new Profile
                    If Not txtProfile Is Nothing Then
                        newProfile.SetPropertyValue(profileProperty.Name, txtProfile.Text)
                        txtProfile.Text = ""
                    End If
                Catch ex As Exception
                    strErrorMessage = "There was a problem creating the User's Profile"
                    Return strErrorMessage
                    Exit Function
                End Try
            Else
                ddlProfile = tableAddUser.Rows.Item(rowIndex).Cells(1).Controls(0)
                Try
                    'add the information to the new Profile
                    If Not ddlProfile Is Nothing Then
                        newProfile.SetPropertyValue(profileProperty.Name, ddlProfile.Text)
                        ddlProfile.SelectedValue = ""
                    End If
                Catch ex As Exception
                    strErrorMessage = "There was a problem creating the User's Profile"
                    Return strErrorMessage
                    Exit Function
                End Try
            End If

        Next
        Try
            newProfile.Save()
        Catch ex As Exception
            strErrorMessage = "There was a problem creating the User's Profile"
        End Try
        Return strErrorMessage
    End Function
    Private Sub UpdateAddUserTable()
        'append Table Web Server based on custom properties of the Profile provider
        'this code needs to be executed with each round trip so the controls will be created everytime
        Dim profileProperty As System.Configuration.SettingsProperty
        Dim row As TableRow
        Dim cell As TableCell

        'set cell styles
        Dim cellLabelStyle As New TableItemStyle()
        cellLabelStyle.HorizontalAlign = HorizontalAlign.Left
        cellLabelStyle.VerticalAlign = VerticalAlign.Middle
        cellLabelStyle.Width = Unit.Pixel(120)

        Dim cellTextboxStyle As New TableItemStyle()
        cellTextboxStyle.HorizontalAlign = HorizontalAlign.Left
        cellTextboxStyle.VerticalAlign = VerticalAlign.Middle

        'iterate through the Properties of the default Profile
        For Each profileProperty In ProfileBase.Properties
            Dim txtProfile As New TextBox
            Dim ddlProfile As New DropDownList
            Dim lblProfile As New Label
            Dim valProfile As New RequiredFieldValidator
            Dim intRow As Int16

            'add a new row to the table
            row = New TableRow
            'add the label control to the 1st cell
            cell = New TableCell
            cell.ApplyStyle(cellLabelStyle)
            lblProfile.ID = "lbl" & profileProperty.Name
            lblProfile.Text = profileProperty.Name
            cell.Controls.Add(lblProfile)
            row.Cells.Add(cell)

            If profileProperty.Name <> "State" Then
                'add the textbox control to the next cell
                cell = New TableCell
                cell.ApplyStyle(cellTextboxStyle)
                txtProfile.ID = "txt" & profileProperty.Name
                cell.Controls.Add(txtProfile)
                row.Cells.Add(cell)
            Else
                'add the textbox control to the next cell
                cell = New TableCell
                cell.ApplyStyle(cellTextboxStyle)
                ddlProfile.ID = "ddl" & profileProperty.Name
                ddlProfile.Width = WebControls.Unit.Pixel(156)
                ddlProfile.DataSource = dsxmlState
                ddlProfile.DataTextField = "name"
                ddlProfile.Items.Add("")
                ddlProfile.AppendDataBoundItems = True
                ddlProfile.DataBind()
                ddlProfile.SelectedIndex = 0
                cell.Controls.Add(ddlProfile)
                row.Cells.Add(cell)
            End If


            'add a third cell for validation
            'not implemented
            cell = New TableCell
            cell.ApplyStyle(cellTextboxStyle)
            row.Cells.Add(cell)

            'add the row to the table which returns the index of the row that was just added
            intRow = tableAddUser.Rows.Add(row)

            'use this array to keep track of the indexes of the rows that are added along with 
            'the Profile property they contain
            'this is needed later to find the controls that contain the values
            ReDim Preserve _strRows(1, _strRows.GetUpperBound(1) + 1)
            _strRows(0, _strRows.GetUpperBound(1) - 1) = profileProperty.Name
            _strRows(1, _strRows.GetUpperBound(1) - 1) = intRow

        Next
        'add the roles checklist
        'contains a list of all currently defined Roles
        row = New TableRow
        cell = New TableCell
        cell.ID = "cellRoleLabel"
        Dim lblRole As New Label
        lblRole.ID = "lblNewUserRole"
        lblRole.Text = "Select Role(s)"
        cell.Controls.Add(lblRole)
        row.Cells.Add(cell)
        cell = New TableCell
        cell.ID = "cellRole"
        cell.ColumnSpan = 2
        Dim roleCheckList As CheckBoxList
        roleCheckList = New CheckBoxList
        roleCheckList.RepeatDirection = RepeatDirection.Horizontal
        Dim role As String = String.Empty
        For Each role In Roles.GetAllRoles
            roleCheckList.Items.Add(role.ToString)
        Next
        cell.Controls.Add(roleCheckList)
        row.Cells.Add(cell)
        tableAddUser.Rows.Add(row)
        _roleRowID = tableAddUser.Rows.GetRowIndex(row)
    End Sub

    Protected Sub gvwUser_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwUser.PageIndexChanging
        gvwUser.PageIndex = e.NewPageIndex
        BindUserGrid()
    End Sub

    Protected Sub gvwUser_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvwUser.RowCancelingEdit
        gvwUser.EditIndex = -1
        BindUserGrid()
    End Sub

    Protected Sub gvwUser_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvwUser.RowEditing
        gvwUser.EditIndex = e.NewEditIndex
        BindUserGrid()
        Try
            CType(Me.gvwUser.Rows(e.NewEditIndex).Cells(1).Controls(0), TextBox).Enabled = False
            CType(Me.gvwUser.Rows(e.NewEditIndex).Cells(2).Controls(0), TextBox).Enabled = False
            CType(Me.gvwUser.Rows(e.NewEditIndex).Cells(3).Controls(0), TextBox).Enabled = False

            Dim tmpCheckBox As CheckBox
            tmpCheckBox = CType(Me.gvwUser.Rows(e.NewEditIndex).Cells(4).Controls(0), CheckBox)
            If (tmpCheckBox.Checked = True) Then
                tmpCheckBox.Enabled = True
            Else
                tmpCheckBox.Enabled = False
            End If

        Catch ex As Exception
            'disallow editing
            gvwUser.EditIndex = -1
        End Try
    End Sub

    Protected Sub gvwUser_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvwUser.RowUpdating

        Dim dsUsers As DataSet = Cache.Get("UsersDataSet")
        If dsUsers Is Nothing Then
            dsUsers = GetAllUsers()
        End If
        Dim dtUsers As New DataTable
        dtUsers = dsUsers.Tables(0)

        Dim user As MembershipUser
        Dim users As MembershipUserCollection = Membership.GetAllUsers
        user = users.Item(CType(Me.gvwUser.Rows(e.RowIndex).Cells(1).Controls(0), TextBox).Text)

        Dim dr As DataRow

        Dim intCol As Int16 = 6

        For Each col As DataColumn In dtUsers.Columns
            Select Case col.ColumnName
                Case "User Name"
                Case "Creation Date"
                Case "Last Login Date"
                Case "Account LockOut"
                    If CType(Me.gvwUser.Rows(e.RowIndex).Cells(4).Controls(0), CheckBox).Checked = False AndAlso user.IsLockedOut Then
                        user.UnlockUser()
                        dr = dtUsers.Rows(e.RowIndex)
                        dr(3) = False
                    End If
                Case "Account Approved"
                    If CType(Me.gvwUser.Rows(e.RowIndex).Cells(5).Controls(0), CheckBox).Checked = False AndAlso user.IsApproved Then
                        user.IsApproved = False
                    ElseIf CType(Me.gvwUser.Rows(e.RowIndex).Cells(5).Controls(0), CheckBox).Checked = True AndAlso user.IsApproved = False Then
                        user.IsApproved = True
                    End If
                    Membership.UpdateUser(user)
                    dr = dtUsers.Rows(e.RowIndex)
                    dr(4) = user.IsApproved
                Case Else
                    For intCol = intCol To dtUsers.Columns.Count
                        Select Case CType(Me.gvwUser.Rows(e.RowIndex).Cells(intCol).Controls(0), CheckBox).Checked
                            Case True
                                If Not Roles.IsUserInRole(user.UserName, col.ColumnName) Then
                                    'role selected and user NOT in role, add user to role
                                    Roles.AddUserToRole(user.UserName, col.ColumnName)
                                    'update datatable
                                    dr = dtUsers.Rows(e.RowIndex)
                                    dr(intCol - 1) = True
                                End If
                                Exit For
                            Case False
                                If Roles.IsUserInRole(user.UserName, col.ColumnName) Then
                                    'role NOT selected and user IS in role, delete user from role
                                    Roles.RemoveUserFromRole(user.UserName, col.ColumnName)

                                    'update datatable
                                    dr = dtUsers.Rows(e.RowIndex)
                                    dr(intCol - 1) = False
                                End If
                                Exit For
                        End Select
                    Next

                    intCol += 1

            End Select
        Next

        dsUsers.AcceptChanges()

        Dim ts As New TimeSpan(600000000000)

        Me.Cache.Add("UsersDataSet", dsUsers, Nothing, DateTime.MaxValue, ts, CacheItemPriority.Normal, Nothing)

        gvwUser.EditIndex = -1

        Me.gvwUser.DataSource = dsUsers
        Me.gvwUser.DataMember = "Users"
        Me.gvwUser.DataBind()

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dsUsers As DataSet = Cache.Get("UsersDataSet")
        If dsUsers Is Nothing Then
            dsUsers = GetAllUsers()
        End If

        Dim dvSortedUsers As New DataView
        dvSortedUsers.Table = dsUsers.Tables(0)

        dvSortedUsers.RowFilter = "[User Name] LIKE '" & Me.txtSearch.Text & "%'"


        Me.gvwUser.DataSource = dvSortedUsers.ToTable("Users")
        Me.gvwUser.DataBind()
    End Sub

    Protected Sub btnRemoveFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveFilter.Click
        BindUserGrid()
    End Sub

    Protected Sub gvwUser_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvwUser.Sorting
        BindUserGrid(e.SortExpression)

    End Sub

    Protected Sub lbtnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnRefresh.Click
        Cache.Remove("UsersDataSet")
        BindUserGrid()
    End Sub
End Class
