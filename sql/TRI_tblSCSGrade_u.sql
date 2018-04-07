ALTER TRIGGER [dbo].[TRI_tblSCSGrade_u] ON [dbo].[tblSCSGrade] 
AFTER UPDATE
AS 
DECLARE @MUDATE DATETIME,
        @OLDMUDATE DATETIME
DECLARE @SCHEDATE DATETIME,
        @OLDSCHEDATE DATETIME	
DECLARE @SCHETIME VARCHAR(8),
        @OLDSCHETIME VARCHAR(8),
		@PL_DateSent Datetime,
		--@NE_NumNewEnrollees int,
		--@OLDNE_NumNewEnrollees int,
		@NE_NumNEAddedToSample int,
		@OLDNE_NumNEAddedToSample int,
		@IN_SchoolMergeYN varchar(1),
		@IN_OwnLEAYN varchar(1),
		@IN_PartLEAYN varchar(1),
		@IN_SchoolSplitYN varchar(1),
		@IN_SchoolClosedYN varchar(1),
		@IN_SchoolVerifiedYN varchar(1),
		@IN_SchoolRevertPendingYN varchar(1),
		@IN_SchoolApplyYN varchar(1),
		@sampdate datetime
DECLARE @fldProjectID int,
		@ID varchar(7),
		@CharterFlag Smallint,
		@OLDCharterFlag Smallint
DECLARE @PREAssessDate AS DATETIME,
        @PapkSentDT AS DATETIME,
        @SLF_CompDate DATETIME,
		@errno int,
		@errmsg varchar(255)
DECLARE @DISP CHAR (2), 
		@HASSUB CHAR (1) , 
		@ORIG_FRAME_N_  CHAR (6), 
		@SUB_FRAME_N_ CHAR (6),
		@ORIG_ID CHAR (7), 
		@SUB_ID CHAR(7), 
		@SUBID4 CHAR(7),
		@SUBID8 CHAR(7), 
		@SUBID12 CHAR(7), 
		@LEAID CHAR (8),
		@REGIONCODE INT, 
		@AREAID INT, 
		@OLD_DISP CHAR(2), 
		@AREAIDGC INT,
		/* Activated School SLMS Variables   -- Programmer: Christian Kapombe , Date: 4/10/2006 */
		@EFilePreparerCode CHAR(1),  
		@EFileListTypeCode CHAR(1),  
		@EFileSubmitterCode CHAR(1),  
		@EFileMultipleSingleRaceCode varchar(2),
		@EFileDueDate SMALLDATETIME, 
		@EFileLevel CHAR(1),  
		@flgSave CHAR(1), 
		@EFileFormerELLCode INT,
		@EFileZipCode INT,
		@EFileDefinedTemplateCode VARCHAR(1),
		@FIPSCODE CHAR(2),
		@UPDATEdByUID INT,
		@IN_ConfirmationDate DATETIME,
		@OLDIN_ConfirmationDate DATETIME,
		@CreatingGroupCYN INT,
		@SESSID varchar(6),
		@old_CreatingGroupCYN INT,
		@AIFID_Bgroup int,
		@AIFID_Cgroup int,
		@old_CertifyName varchar(128),
		@new_CertifyName varchar(128),
		@AssessmentCompleteFlag smallint,
		@DestroyredenvelopeYN int

SET NOCOUNT ON

