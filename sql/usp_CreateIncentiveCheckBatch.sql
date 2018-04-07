use TIMSSSCS2015
go

/*

--ALTER TABLE tblCaseinfo DROP CONSTRAINT fk_tblCaseinfo_IncentiveCheckBatchID_tblIncentiveCheckBatch_IncentiveCheckBatchID
ALTER TABLE tblCaseinfo DROP CONSTRAINT fk_tblCaseinfo_IncentiveCheckBatchID_tblIncentiveCheckBatch_PrincipalIncentiveCheckBatchID
go

ALTER TABLE tblCaseinfo DROP CONSTRAINT fk_tblCaseinfo_IncentiveCheckBatchID_tblIncentiveCheckBatch_SCIncentiveCheckBatchID
go

alter table tblCaseinfo drop column PrincipalIncentiveCheckBatchID;
go

alter table tblCaseinfo drop column SCIncentiveCheckBatchID;
go

if OBJECT_ID('tblIncentiveCheckBatch') > 0
	drop table tblIncentiveCheckBatch
go

CREATE TABLE tblIncentiveCheckBatch (
	[IncentiveCheckBatchID] int identity Primary Key NOT NULL
	,[IncentiveCheckBatchDate] datetime NOT NULL
)
go

alter table tblCaseinfo add PrincipalIncentiveCheckBatchID int NULL;
go

alter table tblCaseinfo add SCIncentiveCheckBatchID int NULL;
go

ALTER TABLE tblCaseinfo ADD CONSTRAINT fk_tblCaseinfo_IncentiveCheckBatchID_tblIncentiveCheckBatch_PrincipalIncentiveCheckBatchID FOREIGN KEY (PrincipalIncentiveCheckBatchID) REFERENCES tblIncentiveCheckBatch(IncentiveCheckBatchID)
go

ALTER TABLE tblCaseinfo ADD CONSTRAINT fk_tblCaseinfo_IncentiveCheckBatchID_tblIncentiveCheckBatch_SCIncentiveCheckBatchID FOREIGN KEY (SCIncentiveCheckBatchID) REFERENCES tblIncentiveCheckBatch(IncentiveCheckBatchID)
go

update tblSCSGrade
set PrincipalIncentiveCheckBatchID = null
	,SCIncentiveCheckBatchID = null
where PrincipalIncentiveCheckBatchID is not null
or SCIncentiveCheckBatchID is not null

exec usp_CreateIncentiveCheckBatch
select * from tblIncentiveCheckBatch

--bcp "exec usp_CreateIncentiveCheckBatch" queryout "C:\Projects\2014\TIMSS\MyTable.csv" -c -t , -S TESTSQL9 -T -d TIMSS2010DEV

SQLCMD -Q "select * from tblIncentiveCheckBatch"

--sqlcmd -S TESTSQL9 -d TIMSS2010DEV -E -Q "exec usp_CreateIncentiveCheckBatch" -s "," -o "C:\Projects\2014\TIMSS\MyData.csv" 

--SQLCMD -S TESTSQL9 -d TIMSS2010DEV -E -Q "exec usp_CreateIncentiveCheckBatch" -s "," -o "C:\Projects\2014\TIMSS\MyData.csv" 

--exec master..xp_cmdshell 'bcp "exec usp_CreateIncentiveCheckBatch" queryout "C:\Projects\2014\TIMSS\MyTable.csv" -c -t , -S TESTSQL9 -T -d TIMSS2010DEV'

*/

if OBJECT_ID('usp_CreateIncentiveCheckBatch') > 0
	drop proc usp_CreateIncentiveCheckBatch
go

Create PROCEDURE [dbo].[usp_CreateIncentiveCheckBatch]

AS
/*
if OBJECT_ID('tempdb..#tempChecks') > 0
	drop table #tempChecks
go
*/

SET NOCOUNT ON

declare @IncentiveCheckBatchID int
insert into tblIncentiveCheckBatch (IncentiveCheckBatchDate) values (GETDATE())
SET @IncentiveCheckBatchID = scope_identity()

