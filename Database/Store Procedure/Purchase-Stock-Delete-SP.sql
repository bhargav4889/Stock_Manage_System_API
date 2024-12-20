CREATE OR
ALTER PROCEDURE [dbo].[API_PURCHASE_STOCK_DELETE]
    @STOCK_ID INT 
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete dependent records from PAYMENT_TABLE and REMAIN_TABLE
    DELETE FROM dbo.PAYMENT_TABLE WHERE STOCK_ID = @STOCK_ID;
    DELETE FROM dbo.REMAIN_PAYMENT_TABLE WHERE STOCK_ID = @STOCK_ID;

    -- Delete record from PURCHASE_STOCK_TABLE
    DELETE FROM dbo.PURCHASE_STOCK_TABLE
    WHERE PUR_STOCK_ID = @STOCK_ID;

   
END;
