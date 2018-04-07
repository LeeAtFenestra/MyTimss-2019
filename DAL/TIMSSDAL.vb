Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.UI
Imports Westat.TIMSS.HL

Public Class TIMSSDAL

    Protected strConnMembership As String = ConfigurationManager.ConnectionStrings("FAB_DefaultConnectionString").ConnectionString
    Protected strConn As String = ConfigurationManager.ConnectionStrings("FAB_DefaultConnectionString").ConnectionString
    Private Const cFilterShortcutOrginalSchools As String = "org"
    Private Const cFilterShortcutSubstituteSchools As String = "sub"
    Private Const cFilterShortcutPendingSchools As String = "pending"
    Private Const cFilterShortcutInitialContactPendingSchools As String = "initcontpending"
    Private Const cFilterShortcutCooperatingSchools As String = "coop"
    Private Const cFilterShortcutCooperatingPendingSchools As String = "cooppending"
    Private Const cFilterShortcutRefusingSchools As String = "refusing"

    Public Function GetPageSizeNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        'results.Add(New NameValuePair("2", "2"))
        'results.Add(New NameValuePair("5", "5"))
        results.Add(New NameValuePair("15", "15"))
        results.Add(New NameValuePair("30", "30"))
        results.Add(New NameValuePair("50", "50"))
        results.Add(New NameValuePair("100", "100"))

        Return results
    End Function

    Public Function GetDispCodesNameValuePairArrayList() As ArrayList
        Return GetDispCodesNameValuePairArrayList(False)
    End Function

    Public Function GetDispCodesNameValuePairArrayList(displayCodeInText As Boolean) As ArrayList
        Dim results As New ArrayList



        Dim dt As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.DISP, DispName " &
                    "from	tblSCSGradeDispositionCodes a " &
            "Where	a.fldProjectID = 780 " &
             " order by sortindx "

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            For Each row As DataRow In dt.Rows
                results.Add(New NameValuePair(IIf(displayCodeInText, row("DISP") & " - ", "").ToString() & row("DispName").ToString(), row("DISP")))
            Next
        End Using
        Return results

    End Function

    Public Function GetParticipateIfNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("No Reason Given", "1"))
        results.Add(New NameValuePair("Not Under Any Circumstance", "2"))
        results.Add(New NameValuePair("If Assessment was Out-of-hours (e.g., Saturday)", "3"))
        results.Add(New NameValuePair("If Assessment was In Fall/Winter", "4"))
        results.Add(New NameValuePair("Need to Get on School Calendar Earlier", "5"))
        results.Add(New NameValuePair("Personal Visit from PISA Representative", "6"))
        results.Add(New NameValuePair("Increased Compensation", "7"))
        results.Add(New NameValuePair("Feedback on School/Student Performance", "8"))
        results.Add(New NameValuePair("Sample Less Students", "9"))
        results.Add(New NameValuePair("Reduce the Amount of Time Required", "10"))
        results.Add(New NameValuePair("Other (Please specify)", "11"))

        Return results
    End Function

    Public Function GetRespondentAttitudeNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("Engaged", "Engaged"))
        results.Add(New NameValuePair("Netural", "Netural"))
        results.Add(New NameValuePair("Not interested, refusing to participate", "Not interested, refusing to participate"))

        Return results
    End Function
    Public Function GetEROCCodeNameValuePairArrayList(displayCodeInText As Boolean) As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair(IIf(displayCodeInText, "20 - ", "").ToString() & "Will participate/Cooperating", "20"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "21 - ", "").ToString() & "Interested in knowing more about the study", "21"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "22 - ", "").ToString() & "Previous participant, familiar with study", "22"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "23 - ", "").ToString() & "Consensus needed", "23"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "24 - ", "").ToString() & "Remail requested", "24"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "25 - ", "").ToString() & "E-materials requested", "25"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "30 - ", "").ToString() & "No interest in study", "30"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "31 - ", "").ToString() & "Too much testing, conflict with other testing", "31"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "32 - ", "").ToString() & "Disruption/Time away from classroom instruction", "32"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "33 - ", "").ToString() & "Burden on teaching staff", "33"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "34 - ", "").ToString() & "Parents would not approve", "34"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "35 - ", "").ToString() & "Voluntary, school does not have to participate", "35"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "36 - ", "").ToString() & "Nothing in it for school or students", "36"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "40 - ", "").ToString() & "Ring No answer/busy", "40"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "41 - ", "").ToString() & "Voicemail/Message taken", "41"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "70 - ", "").ToString() & "Initial out of scope", "70"))
        results.Add(New NameValuePair(IIf(displayCodeInText, "71 - ", "").ToString() & "Tragedy or other school related issue, specify", "71"))

        Return results
    End Function

    Public Function GetDateRangeNameValuePairArrayList(AddNoneOption As Boolean, StartDate As Date, EndDate As Date) As ArrayList
        Dim results As New ArrayList
        If AddNoneOption Then
            results.Add(New NameValuePair("No Available Date", ""))
        End If

        'Dim currentDate As New Date(2014, 5, 5)
        'Dim endDate As New Date(2015, 3, 31)
        '.ToString("MM/dd/yyyy")
        While StartDate <= EndDate
            results.Add(New NameValuePair(StartDate.ToString("MM/dd/yyyy"), StartDate))
            StartDate = StartDate.AddDays(1)
        End While
        Return results
    End Function



    Public Function GetSampleListAvailableNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("No", "N"))
        results.Add(New NameValuePair("Yes", "Y"))
        results.Add(New NameValuePair("Yes, follow-up needed", "F"))

        Return results
    End Function

    Public Function GetStudentSampledNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("No", "NO"))
        results.Add(New NameValuePair("Yes", "YES"))

        Return results
    End Function

    Public Function GetParentLetterTypeNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("", ""))
        results.Add(New NameValuePair("Notification", "NOTIFICATION"))
        results.Add(New NameValuePair("Implicit", "IMPLICIT"))
        results.Add(New NameValuePair("Explicit", "EXPLICIT"))

        Return results
    End Function

    Public Function GetParentLetterLngNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("", ""))
        results.Add(New NameValuePair("English", "ENGLISH"))
        results.Add(New NameValuePair("Spanish", "SPANISH"))
        results.Add(New NameValuePair("Both", "Both"))

        Return results
    End Function

    Public Function GetPreassessmentNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("No", "N"))
        results.Add(New NameValuePair("Yes", "Y"))

        Return results
    End Function

    Public Function GetAssessmentNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("N/A", ""))
        results.Add(New NameValuePair("No", "NO"))
        results.Add(New NameValuePair("Yes", "YES"))
        results.Add(New NameValuePair("Make-up required", "MAKE-UP REQUIRED"))

        Return results
    End Function


    Public Function GetMarkNCSNameValuePairArrayList() As ArrayList
        Dim results As New ArrayList

        results.Add(New NameValuePair("No", "N"))
        results.Add(New NameValuePair("Yes", "Y"))

        Return results
    End Function

    Public Sub HandleTableUpdate(NameValuePairList As List(Of NameValuePair), primarykeyvalue As String, table As String, primarykeyname As String)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn

            Dim sql As String = "update [" & table & "]"
            Dim name As String = ""
            Dim value As String = ""
            Dim cnt As Integer = 0
            Dim bytes As Byte()

            For Each nv As NameValuePair In NameValuePairList
                name = nv.Name
                value = nv.Value
                bytes = nv.Bytes()


                If Not value Is Nothing Or Not bytes Is Nothing Then
                    If Not bytes Is Nothing Then
                        cmd.Parameters.AddWithValue(name, bytes)
                        sql = sql & vbCrLf & IIf(cnt > 0, ", ", "set ") & "[" & name & "] = @" & name & ""
                    ElseIf value.Equals("getdate()", StringComparison.CurrentCultureIgnoreCase) Then
                        sql = sql & vbCrLf & IIf(cnt > 0, ", ", "set ") & "[" & name & "] = GETDATE()"
                    Else
                        cmd.Parameters.AddWithValue(name, value)
                        sql = sql & vbCrLf & IIf(cnt > 0, ", ", "set ") & "[" & name & "] = " & "@" & name & ""
                    End If
                Else
                    sql = sql & vbCrLf & IIf(cnt > 0, ", ", "set ") & "[" & name & "] = null"
                End If
                cnt = cnt + 1
            Next

            sql = sql & vbCrLf & "Where	[" & primarykeyname & "] = @" & primarykeyname

            cmd.Parameters.AddWithValue(primarykeyname, primarykeyvalue)

            cmd.CommandText = sql

            cmd.ExecuteNonQuery()

        End Using
    End Sub
    Public Sub HandleTableUpdate(DbControls As List(Of Control), primarykeyvalue As String, table As String, primarykeyname As String)

        HandleTableUpdate(ListOfControlsToNameValuePairList(DbControls), primarykeyvalue, table, primarykeyname)

    End Sub

    Private Function ListOfControlsToNameValuePairList(DbControls As List(Of Control)) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        Dim name As String = ""
        Dim value As String = ""

        For Each ctrl As Control In DbControls
            name = ParseDbFieldName(ctrl.ID)
            value = ParseDbValue(ctrl)
            If Not value Is Nothing Then
                results.Add(New NameValuePair(name, value))
            Else
                results.Add(New NameValuePair(name, Nothing))
            End If
        Next
        Return results
    End Function

    Public Function HandleTableInsert(DbControls As List(Of Control), table As String, returnidentity As Boolean) As Integer

        Return HandleTableInsert(ListOfControlsToNameValuePairList(DbControls), table, returnidentity)

    End Function


    Public Function HandleTableInsert(NameValuePairList As List(Of NameValuePair), table As String, returnidentity As Boolean) As Integer
        Dim result As Integer = -1
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn

            Dim sql As String = "insert into [" & table & "]"
            Dim name As String = ""
            Dim value As String = ""
            Dim cnt As Integer = 0
            Dim fields As String = ""
            Dim values As String = ""
            Dim bytes As Byte()

            For Each nv As NameValuePair In NameValuePairList
                name = nv.Name
                value = nv.Value
                bytes = nv.Bytes()

                If value Is Nothing And bytes Is Nothing Then
                    fields = fields & IIf(cnt > 0, ", ", "") & "[" & name & "]"
                    values = values & IIf(cnt > 0, ", ", "") & "null"
                Else
                    If Not bytes Is Nothing Then
                        cmd.Parameters.AddWithValue(name, bytes)
                        fields = fields & IIf(cnt > 0, ", ", "") & "[" & name & "]"
                        values = values & IIf(cnt > 0, ", ", "") & "@" & name & ""
                    ElseIf value.Equals("getdate()", StringComparison.CurrentCultureIgnoreCase) Then
                        fields = fields & IIf(cnt > 0, ", ", "") & "[" & name & "]"
                        values = values & IIf(cnt > 0, ", ", "") & "GETDATE()"
                    Else
                        If Not value Is Nothing Then
                            cmd.Parameters.AddWithValue(name, value)
                            fields = fields & IIf(cnt > 0, ", ", "") & "[" & name & "]"
                            values = values & IIf(cnt > 0, ", ", "") & "@" & name & ""
                        Else

                            fields = fields & IIf(cnt > 0, ", ", "") & "[" & name & "]"
                            values = values & IIf(cnt > 0, ", ", "") & "null"
                        End If
                    End If
                End If

                cnt = cnt + 1
            Next

            sql = sql & " (" & fields & ") values (" & values & ")"

            If returnidentity Then
                sql = sql & " SELECT @IDENTITY = SCOPE_IDENTITY()"
                cmd.Parameters.Add("IDENTITY", SqlDbType.Int).Direction = ParameterDirection.Output
            End If



            cmd.CommandText = sql

            cmd.ExecuteNonQuery()

            If returnidentity Then
                result = cmd.Parameters("IDENTITY").Value
            End If

        End Using

        Return result
    End Function

    Public Sub SaveDistrictEditChanges(args As SaveChangesArgsBase)

        HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblscsDistrict", "leaid")

    End Sub

    Public Function ParseDbFieldName(id As String) As String
        If id.IndexOf("_") = -1 Then
            Return id
        Else
            Return id.Substring(id.IndexOf("_") + 1)
        End If
    End Function


    Public Function ParseDbValue(control As Control) As String
        Dim result As String = ""
        If control.GetType() Is GetType(DropDownList) Then
            Dim d As DropDownList = control
            result = d.SelectedValue
            If result = "No Available Date" Then
                result = ""
            End If
        ElseIf control.GetType() Is GetType(HiddenField) Then
            Dim d As HiddenField = control
            result = d.Value
        Else
            Dim d As TextBox = control
            result = d.Text
        End If
        If String.IsNullOrEmpty(result) Then
            result = Nothing
        End If
        Return result
    End Function

    Public Sub SaveSchoolEditChanges(args As SaveSchoolEditChangesArgs)
        HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblscsschool", "frame_n_")
        HandleTableUpdate(args.GradeDbControls, args.GradeId, "tblscsgrade", "ID")
    End Sub

    Public Function GetDistrictListDataTable(args As GetDistrictListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.D_Name " &
                                  "       ,a.LEAID " &
                                  "       ,a.D_Super " &
                                  "       ,a.D_Contact " &
                                  "       ,a.td_Name " &
                                  "       ,a.D_City " &
                                  "       ,a.D_State " &
                                "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then

                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetSchoolListDataTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.ID " &
                      "       ,a.fldProjectDesc Project " &
                      "       ,a.DispName " &
                      "       ,a.testregion " &
                      "       ,a.fldAreaCode " &
                      "       ,a.S_County " &
                      "       ,a.ScheDate " &
                      "       ,a.ScheTime " &
                      "       ,a.fldTerritoryCode " &
                      "       ,a.S_State " &
                      "       , case a.isPublic when 1 then 'Public' else 'Private' end [SchoolType]" &
                      "       ,a.D_Name " &
                      "       ,a.REPSBGRP " &
                      "       ,a.STLFUploaded " &
                      "       ,a.STLFUploadedText " &
                      "       ,a.STLFUserFilePath " &
                       "       ,a.[Study_Name] " &
                       "       ,a.[MyNAEPRegID] " &
                    "from	uv_Customize a "
            ' Removed 9/14/2017 - DCB
            '"       ,a.ScheDate2 " &
            '"       ,a.ScheTime2 " &



            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(IIf(f.FilterColumn = "project", "fldProjectDesc", f.FilterColumn) & f.Index, f.FilterValue)
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc").Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc")
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc")
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetSchoolsEROCDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.EROCID " &
                      "       ,a.frame_n_ " &
                      "       ,a.id " &
                      "       ,a.PersonContactTitle " &
                      "       ,a.PersonContacted " &
                      "       ,a.DateContacted " &
                      "       ,a.ContactMode " &
                      "       ,a.OutcomeOfTheCall " &
                      "       ,a.Disp " &
                      "       ,a.AdditionalNotes " &
                      "       ,a.UpdatedbyFirstAndLastName Updatedby " &
                      "       ,a.DispName " &
                    "from	[uv_EROC] a "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            If args.SortParameters.Count > 0 Then
                cnt = 0
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetSchoolDetailsDataRow(args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.S_Name " &
                                  "       ,a.ID " &
                                  "       ,a.SMPGRD " &
                                  "       ,a.Disp " &
                                  "       ,a.fldRegionCode " &
                                  "       ,a.fldAreaCode " &
                                  "       ,a.S_County " &
                                  "       ,a.fldTerritoryCode " &
                                  "       ,a.D_Name " &
                                "       ,a.Expr1 Project " &
                                "       ,a.S_Addr1 " &
                                "       ,a.S_Addr2 " &
                                "       ,a.S_City " &
                                "       ,a.S_State " &
                                "       ,a.S_Zip " &
                                "       ,a.S_County " &
                                "       ,a.S_Phone " &
                                "       ,a.S_Fax " &
                                "       ,a.leaid " &
                                "       ,a.MailAddr1 " &
                                "       ,a.MailAddr2 " &
                                "       ,a.MailCity " &
                                "       ,a.MailState " &
                                "       ,a.MailZip " &
                                "       ,a.TypeName " &
                                "       ,a.ESTGRE " &
                                "       ,a.s_comment " &
                                "       ,a.ArrivalTime " &
                                "       ,a.ScheTime " &
                                "       ,a.ScheDate " &
                                "       ,a.sch_partltrsentdt " &
                                "       ,a.SchAsmtLtrSentDT " &
                                "       ,a.AugSchLtrSentDT " &
                                "       ,a.principalID " &
                                "       ,a.coordinatorid " &
                                "       ,a.SEASCH " &
                                "       ,a.ChgTIMSSDISPComments " &
                                "       ,a.fldProjectID " &
                                "       ,a.EnrollmentAtGrade " &
                                "       ,a.NumberOfMathClasses " &
                                "       ,a.NumberOfClasses " &
                                "       ,a.MyNAEPREGID " &
                                "       ,a.DateSchoolReturnsFromSpringBreak " &
                                "       ,a.DateSchoolReturnsFromWinterBreak " &
                                "       ,a.LastDayofSchool " &
                                "       ,a.testregion " &
                                "       ,a.AdvancedEligibility " &
                                "       ,a.AdvancedEligibilityComments " &
                                "       ,a.AdvancedMathComments " &
                                "       ,a.AdvancedPhysicsComments " &
                                "       ,a.PrincipalIncentiveCheckBatchID " &
                                "       ,a.SCIncentiveCheckBatchID " &
                                "       ,a.SchoolIncentiveCheckSentDT " &
                                "       ,a.SCIncentiveCheckSentDT " &
                                "       ,a.UPSNumber1 " &
                                "       ,a.UPSNumber2 " &
                                "       ,a.FedExNumber1 " &
                                "       ,a.FedExNumber2 " &
                                "       ,a.SchoolIncentiveCheckSentTxt " &
                                "       ,a.SCIncentiveCheckSentTxt " &
                                "       ,a.AssessmentLocation " &
                                "       ,a.AssessmentDayLogisticsInformation " &
                                "       ,a.ParentConsentType " &
                                "       ,a.ParentConsentLanguage " &
                                "       ,a.PreAssessmentCallCompleted " &
                                "       ,a.AssessmentCompleted " &
                                "       ,a.DateOfMakeUp " &
                                "       ,a.AssessmentMaterialsMailedToPearson " &
                                "       ,a.fldProjectDesc " &
                                "       ,a.isICILS " &
                                "       ,a.isETIMSS " &
                                "       ,a.ScheDate2 " &
                                "       ,a.ScheTime2 " &
                                "       ,a.ArrivalTime2 " &
                                "       ,a.REPSBGRP " &
                                "       ,a.ORIGSUB " &
                                "       ,a.IEA_ID " &
                                "       ,a.SPECIAL_CASE " &
                                "from	sgep_General a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            da = New SqlDataAdapter(sql, objConn)

            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function


    Public Function GetDistrictDetailsDataRow(args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select distinct a.D_Name " &
                              "       ,a.LEAID " &
                              "       ,a.D_Super " &
                              "       ,a.d_tDirector " &
                              "       ,a.D_Contact " &
                              "       ,a.D_City " &
                              "       ,a.D_State " &
                              "       ,a.D_Addr1 " &
                              "       ,a.D_Addr2 " &
                              "       ,a.D_Zip " &
                              "       ,a.D_Phone " &
                              "       ,a.D_Fax " &
                              "       ,a.d_comment " &
                              "       ,a.d_partltrsentdt " &
                            "from	tblSCSDistrict a "

            Dim cnt As Integer = 0


            If args.IsNaepStateCoordinator Then
                sql = sql & IIf(cnt = 0, " where ", " and ") & "a.[LEAID] in (select distinct a.LEAID from uv_Customize a where	a.REPSBGRP = @REPSBGRP and isPublic = 1)"
                cnt = cnt + 1
            ElseIf args.IsFieldDirector Or args.IsFieldManager Then
                sql = sql & IIf(cnt = 0, " where ", " and ") & "a.[LEAID] in (select distinct a.LEAID from uv_Customize a join NAEPFRS2015.[dbo].[uv_TIMSSStaffTRA] b on b.fldTerritoryCode = a.fldTerritoryCode " & IIf(args.IsFieldManager, " and b.fldRegionCode = a.fldRegionCode ", "") & "  where	b.fldWINSID = @fldWINSID)"
                cnt = cnt + 1
            End If

            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            da = New SqlDataAdapter(sql, objConn)

            If args.IsNaepStateCoordinator Then
                da.SelectCommand.Parameters.AddWithValue("@REPSBGRP", args.REPSBGRP)
            ElseIf args.IsFieldDirector Or args.IsFieldManager Then
                da.SelectCommand.Parameters.AddWithValue("@fldWINSID", args.WINSID)
            End If
            'da.SelectCommand.Parameters.AddWithValue("@leaid", leaid)

            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next


            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function



    Public Function GetDistrictPersonnelDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select fullname, pid, fname, lname " &
                    "from	web_district_personnel "
            Dim cnt As Integer = 0

            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            If args.SortParameters.Count > 0 Then
                cnt = 0
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)


            Dim row As DataRow
            row = result.NewRow()
            row("fullname") = "Select..."
            row("pid") = DBNull.Value
            result.Rows.InsertAt(row, 0)

        End Using
        Return result
    End Function

    Public Sub SCR_UpdateEmailSentDT(timssid As String)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn
            Dim sql As String = "UPDATE [tblSchoolReportDissemination] " &
                                "SET [ReportEmailedDT] = '" & Now() & "' " &
                                "WHERE [TIMSSID] = '" & timssid & "'"
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub SCR_UpdateRespondedFlag(timssid As String, responded As String)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn
            Dim sql As String = "UPDATE [tblSchoolReportDissemination] " &
                                "SET [RESPONDED] = '" & responded & "' " &
                                "WHERE [TIMSSID] = '" & timssid & "'"
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub SCR_SavePrincipalInformation(timssid As String, firstname As String, lastname As String, email As String)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn
            Dim sql As String = "UPDATE [tblSchoolReportDissemination] " &
                                "SET [PRINCIPALFIRSTNAME] = '" & firstname & "', [PRINCIPALLASTNAME] = '" & lastname & "', [PRINCIPALEMAIL] = '" & email & "' " &
                                "WHERE [TIMSSID] = '" & timssid & "'"
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub SaveDistrictPersonnelEditChanges(args As SaveChangesArgsBase, fldname As String, leaid As String, email As String)
        If Not args.HasPrimaryKey Then
            Using objConn As New SqlConnection(strConn)
                objConn.Open()
                Dim cmd = New SqlCommand
                cmd.Connection = objConn
                cmd.CommandText = "insertDistrictPersonnel"
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("RC", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

                Dim name As String = ""
                Dim value As String = ""
                Dim cnt As Integer = 0

                For Each ctrl As Control In args.DbControls
                    name = ParseDbFieldName(ctrl.ID)
                    value = ParseDbValue(ctrl)
                    If Not value Is Nothing Then
                        cmd.Parameters.AddWithValue(name, value)
                    Else
                        cmd.Parameters.AddWithValue(name, "")
                    End If
                    cnt = cnt + 1
                Next

                cmd.ExecuteNonQuery()
                Dim pk As Integer = cmd.Parameters("rc").Value

                'Validate fldname
                If Not (fldname.Equals("d_super", StringComparison.CurrentCultureIgnoreCase) _
                    Or fldname.Equals("d_tdirector", StringComparison.CurrentCultureIgnoreCase) _
                    Or fldname.Equals("d_contact", StringComparison.CurrentCultureIgnoreCase)) Then
                    fldname = "FieldNotValidated"
                End If

                Dim sql As String = "update tblscsdistrict set [" & fldname & "] = @" & fldname & " where leaid = @leaid"
                cmd.CommandText = sql
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue(fldname, pk)
                cmd.Parameters.AddWithValue("leaid", leaid)
                cmd.ExecuteNonQuery()

            End Using
        Else
            ClearPersonnelConfirmationEmailSentIfEmailChanged(args.PrimaryKey, email)
            HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblscsPersonnel", "pid")
        End If
    End Sub



    Public Function GetDistrictPersonnelDataRow(args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.PREFIX " &
                              "       ,a.FNAME " &
                              "       ,a.LEAID " &
                              "       ,a.LNAME " &
                              "       ,a.SUFFIX " &
                              "       ,a.TITLE " &
                              "       ,a.EMAIL " &
                              "       ,a.ADDR_1 " &
                              "       ,a.ADDR_2 " &
                              "       ,a.CITY " &
                              "       ,a.STATE " &
                              "       ,a.ZIP " &
                              "       ,a.fax " &
                              "       ,a.PHONE " &
                              "       ,a.PHONEEXT " &
                              "       ,a.ConfirmationEmailSent " &
                            "from	tblscsPersonnel a "

            Dim cnt As Integer = 0
            If args.IsNaepStateCoordinator Then
                sql = sql & IIf(cnt = 0, " where ", " and ") & "[LEAID] in (select distinct a.LEAID from uv_Customize a where	a.REPSBGRP = @REPSBGRP and a.isPublic = 1)"
                cnt = cnt + 1
            ElseIf args.IsFieldDirector Or args.IsFieldManager Then
                sql = sql & IIf(cnt = 0, " where ", " and ") & "a.[LEAID] in (select distinct a.LEAID from uv_Customize a join NAEPFRS2015.[dbo].[uv_TIMSSStaffTRA] b on b.fldTerritoryCode = a.fldTerritoryCode " & IIf(args.IsFieldManager, " and b.fldRegionCode = a.fldRegionCode ", "") & "  where	b.fldWINSID = @fldWINSID)"
                cnt = cnt + 1
            End If



            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            da = New SqlDataAdapter(sql, objConn)

            If args.IsNaepStateCoordinator Then
                da.SelectCommand.Parameters.AddWithValue("@REPSBGRP", args.REPSBGRP)
            ElseIf args.IsFieldDirector Or args.IsFieldManager Then
                da.SelectCommand.Parameters.AddWithValue("@fldWINSID", args.WINSID)

            End If

            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function



    Public Function GetSchoolPersonnelDataRow(args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.PREFIX " &
                              "       ,a.FNAME " &
                              "       ,a.frame_n_ " &
                              "       ,a.LNAME " &
                              "       ,a.SUFFIX " &
                              "       ,a.TITLE " &
                              "       ,a.EMAIL " &
                              "       ,a.fax " &
                              "       ,a.PHONE " &
                              "       ,a.PHONEEXT " &
                              "       ,a.ConfirmationEmailSent " &
                            "from	tblscsPersonnel a "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            da = New SqlDataAdapter(sql, objConn)
            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next


            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function

    Public Function GetFrameN(gradeid As String) As String

        Dim result As String = ""

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.Frame_N_ " &
                            "from	sgep_General a " &
                            "WHERE (ID = @id) " &
                            "and ((DISP NOT IN ('03', '99')) " &
                            "OR (fldProjectID = 780 AND DISP IN ('03')))" &
                            " AND (ID = @id)"


            da = New SqlDataAdapter(sql, objConn)
            da.SelectCommand.Parameters.AddWithValue("@id", gradeid)

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = dset.Tables(0).Rows(0)
                result = dr("frame_n_")
            Else

            End If
        End Using

        Return result
    End Function


    Public Function GetDisp(gradeid As String) As String

        Dim result As String = ""

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.Disp " &
                            "from	sgep_General a " &
                            "WHERE (ID = @id) "


            da = New SqlDataAdapter(sql, objConn)
            da.SelectCommand.Parameters.AddWithValue("@id", gradeid)

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = dset.Tables(0).Rows(0)
                result = dr("Disp")
            Else

            End If
        End Using

        Return result
    End Function

    Public Function GetSchoolPersonnelDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select fname + ' ' + lname AS fullname, phone, pid, fname, lname, prefix, suffix, phone, phoneext, email, fax, title " &
                    "from	tblSCSPersonnel "
            Dim cnt As Integer = 0


            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            If args.SortParameters.Count > 0 Then
                cnt = 0
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            'sql = "select 'Select...' fullname, phone, null pid, null fname, null lname union all " & sql
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

            Dim row As DataRow
            row = result.NewRow()
            row("fullname") = "Select..."
            row("pid") = DBNull.Value
            result.Rows.InsertAt(row, 0)
        End Using
        Return result
    End Function

    Public Sub SaveSchoolPersonnelEditChanges(NameValuePairList As List(Of NameValuePair), primarykeyvalue As String, fldname As String, frame_n_ As String, email As String)
        If String.IsNullOrEmpty(primarykeyvalue) Then
            Using objConn As New SqlConnection(strConn)
                objConn.Open()
                Dim cmd = New SqlCommand
                cmd.Connection = objConn
                cmd.CommandText = "insertSchoolPersonnel"
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("RC", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

                Dim name As String = ""
                Dim value As String = ""
                Dim cnt As Integer = 0

                For Each nv As NameValuePair In NameValuePairList

                    name = nv.Name
                    value = nv.Value

                    If Not value Is Nothing Then
                        cmd.Parameters.AddWithValue(name, value)
                    Else
                        cmd.Parameters.AddWithValue(name, "")
                    End If
                    cnt = cnt + 1
                Next

                cmd.ExecuteNonQuery()
                Dim pk As Integer = cmd.Parameters("rc").Value

                If Not String.IsNullOrEmpty(fldname) Then

                    'Validate fldname
                    If Not (fldname.Equals("principalid", StringComparison.CurrentCultureIgnoreCase) _
                        Or fldname.Equals("coordinatorid", StringComparison.CurrentCultureIgnoreCase)) Then
                        fldname = "FieldNotValidated"
                    End If

                    Dim sql As String = "update tblscsschool set [" & fldname & "] = @" & fldname & " where frame_n_ = @frame_n_"
                    cmd.CommandText = sql
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue(fldname, pk)
                    cmd.Parameters.AddWithValue("frame_n_", frame_n_)
                    cmd.ExecuteNonQuery()

                End If

            End Using
        Else

            ClearPersonnelConfirmationEmailSentIfEmailChanged(primarykeyvalue, email)
            HandleTableUpdate(NameValuePairList, primarykeyvalue, "tblscsPersonnel", "pid")

        End If
    End Sub

    Public Sub SaveSchoolPersonnelEditChanges(args As SaveChangesArgsBase, fldname As String, frame_n_ As String, email As String)


        SaveSchoolPersonnelEditChanges(ListOfControlsToNameValuePairList(args.DbControls), args.PrimaryKey, fldname, frame_n_, email)

        'If Not args.HasPrimaryKey Then
        '    Using objConn As New SqlConnection(strConn)
        '        objConn.Open()
        '        Dim cmd = New SqlCommand
        '        cmd.Connection = objConn
        '        cmd.CommandText = "insertSchoolPersonnel"
        '        cmd.CommandType = CommandType.StoredProcedure

        '        cmd.Parameters.Add("RC", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

        '        Dim name As String = ""
        '        Dim value As String = ""
        '        Dim cnt As Integer = 0

        '        For Each ctrl As Control In args.DbControls
        '            name = ParseDbFieldName(ctrl.ID)
        '            value = ParseDbValue(ctrl)
        '            If Not value Is Nothing Then
        '                cmd.Parameters.AddWithValue(name, value)
        '            Else
        '                cmd.Parameters.AddWithValue(name, "")
        '            End If
        '            cnt = cnt + 1
        '        Next

        '        cmd.ExecuteNonQuery()
        '        Dim pk As Integer = cmd.Parameters("rc").Value

        '        'Validate fldname
        '        If Not (fldname.Equals("principalid", StringComparison.CurrentCultureIgnoreCase) _
        '            Or fldname.Equals("coordinatorid", StringComparison.CurrentCultureIgnoreCase)) Then
        '            fldname = "FieldNotValidated"
        '        End If

        '        Dim sql As String = "update tblscsschool set [" & fldname & "] = @" & fldname & " where frame_n_ = @frame_n_"
        '        cmd.CommandText = sql
        '        cmd.CommandType = CommandType.Text
        '        cmd.Parameters.Clear()
        '        cmd.Parameters.AddWithValue(fldname, pk)
        '        cmd.Parameters.AddWithValue("frame_n_", frame_n_)
        '        cmd.ExecuteNonQuery()

        '    End Using
        'Else

        '    ClearPersonnelConfirmationEmailSentIfEmailChanged(args.PrimaryKey, email)
        '    HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblscsPersonnel", "pid")

        'End If

    End Sub
    Private Sub ClearPersonnelConfirmationEmailSentIfEmailChanged(pid As String, email As String)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn
            Dim sql As String = "update tblSCSPersonnel set [ConfirmationEmailSent] = null where pid = @pid and email <> @email"
            cmd.CommandText = sql
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("pid", pid)
            cmd.Parameters.AddWithValue("email", email)
            cmd.ExecuteNonQuery()

        End Using

    End Sub

    'Public Sub SaveSchoolErocChanges(args As SaveChangesArgsBase, gradeid As String, disp As String, updatedby As Guid)
    '    Dim currentDisp = GetDisp(gradeid)
    '    Dim DbFields As List(Of NameValuePair) = Me.ListOfControlsToNameValuePairList(args.DbControls)
    '    DbFields.Add(New NameValuePair("CreateDT", "Getdate()"))
    '    DbFields.Add(New NameValuePair("UpdatedBy", updatedby.ToString()))
    '    Dim result As Integer = HandleTableInsert(DbFields, "tblEroc", True)

    '    If Not currentDisp.Equals(disp) Then
    '        Dim NameValuePairList As New List(Of NameValuePair)
    '        NameValuePairList.Add(New NameValuePair("disp", disp))
    '        HandleTableUpdate(NameValuePairList, gradeid, "tblscsgrade", "ID")
    '    End If

    'End Sub

    Public Sub SaveSchoolErocChanges(args As SaveChangesArgsBase, gradeid As String, updatedby As Guid)
        'Dim currentDisp = GetDisp(gradeid)
        Dim DbFields As List(Of NameValuePair) = Me.ListOfControlsToNameValuePairList(args.DbControls)
        DbFields.Add(New NameValuePair("CreateDT", "Getdate()"))
        DbFields.Add(New NameValuePair("UpdatedBy", updatedby.ToString()))
        Dim result As Integer = HandleTableInsert(DbFields, "tblEroc", True)


    End Sub

    Public Function GetWorkAreaNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim dt As New DataTable("TheData")

        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct  a.fldAreaCode " &
                    "from	uv_Customize a " &
            "Where	a.fldAreaCode is not null "

            Dim cnt As Integer = 1

            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(IIf(f.FilterColumn = "project", "expr1", f.FilterColumn) & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[expr1]").Replace("@project", "@expr1")
                cnt = cnt + 1
            Next
            cnt = 0
            If args.SortParameters.Count > 0 Then

                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            results.Add(New NameValuePair("Show All...", ""))
            For Each row As DataRow In dt.Rows
                results.Add(New NameValuePair(row("fldAreaCode").ToString(), row("fldAreaCode")))
            Next
        End Using
        Return results
    End Function


    Public Function GetAccountDetailsDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.[ApplicationId] " &
                                  "       ,a.[UserId] " &
                                  "       ,a.[UserName] " &
                                  "       ,a.Comment " &
                                  "       ,a.CreationDate " &
                                  "       ,a.Email " &
                                  "       ,a.IsApproved " &
                                  "       ,a.IsLockedOut " &
                                  "       ,a.LastLockoutDate " &
                                  "       ,a.LastLoginDate " &
                                  "       ,a.LastPasswordChangedDate " &
                                  "       ,a.FirstName " &
                                  "       ,a.LastArea " &
                                  "       ,a.LastName " &
                                  "       ,a.LastPageSize " &
                                  "       ,a.LastRegion " &
                                  "       ,a.LastUpdatedDate " &
                                  "       ,a.MIDDLENAME " &
                                  "       ,a.PREFIX " &
                                  "       ,a.PROJECTSTAFFID " &
                                  "       ,a.ProfileVersion " &
                                  "       ,a.WINSID " &
                                  "       ,a.CreateDate " &
                                  "       ,a.REPSBGRP " &
                                  "       ,a.Telephone " &
                                  "       ,a.TelephoneExtension " &
                                  "       ,a.RegistrationId " &
                                  "       ,a.Frame_N_ " &
                                "from	[uv_AccountDetails] a "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetREPSBGRPNameValuePairList() As List(Of NameValuePair)
        Dim dt As New DataTable("TheData")

        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.repsbgrp " &
                    "from	uv_Customize a " &
            "where	a.isPublic = 1 " &
            "order by a.repsbgrp "


            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            results.Add(New NameValuePair("Unassigned", ""))
            For Each row As DataRow In dt.Rows
                results.Add(New NameValuePair(row("repsbgrp").ToString(), row("repsbgrp")))
            Next
        End Using
        Return results
    End Function


    Public Function GetFirstLetterOfSchoolNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim dt As New DataTable("TheData")

        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct substring(a.s_name,1,1) [Letter] " &
                    "from	uv_Customize a " '& _
            '                    "where	substring(a.s_name,1,1) is not null "


            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            sql = sql & "where	substring(a.s_name,1,1) is not null "

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                'cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                cmd.SelectCommand.Parameters.AddWithValue(IIf(f.FilterColumn = "project", "fldProjectDesc", f.FilterColumn) & f.Index, f.FilterValue)
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc").Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc")
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            sql = sql & " order by substring(a.s_name,1,1) "


            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            results.Add(New NameValuePair("*", ""))
            For Each row As DataRow In dt.Rows
                results.Add(New NameValuePair(row("Letter").ToString(), row("Letter")))
            Next
        End Using
        Return results
    End Function


    Public Function GetFirstLetterOfDistrictNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim dt As New DataTable("TheData")

        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct coalesce(substring(a.d_name,1,1), '') [Letter] " &
            "from	uv_Customize a "


            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            sql = sql & " order by coalesce(substring(a.d_name,1,1), '') "

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            results.Add(New NameValuePair("*", ""))
            For Each row As DataRow In dt.Rows
                results.Add(New NameValuePair(row("Letter").ToString(), row("Letter")))
            Next
        End Using
        Return results
    End Function


    Public Function GetStaffWINSIDNameValuePairList() As List(Of NameValuePair)
        Dim dt As New DataTable("TheData")

        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.fldWINSID, coalesce(a.fldLastName, '') + ', ' + coalesce(a.fldFirstName, '') FullnameLF " &
            "from	NAEPFRS2015.[dbo].[uv_TIMSSStaffTRA]  a " &
            "order by coalesce(a.fldLastName, '') + ', ' + coalesce(a.fldFirstName, '')"

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)
            results.Add(New NameValuePair("Unassigned", ""))
            For Each row As DataRow In dt.Rows
                results.Add(New NameValuePair(row("FullnameLF").ToString() & " (" & row("fldWINSID").ToString() & ")", row("fldWINSID")))
            Next
        End Using
        Return results
    End Function

    Public Function GetEmailTemplateDataRow(args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.[EmailTemplateName] " &
                              "       ,a.[EmailFrom] " &
                              "       ,a.[EmailCC] " &
                              "       ,a.[EmailBCC] " &
                              "       ,a.[EmailSubject] " &
                              "       ,a.[EmailBody] " &
                            "from	tblEmailTemplate a "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            da = New SqlDataAdapter(sql, objConn)
            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next


            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function

    Public Sub SaveEmailTemplateEditChanges(args As SaveChangesArgsBase)
        HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblEmailTemplate", "EmailTemplateName")
    End Sub

    Public Sub UpdateConfirmationEmailSent(pID As Integer)
        Dim nvplst As New List(Of NameValuePair)
        nvplst.Add(New NameValuePair("ConfirmationEmailSent", "getdate()"))
        HandleTableUpdate(nvplst, pID, "tblscsPersonnel", "pID")
    End Sub

    Public Function GetReportDistrictRecruitmentDataTable(args As GetDistrictListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.D_Name " &
                                  "       ,a.D_Addr1 " &
                                  "       ,a.D_Addr2 " &
                                  "       ,a.D_City " &
                                  "       ,a.D_State " &
                                  "       ,a.D_Zip " &
                                  "       ,a.D_Phone " &
                                  "       ,a.LEAID " &
                                  "       ,a.D_SuperPrefix " &
                                  "       ,a.D_SuperFname " &
                                  "       ,a.D_SuperLname " &
                                  "       ,a.D_SuperSuffix " &
                                  "       ,a.D_SuperAddr1 " &
                                  "       ,a.D_SuperAddr2 " &
                                  "       ,a.D_SuperCity " &
                                  "       ,a.D_SuperState " &
                                  "       ,a.D_SuperZip " &
                                  "       ,a.D_SuperPhone " &
                                  "       ,a.D_SuperEmail " &
                                  "       ,a.D_TdPrefix " &
                                  "       ,a.D_TdFname " &
                                  "       ,a.D_TdLname " &
                                  "       ,a.D_TdSuffix " &
                                  "       ,a.D_TdAddr1 " &
                                  "       ,a.D_TdAddr2 " &
                                  "       ,a.D_TdCity " &
                                  "       ,a.D_TdState " &
                                  "       ,a.D_TdZip " &
                                  "       ,a.D_TdPhone " &
                                  "       ,a.D_TdEmail " &
                                  "       ,a.D_SuperPhoneext " &
                                  "       ,a.D_TdPhoneext " &
                                "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then

                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Function GetReportSchoolRecruitmentDataTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.S_Addr1 " &
                      "       ,a.S_Addr2 " &
                      "       ,a.S_City " &
                      "       ,a.S_State " &
                      "       ,a.S_Zip " &
                      "       ,a.S_Phone " &
                      "       ,a.S_County " &
                      "       ,a.DispName " &
                      "       ,a.SMPGRD " &
                      "       ,a.ScheDate " &
                      "       ,a.ScheTime " &
                      "       ,a.ESTGRE " &
                      "       ,a.SEASCH " &
                      "       ,a.ID " &
                      "       ,a.pprefix " &
                      "       ,a.pfname " &
                      "       ,a.plname " &
                      "       ,a.sp_suffix " &
                      "       ,a.sp_phone " &
                      "       ,a.sp_email " &
                      "       ,a.sc_prefix " &
                      "       ,a.sc_fname " &
                      "       ,a.sc_lname " &
                      "       ,a.sc_suffix " &
                      "       ,a.sc_phone " &
                      "       ,a.sc_email " &
                      "       ,a.sp_Phoneext " &
                      "       ,a.sc_Phoneext " &
                      "       ,a.fldTerritoryCode " &
                      "       , case a.isPublic when 1 then 'Public' else 'Private' end [SchoolType]" &
                      "       ,a.fldAreaCode " &
                      "       ,a.testregion " &
                      "       ,a.d_name " &
                      "       ,a.REPSBGRP " &
                      "       ,a.MyNAEPREGID " &
                      "       ,a.LastDayofSchool " &
                      "       ,a.EnrollmentAtGrade " &
                      "       ,a.NumberOfMathClasses " &
                      "       ,a.NumberOfClasses " &
                      "       ,a.DateSchoolReturnsFromSpringBreak " &
                      "       ,a.DateSchoolReturnsFromWinterBreak " &
                      "       ,a.ORIGSUB " &
                      "       ,a.AdvancedEligibility " &
                      "       ,a.AdvancedEligibilityComments " &
                      "       ,a.AdvancedMathComments " &
                      "       ,a.AdvancedPhysicsComments " &
                      "       ,a.STLFUserFilePath " &
                      "       ,a.STLFUploaded " &
                      "       ,a.STLF2UserFilePath " &
                      "       ,a.STLF2Uploaded " &
                      "       ,a.DateOfMakeUp " &
                      "       ,a.ParentConsentType " &
                      "       ,a.ArrivalTime " &
                      "       ,a.[Study_Name] " &
                      "       ,a.D_Name " &
                      "       ,a.D_Addr1 " &
                      "       ,a.D_Addr2 " &
                      "       ,a.D_City " &
                      "       ,a.D_State " &
                      "       ,a.D_zip " &
                      "       ,a.D_Phone " &
                      "       ,a.D_Fax " & '
                      "       ,a.LEAID " & ' district timms id
                      "       ,a.D_Contact " & ' other contact
                      "       ,a.D_ContPhone " & 'contact phone
                      "       ,a.D_ContEmail " & 'contact phone
                      "       ,a.sch_partltrsentdt " & ' Date Initial School Selection Letter Sent
                      "       ,a.SchAsmtLtrSentDT " & ' Date School Assessment Date Letter Sent
                      "       ,a.AugSchLtrSentDT " & ' Date Sch Coord Resp Mailing Sent
                      "       ,a.d_partltrsentdt " & ' Date District Letter sent
                      "       ,a.PSIComplete " &
                      "       ,a.IEA_ID " &
                      "       ,case when a.SPECIAL_CASE = 'Y' then 'Y' ELSE 'N' end [SPECIAL_CASE]" &
                      "from	uv_Customize a "

            'Removed 9/12/17 - DCB
            ' "       ,a.ScheDate2 " &
            ' "       ,a.ScheTime2 " &
            '"       ,a.ArrivalTime2 " &
            '"       ,a.D_Super " & ' supervisor
            '"       ,a.td_Name " & ' test director name

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Function GetRecruitmentReportSchoolTypeNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        ' If Not args.IsNaepStateCoordinator Then results.Add(New NameValuePair("All", ""))
        results.Add(New NameValuePair("All", ""))
        results.Add(New NameValuePair("eTIMSS Public School", "1"))
        If Not args.IsNaepStateCoordinator Then results.Add(New NameValuePair("eTIMSS Private School", "0"))
        results.Add(New NameValuePair("ICILS Public School", "1I"))
        If Not args.IsNaepStateCoordinator Then results.Add(New NameValuePair("ICILS Private School", "0I"))
        Return results
    End Function

    Public Function GetRecruitmentReportFilterNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        results.Add(New NameValuePair("All Schools", ""))
        results.Add(New NameValuePair("Orginal Schools", cFilterShortcutOrginalSchools))
        results.Add(New NameValuePair("Substitute Schools", cFilterShortcutSubstituteSchools))
        results.Add(New NameValuePair("Pending Schools", cFilterShortcutPendingSchools))
        results.Add(New NameValuePair("Initial contact, Pending Schools", cFilterShortcutInitialContactPendingSchools))
        results.Add(New NameValuePair("Cooperating Schools", cFilterShortcutCooperatingSchools))
        results.Add(New NameValuePair("Cooperating/Pending Schools", cFilterShortcutCooperatingPendingSchools))
        results.Add(New NameValuePair("Refusing Schools", cFilterShortcutRefusingSchools))
        Return results
    End Function

    Private Sub HandleFilterShortcuts(args As SelectFromDatabaseArgs)
        If Not args.FilterShortcut Is Nothing Then
            If args.FilterShortcut.Equals(cFilterShortcutOrginalSchools) Then
                args.FilterParameters.Add(New FilterParameter("ORIGSUB", "O", "equal"))
            ElseIf args.FilterShortcut.Equals(cFilterShortcutSubstituteSchools) Then
                args.FilterParameters.Add(New FilterParameter("ORIGSUB", "S", "equal"))
            ElseIf args.FilterShortcut.Equals(cFilterShortcutPendingSchools) Then
                args.FilterParameters.Add(New FilterParameter("DISP", "00", "equal"))
            ElseIf args.FilterShortcut.Equals(cFilterShortcutInitialContactPendingSchools) Then
                args.FilterParameters.Add(New FilterParameter("DISP", "02", "equal"))
            ElseIf args.FilterShortcut.Equals(cFilterShortcutCooperatingSchools) Then
                args.FilterParameters.Add(New FilterParameter("DISP", "01", "equal"))
            ElseIf args.FilterShortcut.Equals(cFilterShortcutCooperatingPendingSchools) Then
                args.FilterParameters.Add(New FilterParameter("DISP", "'00', '01'", "in"))
            ElseIf args.FilterShortcut.Equals(cFilterShortcutRefusingSchools) Then
                args.FilterParameters.Add(New FilterParameter("DISP", "'03', '05', '30', '32', '33'", "in"))
            End If
        End If
    End Sub

    Public Function GeExportReportOptionsNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        results.Add(New NameValuePair("All Records", "all"))
        results.Add(New NameValuePair("Current Page", "current"))
        Return results
    End Function

    Public Function GetAssessmentDateList(args As GetSchoolListSqlDataSourceArgs) As List(Of AssessmentDate)
        Dim result As New List(Of AssessmentDate)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.ID " &
                      "       ,a.fldProjectDesc Project " &
                      "       ,a.DispName " &
                      "       ,a.testregion " &
                      "       ,a.fldAreaCode " &
                      "       ,a.S_County " &
                      "       ,a.D_Name " &
                    "       ,a.ScheDate " &
                    "       ,a.ScheDate2 " &
                     "       ,a.[Study_Name] " &
                     "       ,a.[fldProjectDesc] " &
                     "       ,a.REPSBGRP " &
                    "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)

            sql = sql &
            "Where (a.ScheDate is not null or a.ScheDate2 is not null) and a.Disp not in ('30','32','33','34','40','41','42','07') "

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            For index = dt.Rows.Count - 1 To 0 Step -1
                Dim row As DataRow = dt.Rows(index)
                If Not row("ScheDate") Is DBNull.Value Then
                    Dim targetDate As DateTime = row("ScheDate")
                    Dim ad As AssessmentDate = GetAssessmentDateFromList(result, targetDate)
                    If ad Is Nothing Then
                        ad = New AssessmentDate(targetDate)
                        result.Add(ad)
                    End If
                    ad.Assessments.Add(row)
                End If

                If Not row("ScheDate2") Is DBNull.Value Then
                    Dim targetDate As DateTime = row("ScheDate2")
                    Dim ad As AssessmentDate = GetAssessmentDateFromList(result, targetDate)
                    If ad Is Nothing Then
                        ad = New AssessmentDate(targetDate)
                        result.Add(ad)
                    End If
                    ad.Assessments.Add(row)
                End If

            Next

        End Using
        Return result
    End Function

    Public Function GetAssessmentDateFromList(lst As List(Of AssessmentDate), targetDate As DateTime) As AssessmentDate
        Dim result As AssessmentDate = Nothing
        If Not lst Is Nothing Then
            For Each ad As AssessmentDate In lst
                If ad.TheDate.Equals(targetDate) Then
                    result = ad
                    Exit For
                End If
            Next
        End If
        Return result
    End Function

    Public Function GetAssessmentDateListTestRegionNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.testregion " &
                    "from	uv_Customize a "

            args.SortParameters.Add(New SortParameter("testregion", SortDirection.Ascending))

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            sql = sql &
            "Where (a.ScheDate is not null or a.ScheDate2 is not null) and testregion is not null "

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)


            If args.IsAdmin Or args.IsNaepStateCoordinator Or args.IsHomeOffice Or args.IsFieldManager Then results.Add(New NameValuePair("All", ""))

            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("testregion").ToString(), dt.Rows(index)("testregion")))
            Next

        End Using
        Return results
    End Function


    Public Function GetREPSBGRPValuePairList(args As SelectFromDatabaseArgs, showalloption As Boolean) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.REPSBGRP " &
                    "from	uv_Customize a " '& _


            args.SortParameters.Add(New SortParameter("REPSBGRP", SortDirection.Ascending))

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            'sql = sql & _
            '"Where a.ScheDate is not null "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If f.HasValueParameter() Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If showalloption Then results.Add(New NameValuePair("All", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("REPSBGRP").ToString(), dt.Rows(index)("REPSBGRP")))
            Next

        End Using
        Return results
    End Function

    Public Function GetStateNameValuePairList(args As SelectFromDatabaseArgs, showalloption As Boolean) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.S_State " &
                    "from	uv_Customize a " '& _
            '                    "where a.S_State is not null "

            args.SortParameters.Add(New SortParameter("S_State", SortDirection.Ascending))

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            'sql = sql & _
            '"Where a.ScheDate is not null "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If f.HasValueParameter() Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If showalloption Then results.Add(New NameValuePair("All", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("S_State").ToString(), dt.Rows(index)("S_State")))
            Next

        End Using
        Return results
    End Function

    Public Function GetREPSValuePairList(args As SelectFromDatabaseArgs, showalloption As Boolean) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.REPSBGRP " &
                    "from	uv_Customize a " '& _
            '                    "where a.S_State is not null "

            args.SortParameters.Add(New SortParameter("REPSBGRP", SortDirection.Ascending))

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            'sql = sql & _
            '"Where a.ScheDate is not null "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If f.HasValueParameter() Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If showalloption Then results.Add(New NameValuePair("All", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("REPSBGRP").ToString(), dt.Rows(index)("REPSBGRP")))
            Next

        End Using
        Return results
    End Function

    Private Function HandleJoinToCustomizeViewToFilterByRole(args As SelectFromDatabaseArgs, Sql As String) As String
        If args.IsNaepStateCoordinator Then
            args.RemoveFilterParameter("isPublic")
            args.RemoveFilterParameter("REPSBGRP")
            args.RemoveFilterParameter("TAUFLAG")
            args.FilterParameters.Add(New FilterParameter("REPSBGRP", args.REPSBGRP, "equal"))
            args.FilterParameters.Add(New FilterParameter("isPublic", "1", "equal"))
            args.FilterParameters.Add(New FilterParameter("TAUFLAG", "6,7", "in"))
        ElseIf args.IsFieldDirector Or args.IsFieldManager Or args.IsTestAdministrator Or args.IsTestAdministratorTroubleShooter Then
            Sql = Sql & "join NAEPFRS2015.[dbo].[uv_TIMSSStaffTRA] b " &
                "on b.fldTerritoryCode = a.fldTerritoryCode " &
                "and b.fldstatecode = a.fldStateCode " &
                "and b.fldRegionCode = a.fldRegionCode " &
                "and b.fldAreaCode  = a.fldAreaCode  "
            'If args.IsFieldManager Then
            '    Sql = Sql & "and b.fldRegionCode = a.fldRegionCode "
            'End If
            args.RemoveFilterParameter("fldWINSID")
            args.FilterParameters.Add(New FilterParameter("fldWINSID", args.WINSID, "equal"))
        ElseIf args.IsTudaCoordinator Then
            args.RemoveFilterParameter("TUA_LEA")
            args.RemoveFilterParameter("TAUFLAG")
            args.FilterParameters.Add(New FilterParameter("TUA_LEA", args.TUA_LEA, "equal"))
            args.FilterParameters.Add(New FilterParameter("TAUFLAG", "6,7", "in"))
        ElseIf args.IsHomeOffice Or args.IsAdmin Then 'these roles see everthing
        Else
            args.FilterParameters.Add(New FilterParameter("1", "2", "equal"))
        End If
        Return Sql

    End Function


    Public Function GetTerritoryNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.fldTerritoryCode " &
                    "from	uv_Customize a " '& _
            '                    "where a.fldTerritoryCode is not null"

            args.SortParameters.Add(New SortParameter("fldTerritoryCode", SortDirection.Ascending))

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            sql = sql & " where a.fldTerritoryCode is not null"
            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            results.Add(New NameValuePair("All", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("fldTerritoryCode").ToString(), dt.Rows(index)("fldTerritoryCode")))
            Next

        End Using
        Return results
    End Function

    Public Sub SaveProvideSchoolInformationEditChanges(args As SaveProvideSchoolInformationEditChangesArgs)
        HandleTableUpdate(args.DbControls, args.PrimaryKey, "tblscsschool", "frame_n_")

        'Dim result As Integer = HandleTableInsert(DbFields, "tblEroc", True)
        'HandleTableUpdate(args.GradeDbControls, args.GradeId, "tblscsgrade", "ID")
    End Sub

    Public Function GetDistinctSchoolListDataTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.S_Name " &
                      "       ,a.Frame_N_ " &
                      "       ,a.s_addr1 " &
                      "       ,a.s_addr2 " &
                      "       ,a.s_city " &
                      "       ,a.s_state " &
                      "       ,a.s_zip " &
                      "       ,a.PrincipalID " &
                      "       ,a.CoordinatorID " &
                      "       ,dist.D_Name " &
                    "from uv_Customize a " &
                    "join	tblSCSDistrict dist " &
                    "on	dist.LEAID = a.LEAID "

            '"from	(select a.*, b.REPSBGRP, c.isPublic, b.TUA_LEA from (select * from tblSCSSchool where sysActive = 1) a join (select distinct FRAME_N_, REPSBGRP, TUA_LEA from dbo.tblGrade_Stat) b on b.frame_n_ = a.frame_n_ join dbo.tblSCSSchoolTypes c ON c.Schl_Typ = a.schl_typ) a " & _

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(IIf(f.FilterColumn = "project", "fldProjectDesc", f.FilterColumn) & f.Index, f.FilterValue)
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc")
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace("[project]", "[fldProjectDesc]").Replace("@project", "@fldProjectDesc")
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetTIMSSG4IncompletePSI() As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "SELECT distinct a.S_Name " &
                                ",a.s_addr1 " &
                                ",a.s_city " &
                                ",a.s_state " &
                                ",a.s_zip " &
                                ",a.PrincipalID " &
                                ",a.CoordinatorID " &
                                ",g4.EnrollmentAtGrade  EnrollmentAtGrade_4 " &
                                ",g4.NumberOfClasses  NumberOfClasses_4 " &
                                ",g8.EnrollmentAtGrade  EnrollmentAtGrade_8 " &
                                ",g8.NumberOfMathClasses  NumberOfMathClasses_8 " &
                                ",a.ICTCoordinatorName " &
                                ",a.ICTCoordinatorEmail " &
                                ",p1.fname As PrincipalFName " &
                                ",p1.lname As PrincipalLName " &
                                ",s1.fname As SchCoordFName " &
                                ",s1.lname As SchCoordLname " &
                                ",p1.fname + ' ' + p1.lname As PrincipalFirstAndLastName " &
                                ",s1.fname + ' ' + s1.lname As SchoolCoordFirstAndLastName " &
                                ",s1.phone " &
                                ",p1.phone " &
                                ",s1.email " &
                                ",p1.email " &
                                "From uv_Customize a " &
                                "Join tblSCSDistrict d " &
                                "On  d.LEAID = a.LEAID " &
                                "Join    uv_SchoolWithID c " &
                                "On  c.frame_n_ = a.frame_n_ " &
                                "Left outer join	tblSCSGrade g4 " &
                                "On  g4.id = c.id4 " &
                                "Left outer join	tblSCSGrade g8 " &
                                "On  g8.id = c.id8 " &
                                "Left outer join	tblSCSGrade g12 " &
                                "On  g12.id = c.id12 " &
                                "Left outer join	aspnet_ProfileTIMSS p " &
                                "On  p.UserID = a.PSISubmittedBy " &
                                "Left outer join tblSCSPersonnel p1 " &
                                "On a.principalID = p1.pid " &
                                "Left outer join tblSCSPersonnel s1 " &
                                "On a.coordinatorid = s1.pid " &
                                "WHERE A.S_NAME Is NULL " &
                                "Or A.S_Addr1 Is NULL " &
                                "Or A.s_city Is NULL " &
                                "Or a.s_state Is NULL " &
                                "Or a.s_zip Is NULL " &
                                "Or a.ICTCoordinatorName Is NULL " &
                                "Or a.ICTCoordinatorEmail Is NULL " &
                                "Or p1.fname Is NULL " &
                                "Or p1.lname Is NULL " &
                                "Or s1.fname Is NULL " &
                                "Or s1.lname Is NULL " &
                                "Or p1.email Is NULL " &
                                "Or s1.email Is NULL " &
                                "Or p1.phone Is NULL " &
                                "Or s1.phone Is NULL " &
                                "Or a.principalID Is NULL " &
                                "Or a.coordinatorID Is NULL " &
                                "Or (g4.EnrollmentAtGrade Is NULL Or g4.NumberOfMathClasses Is NULL) " &
                                "ORDER BY A.S_NAME"



            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetTIMSSG8IncompletePSI() As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "SELECT distinct a.S_Name " &
                                ",a.s_addr1 " &
                                ",a.s_city " &
                                ",a.s_state " &
                                ",a.s_zip " &
                                ",a.PrincipalID " &
                                ",a.CoordinatorID " &
                                ",g4.EnrollmentAtGrade  EnrollmentAtGrade_4 " &
                                ",g4.NumberOfClasses  NumberOfClasses_4 " &
                                ",g8.EnrollmentAtGrade  EnrollmentAtGrade_8 " &
                                ",g8.NumberOfMathClasses  NumberOfMathClasses_8 " &
                                ",a.ICTCoordinatorName " &
                                ",a.ICTCoordinatorEmail " &
                                ",p1.fname As PrincipalFName " &
                                ",p1.lname As PrincipalLName " &
                                ",s1.fname As SchCoordFName " &
                                ",s1.lname As SchCoordLname " &
                                ",p1.fname + ' ' + p1.lname As PrincipalFirstAndLastName " &
                                ",s1.fname + ' ' + s1.lname As SchoolCoordFirstAndLastName " &
                                ",s1.phone " &
                                ",p1.phone " &
                                ",s1.email " &
                                ",p1.email " &
                                "From uv_Customize a " &
                                "Join tblSCSDistrict d " &
                                "On  d.LEAID = a.LEAID " &
                                "Join    uv_SchoolWithID c " &
                                "On  c.frame_n_ = a.frame_n_ " &
                                "Left outer join	tblSCSGrade g4 " &
                                "On  g4.id = c.id4 " &
                                "Left outer join	tblSCSGrade g8 " &
                                "On  g8.id = c.id8 " &
                                "Left outer join	tblSCSGrade g12 " &
                                "On  g12.id = c.id12 " &
                                "Left outer join	aspnet_ProfileTIMSS p " &
                                "On  p.UserID = a.PSISubmittedBy " &
                                "Left outer join tblSCSPersonnel p1 " &
                                "On a.principalID = p1.pid " &
                                "Left outer join tblSCSPersonnel s1 " &
                                "On a.coordinatorid = s1.pid " &
                                "WHERE A.S_NAME Is NULL " &
                                "Or A.S_Addr1 Is NULL " &
                                "Or A.s_city Is NULL " &
                                "Or a.s_state Is NULL " &
                                "Or a.s_zip Is NULL " &
                                "Or a.ICTCoordinatorName Is NULL " &
                                "Or a.ICTCoordinatorEmail Is NULL " &
                                "Or p1.fname Is NULL " &
                                "Or p1.lname Is NULL " &
                                "Or s1.fname Is NULL " &
                                "Or s1.lname Is NULL " &
                                "Or p1.email Is NULL " &
                                "Or s1.email Is NULL " &
                                "Or p1.phone Is NULL " &
                                "Or s1.phone Is NULL " &
                                "Or a.principalID Is NULL " &
                                "Or a.coordinatorID Is NULL " &
                                "Or (g8.EnrollmentAtGrade Is NULL Or g8.NumberOfMathClasses Is NULL) " &
                                "ORDER BY A.S_NAME"



            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function getSchoolRecordByConfID(args As SelectFromDatabaseArgs, confid As String) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "SELECT [IDSCHOOL]" &
                                ",      [TIMSSID]" &
                                ",      [SCHOOLNAME]" &
                                ",      [RESPONDED]" &
                                ",      [PRINCIPALFIRSTNAME]" &
                                ",      [PRINCIPALLASTNAME]" &
                                ",      [PRINCIPALEMAIL]" &
                                ",      [CONFIRMATION]" &
                                ",      [REPORTLINK]" &
                                ",      [SCHOOLADDRESS1]" &
                                ",      [SCHOOLADDRESS2]" &
                                "FROM   [tblSchoolReportDissemination]"

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            sql = sql & "WHERE CONFIRMATION = @confid"


            da = New SqlDataAdapter(sql, objConn)
            da.SelectCommand.Parameters.AddWithValue("@confid", confid)


            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function

    Public Function GetPSISchoolDetailsDataRow(args As SelectFromDatabaseArgs, frame_n_ As String) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "Select distinct a.S_Name " &
                      "       ,a.Frame_N_ " &
                      "       ,a.s_addr1 " &
                      "       ,a.s_addr2 " &
                      "       ,a.s_city " &
                      "       ,a.s_state " &
                      "       ,a.s_zip " &
                      "       ,a.PrincipalID " &
                      "       ,a.CoordinatorID " &
                      "       ,a.LEAID " &
                      "       ,d.D_Name " &
                      "       ,g4.LastDayofSchool LastDayofSchool_4 " &
                      "       ,g8.LastDayofSchool LastDayofSchool_8 " &
                      "       ,g12.LastDayofSchool LastDayofSchool_12 " &
                      "       ,g4.DateSchoolReturnsFromSpringBreak  DateSchoolReturnsFromSpringBreak_4 " &
                      "       ,g8.DateSchoolReturnsFromSpringBreak  DateSchoolReturnsFromSpringBreak_8 " &
                      "       ,g12.DateSchoolReturnsFromSpringBreak  DateSchoolReturnsFromSpringBreak_12 " &
                      "       ,g4.DateSchoolReturnsFromWinterBreak  DateSchoolReturnsFromWinterBreak_4 " &
                      "       ,g8.DateSchoolReturnsFromWinterBreak  DateSchoolReturnsFromWinterBreak_8 " &
                      "       ,g12.DateSchoolReturnsFromWinterBreak  DateSchoolReturnsFromWinterBreak_12 " &
                      "       ,g4.EnrollmentAtGrade  EnrollmentAtGrade_4 " &
                      "       ,g8.EnrollmentAtGrade  EnrollmentAtGrade_8 " &
                      "       ,g12.EnrollmentAtGrade  EnrollmentAtGrade_12 " &
                      "       ,g4.NumberOfClasses  NumberOfClasses_4 " &
                      "       ,g8.NumberOfMathClasses  NumberOfMathClasses_8 " &
                      "       ,c.id4 " &
                      "       ,c.id8 " &
                      "       ,c.id12 " &
                      "       ,g4.SCHEDATE SCHEDATE_4" &
                      "       ,g8.SCHEDATE SCHEDATE_8" &
                      "       ,g12.SCHEDATE SCHEDATE_12" &
                      "       ,a.MyNAEPREGID " &
                      "       ,p.FirstName + ' ' + p.LastName PSISubmittedBy " &
                      "       ,c.fldProjectID " &
                      "       ,g4.SCHEDATE2 SCHEDATE2_4" &
                      "       ,g8.SCHEDATE2 SCHEDATE2_8" &
                      "       ,g12.SCHEDATE2 SCHEDATE2_12 " &
                      "       ,g4.DateSchoolStartsSpringBreak  DateSchoolStartsSpringBreak_4 " &
                      "       ,g8.DateSchoolStartsSpringBreak  DateSchoolStartsSpringBreak_8 " &
                      "       ,g12.DateSchoolStartsSpringBreak  DateSchoolStartsSpringBreak_12 " &
                      "       ,a.ICTCoordinatorName " &
                      "       ,a.ICTCoordinatorEmail " &
                      "from uv_Customize a " &
                   "join	tblSCSDistrict d " &
                    "on	d.LEAID = a.LEAID " &
                    "join	uv_SchoolWithID c " &
                    "on	c.frame_n_ = a.frame_n_ " &
                    "left outer join	tblSCSGrade g4 " &
                    "on	g4.id = c.id4 " &
                    "left outer join	tblSCSGrade g8 " &
                    "on	g8.id = c.id8 " &
                    "left outer join	tblSCSGrade g12 " &
                    "on	g12.id = c.id12 " &
                    "left outer join	aspnet_ProfileTIMSS p " &
                    "on	p.UserID = a.PSISubmittedBy " '& _
            '                    "where a.frame_n_ = @frame_n_ "

            '"from	(select a.*, b.REPSBGRP, c.isPublic, b.TUA_LEA from tblSCSSchool a join (select distinct FRAME_N_, REPSBGRP, TUA_LEA from dbo.tblGrade_Stat) b on b.frame_n_ = a.frame_n_ join dbo.tblSCSSchoolTypes c ON c.Schl_Typ = a.schl_typ) a " & _



            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            sql = sql & " where a.frame_n_ = @frame_n_ "

            da = New SqlDataAdapter(sql, objConn)

            da.SelectCommand.Parameters.AddWithValue("@frame_n_", frame_n_)



            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function


    Public Function GetMyTIMSSRegistrationDataRow(MyNAEPREGID As String) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select a.S_Name " &
                      "       ,a.Frame_N_ " &
                      "       ,a.LEAID " &
                      "       ,a.s_state " &
                      "       ,b.D_Name " &
                      "       ,a.MyNAEPREGID " &
                      "       ,c.Study_name " &
                    "from	tblSCSSchool a " &
                    "join	tblSCSDistrict b " &
                    "on	b.LEAID = a.LEAID " &
                    "inner join uv_customize c " &
                    "on c.mynaepregid = a.mynaepregid " &
                    "where a.sysActive = 1 and a.MyNAEPREGID = @MyNAEPREGID "

            da = New SqlDataAdapter(sql, objConn)

            da.SelectCommand.Parameters.AddWithValue("@MyNAEPREGID", MyNAEPREGID)

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function


    Public Function GetRegistrationIDNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.MyNAEPREGID " &
                    ", a.S_Name " &
                    "from	tblSCSSchool a " &
                    "where	a.MyNAEPREGID is not null "

            args.SortParameters.Add(New SortParameter("S_Name", SortDirection.Ascending))

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If f.HasValueParameter() Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            results.Add(New NameValuePair("Select...", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("S_Name").ToString() & " - " & dt.Rows(index)("MyNAEPREGID").ToString(), dt.Rows(index)("MyNAEPREGID")))
            Next

        End Using
        Return results
    End Function

    Public Function getSMPGRDFromMyNAEPRegID(MyNAEPREGID As String) As String

        Dim result As String = ""

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.SMPGRD " &
                    "from	[uv_Customize] a " &
                    "where	a.MyNAEPREGID = @MyNAEPREGID "

            cmd.SelectCommand.Parameters.AddWithValue("MyNAEPREGID", MyNAEPREGID)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If dt.Rows.Count > 0 Then
                result = dt.Rows(0)("SMPGRD")
            End If

        End Using
        Return result

    End Function

    Public Function getProjectNameFromMyNAEPRegID(MyNAEPREGID As String) As String

        Dim result As String = ""

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.fldProjectDesc " &
                    "from	[uv_Customize] a " &
                    "where	a.MyNAEPREGID = @MyNAEPREGID "

            cmd.SelectCommand.Parameters.AddWithValue("MyNAEPREGID", MyNAEPREGID)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If dt.Rows.Count > 0 Then
                result = dt.Rows(0)("fldProjectDesc")
            End If

        End Using
        Return result

    End Function

    Public Function GetFrameNForRegistrationID(MyNAEPREGID As String) As String
        Dim result As String = ""
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.Frame_N_ " &
                    "from	tblSCSSchool a " &
                    "where	a.MyNAEPREGID = @MyNAEPREGID "


            cmd.SelectCommand.Parameters.AddWithValue("MyNAEPREGID", MyNAEPREGID)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If dt.Rows.Count > 0 Then
                result = dt.Rows(0)("Frame_N_")
            End If

        End Using
        Return result

    End Function

    Public Sub SavePSISubmittedBy(frame_n_ As String, submittedby As Guid)


        Dim DbFields As New List(Of NameValuePair)
        DbFields.Add(New NameValuePair("PSISubmittedBy", submittedby.ToString()))
        HandleTableUpdate(DbFields, frame_n_, "tblscsschool", "frame_n_")

    End Sub

    Public Function GetTUA_LEANameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        Dim args As New SelectFromDatabaseArgs
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.TUA_LEA " &
                    "from	uv_Customize a " &
                    "where	a.TUA_LEA is not null " &
                    "group by a.TUA_LEA "

            args.SortParameters.Add(New SortParameter("TUA_LEA", SortDirection.Ascending))

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If f.HasValueParameter() Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            results.Add(New NameValuePair("Select...", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("TUA_LEA").ToString(), dt.Rows(index)("TUA_LEA")))
            Next

        End Using
        Return results
    End Function


    Public Function GetAdvancedEligibilityNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        results.Add(New NameValuePair("Select...", ""))
        results.Add(New NameValuePair("Yes, both Calculus and Physics", "1"))
        results.Add(New NameValuePair("Yes, Calculus only", "2"))
        results.Add(New NameValuePair("Yes, Physics only", "3"))
        results.Add(New NameValuePair("Not eligible", "4"))
        Return results
    End Function

    Public Function GetClassListingForm(id As String) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "SELECT a.ClassListingFormId " &
                      "         ,a.ID " &
                      "         ,a.ClassName " &
                      "         ,a.ClassGroup " &
                      "         ,a.NumberOfStudents " &
                      "         ,a.ClassExclusionStatus " &
                      "         ,a.NameOfMathematicsTeacherG4 " &
                      "         ,a.NameOfMathematicsTeacherEmailG4 " &
                      "         ,a.NameOfScienceTeacherG4 " &
                      "         ,a.NameOfScienceTeacherEmailG4 " &
                      "         ,a.NameOfMathematicsTeacherG8 " &
                      "         ,a.NameOfMathematicsTeacherEmailG8 " &
                      "FROM	tblClassListingForm a " &
                      "WHERE a.ID = @id " &
                      "ORDER BY a.ClassListingFormId"

            cmd.SelectCommand.Parameters.AddWithValue("@id", id)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function



    'Public Function GetScienceTeacherListingForm(id As String) As DataTable
    '    Dim result As New DataTable("TheData")

    '    Using objConn As New SqlConnection(strConn)
    '        objConn.Open()
    '        Dim cmd = New SqlDataAdapter()

    '        cmd.SelectCommand = New SqlCommand()
    '        cmd.SelectCommand.Connection = objConn

    '        Dim sql As String = "select a.ScienceTeacherListingFormId " & _
    '                  "         ,a.ID " & _
    '                  "       ,a.ClassName " & _
    '                  "       ,a.NameOfScienceTeacher " & _
    '                "from	tblScienceTeacherListingForm a " & _
    '                "Where a.ID = @id " & _
    '                "Order by a.ScienceTeacherListingFormId"

    '        cmd.SelectCommand.Parameters.AddWithValue("@id", id)

    '        cmd.SelectCommand.CommandText = sql
    '        cmd.Fill(result)

    '    End Using
    '    Return result
    'End Function

    Public Function GetTeacherNameListingForm(id As String) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.ScienceTeacherListingFormId " &
                      "         ,a.ID " &
                      "       ,a.ClassName " &
                      "       ,a.NameOfScienceTeacher " &
                    "from	tblScienceTeacherListingForm a " &
                    "Where a.ID = @id " &
                    "Order by a.ScienceTeacherListingFormId"

            cmd.SelectCommand.Parameters.AddWithValue("@id", id)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function
    Public Function GetClassListingFormNameValuePairList(id As String) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        Dim dt As DataTable = GetClassListingForm(id)
        results.Add(New NameValuePair("Select Class...", ""))
        For index = 0 To dt.Rows.Count - 1
            results.Add(New NameValuePair(dt.Rows(index)("ClassName").ToString(), dt.Rows(index)("ClassListingFormId")))
        Next
        Return results
    End Function

    Public Function GetClassExclusionStatusNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        results.Add(New NameValuePair("Select...", ""))
        results.Add(New NameValuePair("Students with functional disabilities", "1"))
        results.Add(New NameValuePair("Students with intellectual disabilities", "2"))
        results.Add(New NameValuePair("Non-native language speakers", "3"))
        Return results
    End Function

    Public Function GetClassGroupNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        results.Add(New NameValuePair("Select...", ""))
        results.Add(New NameValuePair("Low", "1"))
        results.Add(New NameValuePair("Average", "2"))
        results.Add(New NameValuePair("High", "3"))
        Return results
    End Function

    Public Sub DeleteClassListingFormItem(id As String, ClassListingFormId As Integer)

        'clear freqs that are linked to the class before deleting it
        'Dim nvp As New List(Of NameValuePair)
        'nvp.Add(New NameValuePair("ClassListingFormId", Nothing))
        'HandleTableUpdate(nvp, ClassListingFormId, "tblEfileResponseFreq", "ClassListingFormId")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.UpdateCommand = New SqlCommand()
            cmd.UpdateCommand.Connection = objConn


            Dim sql As String = "delete from tblClassListingForm " &
                    "Where ID = @id and ClassListingFormId = @ClassListingFormId"

            cmd.UpdateCommand.Parameters.AddWithValue("@id", id)
            cmd.UpdateCommand.Parameters.AddWithValue("@ClassListingFormId", ClassListingFormId)

            cmd.UpdateCommand.CommandText = sql
            cmd.UpdateCommand.ExecuteNonQuery()

        End Using
    End Sub

    Public Sub DeleteScienceTeacherListingFormItem(id As String, ScienceTeacherListingFormId As Integer)


        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.UpdateCommand = New SqlCommand()
            cmd.UpdateCommand.Connection = objConn


            Dim sql As String = "delete from tblScienceTeacherListingForm " &
                    "Where ID = @id and ScienceTeacherListingFormId = @ScienceTeacherListingFormId"

            cmd.UpdateCommand.Parameters.AddWithValue("@id", id)
            cmd.UpdateCommand.Parameters.AddWithValue("@ScienceTeacherListingFormId", ScienceTeacherListingFormId)

            cmd.UpdateCommand.CommandText = sql
            cmd.UpdateCommand.ExecuteNonQuery()

        End Using
    End Sub

    Public Function GetEfileUploadsByGradeId(id As String, EfileTypeId As Integer) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.FileId " &
                      "         ,a.EFileTypeId " &
                      "         ,a.UploadedBy " &
                      "       ,a.UserFilePath " &
                      "       ,a.Filesize " &
                      "       ,a.UploadDT " &
                      "       ,a.ContentType " &
                      "       ,a.ID " &
                      "       ,a.Frame_N_ " &
                      "       ,a.SmpGrd " &
                      "       ,a.fldProjectID " &
                      "       ,coalesce(b.FirstName, '') + ' ' + coalesce(b.LastName, '') UploadedByFirstAndLastName " &
                      "       ,coalesce(b.LastName, '') + ', ' + coalesce(b.FirstName, '') UploadedByLastNameComaFirstName " &
                      "       ,a.IsSynchronous " &
                      "       ,a.TotalColumns " &
                      "       ,a.ExpectedRows " &
                      "       ,a.TotalRows " &
                      "       ,a.TableObject " &
                      "       ,a.HasFilteredData " &
                      "       ,a.fldProjectID " &
                    "from	tblEfileUploads a " &
                    "left outer join aspnet_ProfileTIMSS b " &
                    "on		b.UserId = a.UploadedBy " &
                    "Where a.ID = @id " &
                    "and a.EfileTypeId = @EfileTypeId " &
                    "Order by a.UploadDT"

            cmd.SelectCommand.Parameters.AddWithValue("@id", id)
            cmd.SelectCommand.Parameters.AddWithValue("@EfileTypeId", EfileTypeId)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Sub EfileImportProcessCalculateResponseFreq(usercolumnids As List(Of Integer), grade As Integer, fileid As Integer)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.UpdateCommand = New SqlCommand()
            cmd.UpdateCommand.Connection = objConn

            Dim ColumnSeq As Integer = 0
            For Each col As Integer In usercolumnids

                Dim sql As String = "INSERT INTO 	dbo.tblEfileResponseFreq (dbo.tblEfileResponseFreq.UserColumnId, dbo.tblEfileResponseFreq.Response, dbo.tblEfileResponseRates.TotalResponsesGrade" & grade & ")" &
          " SELECT   " & col & " as UserColumnId,  C" & ColumnSeq & ", COUNT(C" & ColumnSeq & ") AS TotalResponsesGrade" & grade &
   " FROM        tblEfileStudentData" &
   " Where FileId = " & fileid &
 "	 GROUP BY C" & ColumnSeq &
 "	 ORDER BY C" & ColumnSeq


                cmd.UpdateCommand.CommandText = sql
                cmd.UpdateCommand.ExecuteNonQuery()

                ColumnSeq = ColumnSeq + 1
            Next

        End Using

    End Sub

    Public Function GetEfileUserColumns(fileid As Integer) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.UserColumnId " &
                      "         ,a.FileId " &
                      "       ,a.ColumnSeq " &
                      "       ,a.UserColumnLabel " &
                      "       ,a.NaepLabelId " &
                    "from	tblEfileUserColumns a " &
                    "Where a.FileId = @fileid " &
                    "Order by a.ColumnSeq"

            cmd.SelectCommand.Parameters.AddWithValue("@fileid", fileid)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetNaepColumnLabels(file As File) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.NaepLabelId " &
                      "         ,a.NaepLabel " &
                    "from	tblEfileNaepLabels a " &
                    "Where a.LabelIsVisible = 1 " &
                    "and EfileTypeId = @EfileTypeId " &
                    "Order by a.LabelDisplayOrder"

            cmd.SelectCommand.Parameters.AddWithValue("@EfileTypeId", file.EFileType)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            results.Add(New NameValuePair("Column Contains ...", ""))
            Dim addit As Boolean = True
            For index = 0 To dt.Rows.Count - 1
                addit = False
                If file.isICILS() And file.IsSubmitTeacherList() And (dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Last Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher First Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Middle Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Email", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Sex", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Year of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Subject", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("N/A", StringComparison.CurrentCultureIgnoreCase)
                                       ) Then
                    addit = True
                ElseIf file.isICILS() And file.IsSingleGradeFile() And (dt.Rows(index)("NaepLabel").ToString().Equals("Student Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Student Name: Last", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Student Name: First", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Student Name: Middle", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Sex", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Date of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Month of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Year of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Student with a Disability", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("English Language Learner", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Class Name", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                                        Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher Email", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Subject", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("N/A", StringComparison.CurrentCultureIgnoreCase)
                                       ) Then
                    addit = True
                ElseIf file.iseTIMSS() And
                    (dt.Rows(index)("NaepLabel").ToString().Equals("Student Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Student Name: Last", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Student Name: First", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Student Name: Middle", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                                          Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher Email", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher Last Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher First Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics: Teacher Middle Name", StringComparison.CurrentCultureIgnoreCase) _
                                           Or dt.Rows(index)("NaepLabel").ToString().Equals("Science: Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                                           Or dt.Rows(index)("NaepLabel").ToString().Equals("Science: Teacher Email", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Science: Teacher Last Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Science: Teacher First Name", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Science: Teacher Middle Name", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Sex", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Date of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Month of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Year of Birth", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("Students with a Disability Status", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("English Language Learner Status", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Subject", StringComparison.CurrentCultureIgnoreCase) _
                                         Or dt.Rows(index)("NaepLabel").ToString().Equals("Class", StringComparison.CurrentCultureIgnoreCase) _
                                       Or dt.Rows(index)("NaepLabel").ToString().Equals("N/A", StringComparison.CurrentCultureIgnoreCase)
                                       ) Then
                    addit = True
                End If

                'If file.FileHasGrade12 And file.FileHasGrade4 = False And file.FileHasGrade8 = False And dt.Rows(index)("NaepLabel").ToString().Equals("Class", StringComparison.CurrentCultureIgnoreCase) Then
                '    addit = False
                'ElseIf file.FileHasGrade4 And file.FileHasGrade12 = False And file.FileHasGrade8 = False And _
                '    (dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics Teacher Name: Last", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics Teacher Name: First", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Mathematics Teacher Name: Middle", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Science Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Science Teacher Name: Last", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Science Teacher Name: First", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Science Teacher Name: Middle", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Day of Birth", StringComparison.CurrentCultureIgnoreCase)) Then
                '    addit = False
                'ElseIf file.FileHasGrade8 And file.FileHasGrade12 = False And file.FileHasGrade4 = False And _
                '    (dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Name", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Name: Last", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Name: First", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Teacher Name: Middle", StringComparison.CurrentCultureIgnoreCase) _
                '     Or dt.Rows(index)("NaepLabel").ToString().Equals("Birth Date: Day of Birth", StringComparison.CurrentCultureIgnoreCase)) Then
                '    addit = False
                '    '
                'End If
                If addit Then
                    results.Add(New NameValuePair(dt.Rows(index)("NaepLabel").ToString(), dt.Rows(index)("NaepLabelId")))
                End If
            Next

        End Using
        Return results
    End Function


    Public Function GetNaepCodes(NaepLabelId As Integer) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.NaepCodeId " &
                      "         ,a.CodeLabel " &
                    "from	tblEfileNaepCodes a " &
                    "Where a.NaepLabelId = @NaepLabelId " &
                    "and a.CodeIsVisible = 1 " &
                    "Order by a.CodeDisplayOrder"

            cmd.SelectCommand.Parameters.AddWithValue("@NaepLabelId", NaepLabelId)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            results.Add(New NameValuePair("Select Code", ""))
            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("CodeLabel").ToString(), dt.Rows(index)("NaepCodeId")))
            Next

        End Using
        Return results
    End Function


    Public Function GetEfileResponseFreqs(UserColumnId As Integer) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.ResponseFreqId " &
                      "         ,a.UserColumnId " &
                      "       ,a.Response " &
                      "       ,a.NaepCodeId " &
                      "       ,b.NaepLabelId " &
                      "       ,a.TotalResponsesGrade4 " &
                      "       ,a.TotalResponsesGrade8 " &
                      "       ,a.TotalResponsesGrade12 " &
                      "       ,a.ClassListingFormId " &
                      "       ,c.ID " &
                    "from	tblEfileResponseFreq a " &
                    "join	tblEfileUserColumns b " &
                    "on		b.UserColumnId = a.UserColumnId " &
                    "join	tblEfileUploads c " &
                    "on		c.FileId = b.FileId " &
                    "Where a.UserColumnId = @UserColumnId " &
                    "Order by a.Response"

            cmd.SelectCommand.Parameters.AddWithValue("@UserColumnId", UserColumnId)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Function GetEfileColumnsWithValuesToMap(FileId As Integer) As DataTable
        Dim results As New DataTable

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.UserColumnId " &
                      "         ,a.NaepLabelId " &
                      "         ,a.UserColumnLabel " &
                      "         ,b.NaepLabel " &
                    "from	tblEfileUserColumns a " &
                    "join	tblEfileNaepLabels b " &
                    "on		b.NaepLabelId = a.NaepLabelId " &
                    "Where a.FileId = @FileId " &
                    "and b.DoFreq = 1 " &
                    "Order by b.LabelDisplayOrder"

            cmd.SelectCommand.Parameters.AddWithValue("@FileId", FileId)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(results)

            'results.Add(New NameValuePair("Select...", ""))
            'For index = 0 To dt.Rows.Count - 1
            '    results.Add(New NameValuePair(dt.Rows(index)("UserColumnId"), dt.Rows(index)("NaepLabelId")))
            'Next

        End Using
        Return results
    End Function


    Public Function GetFinalResponseFreqs(fileid As Integer) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select * from	(SELECT dbo.tblEfileNaepLabels.NaepLabel, dbo.tblEfileNaepCodes.CodeLabel, SUM(dbo.tblEfileResponseFreq.TotalResponsesGrade4)  AS TotalResponsesGrade4, SUM(dbo.tblEfileResponseFreq.TotalResponsesGrade8) AS TotalResponsesGrade8,  SUM(dbo.tblEfileResponseFreq.TotalResponsesGrade12) AS TotalResponsesGrade12, dbo.tblEfileNaepCodes.NaepCodeId, dbo.tblEfileResponseFreq.UserColumnId, dbo.tblEfileNaepLabels.LabelDisplayOrder, dbo.tblEfileNaepCodes.CodeDisplayOrder " &
             " FROM         dbo.tblEfileResponseFreq  INNER JOIN " &
       "  dbo.tblEfileUserColumns ON dbo.tblEfileResponseFreq.UserColumnId = dbo.tblEfileUserColumns.UserColumnId INNER JOIN " &
       "  dbo.tblEfileNaepLabels ON dbo.tblEfileUserColumns.NaepLabelId = dbo.tblEfileNaepLabels.NaepLabelId LEFT OUTER JOIN " &
       "  dbo.tblEfileNaepCodes ON dbo.tblEfileResponseFreq.NaepCodeId = dbo.tblEfileNaepCodes.NaepCodeId " &
        " WHERE (dbo.tblEfileUserColumns.FileId = @FileId) And (dbo.tblEfileNaepLabels.DoFreq = 1) and (dbo.tblEfileNaepLabels.NaepLabel not in ('Class'))   " &
     " GROUP BY dbo.tblEfileNaepLabels.NaepLabel, dbo.tblEfileNaepLabels.NaepLabelID, dbo.tblEfileNaepCodes.CodeLabel, dbo.tblEfileNaepCodes.NaepCodeId, " &
  "	      dbo.tblEfileNaepLabels.LabelDisplayOrder, dbo.tblEfileNaepCodes.CodeDisplayOrder,dbo.tblEfileResponseFreq.UserColumnId " &
     " union all " &
     " SELECT     dbo.tblEfileNaepLabels.NaepLabel, dbo.tblClassListingForm.ClassName, SUM(dbo.tblEfileResponseFreq.TotalResponsesGrade4)  AS TotalResponsesGrade4, SUM(dbo.tblEfileResponseFreq.TotalResponsesGrade8) AS TotalResponsesGrade8,  SUM(dbo.tblEfileResponseFreq.TotalResponsesGrade12) AS TotalResponsesGrade12, dbo.tblClassListingForm.ClassListingFormId,  dbo.tblEfileResponseFreq.UserColumnId, dbo.tblEfileNaepLabels.LabelDisplayOrder, dbo.tblClassListingForm.ClassListingFormId " &
     " FROM         dbo.tblEfileResponseFreq  INNER JOIN   dbo.tblEfileUserColumns ON dbo.tblEfileResponseFreq.UserColumnId = dbo.tblEfileUserColumns.UserColumnId INNER JOIN   dbo.tblEfileNaepLabels ON dbo.tblEfileUserColumns.NaepLabelId = dbo.tblEfileNaepLabels.NaepLabelId LEFT OUTER JOIN   dbo.tblClassListingForm ON dbo.tblEfileResponseFreq.ClassListingFormId = dbo.tblClassListingForm.ClassListingFormId  " &
     " WHERE (dbo.tblEfileUserColumns.FileId = @FileId) And (dbo.tblEfileNaepLabels.DoFreq = 1) and (dbo.tblEfileNaepLabels.NaepLabel in ('Class')) " &
     " GROUP BY dbo.tblEfileNaepLabels.NaepLabel, dbo.tblEfileNaepLabels.NaepLabelID, dbo.tblClassListingForm.ClassName, dbo.tblClassListingForm.ClassListingFormId, " &
  "	      dbo.tblEfileNaepLabels.LabelDisplayOrder, dbo.tblEfileNaepLabels.LabelDisplayOrder, dbo.tblEfileResponseFreq.UserColumnId) a " &
  " order by a.LabelDisplayOrder, a.CodeDisplayOrder"

            cmd.SelectCommand.Parameters.AddWithValue("@FileId", fileid)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetEfileDetailsDataRow(fileid As Integer) As DataRow
        'Dim result As New DataTable("TheData")
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.FileId " &
                      "         ,a.EfileTypeId " &
                      "         ,a.UploadedBy " &
                      "       ,a.UserFilePath " &
                      "       ,a.Filesize " &
                      "       ,a.UploadDT " &
                      "       ,a.ContentType " &
                      "       ,a.ID " &
                      "       ,a.Frame_N_ " &
                      "       ,a.SmpGrd " &
                      "       ,a.fldProjectID " &
                      "       ,a.IsSynchronous " &
                      "       ,a.TotalColumns " &
                      "       ,a.ExpectedRows " &
                      "       ,a.TotalRows " &
                      "       ,a.TableObject " &
                      "       ,a.HasFilteredData " &
                      "       ,a.fldProjectID " &
                      "       ,b.LEAID " &
                      "       ,b.D_Name " &
                      "       ,b.S_Name " &
                    "from	tblEfileUploads a " &
                    "join	uv_Customize b " &
                    "on	b.id = a.ID " &
                    "and b.fldProjectID = a.fldProjectID " &
                    "Where a.fileid = @fileid "

            cmd.SelectCommand.Parameters.AddWithValue("@fileid", fileid)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dset)

            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function



    Public Function GetEfileDetailsDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.FileId " &
                                  "       ,a.UploadDT " &
                                  "       ,a.UploadedBy " &
                                  "       ,b.FirstName + ' ' + b.LastName Fullname" &
                                  "       ,a.Filesize " &
                                  "       ,a.Frame_N_ " &
                                  "       ,a.ID " &
                                  "       ,a.SmpGrd " &
                                  "       ,a.TotalRows " &
                                  "       ,a.ContentType " &
                                  "       ,a.UserFilePath " &
                                  "       ,c.S_Name " &
                                  "       ,c.LEAID " &
                                  "       ,c.D_Name " &
                                  "       ,a.EfileStatusLogId " &
                                  "       ,a.EfileStatusUID " &
                                  "       ,a.EfileStatusFname " &
                                  "       ,a.EfileStatusLname " &
                                  "       ,a.EfileStatusFname + ' ' + a.EfileStatusLname EfileStatusFullname" &
                                  "       ,a.EfileStatusCode " &
                                  "       ,a.EfileStatusCodeIsError " &
                                  "       ,a.EfileStatusEditDT " &
                                  "       ,a.DPStatusLogId " &
                                  "       ,a.DPStatusUID " &
                                  "       ,a.DPStatusFname " &
                                  "       ,a.DPStatusLname " &
                                  "       ,a.DPStatusFname + ' ' + a.DPStatusLname DPStatusFullname" &
                                  "       ,a.DPStatusCode " &
                                  "       ,a.DPStatusCodeIsError " &
                                  "       ,a.DPStatusEditDT " &
                                  "       ,c.ClassListSubmited " &
                                  "       ,c.ClassListSubmitedBy " &
                                  "       ,c.ClassListSubmitedByFirstName " &
                                  "       ,c.ClassListSubmitedByLastName " &
                                "from	[tblEfileUploads] a " &
                                "join	(select  UserId, FirstName, LastName from [uv_AccountDetails]) b  " &
                                "on		b.UserId = a.UploadedBy " &
                                "join	(select id cid, fldProjectID, S_Name, LEAID, D_Name, ClassListSubmited, ClassListSubmitedBy, ClassListSubmitedByFirstName,ClassListSubmitedByLastName  from [uv_customize]) c " &
                                "on		c.cid = a.id " &
                                "and	c.fldProjectID = a.fldProjectID "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function

    Public Function GetEfileStatusHistoryDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.FileId " &
                                  "       ,a.StatusEditDT " &
                                  "       ,a.FirstName + ' ' + a.LastName Fullname" &
                                  "       ,a.[Status] " &
                                  "       ,a.IsError " &
                                "from	tblEfileStatusLog a "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Function GetColumnsAndCodes() As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.NaepLabel " &
                      "         ,a.DoFreq " &
                      "       ,a.LabelIsVisible " &
                      "       ,b.CodeLabel " &
                      "       ,b.CodeValue " &
                    "from	tblEfileNaepLabels a " &
                    "left outer join	tblEfileNaepCodes b " &
                    "on		b.NaepLabelId = a.NaepLabelId " &
                    "Order by a.LabelDisplayOrder" &
                    "   ,b.CodeDisplayOrder "


            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Function GetClassListSubmitedDataRow(args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = "select distinct a.ClassListSubmited " &
                              "       ,a.ClassListSubmitedBy " &
                              "       ,a.ClassListSubmitedByFirstName " &
                              "       ,a.ClassListSubmitedByLastName " &
                            "from	uv_Customize a "

            Dim cnt As Integer = 0


            If args.IsNaepStateCoordinator Then
                sql = sql & IIf(cnt = 0, " where ", " and ") & "a.[LEAID] in (select distinct a.LEAID from uv_Customize a where	a.REPSBGRP = @REPSBGRP and isPublic = 1)"
                cnt = cnt + 1
            ElseIf args.IsFieldDirector Or args.IsFieldManager Then
                sql = sql & IIf(cnt = 0, " where ", " and ") & "a.[LEAID] in (select distinct a.LEAID from uv_Customize a join NAEPFRS2015.[dbo].[uv_TIMSSStaffTRA] b on b.fldTerritoryCode = a.fldTerritoryCode " & IIf(args.IsFieldManager, " and b.fldRegionCode = a.fldRegionCode ", "") & "  where	b.fldWINSID = @fldWINSID)"
                cnt = cnt + 1
            End If

            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            da = New SqlDataAdapter(sql, objConn)

            If args.IsNaepStateCoordinator Then
                da.SelectCommand.Parameters.AddWithValue("@REPSBGRP", args.REPSBGRP)
            ElseIf args.IsFieldDirector Or args.IsFieldManager Then
                da.SelectCommand.Parameters.AddWithValue("@fldWINSID", args.WINSID)
            End If
            'da.SelectCommand.Parameters.AddWithValue("@leaid", leaid)

            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next


            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function


    Public Function GetTestRegionNameValuePairList(args As SelectFromDatabaseArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.testregion " &
                    "from	uv_Customize a "

            args.SortParameters.Add(New SortParameter("testregion", SortDirection.Ascending))

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            'sql = sql & _
            '"Where a.ScheDate is not null "

            Dim cnt As Integer = 1
            sql = sql & " where testregion is not null "

            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)


            If args.IsAdmin Or args.IsNaepStateCoordinator Or args.IsHomeOffice Then results.Add(New NameValuePair("All", ""))

            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("testregion").ToString(), dt.Rows(index)("testregion")))
            Next

        End Using
        Return results
    End Function


    Public Function GetSmpGrdNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)
        results.Add(New NameValuePair("All", ""))
        results.Add(New NameValuePair("4", "4"))
        results.Add(New NameValuePair("8", "8"))
        results.Add(New NameValuePair("12", "12"))
        Return results
    End Function


    Public Function GetDistinctEfileStatusNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.EfileStatusCode " &
                    "from	tblEfileUploads a " &
                    "where	a.EfileStatusCode is not null " &
                    " order by a.EfileStatusCode"


            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)


            results.Add(New NameValuePair("All", ""))

            For index = 0 To dt.Rows.Count - 1
                If dt.Rows(index)("EfileStatusCode").ToString().Length > 50 Then
                    results.Add(New NameValuePair(System.Web.HttpContext.Current.Server.HtmlEncode(dt.Rows(index)("EfileStatusCode").ToString().Substring(0, 50)), System.Web.HttpContext.Current.Server.HtmlEncode(dt.Rows(index)("EfileStatusCode"))))
                Else
                    results.Add(New NameValuePair(System.Web.HttpContext.Current.Server.HtmlEncode(dt.Rows(index)("EfileStatusCode")), System.Web.HttpContext.Current.Server.HtmlEncode(dt.Rows(index)("EfileStatusCode"))))
                End If
            Next

        End Using
        Return results
    End Function


    Public Function GetDistincDPStatusNameValuePairList() As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select distinct a.DPStatusCode " &
                    "from	tblEfileUploads a " &
                    "where	a.DPStatusCode is not null " &
                    " order by a.DPStatusCode"


            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)


            results.Add(New NameValuePair("All", ""))

            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("DPStatusCode").ToString(), dt.Rows(index)("DPStatusCode")))
            Next

        End Using
        Return results
    End Function


    Public Function GetClassListSubmittedDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.ID " &
                                  "       ,a.SmpGrd " &
                                  "       ,a.S_Name " &
                                  "       ,a.LEAID " &
                                  "       ,a.D_Name " &
                                  "       ,a.ClassListSubmited " &
                                  "       ,a.ClassListSubmitedBy " &
                                  "       ,a.ClassListSubmitedByFirstName " &
                                  "       ,a.ClassListSubmitedByLastName " &
                                "from	 [uv_customize] a " &
                                "where	 ClassListSubmited is not null "

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Sub ParseEfile(fileid As Integer)

        Dim lst As New List(Of NameValuePair)
        lst.Add(New NameValuePair("fileid", fileid))

        HandleStoredProc(lst, "usp_EfileVerificationProcess_ClearParsed")
        HandleStoredProc(lst, "usp_EfileVerificationProcess_ConvertedStudentData")
    End Sub

    Public Sub HandleStoredProc(NameValuePairList As List(Of NameValuePair), procname As String)
        Using objConn As New SqlConnection(strConn)
            objConn.Open()


            Dim cmd = New SqlCommand
            cmd.Connection = objConn
            cmd.CommandText = procname
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("RC", SqlDbType.Int).Direction = ParameterDirection.ReturnValue

            Dim name As String = ""
            Dim value As String = ""
            Dim cnt As Integer = 0

            For Each nv As NameValuePair In NameValuePairList
                name = nv.Name
                value = nv.Value


                If Not value Is Nothing Then
                    cmd.Parameters.AddWithValue(name, value)
                Else
                    cmd.Parameters.AddWithValue(name, "")
                End If
                cnt = cnt + 1

            Next

            cmd.ExecuteNonQuery()

        End Using
    End Sub

    Public Function GetEfileCleanedStudetDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select	a.* " &
                                "from	tblEfileCleanedStudentData a "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)

        End Using
        Return result
    End Function


    Public Function GetAssessmentDateTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.ID " &
                      "       ,a.fldProjectDesc Project " &
                      "       ,a.DispName " &
                      "       ,a.Disp " &
                      "       ,a.testregion " &
                      "       ,a.fldAreaCode " &
                      "       ,a.S_County " &
                      "       ,a.D_Name " &
                    "       ,a.ScheDate " &
                    "       ,a.ScheTime " &
                    "       ,a.AssessmentCompleted " &
                    "       ,a.DateOfMakeUp " &
                    "       ,a.fldTerritoryCode " &
                    "       ,a.ScheDate2 " &
                    "       ,a.ScheTime2 " &
                     "       ,a.Study_Name " &
                    "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)


            sql = sql &
            "Where (a.ScheDate is not null or a.ScheDate2 is not null) and a.Disp not in ('30','32','33','34','40','41','42','07') "

            'sql = sql & _
            '"Where a.ScheDate is not null and a.Disp in ('00', '01', '02') "

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)


        End Using
        Return result
    End Function



    Public Function GetSchoolCompletionReportDateTable(args As GetSchoolListSqlDataSourceArgs) As DataTable
        Dim result As New DataTable("TheData")


        HandleFilterShortcuts(args)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim cnt As Integer = 1
            Dim sqlFilterCount As String = ""
            Dim sqlJoinCount As String = HandleJoinToCustomizeViewToFilterByRole(args, "")

            sqlJoinCount = sqlJoinCount.Replace(".[uv_TIMSSStaffTRA] b ", ".[uv_TIMSSStaffTRA] c ")
            sqlJoinCount = sqlJoinCount.Replace("b.", "c.")
            sqlJoinCount = sqlJoinCount.Replace("a.", "b.")

            For Each f As FilterParameter In args.FilterParameters
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sqlFilterCount = sqlFilterCount & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " b.[")
                Else
                    sqlFilterCount = sqlFilterCount & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If

                cnt = cnt + 1
            Next

            Dim sql As String = "select a.DISP " &
                      "       ,a.DispName " &
                      "       ,count(*) Total " &
                      "       /*,isnull((select count(*) from uv_Customize b " & sqlJoinCount & " where b.DISP = a.DISP " & sqlFilterCount & "), 0) Total*/ " &
                      "       ,isnull((select count(*) from uv_Customize b " & sqlJoinCount & " where b.DISP = a.DISP and b.ORIGSUB = 'o' " & sqlFilterCount & "), 0) OriginalTotal " &
                      "       ,isnull((select count(*) from uv_Customize b " & sqlJoinCount & " where b.DISP = a.DISP and b.ORIGSUB = 's' " & sqlFilterCount & "), 0) SubTotal " &
                    "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)


            'sql = sql & _
            '"Where a.ScheDate is not null and a.Disp in ('00', '01', '02') "

            cnt = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            sql = sql & " group by a.DISP, a.DispName "

            cnt = 0


            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If


            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)


        End Using
        Return result
    End Function


    Public Function GetQuestionnaireStatusReportSchoolNameValuePairList(args As GetSchoolListSqlDataSourceArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.ID " &
                    "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)

            'sql = sql & _
            '"Where a.ScheDate is not null and a.Disp in ('00', '01', '02') "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0


            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If


            'Dim dt As New DataTable
            'cmd.SelectCommand.CommandText = sql
            'cmd.Fill(result)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            For index = 0 To dt.Rows.Count - 1
                results.Add(New NameValuePair(dt.Rows(index)("id").ToString() & " - " & dt.Rows(index)("S_Name").ToString(), dt.Rows(index)("id")))
            Next


        End Using
        Return results
    End Function


    Public Function GetQuestionnaireStatusReportSchoolDetailsValuePairList(args As GetSchoolListSqlDataSourceArgs) As List(Of NameValuePair)
        Dim results As New List(Of NameValuePair)

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.ID " &
                      "       ,a.S_Addr1 " &
                      "       ,a.S_City " &
                      "       ,a.S_State " &
                      "       ,a.S_Zip " &
                      "       ,a.S_Phone " &
                      "       ,a.testregion " &
                      "       ,a.fldAreaCode " &
                      "       ,a.sc_name " &
                      "       ,a.sc_phone " &
                      "       ,a.sc_email " &
                    "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)

            'sql = sql & _
            '"Where a.ScheDate is not null and a.Disp in ('00', '01', '02') "

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0


            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If


            'Dim dt As New DataTable
            'cmd.SelectCommand.CommandText = sql
            'cmd.Fill(result)

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim index As Integer = 0
                results.Add(New NameValuePair("School Name", IIf(dt.Rows(index)("S_Name") Is DBNull.Value, "", dt.Rows(index)("S_Name"))))
                results.Add(New NameValuePair("Address", IIf(dt.Rows(index)("S_Addr1") Is DBNull.Value, "S_Addr1", dt.Rows(index)("S_Addr1"))))
                results.Add(New NameValuePair("City", IIf(dt.Rows(index)("S_City") Is DBNull.Value, "", dt.Rows(index)("S_City"))))
                results.Add(New NameValuePair("State", IIf(dt.Rows(index)("S_State") Is DBNull.Value, "", dt.Rows(index)("S_State"))))
                results.Add(New NameValuePair("Zip", IIf(dt.Rows(index)("S_Zip") Is DBNull.Value, "", dt.Rows(index)("S_Zip"))))
                results.Add(New NameValuePair("TIMSS ID", IIf(dt.Rows(index)("id") Is DBNull.Value, "", dt.Rows(index)("id"))))
                results.Add(New NameValuePair("Region", IIf(dt.Rows(index)("testregion") Is DBNull.Value, "", dt.Rows(index)("testregion"))))
                results.Add(New NameValuePair("SC Name", IIf(dt.Rows(index)("sc_name") Is DBNull.Value, "", dt.Rows(index)("sc_name"))))
                results.Add(New NameValuePair("SC Phone", IIf(dt.Rows(index)("sc_phone") Is DBNull.Value, "", dt.Rows(index)("sc_phone"))))
                results.Add(New NameValuePair("SC Email", IIf(dt.Rows(index)("sc_email") Is DBNull.Value, "", dt.Rows(index)("sc_email"))))

                results.Add(New NameValuePair("Online Ques Rec'd", "No"))
                results.Add(New NameValuePair("Paper Ques Rec'd", "No"))
                results.Add(New NameValuePair("School Administrator Name", ""))
                results.Add(New NameValuePair("SPID", ""))
            End If

        End Using
        Return results
    End Function


    Public Function GetSTLFFilePath(args As GetSchoolListSqlDataSourceArgs) As String
        Dim result As String = Nothing

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.STLFUserFilePath " &
                    "from	uv_Customize a "

            'sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            'HandleFilterShortcuts(args)

            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0


            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If

            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dt)

            If dt.Rows.Count > 0 Then
                Dim index As Integer = 0
                result = IIf(dt.Rows(index)("STLFUserFilePath") Is DBNull.Value, Nothing, dt.Rows(index)("STLFUserFilePath").ToString())
            End If

        End Using
        Return result
    End Function


    Public Function GetManageDocumentsDataTable(args As SelectFromDatabaseArgs) As DataTable
        Dim result As New DataTable("TheData")

        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.S_Name " &
                      "       ,a.ID " &
                      "       ,a.IEA_ID " &
                      "       ,a.STLFUserFilePath " &
                      "       ,a.Frame_N_ " &
                      "       ,a.SmpGrd " &
                      "       ,a.TTFUserFilePath " &
                      "       ,a.STFUserFilePath " &
                      "       ,a.STF2UserFilePath " &
                      "       ,a.STLF2UserFilePath " &
                    "from	uv_Customize a "

            sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            HandleFilterShortcuts(args)


            Dim cnt As Integer = 0
            For Each f As FilterParameter In args.FilterParameters
                If Not f.ComparisonOperator.Equals("in") Then
                    cmd.SelectCommand.Parameters.AddWithValue(f.FilterColumn & f.Index, f.FilterValue)
                End If
                If f.FilterColumn.Equals("fldTerritoryCode") Then
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                Else
                    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0


            If args.SortParameters.Count > 0 Then
                sql = sql & " order by "
                For Each s As SortParameter In args.SortParameters
                    sql = sql & IIf(cnt = 0, "", ", ") & s.SortExpression
                    cnt = cnt + 1
                Next
            End If


            Dim dt As New DataTable
            cmd.SelectCommand.CommandText = sql
            cmd.Fill(result)


        End Using
        Return result
    End Function


    Public Function GetTIMSSStaffTRADataRow(winsid As String) As DataRow
        'Dim result As New DataTable("TheData")
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.fldWINSID " &
                      "         ,a.fldfirstname " &
                      "       ,a.fldmiddlename " &
                      "       ,a.fldLastName " &
                      "       ,a.Corporate_FOS_SMTPAlias " &
                    "from	NAEPFRS2015.[dbo].[uv_TIMSSStaffTRA] a " &
                    "Where a.fldWINSID = @fldWINSID "

            cmd.SelectCommand.Parameters.AddWithValue("@fldWINSID", winsid)

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dset)

            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)
            Else

            End If
        End Using
        Return result
    End Function

    Public Function HasWINSIDAlreadyBeenLinked(winsid As String, userid As String) As Boolean
        'Dim result As New DataTable("TheData")
        Dim result As Boolean = Nothing

        Dim dset As New DataSet
        Using objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim cmd = New SqlDataAdapter()

            cmd.SelectCommand = New SqlCommand()
            cmd.SelectCommand.Connection = objConn

            Dim sql As String = "select a.WINSID " &
                    "from	uv_AccountDetails a " &
                    "Where a.WINSID = @fldWINSID "

            cmd.SelectCommand.Parameters.AddWithValue("@fldWINSID", winsid)

            If Not userid Is Nothing Then
                sql = sql & "and a.UserId <> @userid "
                cmd.SelectCommand.Parameters.AddWithValue("@userid", userid)
            End If

            cmd.SelectCommand.CommandText = sql
            cmd.Fill(dset)

            If dset.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
            End If
        End Using
        Return result
    End Function


    Public Function GetDocumentsDataRowForStream(whichdocument As String, id As String, args As SelectFromDatabaseArgs, showAll As Boolean) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = Nothing

            If whichdocument.Equals("Efile") Then
                sql = "select a.FileData " &
                      "       ,a.Filesize " &
                      "       ,a.ContentType " &
                      "       ,a.UserFilePath Filename " &
                    "from	tblEfileUploads a " &
                    "where FileId = @FileId "
            ElseIf whichdocument.Equals("STLF") Then
                sql = "select b.FileData " &
                      "       ,b.Filesize " &
                      "       ,b.ContentType " &
                      "       ,b.Filename " &
                    "from	tblSCSGrade a " &
                    "join	tblGradeFiles b " &
                    "on	b.GradeFileId =  a.STLFGradeFileId " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("STF") Then
                sql = "select b.FileData " &
                      "       ,b.Filesize " &
                      "       ,b.ContentType " &
                      "       ,b.Filename " &
                    "from	tblSCSGrade a " &
                    "join	tblGradeFiles b " &
                    "on	b.GradeFileId =  a.STFGradeFileId " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("TTF") Then
                sql = "select b.FileData " &
                      "       ,b.Filesize " &
                      "       ,b.ContentType " &
                      "       ,b.Filename " &
                    "from	tblSCSGrade a " &
                    "join	tblGradeFiles b " &
                    "on	b.GradeFileId =  a.TTFGradeFileId " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("STF2") Then
                sql = "select b.FileData " &
                      "       ,b.Filesize " &
                      "       ,b.ContentType " &
                      "       ,b.Filename " &
                    "from	tblSCSGrade a " &
                    "join	tblGradeFiles b " &
                    "on	b.GradeFileId =  a.STF2GradeFileId " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("STLF2") Then
                sql = "select b.FileData " &
                      "       ,b.Filesize " &
                      "       ,b.ContentType " &
                      "       ,b.Filename " &
                    "from	tblSCSGrade a " &
                    "join	tblGradeFiles b " &
                    "on	b.GradeFileId =  a.STLF2GradeFileId " &
                    "where a.ID = @ID "
            End If


            If showAll = False Then
                sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)
            End If

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")

                'If f.FilterColumn.Equals("fldTerritoryCode") Then
                '    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                'Else
                '    sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                'End If
                'sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression
                cnt = cnt + 1
            Next

            cnt = 0

            da = New SqlDataAdapter(sql, objConn)


            If whichdocument.Equals("Efile") Then
                da.SelectCommand.Parameters.AddWithValue("@FileId", id)
            ElseIf whichdocument.Equals("STLF") Or whichdocument.Equals("TTF") Or whichdocument.Equals("STF") Or whichdocument.Equals("STF2") Or whichdocument.Equals("STLF2") Then
                da.SelectCommand.Parameters.AddWithValue("@ID", id)
            End If

            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)

            Else

            End If
        End Using
        Return result
    End Function

    Public Function ProcessfileUploadForGrade(which As String, ID As String, UploadedBy As String, Filename As String, Filesize As Integer, FileData As Byte(), ContentType As String) As Boolean
        Dim cnt As Integer = -1
        Using objConn As New SqlConnection(strConn)
            objConn.ConnectionString = strConn
            objConn.Open()
            Dim cmd = New SqlCommand
            cmd.Connection = objConn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "[usp_UpdateDocument]"
            cmd.Parameters.AddWithValue("ID", ID)
            cmd.Parameters.AddWithValue("action", "")
            cmd.Parameters.AddWithValue("UploadedBy", UploadedBy)
            cmd.Parameters.AddWithValue("which", which)
            cmd.Parameters.AddWithValue("Filename", Filename)
            cmd.Parameters.AddWithValue("Filesize", Filesize)
            cmd.Parameters.AddWithValue("FileData", FileData)
            cmd.Parameters.AddWithValue("ContentType", ContentType)
            cmd.Parameters.Add("RC", SqlDbType.Int).Direction = ParameterDirection.Output
            cmd.ExecuteNonQuery()
            cnt = cmd.Parameters("RC").Value
        End Using
        Return cnt > 0
    End Function



    Public Function DocumentUploaded(whichdocument As String, id As String, args As SelectFromDatabaseArgs) As DataRow
        Dim result As DataRow = Nothing

        Dim dset As New DataSet
        Dim da As SqlDataAdapter
        Using objConn As New SqlConnection(strConn)
            objConn.Open()

            Dim sql As String = Nothing

            If whichdocument.Equals("STLF") Then
                sql = "select a.STLFUploaded Uploaded " &
                    " ,a.STLFUserFilePath Filename " &
                    "from	uv_Customize a " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("TTF") Then
                sql = "select a.TTFUploaded Uploaded " &
                    " ,a.TTFUserFilePath Filename " &
                    "from	uv_Customize a " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("STF") Then
                sql = "select a.STFUploaded Uploaded " &
                    " ,a.STFUserFilePath Filename " &
                    "from	uv_Customize a " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("STF2") Then
                sql = "select a.STF2Uploaded Uploaded " &
                    " ,a.STF2UserFilePath Filename " &
                    "from	uv_Customize a " &
                    "where a.ID = @ID "
            ElseIf whichdocument.Equals("STLF2") Then
                sql = "select a.STLF2Uploaded Uploaded " &
                    " ,a.STLF2UserFilePath Filename " &
                    "from	uv_Customize a " &
                    "where a.ID = @ID "
            End If

            'sql = HandleJoinToCustomizeViewToFilterByRole(args, sql)

            Dim cnt As Integer = 1
            For Each f As FilterParameter In args.FilterParameters
                sql = sql & IIf(cnt = 0, " where ", " and ") & f.FilterExpression.Replace(" [", " a.[")
                cnt = cnt + 1
            Next

            cnt = 0


            da = New SqlDataAdapter(sql, objConn)


            If whichdocument.Equals("STLF") Or whichdocument.Equals("TTF") Or whichdocument.Equals("STF") Or whichdocument.Equals("STF2") Or whichdocument.Equals("STLF2") Then
                da.SelectCommand.Parameters.AddWithValue("@ID", id)
            End If

            For Each f As FilterParameter In args.FilterParameters
                da.SelectCommand.Parameters.AddWithValue("@" & f.FilterColumn & f.Index, f.FilterValue)
            Next

            da.Fill(dset)
            If dset.Tables(0).Rows.Count > 0 Then
                result = dset.Tables(0).Rows(0)

            Else

            End If
        End Using
        Return result
    End Function
End Class