SELECT @fldProjectID = fldProjectID FROM INSERTED
SELECT @ID = ID FROM INSERTED
SELECT @UPDATEdByUID = UPDATEdByUID FROM INSERTED
/*
IF UPDATE(SCHEDATE) or UPDATE(MUDATE)
BEGIN
	SELECT @SCHEDATE=SCHEDATE  FROM INSERTED
	SELECT @MUDATE=MUDATE  FROM INSERTED

	IF @MUDATE<@SCHEDATE and ( @SCHEDATE IS not NULL AND @MUDATE IS NOT NULL )
	BEGIN
		SELECT @errno  = 30001
	        SELECT @errmsg = 'makeup date ['+convert(varchar,@MUDATE,101)+'] should not be eariler than the assessment date ['+convert(varchar,@SCHEDATE,101)+']'
		GOTO ERROR		
	END 
END

IF UPDATE(SCHEDATE) or UPDATE(PL_DateSent)
BEGIN
	SELECT @SCHEDATE=SCHEDATE  FROM INSERTED
	SELECT @PL_DateSent=PL_DateSent  FROM INSERTED

	IF @PL_DateSent>@SCHEDATE and ( @SCHEDATE IS not NULL AND @PL_DateSent IS NOT NULL )
	BEGIN
		SELECT @errno  = 30001
	        SELECT @errmsg = 'the date parent letter was sent ['+convert(varchar,@PL_DateSent,101)+'] should not after the assessment date ['+convert(varchar,@SCHEDATE,101)+']'
		GOTO ERROR		
	END 
END

IF UPDATE(PREAssessDate) or UPDATE(PapkSentDT)
BEGIN
	SELECT @PREAssessDate=PREAssessDate  FROM INSERTED
	SELECT @PapkSentDT=PapkSentDT  FROM INSERTED
                     
	IF @PREAssessDate<@PapkSentDT AND ( @PREAssessDate IS not NULL AND @PapkSentDT IS NOT NULL )
	BEGIN
		
		SELECT @errno  = 30001
	        SELECT @errmsg = 'the date for the preassessment visit date ['+convert(varchar,@PREAssessDate,101)+'] should not before the date the school packet was sent. ['+convert(varchar,@PapkSentDT,101)+']'
		GOTO ERROR		                     
	END 
end

IF UPDATE(NE_NumNewEnrollees) or UPDATE(NE_NumNEAddedToSample)
BEGIN
	SELECT @NE_NumNewEnrollees=NE_NumNewEnrollees  FROM INSERTED
	SELECT @NE_NumNEAddedToSample=NE_NumNEAddedToSample  FROM INSERTED
                     
	IF @NE_NumNewEnrollees<@NE_NumNEAddedToSample AND ( @NE_NumNewEnrollees IS not NULL AND @NE_NumNEAddedToSample IS NOT NULL )
	BEGIN
		
		SELECT @errno  = 30001
	        SELECT @errmsg = 'the number for the sampled new enrollees ['+cast(@NE_NumNEAddedToSample as varchar)+'] should not larger than the number of new enrollees. ['+cast(@NE_NumNewEnrollees as varchar)+']'
		GOTO ERROR		                     
	END 
end
*/

-- 13/07/29
IF UPDATE(sampdate)
BEGIN
	SELECT @sampdate = sampdate  FROM INSERTED
	
	UPDATE dbo.tblSCSGrade
	   SET PAPkSentDt = @sampdate
	 WHERE fldProjectID = @fldProjectID
	   AND ID = @ID
END

IF	UPDATE(SLF_CompDate)	
BEGIN
	SELECT @SLF_CompDate = SLF_CompDate  FROM INSERTED
	
	UPDATE dbo.tblSCSGrade
	   SET PAPkSentDt = @SLF_CompDate
	 WHERE fldProjectID = @fldProjectID
	   AND ID = @ID
END

-- 08/09/24
IF UPDATE(CharterFlag)
BEGIN
	SELECT @CharterFlag = CharterFlag  FROM INSERTED
	SELECT @OLDCharterFlag = CharterFlag  FROM DELETED

	IF @CharterFlag = 0 OR @CharterFlag = 9
	BEGIN
		UPDATE dbo.tblSCSGrade
		   SET CharterAYP = 9, 
			   CharterAYPTUDAConfirm = 9, 
			   CharterAYPSCConfirm = 9
		 WHERE fldProjectID = @fldProjectID
		   AND ID = @ID
	END
END

IF UPDATE(MUDATE)
BEGIN
	SELECT @MUDATE = MUDATE  FROM INSERTED
	SELECT @OLDMUDATE=MUDATE FROM DELETED
                     
	IF @MUDATE<>@OLDMUDATE OR ( @OLDMUDATE IS NULL AND @MUDATE IS NOT NULL )
	BEGIN
		UPDATE TBLSCSSESSION
		SET TBLSCSSESSION.MAKE_DT=@MUDATE
		FROM TBLSCSSESSION, DELETED, INSERTED
		WHERE TBLSCSSESSION.ID=INSERTED.ID
	END 
