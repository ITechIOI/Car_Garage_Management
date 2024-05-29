﻿CREATE OR ALTER FUNCTION dbo.GET_MAX_IDREPORT_REVENUE()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_ID_REPORT CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM REVENUE) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_ID_REPORT = ID_REVENUE_REPORT
	FROM REVENUE
	ORDER BY CAST(SUBSTRING(ID_REVENUE_REPORT, 4, LEN(ID_REVENUE_REPORT))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_ID_REPORT, 4, LEN(@MAX_ID_REPORT))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END

GO


CREATE OR ALTER FUNCTION dbo.GET_MAX_IDCUSTOMER()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_ID_CUSTOMER CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM CUSTOMERS) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_ID_CUSTOMER = ID_CUS
	FROM CUSTOMERS
	ORDER BY CAST(SUBSTRING(ID_CUS, 4, LEN(ID_CUS))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_ID_CUSTOMER, 4, LEN(@MAX_ID_CUSTOMER))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END

GO

ALTER TABLE CUSTOMER_DETAILS
ADD STATUS_CUSD BIT DEFAULT 0
UPDATE CUSTOMER_DETAILS 
SET STATUS_CUSD = 0 

--Stored procedure to insert, update, delete customer 
CREATE OR ALTER PROCEDURE USP_INSERTCUSTOMER
@gara CHAR(10), @name NVARCHAR(100), @phone CHAR(15), @address NVARCHAR(100)
AS
BEGIN
	DECLARE @cus CHAR(10)
	IF (SELECT COUNT(*) FROM CUSTOMERS WHERE NAME_CUS = @name AND PHONE_NUMBER_CUS = @phone) = 0
	BEGIN
		DECLARE @MAX_ID SMALLINT
		SET @MAX_ID = dbo.GET_MAX_IDCUSTOMER() + 1
		INSERT CUSTOMERS VALUES ('CUS' + cast(@MAX_ID as char(7)), @name, @phone, @address, 0)
		INSERT CUSTOMER_DETAILS (ID_CUS, ID_GARA) VALUES ('CUS' + cast(@MAX_ID as char(7)), @gara)
	END
	ELSE IF (SELECT COUNT(*) FROM CUSTOMERS WHERE NAME_CUS = @name AND PHONE_NUMBER_CUS = @phone AND STATUS_CUS = 1) > 0
	BEGIN
		SELECT @cus = ID_CUS FROM CUSTOMERS WHERE NAME_CUS = @name AND PHONE_NUMBER_CUS = @phone
		UPDATE CUSTOMERS
		SET STATUS_CUS = 0
		WHERE ID_CUS = @cus
		IF (SELECT COUNT(*) FROM CUSTOMER_DETAILS WHERE ID_CUS = @cus AND ID_GARA = @gara) = 0
		BEGIN
			INSERT CUSTOMER_DETAILS (ID_CUS, ID_GARA) VALUES (@cus, @gara)
		END
		ELSE IF (SELECT COUNT(*) FROM CUSTOMER_DETAILS WHERE ID_CUS = @cus AND ID_GARA = @gara AND STATUS_CUSD = 1) > 0
		BEGIN
			UPDATE CUSTOMER_DETAILS
			SET STATUS_CUSD = 0
			WHERE ID_CUS =@cus AND ID_GARA = @gara
		END
	END
	ELSE
	BEGIN
		SELECT @cus = ID_CUS FROM CUSTOMERS WHERE NAME_CUS = @name AND PHONE_NUMBER_CUS = @phone
		IF (SELECT COUNT(*) FROM CUSTOMER_DETAILS WHERE ID_CUS = @cus AND ID_GARA = @gara) = 0
		BEGIN
			INSERT CUSTOMER_DETAILS (ID_CUS, ID_GARA) VALUES (@cus, @gara)
		END
		ELSE IF (SELECT COUNT(*) FROM CUSTOMER_DETAILS WHERE ID_CUS = @cus AND ID_GARA = @gara AND STATUS_CUSD = 1) > 0
		BEGIN
			UPDATE CUSTOMER_DETAILS
			SET STATUS_CUSD = 0
			WHERE ID_CUS =@cus AND ID_GARA = @gara
		END
	END
END

GO


CREATE OR ALTER PROCEDURE USP_UPDATECUSTOMER
@id char(10), @name NVARCHAR(100), @phone CHAR(15), @address NVARCHAR(100)
AS
BEGIN
	UPDATE CUSTOMERS 
	SET NAME_CUS = @name, PHONE_NUMBER_CUS = @phone, ADDRESS_CUS = @address
	WHERE ID_CUS = @id
END

GO


