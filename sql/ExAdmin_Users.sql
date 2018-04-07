USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[ExAdmin_Users]    Script Date: 01/31/2017 10:53:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ExAdmin_Users]
AS
SELECT     u.UID, u.fname, u.lname, u.UserName, u.Password, u.LastLogin, u.editdate, u.CreateDate, u.memtype, u.Title, u.visible, u.Email, u.Organization, 
                      u.addr_1, u.addr_2, u.addr_3, u.City, u.State, u.Zip, u.phone, u.Extension, u.Fax, u.browser, u.altPhone, u.fldStaffID, u.flgDeleted, u.screenWidth, 
                      u.screenHeight, u.screenDepth, u.emailStatus, u.currentview, u.browserApp, u.browserVer, u.orig_delete, ISNULL(u.fname, '') + ' ' + ISNULL(u.lname, '') 
                      AS Expr1, u.fldStaffid AS Expr2, u.fipst, u.sYear, u.IsSchoolCoord, u.mtid, u.memtypename, u.LEAID
FROM         NAEPAdminNET.dbo.uv_tblUsers AS u /*LEFT OUTER JOIN
                      NAEPAdminNET.dbo.ExFrs_StaffUID AS s ON u.UID = s.fldUID*/
WHERE     (u.flgDeleted = 0)

GO


