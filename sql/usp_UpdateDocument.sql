USE [TIMSSSCS2015]
GO

if OBJECT_ID('usp_UpdateDocument') > 0
	drop proc usp_UpdateDocument
go

CREATE PROCEDURE [dbo].[usp_UpdateDocument] 

@Which [nvarchar](20)
,@ID [char](7) = NULL
,@UploadedBy [uniqueidentifier] = NULL
,@Filename nvarchar(100) = null
,@Filesize int = null
,@FileData varbinary(max) = null
,@ContentType nvarchar(100) = null
,@RC int = null output
AS
/*
select *
from	tblGradeFiles
where	STFFileData is not null

select *
from	tblSCSGrade
where STLFGradeFileId is not null

declare @RC int
exec [usp_UpdateDocument] 
@Which = 'STLF'
,@ID = '402033I'
,@UploadedBy = '45001aa5-73d9-4d76-89a4-ae0542cc3398'
,@Filename = 'STF.zip'
,@Filesize = 500
,@FileData = null
,@ContentType = 'application/x-zip-compressed'
,@RC = @RC output

select @RC [@RC]
*/

declare @GradeFileId int


if @FileData is not null and @ID is not null
begin
	insert into tblGradeFiles (ID
	,[FilenType]
	,[UploadedBy]
	,[Filename]
	,[Filesize]
	,[FileData]
	,[ContentType])
	select @ID
	,@Which
	,@UploadedBy
	,@Filename
	,@Filesize
	,@FileData
	,@ContentType

	set @GradeFileId = scope_identity()
end



SET NOCOUNT ON
if @Which = 'STLF'
begin
	Update tblSCSGrade
	set STLFGradeFileId = @GradeFileId
	where ID = @ID
	set @RC = @@ROWCOUNT
end
if @Which = 'TTF'
begin
	Update tblSCSGrade
	set TTFGradeFileId = @GradeFileId
	where ID = @ID
	set @RC = @@ROWCOUNT
end

go

grant execute on usp_UpdateDocument to TIMSSSCS2015_app
go
