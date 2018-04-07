USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[sgep_General]    Script Date: 07/25/2014 14:16:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










/* View*/
ALTER VIEW [dbo].[sgep_General]
AS
SELECT dbo.tblSCSSchool.Frame_N_, dbo.tblSCSSchool.LEAID, dbo.tblSCSSchool.Schl_Typ, dbo.tblSCSSchool.S_Name, dbo.tblSCSSchool.S_Addr1, 
               dbo.tblSCSSchool.S_Addr2, dbo.tblSCSSchool.S_City, dbo.tblSCSSchool.S_State, dbo.tblSCSSchool.S_Zip, dbo.tblSCSSchool.S_County, 
               dbo.tblSCSSchool.MailAddr1, dbo.tblSCSSchool.MailAddr2, dbo.tblSCSSchool.MailCity, dbo.tblSCSSchool.MailState, dbo.tblSCSSchool.MailZip, 
               dbo.tblSCSSchool.S_Phone, dbo.tblSCSSchool.S_Fax, dbo.tblSCSSchool.S_Comment, dbo.tblSCSSchool.CoordinatorID, dbo.tblSCSSchool.PrincipalID, 
               dbo.tblSCSSchool.NCESSCH, dbo.tblSCSGrade.ID, dbo.tblSCSGrade.DISP, dbo.tblSCSGrade.SCHEDATE, dbo.tblSCSGrade.SCHETime, 
               dbo.tblSCSGrade.scdate_sent_info, dbo.tblSCSGrade.QCM25, dbo.tblSCSGrade.sampdate, dbo.tblSCSGrade.SampMeth, dbo.tblSCSGrade.MUDATE, 
               dbo.tblSCSDistrict.STID, dbo.tblSCSDistrict.D_Name, dbo.tblGrade_Stat.SMPGRD, dbo.tblGrade_Stat.ISR, dbo.tblGrade_Stat.ORIGSUB, 
               dbo.tblGrade_Stat.HAS_SUB, dbo.tblGrade_Stat.QCM25 AS Expr1, dbo.tblSCSGrade.AreaID, FRS_AreaManagement.fldStateCode, 
               FRS_AreaManagement.fldTerritoryCode, FRS_AreaManagement.fldAreaCode, FRS_AreaManagement.fldRegionCode, FRS_AreaManagement.FMStaffID, FRS_AreaManagement.FMFirstName, 
               FRS_AreaManagement.FMLastName, FRS_AreaManagement.SVStaffID, FRS_AreaManagement.SVFirstName, FRS_AreaManagement.SVLastName, 
               dbo.tblSCSSchoolTypes.TypeName, dbo.tblSCSSchoolTypes.isPublic, dbo.tblSDSGrade.NSLF, dbo.tblSDSGrade.CNT, 
               CASE WHEN dbo.ExAdmin_Project.IsLTT = 1 THEN ESTAGE ELSE dbo.tblGrade_Stat.ESTGRE END AS ESTGRE, dbo.tblSCSGrade.Enroll, dbo.tblGrade_Stat.PIN, 
               dbo.tblSCSSchool.PIN AS Expr2, dbo.uv_SchoolWithID.id4, dbo.uv_SchoolWithID.id8, dbo.uv_SchoolWithID.id12, dbo.tblGrade_Stat.ID AS Expr3, 
               dbo.tblSCSSchool.DateSDCFReceived, dbo.tblSCSSchool.DateSCReviewCompleted, dbo.tblSCSGrade.CharterFlag, dbo.tblGrade_Stat.DELTFLAG, 
               dbo.tblGrade_Stat.PRIVFLAG, dbo.tblSCSSchool.MyNAEPREGID, dbo.tblGrade_Stat.REPSBGRP, dbo.tblSCSGrade.NewSchoolTrigger, 
               dbo.tblSCSGrade.NewSchoolResult, tblSAMPLE.SAMPLE, dbo.tblSCSGrade.FallVisitStatus, dbo.tblSCSGrade.FallVisitDate, dbo.tblSCSGrade.fldProjectID, 
               dbo.tblSCSGrade.ReasonChgSCHEDATE, dbo.tblSCSGrade.ChgSCHEDATEComments, dbo.tblSCSGrade.PL_DateSent, dbo.tblSCSSchool.CompAdminID, 
               dbo.tblSCSDistrict.d_ITContact, dbo.tblSCSGrade.Sch_PartLtrSentDT, dbo.tblSCSGrade.SchAsmtLtrSentDT, dbo.tblSCSGrade.AugSchLtrSentDT, 
               dbo.udf_SchoolLabel(dbo.tblSCSGrade.fldProjectID, dbo.tblSCSGrade.ID) AS SpecialStudies, dbo.tblGrade_Stat.TUA_LEA, dbo.tblSCSGrade.CharterAYP, 
               dbo.tblSCSSchool.SEASCH, dbo.tblSCSPersonnel.email AS PrincipalEmail, tblSCSPersonnel_1.email AS CoordinatorEmail, COALESCE (tblSCSPersonnel_1.email, 
               dbo.tblSCSPersonnel.email) AS SchoolEmail, dbo.tblSCSGrade.PreAssessArriveTime, dbo.tblSCSGrade.PreAssessDate, dbo.tblSCSGrade.PreAssessTime, 
               dbo.tblSCSGrade.ArrivalTime, dbo.tblGrade_Stat.PSSINTYR, dbo.ExAdmin_Project.fldProjectID AS Expr5, dbo.ExAdmin_Project.IsLTT, 
               dbo.tblSCSGrade.InSessionFirstDT,dbo.tblSCSGrade.SchoolGoesOnBreakDT, dbo.tblSCSGrade.LastDayofSchool, dbo.tblSCSGrade.SchCoordPAVdataEntryFinishedDT,COALESCE(CASE WHEN dbo.tblSCSSchool.S_State IS NULL OR dbo.tblSCSSchool.S_State = '' THEN NULL ELSE dbo.tblSCSSchool.S_State END + ', ' + CASE WHEN dbo.tblSCSSchool.S_Zip IS NULL OR dbo.tblSCSSchool.S_Zip = '' THEN NULL ELSE dbo.tblSCSSchool.S_Zip END, CASE WHEN dbo.tblSCSSchool.S_State IS NULL OR dbo.tblSCSSchool.S_State = '' THEN NULL ELSE dbo.tblSCSSchool.S_State END, CASE WHEN dbo.tblSCSSchool.S_Zip IS NULL OR dbo.tblSCSSchool.S_Zip = '' THEN NULL ELSE dbo.tblSCSSchool.S_Zip END) AS S_StateZIP,
               COALESCE(CASE WHEN dbo.tblSCSSchool.MailState IS NULL OR dbo.tblSCSSchool.MailState = '' THEN NULL ELSE dbo.tblSCSSchool.MailState END + ', ' + CASE WHEN dbo.tblSCSSchool.MailZip IS NULL OR dbo.tblSCSSchool.MailZip = '' THEN NULL ELSE dbo.tblSCSSchool.MailZip END, CASE WHEN dbo.tblSCSSchool.MailState IS NULL OR dbo.tblSCSSchool.MailState = '' THEN NULL ELSE dbo.tblSCSSchool.MailState END, CASE WHEN dbo.tblSCSSchool.MailZip IS NULL OR dbo.tblSCSSchool.MailZip = '' THEN NULL ELSE dbo.tblSCSSchool.MailZip END) AS MailStateZIP,
               CharterAYPTUDAConfirm, CharterAYPSCConfirm, ChgTIMSSDISPComments
               
		,dbo.tblSCSGrade.EnrollmentAtGrade  
		,dbo.tblSCSGrade.NumberOfMathClasses  
		,dbo.tblSCSGrade.NumberOfClasses 
		,dbo.tblSCSGrade.DateSchoolReturnsFromSpringBreak 
		,dbo.tblSCSGrade.DateSchoolReturnsFromWinterBreak 
		,dbo.tblSCSGrade.AdvancedEligibility 
		,dbo.tblSCSGrade.AdvancedEligibilityComments 
		,FRS_AreaManagement.fldStateCode + '-' + dbo.Trim(STR(FRS_AreaManagement.fldRegionCode)) AS testregion
		,dbo.tblSCSGrade.AdvancedMathComments 
		,dbo.tblSCSGrade.AdvancedPhysicsComments 
		
		,dbo.tblSCSGrade.PrincipalIncentiveCheckBatchID 
		,dbo.tblSCSGrade.SCIncentiveCheckBatchID       
		,dbo.tblSCSGrade.SchoolIncentiveCheckSentDT
		,dbo.tblSCSGrade.SCIncentiveCheckSentDT
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
		,dbo.tblSCSGrade.AssessmentCompleted
		,dbo.tblSCSGrade.DateOfMakeUp
		,dbo.tblSCSGrade.AssessmentMaterialsMailedToPearson
		