END

IF UPDATE(SCHEDATE)
BEGIN
	SELECT @SCHEDATE=SCHEDATE  FROM INSERTED
	SELECT @OLDSCHEDATE=SCHEDATE FROM DELETED
                     
	IF @SCHEDATE<>@OLDSCHEDATE OR ( @OLDSCHEDATE IS NULL AND @SCHEDATE IS NOT NULL )
	BEGIN
		UPDATE TBLSCSSESSION
		SET TBLSCSSESSION.SES_DATE=@SCHEDATE
		FROM TBLSCSSESSION, DELETED, INSERTED
		WHERE TBLSCSSESSION.ID=INSERTED.ID
	END 
END

IF UPDATE(SCHETIME)
BEGIN
	SELECT @SCHETIME=SCHETIME  FROM INSERTED
	SELECT @OLDSCHETIME=SCHETIME FROM DELETED

	IF @SCHETIME<>@OLDSCHETIME OR (@OLDSCHETIME IS NULL AND @SCHETIME IS NOT NULL) 
	BEGIN
		UPDATE TBLSCSSESSION
		SET TBLSCSSESSION.SES_TIME=@SCHETIME
		FROM TBLSCSSESSION, DELETED, INSERTED
		WHERE TBLSCSSESSION.ID=INSERTED.ID
	END 
