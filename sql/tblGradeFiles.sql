USE [TIMSSSCS2015]
GO
--delete from tblGradeFiles
if OBJECT_ID('tblGradeFiles') > 0
	drop table tblGradeFiles
go

CREATE TABLE [dbo].tblGradeFiles(
[GradeFileId] int IDENTITY(1,1) NOT NULL
,[ID] [char](7) NOT NULL
,[FilenType] nvarchar(100) NOT  null
,[UploadedBy] [uniqueidentifier] NOT  NULL
,[Filename] nvarchar(100) NOT  null
,[Filesize] int NOT  null
,[FileData] varbinary(max) NOT  null
,[ContentType] nvarchar(100) NOT  null
,[UploadDT] datetime NOT  null default(getdate())
 CONSTRAINT [PK_tblGradeFiles] PRIMARY KEY CLUSTERED 
(
	[GradeFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
go

/*
ALTER TABLE [dbo].[tblGradeFiles]
ADD CONSTRAINT FK_tblGradeFiles_ID__tblSCSGrade FOREIGN KEY (ID) 
    REFERENCES dbo.tblSCSGrade (ID) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
go
*/

ALTER TABLE [dbo].[tblGradeFiles]
ADD CONSTRAINT FK_tblGradeFiles_UploadedBy_aspnet_Users FOREIGN KEY (UploadedBy) 
    REFERENCES dbo.aspnet_Users (UserID) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
go