FROM  dbo.tblSCSSchool INNER JOIN
               dbo.tblSCSGrade ON dbo.tblSCSSchool.Frame_N_ = dbo.tblSCSGrade.Frame_N_ INNER JOIN
               dbo.uv_SchoolWithID ON dbo.tblSCSSchool.Frame_N_ = dbo.uv_SchoolWithID.frame_n_ AND 
               dbo.tblSCSGrade.fldProjectID = dbo.uv_SchoolWithID.fldProjectID INNER JOIN
               dbo.tblSCSDistrict ON dbo.tblSCSSchool.LEAID = dbo.tblSCSDistrict.LEAID INNER JOIN
               dbo.tblSCSSchoolTypes ON dbo.tblSCSSchool.Schl_Typ = dbo.tblSCSSchoolTypes.schl_typ INNER JOIN
               dbo.tblGrade_Stat ON dbo.tblSCSGrade.fldProjectID = dbo.tblGrade_Stat.fldProjectID AND dbo.tblSCSGrade.ID = dbo.tblGrade_Stat.ID INNER JOIN
               dbo.ExAdmin_Project ON dbo.ExAdmin_Project.fldProjectID = dbo.tblGrade_Stat.fldProjectID LEFT OUTER JOIN
               dbo.tblSCSPersonnel ON dbo.tblSCSSchool.Frame_N_ = dbo.tblSCSPersonnel.frame_n_ AND 
               dbo.tblSCSSchool.PrincipalID = dbo.tblSCSPersonnel.pID LEFT OUTER JOIN
               dbo.tblSCSPersonnel AS tblSCSPersonnel_1 ON dbo.tblSCSSchool.Frame_N_ = tblSCSPersonnel_1.frame_n_ AND 
               dbo.tblSCSSchool.CoordinatorID = tblSCSPersonnel_1.pID LEFT OUTER JOIN
               dbo.Ex_FRS_AreaManagement AS FRS_AreaManagement ON dbo.tblSCSGrade.fldProjectID = FRS_AreaManagement.fldProjectID AND 
               dbo.tblSCSGrade.AreaID = FRS_AreaManagement.fldAreaID LEFT OUTER JOIN
               dbo.tblSDSGrade ON dbo.tblSCSGrade.fldProjectID = dbo.tblSDSGrade.fldProjectID AND dbo.tblSCSGrade.ID = dbo.tblSDSGrade.ID LEFT OUTER JOIN
                   (SELECT CASE WHEN SPECIALSTUDY = '' THEN 'N/A' ELSE SPECIALSTUDY END AS SAMPLE, fldProjectID, ID
                    FROM   (SELECT NIES + TIMSS1 + TIMSS2 + ESBQ + SMALLREADINGBUNDLE + PI + ECLSK + SDELLWORKSHEET AS SPECIALSTUDY, fldProjectID, ID
                                    FROM   (SELECT tblGrade_Stat_1.fldProjectID, tblGrade_Stat_1.ID, CASE WHEN NIES_FLAG = 1 THEN 'NIES ' ELSE '' END AS NIES, 
                                                                   CASE WHEN TIMSSFLG4 = 1 THEN 'TIMSS ' ELSE '' END AS TIMSS1, CASE WHEN TIMSSFLG8 = 1 THEN 'TIMSS ' ELSE '' END AS TIMSS2, 
                                                                   CASE WHEN BQFLAG = 1 THEN 'ESBQ ' ELSE '' END AS ESBQ, CASE WHEN ECLSKFLG = 1 THEN 'ECLSK ' ELSE '' END AS ECLSK, 
                                                                   CASE WHEN LEFT(tblSCSSession.SESSID, 2) = 'RE' THEN 'Small Reading Bundle ' ELSE '' END AS SMALLREADINGBUNDLE, 
                                                                   CASE WHEN PIFLAG = 1 THEN 'Puerto Rico PILOT ' ELSE '' END AS PI, 
                                                                   CASE WHEN DECTREEFLG = 1 THEN 'Pilot SD ELL Worksheets ' ELSE '' END AS SDELLWORKSHEET
                                                    FROM   dbo.tblSCSGrade AS tblSCSGrade_1 INNER JOIN
                                                                   dbo.tblGrade_Stat AS tblGrade_Stat_1 ON tblSCSGrade_1.fldProjectID = tblGrade_Stat_1.fldProjectID AND 
                                                                   tblSCSGrade_1.ID = tblGrade_Stat_1.ID LEFT OUTER JOIN
                                                                       (SELECT DISTINCT fldProjectID, ID, Sessid
                                                                        FROM   dbo.tblSCSSession AS tblSCSSession_1
                                                                        WHERE (LEFT(Sessid, 2) = 'RE')) AS tblSCSSession ON tblSCSGrade_1.fldProjectID = tblSCSSession.fldProjectID AND 
                                                                   tblSCSGrade_1.ID = tblSCSSession.ID) AS SAMPLE_1) AS SAMPLE) AS tblSAMPLE ON 
               dbo.tblSCSGrade.fldProjectID = tblSAMPLE.fldProjectID AND dbo.tblSCSGrade.ID = tblSAMPLE.ID
		










GO


