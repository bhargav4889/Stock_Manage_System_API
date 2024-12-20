CREATE OR
ALTER PROC [dbo].[API_PURCHASE_STOCK_INSERT]
    @PUR_STOCK_DATE DATE,
    @CUSTOMER_ID INT,
    @PRODUCT_ID INT,
    @PRODUCT_GRADE_ID INT,
    @PUR_STOCK_LOCATION NVARCHAR(MAX),
    @BAG DECIMAL(10,2),
    @BAG_PER_KG DECIMAL(10,2),
    @TOTAL_WEIGHT DECIMAL(10,2),
    @PRODUCT_PRICE DECIMAL(10,2),
    @TOTAL_PRICE DECIMAL(10,2),
    @VEHICLE_ID INT,
    @VEHICLE_NO VARCHAR(100),
    @TOLAT_NAME NVARCHAR(MAX),
    @DRIVER_NAME NVARCHAR(MAX),
    @PAYMENT_STATUS VARCHAR(100)
AS
BEGIN
    INSERT INTO [dbo].[PURCHASE_STOCK_TABLE]
    (
        [PUR_STOCK_DATE],
        [CUSTOMER_ID],
        [PRODUCT_ID],
        [PRODUCT_GRADE_ID],
        [PUR_STOCK_LOCATION],
        [BAGS],
        [BAG_PER_KG],
        [TOTAL_WEIGHT],
        [PRODUCT_PRICE],
        [TOTAL_PRICE],
        [VEHICLE_ID],
        [VEHICLE_NO],
        [TOLAT_NAME],
        [DRIVER_NAME],
        [PAYMENT_STATUS]
    )
    VALUES
    (
        @PUR_STOCK_DATE,
        @CUSTOMER_ID,
        @PRODUCT_ID,
        @PRODUCT_GRADE_ID, -- Set to 0 if NULL
        @PUR_STOCK_LOCATION,
        @BAG, -- Set to 0 if NULL
        @BAG_PER_KG, -- Set to 0 if NULL
        @TOTAL_WEIGHT,
        @PRODUCT_PRICE,
        @TOTAL_PRICE,
        @VEHICLE_ID,
        @VEHICLE_NO,
        @TOLAT_NAME,
        @DRIVER_NAME,
        'PENDING'
    )
END;
