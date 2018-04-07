USE [TIMSSSCS2015]
GO
IF OBJECT_ID (N'tblEfileStatusLog') IS NOT NULL
    DROP TABLE dbo.tblEfileStatusLog
Go

Create table dbo.tblEfileStatusLog
	(StatusLogId					int IDENTITY(1,1) NOT NULL
	,FileId						int not null
	,StatusType					varchar(10) not null
	,UID						[uniqueidentifier] not null
	,Firstname					varchar(100) not null
	,Lastname					varchar(100) not null
	,Status						varchar(4000) not null
	,IsError						tinyint not null
	,StatusEditDT					datetime not null
	,AccessLogId					int null
	,ActiveFlag					bit null
	, CONSTRAINT [PK_tblEfileStatusLog] PRIMARY KEY
	(
	StatusLogId ASC
	)
)
go

Create TRIGGER [dbo].[trg_LinkStatusChangeToFile] ON [dbo].[tblEfileStatusLog] 
FOR INSERT
AS
declare @FileId int
declare @StatusLogId int
declare @StatusType varchar(10)
declare @UID [uniqueidentifier]
declare @Fname varchar(100)
declare @Lname varchar(100)
declare @Status varchar(4000)
declare @IsError tinyint
declare @StatusEditDT datetime

select @FileId = FileId, @StatusLogId = StatusLogId, @StatusType = StatusType, @UID = UID, @Fname = Firstname, @Lname = Lastname, @Status = Status, @IsError = IsError, @StatusEditDT = StatusEditDT from inserted

if @StatusType = 'E-File'
begin
      Update tblEfileUploads Set EfileStatusLogId = @StatusLogId, EfileStatusUID = @UID, EfileStatusFname = @Fname, EfileStatusLname = @Lname, EfileStatusCode = @Status, EfileStatusCodeIsError = @IsError, EfileStatusEditDT = @StatusEditDT Where FileId = @FileId
end

if @StatusType = 'DP'
begin
      Update tblEfileUploads Set DPStatusLogId = @StatusLogId,DPStatusUID = @UID, DPStatusFname = @Fname, DPStatusLname = @Lname, DPStatusCode = @Status, DPStatusCodeIsError = @IsError, DPStatusEditDT = @StatusEditDT Where FileId = @FileId
end

if @StatusType = 'DataComp'
begin
      Update tblEfileUploads Set DCStatusLogId = @StatusLogId Where FileId = @FileId
end



GO


/*
Create TRIGGER [dbo].[tIU_tblEfileStatusLog_ActiveFlag] ON [dbo].[tblEfileStatusLog] FOR INSERT, UPDATE AS
BEGIN
DECLARE @errno   int,
        @errmsg  varchar(255),
        @rtncode int
DECLARE     @StatusLogId int,
            @Activeflag bit,
            @Status varchar(4000)

IF UPDATE(Status)
BEGIN
            SELECT      @StatusLogId =  StatusLogId FROM INSERTED
            
            UPDATE      tblEfileStatusLog
               SET      Activeflag = dbo.udf_EfileStatus_flag (dbo.udf_RemoveSpecialChar(Status)) 
             WHERE      StatusLogId = @StatusLogId                      
END

LB_RETURN:
      RETURN 
  
LB_Error:
    raiserror @errno @errmsg  
    --rollback transaction    
END
go
*/