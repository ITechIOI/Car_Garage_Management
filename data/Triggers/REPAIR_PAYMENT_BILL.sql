-- REVENUE: EFFECTED BY REPAIR_PAYMENT_BILL

IF DB_ID('CAR_GARAGE_MANAGEMENT') IS NOT NULL
USE CAR_GARAGE_MANAGEMENT

GO

-- EXCUTE FUNCTION: dbo.GET_MAX_IDREPORT_REVENUE() IN FUNCTIONS FILES
-- BEFORE EXCUTE THIS TRIGGER

CREATE OR ALTER TRIGGER TG_TOTAL_REVENUE_INSERT
ON REPAIR_PAYMENT_BILL AFTER INSERT
AS
BEGIN
	DECLARE 
		@COMPLETION_DATE SMALLDATETIME, 
		@ID_REC CHAR(10),
		@TOTAL_PAYMENT MONEY

	DECLARE CUR CURSOR FOR
		SELECT COMPLETION_DATE, ID_REC, TOTAL_PAYMENT
		FROM INSERTED 
		WHERE STATUS_BILL = 0

	OPEN CUR
	FETCH NEXT FROM CUR INTO @COMPLETION_DATE, @ID_REC, @TOTAL_PAYMENT

	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE 
			@COUNT_ROW INT, 
			@ID_GARA CHAR(10)

		SELECT @ID_GARA = ID_GARA 
		FROM RECEPTION_FORMS 
		WHERE ID_REC = @ID_REC

		SELECT @COUNT_ROW = COUNT(*) 
		FROM REVENUE 
		WHERE MONTH(RENDERING_TIME) = MONTH(@COMPLETION_DATE)
		  AND YEAR(RENDERING_TIME) = YEAR(@COMPLETION_DATE)
		  AND ID_GARA = @ID_GARA 

		IF (@COUNT_ROW = 0)
		BEGIN
			-- REVENUE TABLE
			DECLARE 
				@MAX_ID_NUM INT,
				@ID CHAR(10)

			SELECT @MAX_ID_NUM = dbo.GET_MAX_IDREPORT_REVENUE()

			SET @MAX_ID_NUM = @MAX_ID_NUM + 1
			SET @ID = CAST(@MAX_ID_NUM AS CHAR(10))

			INSERT INTO REVENUE (ID_REVENUE_REPORT, ID_GARA, RENDERING_TIME)
			VALUES 
			('REV' + @ID, @ID_GARA, @COMPLETION_DATE)
		END
		ELSE
		BEGIN
			UPDATE REVENUE SET TOTAL_REVENUE=TOTAL_REVENUE + @TOTAL_PAYMENT
			WHERE MONTH(RENDERING_TIME) = MONTH(@COMPLETION_DATE)
			  AND YEAR(RENDERING_TIME) = YEAR(@COMPLETION_DATE)
			  AND ID_GARA = @ID_GARA 
		END

		FETCH NEXT FROM CUR INTO @COMPLETION_DATE, @ID_REC, @TOTAL_PAYMENT
	END

	CLOSE CUR
	DEALLOCATE CUR
END

GO

