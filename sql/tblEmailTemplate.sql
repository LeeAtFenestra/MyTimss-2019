/*
if OBJECT_ID('tblEmailTemplate') > 0
	drop table tblEmailTemplate
go
*/
CREATE TABLE [dbo].tblEmailTemplate(
	[EmailTemplateName] [varchar](300) not null PRIMARY KEY
	,[EmailFrom] varchar(300) not null
	,[EmailCC] varchar(300) null
	,[EmailBCC] varchar(300) null
	,[EmailSubject]  varchar(300) not null
	,[EmailBody] varchar(max) not null
) 
go

insert tblEmailTemplate ([EmailTemplateName], [EmailFrom], [EmailCC], [EmailBCC], [EmailSubject], [EmailBody]) values ('DistrictOtherContactEmail', 'TIMSS@westat.com', '', '', 'Confirm District Other Contact Email', 'Dear [fname] [lname]: 
<br />
<br />
This email is to confirm that we have noted your email address correctly in our system. To confirm receipt of this email, simply click "reply all" and a message will be sent to our Help Desk and your TIMSS Advanced representative confirming your email address. 
<br />
<br />
Your reply is of utmost importance as we will be emailing you updates and reminders for upcoming TIMSS Advanced activities.
<br />
<br />
Thank you for taking time out of your busy day to confirm your email address.
<br />
<br />
Sincerely,
<br />
<br />
TIMSS Advanced 
')
insert tblEmailTemplate ([EmailTemplateName], [EmailFrom], [EmailCC], [EmailBCC], [EmailSubject], [EmailBody]) values ('DistrictTestDirectorEmail', 'TIMSS@westat.com', '', '', 'Confirm District Test Director Email', 'Dear [fname] [lname]:
<br />
<br />
This email is to confirm that we have noted your email address correctly in our system. To confirm receipt of this email, simply click "reply all" and a message will be sent to our Help Desk and your TIMSS Advanced representative confirming your email address. 
<br />
<br />
Your reply is of utmost importance as we will be emailing you updates and reminders for upcoming TIMSS Advanced activities.
<br />
<br />
Thank you for taking time out of your busy day to confirm your email address.
<br />
<br />
Sincerely,
<br />
<br />
TIMSS Advanced 
')
insert tblEmailTemplate ([EmailTemplateName], [EmailFrom], [EmailCC], [EmailBCC], [EmailSubject], [EmailBody]) values ('DistrictSuperintendentEmail', 'TIMSS@westat.com', '', '', 'Confirm District Superintendent Email', 'Dear [fname] [lname]:
<br />
<br />
This email is to confirm that we have noted your email address correctly in our system. To confirm receipt of this email, simply click "reply all" and a message will be sent to our Help Desk and your TIMSS Advanced representative confirming your email address. 
<br />
<br />
Your reply is of utmost importance as we will be emailing you updates and reminders for upcoming TIMSS Advanced activities.
<br />
<br />
Thank you for taking time out of your busy day to confirm your email address.
<br />
<br />
Sincerely,
<br />
<br />
TIMSS Advanced 
')
insert tblEmailTemplate ([EmailTemplateName], [EmailFrom], [EmailCC], [EmailBCC], [EmailSubject], [EmailBody]) values ('PrincipalEmail', 'TIMSS@westat.com', '', '', 'Confirm Principal Email', 'Dear [fname] [lname]: 
<br />
<br />
This email is to confirm that we have noted your email address correctly in our system. To confirm receipt of this email, simply click "reply all" and a message will be sent to our Help Desk and your TIMSS Advanced representative confirming your email address. 
<br />
<br />
Your reply is of utmost importance as we will be emailing you updates and reminders for upcoming TIMSS Advanced activities.
<br />
<br />
Thank you for taking time out of your busy day to confirm your email address.
<br />
<br />
Sincerely,
<br />
<br />
TIMSS Advanced
')
insert tblEmailTemplate ([EmailTemplateName], [EmailFrom], [EmailCC], [EmailBCC], [EmailSubject], [EmailBody]) values ('SchoolContactEmail', 'TIMSS@westat.com', '', '', 'Confirm School Coordinator Email', 'Dear [fname] [lname]: 
<br />
<br />
This email is to confirm that we have noted your email address correctly in our system. To confirm receipt of this email, simply click "reply all" and a message will be sent to our Help Desk and your TIMSS Advanced representative confirming your email address. 
<br />
<br />
Your reply is of utmost importance as we will be emailing you updates and reminders for upcoming TIMSS Advanced activities.
<br />
<br />
Thank you for taking time out of your busy day to confirm your email address.
<br />
<br />
Sincerely,
<br />
<br />
TIMSS Advanced 
')


