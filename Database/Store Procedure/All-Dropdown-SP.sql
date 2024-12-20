CREATE OR ALTER   PROC [dbo].[API_ALL_DROPDOWNS]
AS
BEGIN
    -- Product Dropdown
    SELECT
        [PRODUCT_ID],
        [PRODUCT_NAME_IN_GUJARATI],
        [PRODUCT_NAME_IN_ENGLISH]
    FROM [dbo].[PRODUCT_TABLE];

    -- Product Grade Dropdown
    SELECT
        [PRODUCT_GRADE_ID],
        [PRODUCT_GRADE]
    FROM [dbo].[PRODUCT_GRADE_TABLE];

    -- Vehicle Dropdown
    SELECT
        [VEH_ID] AS VEHICLE_ID,
        [VEH_NAME] AS VEHICLE_NAME
    FROM [dbo].[VEHICLE_TABLE];
END;