END

              
IF UPDATE(DISP) 
BEGIN 
	SELECT @DISP=DISP FROM INSERTED 
	SELECT @OLD_DISP=DISP FROM DELETED
	SELECT @HASSUB=HAS_SUB FROM TBLGRADE_STAT WHERE ID=@ID
	                        
    IF (@DISP='30' OR @DISP='32' OR @DISP='33') AND (@OLD_DISP<>'30' AND @OLD_DISP<>'32' AND @OLD_DISP<>'33')
	BEGIN
                                   
		IF @HASSUB='1'         
		BEGIN
                                               
			SELECT @ORIG_FRAME_N_=FRAME_N_ FROM INSERTED
			SELECT @ORIG_ID=ID FROM INSERTED WHERE FRAME_N_=@ORIG_FRAME_N_
			SELECT @AREAID=AREAID  FROM INSERTED WHERE FRAME_N_=@ORIG_FRAME_N_ 
			SELECT @AREAIDGC=AREAIDGC  FROM INSERTED WHERE FRAME_N_=@ORIG_FRAME_N_ 
                                                                                             
			IF RIGHT(LEFT(@ORIG_ID,3),1)='1'
			BEGIN
				IF @fldProjectID<>607
				BEGIN
					SELECT @SUBID4=SUBID4 FROM uv_SchoolwithID  WHERE FRAME_N_=@ORIG_FRAME_N_ AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_FRAME_N_=FRAME_N_ FROM TBLSCSGRADE WHERE ID=@SUBID4 AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_ID=@SUBID4
				END
                                                            
				IF @fldProjectID=607 
				BEGIN
					SELECT @SUBID4=SUBAGE9 FROM uv_SchoolwithID  WHERE FRAME_N_=@ORIG_FRAME_N_ AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_FRAME_N_=FRAME_N_ FROM TBLSCSGRADE WHERE ID=@SUBID4 AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_ID=@SUBID4
				END
			END
                                               
			IF RIGHT(LEFT(@ORIG_ID,3),1)='2'
			BEGIN
				IF @fldProjectID<>607
				BEGIN
					SELECT @SUBID8=SUBID8 FROM uv_SchoolwithID WHERE FRAME_N_=@ORIG_FRAME_N_ AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_FRAME_N_=FRAME_N_ FROM TBLSCSGRADE  WHERE ID=@SUBID8 AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_ID=@SUBID8
				END
                                                            
				IF @fldProjectID=607
				BEGIN
					SELECT @SUBID8=SUBAGE13  FROM uv_SchoolwithID WHERE FRAME_N_=@ORIG_FRAME_N_ AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_FRAME_N_=FRAME_N_ FROM TBLSCSGRADE  WHERE ID=@SUBID8 AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_ID=@SUBID8
				END
			END
                                               
			IF RIGHT(LEFT(@ORIG_ID,3),1)='3'
			BEGIN
				IF @fldProjectID<>607
				BEGIN
					SELECT @SUBID12=SUBID12 FROM  uv_SchoolwithID  WHERE FRAME_N_=@ORIG_FRAME_N_ AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_FRAME_N_=FRAME_N_ FROM TBLSCSGRADE WHERE ID=@SUBID12 AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_ID=@SUBID12
				END
                                                            
				IF @fldProjectID=607
				BEGIN
					SELECT @SUBID12=SUBID12 FROM  uv_SchoolwithID  WHERE FRAME_N_=@ORIG_FRAME_N_ AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_FRAME_N_=FRAME_N_ FROM TBLSCSGRADE WHERE ID=@SUBID12 AND FLDPROJECTID=@fldProjectID
					SELECT @SUB_ID=@SUBID12
				END
			END
                                               
			UPDATE TBLSCSSCHOOL
			   SET TBLSCSSCHOOL.SYSACTIVE=1
			  FROM TBLSCSSCHOOL, DELETED, INSERTED
			 WHERE TBLSCSSCHOOL.FRAME_N_=@SUB_FRAME_N_

			SELECT @LEAID=LEAID, @FIPSCODE=LEFT(LEAID,2) FROM TBLSCSSCHOOL WHERE FRAME_N_=@SUB_FRAME_N_
      
			UPDATE TBLSCSDISTRICT
			   SET TBLSCSDISTRICT.SYSACTIVE=1
			  FROM TBLSCSDISTRICT, DELETED, INSERTED
			 WHERE TBLSCSDISTRICT.LEAID=@LEAID
                                                                              
			UPDATE TBLSCSGRADE
			   SET TBLSCSGRADE.DISP='00', 
			       TBLSCSGRADE.AREAID=@AREAID,
			       TBLSCSGRADE.AREAIDGC=@AREAIDGC,
			       TBLSCSGRADE.SubActiveDate=getdate(), 
			       TBLSCSGRADE.fldProjectIDGC=(SELECT fldProjectID FROM ExAdmin_Project WHERE (sysActive = 1) AND (fldProjectType = 3))
			  FROM TBLSCSGRADE, DELETED, INSERTED
			 WHERE TBLSCSGRADE.ID=@SUB_ID AND TBLSCSGRADE.FLDPROJECTID=@fldProjectID

			/*Activated subs pickup designations set at the school level*/
			/*School Level */
			IF EXISTS (SELECT * FROM tblSCSSLDesignationStatus WHERE EFileLevel ='C' AND FRAME_N_ = @ORIG_FRAME_N_)
 			
			BEGIN
                /*pickup designations */
	 			SELECT	@EFilePreparerCode=EFilePreparerCode, 
 						@EFileListTypeCode=EFileListTypeCode, 
 						@EFileSubmitterCode=EFileSubmitterCode, 
 						@EFileMultipleSingleRaceCode=EFileMultipleSingleRaceCode,
 						@EFileDueDate=EFileDueDate, 
 						@EFileLevel=EFileLevel, 
 						@flgSave=flgSave,
 						@EFileFormerELLCode=EFileFormerELLCode,
 						@EFileZipCode=EFileZipCode,
 						@EFileDefinedTemplateCode=EFileDefinedTemplateCode
 			  	  FROM	tblSCSSLDesignationStatus
 				 WHERE	EFileLevel='C' AND 
						FRAME_N_=@ORIG_FRAME_N_

				/*Insert picked designations */
				IF NOT EXISTS (SELECT * FROM tblSCSSLDesignationStatus WHERE EFileLevel ='C' AND FRAME_N_ = @SUB_FRAME_N_)
				BEGIN
					INSERT INTO tblSCSSLDesignationStatus
							(FIPSCODE, LEAID, FRAME_N_, EFilePreparerCode, EFileListTypeCode, EFileSubmitterCode, EFileMultipleSingleRaceCode, EFileDueDate, EFileLevel, flgSave, EFileFormerELLCode, EFileZipCode, EFileDefinedTemplateCode)
					VALUES	(@FIPSCODE,@LEAID,@SUB_FRAME_N_,@EFilePreparerCode, @EFileListTypeCode, @EFileSubmitterCode, @EFileMultipleSingleRaceCode, @EFileDueDate, @EFileLevel, @flgSave, @EFileFormerELLCode, @EFileZipCode, @EFileDefinedTemplateCode)
				END

				/*UPDATE picked designation */
				IF  EXISTS (SELECT * FROM tblSCSSLDesignationStatus WHERE EFileLevel='C' AND FRAME_N_=@SUB_FRAME_N_)
				BEGIN   
					UPDATE	tblSCSSLDesignationStatus  
					   SET	EFilePreparerCode=@EFilePreparerCode,
							EFileListTypeCode=@EFileListTypeCode,
							EFileSubmitterCode=@EFileSubmitterCode,
							EFileMultipleSingleRaceCode=@EFileMultipleSingleRaceCode,
 							EFileDueDate=@EFileDueDate,
							EFileLevel=@EFileLevel,
							flgSave=@flgSave,
							EFileFormerELLCode=@EFileFormerELLCode,
 							EFileZipCode=@EFileZipCode,
 							EFileDefinedTemplateCode=@EFileDefinedTemplateCode
					 WHERE  FIPSCODE=@FIPSCODE AND 
							LEAID=@LEAID AND 
							FRAME_N_=@SUB_FRAME_N_
				END
                                       
			END
		END
	END

	IF (@DISP='42') -- Closed DISP: 42 TIMSS
	BEGIN
		SELECT @IN_SchoolMergeYN=IN_SchoolMergeYN FROM INSERTED
		SELECT @IN_OwnLEAYN=IN_OwnLEAYN FROM INSERTED
		SELECT @IN_PartLEAYN=IN_PartLEAYN FROM INSERTED
		SELECT @IN_SchoolSplitYN=IN_SchoolSplitYN FROM INSERTED
        SELECT @IN_SchoolVerifiedYN=IN_SchoolVerifiedYN FROM INSERTED
		SELECT @IN_SchoolRevertPendingYN =IN_SchoolRevertPendingYN FROM INSERTED
		SELECT @IN_SchoolClosedYN =IN_SchoolClosedYN FROM INSERTED
		SELECT @IN_SchoolApplyYN =IN_SchoolApplyYN FROM INSERTED
		
		IF @IN_SchoolClosedYN='N' AND @IN_SchoolVerifiedYN ='Y'
		BEGIN
			UPDATE	TBLSCSGRADE
			   SET	TBLSCSGRADE.DISP='00',
			        IN_SchoolApplyYN = NULL,
			        IN_SchoolVerifiedYN =NULL,
			        IN_SchoolClosedYN = NULL,
					IN_SchoolRevertPendingYN = NULL 
			  FROM	TBLSCSGRADE
			 WHERE	TBLSCSGRADE.FLDPROJECTID=@fldProjectID AND
					TBLSCSGRADE.ID=@ID 					
		END
		
		IF @IN_SchoolApplyYN='N' AND @IN_SchoolVerifiedYN ='N'
		BEGIN
			UPDATE	TBLSCSGRADE
			  SET      IN_SchoolClosedYN = 'Y'
			  FROM	TBLSCSGRADE
			 WHERE	TBLSCSGRADE.FLDPROJECTID=@fldProjectID AND
					TBLSCSGRADE.ID=@ID 					
		END
		
		IF @IN_SchoolApplyYN='Y' AND @IN_SchoolVerifiedYN ='N'
		BEGIN
			UPDATE	TBLSCSGRADE
			  SET	TBLSCSGRADE.DISP='00',
			        IN_SchoolApplyYN = NULL,
			        IN_SchoolVerifiedYN =NULL,
			        IN_SchoolClosedYN = NULL,
					IN_SchoolRevertPendingYN = NULL 
			  FROM	TBLSCSGRADE
			 WHERE	TBLSCSGRADE.FLDPROJECTID=@fldProjectID AND
					TBLSCSGRADE.ID=@ID 					
		END
    END
