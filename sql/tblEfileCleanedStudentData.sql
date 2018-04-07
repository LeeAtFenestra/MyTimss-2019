USE [TIMSSSCS2015]
GO
/*

select * from dbo.tblEfileCleanedStudentData
*/
IF OBJECT_ID ('dbo.tblEfileCleanedStudentData') > 0
    DROP TABLE dbo.tblEfileCleanedStudentData
go

CREATE TABLE dbo.tblEfileCleanedStudentData (
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
	, CONSTRAINT [PK_tblEfileCleanedStudentData_EfileCleanStudentId] PRIMARY KEY
(
	EfileCleanStudentId ASC
)

) ON [PRIMARY]
go

ALTER TABLE tblEfileCleanedStudentData ADD 
	CONSTRAINT [DF_tblEfileCleanedStudentDataa_SD] DEFAULT (0) FOR [SD],
	CONSTRAINT [DF_tblEfileCleanedStudentData_ELL] DEFAULT (0) FOR [ELL],
	CONSTRAINT [DF_tblEfileCleanedStudentData_Sex] DEFAULT (0) FOR [Sex],
	CONSTRAINT [DF_tblEfileCleanedStudentData_SD_ELL] DEFAULT (0) FOR [SD_ELL],
	CONSTRAINT [DF_tblEfileCleanedStudentData_Sampled] DEFAULT (0) FOR [Sampled],
	CONSTRAINT [IX_NoDupRowInFile] UNIQUE  NONCLUSTERED 
	(
		[FileId],
		[fldProjectID],
		[ID],
		[Row]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
	
ALTER TABLE [dbo].[tblEfileCleanedStudentData]  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileCleanedStudentData_tblEfileUploads] FOREIGN KEY([FileId])
REFERENCES [dbo].[tblEfileUploads] ([FileId])
GO

ALTER TABLE [dbo].[tblEfileCleanedStudentData]  WITH NOCHECK ADD  CONSTRAINT [tblEfileCleanedStudentData_tblClassListingForm] FOREIGN KEY(ClassListingFormId)
REFERENCES [dbo].[tblClassListingForm] ([ClassListingFormId])
GO

ALTER TABLE [dbo].[tblEfileCleanedStudentData] CHECK CONSTRAINT [FK_tblEfileCleanedStudentData_tblEfileUploads]
GO

ALTER TABLE [dbo].[tblEfileCleanedStudentData] CHECK CONSTRAINT [tblEfileCleanedStudentData_tblClassListingForm]
GO