CREATE OR ALTER TRIGGER TG_TOTAL_REVENUE_UPDATE_ID_REC_DATE
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	

	IF (UPDATE(ID_REC) OR UPDATE(COMPLETION_DATE))
	BEGIN
		DECLARE 
			@COMPLETION_DATE SMALLDATETIME, 
			@TOTAL_PAYMENT MONEY, 
			@ID_REC CHAR(10)

		DECLARE 
			@ID_GARA CHAR(10)

		-- INSERT NEW INFORMATION

		DECLARE CUR CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM INSERTED
			WHERE STATUS_BILL = 0

		OPEN CUR
		FETCH NEXT FROM CUR INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @COUNT_ROW_EXIST INT

			SELECT @ID_GARA = ID_GARA 
			FROM RECEPTION_FORMS 
			WHERE STATUS_REC = 0 AND ID_REC = @ID_REC

			SELECT @COUNT_ROW_EXIST = COUNT(*) 
			FROM REVENUE WHERE ID_GARA = @ID_GARA 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDERING_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDERING_TIME)

			IF (@COUNT_ROW_EXIST = 0)
				BEGIN
					DECLARE 
						@MAX_ID_NUM INT,
						@ID CHAR(10)

					SELECT @MAX_ID_NUM = dbo.GET_MAX_IDREPORT_REVENUE()

					SET @MAX_ID_NUM = @MAX_ID_NUM + 1
					SET @ID = CAST(@MAX_ID_NUM AS CHAR(10))

					INSERT INTO REVENUE (ID_REVENUE_REPORT, ID_GARA, RENDERING_TIME, TOTAL_REVENUE)
					VALUES 
					('REV' + @ID, @ID_GARA, @COMPLETION_DATE, @TOTAL_PAYMENT)
				END
			ELSE
				BEGIN
					UPDATE REVENUE 
					SET TOTAL_REVENUE = TOTAL_REVENUE + @TOTAL_PAYMENT
					WHERE ID_GARA = @ID_GARA 
					  AND MONTH(@COMPLETION_DATE) = MONTH(RENDERING_TIME)
					  AND YEAR(@COMPLETION_DATE) = YEAR(RENDERING_TIME)
				END

			FETCH NEXT FROM CUR INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR
		DEALLOCATE CUR

		--- DELETE OLD INFORMATION
		DECLARE CUR_OLD CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM DELETED

		OPEN CUR_OLD
		FETCH NEXT FROM CUR_OLD INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SELECT @ID_GARA = ID_GARA 
			FROM RECEPTION_FORMS 
			WHERE STATUS_REC = 0 AND ID_REC = @ID_REC

			UPDATE REVENUE 
			SET TOTAL_REVENUE=TOTAL_REVENUE - @TOTAL_PAYMENT
			WHERE ID_GARA = @ID_GARA 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDERING_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDERING_TIME)

			FETCH NEXT FROM CUR_OLD INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR_OLD
		DEALLOCATE CUR_OLD
	END
END

GO

CREATE OR ALTER TRIGGER TG_TOTAL_REVENUE_UPDATE_QUANTITY
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(TOTAL_PAYMENT))
	BEGIN
		DECLARE 
			@ID_REC CHAR(10), 
			@COMPLETION_DATE SMALLDATETIME, 
			@TOTAL_PAYMENT MONEY

		DECLARE @ID_GARA CHAR(10)

		---- INSERT NEW VALUES

		DECLARE CUR_NEW CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM INSERTED
			WHERE STATUS_BILL = 0

		OPEN CUR_NEW
		FETCH NEXT FROM CUR_NEW INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SELECT @ID_GARA = ID_GARA 
			FROM RECEPTION_FORMS 
			WHERE STATUS_REC = 0 AND ID_REC = @ID_REC

			UPDATE REVENUE 
			SET TOTAL_REVENUE=TOTAL_REVENUE + @TOTAL_PAYMENT
			WHERE ID_GARA = @ID_GARA 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDERING_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDERING_TIME)

			FETCH NEXT FROM CUR_NEW INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END

		CLOSE CUR_NEW
		DEALLOCATE CUR_NEW

		-- DELETE OLD VALUES

		DECLARE CUR_DEL CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM DELETED
			WHERE STATUS_BILL = 0

		OPEN CUR_DEL
		FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SELECT @ID_GARA = ID_GARA 
			FROM RECEPTION_FORMS 
			WHERE STATUS_REC = 0 AND ID_REC = @ID_REC

			UPDATE REVENUE 
			SET TOTAL_REVENUE=TOTAL_REVENUE - @TOTAL_PAYMENT
			WHERE ID_GARA = @ID_GARA 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDERING_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDERING_TIME)

			FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END

		CLOSE CUR_DEL
		DEALLOCATE CUR_DEL
	END
END

GO

CREATE OR ALTER TRIGGER TG_TOTAL_REVENUE_DELETE
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(STATUS_BILL))
	BEGIN
		DECLARE 			
			@ID_BILL CHAR(10),
			@ID_REC CHAR(10), 
			@COMPLETION_DATE SMALLDATETIME, 
			@TOTAL_PAYMENT MONEY


		DECLARE CUR_DEL_ROW CURSOR FOR
			SELECT ID_BILL, ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM INSERTED

		OPEN CUR_DEL_ROW
		FETCH NEXT FROM CUR_DEL_ROW INTO @ID_BILL, @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @ID_GARA CHAR(10)

			SELECT @ID_GARA = ID_GARA 
			FROM RECEPTION_FORMS 
			WHERE STATUS_REC = 0 AND ID_REC = @ID_REC

			UPDATE REVENUE 
			SET TOTAL_REVENUE=TOTAL_REVENUE - @TOTAL_PAYMENT
			WHERE ID_GARA = @ID_GARA 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDERING_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDERING_TIME)

			UPDATE REPAIR_PAYMENT_DETAILS 
			SET STATUS_RPD=1 
			WHERE ID_BILL= @ID_BILL

			FETCH NEXT FROM CUR_DEL_ROW INTO  @ID_BILL, @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR_DEL_ROW
		DEALLOCATE CUR_DEL_ROW
	END
