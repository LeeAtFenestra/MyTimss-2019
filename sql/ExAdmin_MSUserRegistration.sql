USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[ExAdmin_MSUserRegistration]    Script Date: 09/02/2014 09:13:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[ExAdmin_MSUserRegistration]
AS
SELECT     NAEPAdminNET.dbo.tblMSUserRegistration.UID, NAEPAdminNET.dbo.tblMSUserRegistration.Frame_n_, 
                      NAEPAdminNET.dbo.tblMSUserRegistration.MyNAEPRegID, NAEPAdminNET.dbo.tblMSUserRegistration.LEAID, 
                      NAEPAdminNET.dbo.tblMSUserRegistration.NAEPYear, NAEPAdminNET.dbo.aspnet_Membership.Email, 
                      NAEPAdminNET.dbo.tblUsers.FirstName AS fname, NAEPAdminNET.dbo.tblUsers.LastName AS lname, 
                      NAEPAdminNET.dbo.aspnet_Membership.LastLoginDate AS LastLogin
FROM         NAEPAdminNET.dbo.tblMSUserRegistration INNER JOIN
                      NAEPAdminNET.dbo.tblUsers ON NAEPAdminNET.dbo.tblMSUserRegistration.UID = NAEPAdminNET.dbo.tblUsers.UID INNER JOIN
                      NAEPAdminNET.dbo.aspnet_Users ON NAEPAdminNET.dbo.tblUsers.UserId = NAEPAdminNET.dbo.aspnet_Users.UserId INNER JOIN
                      NAEPAdminNET.dbo.aspnet_Membership ON NAEPAdminNET.dbo.aspnet_Users.UserId = NAEPAdminNET.dbo.aspnet_Membership.UserId
WHERE     (NAEPAdminNET.dbo.tblMSUserRegistration.NAEPYear = 2015)



GO


