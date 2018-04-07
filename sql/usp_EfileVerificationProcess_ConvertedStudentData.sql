USE [TIMSSSCS2015]
GO
if OBJECT_ID('usp_EfileVerificationProcess_ConvertedStudentData') > 0
	drop proc usp_EfileVerificationProcess_ConvertedStudentData
go

CREATE PROCEDURE [dbo].[usp_EfileVerificationProcess_ConvertedStudentData] 

@FileId int
--,@CopyToClean int = 0

AS
/*

declare @FileId int

set @FileId = 24
exec dbo.usp_EfileVerificationProcess_ClearParsed @FileId = @FileId
exec dbo.usp_EfileVerificationProcess_ConvertedStudentData @FileId = @FileId

*/
/*
if OBJECT_ID('tempdb..#tmpColumns') > 0
	drop table [#tmpColumns]
go
if OBJECT_ID('tempdb..#tmpCleanedStudentData') > 0
	drop table #tmpCleanedStudentData
go
if OBJECT_ID('tempdb..#tmpLinkedValues') > 0
	drop table #tmpLinkedValues
go

declare @FileId int
declare @ReturnResults int

set @FileId = 24
*/

set nocount on



/*

Convert and copy student data to tblEfileCleanedStudentData

*/



CREATE TABLE [#tmpCleanedStudentData] (
	[EfileCleanStudentId] [int] IDENTITY (1, 1) NOT NULL ,
	[FileId] [int] NOT NULL ,
	[fldProjectID] [int] not null,
	[ID] [char] (7) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Row] [int] NOT NULL ,
	[Fullname] [varchar] (750) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Lastname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Firstname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Middlename] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Homeroom] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SD] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ELL] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL  ,
		
	[DOB] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DOBFormat] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	
	[MOB] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DAOB] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[YOB] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Sex] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	--[Title1] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	--[SchoolLunch] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[SESSID] [char] (6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AdminLineNumber] [smallint] NULL ,
	[SLFLineNumber] [smallint] NULL ,
	[wesroom] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SD_ELL] [char] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Sampled] [int] NOT NULL ,
	
	[MathematicsTeacherFullname] [varchar] (750) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MathematicsTeacherLastname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MathematicsTeacherFirstname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MathematicsTeacherMiddlename] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	
	[ScienceTeacherFullname] [varchar] (750) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ScienceTeacherLastname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ScienceTeacherFirstname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ScienceTeacherMiddlename] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	
	[TeacherFullname] [varchar] (750) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TeacherLastname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TeacherFirstname] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TeacherMiddlename] [varchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	
	ClassListingFormId					int null
	
) ON [PRIMARY]