END

IF	UPDATE(IN_SchoolMergeYN) OR
	UPDATE(IN_OwnLEAYN) OR
	UPDATE(IN_PartLEAYN) OR
	UPDATE(IN_SchoolSplitYN) 
BEGIN
	SELECT @DISP=DISP FROM INSERTED 
	
	IF (@DISP='42')  -- Closed DISP: 42 TIMSS 
	BEGIN
		SELECT @IN_SchoolMergeYN=IN_SchoolMergeYN FROM INSERTED
		SELECT @IN_OwnLEAYN=IN_OwnLEAYN FROM INSERTED
		SELECT @IN_PartLEAYN=IN_PartLEAYN FROM INSERTED
		SELECT @IN_SchoolSplitYN=IN_SchoolSplitYN FROM INSERTED
        SELECT @IN_SchoolClosedYN=IN_SchoolClosedYN FROM INSERTED
		SELECT @IN_SchoolVerifiedYN=IN_SchoolVerifiedYN FROM INSERTED
		SELECT @IN_SchoolRevertPendingYN =IN_SchoolRevertPendingYN FROM INSERTED
		SELECT @IN_SchoolApplyYN =IN_SchoolApplyYN FROM INSERTED
		
		IF @IN_SchoolClosedYN='N' AND @IN_SchoolVerifiedYN ='Y'
		BEGIN
			UPDATE	TBLSCSGRADE
			   SET	TBLSCSGRADE.DISP='00',
			        IN_SchoolApplyYN = NULL,
			        IN_SchoolVerifiedYN =NULL,
			        IN_SchoolClosedYN = NULL,
					IN_SchoolRevertPendingYN = NULL 
			  FROM	TBLSCSGRADE
			 WHERE	TBLSCSGRADE.FLDPROJECTID=@fldProjectID AND
					TBLSCSGRADE.ID=@ID 					
		END
		
		IF @IN_SchoolApplyYN='N' AND @IN_SchoolVerifiedYN ='N'
		BEGIN
			UPDATE	TBLSCSGRADE
			  SET      IN_SchoolClosedYN = 'Y'
			  FROM	TBLSCSGRADE
			 WHERE	TBLSCSGRADE.FLDPROJECTID=@fldProjectID AND
					TBLSCSGRADE.ID=@ID 					
		END
		
		IF @IN_SchoolApplyYN='Y' AND @IN_SchoolVerifiedYN ='N'
		BEGIN
			UPDATE	TBLSCSGRADE
			  SET	TBLSCSGRADE.DISP='00',
			        IN_SchoolApplyYN = NULL,
			        IN_SchoolVerifiedYN =NULL,
			        IN_SchoolClosedYN = NULL,
					IN_SchoolRevertPendingYN = NULL 
			  FROM	TBLSCSGRADE
			 WHERE	TBLSCSGRADE.FLDPROJECTID=@fldProjectID AND
					TBLSCSGRADE.ID=@ID 					
		END
    END
