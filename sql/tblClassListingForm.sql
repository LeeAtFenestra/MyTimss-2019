USE [TIMSSSCS2015]
GO

if OBJECT_ID('tblClassListingForm') > 0 
	drop table tblClassListingForm
go

CREATE TABLE [dbo].tblClassListingForm
(
ClassListingFormId int IDENTITY(1,1) NOT NULL
,[ID] char(7) not null
,ClassName  [nvarchar](100) NULL
,ClassGroup  [nvarchar](100) NULL
,NumberOfStudents  int NULL
,ClassExclusionStatus  [nvarchar](100) NULL
,NameOfMathematicsTeacher [nvarchar](500) NULL
,NameOfScienceTeacher [nvarchar](500) NULL
, CONSTRAINT [PK_tblClassListingForm] PRIMARY KEY
(
	[ClassListingFormId] ASC
)
)
go
/*
insert into tblClassListingForm ([ID], ClassName, ClassGroup, NumberOfStudents, ClassExclusionStatus, NameOfMathematicsTeacher, NameOfScienceTeacher)
select '2515066', 'Period1', null,15,null,'Ms. Knight', 'Ms. Knight'  union all
select '2515066', 'Period2', null,14,1,'Mr. Bill', 'Mr. Bill'  union all
select '2515066', 'Period3', null,16,2,'Mrs. Kirk', 'Mrs. Kirk'  union all
select '2515066', 'Period4', null,14,3,'Mr. Owens', 'Mr. Owens'  union all

select '4820205', 'Period1', null,15,null,'Ms. Knight', 'Ms. Knight'  union all
select '4820205', 'Period2', null,14,1,'Mr. Bill', 'Mr. Bill'  union all
select '4820205', 'Period3', null,16,2,'Mrs. Kirk', 'Mrs. Kirk'  union all
select '4820205', 'Period4', null,14,3,'Mr. Owens', 'Mr. Owens'

*/

/*
select b.ID, * 
from tblSCSSchool a
join tblSCSGrade b
on b.Frame_N_ = a.Frame_N_
where a.MyNAEPREGID = '50097054'
*/