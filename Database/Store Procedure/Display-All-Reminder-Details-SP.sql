CREATE OR
ALTER   PROCEDURE [dbo].[API_DISPLAY_REMINDER_DETAILS]
AS
BEGIN
    -- Select reminders where the ScheduledTime is on or slightly before the current time
    SELECT [REMINDER_DATE_TIME] AS [REMINDER_DATETIME],
           [REMINDER_TYPE],
           [REMINDER_CUSTOM_TYPE],
           [REMINDER_DESCRIPTION],
           [REMINDER_SET_EMAIL_ADDRESS] AS [EMAIL_ADDRESS],
           [REMINDER_SET_MOBILE_NO] AS [PHONE_NO]
    FROM [dbo].[REMINDER_TABLE]
    WHERE REMINDER_DATE_TIME <= DATEADD(MINUTE, 1, GETDATE()) 
      AND REMINDER_DATE_TIME >= GETDATE()
END;
