CREATE OR
ALTER    PROC [dbo].[API_INSERT_REMAIN_PAYMENT]


@PaymentID int,
@StockID int,
@CustomerID int,
@Remain_PaymentDate datetime,
@Remain_PayingAmount decimal(10,2),
@Remain_PaymentMethod varchar(200),
@Remain_BankID int = NULL,
@Remain_Bank_AC_No varchar(12) = NULL,
@Remain_CHEQ_No varchar(10) = NULL,
@Remain_RTGS_No varchar(8) = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Conditional logic for payment method
        IF @Remain_PaymentMethod = 'CASH'
        BEGIN
            SET @Remain_BankID = 0;
            SET @Remain_Bank_AC_No = '--';
            SET @Remain_CHEQ_No = '--';
            SET @Remain_RTGS_No = '--';
        END
        ELSE IF @Remain_PaymentMethod = 'BANK'
        BEGIN
            SET @Remain_CHEQ_No = '--';
            SET @Remain_RTGS_No = '--';
        END
        ELSE IF @Remain_PaymentMethod = 'CHEQ'
        BEGIN
            SET @Remain_RTGS_No = '--';
        END
        ELSE IF @Remain_PaymentMethod = 'RTGS'
        BEGIN
            SET @Remain_CHEQ_No = '--';
        END

        -- Insertion into PAYMENT_TABLE
        INSERT INTO REMAIN_PAYMENT_TABLE
        (
            [PAYMENT_ID],
			[STOCK_ID],
			[CUSTOMER_ID],
            [REMAIN_PAYMENT_DATE],
            [REMAIN_PAYMENT_AMOUNT],
            [REMAIN_PAYMENT_METHOD],
            [REMAIN_BANK_ID],
            [REMAIN_BANK_AC_NO],
            [REMAIN_CHEQ_NO],
            [REMAIN_RTGS_NO]
        )
        VALUES
        (
          @PaymentID,
		  @StockID,
		  @CustomerID,
		  @Remain_PaymentDate,
	      @Remain_PayingAmount,
          @Remain_PaymentMethod,
          @Remain_BankID,
          @Remain_Bank_AC_No,
          @Remain_CHEQ_No,
          @Remain_RTGS_No
        )

        -- Main Logic of Payment Status 
      
		-- 1. Retrive Remaining Amount From Payment Table
		-- 2. Retrive First Time Payment Amount From Payment Table
		-- 3. Retrive Total Amount From Purchase Stock Table
		-- 4. New Declare Final Payment = First Payment Amount + Remain Payment Amount
		-- 5. Check Condtions Remain Payment Entered Equals Payment Remain Value And Final Payment Equals Total Amount From Purchase Stock Table
			  	
		DECLARE @PAYMENT_REMAIN decimal(10,2);
		
		DECLARE @FIRST_PAYMENT_AMOUNT decimal(10,2);

		DECLARE @TOTAL_PAYMENT_AMOUNT decimal(10,2);

		DECLARE @FINAL_PAYMENT decimal(10,2);

		SELECT @PAYMENT_REMAIN = REMAIN_AMOUNT FROM PAYMENT_TABLE WHERE PAYMENT_ID = @PaymentID;

		SELECT @TOTAL_PAYMENT_AMOUNT = TOTAL_PRICE from PURCHASE_STOCK_TABLE WHERE PUR_STOCK_ID = @StockID;

		SELECT @FIRST_PAYMENT_AMOUNT = AMOUNT_PAID from PAYMENT_TABLE WHERE PAYMENT_ID = @PaymentID;
		
		SET @FINAL_PAYMENT = @FIRST_PAYMENT_AMOUNT + @Remain_PayingAmount ;
		
       IF @Remain_PayingAmount =  @PAYMENT_REMAIN   AND   @TOTAL_PAYMENT_AMOUNT  = @FINAL_PAYMENT
    BEGIN
        UPDATE PURCHASE_STOCK_TABLE SET PAYMENT_STATUS = 'PAID' WHERE PUR_STOCK_ID = @StockID;
    END
    ELSE 
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