END

GO

-- REVENUE DETAILS: EFFECTED BY REPAIR_PAYMENT_BILL

CREATE OR ALTER TRIGGER TG_CALCULATE_RATE_OF_EACHBRAND_INSERT
ON REPAIR_PAYMENT_BILL AFTER INSERT
AS
BEGIN
	DECLARE 
		@ID_REC CHAR(10), 
		@COMPLETION_DATE SMALLDATETIME, 
		@TOTAL_PAYMENT MONEY

	DECLARE CUR_INS CURSOR FOR
		SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
		FROM INSERTED

	OPEN CUR_INS
	FETCH NEXT FROM CUR_INS INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE 
			@ID_GARA CHAR(10),
			@ID_BRAND CHAR(10)

		DECLARE 
			@COUNT_ROW INT,
			@SUM_REPAIR FLOAT,
			@RATE_REPAIR FLOAT

		SELECT 
			@ID_BRAND=ID_BRAND, 
			@ID_GARA=ID_GARA
		FROM RECEPTION_FORMS 
		WHERE ID_REC= @ID_REC AND STATUS_REC = 0

		SELECT @COUNT_ROW = COUNT(*) 
		FROM REVENUE_DETAILS 
		WHERE ID_BRAND = @ID_BRAND 
			AND ID_GARA=@ID_GARA 
			AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
			AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
			AND STATUS_RD = 0

		SELECT @SUM_REPAIR = SUM(NUMBER_OF_REPAIRS) 
		FROM REVENUE_DETAILS 
		WHERE ID_GARA = @ID_GARA
			AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
			AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
			AND STATUS_RD = 0

		IF (@COUNT_ROW = 0)
			BEGIN
				IF (@SUM_REPAIR IS NULL)
					SET @RATE_REPAIR = 1
				ELSE
					SET @RATE_REPAIR= 1 / (@SUM_REPAIR + 1)

				INSERT INTO REVENUE_DETAILS
				(ID_GARA, ID_BRAND, RENDER_TIME, NUMBER_OF_REPAIRS, RATE,TOTAL_MONEY, STATUS_RD)
				VALUES 
				(@ID_GARA, @ID_BRAND, @COMPLETION_DATE, 1, @RATE_REPAIR, @TOTAL_PAYMENT, 0)
			END
		ELSE
			BEGIN		
				DECLARE @NUMBER INT 

				SELECT @NUMBER = NUMBER_OF_REPAIRS 
				FROM REVENUE_DETAILS 
				WHERE ID_BRAND = @ID_BRAND 
					AND ID_GARA = @ID_GARA 
					AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
					AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
					AND STATUS_RD = 0

				SET @RATE_REPAIR = (@NUMBER + 1) / (@SUM_REPAIR + 1)

				UPDATE REVENUE_DETAILS 
				SET 
					RATE = @RATE_REPAIR, 
					NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS + 1,
					TOTAL_MONEY = @TOTAL_PAYMENT + TOTAL_MONEY
				WHERE ID_GARA = @ID_GARA 
					AND ID_BRAND = @ID_BRAND
					AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
					AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME) 
					AND STATUS_RD=0

				DECLARE 
					@BRAND_UPDATE_RATE CHAR(10), 
					@NUMBER_UPDATE_RATE INT, 
					@RENDER_TIME SMALLDATETIME

				DECLARE CUR_UPDATE_RATE_INS CURSOR FOR
					SELECT ID_BRAND, NUMBER_OF_REPAIRS, RENDER_TIME 
					FROM REVENUE_DETAILS 
					WHERE ID_GARA = @ID_GARA
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0 
						AND ID_BRAND <> @ID_BRAND

				OPEN CUR_UPDATE_RATE_INS
				FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
				@BRAND_UPDATE_RATE, @NUMBER_UPDATE_RATE, @RENDER_TIME

				WHILE (@@FETCH_STATUS = 0)
				BEGIN 
					UPDATE REVENUE_DETAILS 
					SET RATE = @NUMBER_UPDATE_RATE / (@SUM_REPAIR + 1)
					WHERE ID_BRAND = @BRAND_UPDATE_RATE 
						AND MONTH(@RENDER_TIME) = MONTH( RENDER_TIME)
						AND YEAR(@RENDER_TIME) = YEAR(RENDER_TIME)
						AND ID_BRAND <> @ID_BRAND 
						AND STATUS_RD=0 
						AND ID_GARA = @ID_GARA

					FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
					@BRAND_UPDATE_RATE, @NUMBER_UPDATE_RATE, @RENDER_TIME
				END
				CLOSE CUR_UPDATE_RATE_INS
				DEALLOCATE CUR_UPDATE_RATE_INS
			END
		FETCH NEXT FROM CUR_INS INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
	END
	CLOSE CUR_INS
	DEALLOCATE CUR_INS
