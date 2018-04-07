alter Function [dbo].[udf_Get_AssessmentMethod] (@as_fldProjectID int, @as_ID varchar(7), @as_TypeAssessmentMethod int)  
returns varchar(1024)
AS
BEGIN
DECLARE	@lv_LEAID VARCHAR(7),
		@lv_REPSBGRP VARCHAR(2),
		@lv_NAEPYear int,
		@lv_AssessmentMethod varchar(1024)
		
SELECT	@lv_LEAID = LEAID, 
		@lv_REPSBGRP = REPSBGRP
  FROM	dbo.tblGrade_Stat
 WHERE	fldProjectID = @as_fldProjectID AND
		ID = @as_ID
		
SELECT	@lv_NAEPYear = CAST(SUBSTRING(DB_NAME(),9,4) AS INT) 

--13	Name of Modified Assessment	1
--14	Name of Alternate State Assessment	
SELECT	@lv_AssessmentMethod = fldSDCFFieldGlobalValue
  FROM  NAEPAdminNET.dbo.tblMSSDCFFieldSetConfig
 WHERE	(fldSDCFFieldSetID = @as_TypeAssessmentMethod) AND
		(NAEPYear = @lv_NAEPYear) AND
		(REPSBGRP = @lv_REPSBGRP) AND
		(Leaid = @lv_LEAID)
	
IF	@lv_AssessmentMethod IS NOT NULL AND @lv_AssessmentMethod <> ''
	GOTO LB_END
	
SELECT	@lv_AssessmentMethod = fldSDCFFieldGlobalValue
  FROM  NAEPAdminNET.dbo.tblMSSDCFFieldSetConfig
 WHERE	(fldSDCFFieldSetID = @as_TypeAssessmentMethod) AND
		(NAEPYear = @lv_NAEPYear) AND
		(REPSBGRP = @lv_REPSBGRP) AND
		(Leaid IS NULL)

/*	
IF	@lv_AssessmentMethod IS NOT NULL AND @lv_AssessmentMethod <> ''
	GOTO LB_END
	
SELECT	@lv_AssessmentMethod = fldSDCFFieldGlobalValue
  FROM  NAEPAdminNET.dbo.tblMSSDCFFieldSetConfig
 WHERE	(fldSDCFFieldSetID = @as_TypeAssessmentMethod) AND
		(NAEPYear = @lv_NAEPYear) AND
		(REPSBGRP IS NULL) AND
		(Leaid IS NULL)	
*/
                       
LB_END:
RETURN @lv_AssessmentMethod

---Error:
--print 'Error: ' + @errmsg
--RAISERROR (@errmsg,16,1)
--return -1
END
