CREATE OR
ALTER PROCEDURE [dbo].[API_PURCHASE_STOCK_BY_PK] 
    @STOCK_ID INT 
AS
BEGIN
    SELECT
        PST.[PUR_STOCK_ID] AS [STOCK_ID],
        PST.[PUR_STOCK_DATE],
        CT.[CUSTOMER_ID],
        CT.[CUSTOMER_NAME],
        CT.[CUSTOMER_TYPE],
        PT.[PRODUCT_ID],
        PT.[PRODUCT_NAME_IN_GUJARATI] AS [PRODUCT_NAME],
        ISNULL(PGT.[PRODUCT_GRADE_ID], 0) AS [PRODUCT_GRADE_ID],
        COALESCE(PGT.[PRODUCT_GRADE], '--') AS [PRODUCT_GRADE],
        PST.[PUR_STOCK_LOCATION] AS [LOCATION],
        COALESCE(CONVERT(DECIMAL, PST.[BAGS]), 0) AS [BAGS],
        COALESCE(CONVERT(DECIMAL, PST.[BAG_PER_KG]), 0) AS [BAG_PER_KG],
        PST.[TOTAL_WEIGHT],
        PST.[PRODUCT_PRICE],
        PST.[TOTAL_PRICE],
        VT.[VEH_ID] AS [VEHICLE_ID],
        VT.[VEH_NAME] AS [VEHICLE_NAME],
        PST.[VEHICLE_NO],
        PST.[DRIVER_NAME],
        COALESCE(PST.[TOLAT_NAME], '--') AS [TOLAT_NAME]
    FROM [dbo].[PURCHASE_STOCK_TABLE] PST
    LEFT JOIN [dbo].[CUSTOMER_TABLE] CT ON CT.[CUSTOMER_ID] = PST.[CUSTOMER_ID]
    LEFT JOIN [dbo].[PRODUCT_TABLE] PT ON PT.[PRODUCT_ID] = PST.[PRODUCT_ID]
    LEFT JOIN [dbo].[PRODUCT_GRADE_TABLE] PGT ON PGT.[PRODUCT_GRADE_ID] = PST.[PRODUCT_GRADE_ID]
    LEFT JOIN [dbo].[VEHICLE_TABLE] VT ON VT.[VEH_ID] = PST.[VEHICLE_ID]
    WHERE PST.[PUR_STOCK_ID] = @STOCK_ID
END;
