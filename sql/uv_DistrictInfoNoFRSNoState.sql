USE [TIMSSSCS2015]
GO

/****** Object:  View [dbo].[uv_DistrictInfoNoFRSNoState]    Script Date: 07/30/2014 14:33:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[uv_DistrictInfoNoFRSNoState]
AS
SELECT     dbo.tblSCSDistrict.LEAID
	, dbo.tblSCSDistrict.D_Name
	, dbo.tblSCSDistrict.D_Addr1
	, dbo.tblSCSDistrict.D_Addr2
	, dbo.tblSCSDistrict.D_City
	, dbo.tblSCSDistrict.D_State
	, dbo.tblSCSDistrict.D_Zip
	, dbo.tblSCSDistrict.D_Phone
	, dbo.tblSCSDistrict.D_Fax
	, dbo.tblSCSDistrict.D_Comment
	, dbo.tblSCSDistrict.flgTuda
	, dbo.tblSCSDistrict.MultipleFlag
	, dbo.tblSCSDistrict.d_super AS super_id
	, dbo.tblSCSDistrict.d_tDirector AS td_id
	, dbo.tblSCSDistrict.d_aCoord AS acoord_id
	, dbo.tblSCSDistrict.d_contact AS contact_id
	, dbo.tblSCSDistrict.sysActive
	, dbo.tblSCSDistrict.smalldst
	,  CASE WHEN (sp.prefix <> '' AND sp.prefix IS NOT NULL) THEN 
		(sp.prefix + ' ' + sp.fname + ' ' + sp.lname) 
	ELSE (sp.fname + ' ' + sp.lname) 
                      END AS D_Super
	, sp.phone AS D_SuperPhone
	, sp.email AS D_SuperEmail
	, sp.fax AS D_SuperFax
	
	, CASE WHEN (ct.prefix <> '' AND ct.prefix IS NOT NULL) THEN 
         (ct.prefix + ' ' + ct.fname + ' ' + ct.lname) 
      ELSE 
		(ct.fname + ' ' + ct.lname) 
	END AS D_Contact
	, ct.phone AS D_ContPhone
	, ct.email AS D_ContEmail
	, ct.fax AS D_ContFax
	, ct.title AS D_ContTitle
	
	, COALESCE (a_Coord.prefix + ' ', '') + a_Coord.fname + ' ' + a_Coord.lname AS D_Acoord
	, a_Coord.phone AS D_AcoordPhone
	, a_Coord.email AS D_AcoordEmail
	, a_Coord.fax AS D_AcoordFax
	, a_Coord.title AS D_AcoordTitle
	
	, COALESCE (td.prefix + ' ', '') + td.fname + ' ' + td.lname AS td_Name
	, td.phone AS D_TdPhone
	, td.email AS D_TdEmail
	, td.fax AS D_TdFax
	, td.addr_1 AS td_addr1
	, td.addr_2 AS td_addr2
	, td.city AS td_city
	, td.state AS td_state
	, td.zip AS td_zip	
	, 'FA' AS fldPhaseCode
	, dbo.tblSCSDistrict.D_PartLtrSentDT
	
	
	
	, sp.prefix AS D_SuperPrefix
	, sp.fname D_SuperFname
	, sp.lname D_SuperLname
	, sp.addr_1 AS D_SuperAddr1
	, sp.addr_2 AS D_SuperAddr2
	, sp.city AS D_SuperCity
	, sp.state AS D_SuperState
	, sp.zip AS D_SuperZip
	, sp.suffix AS D_SuperSuffix
	
	
	, td.prefix AS D_TdPrefix
	, td.fname D_TdFname
	, td.lname D_TdLname
	, td.addr_1 AS D_TdAddr1
	, td.addr_2 AS D_TdAddr2
	, td.city AS D_TdCity
	, td.state AS D_TdState
	, td.zip AS D_TdZip
	, td.suffix AS D_TdSuffix
	
	, sp.phoneext D_SuperPhoneext
	, td.phoneext D_TdPhoneext
	
FROM         dbo.tblSCSDistrict LEFT OUTER JOIN
                      dbo.tblSCSPersonnel AS a_Coord ON dbo.tblSCSDistrict.d_aCoord = a_Coord.pID AND a_Coord.SchDistFlag = 'D' LEFT OUTER JOIN
                      dbo.tblSCSPersonnel AS ct ON dbo.tblSCSDistrict.d_contact = ct.pID AND ct.SchDistFlag = 'D' LEFT OUTER JOIN
                      dbo.tblSCSPersonnel AS td ON dbo.tblSCSDistrict.d_tDirector = td.pID AND td.SchDistFlag = 'D' LEFT OUTER JOIN
                      dbo.tblSCSPersonnel AS sp ON dbo.tblSCSDistrict.d_super = sp.pID AND sp.SchDistFlag = 'D'
WHERE     (dbo.tblSCSDistrict.sysActive = 1)

GO