END

IF UPDATE(IN_ConfirmationDate)
BEGIN
	SELECT @IN_ConfirmationDate=IN_ConfirmationDate  FROM INSERTED
	SELECT @OLDIN_ConfirmationDate=IN_ConfirmationDate FROM DELETED
                     
	IF (@IN_ConfirmationDate<>@OLDIN_ConfirmationDate) OR 
	   (@OLDIN_ConfirmationDate IS NULL AND @IN_ConfirmationDate IS NOT NULL) OR
	   (@OLDIN_ConfirmationDate IS NOT NULL AND @IN_ConfirmationDate IS NULL)
	BEGIN
		INSERT INTO tblSCSTrackingGrade
			(fldProjectID, ID, fldID, tDate, CurrVal, PreVal, WhoChanged)
		VALUES
			(@fldProjectID, @ID, 'IN_ConfirmationDate', getdate(),@IN_ConfirmationDate,@OLDIN_ConfirmationDate,@UPDATEdByUID)
	END 
END

IF UPDATE(NE_NumNEAddedToSample)
BEGIN
	SELECT @NE_NumNEAddedToSample = NE_NumNEAddedToSample  FROM INSERTED
	SELECT @OLDNE_NumNEAddedToSample = NE_NumNEAddedToSample FROM DELETED
                     
	IF (@NE_NumNEAddedToSample <> @OLDNE_NumNEAddedToSample) OR 
	   (@OLDNE_NumNEAddedToSample IS NULL AND @NE_NumNEAddedToSample IS NOT NULL) OR
	   (@OLDNE_NumNEAddedToSample IS NOT NULL AND @NE_NumNEAddedToSample IS NULL)
	BEGIN
		INSERT INTO tblSCSTrackingGrade
			(fldProjectID, ID, fldID, tDate, CurrVal, PreVal, WhoChanged)
		VALUES
			(@fldProjectID, @ID, 'NE_NumNEAddedToSample', getdate(),@NE_NumNEAddedToSample,@OLDNE_NumNEAddedToSample,@UPDATEdByUID)
	END 