END

GO

CREATE OR ALTER TRIGGER TG_CALCULATE_RATE_OF_EACHBRAND_DELETE
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(STATUS_BILL))
	BEGIN
		DECLARE 
			@ID_REC CHAR(10), 
			@COMPLETION_DATE SMALLDATETIME, 
			@TOTAL_PAYMENT MONEY

		DECLARE CUR_DEL CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM DELETED

		OPEN CUR_DEL
		FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE 
				@ID_BRAND CHAR(10), 
				@ID_GARA CHAR(10),
				@COUNT_BILL_ROW INT

			SELECT 
				@ID_BRAND=ID_BRAND, 
				@ID_GARA=ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC= @ID_REC AND STATUS_REC = 0

			SELECT @COUNT_BILL_ROW = COUNT(*) 
			FROM REPAIR_PAYMENT_BILL AS BILL 
			JOIN RECEPTION_FORMS AS REC ON BILL.ID_REC=REC.ID_REC 
			WHERE REC.ID_GARA = @ID_GARA
				AND REC.ID_BRAND = @ID_BRAND 
				AND BILL.STATUS_BILL = 0
				AND MONTH(@COMPLETION_DATE) = MONTH(COMPLETION_DATE)
				AND YEAR(@COMPLETION_DATE) = YEAR(COMPLETION_DATE)

			IF (@COUNT_BILL_ROW = 0) 
				BEGIN
					UPDATE REVENUE_DETAILS 
					SET 
						STATUS_RD = 1, 
						TOTAL_MONEY = 0
					WHERE ID_BRAND = @ID_BRAND 
						AND ID_GARA = @ID_GARA
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0
				END
			ELSE 
				BEGIN
					DECLARE 
						@RATE FLOAT, 
						@SUM_REPAIR FLOAT,
						@NUMBER_OF_REPAIRS INT 

					SELECT @SUM_REPAIR = SUM(NUMBER_OF_REPAIRS) 
					FROM REVENUE_DETAILS 
					WHERE ID_GARA = @ID_GARA
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0

					SELECT @NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS 
					FROM REVENUE_DETAILS 
					WHERE ID_BRAND = @ID_BRAND 
						AND ID_GARA = @ID_GARA 
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0

					SET @RATE = (@NUMBER_OF_REPAIRS - 1) / (@SUM_REPAIR - 1)

					UPDATE REVENUE_DETAILS 
					SET 
						RATE = @RATE, 
						NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS - 1,
						TOTAL_MONEY = TOTAL_MONEY - @TOTAL_PAYMENT
					WHERE ID_GARA = @ID_GARA 
						AND ID_BRAND = @ID_BRAND
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME) 
						AND STATUS_RD = 0

					DECLARE 
						@BRAND_UPDATE_RATE CHAR(10), 
						@NUMBER_UPDATE_RATE INT, 
						@RENDER_TIME SMALLDATETIME

					DECLARE CUR_UPDATE_RATE_INS CURSOR FOR
						SELECT ID_BRAND, NUMBER_OF_REPAIRS, RENDER_TIME 
						FROM REVENUE_DETAILS 
						WHERE ID_GARA = @ID_GARA
							AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
							AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
							AND STATUS_RD = 0 
							AND ID_BRAND <> @ID_BRAND

					OPEN CUR_UPDATE_RATE_INS
					FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
					@BRAND_UPDATE_RATE,	@NUMBER_UPDATE_RATE, @RENDER_TIME

					WHILE (@@FETCH_STATUS = 0)
					BEGIN 
						UPDATE REVENUE_DETAILS 
						SET RATE = @NUMBER_UPDATE_RATE / (@SUM_REPAIR - 1)
						WHERE ID_BRAND = @BRAND_UPDATE_RATE 
							AND MONTH(@RENDER_TIME) = MONTH(RENDER_TIME)
							AND YEAR(@RENDER_TIME) = YEAR(RENDER_TIME)
							AND ID_BRAND <> @ID_BRAND AND STATUS_RD=0 
							AND ID_GARA = @ID_GARA

						FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
						@BRAND_UPDATE_RATE, @NUMBER_UPDATE_RATE, @RENDER_TIME
					END

					CLOSE CUR_UPDATE_RATE_INS
					DEALLOCATE CUR_UPDATE_RATE_INS
				END
			FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR_DEL
		DEALLOCATE CUR_DEL
	END
