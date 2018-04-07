ALTER TRIGGER [dbo].[TRI_tblSCSSchool_UpdateNodeStatusGreen] ON [dbo].[tblSCSSchool] 
AFTER UPDATE
AS 
DECLARE	@Frame_n_ VARCHAR(6),
		@UpdatedByUID int,
		@SDCF_flag int,
		@DateSCReviewCompleted datetime,		
		@WhoUpdated int,
		@lv_SQL varchar(max)	
DECLARE	@errmsg VARCHAR(1024),
		@NAEPyear int
DECLARE	@T_NODEID TABLE
		(NODEID int,
		 SchoolGradeLevel char(1))		
DECLARE	@tblTaskManager TABLE
		(NODEID int,
		fldProjectID int, 
		ID varchar(7))
		
SET NOCOUNT ON

IF	UPDATE(DateSCReviewCompleted)	
BEGIN
	SELECT	@Frame_n_ = Frame_n_ FROM INSERTED	
	SELECT	@WhoUpdated = ISNULL(WhoUpdated,'') FROM INSERTED
	SELECT	@Frame_n_ = Frame_n_ FROM INSERTED
	SELECT	@SDCF_flag = SDCF_flag FROM INSERTED
	SELECT	@DateSCReviewCompleted = DateSCReviewCompleted FROM INSERTED
		
	IF	@DateSCReviewCompleted IS NOT NULL
	BEGIN
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (188,'S')
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (189,'G')
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (190,'G')
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (191,'G')
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (192,'G')
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (193,'G')
		INSERT INTO @T_NODEID (NODEID,SchoolGradeLevel) VALUES (194,'G')

		SELECT @NAEPyear = CAST(SUBSTRING(DB_NAME(),9,4) AS int)

		INSERT INTO @tblTaskManager
				(NODEID,
				fldProjectID, 
				ID)
		SELECT	NODEID, 0, @Frame_n_ FROM @T_NODEID WHERE SchoolGradeLevel = 'S'
     
		INSERT INTO @tblTaskManager
				(NODEID,
				fldProjectID, 
				ID)
		SELECT	NODEID, fldProjectID, ID
		  FROM	dbo.tblSCSGrade CROSS JOIN
				(SELECT NODEID FROM @T_NODEID WHERE SchoolGradeLevel = 'G') T 
		 WHERE	Frame_N_ = @Frame_n_
     
		SELECT  *
		  FROM	@tblTaskManager A LEFT OUTER JOIN
				dbo.ExAdmin_TaskManagerByID B ON A.NODEID = B.NODEID AND A.fldProjectID = B.fldProjectID AND A.ID = B.ID
		 WHERE	B.fldNodeStatus = 2			
		 
		SELECT  @lv_SQL = coalesce(@lv_SQL + 'EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus ' + CAST(A.NODEID AS VARCHAR(10)) + ',' + CAST(A.fldProjectID AS VARCHAR(10)) + ',''' + A.ID + ''', 3, ''' + CAST(@DateSCReviewCompleted AS VARCHAR(20))
 + ''',' + CAST(@WhoUpdated AS VARCHAR(10)) + ', ' + CAST(@NAEPyear AS varchar(10)) + ';','EXEC NAEPAdminNET.dbo.NAEP_SiteMapNode_UpdateSiteMapNodeStatus ' + CAST(A.NODEID AS VARCHAR(10)) + ',' + CAST(A.fldProjectID AS VARCHAR(10)) + ',''' + A.ID + ''', 3
, ''' + CAST(@DateSCReviewCompleted AS VARCHAR(20)) + ''',' + CAST(@WhoUpdated AS VARCHAR(10)) + ', ' + CAST(@NAEPyear AS varchar(10)) + ';')
		  FROM	@tblTaskManager A LEFT OUTER JOIN
				dbo.ExAdmin_TaskManagerByID B ON A.NODEID = B.NODEID AND A.fldProjectID = B.fldProjectID AND A.ID = B.ID
		 WHERE	B.fldNodeStatus = 2			

		IF @lv_SQL IS NOT NULL
			EXEC (@lv_SQL)	
	END	
END

LB_return:
return
         
LB_error:
RAISERROR (@errmsg,16,1)
Rollback







