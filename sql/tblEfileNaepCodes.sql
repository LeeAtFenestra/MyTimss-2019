USE [TIMSSSCS2015]
GO

if OBJECT_ID('tblEfileNaepCodes') > 0 
	drop table tblEfileNaepCodes
go

/****** Object:  Table [dbo].[tblEfileNaepCodes]    Script Date: 11/21/2014 15:20:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblEfileNaepCodes](
	[NaepCodeId] [int] IDENTITY(1,1) NOT NULL,
	[NaepLabelId] [int] NOT NULL,
	[CodeDisplayOrder] [int] NOT NULL,
	[CodeLabel] [varchar](75) NOT NULL,
	[CodeValue] [int] NULL,
	[CodeIsVisible] [bit] NOT NULL,
	[IsLTTCode] [bit] NOT NULL,
 CONSTRAINT [PK_tblEfileNaepCodes] PRIMARY KEY CLUSTERED 
(
	[NaepCodeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [NoDupCodeLabel] UNIQUE NONCLUSTERED 
(
	[NaepLabelId] ASC,
	[CodeLabel] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [NoDupDisplayOrder] UNIQUE NONCLUSTERED 
(
	[NaepLabelId] ASC,
	[CodeDisplayOrder] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Table''s autonum primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes', @level2type=N'COLUMN',@level2name=N'NaepCodeId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Links the code to NAEP Label' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes', @level2type=N'COLUMN',@level2name=N'NaepLabelId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Display order' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes', @level2type=N'COLUMN',@level2name=N'CodeDisplayOrder'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Code label' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes', @level2type=N'COLUMN',@level2name=N'CodeLabel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NAEP value' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes', @level2type=N'COLUMN',@level2name=N'CodeValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identifies if could should be shown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes', @level2type=N'COLUMN',@level2name=N'CodeIsVisible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Stores a NAEP codes which will be linked to the unique values in each column' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblEfileNaepCodes'
GO

ALTER TABLE [dbo].[tblEfileNaepCodes]  WITH NOCHECK ADD  CONSTRAINT [FK_tblEfileNaepCodes_tblEfileNaepLabels] FOREIGN KEY([NaepLabelId])
REFERENCES [dbo].[tblEfileNaepLabels] ([NaepLabelId])
GO

ALTER TABLE [dbo].[tblEfileNaepCodes] CHECK CONSTRAINT [FK_tblEfileNaepCodes_tblEfileNaepLabels]
GO

Set nocount on
set identity_insert tblEfileNaepCodes on

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (1, 8, 1, 'MDY', NULL, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (2, 8, 2, 'MY', NULL, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (3, 8, 3, 'DMY', NULL, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (4, 8, 4, 'YMD', NULL, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (5, 8, 5, 'YM', NULL, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (9, 18, 7, 'Grade 4', 4, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (13, 18, 11, 'Grade 8', 8, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (17, 18, 15, 'Grade 12', 12, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (19, 15, 2, 'Yes, ELL', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (20, 15, 3, 'No, not ELL', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (22, 16, 2, 'Yes, SD', 1, 0, 0)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (23, 16, 3, 'No, not SD', 3, 0, 0)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (25, 14, 2, 'Yes, student receives Title I services', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (27, 17, 1, 'Information unavailable at this time', 0, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (28, 17, 2, 'Not On Break', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (29, 17, 3, 'On Break', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (38, 12, 2, 'White, not Hispanic', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (39, 12, 3, 'Black or African American, not Hispanic', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (40, 12, 1, 'Hispanic, of any race', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (41, 12, 4, 'Asian, not Hispanic', 4, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (42, 12, 5, 'American Indian or Alaska Native, not Hispanic', 5, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (43, 12, 7, 'Two or More Races (not Hispanic)', 7, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (45, 13, 2, 'Student not eligible', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (46, 13, 3, 'Free lunch', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (47, 13, 4, 'Reduced price lunch', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (48, 13, 6, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (49, 13, 5, 'School not participating', 4, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (50, 11, 1, 'N/A', 0, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (51, 11, 2, 'Male', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (52, 11, 3, 'Female', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (53, 3, 1, 'First Middle Last', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (54, 3, 2, 'Last, First Middle', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (55, 18, 1, 'N/A', 0, 0, 0)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (56, 12, 9, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (58, 15, 4, 'No, Formerly ELL (and monitored for AYP reporting)', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (59, 15, 9, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (60, 16, 9, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (61, 14, 3, 'No, student does not receive Title I services', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (62, 14, 4, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (63, 12, 8, 'School does not collect this information', 8, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (64, 20, 1, 'Yes, White', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (67, 20, 3, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (68, 21, 1, 'Yes, Black or African-American', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (69, 21, 2, 'No, not Black or African-American', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (70, 21, 3, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (71, 22, 1, 'Yes, Asian', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (72, 22, 2, 'No, Not Asian', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (73, 22, 3, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (74, 23, 1, 'Yes, Native Hawaiian or Pacific Islander', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (75, 23, 2, 'No, not Native Hawaiian or Pacific Islander', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (76, 23, 3, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (77, 24, 1, 'Yes, American Indian or Alaska Native', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (78, 24, 2, 'No, not American Indian or Alaska Native', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (79, 24, 3, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (80, 25, 1, 'Yes, Hispanic', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (81, 25, 2, 'No, not Hispanic', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (82, 25, 3, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (84, 18, 2, 'Pre Kindergarten', -2, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (85, 18, 3, 'Kindergarten', -1, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (86, 18, 4, 'Grade 1', 1, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (87, 18, 5, 'Grade 2', 2, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (88, 18, 6, 'Grade 3', 3, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (89, 18, 8, 'Grade 5', 5, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (90, 18, 9, 'Grade 6', 6, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (91, 18, 10, 'Grade 7', 7, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (92, 18, 12, 'Grade 9', 9, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (93, 18, 13, 'Grade 10', 10, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (94, 18, 14, 'Grade 11', 11, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (95, 16, 1, 'Yes, IEP', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (100, 16, 5, 'Yes, 504', 2, 0, 0)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (101, 16, 6, 'Yes, IEP/504 plan in process', 4, 0, 0)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (104, 16, 7, 'No,  not SD', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (107, 12, 6, 'Native Hawaiian or Pacific Islander, not Hispanic', 6, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (108, 20, 2, 'No, not White', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (109, 15, 6, 'Yes, LSP', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (117, 15, 7, 'No, not LSP', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (118, 28, 1, 'White, not Hispanic', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (119, 28, 2, 'Black or African American, not Hispanic', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (120, 28, 3, 'Hispanic, of any race', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (121, 28, 4, 'Asian, not Hispanic', 4, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (122, 28, 5, 'American Indian or Alaska Native, not Hispanic', 5, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (123, 28, 6, 'Native Hawaiian or Pacific Islander, not Hispanic', 6, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (124, 28, 7, 'Two or More Races (not Hispanic)', 7, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (125, 28, 8, 'School does not collect this information', 8, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (126, 28, 9, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (134, 29, 2, 'Yes, ELL', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (135, 29, 3, 'No, not ELL', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (138, 29, 4, 'No, Formerly ELL (and monitored for AYP reporting)', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (139, 29, 9, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (140, 29, 6, 'Yes, LSP', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (141, 29, 7, 'No, not LSP', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (142, 30, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (143, 30, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (144, 30, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (145, 31, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (146, 31, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (147, 31, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (148, 32, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (149, 32, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (150, 32, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (151, 33, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (152, 33, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (153, 33, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (154, 34, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (155, 34, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (156, 34, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (157, 35, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (158, 35, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (159, 35, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (160, 36, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (161, 36, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (162, 36, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (163, 37, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (164, 37, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (165, 37, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (166, 38, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (167, 38, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (168, 38, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (169, 39, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (170, 39, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (171, 39, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (172, 40, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (173, 40, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (174, 40, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (175, 41, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (176, 41, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (177, 41, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (178, 42, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (179, 42, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (180, 42, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (181, 43, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (182, 43, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (183, 43, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (184, 44, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (185, 44, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (186, 44, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (187, 45, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (188, 45, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (189, 45, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (190, 46, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (191, 46, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (192, 46, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (193, 47, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (194, 47, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (195, 47, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (196, 48, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (197, 48, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (198, 48, 3, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (200, 53, 1, 'English Only', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (203, 53, 2, 'I-FEP', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (204, 53, 3, 'English Learner', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (205, 53, 4, 'R-FEP', 4, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (206, 53, 5, 'N/A', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (207, 55, 1, 'YES', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (208, 55, 2, 'NO', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (210, 55, 3, 'Grade 4 or not R-FEP', 3, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (211, 51, 1, 'Yes, Hispanic', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (212, 51, 2, 'No, not Hispanic', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (213, 51, 9, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (214, 52, 1, 'White', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (215, 52, 2, 'Black or African American', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (216, 52, 3, 'Asian', 4, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (218, 52, 4, 'American Indian or Alaska Native', 5, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (220, 52, 5, 'Native Hawaiian or Pacific Islander', 6, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (223, 52, 6, 'Two or More Races', 7, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (224, 52, 7, 'School does not collect this information', 8, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (225, 52, 8, 'Information unavailable at this time', 9, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (226, 55, 4, 'N/A', 4, 1, 1)


insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (227, 84, 1, 'N/A', 0, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (228, 84, 2, 'Male', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (229, 84, 3, 'Female', 2, 1, 1)


insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (230, 86, 1, 'N/A', 0, 0, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (231, 86, 2, 'Language Arts', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (232, 86, 3, 'Social Studies', 2, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (233, 86, 4, 'Mathematics & Sciences', 1, 1, 1)
insert tblEfileNaepCodes ([NaepCodeId], [NaepLabelId], [CodeDisplayOrder], [CodeLabel], [CodeValue], [CodeIsVisible], [IsLTTCode]) values (234, 86, 5, 'Other', 2, 1, 1)

set identity_insert tblEfileNaepCodes off