END

GO

CREATE OR ALTER TRIGGER TG_CALCULATE_RATE_OF_EACHBRAND_UPDATE_TOTAL_PAYMENT
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(TOTAL_PAYMENT))
	BEGIN
		DECLARE 
			@ID_REC CHAR(10), 
			@COMPLETION_DATE SMALLDATETIME, 
			@TOTAL_PAYMENT MONEY

		DECLARE 
			@ID_BRAND CHAR(10), 
			@ID_GARA CHAR(10)
		---- INSERT NEW INFORMATION

		DECLARE CUR_NEW CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM INSERTED

		OPEN CUR_NEW
		FETCH NEXT FROM CUR_NEW INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SELECT 
				@ID_BRAND=ID_BRAND, 
				@ID_GARA=ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC= @ID_REC AND STATUS_REC = 0

			UPDATE REVENUE_DETAILS 
			SET TOTAL_MONEY = TOTAL_MONEY + @TOTAL_PAYMENT
			WHERE ID_GARA = @ID_GARA 
				AND ID_BRAND=@ID_BRAND 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
				AND STATUS_RD = 0

			FETCH NEXT FROM CUR_NEW INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END

		CLOSE CUR_NEW
		DEALLOCATE CUR_NEW

		-- DELETE OLD INFORMATION

		DECLARE CUR_DEL CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM DELETED

		OPEN CUR_DEL
		FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
	
		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SELECT 
				@ID_GARA = ID_GARA, 
				@ID_BRAND = ID_BRAND 
			FROM RECEPTION_FORMS
				WHERE ID_REC = @ID_REC AND STATUS_REC = 0

			UPDATE REVENUE_DETAILS 
			SET TOTAL_MONEY=TOTAL_MONEY - @TOTAL_PAYMENT
			WHERE ID_GARA = @ID_GARA 
				AND ID_BRAND = @ID_BRAND  
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME) 
				AND STATUS_RD = 0

			FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR_DEL
		DEALLOCATE CUR_DEL
	END
END

GO

