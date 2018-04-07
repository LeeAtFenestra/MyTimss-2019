USE [TIMSSSCS2015]
GO

--drop view [uv_AccountDetails]
ALTER VIEW [dbo].[uv_AccountDetails]
AS
/*
This view combines the asp.net user, membership and the timss custom profile table
*/
select	a.[ApplicationId]
		,a.ApplicationName
		,u.[UserId]
		,u.[UserName]
		,m.Comment
		,m.CreateDate CreationDate
		,m.Email
		,m.IsApproved
		,m.IsLockedOut
		,m.LastLockoutDate
		,m.LastLoginDate
		,m.LastPasswordChangedDate
		,p.FirstName
		,p.LastArea
		,p.LastName
		,p.LastPageSize
		,p.LastRegion
		,p.LastUpdatedDate
		,p.MIDDLENAME
		,p.PREFIX
		,p.PROJECTSTAFFID
		,p.ProfileVersion
		,p.WINSID
		,ph.CreateDate
		,p.REPSBGRP
		,p.Telephone
		,p.TelephoneExtension
		,p.RegistrationId
		,p.Frame_N_
		,p.TUA_LEA
from	aspnet_Applications a
join	[vw_aspnet_Users] u
on		u.ApplicationId = a.ApplicationId
join	[aspnet_Membership] m
on		m.ApplicationId = u.ApplicationId
and		m.UserId = u.UserId
join	(select UserId, max(CreateDate) CreateDate from FAB_PasswordHistory group by UserId) ph
on		ph.UserId = u.UserId
left outer join	[aspnet_ProfileTIMSS] p
on		p.UserID = u.UserId


go

grant select on [uv_AccountDetails] to TIMSSSCS2015_app
go