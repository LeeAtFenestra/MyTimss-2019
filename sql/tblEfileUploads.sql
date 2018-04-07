USE [TIMSSSCS2015]
GO

if OBJECT_ID('tblEfileUploads') > 0 
	drop table tblEfileUploads
go

CREATE TABLE [dbo].tblEfileUploads
(
FileId int IDENTITY(1,1) NOT NULL
,EfileTypeId  int NOT NULL
,UploadedBy [uniqueidentifier] NOT NULL
,[UserFilePath] varchar(100) not null
,[Filesize] int not null
--,[ServerFilePath] varchar(500) not null
--,[ServerRelativePath] varchar(100) not null
,[FileData] varbinary(max) not null
,[UploadDT] datetime not null
,HasColumnHeader varchar(5) null


	,IsSynchronous					bit not null default(1)
	,TotalColumns					int not null default(0)
	,ExpectedRows					int not null default(0)
	,TotalRows					int not null default(0)
	,TotalRowsDeleted				int not null default(0)
	,TotalGrades					int not null default(0)
	,Total4thGrades					int not null default(0)
	,Total4thGradeRows				int not null default(0)
	,Total8thGrades					int not null default(0)
	,Total8thGradeRows				int not null default(0)
	,Total12thGrades					int not null default(0)
	,Total12thGradeRows				int not null default(0)
	,TotalOrphanGrades				int not null default(0)
	,TotalOrphanGradeRows				int not null default(0)
	,TotalWarnings					int not null default(0)
	,HasVerificationComments				bit not null default(0)
	,HasFilteredData					bit not null default(0)
	,TableObject					varchar(50) null
	,ExcelObjectWorksheetName			varchar(50) null

,[ID] char(7) null
,[Frame_N_] [char](6) NULL
,[fldProjectID] [int] NOT NULL
,[SmpGrd] [int] NULL
,[ContentType] varchar(100) not null

,VerificationDT datetime null
,VerificationComments varchar(5000) null

,EfileStatusLogId int null
,EfileStatusUID [uniqueidentifier] null
,EfileStatusFname varchar(100) null
,EfileStatusLname varchar(100) null
,EfileStatusCode varchar(4000) null
,EfileStatusCodeIsError tinyint null
,EfileStatusEditDT datetime null

,DPStatusLogId int null
,DPStatusUID [uniqueidentifier] null
,DPStatusFname varchar(100) null
,DPStatusLname varchar(100) null
,DPStatusCode varchar(4000) null
,DPStatusCodeIsError tinyint null
,DPStatusEditDT datetime null

,DCStatusLogId int null

,ProcessedDT datetime NULL
,ProcessingTime int NULL

, CONSTRAINT [PK_tblEfileUploads] PRIMARY KEY
(
	[FileId] ASC
)
)
go

ALTER TABLE [dbo].[tblEfileUploads]
ADD CONSTRAINT FK_tblEfileUploads_aspnet_Users FOREIGN KEY (UploadedBy) 
    REFERENCES dbo.aspnet_Users (UserID) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
go
ALTER TABLE [dbo].[tblEfileUploads]
ADD CONSTRAINT FK_tblEfileUploads_tblEfileTypes FOREIGN KEY (EfileTypeId) 
    REFERENCES dbo.tblEfileTypes (EfileTypeId) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
go
/*

select *
from	tblEfileUploads

*/