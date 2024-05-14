CREATE OR
ALTER   PROC [dbo].[API_REMINDER_UPDATE]
@REMINDER_ID INT,
@REMINDER_DATE_TIME datetime,
@REMINDER_TYPE varchar(100),
@REMINDER_CUSTOM_TYPE varchar (500) = NULL,
@REMINDER_DESCRIPTION varchar(5000)
AS
UPDATE [dbo].[REMINDER_TABLE]

SET 
[REMINDER_DATE_TIME] = @REMINDER_DATE_TIME,
[REMINDER_TYPE] = @REMINDER_TYPE,
[REMINDER_CUSTOM_TYPE] = @REMINDER_CUSTOM_TYPE,
[REMINDER_DESCRIPTION] = @REMINDER_DESCRIPTION

WHERE [REMINDER_ID] = @REMINDER_ID