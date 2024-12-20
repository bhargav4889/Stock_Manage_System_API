CREATE OR
ALTER   PROC [dbo].[API_DISPLAY_ALL_PAYMENTS]
AS
SELECT 
[dbo].[PAYMENT_TABLE].[PAYMENT_ID],
[dbo].[PAYMENT_TABLE].[PAYMENT_DATE],
[dbo].[CUSTOMER_TABLE].[CUSTOMER_ID],
[dbo].[CUSTOMER_TABLE].[CUSTOMER_NAME],
[dbo].[CUSTOMER_TABLE].[CUSTOMER_TYPE],
[dbo].[PURCHASE_STOCK_TABLE].[PUR_STOCK_ID],
[dbo].[PRODUCT_TABLE].[PRODUCT_ID],
[dbo].[PRODUCT_TABLE].[PRODUCT_NAME_IN_GUJARATI],
[dbo].[PURCHASE_STOCK_TABLE].[TOTAL_PRICE],
[dbo].[PAYMENT_TABLE].[REMAIN_AMOUNT],
[dbo].[PAYMENT_TABLE].[PAYMENT_METHOD],
[dbo].[PURCHASE_STOCK_TABLE].[PAYMENT_STATUS]

FROM PAYMENT_TABLE

INNER JOIN [dbo].[CUSTOMER_TABLE] ON [dbo].[CUSTOMER_TABLE].[CUSTOMER_ID] = [dbo].[PAYMENT_TABLE].[CUSTOMER_ID]
INNER JOIN [dbo].[PRODUCT_TABLE] ON [dbo].[PRODUCT_TABLE].[PRODUCT_ID] = [dbo].[PAYMENT_TABLE].[PRODUCT_ID]
INNER JOIN [dbo].[PURCHASE_STOCK_TABLE] ON [dbo].[PURCHASE_STOCK_TABLE].[PUR_STOCK_ID] = [dbo].[PAYMENT_TABLE].[STOCK_ID]