CREATE OR
ALTER   PROCEDURE [dbo].[API_CUSTOMER_INSERT]
    @CUSTOMER_ID INT OUTPUT,
    @CUSTOMER_NAME NVARCHAR(255), -- Specify appropriate size
    @CUSTOMER_TYPE NVARCHAR(255), -- Specify appropriate size
    @CUSTOMER_CONTACT VARCHAR(10),
    @CUSTOMER_ADDRESS NVARCHAR(255), -- Specify appropriate size
    @CUSTOMER_CREATE DATE = NULL, -- Default to NULL and handle inside
    @CUSTOMER_UPDATE DATE = NULL -- Default to NULL and handle inside
AS
BEGIN
    SET NOCOUNT ON;

    -- Handle default date values
    IF @CUSTOMER_CREATE IS NULL
        SET @CUSTOMER_CREATE = GETDATE();
    IF @CUSTOMER_UPDATE IS NULL
        SET @CUSTOMER_UPDATE = GETDATE();

    INSERT INTO [dbo].[CUSTOMER_TABLE]
    (
        [CUSTOMER_NAME],
        [CUSTOMER_TYPE],
        [CUSTOMER_CONTACT],
        [CUSTOMER_ADDRESS],
        [CUSTOMER_CREATE_DATE],
        [CUSTOMER_UPDATE_DATE]
    )
    VALUES
    (
        @CUSTOMER_NAME,
        @CUSTOMER_TYPE,
        @CUSTOMER_CONTACT,
        @CUSTOMER_ADDRESS,
        @CUSTOMER_CREATE,
        @CUSTOMER_UPDATE
    );

    SET @CUSTOMER_ID = SCOPE_IDENTITY();
END;