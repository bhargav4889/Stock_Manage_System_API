CREATE OR
ALTER     PROC [dbo].[API_SAVE_INFORMATION_UPDATE]
@Information_ID int,
@BANK_ID Int,
@Account_No Varchar(20),
@IFSC_Code Varchar(11),
@Account_Holder_Name Varchar(500)
AS
Update  [dbo].[SAVE_INFORMATION_TABLE]
SET
BANK_ID = @BANK_ID,
BANK_ACCOUNT_NO = @Account_No,
BANK_IFSC_CODE = @IFSC_Code,
BANK_ACCOUNT_HOLDER_NAME = @Account_Holder_Name

WHERE INFORMATION_ID = @Information_ID