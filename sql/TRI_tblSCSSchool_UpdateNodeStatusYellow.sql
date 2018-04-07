ALTER TRIGGER [dbo].[TRI_tblSCSSchool_UpdateNodeStatusYellow] ON [dbo].[tblSCSSchool] 
AFTER UPDATE
AS 
DECLARE	@Frame_n_ VARCHAR(6),
		@UpdatedByUID int,
		@SDCF_flag int,
		@DateSCReviewCompleted datetime,
		@SchoolName_N varchar(60),
		@SchoolName_O varchar(60),		
		@PrincPrefix_N varchar(8),
		@PrincPrefix_O varchar(8),
		@PrinFName_N varchar(30),
		@PrinFName_O varchar(30),
		@PrinLName_N varchar(30),
		@PrinLName_O varchar(30),
		@PrinSuffix_N varchar(8),
		@PrinSuffix_O varchar(8),
		@Address1_N varchar(50),
		@Address1_O varchar(50),
		@Address2_N varchar(50),
		@Address2_O varchar(50),
		@City_N varchar(30),
		@City_O varchar(30),
		@State_N varchar(2),
		@State_O varchar(2),
		@Zip_N varchar(10),
		@Zip_O varchar(10),
		@enrollgr4_N numeric(18, 0),
		@enrollgr4_O numeric(18, 0),
		@enrollgr8_N numeric(18, 0),
		@enrollgr8_O numeric(18, 0),
		@enrollgr12_N numeric(18, 0),
		@enrollgr12_O numeric(18, 0),
		@CoordPrefix_N varchar(8),
		@CoordPrefix_O varchar(8),
		@CoordFName_N varchar(30),
		@CoordFName_O varchar(30),
		@CoordLName_N varchar(30),
		@CoordLName_O varchar(30),
		@CoordSuffix_N varchar(8),
		@CoordSuffix_O varchar(8),
		@CoordTitle_N varchar(50),
		@CoordTitle_O varchar(50),
		@CoordPhone_N varchar(27),
		@CoordPhone_O varchar(27),
		@CoordEmail_N varchar(50),
		@CoordEmail_O varchar(50),
		@AssessAll4thGr_N varchar(3),
		@AssessAll4thGr_O varchar(3),
		@AssessAll8thGr_N varchar(3),
		@AssessAll8thGr_O varchar(3),
		@AssessAll12thGr_N varchar(3),
		@AssessAll12thGr_O varchar(3),
		@YRCalendar4thGrYN_N int,
		@YRCalendar4thGrYN_O int,
		@YRCalendar8thGrYN_N int,
		@YRCalendar8thGrYN_O int,
		@YRCalendar12thGrYN_N int,
		@YRCalendar12thGrYN_O int,
		@YearRoundNumTracks4th_N int,
		@YearRoundNumTracks4th_O int,
		@YearRoundNumTracks8th_N int,
		@YearRoundNumTracks8th_O int,
		@YearRoundNumTracks12th_N int,
		@YearRoundNumTracks12th_O int,
		@PercentOffTrack4th_N numeric(18, 0),
		@PercentOffTrack4th_O numeric(18, 0),
		@PercentOffTrack8th_N numeric(18, 0),
		@PercentOffTrack8th_O numeric(18, 0),
		@PercentOffTrack12th_N numeric(18, 0),
		@PercentOffTrack12th_O numeric(18, 0),
		@YearRound4thAbil_N int,
		@YearRound4thAbil_O int,
		@YearRound8thAbil_N int,
		@YearRound8thAbil_O int,
		@YearRound12thAbil_N int,
		@YearRound12thAbil_O int,
		@YearRound4thReasonOS_N varchar(1024),
		@YearRound4thReasonOS_O varchar(1024),
		@YearRound8thReasonOS_N varchar(1024),
		@YearRound8thReasonOS_O varchar(1024),
		@YearRound12thReasonOS_N varchar(1024),
		@YearRound12thReasonOS_O varchar(1024),
		@FlagCharter4_N smallint,
		@FlagCharter4_O smallint,
		@FlagCharter8_N smallint,
		@FlagCharter8_O smallint,
		@FlagCharter12_N smallint,
		@FlagCharter12_O smallint,
		@SDCF_MailAddr1_N varchar(50),
		@SDCF_MailAddr1_O varchar(50),
		@SDCF_MailAddr2_N varchar(50),
		@SDCF_MailAddr2_O varchar(50),
		@SDCF_MailCity_N varchar(30),
		@SDCF_MailCity_O varchar(30),
		@SDCF_MailState_N char(2),
		@SDCF_MailState_O char(2),
		@SDCF_MailZip_N varchar(10),
		@SDCF_MailZip_O varchar(10),
		@InSessionFirstDT_4_N datetime,
		@InSessionFirstDT_4_O datetime,
		@InSessionFirstDT_8_N datetime,
		@InSessionFirstDT_8_O datetime,
		@InSessionFirstDT_12_N datetime,
		@InSessionFirstDT_12_O datetime,
		@LastDayofSchool_4_N datetime,
		@LastDayofSchool_4_O datetime,
		@LastDayofSchool_8_N datetime,
		@LastDayofSchool_8_O datetime,
		@LastDayofSchool_12_N datetime,
		@LastDayofSchool_12_O datetime,
		@SchoolGoesOnBreakDT_4_N datetime,
		@SchoolGoesOnBreakDT_4_O datetime,
		@SchoolGoesOnBreakDT_8_N datetime,
		@SchoolGoesOnBreakDT_8_O datetime,
		@SchoolGoesOnBreakDT_12_N datetime,
		@SchoolGoesOnBreakDT_12_O datetime,
		@PrinPhone_N varchar(27),
		@PrinPhone_O varchar(27),
		@PrinEmail_N varchar(50),
		@PrinEmail_O varchar(50),
		@CoordFax_N varchar(27),
		@CoordFax_O varchar(27),
		@NodeID_188 int,
		@NodeID_189 int,
		@NodeID_190 int,
		@NodeID_191 int,
		@NodeID_192 int,
		@NodeID_193 int,
		@NodeID_194 int,
		@NAEPyear int,
		@fldLastUpdatedDT DATETIME,
		@SDCFUID int,
		@NODEID int,
		@isPublic tinyint,
		@ID VARCHAR(7),
		@fldProjectID int
