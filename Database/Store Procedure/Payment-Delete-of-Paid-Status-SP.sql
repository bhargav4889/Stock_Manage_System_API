CREATE OR
ALTER   PROC [dbo].[API_PAYMENT_DELETE_OF_PAID_STATUS]
@PAYMENT_ID INT,
@STOCK_ID INT
AS
BEGIN

SET NOCOUNT ON; 

UPDATE PURCHASE_STOCK_TABLE SET PAYMENT_STATUS = 'PENDING' WHERE PUR_STOCK_ID = @STOCK_ID

DELETE FROM PAYMENT_TABLE WHERE PAYMENT_ID = @PAYMENT_ID

END;
