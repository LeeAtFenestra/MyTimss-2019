USE [TIMSSSCS2015]
GO
if OBJECT_ID('tblEfileStudentData') > 0 
	drop table tblEfileStudentData
go

/****** Object:  Table [dbo].[tblEfileStudentData]    Script Date: 11/17/2014 14:11:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblEfileStudentData](
	[FileId] [int] NOT NULL,
	[Row] [int] NOT NULL,
	[Frame_N_] [char](6) NOT NULL,
	[fldProjectID] [int] NOT NULL,
	[ID] [char](7) NOT NULL,
	[SmpGrd] [int] NOT NULL,
	[C0] [varchar](250) NULL,
	[C1] [varchar](250) NULL,
	[C2] [varchar](250) NULL,
	[C3] [varchar](250) NULL,
	[C4] [varchar](250) NULL,
	[C5] [varchar](250) NULL,
	[C6] [varchar](250) NULL,
	[C7] [varchar](250) NULL,
	[C8] [varchar](250) NULL,
	[C9] [varchar](250) NULL,
	[C10] [varchar](250) NULL,
	[C11] [varchar](250) NULL,
	[C12] [varchar](250) NULL,
	[C13] [varchar](250) NULL,
	[C14] [varchar](250) NULL,
	[C15] [varchar](250) NULL,
	[C16] [varchar](250) NULL,
	[C17] [varchar](250) NULL,
	[C18] [varchar](250) NULL,
	[C19] [varchar](250) NULL,
	[C20] [varchar](250) NULL,
	[C21] [varchar](250) NULL,
	[C22] [varchar](250) NULL,
	[C23] [varchar](250) NULL,
	[C24] [varchar](250) NULL,
	[C0ID] [int] NULL,
	[C1ID] [int] NULL,
	[C2ID] [int] NULL,
	[C3ID] [int] NULL,
	[C4ID] [int] NULL,
	[C5ID] [int] NULL,
	[C6ID] [int] NULL,
	[C7ID] [int] NULL,
	[C8ID] [int] NULL,
	[C9ID] [int] NULL,
	[C10ID] [int] NULL,
	[C11ID] [int] NULL,
	[C12ID] [int] NULL,
	[C13ID] [int] NULL,
	[C14ID] [int] NULL,
	[C15ID] [int] NULL,
	[C16ID] [int] NULL,
	[C17ID] [int] NULL,
	[C18ID] [int] NULL,
	[C19ID] [int] NULL,
	[C20ID] [int] NULL,
	[C21ID] [int] NULL,
	[C22ID] [int] NULL,
	[C23ID] [int] NULL,
	[C24ID] [int] NULL,
	[C25] [varchar](250) NULL,
	[C26] [varchar](250) NULL,
	[C27] [varchar](250) NULL,
	[C28] [varchar](250) NULL,
	[C29] [varchar](250) NULL,
	[C30] [varchar](250) NULL,
	[C31] [varchar](250) NULL,
	[C32] [varchar](250) NULL,
	[C33] [varchar](250) NULL,
	[C34] [varchar](250) NULL,
	[C35] [varchar](250) NULL,
	[C36] [varchar](250) NULL,
	[C37] [varchar](250) NULL,
	[C38] [varchar](250) NULL,
	[C39] [varchar](250) NULL,
	[C40] [varchar](250) NULL,
	[C41] [varchar](250) NULL,
	[C42] [varchar](250) NULL,
	[C43] [varchar](250) NULL,
	[C44] [varchar](250) NULL,
	[C45] [varchar](250) NULL,
	[C46] [varchar](250) NULL,
	[C47] [varchar](250) NULL,
	[C48] [varchar](250) NULL,
	[C49] [varchar](250) NULL,
	[C50] [varchar](250) NULL,
	[C51] [varchar](250) NULL,
	[C52] [varchar](250) NULL,
	[C53] [varchar](250) NULL,
	[C54] [varchar](250) NULL,
	[C55] [varchar](250) NULL,
	[C56] [varchar](250) NULL,
	[C57] [varchar](250) NULL,
	[C58] [varchar](250) NULL,
	[C59] [varchar](250) NULL,
	[C60] [varchar](250) NULL,
	[C61] [varchar](250) NULL,
	[C62] [varchar](250) NULL,
	[C63] [varchar](250) NULL,
	[C64] [varchar](250) NULL,
	[C65] [varchar](250) NULL,
	[C66] [varchar](250) NULL,
	[C67] [varchar](250) NULL,
	[C68] [varchar](250) NULL,
	[C69] [varchar](250) NULL,
	[C70] [varchar](250) NULL,
	[C71] [varchar](250) NULL,
	[C72] [varchar](250) NULL,
	[C73] [varchar](250) NULL,
	[C74] [varchar](250) NULL,
	[C75] [varchar](250) NULL,
	[C76] [varchar](250) NULL,
	[C77] [varchar](250) NULL,
	[C78] [varchar](250) NULL,
	[C79] [varchar](250) NULL,
	[C80] [varchar](250) NULL,
	[C81] [varchar](250) NULL,
	[C82] [varchar](250) NULL,
	[C83] [varchar](250) NULL,
	[C84] [varchar](250) NULL,
	[C85] [varchar](250) NULL,
	[C86] [varchar](250) NULL,
	[C87] [varchar](250) NULL,
	[C88] [varchar](250) NULL,
	[C89] [varchar](250) NULL,
	[C90] [varchar](250) NULL,
	[C91] [varchar](250) NULL,
	[C92] [varchar](250) NULL,
	[C93] [varchar](250) NULL,
	[C94] [varchar](250) NULL,
	[C95] [varchar](250) NULL,
	[C96] [varchar](250) NULL,
	[C97] [varchar](250) NULL,
	[C98] [varchar](250) NULL,
	[C99] [varchar](250) NULL,
	[C100] [varchar](250) NULL,
	[C101] [varchar](250) NULL,
	[C102] [varchar](250) NULL,
	[C103] [varchar](250) NULL,
	[C104] [varchar](250) NULL,
	[C105] [varchar](250) NULL,
	[C25ID] [int] NULL,
	[C26ID] [int] NULL,
	[C27ID] [int] NULL,
	[C28ID] [int] NULL,
	[C29ID] [int] NULL,
	[C30ID] [int] NULL,
	[C31ID] [int] NULL,
	[C32ID] [int] NULL,
	[C33ID] [int] NULL,
	[C34ID] [int] NULL,
	[C35ID] [int] NULL,
	[C36ID] [int] NULL,
	[C37ID] [int] NULL,
	[C38ID] [int] NULL,
	[C39ID] [int] NULL,
	[C40ID] [int] NULL,
	[C41ID] [int] NULL,
	[C42ID] [int] NULL,
	[C43ID] [int] NULL,
	[C44ID] [int] NULL,
	[C45ID] [int] NULL,
	[C46ID] [int] NULL,
	[C47ID] [int] NULL,
	[C48ID] [int] NULL,
	[C49ID] [int] NULL,
	[C50ID] [int] NULL,
	[C51ID] [int] NULL,
	[C52ID] [int] NULL,
	[C53ID] [int] NULL,
	[C54ID] [int] NULL,
	[C55ID] [int] NULL,
	[C56ID] [int] NULL,
	[C57ID] [int] NULL,
	[C58ID] [int] NULL,
	[C59ID] [int] NULL,
	[C60ID] [int] NULL,
	[C61ID] [int] NULL,
	[C62ID] [int] NULL,
	[C63ID] [int] NULL,
	[C64ID] [int] NULL,
	[C65ID] [int] NULL,
	[C66ID] [int] NULL,
	[C67ID] [int] NULL,
	[C68ID] [int] NULL,
	[C69ID] [int] NULL,
	[C70ID] [int] NULL,
	[C71ID] [int] NULL,
	[C72ID] [int] NULL,
	[C73ID] [int] NULL,
	[C74ID] [int] NULL,
	[C75ID] [int] NULL,
	[C76ID] [int] NULL,
	[C77ID] [int] NULL,
	[C78ID] [int] NULL,
	[C79ID] [int] NULL,
	[C80ID] [int] NULL,
	[C81ID] [int] NULL,
	[C82ID] [int] NULL,
	[C83ID] [int] NULL,
	[C84ID] [int] NULL,
	[C85ID] [int] NULL,
	[C86ID] [int] NULL,
	[C87ID] [int] NULL,
	[C88ID] [int] NULL,
	[C89ID] [int] NULL,
	[C90ID] [int] NULL,
	[C91ID] [int] NULL,
	[C92ID] [int] NULL,
	[C93ID] [int] NULL,
	[C94ID] [int] NULL,
	[C95ID] [int] NULL,
	[C96ID] [int] NULL,
	[C97ID] [int] NULL,
	[C98ID] [int] NULL,
	[C99ID] [int] NULL,
	[C100ID] [int] NULL,
	[C101ID] [int] NULL,
	[C102ID] [int] NULL,
	[C103ID] [int] NULL,
	[C104ID] [int] NULL,
	[C105ID] [int] NULL,
 CONSTRAINT [PK_tblEfileStudentData] PRIMARY KEY NONCLUSTERED 
(
	[FileId] ASC,
	[Row] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) --ON [Indexes]

GO

SET ANSI_PADDING OFF
GO


ALTER TABLE [dbo].[tblEfileStudentData]  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileStudentData_tblEfileUploads] FOREIGN KEY([FileId])
REFERENCES [dbo].[tblEfileUploads] ([FileId])
GO

ALTER TABLE [dbo].[tblEfileStudentData] CHECK CONSTRAINT [FK_tblEfileStudentData_tblEfileUploads]
GO