CREATE OR ALTER TRIGGER TG_CALCULATE_RATE_OF_EACHBRAND_UPDATE_ID_REC_OR_COMPLETION_DATE
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(COMPLETION_DATE) OR UPDATE(ID_REC))
	BEGIN
		-- INSERT NEW VALUES
		DECLARE 
			@ID_REC CHAR(10), 
			@COMPLETION_DATE SMALLDATETIME, 
			@TOTAL_PAYMENT MONEY

		DECLARE 
			@ID_BRAND CHAR(10), 
			@ID_GARA CHAR(10)

		DECLARE 
			@RATE FLOAT, 
			@SUM_REPAIR FLOAT,
			@COUNT_ROW INT

		DECLARE 
			@BRAND_UPDATE_RATE CHAR(10), 
			@NUMBER_UPDATE_RATE INT, 
			@RENDER_TIME SMALLDATETIME

		DECLARE @NUMBER_OF_REPAIRS INT 

		DECLARE CUR_INS CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM INSERTED

		OPEN CUR_INS
		FETCH NEXT FROM CUR_INS INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SELECT 
				@ID_BRAND = ID_BRAND, 
				@ID_GARA = ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC= @ID_REC AND STATUS_REC = 0

			SELECT @COUNT_ROW = COUNT(*) 
			FROM REVENUE_DETAILS 
			WHERE ID_BRAND = @ID_BRAND 
				AND ID_GARA = @ID_GARA 
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
				AND STATUS_RD = 0

			SET @SUM_REPAIR = 0

			SELECT @SUM_REPAIR = SUM(NUMBER_OF_REPAIRS) 
			FROM REVENUE_DETAILS 
			WHERE ID_GARA = @ID_GARA
				AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
				AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
				AND STATUS_RD = 0

			IF (@COUNT_ROW = 0)
				BEGIN
					IF (@SUM_REPAIR IS NULL)
						SET @RATE =1
					ELSE
						SET @RATE = 1 / (@SUM_REPAIR + 1)

					INSERT INTO REVENUE_DETAILS
					(ID_GARA, ID_BRAND, RENDER_TIME, NUMBER_OF_REPAIRS, RATE,TOTAL_MONEY, STATUS_RD)
					VALUES 
					(@ID_GARA, @ID_BRAND, @COMPLETION_DATE, 1, @RATE, @TOTAL_PAYMENT, 0)
					END
			ELSE
				BEGIN
					SELECT @NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS 
					FROM REVENUE_DETAILS 
					WHERE ID_BRAND = @ID_BRAND 
						AND ID_GARA = @ID_GARA 
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0

					SET @RATE = (@NUMBER_OF_REPAIRS + 1)/(@SUM_REPAIR + 1)

					UPDATE REVENUE_DETAILS 
					SET 
						RATE = @RATE, 
						NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS + 1,
						TOTAL_MONEY = @TOTAL_PAYMENT + TOTAL_MONEY
					WHERE ID_GARA = @ID_GARA 
						AND ID_BRAND = @ID_BRAND
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME) 
						AND STATUS_RD = 0

					DECLARE CUR_UPDATE_RATE_INS CURSOR FOR
					SELECT ID_BRAND, NUMBER_OF_REPAIRS, RENDER_TIME 
					FROM REVENUE_DETAILS 
					WHERE ID_GARA = @ID_GARA
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0 
						AND ID_BRAND <> @ID_BRAND

					OPEN CUR_UPDATE_RATE_INS
					FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
					@BRAND_UPDATE_RATE, @NUMBER_UPDATE_RATE, @RENDER_TIME

					WHILE (@@FETCH_STATUS = 0)
					BEGIN 
						UPDATE REVENUE_DETAILS 
						SET RATE=@NUMBER_UPDATE_RATE / (@SUM_REPAIR + 1)
						WHERE ID_BRAND = @BRAND_UPDATE_RATE 
							AND MONTH(@RENDER_TIME) = MONTH( RENDER_TIME)
							AND YEAR(@RENDER_TIME) = YEAR(RENDER_TIME)
							AND ID_BRAND <> @ID_BRAND 
							AND STATUS_RD = 0 
							AND ID_GARA = @ID_GARA

						FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
						@BRAND_UPDATE_RATE, @NUMBER_UPDATE_RATE, @RENDER_TIME
					END

					CLOSE CUR_UPDATE_RATE_INS
					DEALLOCATE CUR_UPDATE_RATE_INS
				END
			FETCH NEXT FROM CUR_INS INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR_INS
		DEALLOCATE CUR_INS

		--- DELETE OLD INFORMATION

		DECLARE @COUNT_BILL_ROW INT

		DECLARE CUR_DEL CURSOR FOR
			SELECT ID_REC, COMPLETION_DATE, TOTAL_PAYMENT 
			FROM DELETED

		OPEN CUR_DEL
		FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT

		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			SELECT 
				@ID_BRAND = ID_BRAND, 
				@ID_GARA = ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC= @ID_REC AND STATUS_REC = 0
			
			SELECT @COUNT_BILL_ROW = COUNT(*) 
			FROM REPAIR_PAYMENT_BILL AS BILL 
			JOIN RECEPTION_FORMS AS REC ON BILL.ID_REC=REC.ID_REC 
			WHERE REC.ID_GARA = @ID_GARA
				AND REC.ID_BRAND = @ID_BRAND 
				AND MONTH(@COMPLETION_DATE) = MONTH(COMPLETION_DATE)
				AND YEAR(@COMPLETION_DATE) = YEAR(COMPLETION_DATE)
				AND BILL.STATUS_BILL = 0

			IF (@COUNT_BILL_ROW = 0) 
				BEGIN
					UPDATE REVENUE_DETAILS 
						SET STATUS_RD = 1, 
						TOTAL_MONEY = 0
					WHERE ID_BRAND = @ID_BRAND 
						AND ID_GARA = @ID_GARA
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0
				END
			ELSE 
				BEGIN
					SELECT @SUM_REPAIR = SUM(NUMBER_OF_REPAIRS) 
					FROM REVENUE_DETAILS 
					WHERE ID_GARA = @ID_GARA
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0

					SELECT @NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS 
					FROM REVENUE_DETAILS 
					WHERE ID_BRAND = @ID_BRAND 
						AND ID_GARA = @ID_GARA 
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
						AND STATUS_RD = 0

					SET @RATE = (@NUMBER_OF_REPAIRS - 1) / (@SUM_REPAIR - 1)

					UPDATE REVENUE_DETAILS 
					SET 
						RATE = @RATE, 
						NUMBER_OF_REPAIRS = NUMBER_OF_REPAIRS - 1,
						TOTAL_MONEY = TOTAL_MONEY - @TOTAL_PAYMENT
					WHERE ID_GARA = @ID_GARA 
						AND ID_BRAND = @ID_BRAND
						AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
						AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME) 
						AND STATUS_RD = 0

					DECLARE CUR_UPDATE_RATE_INS CURSOR FOR
						SELECT ID_BRAND, NUMBER_OF_REPAIRS, RENDER_TIME 
						FROM REVENUE_DETAILS 
						WHERE ID_GARA = @ID_GARA
							AND MONTH(@COMPLETION_DATE) = MONTH(RENDER_TIME)
							AND YEAR(@COMPLETION_DATE) = YEAR(RENDER_TIME)
							AND STATUS_RD = 0 
							AND ID_BRAND <> @ID_BRAND

					OPEN CUR_UPDATE_RATE_INS
					FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
					@BRAND_UPDATE_RATE,	@NUMBER_UPDATE_RATE, @RENDER_TIME

					WHILE (@@FETCH_STATUS = 0)
					BEGIN 
						UPDATE REVENUE_DETAILS 
						SET RATE = @NUMBER_UPDATE_RATE / (@SUM_REPAIR - 1)
						WHERE ID_BRAND = @BRAND_UPDATE_RATE 
							AND MONTH(@RENDER_TIME) = MONTH(RENDER_TIME)
							AND YEAR(@RENDER_TIME) = YEAR(RENDER_TIME)
							AND ID_BRAND <> @ID_BRAND 
							AND STATUS_RD = 0 
							AND ID_GARA = @ID_GARA

						FETCH NEXT FROM CUR_UPDATE_RATE_INS INTO 
						@BRAND_UPDATE_RATE, @NUMBER_UPDATE_RATE, @RENDER_TIME
					END

					CLOSE CUR_UPDATE_RATE_INS
					DEALLOCATE CUR_UPDATE_RATE_INS
				END
			FETCH NEXT FROM CUR_DEL INTO @ID_REC, @COMPLETION_DATE, @TOTAL_PAYMENT
		END
		CLOSE CUR_DEL
		DEALLOCATE CUR_DEL
	END