CREATE OR ALTER PROCEDURE USP_DELETECUSTOMER
@gara char(10), @id char(10)
AS
BEGIN
	UPDATE CUSTOMER_DETAILS 
	SET STATUS_CUSD = 1
	WHERE ID_CUS = @id AND ID_GARA = @gara
	IF (SELECT COUNT(*) FROM CUSTOMER_DETAILS WHERE ID_CUS = @id AND STATUS_CUSD = 0) = 0
	BEGIN
		UPDATE CUSTOMERS
		SET STATUS_CUS = 1
		WHERE ID_CUS = @id
	END
END

GO

CREATE OR ALTER PROCEDURE USP_UPDATEDEBTOFCUSTOMER
@gara CHAR(10), @id CHAR(10), @updateMoney MONEY 
AS
BEGIN
	UPDATE CUSTOMER_DETAILS 
	SET DEBT = DEBT + @updateMoney
	WHERE ID_CUS= @id AND ID_GARA = @gara
END

GO

ALTER TABLE ACCOUNTS 
ADD CONSTRAINT FK_ACC_STAFF FOREIGN KEY(ID_STAFF) REFERENCES STAFFS(ID_STAFF) 
GO

GO

CREATE OR ALTER FUNCTION dbo.GET_MAX_IDSTAFF()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_ID_STAFF CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM CUSTOMERS) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_ID_STAFF = ID_STAFF
	FROM STAFFS
	ORDER BY CAST(SUBSTRING(ID_STAFF,4,LEN(ID_STAFF))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_ID_STAFF, 4, LEN(@MAX_ID_STAFF))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END

GO

--Stored procedure to insert, update, delete Staff


CREATE OR ALTER PROCEDURE USP_INSERTSTAFF
@name NVARCHAR(100), @birthday VARCHAR(15), @address NVARCHAR(100), @email VARCHAR(50), @phone CHAR(15), @salary MONEY, @position nvarchar(50), @gara char(10)
AS
BEGIN
	IF (SELECT COUNT(*) FROM STAFFS WHERE NAME_STAFF = @name AND PHONE_NUMBER_STAFF = @phone AND ID_GARA = @gara) = 0
	BEGIN
		DECLARE @IDPOS CHAR(10), @MAX_ID INT, @birthday1 smalldatetime
		SELECT @IDPOS = ID_POS FROM STAFF_POSITION WHERE NAME_POS = @position
		SET @MAX_ID=dbo.GET_MAX_IDSTAFF() + 1
		SET DATEFORMAT DMY
		SET @birthday1 = CAST(@birthday AS SMALLDATETIME)
		INSERT STAFFS VALUES ('STF' + cast(@MAX_ID as char(7)), @name, @birthday1, @address, @email, @phone, @salary, @IDPOS, @gara, 0)
	END
	ELSE 
	BEGIN
		DECLARE @ID_STAFF CHAR(10), @IDPOS1 CHAR(10)
		SELECT @ID_STAFF = ID_STAFF FROM STAFFS WHERE NAME_STAFF = @name AND PHONE_NUMBER_STAFF = @phone AND ID_GARA = @gara
		SELECT @IDPOS1 = ID_POS FROM STAFF_POSITION WHERE NAME_POS = @position
		UPDATE STAFFS
		SET STATUS_STAFF = 0, SALARY = @salary, ID_POSITION = @IDPOS1
		WHERE ID_STAFF = @ID_STAFF
	END
END

GO


CREATE OR ALTER PROCEDURE USP_UPDATESTAFF
@id CHAR(10), @name NVARCHAR(100), @birthday VARCHAR(15), @address NVARCHAR(100), @email VARCHAR(50), @phone CHAR(15), @salary MONEY, @position nvarchar(50)
AS
BEGIN
DECLARE @IDPOS CHAR(10), @birthday1 smalldatetime
	SELECT @IDPOS = ID_POS FROM STAFF_POSITION WHERE NAME_POS = @position	
	SET DATEFORMAT DMY
	SET @birthday1 = CAST(@birthday AS SMALLDATETIME)
	UPDATE STAFFS
	SET NAME_STAFF=@name, BIRTHDAY_STAFF=@birthday1, ADDRESS_STAFF=@address, EMAIL_STAFF=@email, PHONE_NUMBER_STAFF=@phone, SALARY=@salary, ID_POSITION=@IDPOS
	WHERE ID_STAFF=@id
END

GO


CREATE OR ALTER PROCEDURE USP_DELETESTAFF
@id CHAR(10)
AS
BEGIN	
	UPDATE STAFFS
	SET STATUS_STAFF=1
	WHERE ID_STAFF=@id
END

GO


CREATE OR ALTER FUNCTION dbo.GET_MAX_IDACC()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_ID_ACC CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM ACCOUNTS) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_ID_ACC = ID_ACC
	FROM ACCOUNTS
	ORDER BY CAST(SUBSTRING(ID_ACC,3,LEN(ID_ACC))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_ID_ACC, 3, LEN(@MAX_ID_ACC))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END

