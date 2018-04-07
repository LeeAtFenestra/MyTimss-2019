use NAEPSCS2015
go
--drop view uv_EROC
CREATE VIEW [dbo].[uv_EROC] 
AS
select	a.EROCID
		,a.frame_n_
		,a.id
		,a.PersonContactTitle
		,a.PersonContacted
		,a.DateContacted
		,a.ContactMode
		,a.OutcomeOfTheCall
		,a.Disp
		,a.AdditionalNotes
		,a.CreateDT
		,b.DispName
		,coalesce(c.FirstName, '') + ' ' + coalesce(c.LastName, '') UpdatedbyFirstAndLastName
		,coalesce(c.LastName, '') + ', ' + coalesce(c.FirstName, '') UpdatedbyLastNameComaFirstName
from	tblEROC a
left outer join tblSCSGradeDispositionCodes b
on		b.fldProjectID = a.fldProjectID
and		b.DISP = a.Disp
left outer join aspnet_ProfileTIMSS c
on		c.UserId = a.Updatedby