ALTER TABLE [#tmpCleanedStudentData] ADD 
	CONSTRAINT [DF_#tmpCleanedStudentData_SD] DEFAULT (0) FOR [SD],
	CONSTRAINT [DF_#tmpCleanedStudentData_ELL] DEFAULT (0) FOR [ELL],
	CONSTRAINT [DF_#tmpCleanedStudentData_Sex] DEFAULT (0) FOR [Sex],
	--CONSTRAINT [DF_#tmpCleanedStudentData_Title1] DEFAULT (0) FOR [Title1],
	--CONSTRAINT [DF_#tmpCleanedStudentData_SchoolLunch] DEFAULT (0) FOR [SchoolLunch],
	CONSTRAINT [DF_#tmpCleanedStudentData_SD_ELL] DEFAULT (0) FOR [SD_ELL],
	CONSTRAINT [DF_#tmpCleanedStudentData_Sampled] DEFAULT (0) FOR [Sampled],
	CONSTRAINT [IX_NoDupRowInFile] UNIQUE  NONCLUSTERED 
	(
		[FileId],
		[fldProjectID],
		[ID],
		[Row]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 




declare @StudentNameFullColumnSeq int
declare @StudentNameLastColumnSeq int
declare @StudentNameFirstColumnSeq int
declare @StudentNameMiddleColumnSeq int
declare @BirthDateMonthColumnSeq int
declare @BirthDateDayColumnSeq int
declare @BirthDateYearColumnSeq  int
declare @DOBColumnSeq  int
declare @GradeColumnSeq int
declare @NaepIdColumnSeq int
declare @RaceColumnSeq int
declare @SexColumnSeq int
declare @SDColumnSeq int
declare @ELLColumnSeq int
declare @SchoolLunchColumnSeq int
declare @Title1ColumnSeq int
declare @HRColumnSeq int
declare @OnBreakColumnSeq int
declare @strsql varchar(8000)
declare @ColumnSeq int
declare @Response varchar(250)
declare @NaepLabel varchar(100)
declare @CodeValue varchar(50)
declare @column varchar(50)
declare @cnt int

declare @MathematicsTeacherNameFullColumnSeq int
declare @MathematicsTeacherNameLastColumnSeq int
declare @MathematicsTeacherNameFirstColumnSeq int
declare @MathematicsTeacherNameMiddleColumnSeq int

declare @ScienceTeacherNameFullColumnSeq int
declare @ScienceTeacherNameLastColumnSeq int
declare @ScienceTeacherNameFirstColumnSeq int
declare @ScienceTeacherNameMiddleColumnSeq int

declare @TeacherNameFullColumnSeq int
declare @TeacherNameLastColumnSeq int
declare @TeacherNameFirstColumnSeq int
declare @TeacherNameMiddleColumnSeq int

declare @ClassListingFormIdColumnSeq int
declare @ClassListingFormId int

/*
declare @GetColumnSequenceMethod varchar(50)

set @GetColumnSequenceMethod = dbo.udf_GetColumnSequenceMethod()
*/

CREATE TABLE [#tmpColumns] (
	[CID] [int] IDENTITY (1, 1) NOT NULL
	, NaepLabel varchar(100) not null
	, ColumnSeq int not null)

insert into [#tmpColumns] (NaepLabel, ColumnSeq)
select	b.NaepLabel
		,a.ColumnSeq
from	tblEfileUserColumns a
join	tblEfileNaepLabels b
on		b.NaepLabelId = a.NaepLabelId
where   a.FileId = @FileId
--and		b.NaepLabel in ('Student Name', 'Student Name: Last', 'Student Name: First', 'Student Name: Middle', 'Birth Date: Month of Birth', 'Birth Date: Year of Birth', 'Mathematics Teacher Name', 'Mathematics Teacher Name: Last', 'Mathematics Teacher Name: First', 'Mathematics Teacher Name: Middle', 'Science Teacher Name', 'Science Teacher Name: Last', 'Science Teacher Name: First', 'Science Teacher Name: Middle')

declare @cid int
--declare @NaepLabel varchar(100)
--declare @ColumnSeq int

select @cid = min(cid)
from [#tmpColumns]
while @cid is not null
begin

	select 	@NaepLabel = NaepLabel
			,@ColumnSeq = ColumnSeq
	from 	[#tmpColumns] 
	where 	cid = @cid
	
	print @NaepLabel
	
	if @NaepLabel = 'Student Name'
	begin
		set @StudentNameFullColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Student Name: Last'
	begin
		set @StudentNameLastColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Student Name: First'
	begin
		set @StudentNameFirstColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Student Name: Middle'
	begin
		set @StudentNameMiddleColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Date of Birth'
	begin
		set @DOBColumnSeq = @ColumnSeq
	end
		
	if @NaepLabel = 'Birth Date: Month of Birth'
	begin
		set @BirthDateMonthColumnSeq = @ColumnSeq
	end
		
	if @NaepLabel = 'Birth Date: Day of Birth'
	begin
		set @BirthDateDayColumnSeq = @ColumnSeq
	end
	
	
	if @NaepLabel = 'Birth Date: Year of Birth'
	begin
		set @BirthDateYearColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Mathematics Teacher Name'
	begin
		set @MathematicsTeacherNameFullColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Mathematics Teacher Name: Last'
	begin
		set @MathematicsTeacherNameLastColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Mathematics Teacher Name: First'
	begin
		set @MathematicsTeacherNameFirstColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Mathematics Teacher Name: Middle'
	begin
		set @MathematicsTeacherNameMiddleColumnSeq = @ColumnSeq
	end
		
	if @NaepLabel = 'Science Teacher Name'
	begin
		set @ScienceTeacherNameFullColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Science Teacher Name: Last'
	begin
		set @ScienceTeacherNameLastColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Science Teacher Name: First'
	begin
		set @ScienceTeacherNameFirstColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Science Teacher Name: Middle'
	begin
		set @ScienceTeacherNameMiddleColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Class'
	begin
		set @ClassListingFormIdColumnSeq = @ColumnSeq
	end

		
	if @NaepLabel = 'Teacher Name'
	begin
		set @TeacherNameFullColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Teacher Name: Last'
	begin
		set @TeacherNameLastColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Teacher Name: First'
	begin
		set @TeacherNameFirstColumnSeq = @ColumnSeq
	end
	
	if @NaepLabel = 'Teacher Name: Middle'
	begin
		set @TeacherNameMiddleColumnSeq = @ColumnSeq
	end
	
	select	@cid = min(cid)
	from	[#tmpColumns]
	Where	cid > @cid
end

select *
from [#tmpColumns]

select @TeacherNameFullColumnSeq
/*
if @GetColumnSequenceMethod = 'view'
begin
	SELECT     @StudentNameLastColumnSeq = StudentNameLastColumnSeq
	, @StudentNameFirstColumnSeq = StudentNameFirstColumnSeq
	, @StudentNameMiddleColumnSeq = StudentNameMiddleColumnSeq
	, @BirthDateMonthColumnSeq = BirthDateMonthColumnSeq
	, @BirthDateYearColumnSeq = BirthDateYearColumnSeq
	, @GradeColumnSeq = GradeColumnSeq
	, @NaepIdColumnSeq = NaepIdColumnSeq
	, @RaceColumnSeq = RaceColumnSeq
	, @SexColumnSeq = SexColumnSeq
	, @SDColumnSeq = SDColumnSeq
	, @ELLColumnSeq = ELLColumnSeq
	, @SchoolLunchColumnSeq = SchoolLunchColumnSeq
	, @Title1ColumnSeq = Title1ColumnSeq
	, @HRColumnSeq = HRColumnSeq
FROM         dbo.uvw_EfileColumnSequence
WHERE     (FileId = @fileid)
end
if @GetColumnSequenceMethod = 'stored proc'
begin
	exec usp_EfileLinkingProcess_ColumnSequencesAll @fileid,
	NULL,
	NULL,
	@StudentNameLastColumnSeq output,
	@StudentNameFirstColumnSeq output,
	@StudentNameMiddleColumnSeq output,
	@HRColumnSeq output,
	@BirthDateMonthColumnSeq output,
	@BirthDateYearColumnSeq output,
	@SexColumnSeq output,
	@RaceColumnSeq output,
	@SchoolLunchColumnSeq output,
	@ELLColumnSeq output,
	@SDColumnSeq output,
	@OnBreakColumnSeq output,
	@GradeColumnSeq output
end
*/

set @strsql = 'Insert into #tmpCleanedStudentData (FileId, fldProjectID, ID, Row'


if not @StudentNameFullColumnSeq is null
	set @strsql = @strsql + ', Fullname'

if not @StudentNameLastColumnSeq is null
	set @strsql = @strsql + ', Lastname'

if not @StudentNameFirstColumnSeq is null
	set @strsql = @strsql + ', Firstname'

if not @StudentNameMiddleColumnSeq is null
	set @strsql = @strsql + ', Middlename'
	
if not @DOBColumnSeq is null
	set @strsql = @strsql + ', DOB'
	
if not @BirthDateMonthColumnSeq is null
	set @strsql = @strsql + ', MOB'
	
if not @BirthDateDayColumnSeq is null
	set @strsql = @strsql + ', DAOB'

if not @BirthDateYearColumnSeq is null
	set @strsql = @strsql + ', YOB'
	
if not @MathematicsTeacherNameFullColumnSeq is null
	set @strsql = @strsql + ', MathematicsTeacherFullname'

if not @MathematicsTeacherNameLastColumnSeq is null
	set @strsql = @strsql + ', MathematicsTeacherLastname'
	
if not @MathematicsTeacherNameFirstColumnSeq is null
	set @strsql = @strsql + ', MathematicsTeacherFirstname'
	
if not @MathematicsTeacherNameMiddleColumnSeq is null
	set @strsql = @strsql + ', MathematicsTeacherMiddlename'

if not @ScienceTeacherNameFullColumnSeq is null
	set @strsql = @strsql + ', ScienceTeacherFullname'

if not @ScienceTeacherNameLastColumnSeq is null
	set @strsql = @strsql + ', ScienceTeacherLastname'
	
if not @ScienceTeacherNameFirstColumnSeq is null
	set @strsql = @strsql + ', ScienceTeacherFirstname'
	
if not @ScienceTeacherNameMiddleColumnSeq is null
	set @strsql = @strsql + ', ScienceTeacherMiddlename'
	
	
if not @TeacherNameFullColumnSeq is null
	set @strsql = @strsql + ', TeacherFullname'

if not @TeacherNameLastColumnSeq is null
	set @strsql = @strsql + ', TeacherLastname'
	
if not @TeacherNameFirstColumnSeq is null
	set @strsql = @strsql + ', TeacherFirstname'
	
if not @TeacherNameMiddleColumnSeq is null
	set @strsql = @strsql + ', TeacherMiddlename'
		
set @strsql = @strsql + ')'

set @strsql = @strsql +'
select FileId, fldProjectID, ID, Row'

if not @StudentNameFullColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@StudentNameFullColumnSeq as varchar(10))

if not @StudentNameLastColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@StudentNameLastColumnSeq as varchar(10))

if not @StudentNameFirstColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@StudentNameFirstColumnSeq as varchar(10))

if not @StudentNameMiddleColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@StudentNameMiddleColumnSeq as varchar(10))


	
if not @DOBColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@DOBColumnSeq as varchar(10))
	
if not @BirthDateMonthColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@BirthDateMonthColumnSeq as varchar(10))
	
if not @BirthDateDayColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@BirthDateDayColumnSeq as varchar(10))
	
if not @BirthDateYearColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@BirthDateYearColumnSeq as varchar(10))


if not @MathematicsTeacherNameFullColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@MathematicsTeacherNameFullColumnSeq as varchar(10))
	
if not @MathematicsTeacherNameLastColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@MathematicsTeacherNameLastColumnSeq as varchar(10))
	
if not @MathematicsTeacherNameFirstColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@MathematicsTeacherNameFirstColumnSeq as varchar(10))
	
if not @MathematicsTeacherNameMiddleColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@MathematicsTeacherNameMiddleColumnSeq as varchar(10))
	


if not @ScienceTeacherNameFullColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@ScienceTeacherNameFullColumnSeq as varchar(10))
	
if not @ScienceTeacherNameLastColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@ScienceTeacherNameLastColumnSeq as varchar(10))
	
if not @ScienceTeacherNameFirstColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@ScienceTeacherNameFirstColumnSeq as varchar(10))
	
if not @ScienceTeacherNameMiddleColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@ScienceTeacherNameMiddleColumnSeq as varchar(10))
	


if not @TeacherNameFullColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@TeacherNameFullColumnSeq as varchar(10))
	
if not @TeacherNameLastColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@TeacherNameLastColumnSeq as varchar(10))
	
if not @TeacherNameFirstColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@TeacherNameFirstColumnSeq as varchar(10))
	
if not @TeacherNameMiddleColumnSeq is null
	set @strsql = @strsql + ', C'+cast(@TeacherNameMiddleColumnSeq as varchar(10))
	
set @strsql = @strsql + ' from dbo.tblEfileStudentData Where FileId = ' + cast(@FileID as varchar(10))

--print @strsql

exec(@strsql)

CREATE TABLE [#tmpLinkedValues] (
	[LVID] [int] IDENTITY (1, 1) NOT NULL
	, NaepLabel varchar(100) not null
	, Response varchar(250) not null
	, CodeValue varchar(100) null
	, CodeLabel varchar(100) null
	, ColumnSeq int not null
	, ClassListingFormId int null
	)

insert into [#tmpLinkedValues] (NaepLabel, Response, CodeLabel, CodeValue, ColumnSeq, ClassListingFormId)
select	c.NaepLabel
		,b.Response
		,d.CodeLabel
		,isnull(cast(d.CodeValue as varchar), d.CodeLabel)
		,a.ColumnSeq
		,b.ClassListingFormId
from	tblEfileUserColumns a
join	tblEfileResponseFreq b
on		b.UserColumnId = a.UserColumnId
join	tblEfileNaepLabels c
on		c.NaepLabelId = a.NaepLabelId
left outer join	tblEfileNaepCodes d
on		d.NaepCodeId = b.NaepCodeId
where	a.FileId = @FileId




declare @LVID int

select @LVID = min(LVID)
from [#tmpLinkedValues]
while @LVID is not null
begin

	select 	@NaepLabel = NaepLabel
			,@ColumnSeq = ColumnSeq
			,@Response = Response
			--,@CodeLabel = CodeLabel
			,@CodeValue = CodeValue
			,@ClassListingFormId = ClassListingFormId
	from 	[#tmpLinkedValues] 
	where 	LVID = @LVID
	
	set @column = ''
	
	if @NaepLabel = 'Student with a Disability' 
		set @column = 'SD'
	if @NaepLabel = 'English Language Learner'
		set @column = 'ELL'
	if @NaepLabel = 'Sex'
		 set @column = 'SEX'
	if @NaepLabel = 'Date of Birth'
		 set @column = 'DOBFormat'
	if @NaepLabel = 'Class'
		 set @column = 'ClassListingFormId'
	
	--@ColumnSeq
	--print @NaepLabel

	if @column <> ''
	begin
		if @NaepLabel = 'Class'
		begin
			set @strsql = 'Update #tmpCleanedStudentData Set ['+@column+']='+cast(@ClassListingFormId as varchar(20))+' Where FileId = '+cast(@FileId as varchar(20))+' and row in (Select Row from tblEfileStudentData Where Fileid = '+cast(@FileId as varchar(20))+'  and c'+cast(@ColumnSeq as varchar(10))+' = '''+replace(@response, '''', '''''')+''')'
		end
		else
		begin
			set @strsql = 'Update #tmpCleanedStudentData Set ['+@column+']='''+@CodeValue+''' Where FileId = '+cast(@FileId as varchar(20))+' and row in (Select Row from tblEfileStudentData Where Fileid = '+cast(@FileId as varchar(20))+'  and c'+cast(@ColumnSeq as varchar(10))+' = '''+replace(@response, '''', '''''')+''')'
		end
		
		--print 'adfadsf'
		--print '[' + @strsql + ']'
		exec(@strsql)
	end
	/*
	else
	begin
		print 'skipping [' + @NaepLabel + ']'
	end
	*/
	select	@LVID = min(LVID)
	from	[#tmpLinkedValues]
	Where	LVID > @LVID
end
insert into tblEfileCleanedStudentData ([FileId],
	[fldProjectID],
	[ID],
	[Row],
	[Fullname],
	[Lastname],
	[Firstname],
	[Middlename],
	[Homeroom],
	[SD],
	[ELL],
		
	[DOB],
	[DOBFormat],
	
	[MOB],
	[DAOB],
	[YOB],
	[Sex],

	
	[MathematicsTeacherFullname],
	[MathematicsTeacherLastname],
	[MathematicsTeacherFirstname],
	[MathematicsTeacherMiddlename],
	
	[ScienceTeacherFullname],
	[ScienceTeacherLastname],
	[ScienceTeacherFirstname],
	[ScienceTeacherMiddlename],
	
	[TeacherFullname],
	[TeacherLastname],
	[TeacherFirstname],
	[TeacherMiddlename],
	
	ClassListingFormId)
select [FileId],
	[fldProjectID],
	[ID],
	[Row],
	[Fullname],
	[Lastname],
	[Firstname],
	[Middlename],
	[Homeroom],
	[SD],
	[ELL],
		
	[DOB],
	[DOBFormat],
	
	[MOB],
	[DAOB],
	[YOB],
	[Sex],

	
	[MathematicsTeacherFullname],
	[MathematicsTeacherLastname],
	[MathematicsTeacherFirstname],
	[MathematicsTeacherMiddlename],
	
	[ScienceTeacherFullname],
	[ScienceTeacherLastname],
	[ScienceTeacherFirstname],
	[ScienceTeacherMiddlename],
	
	[TeacherFullname],
	[TeacherLastname],
	[TeacherFirstname],
	[TeacherMiddlename],
	
	ClassListingFormId
	
from	#tmpCleanedStudentData
Where FileId = @FileId
order by [row]


select *
from	tblEfileCleanedStudentData
Where FileId = @FileId
order by [row]
go

grant execute on usp_EfileVerificationProcess_ConvertedStudentData to TIMSSSCS2015_app
go
--grant execute on usp_EfileVerificationProcess_ConvertedStudentData to NAEP_admin
--go
/* Covert there codes to naep codes */
/*
declare curCodeMapping Cursor STATIC For

select ColumnSeq, Response, NaepLabel, CodeValue 
from uvw_EfileLinkedResponses 
Where Fileid = @FileId and naeplabel not in ('Student Name: Last','Student Name: First','Student Name: Middle', 'Homeroom or Other Locator','Birth Date: Month of Birth','Birth Date: Year of Birth')

open curCodeMapping

Fetch Next from curCodeMapping into @ColumnSeq, @Response, @NaepLabel, @CodeValue

--print '  '+cast(@@CURSOR_ROWS as varchar(10))+ ' Records found....'
--print ''
set @cnt = 0

While (@@Fetch_Status <> -1)
begin
	set @cnt = @cnt + 1
	set @column = ''

	if @NaepLabel = 'Student with a Disability' 
		set @column = 'SD'
	if @NaepLabel = 'English Language Learner'
		set @column = 'ELL'
	if @NaepLabel = 'Sex'
		 set @column = 'SEX'
	if @NaepLabel = 'Race/Ethnicity'
		 set @column = 'RACE'
	if @NaepLabel = 'Title I'
		 set @column = 'Title1'
	if @NaepLabel = 'School Lunch'
		set @column = 'SchoolLunch'
	print @NaepLabel

	set @strsql = 'Update #tmpCleanedStudentData Set ['+@column+']='''+@CodeValue+''' Where FileId = '+cast(@FileId as varchar(20))+' and row in (Select Row from tblEfileStudentData Where Fileid = '+cast(@FileId as varchar(20))+'  and c'+cast(@ColumnSeq as varchar(10))+' = '''+@Response+''')'

	print @strsql
	exec(@strsql)

	Fetch Next from curCodeMapping into @ColumnSeq, @Response, @NaepLabel, @CodeValue
end

close curCodeMapping
DEALLOCATE curCodeMapping
*/
/*
Insert into tblEfileCleanedStudentData (FileId, fldProjectID, ID, Row, Lastname, Firstname, Middlename, Homeroom, SD, ELL, MOB, YOB, Sex, Race, White, Black, Asian, 
                      [Native Hawaiian/Pacific Islander], [American Indian/Alaska Native], Hispanic, Title1, Onbreak, SchoolLunch, Grade, SESSID, AdminLineNumber, 
                      SLFLineNumber, wesroom, SD_ELL, Sampled)
select FileId, fldProjectID, ID, Row, Lastname, Firstname, Middlename, Homeroom, SD, ELL, MOB, YOB, Sex, Race, White, Black, Asian, 
                      [Native Hawaiian/Pacific Islander], [American Indian/Alaska Native], Hispanic, Title1, Onbreak, SchoolLunch, Grade, SESSID, AdminLineNumber, 
                      SLFLineNumber, wesroom, SD_ELL, Sampled from #tmpCleanedStudentData

drop table #tmpCleanedStudentData
*/