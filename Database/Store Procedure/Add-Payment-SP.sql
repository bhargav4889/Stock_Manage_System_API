CREATE OR
ALTER   PROCEDURE [dbo].[API_INSERT_PAYMENT]
@PaymentDate datetime,
@CustomerID int,
@ProductID int,
@StockID int,
@PaidAmount decimal(10,2),
@RemainAmount decimal(10,2),
@PaymentMethod varchar(200),
@BankID int = NULL, -- Optional parameters, default to NULL
@Bank_AC_No varchar(12) = NULL,
@CHEQ_No varchar(10) = NULL,
@RTGS_No varchar(8) = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Conditional logic for payment method
        IF @PaymentMethod = 'CASH'
        BEGIN
            SET @BankID = NULL;
            SET @Bank_AC_No = '--';
            SET @CHEQ_No = '--';
            SET @RTGS_No = '--';
        END
        ELSE IF @PaymentMethod = 'BANK'
        BEGIN
            SET @CHEQ_No = '--';
            SET @RTGS_No = '--';
        END
        ELSE IF @PaymentMethod = 'CHEQ'
        BEGIN
            SET @RTGS_No = '--';
        END
        ELSE IF @PaymentMethod = 'RTGS'
        BEGIN
            SET @CHEQ_No = '--';
        END

        -- Insertion into PAYMENT_TABLE
        INSERT INTO PAYMENT_TABLE
        (
            [PAYMENT_DATE],
            [CUSTOMER_ID],
            [PRODUCT_ID],
            [STOCK_ID],
            [AMOUNT_PAID],
            [REMAIN_AMOUNT],
            [PAYMENT_METHOD],
            [BANK_ID],
            [BANK_AC_NO],
            [CHEQ_NO],
            [RTGS_NO]
        )
        VALUES
        (
            @PaymentDate,
            @CustomerID,
            @ProductID,
            @StockID,
            @PaidAmount,
            @RemainAmount,
            @PaymentMethod,
            @BankID,
            @Bank_AC_No,
            @CHEQ_No,
            @RTGS_No
        )

        -- Update payment status in PURCHASE_STOCK_TABLE
        DECLARE @TotalPrice DECIMAL(10,2);
        SELECT @TotalPrice = TOTAL_PRICE FROM PURCHASE_STOCK_TABLE WHERE PUR_STOCK_ID = @StockID; 

       IF @RemainAmount = 0
    BEGIN
        UPDATE PURCHASE_STOCK_TABLE SET PAYMENT_STATUS = 'PAID' WHERE PUR_STOCK_ID = @StockID;
    END
    ELSE IF @RemainAmount > 0 AND @RemainAmount < @TotalPrice
    BEGIN
        UPDATE PURCHASE_STOCK_TABLE SET PAYMENT_STATUS = 'REMAIN' WHERE PUR_STOCK_ID = @StockID;
    END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        -- Here, you might want to handle the error, e.g., logging it or notifying someone.
    END CATCH
END


  