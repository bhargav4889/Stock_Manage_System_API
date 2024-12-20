CREATE OR
ALTER   PROC [dbo].[API_PURCHASE_INVOICE_INSERT]
    @INVOICE_DATE DATE,
    @CUSTOMER_NAME NVARCHAR(MAX),
    @PRODUCT_ID INT,
    @PRODUCT_GRADE_ID INT = NULL, -- Optional, defaults to NULL
    @BAGS DECIMAL(10,2) = NULL, -- Optional, defaults to NULL
    @BAG_PER_KG DECIMAL(10,2) = NULL, -- Optional, defaults to NULL
    @TOTAL_WEIGHT DECIMAL(10,2),
    @PRODUCT_PRICE DECIMAL(10,2),
    @TOTAL_PRICE DECIMAL(10,2),
    @VEHICLE_ID INT,
    @VEHICLE_NO VARCHAR(200),
    @DRIVER_NAME NVARCHAR(MAX),
    @TOLAT_NAME NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO [dbo].[PURCHASE_INVOICE_TABLE]
    (
        [PUR_INV_DATE],
        [CUSTOMER_NAME],
        [PRODUCT_ID],
        [PRODUCT_GRADE_ID],
        [BAGS],
        [BAG_PER_KG],
        [TOTAL_WEIGHT],
        [PRODUCT_PRICE],
        [TOTAL_PRICE],
        [VEHICLE_ID],
        [VEHICLE_NO],
        [DRIVER_NAME],
        [TOLAT_NAME]
    )
    VALUES
    (
        @INVOICE_DATE,
        @CUSTOMER_NAME,
        @PRODUCT_ID,
        @PRODUCT_GRADE_ID,
        @BAGS,
        @BAG_PER_KG,
        @TOTAL_WEIGHT,
        @PRODUCT_PRICE,
        @TOTAL_PRICE,
        @VEHICLE_ID,
        @VEHICLE_NO,
        @DRIVER_NAME,
        @TOLAT_NAME
    )
END;