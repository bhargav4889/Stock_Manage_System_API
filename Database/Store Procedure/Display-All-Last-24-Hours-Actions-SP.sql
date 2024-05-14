CREATE OR
ALTER   PROC [dbo].[API_DISPLAY_LAST_24_HOURS_ACTIONS]
AS
  SELECT [REC_ACT_TABLE_NAME] AS ACTION_TYPE, [RECT_ACT_INFO] AS INFORMATION, [RECT_ACT_CREATE] AS TIME
                FROM [dbo].[RECENT_ACTIONS_TABLE]
                WHERE [RECT_ACT_CREATE] >= DATEADD(DAY, -1, GETDATE())
                ORDER BY [RECT_ACT_CREATE] DESC;