END

GO

-- DEBT = DEBT + (TOTAL_PAYMENT - PAID)

GO

CREATE OR ALTER TRIGGER TG_CALCULATE_DEBT_INSERT
ON REPAIR_PAYMENT_BILL AFTER INSERT
AS
BEGIN
	DECLARE 
		@ID_REC CHAR(10),  
		@TOTAL_PAYMENT MONEY,
		@PAID MONEY

	DECLARE 
		@ID_CUS CHAR(10),
		@ID_GARA CHAR(10)

	DECLARE CURSOR_DEBT CURSOR FOR
		SELECT ID_REC, TOTAL_PAYMENT, PAID 
		FROM INSERTED

	OPEN CURSOR_DEBT 
	FETCH NEXT FROM CURSOR_DEBT INTO @ID_REC, @TOTAL_PAYMENT, @PAID

	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		SELECT @ID_CUS = ID_CUS, @ID_GARA = ID_GARA
		FROM RECEPTION_FORMS 
		WHERE ID_REC = @ID_REC	
		AND STATUS_REC=0

		UPDATE CUSTOMER_DETAILS
		SET DEBT = DEBT + (@TOTAL_PAYMENT - @PAID)
		WHERE ID_CUS = @ID_CUS AND ID_GARA=@ID_GARA

		FETCH NEXT FROM CURSOR_DEBT INTO @ID_REC, @TOTAL_PAYMENT, @PAID
	END
	CLOSE CURSOR_DEBT
	DEALLOCATE CURSOR_DEBT