END

IF UPDATE(CreatingGroupC)
BEGIN
   SELECT @CreatingGroupCYN=CreatingGroupC FROM INSERTED
   SELECT @old_CreatingGroupCYN=CreatingGroupC FROM DELETED
   IF @CreatingGroupCYN=1 and not exists (SELECT * FROM tblAIFSession WHERE ID= @ID and fldSubGroup='C')
   BEGIN
		SELECT @SESSID=sessid FROM tblSCSSession WHERE ID=@ID and fldProjectID=754
		INSERT INTO tblAIFSession(fldProjectID, ID, SessID, SessionType, fldSubGroup, UPDATEdDT, UPDATEdByUID)
		VALUES(@fldProjectID, @ID, @SESSID, 1, 'C', GETDATE(), 9999)
   END 
   IF @CreatingGroupCYN=2 and @old_CreatingGroupCYN=1 
   BEGIN
      SELECT @AIFID_Bgroup=fldAIFID FROM tblAIFSession WHERE ID=@ID and fldSubGroup='B' and fldProjectID=754
      SELECT @AIFID_Cgroup=fldAIFID FROM tblAIFSession WHERE ID=@ID and fldSubGroup='C' and fldProjectID=754
      UPDATE tblSDSStudents set fldAIFID=@AIFID_Bgroup WHERE fldAIFID=@AIFID_Cgroup and fldProjectID=754
      DELETE FROM tblAIFSession WHERE ID=@ID and fldSubGroup='C'
   END
END

IF UPDATE(PL_CertifyName)
BEGIN
  SELECT @old_CertifyName=PL_certifyname FROM DELETED 
  SELECT @new_CertifyName=PL_certifyname FROM INSERTED
  IF (@old_CertifyName='' or @old_CertifyName is null)  and @new_CertifyName<>''
  BEGIN
     UPDATE tblSCSGrade set PL_CertifyDate=GETDATE() WHERE ID=@ID
  END
END

IF UPDATE(AssessmentCompleteFlag)
BEGIN
	SELECT @AssessmentCompleteFlag = AssessmentCompleteFlag FROM INSERTED
	
	IF	@AssessmentCompleteFlag = 1
	BEGIN
		EXEC dbo.NAEP_BarCodes_UPDATEAdminCode @fldProjectID, @ID, 52 
		EXEC usp_ProvideFeedback_Notification @fldProjectID, @ID
	END
END

IF UPDATE(DestroyredenvelopeYN)
BEGIN
	SELECT @DestroyredenvelopeYN = DestroyredenvelopeYN FROM INSERTED
	
	IF	@DestroyredenvelopeYN = 1 
	BEGIN
		UPDATE	tblSCSGrade 
		   SET	NotDestroyredenvelopereason = NULL
		 WHERE	fldProjectID = @fldProjectID AND
				ID = @ID  
	END
	ELSE IF @DestroyredenvelopeYN = 0			
	BEGIN
		UPDATE	tblSCSGrade 
		   SET	destroyredenvelopeDT = NULL
		 WHERE	fldProjectID = @fldProjectID AND
				ID = @ID  
	END
END

RETURN
         
error:
RAISERROR (@errmsg,16,1)
Rollback