DECLARE	@errmsg VARCHAR(1024)
		
SET NOCOUNT ON

SELECT	@Frame_n_ = Frame_n_ FROM INSERTED
SELECT	@SDCF_flag = SDCF_flag FROM INSERTED
SELECT	@DateSCReviewCompleted = DateSCReviewCompleted FROM INSERTED
SELECT	@SDCFUID = ''
SELECT	@NAEPyear = CAST(SUBSTRING(DB_NAME(),9,4) AS int)
SELECT	@fldLastUpdatedDT = GETDATE()
SELECT	@NodeID_188 = 0
SELECT	@NodeID_189 = 0
SELECT	@NodeID_190 = 0
SELECT	@NodeID_191 = 0
SELECT  @NodeID_192 = 0
SELECT	@NodeID_193 = 0
SELECT	@NodeID_194 = 0

SELECT	@isPublic = dbo.tblSCSSchoolTypes.isPublic
  FROM	dbo.tblSCSSchool INNER JOIN
        dbo.tblSCSSchoolTypes ON dbo.tblSCSSchool.Schl_Typ = dbo.tblSCSSchoolTypes.schl_typ
 WHERE	dbo.tblSCSSchool.Frame_n_ = @Frame_n_        

IF	@isPublic = 0
	GOTO LB_return
		
IF	UPDATE(SchoolName) OR -- NodeID = 188	
	UPDATE(Address1) OR
	UPDATE(Address2) OR
	UPDATE(City) OR
	UPDATE(State) OR
	UPDATE(Zip) OR
	
	UPDATE(SDCF_MailAddr1) OR
	UPDATE(SDCF_MailAddr2) OR
	UPDATE(SDCF_MailCity) OR
	UPDATE(SDCF_MailState) OR
	UPDATE(SDCF_MailZip) OR 
	
	UPDATE(PrincPrefix) OR
	UPDATE(PrinFName) OR
	UPDATE(PrinLName) OR
	UPDATE(PrinSuffix) OR
	UPDATE(PrinPhone) OR
	UPDATE(PrinEmail) OR
	
	UPDATE(CoordPrefix) OR
	UPDATE(CoordFName) OR
	UPDATE(CoordLName) OR
	UPDATE(CoordSuffix) OR
	UPDATE(CoordTitle) OR
	UPDATE(CoordPhone) OR
	UPDATE(CoordEmail) OR
	UPDATE(CoordFax) OR

	UPDATE(FlagCharter4) OR -- NodeID = 189
	UPDATE(FlagCharter8) OR
	UPDATE(FlagCharter12) OR
			
	UPDATE(YRCalendar4thGrYN) OR -- NodeID = 190
	UPDATE(YRCalendar8thGrYN) OR
	UPDATE(YRCalendar12thGrYN) OR
	UPDATE(YearRoundNumTracks4th) OR
	UPDATE(YearRoundNumTracks8th) OR
	UPDATE(YearRoundNumTracks12th) OR
	UPDATE(PercentOffTrack4th) OR
	UPDATE(PercentOffTrack8th) OR
	UPDATE(PercentOffTrack12th) OR
	UPDATE(YearRound4thAbil) OR
	UPDATE(YearRound8thAbil) OR
	UPDATE(YearRound12thAbil) OR
	UPDATE(YearRound4thReasonOS) OR
	UPDATE(YearRound8thReasonOS) OR 
	UPDATE(YearRound12thReasonOS) OR
	
	UPDATE(enrollgr4) OR -- NodeID = 191
	UPDATE(enrollgr8) OR
	UPDATE(enrollgr12) OR
	
	UPDATE(SchoolGoesOnBreakDT_4) OR -- 192
	UPDATE(SchoolGoesOnBreakDT_8) OR
	UPDATE(SchoolGoesOnBreakDT_12) OR 
	
	UPDATE(InSessionFirstDT_4) OR -- 193
	UPDATE(InSessionFirstDT_8) OR
	UPDATE(InSessionFirstDT_12) OR
	
	UPDATE(LastDayofSchool_4) OR -- 194
	UPDATE(LastDayofSchool_8) OR
	UPDATE(LastDayofSchool_12) 
