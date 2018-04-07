USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[uv_NewGradeInfoWithFRS]    Script Date: 08/28/2014 14:03:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER VIEW [dbo].[uv_NewGradeInfoWithFRS]
AS
SELECT	dbo.uv_tblSCSGrade.ID, dbo.uv_tblSCSGrade.Frame_N_, dbo.tblGrade_Stat.SMPGRD, dbo.tblGrade_Stat.TRGSSZ, 
		CASE WHEN dbo.tblGrade_stat.DOM_TA = '1' THEN 'TAKE ALL' ELSE CAST(dbo.tblGrade_Stat.SCSSSZ AS varchar) END AS SCSSSZ, dbo.uv_tblSCSGrade.DISP, 
		dbo.tblSCSGradeDispositionCodes.DispName, dbo.uv_tblSCSGrade.PreAssessDate, dbo.uv_tblSCSGrade.SCHEDATE, dbo.tblGrade_Stat.ORIGSUB, 
		dbo.tblGrade_Stat.ESTGRE, dbo.uv_tblSCSGrade.Enroll, dbo.uv_tblSCSGrade.SampMeth, dbo.tblSCSGradeSamplingMethods.MethodName, 
		dbo.uv_tblSCSGrade.SLF_DateReceived, dbo.tblGrade_Stat.HAS_SUB, dbo.uv_tblSCSGrade.Pct_On_Break, dbo.uv_tblSCSGrade.AreaID, 
		dbo.uv_tblSCSGrade.CharterFlag, dbo.tblGrade_Stat.HSTSFLAG, dbo.uv_tblSCSGrade.AssessmentCompleteFlag, dbo.uv_tblSCSGrade.EfilingSummaryFormFlag, 
		dbo.uv_tblSCSGrade.NE_SampledfromListNE, dbo.tblGrade_Stat.STUD_CNT, dbo.uv_tblSCSGrade.sampdate, dbo.uv_tblSCSGrade.PL_SchoolUsingNCESPL, 
		dbo.uv_tblSCSGrade.PL_StateProvidedPLToSchool, dbo.uv_tblSCSGrade.PL_DateStatePLSentToSchool, dbo.uv_tblSCSGrade.PL_DateSCRecdPLFromSchool, 
		dbo.uv_tblSCSGrade.PL_scdate_discuss_pnot, dbo.uv_tblSCSGrade.AssessmentCompleteDate, dbo.tblGrade_Stat.DELTFLAG, dbo.tblGrade_Stat.PRIVFLAG, 
		dbo.uv_tblSCSGrade.AreaIDgc, dbo.Ex_FRS_AreaManagement.fldAreaID, dbo.uv_tblSCSGrade.fldProjectID, dbo.uv_tblSCSGrade.fldPhasecode, 
		dbo.Ex_FRS_AreaManagement.fldTerritoryCode, dbo.Ex_FRS_AreaManagement.fldStateCode, dbo.Ex_FRS_AreaManagement.fldRegionCode, 
		dbo.Ex_FRS_AreaManagement.fldAreaCode, dbo.tblGrade_Stat.REPSBGRP, dbo.Ex_FRS_AreaManagement.fldSuperTerritoryCode
		--, dbo.Ex_FRS_AreaManagement.fldProjectDesc
		, dbo.udf_SchoolLabel(dbo.uv_tblSCSGrade.fldProjectID, dbo.uv_tblSCSGrade.ID) AS sample, 
		dbo.tblGrade_Stat.ESTAGE, dbo.tblGrade_Stat.ESS_TA, dbo.uv_tblSCSGrade.num_tracks, dbo.uv_tblSCSGrade.Calendar, dbo.uv_tblSCSGrade.SLF_Name, 
		dbo.tblGrade_Stat.ALPHFLAG, dbo.tblGrade_Stat.BETAFLAG, dbo.tblGrade_Stat.GAMMFLAG, dbo.uv_tblSCSGrade.NewSchoolResultFormat, 
		dbo.uv_tblSCSGrade.NewSchoolTriggerFormat, dbo.uv_tblSCSGrade.CharterFlagFormat, dbo.uv_tblSCSGrade.Sch_PartLtrSentDT, 
		dbo.uv_tblSCSGrade.SchAsmtLtrSentDT, dbo.uv_tblSCSGrade.AugSchLtrSentDT, dbo.tblGrade_Stat.NEWSCHL, dbo.uv_tblSCSGrade.PAV_PEARSON_ELLTotal, 
		dbo.uv_tblSCSGrade.PAV_PEARSON_SDTotal, dbo.uv_tblSCSGrade.PAV_PEARSON_SDELLTotal, dbo.uv_tblSCSGrade.fldProjectID_FA, 
		dbo.uv_tblSCSGrade.InSessionFirstDT, dbo.uv_tblSCSGrade.MUDate, dbo.tblGrade_Stat.PPFLAG, dbo.tblGrade_Stat.TELFLAG, dbo.tblGrade_Stat.ICTFLAG,
		dbo.uv_tblSCSGrade.LastDayofSchool, dbo.uv_tblSCSGrade.CharterAYPTUDAConfirm, dbo.uv_tblSCSGrade.CharterAYPSCConfirm,
		dbo.tblGrade_Stat.TAUFLAG, dbo.tblGrade_Stat.EPSIFLAG  
		
		,dbo.uv_tblSCSGrade.ScheTime  
		,dbo.uv_tblSCSGrade.EnrollmentAtGrade  
		,dbo.uv_tblSCSGrade.NumberOfMathClasses  
		,dbo.uv_tblSCSGrade.NumberOfClasses          
		,dbo.uv_tblSCSGrade.DateSchoolReturnsFromSpringBreak 
		,dbo.uv_tblSCSGrade.DateSchoolReturnsFromWinterBreak   
		,dbo.tblGrade_Stat.TUA_LEA
		,dbo.uv_tblSCSGrade.ClassListSubmited
		,dbo.uv_tblSCSGrade.ClassListSubmitedBy
		,dbo.uv_AccountDetails.FirstName ClassListSubmitedByFirstName
		,dbo.uv_AccountDetails.LastName ClassListSubmitedByLastName    
		,dbo.uv_tblSCSGrade.AdvancedEligibility 
		,dbo.uv_tblSCSGrade.AdvancedEligibilityComments 
		,dbo.uv_tblSCSGrade.AdvancedMathComments 
		,dbo.uv_tblSCSGrade.AdvancedPhysicsComments   
		,dbo.uv_tblSCSGrade.PrincipalIncentiveCheckBatchID 
		,dbo.uv_tblSCSGrade.SCIncentiveCheckBatchID       
		,dbo.uv_tblSCSGrade.UPSNumber1
		,dbo.uv_tblSCSGrade.UPSNumber2
		,dbo.uv_tblSCSGrade.FedExNumber1
		,dbo.uv_tblSCSGrade.FedExNumber2
		,dbo.uv_tblSCSGrade.SchoolIncentiveCheckSentTxt
		,dbo.uv_tblSCSGrade.SCIncentiveCheckSentTxt
		,dbo.uv_tblSCSGrade.AssessmentLocation
		,dbo.uv_tblSCSGrade.AssessmentDayLogisticsInformation
		,dbo.uv_tblSCSGrade.ParentConsentType
		,dbo.uv_tblSCSGrade.ParentConsentLanguage
		,dbo.uv_tblSCSGrade.PreAssessmentCallCompleted
		,dbo.uv_tblSCSGrade.SchoolIncentiveCheckSentDT
		,dbo.uv_tblSCSGrade.SCIncentiveCheckSentDT
		,dbo.uv_tblSCSGrade.AssessmentCompleted
		,dbo.uv_tblSCSGrade.DateOfMakeUp
		,dbo.uv_tblSCSGrade.AssessmentMaterialsMailedToPearson
		,dbo.uv_tblSCSGrade.STLFUserFilePath
		,dbo.uv_tblSCSGrade.STLFFilesize
		,dbo.uv_tblSCSGrade.STLFContentType
		,dbo.uv_tblSCSGrade.STLFUploaded	
		
		,case when SUBSTRING(reverse(dbo.uv_tblSCSGrade.ID),1,1) = 'I' then 'ICILS' when SUBSTRING(reverse(dbo.uv_tblSCSGrade.ID),1,1) = 'T' then 'eTIMSS'  else fldProjectDesc  end fldProjectDesc
		,cast(case when SUBSTRING(reverse(dbo.uv_tblSCSGrade.ID),1,1) = 'I' then 1 else 0 end as bit) isICILS
		,cast(case when SUBSTRING(reverse(dbo.uv_tblSCSGrade.ID),1,1) = 'T' then 1 else 0 end as bit) isETIMSS
		, dbo.uv_tblSCSGrade.SCHEDATE2
		,dbo.uv_tblSCSGrade.SCHETime2
		,dbo.uv_tblSCSGrade.ArrivalTime2
		,dbo.uv_tblSCSGrade.DateSchoolStartsSpringBreak 		
		
		,dbo.uv_tblSCSGrade.TTFUserFilePath
		,dbo.uv_tblSCSGrade.TTFFilesize
		,dbo.uv_tblSCSGrade.TTFContentType
		,dbo.uv_tblSCSGrade.TTFUploaded	
  FROM	dbo.uv_tblSCSGrade INNER JOIN
		dbo.tblGrade_Stat ON dbo.uv_tblSCSGrade.fldProjectID_FA = dbo.tblGrade_Stat.fldProjectID AND dbo.uv_tblSCSGrade.ID = dbo.tblGrade_Stat.ID LEFT OUTER JOIN
		dbo.tblSCSGradeSamplingMethods ON dbo.uv_tblSCSGrade.SampMeth = dbo.tblSCSGradeSamplingMethods.SampMeth LEFT OUTER JOIN
		dbo.tblSCSGradeDispositionCodes ON dbo.uv_tblSCSGrade.fldProjectID_FA = dbo.tblSCSGradeDispositionCodes.fldProjectID AND 
		dbo.uv_tblSCSGrade.DISP = dbo.tblSCSGradeDispositionCodes.DISP LEFT OUTER JOIN
		dbo.Ex_FRS_AreaManagement ON dbo.uv_tblSCSGrade.t_areaid = dbo.Ex_FRS_AreaManagement.fldAreaID
		LEFT OUTER JOIN
		dbo.uv_AccountDetails ON dbo.uv_AccountDetails.UserId = dbo.uv_tblSCSGrade.ClassListSubmitedBy
		
		
		 



GO


