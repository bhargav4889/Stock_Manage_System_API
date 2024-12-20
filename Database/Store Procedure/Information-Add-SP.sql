CREATE OR
ALTER   PROC [dbo].[API_SAVE_INFORMATION_INSERT]
@BANK_ID Int,
@Account_No Varchar(20),
@IFSC_Code Varchar(11),
@Account_Holder_Name Varchar(500)
AS
INSERT INTO [dbo].[SAVE_INFORMATION_TABLE]
([BANK_ID],[BANK_ACCOUNT_NO],[BANK_IFSC_CODE],[BANK_ACCOUNT_HOLDER_NAME])
VALUES 
(@BANK_ID,@Account_No,@IFSC_Code,@Account_Holder_Name)