BEGIN
	SELECT	@SchoolName_N = SchoolName FROM INSERTED
	SELECT	@SchoolName_O = SchoolName FROM DELETED
	IF	(@SchoolName_N <> @SchoolName_O) OR (@SchoolName_N IS NOT NULL AND @SchoolName_O IS NULL) OR (@SchoolName_N IS NULL AND @SchoolName_O IS NOT NULL)
		SET @NodeID_188 = 1 

	SELECT	@Address1_N = Address1 FROM INSERTED
	SELECT	@Address1_O = Address1 FROM DELETED
	IF	(@Address1_N <> @Address1_O) OR (@Address1_N IS NOT NULL AND @Address1_O IS NULL) OR (@Address1_N IS NULL AND @Address1_O IS NOT NULL)
		SET @NodeID_188 = 1 
				
	SELECT	@Address2_N = Address2 FROM INSERTED
	SELECT	@Address2_O = Address2 FROM DELETED
	IF	(@Address2_N <> @Address2_O) OR (@Address2_N IS NOT NULL AND @Address2_O IS NULL) OR (@Address2_N IS NULL AND @Address2_O IS NOT NULL)
		SET @NodeID_188 = 1 

	SELECT	@City_N = City FROM INSERTED
	SELECT	@City_O = City FROM DELETED
	IF	(@City_N <> @City_O) OR (@City_N IS NOT NULL AND @City_O IS NULL) OR (@City_N IS NULL AND @City_O IS NOT NULL)
		SET @NodeID_188 = 1 
				
	SELECT	@State_N = State FROM INSERTED
	SELECT	@State_O = State FROM DELETED
	IF	(@State_N <> @State_O) OR (@State_N IS NOT NULL AND @State_O IS NULL) OR (@State_N IS NULL AND @State_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@Zip_N = Zip FROM INSERTED
	SELECT	@Zip_O = Zip FROM DELETED
	IF	(@Zip_N <> @Zip_O) OR (@Zip_N IS NOT NULL AND @Zip_O IS NULL) OR (@Zip_N IS NULL AND @Zip_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@SDCF_MailAddr1_N = SDCF_MailAddr1 FROM INSERTED
	SELECT	@SDCF_MailAddr1_O = SDCF_MailAddr1 FROM DELETED
	IF	(@SDCF_MailAddr1_N <> @SDCF_MailAddr1_O) OR (@SDCF_MailAddr1_N IS NOT NULL AND @SDCF_MailAddr1_O IS NULL) OR (@SDCF_MailAddr1_N IS NULL AND @SDCF_MailAddr1_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@SDCF_MailAddr2_N = SDCF_MailAddr2 FROM INSERTED
	SELECT	@SDCF_MailAddr2_O = SDCF_MailAddr2 FROM DELETED
	IF	(@SDCF_MailAddr2_N <> @SDCF_MailAddr2_O) OR (@SDCF_MailAddr2_N IS NOT NULL AND @SDCF_MailAddr2_O IS NULL) OR (@SDCF_MailAddr2_N IS NULL AND @SDCF_MailAddr2_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@SDCF_MailCity_N = SDCF_MailCity FROM INSERTED
	SELECT	@SDCF_MailCity_O = SDCF_MailCity FROM DELETED
	IF	(@SDCF_MailCity_N <> @SDCF_MailCity_O) OR (@SDCF_MailCity_N IS NOT NULL AND @SDCF_MailCity_O IS NULL) OR (@SDCF_MailCity_N IS NULL AND @SDCF_MailCity_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@SDCF_MailState_N = SDCF_MailState FROM INSERTED
	SELECT	@SDCF_MailState_O = SDCF_MailState FROM DELETED
	IF	(@SDCF_MailState_N <> @SDCF_MailState_O) OR (@SDCF_MailState_N IS NOT NULL AND @SDCF_MailState_O IS NULL) OR (@SDCF_MailState_N IS NULL AND @SDCF_MailState_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@SDCF_MailZip_N = SDCF_MailZip FROM INSERTED
	SELECT	@SDCF_MailZip_O = SDCF_MailZip FROM DELETED
	IF	(@SDCF_MailZip_N <> @SDCF_MailZip_O) OR (@SDCF_MailZip_N IS NOT NULL AND @SDCF_MailZip_O IS NULL) OR (@SDCF_MailZip_N IS NULL AND @SDCF_MailZip_O IS NOT NULL)
		SET @NodeID_188 = 1 
	
	SELECT	@PrincPrefix_N = PrincPrefix FROM INSERTED
	SELECT	@PrincPrefix_O = PrincPrefix FROM DELETED
	IF	(@PrincPrefix_N <> @PrincPrefix_O) OR (@PrincPrefix_N IS NOT NULL AND @PrincPrefix_O IS NULL) OR (@PrincPrefix_N IS NULL AND @PrincPrefix_O IS NOT NULL)
		SET @NodeID_188 = 1 
	
	SELECT	@PrinFName_N = PrinFName FROM INSERTED
	SELECT	@PrinFName_O = PrinFName FROM DELETED
	IF	(@PrinFName_N <> @PrinFName_O) OR (@PrinFName_N IS NOT NULL AND @PrinFName_O IS NULL) OR (@PrinFName_N IS NULL AND @PrinFName_O IS NOT NULL)
		SET @NodeID_188 = 1 
	
	SELECT	@PrinLName_N = PrinLName FROM INSERTED
	SELECT	@PrinLName_O = PrinLName FROM DELETED
	IF	(@PrinLName_N <> @PrinLName_O) OR (@PrinLName_N IS NOT NULL AND @PrinLName_O IS NULL) OR (@PrinLName_N IS NULL AND @PrinLName_O IS NOT NULL)
		SET @NodeID_188 = 1 

	SELECT	@PrinSuffix_N = PrinSuffix FROM INSERTED
	SELECT	@PrinSuffix_O = PrinSuffix FROM DELETED
	IF	(@PrinSuffix_N <> @PrinSuffix_O) OR (@PrinSuffix_N IS NOT NULL AND @PrinSuffix_O IS NULL) OR (@PrinSuffix_N IS NULL AND @PrinSuffix_O IS NOT NULL)
		SET @NodeID_188 = 1 

	SELECT	@PrinPhone_N = PrinPhone FROM INSERTED
	SELECT	@PrinPhone_O = PrinPhone FROM DELETED
	IF	(@PrinPhone_N <> @PrinPhone_O) OR (@PrinPhone_N IS NOT NULL AND @PrinPhone_O IS NULL) OR (@PrinPhone_N IS NULL AND @PrinPhone_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@PrinEmail_N = PrinEmail FROM INSERTED
	SELECT	@PrinEmail_O = PrinEmail FROM DELETED
	IF	(@PrinEmail_N <> @PrinEmail_O) OR (@PrinEmail_N IS NOT NULL AND @PrinEmail_O IS NULL) OR (@PrinEmail_N IS NULL AND @PrinEmail_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordPrefix_N = CoordPrefix FROM INSERTED
	SELECT	@CoordPrefix_O = CoordPrefix FROM DELETED
	IF	(@CoordPrefix_N <> @CoordPrefix_O) OR (@CoordPrefix_N IS NOT NULL AND @CoordPrefix_O IS NULL) OR (@CoordPrefix_N IS NULL AND @CoordPrefix_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordFName_N = CoordFName FROM INSERTED
	SELECT	@CoordFName_O = CoordFName FROM DELETED
	IF	(@CoordFName_N <> @CoordFName_O) OR (@CoordFName_N IS NOT NULL AND @CoordFName_O IS NULL) OR (@CoordFName_N IS NULL AND @CoordFName_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordLName_N = CoordLName FROM INSERTED
	SELECT	@CoordLName_O = CoordLName FROM DELETED
	IF	(@CoordLName_N <> @CoordLName_O) OR (@CoordLName_N IS NOT NULL AND @CoordLName_O IS NULL) OR (@CoordLName_N IS NULL AND @CoordLName_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordSuffix_N = CoordSuffix FROM INSERTED
	SELECT	@CoordSuffix_O = CoordSuffix FROM DELETED
	IF	(@CoordSuffix_N <> @CoordSuffix_O) OR (@CoordSuffix_N IS NOT NULL AND @CoordSuffix_O IS NULL) OR (@CoordSuffix_N IS NULL AND @CoordSuffix_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordTitle_N = CoordTitle FROM INSERTED
	SELECT	@CoordTitle_O = CoordTitle FROM DELETED
	IF	(@CoordTitle_N <> @CoordTitle_O) OR (@CoordTitle_N IS NOT NULL AND @CoordTitle_O IS NULL) OR (@CoordTitle_N IS NULL AND @CoordTitle_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordPhone_N = CoordPhone FROM INSERTED
	SELECT	@CoordPhone_O = CoordPhone FROM DELETED
	IF	(@CoordPhone_N <> @CoordPhone_O) OR (@CoordPhone_N IS NOT NULL AND @CoordPhone_O IS NULL) OR (@CoordPhone_N IS NULL AND @CoordPhone_O IS NOT NULL)
		SET @NodeID_188 = 1 
		
	SELECT	@CoordEmail_N = CoordEmail FROM INSERTED
	SELECT	@CoordEmail_O = CoordEmail FROM DELETED
	IF	(@CoordEmail_N <> @CoordEmail_O) OR (@CoordEmail_N IS NOT NULL AND @CoordEmail_O IS NULL) OR (@CoordEmail_N IS NULL AND @CoordEmail_O IS NOT NULL)
		SET @NodeID_188 = 1 
	
	SELECT	@CoordFax_N = CoordFax FROM INSERTED
	SELECT	@CoordFax_O = CoordFax FROM DELETED
	IF	(@CoordFax_N <> @CoordFax_O) OR (@CoordFax_N IS NOT NULL AND @CoordFax_O IS NULL) OR (@CoordFax_N IS NULL AND @CoordFax_O IS NOT NULL)
		SET @NodeID_188 = 1 

	-- NodeID = 189
	SELECT	@FlagCharter4_N = FlagCharter4 FROM INSERTED
	SELECT	@FlagCharter4_O = FlagCharter4 FROM DELETED
	IF	(@FlagCharter4_N <> @FlagCharter4_O) OR (@FlagCharter4_N IS NOT NULL AND @FlagCharter4_O IS NULL) OR (@FlagCharter4_N IS NULL AND @FlagCharter4_O IS NOT NULL)
		SET @NodeID_189 = 1 
		
	SELECT	@FlagCharter8_N = FlagCharter8 FROM INSERTED
	SELECT	@FlagCharter8_O = FlagCharter8 FROM DELETED
	IF	(@FlagCharter8_N <> @FlagCharter8_O) OR (@FlagCharter8_N IS NOT NULL AND @FlagCharter8_O IS NULL) OR (@FlagCharter8_N IS NULL AND @FlagCharter8_O IS NOT NULL)
		SET @NodeID_189 = 2 
		
	SELECT	@FlagCharter12_N = FlagCharter12 FROM INSERTED
	SELECT	@FlagCharter12_O = FlagCharter12 FROM DELETED
	IF	(@FlagCharter12_N <> @FlagCharter12_O) OR (@FlagCharter12_N IS NOT NULL AND @FlagCharter12_O IS NULL) OR (@FlagCharter12_N IS NULL AND @FlagCharter12_O IS NOT NULL)
		SET @NodeID_189 = 3 

	-- 	NodeID = 190
	SELECT	@YRCalendar4thGrYN_N = YRCalendar4thGrYN FROM INSERTED
	SELECT	@YRCalendar4thGrYN_O = YRCalendar4thGrYN FROM DELETED
	IF	(@YRCalendar4thGrYN_N <> @YRCalendar4thGrYN_O) OR (@YRCalendar4thGrYN_N IS NOT NULL AND @YRCalendar4thGrYN_O IS NULL) OR (@YRCalendar4thGrYN_N IS NULL AND @YRCalendar4thGrYN_O IS NOT NULL)
		SET @NodeID_190 = 1
		
	SELECT	@YRCalendar8thGrYN_N = YRCalendar8thGrYN FROM INSERTED
	SELECT	@YRCalendar8thGrYN_O = YRCalendar8thGrYN FROM DELETED
	IF	(@YRCalendar8thGrYN_N <> @YRCalendar8thGrYN_O) OR (@YRCalendar8thGrYN_N IS NOT NULL AND @YRCalendar8thGrYN_O IS NULL) OR (@YRCalendar8thGrYN_N IS NULL AND @YRCalendar8thGrYN_O IS NOT NULL)
		SET @NodeID_190 = 2
		
	SELECT	@YRCalendar12thGrYN_N = YRCalendar12thGrYN FROM INSERTED
	SELECT	@YRCalendar12thGrYN_O = YRCalendar12thGrYN FROM DELETED
	IF	(@YRCalendar12thGrYN_N <> @YRCalendar12thGrYN_O) OR (@YRCalendar12thGrYN_N IS NOT NULL AND @YRCalendar12thGrYN_O IS NULL) OR (@YRCalendar12thGrYN_N IS NULL AND @YRCalendar12thGrYN_O IS NOT NULL)
		SET @NodeID_190 = 3
		
	SELECT	@YearRoundNumTracks4th_N = YearRoundNumTracks4th FROM INSERTED
	SELECT	@YearRoundNumTracks4th_O = YearRoundNumTracks4th FROM DELETED
	IF	(@YearRoundNumTracks4th_N <> @YearRoundNumTracks4th_O) OR (@YearRoundNumTracks4th_N IS NOT NULL AND @YearRoundNumTracks4th_O IS NULL) OR (@YearRoundNumTracks4th_N IS NULL AND @YearRoundNumTracks4th_O IS NOT NULL)
		SET @NodeID_190 = 1
		
	SELECT	@YearRoundNumTracks8th_N = YearRoundNumTracks8th FROM INSERTED
	SELECT	@YearRoundNumTracks8th_O = YearRoundNumTracks8th FROM DELETED
	IF	(@YearRoundNumTracks8th_N <> @YearRoundNumTracks8th_O) OR (@YearRoundNumTracks8th_N IS NOT NULL AND @YearRoundNumTracks8th_O IS NULL) OR (@YearRoundNumTracks8th_N IS NULL AND @YearRoundNumTracks8th_O IS NOT NULL)
		SET @NodeID_190 = 2
		
	SELECT	@YearRoundNumTracks12th_N = YearRoundNumTracks12th FROM INSERTED
	SELECT	@YearRoundNumTracks12th_O = YearRoundNumTracks12th FROM DELETED
	IF	(@YearRoundNumTracks12th_N <> @YearRoundNumTracks12th_O) OR (@YearRoundNumTracks12th_N IS NOT NULL AND @YearRoundNumTracks12th_O IS NULL) OR (@YearRoundNumTracks12th_N IS NULL AND @YearRoundNumTracks12th_O IS NOT NULL)
		SET @NodeID_190 = 3
	
	SELECT	@PercentOffTrack4th_N = PercentOffTrack4th FROM INSERTED
	SELECT	@PercentOffTrack4th_O = PercentOffTrack4th FROM DELETED
	IF	(@PercentOffTrack4th_N <> @PercentOffTrack4th_O) OR (@PercentOffTrack4th_N IS NOT NULL AND @PercentOffTrack4th_O IS NULL) OR (@PercentOffTrack4th_N IS NULL AND @PercentOffTrack4th_O IS NOT NULL)
		SET @NodeID_190 = 1
		
	SELECT	@PercentOffTrack8th_N = PercentOffTrack8th FROM INSERTED
	SELECT	@PercentOffTrack8th_O = PercentOffTrack8th FROM DELETED
	IF	(@PercentOffTrack8th_N <> @PercentOffTrack8th_O) OR (@PercentOffTrack8th_N IS NOT NULL AND @PercentOffTrack8th_O IS NULL) OR (@PercentOffTrack8th_N IS NULL AND @PercentOffTrack8th_O IS NOT NULL)
		SET @NodeID_190 = 2
		
	SELECT	@PercentOffTrack12th_N = PercentOffTrack12th FROM INSERTED
	SELECT	@PercentOffTrack12th_O = PercentOffTrack12th FROM DELETED
	IF	(@PercentOffTrack12th_N <> @PercentOffTrack12th_O) OR (@PercentOffTrack12th_N IS NOT NULL AND @PercentOffTrack12th_O IS NULL) OR (@PercentOffTrack12th_N IS NULL AND @PercentOffTrack12th_O IS NOT NULL)
		SET @NodeID_190 = 3
		
	SELECT	@YearRound4thAbil_N = YearRound4thAbil FROM INSERTED
	SELECT	@YearRound4thAbil_O = YearRound4thAbil FROM DELETED
	IF	(@YearRound4thAbil_N <> @YearRound4thAbil_O) OR (@YearRound4thAbil_N IS NOT NULL AND @YearRound4thAbil_O IS NULL) OR (@YearRound4thAbil_N IS NULL AND @YearRound4thAbil_O IS NOT NULL)
		SET @NodeID_190 = 1

	SELECT	@YearRound8thAbil_N = YearRound8thAbil FROM INSERTED
	SELECT	@YearRound8thAbil_O = YearRound8thAbil FROM DELETED
	IF	(@YearRound8thAbil_N <> @YearRound8thAbil_O) OR (@YearRound8thAbil_N IS NOT NULL AND @YearRound8thAbil_O IS NULL) OR (@YearRound8thAbil_N IS NULL AND @YearRound8thAbil_O IS NOT NULL)
		SET @NodeID_190 = 2
		
	SELECT	@YearRound12thAbil_N = YearRound12thAbil FROM INSERTED
	SELECT	@YearRound12thAbil_O = YearRound12thAbil FROM DELETED
	IF	(@YearRound12thAbil_N <> @YearRound12thAbil_O) OR (@YearRound12thAbil_N IS NOT NULL AND @YearRound12thAbil_O IS NULL) OR (@YearRound12thAbil_N IS NULL AND @YearRound12thAbil_O IS NOT NULL)
		SET @NodeID_190 = 3
		
	SELECT	@YearRound4thReasonOS_N = YearRound4thReasonOS FROM INSERTED
	SELECT	@YearRound4thReasonOS_O = YearRound4thReasonOS FROM DELETED
	IF	(@YearRound4thReasonOS_N <> @YearRound4thReasonOS_O) OR (@YearRound4thReasonOS_N IS NOT NULL AND @YearRound4thReasonOS_O IS NULL) OR (@YearRound4thReasonOS_N IS NULL AND @YearRound4thReasonOS_O IS NOT NULL)
		SET @NodeID_190 = 1
	
	SELECT	@YearRound8thReasonOS_N = YearRound8thReasonOS FROM INSERTED
	SELECT	@YearRound8thReasonOS_O = YearRound8thReasonOS FROM DELETED
	IF (@YearRound8thReasonOS_N <> @YearRound8thReasonOS_O) OR (@YearRound8thReasonOS_N IS NOT NULL AND @YearRound8thReasonOS_O IS NULL) OR (@YearRound8thReasonOS_N IS NULL AND @YearRound8thReasonOS_O IS NOT NULL)
		SET @NodeID_190 = 2
		
	SELECT	@YearRound12thReasonOS_N = YearRound12thReasonOS FROM INSERTED
	SELECT	@YearRound12thReasonOS_O = YearRound12thReasonOS FROM DELETED
	IF	(@YearRound12thReasonOS_N <> @YearRound12thReasonOS_O) OR (@YearRound12thReasonOS_N IS NOT NULL AND @YearRound12thReasonOS_O IS NULL) OR (@YearRound12thReasonOS_N IS NULL AND @YearRound12thReasonOS_O IS NOT NULL)
		SET @NodeID_190 = 3

	-- NodeID = 191	
	SELECT	@enrollgr4_N = enrollgr4 FROM INSERTED
	SELECT	@enrollgr4_O = enrollgr4 FROM DELETED
	IF	(@enrollgr4_N <> @enrollgr4_O) OR (@enrollgr4_N IS NOT NULL AND @enrollgr4_O IS NULL) OR (@enrollgr4_N IS NULL AND @enrollgr4_O IS NOT NULL)
		SET @NodeID_191 = 1 
	
	SELECT	@enrollgr8_N = enrollgr8 FROM INSERTED
	SELECT	@enrollgr8_O = enrollgr8 FROM DELETED
	IF	(@enrollgr8_N <> @enrollgr8_O) OR (@enrollgr8_N IS NOT NULL AND @enrollgr8_O IS NULL) OR (@enrollgr8_N IS NULL AND @enrollgr8_O IS NOT NULL)
		SET @NodeID_191 = 2 
						
	SELECT	@enrollgr12_N = enrollgr12 FROM INSERTED
	SELECT	@enrollgr12_O = enrollgr12 FROM DELETED
	IF	(@enrollgr12_N <> @enrollgr12_O) OR (@enrollgr12_N IS NOT NULL AND @enrollgr12_O IS NULL) OR (@enrollgr12_N IS NULL AND @enrollgr12_O IS NOT NULL)
		SET @NodeID_191 = 3 
			
	-- NodeID = 192
	SELECT	@SchoolGoesOnBreakDT_4_N = SchoolGoesOnBreakDT_4 FROM INSERTED
	SELECT	@SchoolGoesOnBreakDT_4_O = SchoolGoesOnBreakDT_4 FROM DELETED
	IF	(@SchoolGoesOnBreakDT_4_N <> @SchoolGoesOnBreakDT_4_O) OR (@SchoolGoesOnBreakDT_4_N IS NOT NULL AND @SchoolGoesOnBreakDT_4_O IS NULL) OR (@SchoolGoesOnBreakDT_4_N IS NULL AND @SchoolGoesOnBreakDT_4_O IS NOT NULL)
		SET @NodeID_192 = 1 
		
	SELECT	@SchoolGoesOnBreakDT_8_N = SchoolGoesOnBreakDT_8 FROM INSERTED
	SELECT	@SchoolGoesOnBreakDT_8_O = SchoolGoesOnBreakDT_8 FROM DELETED
	IF	(@SchoolGoesOnBreakDT_8_N <> @SchoolGoesOnBreakDT_8_O) OR (@SchoolGoesOnBreakDT_8_N IS NOT NULL AND @SchoolGoesOnBreakDT_8_O IS NULL) OR (@SchoolGoesOnBreakDT_8_N IS NULL AND @SchoolGoesOnBreakDT_8_O IS NOT NULL)
		SET @NodeID_192 = 2 
		
	SELECT	@SchoolGoesOnBreakDT_12_N = SchoolGoesOnBreakDT_12 FROM INSERTED
	SELECT	@SchoolGoesOnBreakDT_12_O = SchoolGoesOnBreakDT_12 FROM DELETED
	IF	(@SchoolGoesOnBreakDT_12_N <> @SchoolGoesOnBreakDT_12_O) OR (@SchoolGoesOnBreakDT_12_N IS NOT NULL AND @SchoolGoesOnBreakDT_12_O IS NULL) OR (@SchoolGoesOnBreakDT_12_N IS NULL AND @SchoolGoesOnBreakDT_12_O IS NOT NULL)
		SET @NodeID_192 = 3 
		
	-- NodeID = 193
	SELECT	@InSessionFirstDT_4_N = InSessionFirstDT_4 FROM INSERTED
	SELECT	@InSessionFirstDT_4_O = InSessionFirstDT_4 FROM DELETED
	IF	(@InSessionFirstDT_4_N <> @InSessionFirstDT_4_O) OR (@InSessionFirstDT_4_N IS NOT NULL AND @InSessionFirstDT_4_O IS NULL) OR (@InSessionFirstDT_4_N IS NULL AND @InSessionFirstDT_4_O IS NOT NULL)
		SET @NodeID_193 = 1 
		
	SELECT	@InSessionFirstDT_8_N = InSessionFirstDT_8 FROM INSERTED
	SELECT	@InSessionFirstDT_8_O = InSessionFirstDT_8 FROM DELETED
	IF	(@InSessionFirstDT_8_N <> @InSessionFirstDT_8_O) OR (@InSessionFirstDT_8_N IS NOT NULL AND @InSessionFirstDT_8_O IS NULL) OR (@InSessionFirstDT_8_N IS NULL AND @InSessionFirstDT_8_O IS NOT NULL)
		SET @NodeID_193 = 2 
		
	SELECT	@InSessionFirstDT_12_N = InSessionFirstDT_12 FROM INSERTED
	SELECT	@InSessionFirstDT_12_O = InSessionFirstDT_12 FROM DELETED
	IF	(@InSessionFirstDT_12_N <> @InSessionFirstDT_12_O) OR (@InSessionFirstDT_12_N IS NOT NULL AND @InSessionFirstDT_12_O IS NULL) OR (@InSessionFirstDT_12_N IS NULL AND @InSessionFirstDT_12_O IS NOT NULL)
		SET @NodeID_193 = 3 
		
	-- NodeID = 194		
	SELECT	@LastDayofSchool_4_N = LastDayofSchool_4 FROM INSERTED
	SELECT	@LastDayofSchool_4_O = LastDayofSchool_4 FROM DELETED
	IF	(@LastDayofSchool_4_N <> @LastDayofSchool_4_O) OR (@LastDayofSchool_4_N IS NOT NULL AND @LastDayofSchool_4_O IS NULL) OR (@LastDayofSchool_4_N IS NULL AND @LastDayofSchool_4_O IS NOT NULL)
		SET @NodeID_194 = 1 
		
	SELECT	@LastDayofSchool_8_N = LastDayofSchool_8 FROM INSERTED
	SELECT	@LastDayofSchool_8_O = LastDayofSchool_8 FROM DELETED
	IF	(@LastDayofSchool_8_N <> @LastDayofSchool_8_O) OR (@LastDayofSchool_8_N IS NOT NULL AND @LastDayofSchool_8_O IS NULL) OR (@LastDayofSchool_8_N IS NULL AND @LastDayofSchool_8_O IS NOT NULL)
		SET @NodeID_194 = 2 
		
	SELECT	@LastDayofSchool_12_N = LastDayofSchool_12 FROM INSERTED
	SELECT	@LastDayofSchool_12_O = LastDayofSchool_12 FROM DELETED
	IF	(@LastDayofSchool_12_N <> @LastDayofSchool_12_O) OR (@LastDayofSchool_12_N IS NOT NULL AND @LastDayofSchool_12_O IS NULL) OR (@LastDayofSchool_12_N IS NULL AND @LastDayofSchool_12_O IS NOT NULL)
		SET @NodeID_194 = 3 
	
	IF	@NodeID_188 = 1 
	BEGIN
		PRINT	'188'
		SELECT	@NODEID = 188
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID WHERE NODEID = @NODEID AND fldProjectID = 0	AND ID = @Frame_n_ AND fldNodeStatus = 3 AND NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,0,@Frame_n_,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END
	
	IF	@NodeID_189 IN (1,2,3)
	BEGIN
		PRINT	'189'
		SELECT	@NODEID = 189
		SELECT	@fldProjectID = fldProjectID, @ID = ID FROM tblSCSGrade WHERE Frame_N_ = @Frame_n_ AND SUBSTRING(ID,3,1) = @NodeID_189
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID A INNER JOIN tblSCSGrade B ON A.fldProjecTID = B.fldProjecTID AND A.ID = B.ID WHERE A.NODEID = @NODEID AND B.ID = @ID AND A.fldNodeStatus = 3 AND A.NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,@fldProjectID,@ID,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END
		
	IF	@NodeID_190 IN (1,2,3)
	BEGIN
		PRINT	'190'
		SELECT	@NODEID = 190
		SELECT	@fldProjectID = fldProjectID, @ID = ID FROM tblSCSGrade WHERE Frame_N_ = @Frame_n_ AND SUBSTRING(ID,3,1) = @NodeID_190
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID A INNER JOIN tblSCSGrade B ON A.fldProjecTID = B.fldProjecTID AND A.ID = B.ID WHERE A.NODEID = @NODEID AND B.ID = @ID AND A.fldNodeStatus = 3 AND A.NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,@fldProjectID,@ID,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END
	
	IF	@NodeID_191 IN (1,2,3)
	BEGIN
		PRINT	'191'
		SELECT	@NODEID = 191
		SELECT	@fldProjectID = fldProjectID, @ID = ID FROM tblSCSGrade WHERE Frame_N_ = @Frame_n_ AND SUBSTRING(ID,3,1) = @NodeID_191
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID A INNER JOIN tblSCSGrade B ON A.fldProjecTID = B.fldProjecTID AND A.ID = B.ID WHERE A.NODEID = @NODEID AND B.ID = @ID AND A.fldNodeStatus = 3 AND A.NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,@fldProjectID,@ID,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END
	
	IF	@NodeID_192 IN (1,2,3)
	BEGIN
		PRINT	'192'
		SELECT	@NODEID = 192
		SELECT	@fldProjectID = fldProjectID, @ID = ID FROM tblSCSGrade WHERE Frame_N_ = @Frame_n_ AND SUBSTRING(ID,3,1) = @NodeID_192
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID A INNER JOIN tblSCSGrade B ON A.fldProjecTID = B.fldProjecTID AND A.ID = B.ID WHERE A.NODEID = @NODEID AND B.ID = @ID AND A.fldNodeStatus = 3 AND A.NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,@fldProjectID,@ID,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END
	
	IF	@NodeID_193 IN (1,2,3)
	BEGIN
		PRINT	'193'
		SELECT	@NODEID = 193
		SELECT	@fldProjectID = fldProjectID, @ID = ID FROM tblSCSGrade WHERE Frame_N_ = @Frame_n_ AND SUBSTRING(ID,3,1) = @NodeID_193
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID A INNER JOIN tblSCSGrade B ON A.fldProjecTID = B.fldProjecTID AND A.ID = B.ID WHERE A.NODEID = @NODEID AND B.ID = @ID AND A.fldNodeStatus = 3 AND A.NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,@fldProjectID,@ID,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END
	
	IF	@NodeID_194 IN (1,2,3)
	BEGIN
		PRINT	'194'
		SELECT	@NODEID = 194
		SELECT	@fldProjectID = fldProjectID, @ID = ID FROM tblSCSGrade WHERE Frame_N_ = @Frame_n_ AND SUBSTRING(ID,3,1) = @NodeID_194
		IF EXISTS(SELECT * FROM	dbo.ExAdmin_TaskManagerByID A INNER JOIN tblSCSGrade B ON A.fldProjecTID = B.fldProjecTID AND A.ID = B.ID WHERE A.NODEID = @NODEID AND B.ID = @ID AND A.fldNodeStatus = 3 AND A.NAEPYear = @NAEPyear)
		BEGIN
			EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus @NODEID,@fldProjectID,@ID,2,@fldLastUpdatedDT,@SDCFUID,@NAEPyear
		END
	END			
			
	IF	@NodeID_188 > 0 OR @NodeID_189 > 0 OR @NodeID_190 > 0 OR @NodeID_191 > 0 OR @NodeID_192 > 0 OR @NodeID_193 > 0 OR @NodeID_194 > 0
	BEGIN
		UPDATE	tblSCSSchool
		   SET	SDCF_flag = 1,
				DateSCReviewCompleted = NULL
		 WHERE	Frame_n_ = @Frame_n_  			
	END
END

LB_return:
return
         
LB_error:
RAISERROR (@errmsg,16,1)
Rollback
