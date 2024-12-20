CREATE OR

ALTER PROC [dbo].[API_DISPLAY_ALL_PENDING_PAYMENTS_CUSTOMERS_LIST]
AS
BEGIN
    SELECT 
        PST.[PUR_STOCK_ID] AS STOCK_ID,
        PST.[PUR_STOCK_DATE] AS STOCK_DATE,
        CT.[CUSTOMER_ID],
        CT.[CUSTOMER_NAME],
        PT.[PRODUCT_ID],
        PT.[PRODUCT_NAME_IN_GUJARATI] AS [PRODUCT_NAME],
        PST.[PUR_STOCK_LOCATION] AS [LOCATION],
        PST.[TOTAL_PRICE],
        PST.[PAYMENT_STATUS],
        PMT.[PAYMENT_ID]  -- Including the Payment ID

    FROM [dbo].[PURCHASE_STOCK_TABLE] PST

    INNER JOIN [dbo].[CUSTOMER_TABLE] CT
        ON PST.[CUSTOMER_ID] = CT.[CUSTOMER_ID]

    INNER JOIN [dbo].[PRODUCT_TABLE] PT
        ON PST.[PRODUCT_ID] = PT.[PRODUCT_ID]
        
    LEFT JOIN [dbo].[PAYMENT_TABLE] PMT  -- Assuming a left join if not all stocks may have payments
        ON PST.[PUR_STOCK_ID] = PMT.[STOCK_ID]  -- Ensure this is the correct column in your PAYMENT_TABLE

    WHERE PST.[PAYMENT_STATUS] = 'PENDING'
    ORDER BY PST.[PUR_STOCK_DATE] DESC
END;
