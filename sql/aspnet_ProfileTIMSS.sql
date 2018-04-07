SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aspnet_ProfileTIMSS](
	[UserID] [uniqueidentifier] NOT NULL,
	[PROJECTSTAFFID] [nvarchar](16) NULL,
	[PREFIX] [nvarchar](4) NULL,
	[FirstName] [nvarchar](50) NULL,
	[MIDDLENAME] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[WINSID] [nvarchar](50) NULL,
	[LastPageSize] [int] NULL,
	[LastArea] [nvarchar](50) NULL,
	[LastRegion] [nvarchar](50) NULL,
	[REPSBGRP] [nvarchar](2) NULL,
	
	[Telephone] [nvarchar](30) NULL,
	[TelephoneExtension] [nvarchar](10) NULL,
	[RegistrationId] [nvarchar](30) NULL,
	
	[ProfileVersion] [int] NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_aspnet_ProfileTIMSS_UserID] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[aspnet_ProfileTIMSS]
ADD CONSTRAINT FK_aspnet_ProfileTIMSS_aspnet_Users FOREIGN KEY (UserID) 
    REFERENCES dbo.aspnet_Users (UserID) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
go