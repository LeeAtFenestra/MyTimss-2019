USE [TIMSSSCS2015]
GO
if OBJECT_ID('usp_EfileVerificationProcess_ClearParsed') > 0
	drop proc usp_EfileVerificationProcess_ClearParsed
go

CREATE PROCEDURE [dbo].[usp_EfileVerificationProcess_ClearParsed] 

@FileId int

AS

/*

Clear previously added data

*/

SET NOCOUNT ON

Delete from dbo.tblEfileCleanedStudentData Where FileId = @FileId

grant execute on usp_EfileVerificationProcess_ConvertedStudentData to TIMSSSCS2015_app
go