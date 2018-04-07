use TIMSSSCS2015
go


if OBJECT_ID('dbo.tblIncentiveCheckBatch') > 0
	drop table dbo.tblIncentiveCheckBatch
go

CREATE TABLE dbo.tblIncentiveCheckBatch (
	[IncentiveCheckBatchID] int identity Primary Key NOT NULL
	,[IncentiveCheckBatchDate] datetime NOT NULL
)
go