END

GO

CREATE OR ALTER TRIGGER TG_CALCULATE_DEBT_DELETE
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(STATUS_BILL))
	BEGIN
		DECLARE 
			@ID_REC CHAR(10),  
			@TOTAL_PAYMENT MONEY,
			@PAID MONEY

		DECLARE 
			@ID_CUS CHAR(10),
			@ID_GARA CHAR(10)

		DECLARE CURSOR_DEBT_DEL CURSOR FOR
			SELECT ID_REC, TOTAL_PAYMENT, PAID 
			FROM DELETED 
			WHERE STATUS_BILL=0

		OPEN CURSOR_DEBT_DEL
		FETCH NEXT FROM CURSOR_DEBT_DEL INTO @ID_REC, @TOTAL_PAYMENT, @PAID

		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SELECT @ID_CUS = ID_CUS, @ID_GARA=ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC = @ID_REC 
				AND STATUS_REC = 0

			UPDATE CUSTOMER_DETAILS
			SET DEBT = DEBT - (@TOTAL_PAYMENT - @PAID)
			WHERE ID_CUS = @ID_CUS 
			AND ID_GARA = @ID_GARA

			FETCH NEXT FROM CURSOR_DEBT_DEL INTO @ID_REC, @TOTAL_PAYMENT, @PAID
		END
		CLOSE CURSOR_DEBT_DEL
		DEALLOCATE CURSOR_DEBT_DEL
	END
END

GO

CREATE OR ALTER TRIGGER TG_CALCULATE_DEBT_UPDATE
ON REPAIR_PAYMENT_BILL AFTER UPDATE
AS
BEGIN
	IF (UPDATE(ID_REC) OR UPDATE(COMPLETION_DATE) OR UPDATE(TOTAL_PAYMENT) OR UPDATE(PAID))
	BEGIN
		DECLARE 
			@ID_REC CHAR(10),  
			@TOTAL_PAYMENT MONEY,
			@PAID MONEY

		DECLARE 
			@ID_CUS CHAR(10), 
			@ID_GARA CHAR(10)

		-- INSERT NEW VALUES
		
		DECLARE CURSOR_DEBT CURSOR FOR
			SELECT ID_REC, TOTAL_PAYMENT, PAID 
			FROM INSERTED

		OPEN CURSOR_DEBT 
		FETCH NEXT FROM CURSOR_DEBT INTO @ID_REC, @TOTAL_PAYMENT, @PAID

		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SELECT @ID_CUS = ID_CUS, @ID_GARA = ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC = @ID_REC 
			AND STATUS_REC=0

			UPDATE CUSTOMER_DETAILS 
			SET DEBT = DEBT + (@TOTAL_PAYMENT - @PAID)
			WHERE ID_CUS = @ID_CUS 
			AND ID_GARA = @ID_GARA

			FETCH NEXT FROM CURSOR_DEBT INTO @ID_REC, @TOTAL_PAYMENT, @PAID
		END
		CLOSE CURSOR_DEBT
		DEALLOCATE CURSOR_DEBT

		-- DELETE OLD VALUES
		DECLARE CURSOR_DEBT_DEL CURSOR FOR
			SELECT ID_REC, TOTAL_PAYMENT, PAID 
			FROM DELETED 
			WHERE STATUS_BILL=0

		OPEN CURSOR_DEBT_DEL
		FETCH NEXT FROM CURSOR_DEBT_DEL INTO @ID_REC, @TOTAL_PAYMENT, @PAID

		WHILE (@@FETCH_STATUS = 0)
		BEGIN
			SELECT @ID_CUS = ID_CUS, @ID_GARA = ID_GARA
			FROM RECEPTION_FORMS 
			WHERE ID_REC = @ID_REC
			AND STATUS_REC=0

			UPDATE CUSTOMER_DETAILS
			SET DEBT = DEBT - (@TOTAL_PAYMENT - @PAID)
			WHERE ID_CUS = @ID_CUS
			AND ID_GARA = @ID_GARA

			FETCH NEXT FROM CURSOR_DEBT_DEL INTO @ID_REC, @TOTAL_PAYMENT, @PAID
		END

		CLOSE CURSOR_DEBT_DEL
		DEALLOCATE CURSOR_DEBT_DEL
	END
END

