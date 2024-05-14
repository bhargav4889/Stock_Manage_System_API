CREATE OR
ALTER   PROC [dbo].[API_DROPDOWN_VEHICLE]
AS
BEGIN 
SELECT 
	[dbo].[VEHICLE_TABLE].[VEH_ID],
	[dbo].[VEHICLE_TABLE].[VEH_NAME] AS [VEHICLE_NAME]

FROM [dbo].[VEHICLE_TABLE]

END;