select	a.[ID]
		,a.[Amount]
		,a.[DispCode]
		,a.[Name1]
		,a.[Name2]
		,a.[Address1]
		,a.[Address2]
		,a.[City]
		,a.[State]
		,a.[Zip]
		,a.[School Phone Number]
		,a.[Project]
		,@IncentiveCheckBatchID IncentiveCheckBatchID
		,a.[Grade]
		,a.[TIMSS ID]
		,[CheckType]
into	#tempChecks
from	(
		select	ID [ID]
				,400.00 [Amount]
				,Disp [DispCode]
				,SchoolIncentiveCheckSentTxt [Name1]
				,S_Name [Name2]
				,S_Addr1 [Address1]
				,S_Addr2 [Address2]
				,S_City [City]
				,S_State [State]
				,S_Zip [Zip]
				,S_Phone [School Phone Number]
				,'' [Project]
				,SMPGRD [Grade]
				,id [TIMSS ID]
				--,PrincipalIncentiveCheckBatchID IncentiveCheckBatchID
				,'School/Principal' [CheckType]
		from	uv_Customize 
		Where	PrincipalIncentiveCheckBatchID is null
		and		AssessmentCompleted = 'YES'
		
		union all

		select	ID [ID]
				,100.00 [Amount]
				,Disp [DispCode]
				,SCIncentiveCheckSentTxt [Name1]
				,S_Name [Name2]
				,S_Addr1 [Address1]
				,S_Addr2 [Address2]
				,S_City [City]
				,S_State [State]
				,S_Zip [Zip]
				,S_Phone [School Phone Number]
				,'' [Project]
				,SMPGRD [Grade]
				,id [TIMSS ID]
				--,SCIncentiveCheckBatchID IncentiveCheckBatchID
				,'School Coordinator' [CheckType]
		from	uv_Customize
		Where	SCIncentiveCheckBatchID is null
		and		AssessmentCompleted = 'YES'
		) a
where	a.[DispCode] in ('20', '21', '22') --Production
and		a.[State] not in ('IN')
Order by a.[ID]
		,a.Amount
		
Update	tblSCSGrade
set		PrincipalIncentiveCheckBatchID = @IncentiveCheckBatchID
where	ID in (select	distinct id from #tempChecks where CheckType = 'School/Principal')

Update	tblSCSGrade
set		SCIncentiveCheckBatchID = @IncentiveCheckBatchID
where	ID in (select	distinct id from #tempChecks where CheckType = 'School Coordinator')
/*
select	'ID' [ID]
		,'Amount' [Amount]
		,'DispCode' DispCode
		,'Name1' [Name1]
		,'Name2' [Name2]
		,'Address1' [Address1]
		,'Address2' [Address2]
		,'City' [City]
		,'State' [State]
		,'Zip' [Zip]
		,'Project' [Project]
		,'IncentiveCheckBatchID' [IncentiveCheckBatchID]
		,'CheckType' [CheckType]
		union all
select	'''' + a.[ID]
		,'''' + cast(a.[Amount] as varchar)
		,'''' + a.[DispCode]
		,'''' + a.[Name1]
		,'''' + a.[Name2]
		,'''' + a.[Address1]
		,'''' + a.[Address2]
		,'''' + a.[City]
		,'''' + a.[State]
		,'''' + a.[Zip]
		,'''' + a.[Project]
		,cast(a.[IncentiveCheckBatchID] as varchar)
		,'''' + a.[CheckType]
from	#tempChecks a
*/

select	'''' + a.[TIMSS ID] [TIMSS ID]
		,a.[Grade]
		,a.[Amount]
		,a.[DispCode]
		,a.[Name1]
		,a.[Name2]
		,a.[Address1]
		,isnull(a.[Address2], '') [Address2]
		,a.[City]
		,a.[State]
		,'''' + a.[Zip] [Zip]
		,a.[School Phone Number]
		,a.[Project]
		,a.[CheckType]
		,a.[IncentiveCheckBatchID]
from	#tempChecks a
Order by a.[TIMSS ID]
		,a.Amount