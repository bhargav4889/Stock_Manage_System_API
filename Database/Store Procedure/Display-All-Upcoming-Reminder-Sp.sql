CREATE OR
ALTER     PROCEDURE [dbo].[API_DISPLAY_ALL_UPCOMING_REMINDER]
AS
BEGIN
    SELECT TOP 5
		[REMINDER_TABLE].[REMINDER_ID],
        [REMINDER_DATE_TIME] AS [REMINDER_DATETIME],
        [REMINDER_TYPE],
        [REMINDER_CUSTOM_TYPE],
        [REMINDER_DESCRIPTION],
        [REMINDER_SET_EMAIL_ADDRESS] AS [EMAIL_ADDRESS]
    FROM [dbo].[REMINDER_TABLE]
    WHERE REMINDER_DATE_TIME > GETDATE()
    ORDER BY REMINDER_DATE_TIME ASC
END;
