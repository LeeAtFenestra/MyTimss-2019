USE [TIMSSSCS2015]
GO


IF OBJECT_ID (N'tblEfileResponseFreq') IS NOT NULL
    DROP TABLE dbo.tblEfileResponseFreq
GO

Create table dbo.tblEfileResponseFreq
	(ResponseFreqId					 int IDENTITY(1,1) NOT NULL
	,UserColumnId					int not null
	,Response					varchar(250) null
	,TotalResponsesGrade4				int not null default(0)
	,TotalResponsesGrade8				int not null default(0)
	,TotalResponsesGrade12				int not null default(0)
	,NaepCodeId					int null
	,ClassListingFormId					int null
	,EditDT					datetime null
, CONSTRAINT [PK_tblEfileResponseFreq] PRIMARY KEY
(
	[ResponseFreqId] ASC
)
)
go

ALTER TABLE [dbo].tblEfileResponseFreq  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileResponseFreq_tblEfileUserColumns] FOREIGN KEY([UserColumnId])
REFERENCES [dbo].[tblEfileUserColumns] ([UserColumnId])
GO

ALTER TABLE [dbo].tblEfileResponseFreq  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileResponseFreq_tblEfileNaepCodes] FOREIGN KEY([NaepCodeId])
REFERENCES [dbo].[tblEfileNaepCodes] ([NaepCodeId])
GO

ALTER TABLE [dbo].tblEfileResponseFreq  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileResponseFreq_tblClassListingForm] FOREIGN KEY([ClassListingFormId])
REFERENCES [dbo].[tblClassListingForm] ([ClassListingFormId])
GO

ALTER TABLE [dbo].[tblEfileResponseFreq] CHECK CONSTRAINT [FK_tblEfileResponseFreq_tblEfileUserColumns]
GO
