USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[uv_tblSCSGrade]    Script Date: 10/17/2014 07:56:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[uv_tblSCSGrade]
AS
SELECT	dbo.tblSCSGrade.fldProjectID, dbo.tblSCSGrade.fldProjectID AS fldProjectID_FA, dbo.tblSCSGrade.ID, dbo.tblSCSGrade.AreaID AS t_areaid, dbo.tblSCSGrade.AreaID, dbo.tblSCSGrade.AreaIDgc, 
		dbo.tblSCSGrade.Frame_N_, dbo.tblSCSGrade.DISP, dbo.tblSCSGrade.PreAssessDate, dbo.tblSCSGrade.SCHEDATE, dbo.tblSCSGrade.Enroll, 
		dbo.tblSCSGrade.SampMeth, dbo.tblSCSGrade.Pct_On_Break, dbo.tblSCSGrade.CharterFlag, dbo.tblSCSGrade.Pct_On_Break_Ability, 
		dbo.tblSCSGrade.Pct_On_Break_Other, /* dbo.tblSCSGrade.HSTSFlag, */ dbo.tblSCSGrade.AssessmentCompleteFlag, 
		dbo.tblSCSGrade.EfilingSummaryFormFlag, dbo.tblSCSGrade.sampdate, dbo.tblSCSGrade.AssessmentCompleteDate, dbo.tblSCSGrade.nies_flag, 
		dbo.tblSCSGrade.PL_SchoolUsingNCESPL, dbo.tblSCSGrade.PL_StateProvidedPLToSchool, dbo.tblSCSGrade.PL_DateStatePLSentToSchool, 
		dbo.tblSCSGrade.PL_DateSCRecdPLFromSchool, dbo.tblSCSGrade.PL_scdate_discuss_pnot, dbo.tblSCSGrade.SLF_DateReceived, 
		dbo.tblSCSGrade.Phase, dbo.tblSCSGrade.SLF_CompDate, dbo.tblSCSGrade.SCHETime, dbo.tblSCSGrade.QCM25, dbo.tblSCSGrade.scdate_sent_info, 
		dbo.tblSCSGrade.NewSchoolTrigger, dbo.tblSCSGrade.NewSchoolResult, dbo.tblSCSGrade.Calendar, dbo.tblSCSGrade.num_tracks, 
		dbo.tblSCSGrade.eqcbpacketdeliverydate, dbo.tblSCSGrade.eqcbestimatedtimehours, dbo.tblSCSGrade.eqcbsuggestedpavdate, 
		dbo.tblSCSGrade.eqcbsuggestedpavtime, dbo.tblSCSGrade.eqcbschedulingcalldate, dbo.tblSCSGrade.eqcbpavpacketdate, 
		dbo.tblSCSGrade.eqcbupdatedt, dbo.tblSCSGrade.eqcblasttransmitdt, 
		dbo.tblSCSGrade.EQCBPAVPacketSignedForBy, dbo.tblSCSGrade.NE_SampledfromListNE, dbo.tblSCSGrade.SpecSitAssessAll, 'FA' AS fldPhasecode, SLF_Name, 
		CASE dbo.tblSCSGrade.NewSchoolResult WHEN 9 THEN 'Not Answered' WHEN 1 THEN 'Yes, New Schools to Add' WHEN 2 THEN 'Yes, No New Schools'
		WHEN 0 THEN 'No' END AS NewSchoolResultFormat, 
		CASE dbo.tblSCSGrade.NewSchoolTrigger WHEN '1' THEN 'New School Required' WHEN '0' THEN 'New School Not Required' ELSE 'Unknown' END AS NewSchoolTriggerFormat,
		CASE dbo.tblSCSGrade.Charterflag WHEN 9 THEN 'Not Answered' WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' WHEN 2 THEN 'Yes: CCD' WHEN 3 THEN 'Yes: School QuestiONnaire'
		END AS CharterFlagFormat, dbo.tblSCSGrade.Sch_PartLtrSentDT, dbo.tblSCSGrade.SchAsmtLtrSentDT, dbo.tblSCSGrade.AugSchLtrSentDT, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_W, 0) AS PAV_PEARSON_ELLTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_W, 0) AS PAV_PEARSON_SDTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_W, 0) 
		AS PAV_PEARSON_SDELLTotal, dbo.tblSCSGrade.EQCBWorkLengthEstimate,dbo.tblSCSGrade.PreAssessTime,
		dbo.tblSCSGrade.NIES_ScheDate, dbo.tblSCSGrade.NIES_ScheTime, dbo.tblSCSGrade.NIES_Disp, dbo.tblSCSGrade.schedate2,
		dbo.tblSCSGrade.IN_IneligStatusCode, dbo.tblSCSGrade.GR_Date, dbo.tblSCSGrade.SLF_DateCurrent,dbo.tblSCSGrade.pav_pearson_r,dbo.tblSCSGrade.pav_pearson_m,dbo.tblSCSGrade.pav_pearson_s,dbo.tblSCSGrade.pav_pearson_w,dbo.tblSCSGrade.pav_pearson_soc,
		dbo.tblSCSGrade.pav_pearson_sd_r,dbo.tblSCSGrade.pav_pearson_sd_m,dbo.tblSCSGrade.pav_pearson_sd_s,dbo.tblSCSGrade.pav_pearson_sd_w,dbo.tblSCSGrade.pav_pearson_sd_soc,
		dbo.tblSCSGrade.pav_pearson_sd504_r,dbo.tblSCSGrade.pav_pearson_sd504_m,dbo.tblSCSGrade.pav_pearson_sd504_s,dbo.tblSCSGrade.pav_pearson_sd504_w,dbo.tblSCSGrade.pav_pearson_sd504_soc,
		dbo.tblSCSGrade.pav_pearson_ell_r,dbo.tblSCSGrade.pav_pearson_ell_m,dbo.tblSCSGrade.pav_pearson_ell_s,dbo.tblSCSGrade.pav_pearson_ell_w,dbo.tblSCSGrade.pav_pearson_ell_soc,
		dbo.tblSCSGrade.pav_pearson_sdell_r,dbo.tblSCSGrade.pav_pearson_sdell_m,dbo.tblSCSGrade.pav_pearson_sdell_s,dbo.tblSCSGrade.pav_pearson_sdell_w,dbo.tblSCSGrade.pav_pearson_sdell_soc,
		dbo.tblSCSGrade.pav_excl_sd_r,dbo.tblSCSGrade.pav_excl_sd_m,dbo.tblSCSGrade.pav_excl_sd_s,dbo.tblSCSGrade.pav_excl_sd_w,dbo.tblSCSGrade.pav_excl_sd_soc,  
		dbo.tblSCSGrade.pav_excl_sd504_r,dbo.tblSCSGrade.pav_excl_sd504_m,dbo.tblSCSGrade.pav_excl_sd504_s,dbo.tblSCSGrade.pav_excl_sd504_w,dbo.tblSCSGrade.pav_excl_sd504_soc,
		dbo.tblSCSGrade.pav_excl_ell_r,dbo.tblSCSGrade.pav_excl_ell_m ,dbo.tblSCSGrade.pav_excl_ell_s,dbo.tblSCSGrade.pav_excl_ell_w ,dbo.tblSCSGrade.pav_excl_ell_soc,
		dbo.tblSCSGrade.pav_excl_sdell_r,dbo.tblSCSGrade.pav_excl_sdell_m,dbo.tblSCSGrade.pav_excl_sdell_s,dbo.tblSCSGrade.pav_excl_sdell_w,dbo.tblSCSGrade.pav_excl_sdell_soc, CONVERT(VARCHAR(10), InSessionFirstDT, 101) as InSessionFirstDT, tblScsGrade.MUDATE, tblSCSGrade.SchoolGoesonBreakDT,
		AIF_SchoolDirection, AIF_SchoolParking, AIF_WhereToMeet, AIF_HowHandleLateComers, AIF_HowContactSchool, 
		AIF_DismissStuPolicy, AIF_Others, AIF_NIESDismissStuPolicy, AIF_SchoolDirection2, AIF_SchoolParking2, AIF_WhereToMeet2, AIF_HowHandleLateComers2, 
		AIF_HowContactSchool2, AIF_DismissStuPolicy2, AIF_Others2, AIF_NIESDismissStuPolicy2, AIF_Protocols, AIF_WorkPlace, AIF_ScheduleEvents, AIF_MeetAfter, LastDayofSchool, CONVERT(VARCHAR(100),schedate,107) AS schedate_107, tblSCSGrade.PreAssessReviewCallDate, tblSCSGrade.PreAssessReviewCallTime, tblSCSGrade.SCR_UploadType,
        CharterAYPTUDAConfirm,CharterAYPSCConfirm,
        dbo.tblSCSGrade.NSLP_Participation    
		,dbo.tblSCSGrade.EnrollmentAtGrade  
		,dbo.tblSCSGrade.NumberOfMathClasses  
		,dbo.tblSCSGrade.NumberOfClasses     
		,dbo.tblSCSGrade.DateSchoolReturnsFromSpringBreak 
		,dbo.tblSCSGrade.DateSchoolReturnsFromWinterBreak      
		,dbo.tblSCSGrade.ClassListSubmited    
		,dbo.tblSCSGrade.ClassListSubmitedBy                 
		,dbo.tblSCSGrade.AdvancedEligibility 
		,dbo.tblSCSGrade.AdvancedEligibilityComments 
		,dbo.tblSCSGrade.AdvancedMathComments 
		,dbo.tblSCSGrade.AdvancedPhysicsComments              
		,dbo.tblSCSGrade.PrincipalIncentiveCheckBatchID 
		,dbo.tblSCSGrade.SCIncentiveCheckBatchID        
		,dbo.tblSCSGrade.UPSNumber1
		,dbo.tblSCSGrade.UPSNumber2
		,dbo.tblSCSGrade.FedExNumber1
		,dbo.tblSCSGrade.FedExNumber2
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentTxt
		,dbo.tblSCSGrade.SCIncentiveCheckSentTxt
		,dbo.tblSCSGrade.AssessmentLocation
		,dbo.tblSCSGrade.AssessmentDayLogisticsInformation
		,dbo.tblSCSGrade.ParentConsentType
		,dbo.tblSCSGrade.ParentConsentLanguage
		,dbo.tblSCSGrade.PreAssessmentCallCompleted
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentDT
		,dbo.tblSCSGrade.SCIncentiveCheckSentDT
		,dbo.tblSCSGrade.AssessmentCompleted
		,dbo.tblSCSGrade.DateOfMakeUp
		,dbo.tblSCSGrade.AssessmentMaterialsMailedToPearson
		,gf.[Filename] STLFUserFilePath
		,gf.Filesize STLFFilesize
		,gf.ContentType STLFContentType
		,cast(case when dbo.tblSCSGrade.STLFGradeFileId is null then 0 else 1 end as bit) STLFUploaded	
		,dbo.tblSCSGrade.SCHETime2
		,dbo.tblSCSGrade.ArrivalTime2		
		,dbo.tblSCSGrade.DateSchoolStartsSpringBreak 
		
		,ttf.[Filename] TTFUserFilePath
		,ttf.Filesize TTFFilesize
		,ttf.ContentType TTFContentType
		,cast(case when dbo.tblSCSGrade.TTFGradeFileId is null then 0 else 1 end as bit) TTFUploaded	
  FROM	dbo.tblSCSGrade
  left outer join tblGradeFiles gf
  on	gf.GradeFileId =  dbo.tblSCSGrade.STLFGradeFileId
  left outer join tblGradeFiles ttf
  on	ttf.GradeFileId =  dbo.tblSCSGrade.TTFGradeFileId
 WHERE  dbo.tblSCSGrade.areaid IS NOT NULL
