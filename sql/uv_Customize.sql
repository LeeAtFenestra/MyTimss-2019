USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[uv_Customize]    Script Date: 07/29/2014 09:32:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER VIEW [dbo].[uv_Customize]
AS
SELECT	dbo.uv_DistrictInfoNoFRSNoState.LEAID, dbo.uv_DistrictInfoNoFRSNoState.D_Name, dbo.uv_DistrictInfoNoFRSNoState.D_Addr1, 
		dbo.uv_DistrictInfoNoFRSNoState.D_Addr2, dbo.uv_DistrictInfoNoFRSNoState.D_City, dbo.uv_DistrictInfoNoFRSNoState.D_State, 
		dbo.uv_DistrictInfoNoFRSNoState.D_Zip, dbo.uv_DistrictInfoNoFRSNoState.D_Phone, dbo.uv_DistrictInfoNoFRSNoState.D_Fax, 
		dbo.uv_DistrictInfoNoFRSNoState.flgTuda, dbo.uv_DistrictInfoNoFRSNoState.smalldst, dbo.uv_DistrictInfoNoFRSNoState.D_Super, 
		dbo.uv_DistrictInfoNoFRSNoState.D_SuperPhone, dbo.uv_DistrictInfoNoFRSNoState.D_SuperEmail, dbo.uv_DistrictInfoNoFRSNoState.D_SuperFax, 
		dbo.uv_DistrictInfoNoFRSNoState.td_Name, dbo.uv_DistrictInfoNoFRSNoState.D_TdPhone, dbo.uv_DistrictInfoNoFRSNoState.D_TdEmail, 
		dbo.uv_DistrictInfoNoFRSNoState.D_TdFax, dbo.uv_DistrictInfoNoFRSNoState.D_Contact, dbo.uv_DistrictInfoNoFRSNoState.D_ContPhone, 
		dbo.uv_DistrictInfoNoFRSNoState.D_ContEmail, dbo.uv_DistrictInfoNoFRSNoState.D_ContFax, dbo.uv_DistrictInfoNoFRSNoState.D_ContTitle, 
		dbo.uv_DistrictInfoNoFRSNoState.D_Acoord, dbo.uv_DistrictInfoNoFRSNoState.D_AcoordPhone, dbo.uv_DistrictInfoNoFRSNoState.D_AcoordEmail, 
		dbo.uv_DistrictInfoNoFRSNoState.D_AcoordFax, dbo.uv_DistrictInfoNoFRSNoState.D_AcoordTitle, dbo.uv_DistrictInfoNoFRSNoState.D_PartLtrSentDT, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.S_Name, dbo.uv_SchoolInfoNoDistNoFRSNoState.S_Addr1, dbo.uv_SchoolInfoNoDistNoFRSNoState.S_Addr2, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.S_City, dbo.uv_SchoolInfoNoDistNoFRSNoState.S_State, dbo.uv_SchoolInfoNoDistNoFRSNoState.S_Zip, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.S_Phone, dbo.uv_SchoolInfoNoDistNoFRSNoState.S_Fax, dbo.uv_SchoolInfoNoDistNoFRSNoState.S_County, 
		dbo.uv_NewGradeInfoWithFRS.fldStateCode + '-' + dbo.Trim(STR(dbo.uv_NewGradeInfoWithFRS.fldRegionCode)) AS testregion, 
		dbo.uv_NewGradeInfoWithFRS.fldAreaCode, dbo.uv_SchoolInfoNoDistNoFRSNoState.TypeName, 
		CASE WHEN dbo.uv_NewGradeInfoWithFRS.DELTFlag = 0 THEN 'Public' WHEN dbo.uv_NewGradeInfoWithFRS.DELTFlag = 1 THEN 'Non-Public' END AS DELTFlag, 
		CASE WHEN dbo.uv_NewGradeInfoWithFRS.PRIVFlag = 0 THEN 'Public' WHEN dbo.uv_NewGradeInfoWithFRS.PRIVFlag = 1 THEN 'Non-Public' END AS PRIVFlag, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.sp_name, dbo.uv_SchoolInfoNoDistNoFRSNoState.sp_phone, dbo.uv_SchoolInfoNoDistNoFRSNoState.sp_email, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.sp_fax, dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_name, dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_phone, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_email, dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_fax, dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_title, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.id4, dbo.uv_SchoolInfoNoDistNoFRSNoState.id8, dbo.uv_SchoolInfoNoDistNoFRSNoState.id12, 
		dbo.uv_NewGradeInfoWithFRS.ID, dbo.uv_NewGradeInfoWithFRS.SMPGRD, 
		CASE WHEN dbo.uv_NewGradeInfoWithFRS.SMPGRD = 9 THEN 9 WHEN dbo.uv_NewGradeInfoWithFRS.SMPGRD = 13 THEN 13 WHEN dbo.uv_NewGradeInfoWithFRS.SMPGRD
		= 17 THEN 17 END AS smpage, dbo.uv_NewGradeInfoWithFRS.ESS_TA, dbo.uv_NewGradeInfoWithFRS.DispName, dbo.uv_NewGradeInfoWithFRS.PreAssessDate, 
		dbo.uv_NewGradeInfoWithFRS.SCHEDATE, dbo.uv_NewGradeInfoWithFRS.ORIGSUB, dbo.uv_NewGradeInfoWithFRS.ESTGRE, dbo.uv_NewGradeInfoWithFRS.Enroll, 
		dbo.uv_NewGradeInfoWithFRS.MethodName, dbo.uv_NewGradeInfoWithFRS.SLF_DateReceived, dbo.uv_NewGradeInfoWithFRS.HAS_SUB, 
		dbo.uv_NewGradeInfoWithFRS.Pct_On_Break, dbo.uv_NewGradeInfoWithFRS.STUD_CNT, dbo.uv_SchoolInfoNoDistNoFRSNoState.SDCFFlag, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.DateSDCFReceived, dbo.uv_SchoolInfoNoDistNoFRSNoState.DateSCReviewCompleted, 
		dbo.uv_NewGradeInfoWithFRS.PL_SchoolUsingNCESPL, dbo.uv_NewGradeInfoWithFRS.PL_StateProvidedPLToSchool, 
		dbo.uv_NewGradeInfoWithFRS.PL_DateStatePLSentToSchool, dbo.uv_NewGradeInfoWithFRS.PL_DateSCRecdPLFromSchool, 
		dbo.uv_NewGradeInfoWithFRS.PL_scdate_discuss_pnot, dbo.uv_NewGradeInfoWithFRS.AssessmentCompleteDate, dbo.uv_NewGradeInfoWithFRS.fldProjectID, 
		dbo.uv_NewGradeInfoWithFRS.fldPhasecode, dbo.uv_NewGradeInfoWithFRS.sample, dbo.uv_SchoolInfoNoDistNoFRSNoState.Age9, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.Age13, dbo.uv_SchoolInfoNoDistNoFRSNoState.Age17, dbo.uv_NewGradeInfoWithFRS.fldProjectDesc, 
		dbo.uv_NewGradeInfoWithFRS.REPSBGRP, dbo.uv_SchoolInfoNoDistNoFRSNoState.isPublic, 
		CASE WHEN dbo.uv_NewGradeInfoWithFRS.CharterFlag = 9 THEN 'Not answered' WHEN dbo.uv_NewGradeInfoWithFRS.CharterFlag = 0 THEN 'No' WHEN dbo.uv_NewGradeInfoWithFRS.CharterFlag
		= 1 THEN 'Yes' WHEN dbo.uv_NewGradeInfoWithFRS.CharterFlag = 2 THEN 'Yes: CCD' WHEN dbo.uv_NewGradeInfoWithFRS.CharterFlag = 3 THEN 'Yes: School Questionnaire'
		END AS CharterFlag, dbo.uv_NewGradeInfoWithFRS.SCSSSZ, dbo.uv_NewGradeInfoWithFRS.TRGSSZ, dbo.uv_NewGradeInfoWithFRS.fldSuperTerritoryCode, 
		dbo.uv_NewGradeInfoWithFRS.fldTerritoryCode, dbo.uv_NewGradeInfoWithFRS.fldStateCode, dbo.uv_NewGradeInfoWithFRS.fldRegionCode, 
		dbo.uv_NewGradeInfoWithFRS.AreaID, dbo.EX_FRS_Project.fldProjectDesc AS Expr1, 
		CASE WHEN dbo.tblSDSGrade.ListType = 'S' THEN 'SDS' WHEN dbo.tblSDSGrade.ListType = 'E' THEN 'E-Sample' ELSE NULL END AS ListType, 
		ISNULL(dbo.tblSDSGrade.CNT, 0) + ISNULL(dbo.tblSDSGrade.BCNT, 0) AS CNT, dbo.tblSDSGrade.SampleNumber, dbo.uv_SchoolInfoNoDistNoFRSNoState.Status, 
		--MS_District.fname AS D_MSfname, MS_District.lname AS D_MSlname, MS_District.LastLogin AS D_MSlastlogin, MS_District.Email AS D_MSemail, 
		--MS_School.fname AS S_MSfname, MS_School.lname AS S_MSLname, MS_School.LastLogin AS S_MSlastlogin, MS_School.Email AS S_MSemail, 
		dbo.uv_SchoolInfoNoDistNoFRSNoState.fname + ' ' + dbo.uv_SchoolInfoNoDistNoFRSNoState.lname AS PersonCompletedPSI, 
		dbo.uv_NewGradeInfoWithFRS.num_tracks, 
		CASE WHEN dbo.uv_NewGradeInfoWithFRS.Calendar = 1 THEN 'Traditional Non-Year-Round' WHEN dbo.uv_NewGradeInfoWithFRS.Calendar = 2 THEN 'Year-Round, Multi-Track'
		WHEN dbo.uv_NewGradeInfoWithFRS.Calendar = 3 THEN 'Year-Round, Single-Track' WHEN dbo.uv_NewGradeInfoWithFRS.Calendar = 4 THEN 'Other' WHEN dbo.uv_NewGradeInfoWithFRS.Calendar
		= 9 THEN 'Not Answered' ELSE 'Unknown' END AS CalendarFormat, dbo.uv_NewGradeInfoWithFRS.SLF_Name, dbo.tblSDSGrade.NSLF, 
		dbo.uv_NewGradeInfoWithFRS.sampdate, CASE WHEN dbo.tblSDSGrade.INT = 1 THEN 'Yes' WHEN dbo.tblSDSGrade.INT IS NULL 
		THEN 'Unsampled' ELSE 'No' END AS takeallformat, dbo.uv_NewGradeInfoWithFRS.ALPHFLAG, dbo.uv_NewGradeInfoWithFRS.BETAFLAG, 
		dbo.uv_NewGradeInfoWithFRS.GAMMFLAG, dbo.uv_NewGradeInfoWithFRS.NewSchoolResultFormat, dbo.uv_NewGradeInfoWithFRS.NewSchoolTriggerFormat, 
		dbo.uv_NewGradeInfoWithFRS.CharterFlagFormat, dbo.uv_NewGradeInfoWithFRS.Sch_PartLtrSentDT, dbo.uv_NewGradeInfoWithFRS.SchAsmtLtrSentDT, 
		dbo.uv_NewGradeInfoWithFRS.AugSchLtrSentDT, dbo.uv_NewGradeInfoWithFRS.NEWSCHL, dbo.uv_NewGradeInfoWithFRS.PAV_PEARSON_ELLTotal, 
		dbo.uv_NewGradeInfoWithFRS.PAV_PEARSON_SDTotal, dbo.uv_NewGradeInfoWithFRS.PAV_PEARSON_SDELLTotal, 
		dbo.uv_NewGradeInfoWithFRS.InSessionFirstDT, A.MUDate, dbo.uv_NewGradeInfoWithFRS.ICTFLAG, dbo.uv_NewGradeInfoWithFRS.PPFLAG, dbo.uv_NewGradeInfoWithFRS.TELFLAG,dbo.uv_NewGradeInfoWithFRS.LastDayofSchool,
		dbo.uv_ePAVProgress.NotifyParentsDate, dbo.uv_ePAVProgress.CurrentListofStudentsSubmittedDate, dbo.uv_ePAVProgress.UpdateStudentListDate, dbo.uv_ePAVProgress.IncludeStudentsDate,
		dbo.uv_NewGradeInfoWithFRS.CharterAYPTUDAConfirm, dbo.uv_NewGradeInfoWithFRS.CharterAYPSCConfirm, dbo.uv_NewGradeInfoWithFRS.TAUFLAG, dbo.uv_NewGradeInfoWithFRS.EPSIFLAG,
		dbo.uv_NewGradeInfoWithFRS.DELTFlag AS DELTFlag2     
		                                
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperPrefix
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperFname
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperLname
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperAddr1
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperAddr2
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperCity
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperState
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperZip
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperSuffix
		
		
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdPrefix
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdFname
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdLname
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdSuffix
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdAddr1
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdAddr2
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdCity
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdState
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdZip
		
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.pprefix
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.pfname
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.plname
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sp_suffix
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_prefix
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_fname
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_lname
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_suffix
		
		
		,dbo.uv_DistrictInfoNoFRSNoState.D_SuperPhoneext
		,dbo.uv_DistrictInfoNoFRSNoState.D_TdPhoneext
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sp_Phoneext
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.sc_Phoneext
		
		,dbo.uv_NewGradeInfoWithFRS.DISP
		
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.SEASCH
		
		,dbo.uv_NewGradeInfoWithFRS.ScheTime
		
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.MyNAEPREGID
		,dbo.uv_NewGradeInfoWithFRS.EnrollmentAtGrade  
		,dbo.uv_NewGradeInfoWithFRS.NumberOfMathClasses  
		,dbo.uv_NewGradeInfoWithFRS.NumberOfClasses 
		,dbo.uv_NewGradeInfoWithFRS.DateSchoolReturnsFromSpringBreak 
		,dbo.uv_NewGradeInfoWithFRS.DateSchoolReturnsFromWinterBreak 
		,dbo.uv_NewGradeInfoWithFRS.TUA_LEA 
		,dbo.uv_NewGradeInfoWithFRS.ClassListSubmited
		,dbo.uv_NewGradeInfoWithFRS.ClassListSubmitedBy
		,dbo.uv_NewGradeInfoWithFRS.ClassListSubmitedByFirstName
		,dbo.uv_NewGradeInfoWithFRS.ClassListSubmitedByLastName
		
		,dbo.uv_NewGradeInfoWithFRS.AdvancedEligibility 
		,dbo.uv_NewGradeInfoWithFRS.AdvancedEligibilityComments 
		,dbo.uv_NewGradeInfoWithFRS.AdvancedMathComments 
		,dbo.uv_NewGradeInfoWithFRS.AdvancedPhysicsComments   
		
		,dbo.uv_NewGradeInfoWithFRS.PrincipalIncentiveCheckBatchID 
		,dbo.uv_NewGradeInfoWithFRS.SCIncentiveCheckBatchID       
		,dbo.uv_NewGradeInfoWithFRS.SchoolIncentiveCheckSentDT
		,dbo.uv_NewGradeInfoWithFRS.SCIncentiveCheckSentDT
		,dbo.uv_NewGradeInfoWithFRS.UPSNumber1
		,dbo.uv_NewGradeInfoWithFRS.UPSNumber2
		,dbo.uv_NewGradeInfoWithFRS.FedExNumber1
		,dbo.uv_NewGradeInfoWithFRS.FedExNumber2
		,dbo.uv_NewGradeInfoWithFRS.SchoolIncentiveCheckSentTxt
		,dbo.uv_NewGradeInfoWithFRS.SCIncentiveCheckSentTxt
		,dbo.uv_NewGradeInfoWithFRS.AssessmentLocation
		,dbo.uv_NewGradeInfoWithFRS.AssessmentDayLogisticsInformation
		,dbo.uv_NewGradeInfoWithFRS.ParentConsentType
		,dbo.uv_NewGradeInfoWithFRS.ParentConsentLanguage
		,dbo.uv_NewGradeInfoWithFRS.PreAssessmentCallCompleted
		,dbo.uv_NewGradeInfoWithFRS.AssessmentCompleted
		,dbo.uv_NewGradeInfoWithFRS.DateOfMakeUp
		,dbo.uv_NewGradeInfoWithFRS.AssessmentMaterialsMailedToPearson
		
		,dbo.uv_NewGradeInfoWithFRS.Frame_N_
	, dbo.uv_SchoolInfoNoDistNoFRSNoState.PrincipalID
	, dbo.uv_SchoolInfoNoDistNoFRSNoState.CoordinatorID
	, dbo.uv_SchoolInfoNoDistNoFRSNoState.PSISubmittedBy
		,dbo.uv_NewGradeInfoWithFRS.isICILS
		,dbo.uv_NewGradeInfoWithFRS.isETIMSS
		,dbo.uv_NewGradeInfoWithFRS.SCHEDATE2
		,dbo.uv_NewGradeInfoWithFRS.SCHETime2
		,dbo.uv_NewGradeInfoWithFRS.ArrivalTime2
		,dbo.uv_NewGradeInfoWithFRS.DateSchoolStartsSpringBreak 
		,case when dbo.uv_NewGradeInfoWithFRS.STLFUploaded = 0 then 'No' else 'Yes' end STLFUploadedText
		,dbo.uv_NewGradeInfoWithFRS.STLFUserFilePath
		,dbo.uv_NewGradeInfoWithFRS.STLFFilesize
		,dbo.uv_NewGradeInfoWithFRS.STLFContentType
		,dbo.uv_NewGradeInfoWithFRS.STLFUploaded	
		,dbo.uv_SchoolInfoNoDistNoFRSNoState.ICTCoordinatorName			
		
		,dbo.uv_NewGradeInfoWithFRS.TTFUserFilePath
		,dbo.uv_NewGradeInfoWithFRS.TTFFilesize
		,dbo.uv_NewGradeInfoWithFRS.TTFContentType
		,dbo.uv_NewGradeInfoWithFRS.TTFUploaded	
		
  FROM	dbo.uv_NewGradeInfoWithFRS LEFT OUTER JOIN
        dbo.EX_FRS_Project ON dbo.uv_NewGradeInfoWithFRS.fldProjectID = dbo.EX_FRS_Project.fldProjectID LEFT OUTER JOIN
        dbo.uv_SchoolInfoNoDistNoFRSNoState ON dbo.uv_NewGradeInfoWithFRS.fldProjectID_FA = dbo.uv_SchoolInfoNoDistNoFRSNoState.fldProjectID AND 
        dbo.uv_NewGradeInfoWithFRS.Frame_N_ = dbo.uv_SchoolInfoNoDistNoFRSNoState.Frame_N_ LEFT OUTER JOIN
        dbo.uv_DistrictInfoNoFRSNoState ON dbo.uv_DistrictInfoNoFRSNoState.LEAID = dbo.uv_SchoolInfoNoDistNoFRSNoState.LEAID 
        
        /*LEFT OUTER JOIN
        dbo.ExAdmin_MSUserRegistration_District AS MS_District ON dbo.uv_DistrictInfoNoFRSNoState.LEAID = MS_District.LEAID*/ 
        
        /*
        LEFT OUTER JOIN
        dbo.ExAdmin_MSUserRegistration_School AS MS_School ON dbo.uv_SchoolInfoNoDistNoFRSNoState.Frame_N_ = MS_School.Frame_n_
        */
        
         LEFT OUTER JOIN
        dbo.tblSDSGrade ON dbo.uv_NewGradeInfoWithFRS.fldProjectID = dbo.tblSDSGrade.fldProjectID AND dbo.uv_NewGradeInfoWithFRS.ID = dbo.tblSDSGrade.ID LEFT OUTER JOIN
        dbo.uv_ePAVProgress ON dbo.uv_NewGradeInfoWithFRS.fldProjectID = dbo.uv_ePAVProgress.fldProjectID AND dbo.uv_NewGradeInfoWithFRS.ID = dbo.uv_ePAVProgress.ID LEFT OUTER JOIN
        (SELECT	fldProjectID, ID, MIN(MakeUp_Date) AS MUDate
		   FROM dbo.tblSCSGradeSubject
		  WHERE	(MakeUp_Date IS NOT NULL)
		  GROUP BY fldProjectID, ID) A ON A.fldProjectID = dbo.uv_NewGradeInfoWithFRS.fldProjectID AND A.ID = dbo.uv_NewGradeInfoWithFRS.ID

Where	dbo.uv_NewGradeInfoWithFRS.fldProjectID=780 
and		dbo.uv_NewGradeInfoWithFRS.fldPhasecode='FA'
and		dbo.uv_SchoolInfoNoDistNoFRSNoState.sysActive = 1



GO


