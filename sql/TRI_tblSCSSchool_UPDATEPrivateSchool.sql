
ALTER TRIGGER [dbo].[TRI_tblSCSSchool_UPDATEPrivateSchool] ON [dbo].[tblSCSSchool] 
AFTER UPDATE
AS 
DECLARE	@Frame_n_ VARCHAR(6),
        @UPDATEdByUID int,
        @SDCF_flag int,
        @DateSCReviewCompleted datetime,
        @SchoolName_N varchar(60),    
        @PrincPrefix_N varchar(8),
        @PrinFName_N varchar(30),
        @PrinLName_N varchar(30),
        @PrinSuffix_N varchar(8),
        @Address1_N varchar(50),
        @Address2_N varchar(50),
        @City_N varchar(30),
        @State_N varchar(2),
		@Zip_N varchar(10),
		@enrollgr4_N numeric(18, 0),
		@enrollgr8_N numeric(18, 0),
		@enrollgr12_N numeric(18, 0),
		@CoordPrefix_N varchar(8),
		@CoordFName_N varchar(30),
		@CoordLName_N varchar(30),
		@CoordSuffix_N varchar(8),
		@CoordTitle_N varchar(50),
		@CoordPhone_N varchar(27),
		@CoordEmail_N varchar(50),
		@AssessAll4thGr_N varchar(3),
		@AssessAll8thGr_N varchar(3),
		@AssessAll12thGr_N varchar(3),
		@YRCalendar4thGrYN_N int,
		@YRCalendar8thGrYN_N int,
		@YRCalendar12thGrYN_N int,
		@YearRoundNumTracks4th_N int,
		@YearRoundNumTracks8th_N int,
		@YearRoundNumTracks12th_N int,
		@PercentOffTrack4th_N numeric(18, 0),
		@PercentOffTrack8th_N numeric(18, 0),
		@PercentOffTrack12th_N numeric(18, 0),
		@YearRound4thAbil_N int,
		@YearRound8thAbil_N int,
		@YearRound12thAbil_N int,
		@YearRound4thReasonOS_N varchar(1024),
		@YearRound8thReasonOS_N varchar(1024),
		@YearRound12thReasonOS_N varchar(1024),
		@FlagCharter4_N smallint,
		@FlagCharter8_N smallint,
		@FlagCharter12_N smallint,
		@SDCF_MailAddr1_N varchar(50),
		@SDCF_MailAddr2_N varchar(50),
		@SDCF_MailCity_N varchar(30),
		@SDCF_MailState_N char(2),
		@SDCF_MailZip_N varchar(10),
		@InSessionFirstDT_4_N datetime,
		@InSessionFirstDT_8_N datetime,
		@InSessionFirstDT_12_N datetime,
		@LastDayofSchool_4_N datetime,
		@LastDayofSchool_8_N datetime,
		@LastDayofSchool_12_N datetime,
		@SchoolGoesOnBreakDT_4_N datetime,
		@SchoolGoesOnBreakDT_8_N datetime,
		@SchoolGoesOnBreakDT_12_N datetime,
		@PrinPhone_N varchar(27),
		@PrinEmail_N varchar(50),
		@CoordFax_N varchar(27),
		@NAEPyear int,
		@fldLastUPDATEdDT DATETIME,
		@SDCFUID int,
		@NODEID int,
		@ISPublic tinyint,
		@ID4 VARCHAR(7),
		@ID8 VARCHAR(7),
		@ID12 VARCHAR(7),
		@fldProjectID int, 
		@PID int,
		@PrincipalID int,
		@SCID int
DECLARE @errmsg VARCHAR(1024)
            
SET NOCOUNT ON

SELECT	@Frame_n_ = Frame_n_ FROM INSERTED

SELECT  @ISPublic = dbo.tblSCSSchoolTypes.ISPublic
  FROM  dbo.tblSCSSchool INNER JOIN
		dbo.tblSCSSchoolTypes ON dbo.tblSCSSchool.Schl_Typ = dbo.tblSCSSchoolTypes.schl_typ
 WHERE  dbo.tblSCSSchool.Frame_n_ = @Frame_n_        

SELECT	@SDCF_flag = SDCF_flag FROM INSERTED
SELECT	@DateSCReviewCompleted = DateSCReviewCompleted FROM INSERTED
SELECT	@SDCFUID = ''
SELECT	@NAEPyear = CAST(SUBSTRING(DB_NAME(),9,4) AS int)
SELECT	@fldLastUPDATEdDT = GETDATE()

