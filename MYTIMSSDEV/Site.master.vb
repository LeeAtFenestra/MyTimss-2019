Imports Westat.TIMSS.BLL

Partial Class Site
    Inherits System.Web.UI.MasterPage

    Private mTimssBll As TIMSSBLL
    Public ReadOnly Property TimssBll() As TIMSSBLL
        Get
            If mTimssBll Is Nothing Then
                mTimssBll = New TIMSSBLL()
            End If
            Return mTimssBll
        End Get
    End Property

    Public Function HighlightMenuItemCSS(targetList As String) As String
        Dim result As String = "menu"
        Dim targets As String() = targetList.Split("|")
        For Each target As String In targets
            If Request.ServerVariables("SCRIPT_NAME").ToLower().EndsWith(target.ToLower()) Then
                result = "menuOn"
                Exit For
            End If
        Next
        Return result
    End Function

    'Private prof As ProfileCommon = ProfileCommon.GetUserProfile()

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        '   Dim currentabsolute As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim currentabsolute As String = System.Web.HttpContext.Current.Request.Url.AbsoluteUri

        'If currentabsolute.Contains("mytimss.com") Or Request.Url.ToString().ToLower().Contains("www.myicils.com") Then

        ' lblMyschool.Text = currentabsolute
        '  End If

        If Me.TimssBll.iseTIMSS() Then
            lblMyschool.Text = "MyTIMSS"
            IMG1.Visible = True
            IMG3.Visible = False
            rowColor1.Style.Add("background-color", "#FFC000")
            rowColor2.Style.Add("background-color", "#FFC000")
        ElseIf Me.TimssBll.isICILS() Then
            lblMyschool.Text = "MyICILS"
            IMG1.Visible = False
            IMG3.Visible = True
            rowColor1.Style.Add("background-color", "#E66057")
            rowColor2.Style.Add("background-color", "#E66057")
        ElseIf currentabsolute.Contains("mytimss.com") Or currentabsolute.Contains("mytimssdev.westat.com") Or currentabsolute.Contains("mytimsstst.wesdemo.com") Or currentabsolute.Contains("mytimssdemo.wesdemo.com") Then
            lblMyschool.Text = "MyTIMSS"
            IMG1.Visible = True
            IMG3.Visible = False
            rowColor1.Style.Add("background-color", "#FFC000")
            rowColor2.Style.Add("background-color", "#FFC000")
        ElseIf currentabsolute.Contains("myicils.com") Then
            lblMyschool.Text = "MyICILS"
            IMG1.Visible = False
            IMG3.Visible = True
            rowColor1.Style.Add("background-color", "#E66057")
            rowColor2.Style.Add("background-color", "#E66057")
        End If

        'LoginView1.DataBind()
        If HttpContext.Current.User.Identity.IsAuthenticated Then

            If Me.TimssBll.iseTIMSS() Then
                footerTIMSS.Visible = True
            ElseIf Me.TimssBll.isICILS() Then
                footerICILS.Visible = True
            End If

            'Stops user from getting into TIMSS from the document page
            '  If Request.Url.ToString().Contains("www.mytimss.com") Then
            If Page.User.Identity.Name.ToString().ToLower() = "prin2015" Or Page.User.Identity.Name.ToString().ToLower() = "testnces" Or Page.User.Identity.Name.ToString().ToLower() = "reports2015" Then
                    Session.Abandon()
                    FormsAuthentication.SignOut()
                    Response.Redirect("~/")
                    Exit Sub
                End If
                'End If


                If Not TIMSSBLL.IsMyTIMSSUser Then
                    If Not String.IsNullOrEmpty(Request.QueryString("s")) Then
                        TimssBll.Frame_N_ = Server.HtmlEncode(Request.QueryString("s"))
                        TimssBll.InitSchoolSessionInfo()
                        Dim url As String = Request.ServerVariables("PATH_INFO")
                        If Not String.IsNullOrEmpty(Request.QueryString("f")) Then
                            url = url & "?f=" & Request.QueryString("f")
                        End If
                        Response.Redirect(url)
                    End If
                End If

                If Not TimssBll.HasFrame_N_ Then

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>alert('Select a school to continue!');javascript:openAWindow('" & Page.ResolveClientUrl("~/") & "SelectSchool.aspx', 'SelectSchool', 500, 500,1)</script>")
                End If

                If Not Me.LoginView1.FindControl("spanscs") Is Nothing Then
                    Dim spanscs = Me.LoginView1.FindControl("spanscs")
                    spanscs.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanAccessSCS()
                End If


                If Not Me.LoginView1.FindControl("spanefile") Is Nothing Then
                    Dim spanefile = Me.LoginView1.FindControl("spanefile")
                    spanefile.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanAccessEfile()
                End If


                If Not Me.LoginView1.FindControl("spanchangeschool") Is Nothing Then
                    Dim spanchangeschool = Me.LoginView1.FindControl("spanchangeschool")
                    spanchangeschool.Visible = Westat.TIMSS.BLL.TIMSSBLL.CanChangeSchools()
                End If

            End If

        'If Not IsPostBack Then

        '    Me.DataBind()
        'End If
        'If Not IsPostBack Then
        '    If HttpContext.Current.User.Identity.IsAuthenticated Then
        '        If String.IsNullOrEmpty(Session("SelectedGrade")) Then
        '            Session("SelectedGrade") = "4820205"
        '            'Response.Redirect(Request.ServerVariables("PATH_INFO"))
        '        Else
        '            Dim ddl As DropDownList = Me.LoginView4.FindControl("SelectedSchool")
        '            ddl.SelectedValue = Session("SelectedGrade")
        '        End If
        '    End If
        'Else
        '    'If HttpContext.Current.User.Identity.IsAuthenticated Then
        '    '    Dim ddl As DropDownList = Me.LoginView4.FindControl("SelectedSchool")
        '    '    ddl.SelectedValue = Session("SelectedGrade")
        '    'End If
        'End If

    End Sub

    Sub HeadLoginStatus_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs)
        TimssBll.CleanUpSessionVariables()
    End Sub
End Class

