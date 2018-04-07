USE [TIMSSSCS2015]
GO
if OBJECT_ID('tblEfileUserColumns') > 0 
	drop table tblEfileUserColumns
go
/****** Object:  Table [dbo].[tblEfileUserColumns]    Script Date: 11/17/2014 15:12:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblEfileUserColumns](
	[UserColumnId] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[ColumnSeq] [int] NOT NULL,
	[UserColumnLabel] [varchar](250) NULL,
	[NaepLabelId] [int] NULL
	,EditDT					datetime null
 ,CONSTRAINT [PK_tblEfileUserColumns] PRIMARY KEY CLUSTERED 
(
	[UserColumnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [IX_NoDupColumnSeq] UNIQUE NONCLUSTERED 
(
	[FileId] ASC,
	[ColumnSeq] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblEfileUserColumns]  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileUserColumns_tblEfileUploads] FOREIGN KEY([FileId])
REFERENCES [dbo].[tblEfileUploads] ([FileId])
GO

ALTER TABLE [dbo].[tblEfileUserColumns]  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileUserColumns_tblEfileNaepLabels] FOREIGN KEY([NaepLabelId])
REFERENCES [dbo].[tblEfileNaepLabels] ([NaepLabelId])
GO

ALTER TABLE [dbo].[tblEfileUserColumns] CHECK CONSTRAINT [FK_tblEfileUserColumns_tblEfileUploads]
GO


