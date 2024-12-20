CREATE OR 
ALTER   proc [dbo].[API_CHECK_AUTH_DETAILS]
@Auth_Username varchar(10),
@Auth_Password varchar(50)
AS
select
	[dbo].[ADMIN_TABLE].[ADMIN_ID] AS [AUTH_ID],
	[dbo].[ADMIN_TABLE].[ADMIN_NAME] AS [AUTH_NAME],
	[dbo].[ADMIN_TABLE].[ADMIN_PASSWORD] AS [AUTH_PASSWORD],
	[dbo].[ADMIN_TABLE].[ADMIN_EMAIL] AS [AUTH_EMAIL],
	[dbo].[ADMIN_TABLE].[ADMIN_PHONE_NO] AS [AUTH_PHONE_NO]
from [dbo].[ADMIN_TABLE]

Where [dbo].[ADMIN_TABLE].[ADMIN_NAME] = @Auth_Username AND [dbo].[ADMIN_TABLE].[ADMIN_PASSWORD] = @Auth_Password