GO

----Stored procedure to insert, update, delete Accounts
CREATE OR ALTER PROCEDURE USP_INSERTACCOUNT
@username VARCHAR(50), @idStaff CHAR(10), @accAuthor BIT
AS
BEGIN
	DECLARE @MAX_ID INT
	SET @MAX_ID = DBO.GET_MAX_IDACC() + 1
	INSERT ACCOUNTS VALUES('AC'+CAST(@MAX_ID AS CHAR(8)),@username, '123456', @idStaff, @accAuthor,0)
END
GO

CREATE OR ALTER PROCEDURE USP_CHANGEPASSWORD
@idAcc CHAR(10), @password VARCHAR(50)
AS
BEGIN
	UPDATE ACCOUNTS
	SET PASSWORD = @password
	WHERE ID_ACC = @idAcc
END
GO

CREATE OR ALTER PROCEDURE USP_RESETPASSWORD
@idAcc CHAR(10)
AS
BEGIN
	UPDATE ACCOUNTS
	SET PASSWORD = '123456'
	WHERE ID_ACC = @idAcc
END
GO

CREATE OR ALTER PROCEDURE USP_UPDATEAUTHOR
@idAcc CHAR(10), @accAuthor BIT
AS
BEGIN
	UPDATE ACCOUNTS
	SET ACC_AUTHORIZATION = @accAuthor
	WHERE ID_ACC = @idAcc
END
GO

CREATE OR ALTER PROCEDURE USP_DELETEACCOUNTS
@idAcc CHAR(10)
AS
BEGIN
	UPDATE ACCOUNTS
	SET STATUS_ACCOUNT = 1
	WHERE ID_ACC = @idAcc
END
GO

CREATE OR ALTER FUNCTION dbo.GET_MAX_IDRECEPTION()
RETURNS INT
AS
BEGIN
	DECLARE
		@MAX_ID_RECEPTION CHAR(10),
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM RECEPTION_FORMS) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_ID_RECEPTION = ID_REC
	FROM RECEPTION_FORMS
	ORDER BY ID_REC DESC;

	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_ID_RECEPTION, 4, LEN(@MAX_ID_RECEPTION))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM 
END
GO

----Stored procedure to insert, update, delete Reception Forms
CREATE OR ALTER PROCEDURE USP_INSERT_RECEPTIONFORM
@ID_CUS CHAR(10), @ID_BRAND CHAR(10), @ID_GARA CHAR(10), @NUMBER_PLATES VARCHAR(15), @RECEPTION_DATE SMALLDATETIME
AS
BEGIN
	DECLARE @MAX_ID INT
	SET @MAX_ID = dbo.GET_MAX_IDRECEPTION() + 1
	INSERT RECEPTION_FORMS VALUES ('REC' + CAST(@MAX_ID AS CHAR(7)), @ID_CUS, @ID_BRAND, @ID_GARA, @NUMBER_PLATES, @RECEPTION_DATE, 0)
END
GO

CREATE OR ALTER PROCEDURE USP_UPDATE_RECEPTIONFORM
@ID_REC CHAR(10), @ID_CUS CHAR(10), @ID_BRAND CHAR(10), @ID_GARA CHAR(10), @NUMBER_PLATES VARCHAR(15), @RECEPTION_DATE SMALLDATETIME
AS
BEGIN
	UPDATE RECEPTION_FORMS
	SET ID_CUS = @ID_CUS, ID_BRAND = @ID_BRAND, ID_GARA = @ID_GARA, NUMBER_PLATES = @NUMBER_PLATES, RECEPTION_DATE = @RECEPTION_DATE
	WHERE ID_REC = @ID_REC
END
GO

CREATE OR ALTER PROCEDURE USP_DELETE_RECEPTIONFORM
@ID_REC CHAR(10)
AS
BEGIN
	UPDATE RECEPTION_FORMS
	SET STATUS_REC = 1
	WHERE ID_REC = @ID_REC
END
GO

CREATE OR ALTER PROCEDURE USP_GET_IDRECEPTION
@ID_CUS CHAR(10), @ID_GARA CHAR(10), @NUMBER_PLATES VARCHAR(15), @ID_BRAND CHAR(10), @RECEPTION_DATE SMALLDATETIME
AS
BEGIN
	SELECT ID_REC 
	FROM RECEPTION_FORMS
	WHERE ID_CUS = @ID_CUS AND
		  ID_GARA = @ID_GARA AND
		  NUMBER_PLATES = @NUMBER_PLATES AND
		  ID_BRAND = @ID_BRAND AND
		  RECEPTION_DATE = @RECEPTION_DATE AND
		  STATUS_REC = 0
END
GO