UNION ALL
SELECT	dbo.tblSCSGrade.fldProjectID, dbo.tblSCSGrade.fldProjectID AS fldProjectID_FA, dbo.tblSCSGrade.ID, dbo.tblSCSGrade.areaidgc AS t_areaid, dbo.tblSCSGrade.AreaID, dbo.tblSCSGrade.AreaIDgc, 
		dbo.tblSCSGrade.Frame_N_, dbo.tblSCSGrade.DISP, dbo.tblSCSGrade.PreAssessDate, dbo.tblSCSGrade.SCHEDATE, dbo.tblSCSGrade.Enroll, 
		dbo.tblSCSGrade.SampMeth, dbo.tblSCSGrade.Pct_On_Break, dbo.tblSCSGrade.CharterFlag, dbo.tblSCSGrade.Pct_On_Break_Ability, 
		dbo.tblSCSGrade.Pct_On_Break_Other, /* dbo.tblSCSGrade.HSTSFlag, */ dbo.tblSCSGrade.AssessmentCompleteFlag, 
		dbo.tblSCSGrade.EfilingSummaryFormFlag, dbo.tblSCSGrade.sampdate, dbo.tblSCSGrade.AssessmentCompleteDate, dbo.tblSCSGrade.nies_flag, 
		dbo.tblSCSGrade.PL_SchoolUsingNCESPL, dbo.tblSCSGrade.PL_StateProvidedPLToSchool, dbo.tblSCSGrade.PL_DateStatePLSentToSchool, 
		dbo.tblSCSGrade.PL_DateSCRecdPLFromSchool, dbo.tblSCSGrade.PL_scdate_discuss_pnot, dbo.tblSCSGrade.SLF_DateReceived, 
		dbo.tblSCSGrade.Phase, dbo.tblSCSGrade.SLF_CompDate, dbo.tblSCSGrade.SCHETime, dbo.tblSCSGrade.QCM25, dbo.tblSCSGrade.scdate_sent_info, 
		dbo.tblSCSGrade.NewSchoolTrigger, dbo.tblSCSGrade.NewSchoolResult, dbo.tblSCSGrade.Calendar, dbo.tblSCSGrade.num_tracks, 
		dbo.tblSCSGrade.eqcbpacketdeliverydate, dbo.tblSCSGrade.eqcbestimatedtimehours, dbo.tblSCSGrade.eqcbsuggestedpavdate, 
		dbo.tblSCSGrade.eqcbsuggestedpavtime, dbo.tblSCSGrade.eqcbschedulingcalldate, dbo.tblSCSGrade.eqcbpavpacketdate, 
		dbo.tblSCSGrade.eqcbupdatedt, dbo.tblSCSGrade.eqcblasttransmitdt, 
		dbo.tblSCSGrade.EQCBPAVPacketSignedForBy, dbo.tblSCSGrade.NE_SampledfromListNE, dbo.tblSCSGrade.SpecSitAssessAll, 'GC' AS fldphasecode, SLF_Name, 
		CASE dbo.tblSCSGrade.NewSchoolResult WHEN 9 THEN 'Not Answered' WHEN 1 THEN 'Yes, New Schools to Add' WHEN 2 THEN 'Yes, No New Schools'
		WHEN 0 THEN 'No' END AS NewSchoolResultFormat, 
		CASE dbo.tblSCSGrade.NewSchoolTrigger WHEN '1' THEN 'New School Required' WHEN '0' THEN 'New School Not Required' ELSE 'Unknown' END AS NewSchoolTriggerFormat,
		CASE dbo.tblSCSGrade.Charterflag WHEN 9 THEN 'Not Answered' WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' WHEN 2 THEN 'Yes: CCD' WHEN 3 THEN 'Yes: School QuestiONnaire'
		END AS CharterFlagFormat, dbo.tblSCSGrade.Sch_PartLtrSentDT, dbo.tblSCSGrade.SchAsmtLtrSentDT, dbo.tblSCSGrade.AugSchLtrSentDT, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_W, 0) AS PAV_PEARSON_ELLTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_W, 0) AS PAV_PEARSON_SDTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_W, 0) 
		AS PAV_PEARSON_SDELLTotal,dbo.tblSCSGrade.EQCBWorkLengthEstimate,dbo.tblSCSGrade.PreAssessTime,
		dbo.tblSCSGrade.NIES_ScheDate, dbo.tblSCSGrade.NIES_ScheTime, dbo.tblSCSGrade.NIES_Disp, dbo.tblSCSGrade.schedate2,
		dbo.tblSCSGrade.IN_IneligStatusCode, dbo.tblSCSGrade.GR_Date, dbo.tblSCSGrade.SLF_DateCurrent,dbo.tblSCSGrade.pav_pearson_r,dbo.tblSCSGrade.pav_pearson_m,dbo.tblSCSGrade.pav_pearson_s,dbo.tblSCSGrade.pav_pearson_w,dbo.tblSCSGrade.pav_pearson_soc,
		dbo.tblSCSGrade.pav_pearson_sd_r,dbo.tblSCSGrade.pav_pearson_sd_m,dbo.tblSCSGrade.pav_pearson_sd_s,dbo.tblSCSGrade.pav_pearson_sd_w,dbo.tblSCSGrade.pav_pearson_sd_soc,
		dbo.tblSCSGrade.pav_pearson_sd504_r,dbo.tblSCSGrade.pav_pearson_sd504_m,dbo.tblSCSGrade.pav_pearson_sd504_s,dbo.tblSCSGrade.pav_pearson_sd504_w,dbo.tblSCSGrade.pav_pearson_sd504_soc,
		dbo.tblSCSGrade.pav_pearson_ell_r,dbo.tblSCSGrade.pav_pearson_ell_m,dbo.tblSCSGrade.pav_pearson_ell_s,dbo.tblSCSGrade.pav_pearson_ell_w,dbo.tblSCSGrade.pav_pearson_ell_soc,
		dbo.tblSCSGrade.pav_pearson_sdell_r,dbo.tblSCSGrade.pav_pearson_sdell_m,dbo.tblSCSGrade.pav_pearson_sdell_s,dbo.tblSCSGrade.pav_pearson_sdell_w,dbo.tblSCSGrade.pav_pearson_sdell_soc,
		dbo.tblSCSGrade.pav_excl_sd_r,dbo.tblSCSGrade.pav_excl_sd_m,dbo.tblSCSGrade.pav_excl_sd_s,dbo.tblSCSGrade.pav_excl_sd_w,dbo.tblSCSGrade.pav_excl_sd_soc,  
		dbo.tblSCSGrade.pav_excl_sd504_r,dbo.tblSCSGrade.pav_excl_sd504_m,dbo.tblSCSGrade.pav_excl_sd504_s,dbo.tblSCSGrade.pav_excl_sd504_w,dbo.tblSCSGrade.pav_excl_sd504_soc,
		dbo.tblSCSGrade.pav_excl_ell_r,dbo.tblSCSGrade.pav_excl_ell_m ,dbo.tblSCSGrade.pav_excl_ell_s,dbo.tblSCSGrade.pav_excl_ell_w ,dbo.tblSCSGrade.pav_excl_ell_soc,
		dbo.tblSCSGrade.pav_excl_sdell_r,dbo.tblSCSGrade.pav_excl_sdell_m,dbo.tblSCSGrade.pav_excl_sdell_s,dbo.tblSCSGrade.pav_excl_sdell_w,dbo.tblSCSGrade.pav_excl_sdell_soc, CONVERT(VARCHAR(10), InSessionFirstDT, 101) as InSessionFirstDT, tblScsGrade.MUDate, tblSCSGrade.SchoolGoesonBreakDT,
		AIF_SchoolDirection, AIF_SchoolParking, AIF_WhereToMeet, AIF_HowHandleLateComers, AIF_HowContactSchool, 
		AIF_DismissStuPolicy, AIF_Others, AIF_NIESDismissStuPolicy, AIF_SchoolDirection2, AIF_SchoolParking2, AIF_WhereToMeet2, AIF_HowHandleLateComers2, 
		AIF_HowContactSchool2, AIF_DismissStuPolicy2, AIF_Others2, AIF_NIESDismissStuPolicy2, AIF_Protocols, AIF_WorkPlace, AIF_ScheduleEvents, AIF_MeetAfter, LastDayofSchool, CONVERT(VARCHAR(100),schedate,107) AS schedate_107, tblSCSGrade.PreAssessReviewCallDate, tblSCSGrade.PreAssessReviewCallTime, tblSCSGrade.SCR_UploadType,
		CharterAYPTUDAConfirm,CharterAYPSCConfirm, dbo.tblSCSGrade.NSLP_Participation    
		,dbo.tblSCSGrade.EnrollmentAtGrade  
		,dbo.tblSCSGrade.NumberOfMathClasses  
		,dbo.tblSCSGrade.NumberOfClasses      
		,dbo.tblSCSGrade.DateSchoolReturnsFromSpringBreak 
		,dbo.tblSCSGrade.DateSchoolReturnsFromWinterBreak       
		,dbo.tblSCSGrade.ClassListSubmited    
		,dbo.tblSCSGrade.ClassListSubmitedBy            
		,dbo.tblSCSGrade.AdvancedEligibility 
		,dbo.tblSCSGrade.AdvancedEligibilityComments 
		,dbo.tblSCSGrade.AdvancedMathComments 
		,dbo.tblSCSGrade.AdvancedPhysicsComments                
		,dbo.tblSCSGrade.PrincipalIncentiveCheckBatchID 
		,dbo.tblSCSGrade.SCIncentiveCheckBatchID                 
		,dbo.tblSCSGrade.UPSNumber1
		,dbo.tblSCSGrade.UPSNumber2
		,dbo.tblSCSGrade.FedExNumber1
		,dbo.tblSCSGrade.FedExNumber2
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentTxt
		,dbo.tblSCSGrade.SCIncentiveCheckSentTxt
		,dbo.tblSCSGrade.AssessmentLocation
		,dbo.tblSCSGrade.AssessmentDayLogisticsInformation
		,dbo.tblSCSGrade.ParentConsentType
		,dbo.tblSCSGrade.ParentConsentLanguage
		,dbo.tblSCSGrade.PreAssessmentCallCompleted
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentDT
		,dbo.tblSCSGrade.SCIncentiveCheckSentDT
		,dbo.tblSCSGrade.AssessmentCompleted
		,dbo.tblSCSGrade.DateOfMakeUp
		,dbo.tblSCSGrade.AssessmentMaterialsMailedToPearson
		,gf.[Filename] STLFUserFilePath
		,gf.Filesize STLFFilesize
		,gf.ContentType STLFContentType
		,cast(case when dbo.tblSCSGrade.STLFGradeFileId is null then 0 else 1 end as bit) STLFUploaded	
		,dbo.tblSCSGrade.SCHETime2
		,dbo.tblSCSGrade.ArrivalTime2
		,dbo.tblSCSGrade.DateSchoolStartsSpringBreak 
		
		,ttf.[Filename] TTFUserFilePath
		,ttf.Filesize TTFFilesize
		,ttf.ContentType TTFContentType
		,cast(case when dbo.tblSCSGrade.TTFGradeFileId is null then 0 else 1 end as bit) TTFUploaded		
  FROM  dbo.tblSCSGrade
  left outer join tblGradeFiles gf
  on	gf.GradeFileId =  dbo.tblSCSGrade.STLFGradeFileId
  left outer join tblGradeFiles ttf
  on	ttf.GradeFileId =  dbo.tblSCSGrade.TTFGradeFileId
 WHERE	dbo.tblSCSGrade.areaidgc IS NOT NULL
