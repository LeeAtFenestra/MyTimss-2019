
Imports Westat.TIMSS.BLL
Imports Westat.TIMSS.HL
Imports System.Data

Partial Class DistrictEdit
    Inherits BasePagePublic

    Public ReadOnly Property DistrictId() As String
        Get
            Return Server.HtmlEncode(Request.QueryString("d"))
        End Get
    End Property

    Public ReadOnly Property HasDistrictId() As Boolean
        Get
            Return Not String.IsNullOrEmpty(DistrictId())
        End Get
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        If Not HasDistrictId() Then
            Return
        End If

        Dim dr As DataRow = timssbll.GetDistrictDetailsDataRow(Me.DistrictId)


        db_d_name.Text = dr("d_name")
        TIMSSID.Text = dr("leaid")
        db_d_addr1.Text = IIf(dr("D_Addr1") Is DBNull.Value, "", dr("D_Addr1"))
        db_d_addr2.Text = IIf(dr("D_Addr2") Is DBNull.Value, "", dr("D_Addr2"))
        db_d_city.Text = IIf(dr("D_City") Is DBNull.Value, "", dr("D_City"))
        db_d_state.Text = IIf(dr("D_State") Is DBNull.Value, "", dr("D_State"))
        db_d_zip.Text = IIf(dr("D_Zip") Is DBNull.Value, "", dr("D_Zip"))
        db_d_phone.Text = IIf(dr("D_Phone") Is DBNull.Value, "", dr("D_Phone"))
        db_d_fax.Text = IIf(dr("D_Fax") Is DBNull.Value, "", dr("D_Fax"))

        'd_partltrsentdt.Items.Clear()
        db_d_partltrsentdt.DataSource = TimssBll.GetDateRangeNameValuePairArrayList(True, New Date(2016, 4, 1), New Date(2018, 4, 30))
        db_d_partltrsentdt.DataBind()

        TimssBll.SetDropDownListSelectedValue(db_d_partltrsentdt, IIf(dr("d_partltrsentdt") Is DBNull.Value, "", dr("d_partltrsentdt")))
        'Try
        '    db_d_partltrsentdt.SelectedValue = IIf(dr("d_partltrsentdt") Is DBNull.Value, "", dr("d_partltrsentdt"))
        'Catch ex As Exception

        'End Try


        Dim dtpersonel As DataTable = timssbll.GetDistrictPersonnelDataTable(Me.DistrictId)


        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "autosave", "<script language=javascript>window.onbeforeunload = BeforeUnloadAutoSave</script>")
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "checkforedit", "<script language=javascript>window.onbeforeunload = on_lose_focus</script>")

        'Dim bodytag As HtmlGenericControl = Master.FindControl("mBody")
        'bodytag.Attributes.Add("onbeforeunload", "return  BeforeUnloadAutoSave();")

        'SuperintendentHyperLink.NavigateUrl = "~/DistrictPersonnelEdit.aspx?fldname=d_super&d=" & Me.DistrictId
        'TestDirectorHyperLink.NavigateUrl = "~/DistrictPersonnelEdit.aspx?fldname=d_tDirector&d=" & Me.DistrictId
        'DistrictContactHyperLink.NavigateUrl = "~/DistrictPersonnelEdit.aspx?fldname=d_contact&d=" & Me.DistrictId

        HrefSuperintendent.HRef = "javascript:if(Personnelautosave()){popUp('DistrictPersonnelEdit.aspx?d=" & Me.DistrictId() & "&fldname=d_super', 'mywin', 600, 400,1)}"
        HrefSuperintendent.InnerText = "Add"

        HrefTestDirector.HRef = "javascript:if(Personnelautosave()){popUp('DistrictPersonnelEdit.aspx?d=" & Me.DistrictId() & "&fldname=d_tDirector', 'mywin', 600, 400,1)}"
        HrefTestDirector.InnerText = "Add"

        HrefDistrictContact.HRef = "javascript:if(Personnelautosave()){popUp('DistrictPersonnelEdit.aspx?d=" & Me.DistrictId() & "&fldname=d_contact', 'mywin', 600, 400,1)}"
        HrefDistrictContact.InnerText = "Add"

        If dtpersonel.Rows.Count < 2 Then
            db_d_super.Visible = False
            db_d_tDirector.Visible = False
            db_d_contact.Visible = False
        Else

            db_d_super.Visible = True
            db_d_tDirector.Visible = True
            db_d_contact.Visible = True

            HrefSuperintendent.HRef = "javascript:if(Personnelautosave()){popUp('DistrictPersonnelEdit.aspx?d=" & Me.DistrictId() & "&fldname=d_super&pid=' + getFrm().elements['" & db_d_super.UniqueID & "'].options[getFrm().elements['" & db_d_super.UniqueID & "'].selectedIndex].value, 'mywin', 600, 400,1)}"
            HrefTestDirector.HRef = "javascript:if(Personnelautosave()){popUp('DistrictPersonnelEdit.aspx?d=" & Me.DistrictId() & "&fldname=d_tDirector&pid=' + getFrm().elements['" & db_d_tDirector.UniqueID & "'].options[getFrm().elements['" & db_d_tDirector.UniqueID & "'].selectedIndex].value, 'mywin', 600, 400,1)}"
            HrefDistrictContact.HRef = "javascript:if(Personnelautosave()){popUp('DistrictPersonnelEdit.aspx?d=" & Me.DistrictId() & "&fldname=d_contact&pid=' + getFrm().elements['" & db_d_contact.UniqueID & "'].options[getFrm().elements['" & db_d_contact.UniqueID & "'].selectedIndex].value, 'mywin', 600, 400,1)}"

            db_d_super.DataSource = dtpersonel
            db_d_super.DataBind()

            Dim d_super As Integer = IIf(dr("d_super") Is DBNull.Value, 0, dr("d_super"))
            Dim d_tDirector As Integer = IIf(dr("d_tDirector") Is DBNull.Value, 0, dr("d_tDirector"))
            Dim d_contact As Integer = IIf(dr("d_contact") Is DBNull.Value, 0, dr("d_contact"))
            TimssBll.SetDropDownListSelectedValue(db_d_super, d_super)
            'Try
            '    db_d_super.SelectedValue = d_super
            'Catch ex As Exception

            'End Try

            If d_super <> 0 Then
                HrefSuperintendent.InnerText = "Edit"
            End If

            db_d_tDirector.DataSource = dtpersonel
            db_d_tDirector.DataBind()
            TimssBll.SetDropDownListSelectedValue(db_d_tDirector, d_tDirector)

            'Try
            '    db_d_tDirector.SelectedValue = d_tDirector
            'Catch ex As Exception

            'End Try

            If d_tDirector <> 0 Then
                HrefTestDirector.InnerText = "Edit"
            End If

            db_d_contact.DataSource = dtpersonel
            db_d_contact.DataBind()
            TimssBll.SetDropDownListSelectedValue(db_d_contact, d_contact)

            'Try
            '    db_d_contact.SelectedValue = d_contact
            'Catch ex As Exception

            'End Try

            If d_contact <> 0 Then
                HrefDistrictContact.InnerText = "Edit"
            End If
        End If
        

        db_d_comment.Text = IIf(dr("d_comment") Is DBNull.Value, "", dr("d_comment"))

        'Dim args As New HL.GetSchoolsForDistrictSqlDataSourceArgs()
        'Dim SqlDataSource1 As SqlDataSource = timssbll.GetSchoolsForDistrictSqlDataSource(args)

        Dim shoollistargs As New GetSchoolListSqlDataSourceArgs()



        shoollistargs.SortParameters.Add(New SortParameter("S_Name", SortDirection.Ascending))

        shoollistargs.FilterParameters.Add(New FilterParameter("leaid", Me.DistrictId, "equals"))

        'args.FilterColumn = "leaid"
        'args.FilterValue = dr("leaid")
        'args.ComparisonOperator = "equals"
        'args.SortColumn = "S_Name"
        'args.SortDirection = SortDirection.Ascending

        'Dim SqlDataSource1 As SqlDataSource = timssbll.GetSchoolListSqlDataSource(shoollistargs)

        Dim dt As DataTable = timssbll.GetSchoolListDataTable(shoollistargs)
        'Me.Page.Controls.Add(SqlDataSource1)
        'RepeaterSchoolList.DataSource = timssbll.GetSchoolsForDistrictSqlDataSource(args)
        'RepeaterSchoolList.DataSource = SqlDataSource1
        RepeaterSchoolList.DataSource = dt
        RepeaterSchoolList.DataBind()

    End Sub

    Protected Sub ButtonSave_Click(sender As Object, e As System.EventArgs) Handles ButtonSave.Click, ButtonSave2.Click

        TimssBll.SaveDistrictEditChanges(Me.Form.Controls, Me.DistrictId)
        BindData()

    End Sub

End Class
