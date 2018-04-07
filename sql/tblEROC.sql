use TIMSSSCS2015DEV
go
--drop table tblEROC
CREATE TABLE [dbo].[tblEROC](

	[EROCID] [int] IDENTITY(1,1) NOT NULL,
	[fldProjectID] int NULL,
	[frame_n_] [char](6) NULL,
	[id] [char](7) NULL,
	[PersonContactTitle] [varchar](50) NULL,
	[PersonContacted] [varchar](100) NULL,
	[DateContacted] [datetime] NULL,
	[ContactMode] [varchar](20) NULL,
	[OutcomeOfTheCall] [varchar](500) NULL,
	[Disp] [char](2) NULL,
	[AdditionalNotes] [varchar](1000) NULL,
	[Updatedby] [uniqueidentifier] NULL,
	[CreateDT] [datetime] not null
	
 CONSTRAINT [TBLEROC_PK] PRIMARY KEY NONCLUSTERED 
(
	[EROCID] ASC
)
)
go

ALTER TABLE [dbo].[tblEROC]
ADD CONSTRAINT FK_tblEROC_aspnet_Users FOREIGN KEY (Updatedby) 
    REFERENCES dbo.aspnet_Users (UserID) 
    ON DELETE CASCADE
    ON UPDATE CASCADE
;
go