UNION ALL
SELECT  dbo.tblSCSGrade.fldProjectIDGC AS fldProjectID, dbo.tblSCSGrade.fldProjectID AS fldProjectID_FA, dbo.tblSCSGrade.ID, dbo.tblSCSGrade.areaidgc AS t_areaid, dbo.tblSCSGrade.AreaID, dbo.tblSCSGrade.AreaIDgc, 
		dbo.tblSCSGrade.Frame_N_, dbo.tblSCSGrade.DISP, dbo.tblSCSGrade.PreAssessDate, dbo.tblSCSGrade.SCHEDATE, dbo.tblSCSGrade.Enroll, 
		dbo.tblSCSGrade.SampMeth, dbo.tblSCSGrade.Pct_On_Break, dbo.tblSCSGrade.CharterFlag, dbo.tblSCSGrade.Pct_On_Break_Ability, 
		dbo.tblSCSGrade.Pct_On_Break_Other, /* dbo.tblSCSGrade.HSTSFlag, */ dbo.tblSCSGrade.AssessmentCompleteFlag, 
		dbo.tblSCSGrade.EfilingSummaryFormFlag, dbo.tblSCSGrade.sampdate, dbo.tblSCSGrade.AssessmentCompleteDate, dbo.tblSCSGrade.nies_flag, 
		dbo.tblSCSGrade.PL_SchoolUsingNCESPL, dbo.tblSCSGrade.PL_StateProvidedPLToSchool, dbo.tblSCSGrade.PL_DateStatePLSentToSchool, 
		dbo.tblSCSGrade.PL_DateSCRecdPLFromSchool, dbo.tblSCSGrade.PL_scdate_discuss_pnot, dbo.tblSCSGrade.SLF_DateReceived, 
		dbo.tblSCSGrade.Phase, dbo.tblSCSGrade.SLF_CompDate, dbo.tblSCSGrade.SCHETime, dbo.tblSCSGrade.QCM25, dbo.tblSCSGrade.scdate_sent_info, 
		dbo.tblSCSGrade.NewSchoolTrigger, dbo.tblSCSGrade.NewSchoolResult, dbo.tblSCSGrade.Calendar, dbo.tblSCSGrade.num_tracks, 
		dbo.tblSCSGrade.eqcbpacketdeliverydate, dbo.tblSCSGrade.eqcbestimatedtimehours, dbo.tblSCSGrade.eqcbsuggestedpavdate, 
		dbo.tblSCSGrade.eqcbsuggestedpavtime, dbo.tblSCSGrade.eqcbschedulingcalldate, dbo.tblSCSGrade.eqcbpavpacketdate, 
		dbo.tblSCSGrade.eqcbupdatedt, dbo.tblSCSGrade.eqcblasttransmitdt, 
		dbo.tblSCSGrade.EQCBPAVPacketSignedForBy, dbo.tblSCSGrade.NE_SampledfromListNE, dbo.tblSCSGrade.SpecSitAssessAll, 'GC' AS fldphasecode, SLF_Name, 
		CASE dbo.tblSCSGrade.NewSchoolResult WHEN 9 THEN 'Not Answered' WHEN 1 THEN 'Yes, New Schools to Add' WHEN 2 THEN 'Yes, No New Schools'
		WHEN 0 THEN 'No' END AS NewSchoolResultFormat, 
		CASE dbo.tblSCSGrade.NewSchoolTrigger WHEN '1' THEN 'New School Required' WHEN '0' THEN 'New School Not Required' ELSE 'Unknown' END AS NewSchoolTriggerFormat,
		CASE dbo.tblSCSGrade.Charterflag WHEN 9 THEN 'Not Answered' WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' WHEN 2 THEN 'Yes: CCD' WHEN 3 THEN 'Yes: School QuestiONnaire'
		END AS CharterFlagFormat, dbo.tblSCSGrade.Sch_PartLtrSentDT, dbo.tblSCSGrade.SchAsmtLtrSentDT, dbo.tblSCSGrade.AugSchLtrSentDT, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_W, 0) AS PAV_PEARSON_ELLTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_W, 0) AS PAV_PEARSON_SDTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_W, 0) 
		AS PAV_PEARSON_SDELLTotal,dbo.tblSCSGrade.EQCBWorkLengthEstimate,dbo.tblSCSGrade.PreAssessTime,
		dbo.tblSCSGrade.NIES_ScheDate, dbo.tblSCSGrade.NIES_ScheTime, dbo.tblSCSGrade.NIES_Disp, dbo.tblSCSGrade.schedate2,
		dbo.tblSCSGrade.IN_IneligStatusCode, dbo.tblSCSGrade.GR_Date, dbo.tblSCSGrade.SLF_DateCurrent,dbo.tblSCSGrade.pav_pearson_r,dbo.tblSCSGrade.pav_pearson_m,dbo.tblSCSGrade.pav_pearson_s,dbo.tblSCSGrade.pav_pearson_w,dbo.tblSCSGrade.pav_pearson_soc,
		dbo.tblSCSGrade.pav_pearson_sd_r,dbo.tblSCSGrade.pav_pearson_sd_m,dbo.tblSCSGrade.pav_pearson_sd_s,dbo.tblSCSGrade.pav_pearson_sd_w,dbo.tblSCSGrade.pav_pearson_sd_soc,
		dbo.tblSCSGrade.pav_pearson_sd504_r,dbo.tblSCSGrade.pav_pearson_sd504_m,dbo.tblSCSGrade.pav_pearson_sd504_s,dbo.tblSCSGrade.pav_pearson_sd504_w,dbo.tblSCSGrade.pav_pearson_sd504_soc,
		dbo.tblSCSGrade.pav_pearson_ell_r,dbo.tblSCSGrade.pav_pearson_ell_m,dbo.tblSCSGrade.pav_pearson_ell_s,dbo.tblSCSGrade.pav_pearson_ell_w,dbo.tblSCSGrade.pav_pearson_ell_soc,
		dbo.tblSCSGrade.pav_pearson_sdell_r,dbo.tblSCSGrade.pav_pearson_sdell_m,dbo.tblSCSGrade.pav_pearson_sdell_s,dbo.tblSCSGrade.pav_pearson_sdell_w,dbo.tblSCSGrade.pav_pearson_sdell_soc,
		dbo.tblSCSGrade.pav_excl_sd_r,dbo.tblSCSGrade.pav_excl_sd_m,dbo.tblSCSGrade.pav_excl_sd_s,dbo.tblSCSGrade.pav_excl_sd_w,dbo.tblSCSGrade.pav_excl_sd_soc,  
		dbo.tblSCSGrade.pav_excl_sd504_r,dbo.tblSCSGrade.pav_excl_sd504_m,dbo.tblSCSGrade.pav_excl_sd504_s,dbo.tblSCSGrade.pav_excl_sd504_w,dbo.tblSCSGrade.pav_excl_sd504_soc,
		dbo.tblSCSGrade.pav_excl_ell_r,dbo.tblSCSGrade.pav_excl_ell_m ,dbo.tblSCSGrade.pav_excl_ell_s,dbo.tblSCSGrade.pav_excl_ell_w ,dbo.tblSCSGrade.pav_excl_ell_soc,
		dbo.tblSCSGrade.pav_excl_sdell_r,dbo.tblSCSGrade.pav_excl_sdell_m,dbo.tblSCSGrade.pav_excl_sdell_s,dbo.tblSCSGrade.pav_excl_sdell_w,dbo.tblSCSGrade.pav_excl_sdell_soc, CONVERT(VARCHAR(10), InSessionFirstDT, 101) as InSessionFirstDT, tblScsGrade.MUDate, tblSCSGrade.SchoolGoesonBreakDT,
		AIF_SchoolDirection, AIF_SchoolParking, AIF_WhereToMeet, AIF_HowHandleLateComers, AIF_HowContactSchool, 
		AIF_DismissStuPolicy, AIF_Others, AIF_NIESDismissStuPolicy, AIF_SchoolDirection2, AIF_SchoolParking2, AIF_WhereToMeet2, AIF_HowHandleLateComers2, 
		AIF_HowContactSchool2, AIF_DismissStuPolicy2, AIF_Others2, AIF_NIESDismissStuPolicy2, AIF_Protocols, AIF_WorkPlace, AIF_ScheduleEvents, AIF_MeetAfter, LastDayofSchool, CONVERT(VARCHAR(100),schedate,107) AS schedate_107, tblSCSGrade.PreAssessReviewCallDate, tblSCSGrade.PreAssessReviewCallTime, tblSCSGrade.SCR_UploadType,
		CharterAYPTUDAConfirm,CharterAYPSCConfirm, dbo.tblSCSGrade.NSLP_Participation
		,dbo.tblSCSGrade.EnrollmentAtGrade  
		,dbo.tblSCSGrade.NumberOfMathClasses  
		,dbo.tblSCSGrade.NumberOfClasses 
		,dbo.tblSCSGrade.DateSchoolReturnsFromSpringBreak 
		,dbo.tblSCSGrade.DateSchoolReturnsFromWinterBreak  
		,dbo.tblSCSGrade.ClassListSubmited    
		,dbo.tblSCSGrade.ClassListSubmitedBy           
		,dbo.tblSCSGrade.AdvancedEligibility 
		,dbo.tblSCSGrade.AdvancedEligibilityComments 
		,dbo.tblSCSGrade.AdvancedMathComments 
		,dbo.tblSCSGrade.AdvancedPhysicsComments  
		,dbo.tblSCSGrade.PrincipalIncentiveCheckBatchID 
		,dbo.tblSCSGrade.SCIncentiveCheckBatchID       
		,dbo.tblSCSGrade.UPSNumber1
		,dbo.tblSCSGrade.UPSNumber2
		,dbo.tblSCSGrade.FedExNumber1
		,dbo.tblSCSGrade.FedExNumber2
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentTxt
		,dbo.tblSCSGrade.SCIncentiveCheckSentTxt
		,dbo.tblSCSGrade.AssessmentLocation
		,dbo.tblSCSGrade.AssessmentDayLogisticsInformation
		,dbo.tblSCSGrade.ParentConsentType
		,dbo.tblSCSGrade.ParentConsentLanguage
		,dbo.tblSCSGrade.PreAssessmentCallCompleted
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentDT
		,dbo.tblSCSGrade.SCIncentiveCheckSentDT
		,dbo.tblSCSGrade.AssessmentCompleted
		,dbo.tblSCSGrade.DateOfMakeUp
		,dbo.tblSCSGrade.AssessmentMaterialsMailedToPearson
		,gf.[Filename] STLFUserFilePath
		,gf.Filesize STLFFilesize
		,gf.ContentType STLFContentType
		,cast(case when dbo.tblSCSGrade.STLFGradeFileId is null then 0 else 1 end as bit) STLFUploaded	
		,dbo.tblSCSGrade.SCHETime2
		,dbo.tblSCSGrade.ArrivalTime2
		,dbo.tblSCSGrade.DateSchoolStartsSpringBreak 
		
		,ttf.[Filename] TTFUserFilePath
		,ttf.Filesize TTFFilesize
		,ttf.ContentType TTFContentType
		,cast(case when dbo.tblSCSGrade.TTFGradeFileId is null then 0 else 1 end as bit) TTFUploaded	
  FROM  dbo.tblSCSGrade
  left outer join tblGradeFiles gf
  on	gf.GradeFileId =  dbo.tblSCSGrade.STLFGradeFileId
  left outer join tblGradeFiles ttf
  on	ttf.GradeFileId =  dbo.tblSCSGrade.TTFGradeFileId
 WHERE	dbo.tblSCSGrade.areaidgc IS NOT NULL