CREATE OR ALTER PROCEDURE USP_GET_IDCUSTOMER
@NAME_CUS NVARCHAR(100), @PHONE_NUMBER_CUS CHAR(15)
AS
BEGIN
	SELECT ID_CUS
	FROM CUSTOMERS
	WHERE NAME_CUS = @NAME_CUS AND
		  PHONE_NUMBER_CUS = @PHONE_NUMBER_CUS
END
GO

CREATE OR ALTER FUNCTION dbo.GET_MAX_ID_CARBRAND()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_ID_CARBRAND CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM CAR_BRANDS) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_ID_CARBRAND = ID_BRAND
	FROM CAR_BRANDS
	ORDER BY CAST(SUBSTRING(ID_BRAND, 3, LEN(ID_BRAND))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_ID_CARBRAND, 3, LEN(@MAX_ID_CARBRAND))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END
GO


--Stored procedure to insert, delete car brand and brand detail
CREATE OR ALTER PROC USP_INSERT_CARBRAND
@name NVARCHAR(100), @gara CHAR(10)
AS
BEGIN
	DECLARE @brand CHAR(10)
	IF (SELECT COUNT(*) FROM CAR_BRANDS WHERE NAME_BRAND = @name) = 0
	BEGIN
		DECLARE @MAX_ID SMALLINT
		SET @MAX_ID = dbo.GET_MAX_ID_CARBRAND() + 1
		INSERT CAR_BRANDS VALUES ('BR' + cast(@MAX_ID as char(8)), @name, 0)
		INSERT BRAND_DETAILS VALUES ('BR' + cast(@MAX_ID as char(8)), @gara, 0)
	END
	ELSE IF (SELECT COUNT(*) FROM CAR_BRANDS WHERE NAME_BRAND = @name AND STATUS_BRAND = 1) > 0
	BEGIN
		SELECT @brand=ID_BRAND FROM CAR_BRANDS WHERE NAME_BRAND=@name
		UPDATE CAR_BRANDS 
		SET STATUS_BRAND = 0
		WHERE ID_BRAND = @brand
		IF (SELECT COUNT(*) FROM BRAND_DETAILS WHERE ID_BRAND = @brand AND ID_GARA = @gara) = 0
		BEGIN
			INSERT BRAND_DETAILS VALUES (@brand, @gara, 0)
		END
		ELSE IF (SELECT COUNT(*) FROM BRAND_DETAILS WHERE ID_BRAND = @brand AND ID_GARA = @gara AND STATUS_DETAILS = 1) > 0
		BEGIN
			UPDATE BRAND_DETAILS
			SET STATUS_DETAILS = 0
			WHERE ID_BRAND = @brand AND ID_GARA = @gara
		END
	END
	ELSE
	BEGIN
		SELECT @brand=ID_BRAND FROM CAR_BRANDS WHERE NAME_BRAND=@name
		IF (SELECT COUNT(*) FROM BRAND_DETAILS WHERE ID_BRAND = @brand AND ID_GARA = @gara) = 0
		BEGIN
			INSERT BRAND_DETAILS VALUES (@brand, @gara, 0)
		END
		ELSE IF (SELECT COUNT(*) FROM BRAND_DETAILS WHERE ID_BRAND = @brand AND ID_GARA = @gara AND STATUS_DETAILS = 1) > 0
		BEGIN
			UPDATE BRAND_DETAILS
			SET STATUS_DETAILS = 0
			WHERE ID_BRAND = @brand AND ID_GARA = @gara
		END
	END
END
GO

CREATE OR ALTER PROC USP_DELETE_CARBRAND
@brand CHAR(10), @gara CHAR(10)
AS
BEGIN
	UPDATE BRAND_DETAILS
	SET STATUS_DETAILS = 1
	WHERE ID_BRAND = @brand AND ID_GARA = @gara
	IF (SELECT COUNT(*) FROM BRAND_DETAILS WHERE ID_BRAND = @brand AND STATUS_DETAILS = 0) = 0
	BEGIN
		UPDATE CAR_BRANDS 
		SET STATUS_BRAND = 1
		WHERE ID_BRAND = @brand
	END
END
GO


SELECT * FROM CAR_BRANDS
SELECT * FROM BRAND_DETAILS
EXEC USP_INSERT_CARBRAND N'Porsche', 'GR3'
EXEC USP_DELETE_CARBRAND 'BR7', 'GR3'
DELETE BRAND_DETAILS WHERE ID_BRAND = 'BR7'
DELETE CAR_BRANDS WHERE ID_BRAND = 'BR7'

SELECT MAX_ID = DBO.GET_MAX_IDSTAFF()
SET DATEFORMAT DMY
select * from ACCOUNTS
select * from STAFFS
delete STAFFS where NAME_STAFF = N'Bích'

SELECT ID_STAFF FROM STAFFS WHERE NAME_STAFF = N'Bích' AND PHONE_NUMBER_STAFF = '0125694' AND ID_GARA = 'GR1' 
