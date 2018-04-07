USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[uv_SchoolInfoNoDistNoFRSNoState]    Script Date: 07/31/2014 07:44:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[uv_SchoolInfoNoDistNoFRSNoState]
AS
SELECT     dbo.tblSCSSchool.LEAID
	, dbo.tblSCSSchool.Frame_N_
	, dbo.tblSCSSchoolTypes.TypeName
	, dbo.tblSCSSchoolTypes.isPublic
	, dbo.tblSCSSchool.S_Name
	, dbo.tblSCSSchool.S_Addr1
	, dbo.tblSCSSchool.S_Addr2
	, dbo.tblSCSSchool.S_City
	, dbo.tblSCSSchool.S_State
	, dbo.tblSCSSchool.S_Zip
	, dbo.tblSCSSchool.S_Phone
	, dbo.tblSCSSchool.S_Fax
	, dbo.tblSCSSchool.S_County
	, dbo.uv_SchoolWithID.id4
	, dbo.uv_SchoolWithID.id8
	, dbo.uv_SchoolWithID.id12
	, dbo.uv_SchoolWithID.subid4
	, dbo.uv_SchoolWithID.subid8
	, dbo.uv_SchoolWithID.subid12
	, dbo.uv_SchoolWithID.Age9
	, dbo.uv_SchoolWithID.Age13
	, dbo.uv_SchoolWithID.Age17
	, dbo.tblSCSSchool.PrincipalID
	, dbo.tblSCSSchool.CoordinatorID
	, CASE WHEN dbo.tblSCSSchool.SDCF_Flag = 1 THEN 
		'Yes' 
	ELSE 
		'No' 
	END AS SDCFFlag
	, dbo.tblSCSSchool.DateSDCFReceived
	, dbo.tblSCSSchool.DateSCReviewCompleted
	, dbo.uv_SchoolWithID.fldProjectID
	, coord.fname + '  ' + coord.lname AS sc_name
	, coord.phone AS sc_phone
	, coord.email AS sc_email
	, coord.fax AS sc_fax
	, coord.fname AS sc_fname
	, coord.lname AS sc_lname
	, coord.title AS sc_title
	, coord.prefix AS sc_prefix
	, coord.suffix AS sc_suffix
	, prin.fname + '  ' + prin.lname AS sp_name
	, prin.phone AS sp_phone
	, prin.email AS sp_email
	, prin.fax AS sp_fax
	, prin.fname AS pfname
	, prin.lname AS plname
	, prin.title AS sp_title
	, prin.prefix AS pprefix
	, prin.suffix AS sp_suffix
	, --CASE WHEN dbo.ExAdmin_UsersCountBySchool.numusers > 0 THEN '(R)' ELSE '(NR)' END AS Status, 
      CASE WHEN D.DistrictUser IS NULL AND S.SchoolUser IS NULL THEN 'NR'  
			 WHEN D.DistrictUser IS NOT NULL AND S.SchoolUser IS NULL THEN 'D' 
			 WHEN D.DistrictUser IS NULL AND S.SchoolUser IS NOT NULL THEN 'S' 
			 WHEN D.DistrictUser IS NOT NULL AND S.SchoolUser IS NOT NULL THEN 'DS' ELSE '' 
		END Status
	, dbo.tblSCSSchool.SDCFUID
	, dbo.ExAdmin_Users_t.fname
	, dbo.ExAdmin_Users_t.lname
	
	
	, prin.phoneext sp_Phoneext
	, coord.phoneext sc_Phoneext
	
	, dbo.tblSCSSchool.SEASCH
	, dbo.tblSCSSchool.sysActive
	, dbo.tblSCSSchool.MyNAEPREGID
	
	, dbo.tblSCSSchool.PSISubmittedBy
	, dbo.tblSCSSchool.ICTCoordinatorName
FROM         dbo.tblSCSSchool INNER JOIN
                      dbo.tblSCSSchoolTypes ON dbo.tblSCSSchool.Schl_Typ = dbo.tblSCSSchoolTypes.schl_typ INNER JOIN
                      dbo.uv_SchoolWithID ON dbo.uv_SchoolWithID.frame_n_ = dbo.tblSCSSchool.Frame_N_ LEFT OUTER JOIN
                      dbo.ExAdmin_Users_t ON dbo.tblSCSSchool.SDCFUID = dbo.ExAdmin_Users_t.UID LEFT OUTER JOIN
                      dbo.tblSCSPersonnel AS coord ON dbo.tblSCSSchool.CoordinatorID = coord.pID LEFT OUTER JOIN
                      dbo.tblSCSPersonnel AS prin ON dbo.tblSCSSchool.PrincipalID = prin.pID LEFT OUTER JOIN
                      --dbo.ExAdmin_UsersCountBySchool ON dbo.tblSCSSchool.MyNAEPREGID = dbo.ExAdmin_UsersCountBySchool.MyNAEPRegID
                      (SELECT	DISTINCT 
					dbo.uv_DS_MySchoolDistrictRegUsers.Leaid, 
				dbo.uv_DS_MySchoolDistrictRegUsers.Frame_n_, 
				'S' AS SchoolUser
		   FROM	dbo.uv_DS_MySchoolDistrictRegUsers 
		  WHERE  SchoolUser = 1 ) S ON tblSCSSchool.Frame_n_ = S.Frame_n_ LEFT OUTER JOIN
		(SELECT	DISTINCT dbo.uv_DS_MySchoolDistrictRegUsers.Leaid,
				'D' AS DistrictUser
		   FROM	dbo.uv_DS_MySchoolDistrictRegUsers LEFT OUTER JOIN
				(SELECT LEAID FROM dbo.tblSCSDistrict WHERE flgTuda = 0) tblSCSDistrict ON dbo.uv_DS_MySchoolDistrictRegUsers.LeaID = tblSCSDistrict.LEAID
		  WHERE  DistrictUser = 1) D ON tblSCSSchool.Leaid = D.Leaid 
WHERE     (dbo.tblSCSSchool.sysActive = 1)


GO