SELECT	@ID4 = ID FROM dbo.tblSCSGrade WHERE frame_n_= @Frame_n_ AND SUBSTRING(ID,3,1) = '1'
SELECT	@ID8 = ID FROM dbo.tblSCSGrade WHERE frame_n_= @Frame_n_ AND SUBSTRING(ID,3,1) = '2'
SELECT	@ID12 = ID FROM dbo.tblSCSGrade WHERE frame_n_= @Frame_n_ AND SUBSTRING(ID,3,1) = '3'

-- 14/6/16: IV 7279
IF	UPDATE(PrincPrefix) OR
	UPDATE(PrinFName) OR
	UPDATE(PrinLName) OR
	UPDATE(PrinSuffix) OR
	UPDATE(PrinPhone) OR
	UPDATE(PrinEmail)
BEGIN
    SELECT @PrincipalID = PrincipalID FROM tblSCSSchool WHERE frame_n_ = @Frame_n_
   
	IF UPDATE(PrincPrefix) 
	BEGIN
		SELECT @PrincPrefix_N = PrincPrefix FROM INSERTED
		IF @PrincipalID IS NOT NULL AND @PrincipalID <> ''
			UPDATE tblSCSPersonnel SET prefix = @PrincPrefix_N WHERE pID = @PrincipalID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(prefix, title, frame_n_) VALUES (@PrincPrefix_N , 'Principal', @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET PrincipalID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	
	IF UPDATE(PrinFName) 
	BEGIN
		SELECT @PrinFName_N = PrinFName FROM INSERTED
		IF @PrincipalID IS NOT NULL AND @PrincipalID <> ''
			UPDATE tblSCSPersonnel SET fname = @PrinFName_N WHERE pID = @PrincipalID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(fname, title, frame_n_) VALUES (@PrinFName_N , 'Principal', @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET PrincipalID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	              
	IF UPDATE(PrinLName) 
	BEGIN
		SELECT @PrinLName_N = PrinLName FROM INSERTED
		IF @PrincipalID IS NOT NULL AND @PrincipalID <> ''
			UPDATE tblSCSPersonnel SET lname = @PrinLName_N WHERE pID = @PrincipalID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(Lname, title, frame_n_) VALUES (@PrinLName_N , 'Principal', @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET PrincipalID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	              
	IF UPDATE(PrinPhone) 
	BEGIN
		SELECT @PrinPhone_N = PrinPhone FROM INSERTED
		IF @PrincipalID IS NOT NULL AND @PrincipalID <> ''
			UPDATE tblSCSPersonnel SET phone = @PrinPhone_N WHERE pID = @PrincipalID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(phone, title, frame_n_) VALUES (@PrinPhone_N , 'Principal', @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET PrincipalID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	 
	IF UPDATE(PrinEmail) 
	BEGIN
		SELECT @PrinEmail_N = PrinEmail FROM INSERTED
		IF @PrincipalID IS NOT NULL AND @PrincipalID <> ''
			UPDATE tblSCSPersonnel SET email = @PrinEmail_N WHERE pID = @PrincipalID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(email, title, frame_n_) VALUES (@PrinEmail_N , 'Principal', @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET PrincipalID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
END
      
IF	UPDATE(CoordPrefix) OR
	UPDATE(CoordFName) OR
	UPDATE(CoordLName) OR
	UPDATE(CoordTitle) OR
	UPDATE(CoordPhone) OR
	UPDATE(CoordEmail) OR
	UPDATE(CoordFax) 
BEGIN
	SELECT @SCID=CoordinatorID FROM tblSCSSchool WHERE frame_n_=@Frame_n_
   
	IF UPDATE(CoordPrefix) 
	BEGIN
		SELECT @CoordPrefix_N = CoordPrefix FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET prefix = @CoordPrefix_N WHERE pID = @SCID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(prefix, frame_n_) VALUES (@CoordPrefix_N, @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
		
	IF UPDATE(CoordFName) 
	BEGIN
		SELECT @CoordFName_N = CoordFName FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET fname = @CoordFName_N WHERE pID=@SCID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(fname, frame_n_) VALUES (@CoordFName_N, @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
		
	IF UPDATE(CoordLName) 
	BEGIN
		SELECT @CoordLName_N = CoordLName FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET Lname = @CoordLName_N WHERE pID=@SCID
		ELSE
        BEGIN
			INSERT INTO tblSCSPersonnel(Lname,  frame_n_) VALUES (@CoordLName_N, @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	
	IF UPDATE(CoordTitle) 
	BEGIN
		SELECT @CoordTitle_N = CoordTitle FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET title = @CoordTitle_N WHERE pID=@SCID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(title, frame_n_) VALUES (@CoordTitle_N , @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END

	IF UPDATE(CoordPhone) 
    BEGIN  
		SELECT @CoordPhone_N = CoordPhone FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET phone = @CoordPhone_N WHERE pID = @SCID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(phone, frame_n_) VALUES (@CoordPhone_N , @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	
	IF UPDATE(CoordEmail) 
	BEGIN
		SELECT @CoordEmail_N = CoordEmail FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET email = @CoordEmail_N WHERE pID=@SCID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(email, frame_n_) VALUES (@CoordEmail_N , @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_
		END
	END
	
   IF UPDATE(CoordFax) 
   BEGIN
		SELECT @CoordFax_N = CoordFax FROM INSERTED
		IF @SCID IS NOT NULL AND @SCID <> ''
			UPDATE tblSCSPersonnel SET Fax = @CoordFax_N WHERE pID = @SCID
		ELSE
		BEGIN
			INSERT INTO tblSCSPersonnel(Fax, frame_n_) VALUES (@CoordFax_N , @Frame_n_)
			SELECT @PID = IDENT_CURRENT('tblSCSPersonnel')
			UPDATE tblSCSSchool SET CoordinatorID = @PID WHERE Frame_N_ = @Frame_n_   
		END
	END
END

IF	@ISPublic = 1
	GOTO LB_return
	
IF	UPDATE(SchoolName) 
BEGIN
	SELECT	@SchoolName_N = SchoolName FROM INSERTED
	IF	@SchoolName_N IS NOT NULL and @SchoolName_N <> ''
		UPDATE tblSCSSchool SET s_name = @SchoolName_N WHERE frame_n_ = @Frame_n_
END

IF	UPDATE(Address1) 
BEGIN
    SELECT	@Address1_N = Address1 FROM INSERTED
	IF	@Address1_N IS NOT NULL and @Address1_N <> ''
		UPDATE tblSCSSchool SET s_addr1 = @Address1_N WHERE frame_n_ = @Frame_n_
END

IF	UPDATE(Address2) 
BEGIN
	SELECT	@Address2_N = Address2 FROM INSERTED
	IF	@Address2_N IS NOT NULL and @Address2_N <> ''
            UPDATE tblSCSSchool SET s_addr2 = @Address2_N WHERE frame_n_ = @Frame_n_
END

IF	UPDATE(City) 
BEGIN
	SELECT	@City_N = City FROM INSERTED
	IF	@City_N IS NOT NULL AND @City_N <> ''
		UPDATE tblSCSSchool SET s_city = @City_N WHERE frame_n_ = @Frame_n_
END

IF	UPDATE(State)
BEGIN
	SELECT	@State_N = State FROM INSERTED
	IF	@State_N IS NOT NULL AND @State_N <> ''
		UPDATE tblSCSSchool SET s_state = @State_N WHERE frame_n_ = @Frame_n_
END

IF	UPDATE(Zip) 
BEGIN
	SELECT	@Zip_N = Zip FROM INSERTED
	IF	@Zip_N IS NOT NULL AND @Zip_N <> ''
		UPDATE tblSCSSchool SET s_zip = @Zip_N WHERE frame_n_ = @Frame_n_

END
      
IF	UPDATE(SDCF_MailAddr1) 
BEGIN
	SELECT	@SDCF_MailAddr1_N = SDCF_MailAddr1 FROM INSERTED
	IF	@SDCF_MailAddr1_N IS NOT NULL AND @SDCF_MailAddr1_N <> ''
		UPDATE tblSCSSchool SET MailAddr1 = @SDCF_MailAddr1_N WHERE frame_n_ = @Frame_n_ 
END

IF	UPDATE(SDCF_MailAddr2) 
BEGIN
	SELECT	@SDCF_MailAddr2_N = SDCF_MailAddr2 FROM INSERTED
	IF	@SDCF_MailAddr2_N IS NOT NULL AND @SDCF_MailAddr2_N <> ''
		UPDATE tblSCSSchool SET MailAddr2 = @SDCF_MailAddr2_N WHERE frame_n_ = @Frame_n_
END

IF	UPDATE(SDCF_MailCity) 
BEGIN
	SELECT	@SDCF_MailCity_N = SDCF_MailCity FROM INSERTED
	IF @SDCF_MailCity_N IS NOT NULL AND @SDCF_MailCity_N <> ''
		UPDATE tblSCSSchool SET	MailCity = @SDCF_MailCity_N WHERE frame_n_ = @Frame_n_ 
END

IF	UPDATE(SDCF_MailState) 
BEGIN
	SELECT	@SDCF_MailState_N = SDCF_MailState FROM INSERTED
	IF @SDCF_MailState_N IS NOT NULL AND @SDCF_MailState_N <> ''
		UPDATE tblSCSSchool SET MailState = @SDCF_MailState_N WHERE frame_n_ = @Frame_n_ 
END

IF	UPDATE(SDCF_MailZip) 
BEGIN
	SELECT	@SDCF_MailZip_N = SDCF_MailZip FROM INSERTED
	IF	@SDCF_MailZip_N IS NOT NULL AND @SDCF_MailZip_N <> ''
		UPDATE tblSCSSchool SET MailZip = @SDCF_MailZip_N WHERE frame_n_ = @Frame_n_  
END
   
IF	UPDATE(FlagCharter4) 
BEGIN
	SELECT	@FlagCharter4_N = FlagCharter4 FROM INSERTED
	IF	@FlagCharter4_N IS NOT NULL
		UPDATE tblSCSGrade SET CharterFlag = @FlagCharter4_N WHERE ID = @ID4
END 

IF	UPDATE(FlagCharter8) 
BEGIN
	SELECT	@FlagCharter8_N = FlagCharter8 FROM INSERTED
	IF	@FlagCharter8_N IS NOT NULL
		UPDATE tblSCSGrade SET CharterFlag = @FlagCharter8_N WHERE ID = @ID8
END 

IF	UPDATE(FlagCharter12) 
BEGIN
	SELECT	@FlagCharter12_N = FlagCharter12 FROM INSERTED
	IF	@FlagCharter12_N IS NOT NULL
		UPDATE tblSCSGrade SET CharterFlag = @FlagCharter12_N WHERE ID = @ID12
END 

IF	UPDATE(YRCalendar4thGrYN) 
BEGIN
	SELECT	@YRCalendar4thGrYN_N = YRCalendar4thGrYN FROM INSERTED
	IF	@YRCalendar4thGrYN_N IS NOT NULL
		UPDATE tblSCSGrade SET Calendar = @YRCalendar4thGrYN_N WHERE ID = @ID4
END

IF	UPDATE(YRCalendar8thGrYN) 
BEGIN
	SELECT	@YRCalendar8thGrYN_N = YRCalendar8thGrYN FROM INSERTED
	IF	@YRCalendar8thGrYN_N IS NOT NULL
		UPDATE tblSCSGrade SET Calendar = @YRCalendar8thGrYN_N WHERE ID = @ID8
END

IF	UPDATE(YRCalendar12thGrYN) 
BEGIN
	SELECT	@YRCalendar12ThGrYN_N = YRCalendar12thGrYN FROM INSERTED
	IF	@YRCalendar12thGrYN_N IS NOT NULL
		UPDATE tblSCSGrade SET Calendar = @YRCalendar12thGrYN_N WHERE ID = @ID12            
END

IF	UPDATE(YearRoundNumTracks4th) 
BEGIN
	SELECT	@YearRoundNumTracks4th_N = YearRoundNumTracks4th FROM INSERTED
	IF @YearRoundNumTracks4th_N IS NOT NULL
		UPDATE tblSCSGrade SET Num_Tracks = @YearRoundNumTracks4th_N WHERE ID = @ID4
END

IF	UPDATE(YearRoundNumTracks8th) 
BEGIN
	SELECT	@YearRoundNumTracks8th_N = YearRoundNumTracks8th FROM INSERTED
	IF @YearRoundNumTracks8th_N IS NOT NULL
		UPDATE tblSCSGrade SET Num_Tracks = @YearRoundNumTracks8th_N WHERE ID = @ID8
END

IF	UPDATE(YearRoundNumTracks12th) 
BEGIN
	SELECT	@YearRoundNumTracks12th_N = YearRoundNumTracks12th FROM INSERTED
	IF	@YearRoundNumTracks12th_N IS NOT NULL
		UPDATE tblSCSGrade SET Num_Tracks = @YearRoundNumTracks12th_N WHERE ID = @ID12
END
      
IF	UPDATE(PercentOffTrack4th) 
BEGIN
	SELECT	@PercentOffTrack4th_N = PercentOffTrack4th FROM INSERTED
	IF @PercentOffTrack4th_N IS NOT NULL
		UPDATE tblSCSGrade SET Pct_On_Break = @PercentOffTrack4th_N WHERE ID=@ID4
END

IF	UPDATE(PercentOffTrack8th) 
BEGIN
	SELECT	@PercentOffTrack8th_N = PercentOffTrack8th FROM INSERTED
	IF @PercentOffTrack8th_N IS NOT NULL
		UPDATE tblSCSGrade SET Pct_On_Break=@PercentOffTrack8th_N WHERE ID = @ID8
END
IF	UPDATE(PercentOffTrack12th) 
BEGIN
	SELECT	@PercentOffTrack12th_N = PercentOffTrack12th FROM INSERTED
	IF @PercentOffTrack12th_N IS NOT NULL
		UPDATE tblSCSGrade SET Pct_On_Break = @PercentOffTrack12th_N WHERE ID = @ID12
END

IF	UPDATE(YearRound4thAbil) 
BEGIN
	SELECT	@YearRound4thAbil_N = YearRound4thAbil FROM INSERTED
	IF  @YearRound4thAbil_N IS NOT NULL
		UPDATE tblSCSGrade SET Pct_On_Break_Ability = @YearRound4thAbil_N WHERE ID = @ID4
END 

IF	UPDATE(YearRound8thAbil) 
BEGIN 
	SELECT	@YearRound8thAbil_N = YearRound8thAbil FROM INSERTED
	IF	@YearRound8thAbil_N IS NOT NULL
		UPDATE tblSCSGrade SET Pct_On_Break_Ability = @YearRound8thAbil_N WHERE ID = @ID8
END 

IF	UPDATE(YearRound12thAbil) 
BEGIN 
	SELECT	@YearRound12thAbil_N = YearRound12thAbil FROM INSERTED
	IF	@YearRound12thAbil_N IS NOT NULL
		UPDATE tblSCSGrade SET Pct_On_Break_Ability = @YearRound12thAbil_N WHERE ID = @ID12
END 

IF	UPDATE(YearRound4thReasonOS) 
BEGIN
	SELECT	@YearRound4thReasonOS_N = YearRound4thReasonOS FROM INSERTED
	IF  @YearRound4thReasonOS_N IS NOT NULL AND @YearRound4thReasonOS_N <> ''
		UPDATE tblSCSGrade SET Pct_On_Break_Other = @YearRound4thReasonOS_N WHERE ID = @ID4
END

IF	UPDATE(YearRound8thReasonOS) 
BEGIN
	SELECT	@YearRound8thReasonOS_N = YearRound8thReasonOS FROM INSERTED
	IF  @YearRound8thReasonOS_N IS NOT NULL AND @YearRound8thReasonOS_N <> ''
		UPDATE tblSCSGrade SET Pct_On_Break_Other = @YearRound8thReasonOS_N WHERE ID = @ID8
END

IF	UPDATE(YearRound12thReasonOS) 
BEGIN
	SELECT	@YearRound12thReasonOS_N = YearRound12thReasonOS FROM INSERTED
	IF	@YearRound12thReasonOS_N IS NOT NULL AND @YearRound12thReasonOS_N <> ''
		UPDATE tblSCSGrade SET Pct_On_Break_Other = @YearRound12thReasonOS_N WHERE ID = @ID12
END

IF	UPDATE(enrollgr4) 
BEGIN
	SELECT	@enrollgr4_N = enrollgr4 FROM INSERTED
	IF @enrollgr4_N IS NOT NULL 
		UPDATE tblSCSGrade SET Enroll = @enrollgr4_N WHERE ID = @ID4
END

IF	UPDATE(enrollgr8) 
BEGIN
	SELECT	@enrollgr8_N = enrollgr8 FROM INSERTED
	IF @enrollgr8_N IS NOT NULL
		UPDATE tblSCSGrade SET Enroll = @enrollgr8_N WHERE ID = @ID8
END

IF	UPDATE(enrollgr12) 
BEGIN
	SELECT @enrollgr12_N = enrollgr12 FROM INSERTED
	IF @enrollgr12_N IS NOT NULL 
		UPDATE tblSCSGrade SET Enroll = @enrollgr12_N WHERE ID = @ID12
END

IF	UPDATE(SchoolGoesOnBreakDT_4) 
BEGIN
	SELECT	@SchoolGoesOnBreakDT_4_N = SchoolGoesOnBreakDT_4 FROM INSERTED
	IF	@SchoolGoesOnBreakDT_4_N IS NOT NULL AND @SchoolGoesOnBreakDT_4_N <> ''
		UPDATE tblSCSGrade SET SchoolGoesOnBreakDT = @SchoolGoesOnBreakDT_4_N WHERE ID = @ID4            
END 

IF	UPDATE(SchoolGoesOnBreakDT_8) 
BEGIN
	SELECT	@SchoolGoesOnBreakDT_8_N = SchoolGoesOnBreakDT_8 FROM INSERTED
	IF	@SchoolGoesOnBreakDT_8_N IS NOT NULL AND @SchoolGoesOnBreakDT_8_N <> ''
		UPDATE tblSCSGrade SET SchoolGoesOnBreakDT = @SchoolGoesOnBreakDT_8_N WHERE ID = @ID8            
END 

IF	UPDATE(SchoolGoesOnBreakDT_12) 
BEGIN
	SELECT	@SchoolGoesOnBreakDT_12_N = SchoolGoesOnBreakDT_12 FROM INSERTED
	IF	@SchoolGoesOnBreakDT_12_N IS NOT NULL AND @SchoolGoesOnBreakDT_12_N <> ''
		UPDATE tblSCSGrade SET SchoolGoesOnBreakDT = @SchoolGoesOnBreakDT_12_N WHERE ID = @ID12          
END 
      
IF	UPDATE(InSessionFirstDT_4) 
BEGIN
	SELECT	@InSessionFirstDT_4_N = InSessionFirstDT_4 FROM INSERTED
	IF  @InSessionFirstDT_4_N IS NOT NULL AND @InSessionFirstDT_4_N <> ''
		UPDATE tblSCSGrade SET InSessionFirstDT = @InSessionFirstDT_4_N WHERE ID = @ID4 
END 
      
IF	UPDATE(InSessionFirstDT_8) 
BEGIN
	SELECT	@InSessionFirstDT_8_N = InSessionFirstDT_8 FROM INSERTED
	IF  @InSessionFirstDT_8_N IS NOT NULL AND @InSessionFirstDT_8_N <> ''
		UPDATE tblSCSGrade SET InSessionFirstDT = @InSessionFirstDT_8_N WHERE ID = @ID8 
END 
      
IF	UPDATE(InSessionFirstDT_12) 
BEGIN
	SELECT	@InSessionFirstDT_12_N = InSessionFirstDT_12 FROM INSERTED
	IF	@InSessionFirstDT_12_N IS NOT NULL AND @InSessionFirstDT_12_N <> ''
		UPDATE tblSCSGrade SET InSessionFirstDT = @InSessionFirstDT_12_N WHERE ID = @ID12 
END 
      
IF  UPDATE(LastDayofSchool_4) 
BEGIN
	SELECT	@LastDayofSchool_4_N = LastDayofSchool_4 FROM INSERTED
	IF	@LastDayofSchool_4_N IS NOT NULL AND @LastDayofSchool_4_N <> ''
		UPDATE tblSCSGrade SET LastDayofSchool = @LastDayofSchool_4_N WHERE ID = @ID4     
END 

IF	UPDATE(LastDayofSchool_8) 
BEGIN
	SELECT	@LastDayofSchool_8_N = LastDayofSchool_8 FROM INSERTED
	IF	@LastDayofSchool_8_N IS NOT NULL AND @LastDayofSchool_8_N <> ''
		UPDATE tblSCSGrade SET LastDayofSchool = @LastDayofSchool_8_N WHERE ID = @ID8     
END 

IF	UPDATE(LastDayofSchool_12) 
BEGIN
	SELECT	@LastDayofSchool_12_N = LastDayofSchool_12 FROM INSERTED
	IF  @LastDayofSchool_12_N IS NOT NULL AND @LastDayofSchool_12_N <> ''
		UPDATE tblSCSGrade SET LastDayofSchool = @LastDayofSchool_12_N WHERE ID = @ID12    
END 

LB_return:
return
         
LB_error:
RAISERROR (@errmsg,16,1)
Rollback