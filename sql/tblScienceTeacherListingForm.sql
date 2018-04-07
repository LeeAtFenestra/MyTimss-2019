USE [TIMSSSCS2015]
GO

if OBJECT_ID('tblScienceTeacherListingForm') > 0 
	drop table tblScienceTeacherListingForm
go

CREATE TABLE [dbo].tblScienceTeacherListingForm
(
ScienceTeacherListingFormId int IDENTITY(1,1) NOT NULL
,[ID] char(7) not null
,ClassName  [nvarchar](100) NULL
,NameOfScienceTeacher [nvarchar](500) NULL
, CONSTRAINT [PK_ScienceTeacherListingFormId] PRIMARY KEY
(
	[ScienceTeacherListingFormId] ASC
)
)
go