UNION ALL
SELECT	dbo.tblSCSGrade.fldProjectID, dbo.tblSCSGrade.fldProjectID AS fldProjectID_FA, dbo.tblSCSGrade.ID, dbo.tblSCSGrade.areaidgc AS t_areaid, dbo.tblSCSGrade.AreaID, dbo.tblSCSGrade.AreaIDgc, 
		dbo.tblSCSGrade.Frame_N_, dbo.tblSCSGrade.DISP, dbo.tblSCSGrade.PreAssessDate, dbo.tblSCSGrade.SCHEDATE, dbo.tblSCSGrade.Enroll, 
		dbo.tblSCSGrade.SampMeth, dbo.tblSCSGrade.Pct_On_Break, dbo.tblSCSGrade.CharterFlag, dbo.tblSCSGrade.Pct_On_Break_Ability, 
		dbo.tblSCSGrade.Pct_On_Break_Other, /* dbo.tblSCSGrade.HSTSFlag, */ dbo.tblSCSGrade.AssessmentCompleteFlag, 
		dbo.tblSCSGrade.EfilingSummaryFormFlag, dbo.tblSCSGrade.sampdate, dbo.tblSCSGrade.AssessmentCompleteDate, dbo.tblSCSGrade.nies_flag, 
		dbo.tblSCSGrade.PL_SchoolUsingNCESPL, dbo.tblSCSGrade.PL_StateProvidedPLToSchool, dbo.tblSCSGrade.PL_DateStatePLSentToSchool, 
		dbo.tblSCSGrade.PL_DateSCRecdPLFromSchool, dbo.tblSCSGrade.PL_scdate_discuss_pnot, dbo.tblSCSGrade.SLF_DateReceived, 
		dbo.tblSCSGrade.Phase, dbo.tblSCSGrade.SLF_CompDate, dbo.tblSCSGrade.SCHETime, dbo.tblSCSGrade.QCM25, dbo.tblSCSGrade.scdate_sent_info, 
		dbo.tblSCSGrade.NewSchoolTrigger, dbo.tblSCSGrade.NewSchoolResult, dbo.tblSCSGrade.Calendar, dbo.tblSCSGrade.num_tracks, 
		dbo.tblSCSGrade.eqcbpacketdeliverydate, dbo.tblSCSGrade.eqcbestimatedtimehours, dbo.tblSCSGrade.eqcbsuggestedpavdate, 
		dbo.tblSCSGrade.eqcbsuggestedpavtime, dbo.tblSCSGrade.eqcbschedulingcalldate, dbo.tblSCSGrade.eqcbpavpacketdate, 
		dbo.tblSCSGrade.eqcbupdatedt, dbo.tblSCSGrade.eqcblasttransmitdt, 
		dbo.tblSCSGrade.EQCBPAVPacketSignedForBy, dbo.tblSCSGrade.NE_SampledfromListNE, dbo.tblSCSGrade.SpecSitAssessAll, NULL AS fldphasecode, 
		SLF_Name, 
		CASE dbo.tblSCSGrade.NewSchoolResult WHEN 9 THEN 'Not Answered' WHEN 1 THEN 'Yes, New Schools to Add' WHEN 2 THEN 'Yes, No New Schools'
		WHEN 0 THEN 'No' END AS NewSchoolResultFormat, 
		CASE dbo.tblSCSGrade.NewSchoolTrigger WHEN '1' THEN 'New School Required' WHEN '0' THEN 'New School Not Required' ELSE 'Unknown' END AS NewSchoolTriggerFormat,
		CASE dbo.tblSCSGrade.Charterflag WHEN 9 THEN 'Not Answered' WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' WHEN 2 THEN 'Yes: CCD' WHEN 3 THEN 'Yes: School QuestiONnaire'
		END AS CharterFlagFormat, dbo.tblSCSGrade.Sch_PartLtrSentDT, dbo.tblSCSGrade.SchAsmtLtrSentDT, dbo.tblSCSGrade.AugSchLtrSentDT, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_ELL_W, 0) AS PAV_PEARSON_ELLTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SD_W, 0) AS PAV_PEARSON_SDTotal, 
		ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_L, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_M, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_Mu, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_R, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_S, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_SOC, 0) 
		+ ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_V, 0) + ISNULL(dbo.tblSCSGrade.PAV_PEARSON_SDELL_W, 0) 
		AS PAV_PEARSON_SDELLTotal,dbo.tblSCSGrade.EQCBWorkLengthEstimate,dbo.tblSCSGrade.PreAssessTime,
		dbo.tblSCSGrade.NIES_ScheDate, dbo.tblSCSGrade.NIES_ScheTime, dbo.tblSCSGrade.NIES_Disp, dbo.tblSCSGrade.schedate2,
		dbo.tblSCSGrade.IN_IneligStatusCode, dbo.tblSCSGrade.GR_Date, dbo.tblSCSGrade.SLF_DateCurrent,dbo.tblSCSGrade.pav_pearson_r,dbo.tblSCSGrade.pav_pearson_m,dbo.tblSCSGrade.pav_pearson_s,dbo.tblSCSGrade.pav_pearson_w,dbo.tblSCSGrade.pav_pearson_soc,
		dbo.tblSCSGrade.pav_pearson_sd_r,dbo.tblSCSGrade.pav_pearson_sd_m,dbo.tblSCSGrade.pav_pearson_sd_s,dbo.tblSCSGrade.pav_pearson_sd_w,dbo.tblSCSGrade.pav_pearson_sd_soc,
		dbo.tblSCSGrade.pav_pearson_sd504_r,dbo.tblSCSGrade.pav_pearson_sd504_m,dbo.tblSCSGrade.pav_pearson_sd504_s,dbo.tblSCSGrade.pav_pearson_sd504_w,dbo.tblSCSGrade.pav_pearson_sd504_soc,
		dbo.tblSCSGrade.pav_pearson_ell_r,dbo.tblSCSGrade.pav_pearson_ell_m,dbo.tblSCSGrade.pav_pearson_ell_s,dbo.tblSCSGrade.pav_pearson_ell_w,dbo.tblSCSGrade.pav_pearson_ell_soc,
		dbo.tblSCSGrade.pav_pearson_sdell_r,dbo.tblSCSGrade.pav_pearson_sdell_m,dbo.tblSCSGrade.pav_pearson_sdell_s,dbo.tblSCSGrade.pav_pearson_sdell_w,dbo.tblSCSGrade.pav_pearson_sdell_soc,
		dbo.tblSCSGrade.pav_excl_sd_r,dbo.tblSCSGrade.pav_excl_sd_m,dbo.tblSCSGrade.pav_excl_sd_s,dbo.tblSCSGrade.pav_excl_sd_w,dbo.tblSCSGrade.pav_excl_sd_soc,  
		dbo.tblSCSGrade.pav_excl_sd504_r,dbo.tblSCSGrade.pav_excl_sd504_m,dbo.tblSCSGrade.pav_excl_sd504_s,dbo.tblSCSGrade.pav_excl_sd504_w,dbo.tblSCSGrade.pav_excl_sd504_soc,
		dbo.tblSCSGrade.pav_excl_ell_r,dbo.tblSCSGrade.pav_excl_ell_m ,dbo.tblSCSGrade.pav_excl_ell_s,dbo.tblSCSGrade.pav_excl_ell_w ,dbo.tblSCSGrade.pav_excl_ell_soc,
		dbo.tblSCSGrade.pav_excl_sdell_r,dbo.tblSCSGrade.pav_excl_sdell_m,dbo.tblSCSGrade.pav_excl_sdell_s,dbo.tblSCSGrade.pav_excl_sdell_w,dbo.tblSCSGrade.pav_excl_sdell_soc, CONVERT(VARCHAR(10), InSessionFirstDT, 101) as InSessionFirstDT, tblScsGrade.MUDate, tblSCSGrade.SchoolGoesonBreakDT,
		AIF_SchoolDirection, AIF_SchoolParking, AIF_WhereToMeet, AIF_HowHandleLateComers, AIF_HowContactSchool, 
		AIF_DismissStuPolicy, AIF_Others, AIF_NIESDismissStuPolicy, AIF_SchoolDirection2, AIF_SchoolParking2, AIF_WhereToMeet2, AIF_HowHandleLateComers2, 
		AIF_HowContactSchool2, AIF_DismissStuPolicy2, AIF_Others2, AIF_NIESDismissStuPolicy2, AIF_Protocols, AIF_WorkPlace, AIF_ScheduleEvents, AIF_MeetAfter, LastDayofSchool, CONVERT(VARCHAR(100),schedate,107) AS schedate_107, tblSCSGrade.PreAssessReviewCallDate, tblSCSGrade.PreAssessReviewCallTime, tblSCSGrade.SCR_UploadType,
		CharterAYPTUDAConfirm,CharterAYPSCConfirm, dbo.tblSCSGrade.NSLP_Participation
		,dbo.tblSCSGrade.EnrollmentAtGrade  
		,dbo.tblSCSGrade.NumberOfMathClasses  
		,dbo.tblSCSGrade.NumberOfClasses 
		,dbo.tblSCSGrade.DateSchoolReturnsFromSpringBreak 
		,dbo.tblSCSGrade.DateSchoolReturnsFromWinterBreak  
		,dbo.tblSCSGrade.ClassListSubmited    
		,dbo.tblSCSGrade.ClassListSubmitedBy           
		,dbo.tblSCSGrade.AdvancedEligibility 
		,dbo.tblSCSGrade.AdvancedEligibilityComments 
		,dbo.tblSCSGrade.AdvancedMathComments 
		,dbo.tblSCSGrade.AdvancedPhysicsComments  
		,dbo.tblSCSGrade.PrincipalIncentiveCheckBatchID 
		,dbo.tblSCSGrade.SCIncentiveCheckBatchID       
		,dbo.tblSCSGrade.UPSNumber1
		,dbo.tblSCSGrade.UPSNumber2
		,dbo.tblSCSGrade.FedExNumber1
		,dbo.tblSCSGrade.FedExNumber2
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentTxt
		,dbo.tblSCSGrade.SCIncentiveCheckSentTxt
		,dbo.tblSCSGrade.AssessmentLocation
		,dbo.tblSCSGrade.AssessmentDayLogisticsInformation
		,dbo.tblSCSGrade.ParentConsentType
		,dbo.tblSCSGrade.ParentConsentLanguage
		,dbo.tblSCSGrade.PreAssessmentCallCompleted
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentDT
		,dbo.tblSCSGrade.SCIncentiveCheckSentDT
		,dbo.tblSCSGrade.AssessmentCompleted
		,dbo.tblSCSGrade.DateOfMakeUp
		,dbo.tblSCSGrade.AssessmentMaterialsMailedToPearson
		,gf.[Filename] STLFUserFilePath
		,gf.Filesize STLFFilesize
		,gf.ContentType STLFContentType
		,cast(case when dbo.tblSCSGrade.STLFGradeFileId is null then 0 else 1 end as bit) STLFUploaded	
		,dbo.tblSCSGrade.SCHETime2
		,dbo.tblSCSGrade.ArrivalTime2
		,dbo.tblSCSGrade.DateSchoolStartsSpringBreak 
		
		,ttf.[Filename] TTFUserFilePath
		,ttf.Filesize TTFFilesize
		,ttf.ContentType TTFContentType
		,cast(case when dbo.tblSCSGrade.TTFGradeFileId is null then 0 else 1 end as bit) TTFUploaded		
  FROM	dbo.tblSCSGrade
  left outer join tblGradeFiles gf
  on	gf.GradeFileId =  dbo.tblSCSGrade.STLFGradeFileId
  left outer join tblGradeFiles ttf
  on	ttf.GradeFileId =  dbo.tblSCSGrade.TTFGradeFileId
 WHERE  dbo.tblSCSGrade.areaid IS NULL AND dbo.tblSCSGrade.areaidgc IS NULL























GO


