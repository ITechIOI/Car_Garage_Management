﻿--Lấy các thông tin cần thiết cho crdRepair
CREATE OR ALTER PROCEDURE USP_LOAD_REPAIR_CARD_DETAILS
@ID_REC CHAR(10)
AS
BEGIN
	SELECT RF.ID_REC, RPB.ID_BILL, RF.ID_GARA, RPD_ORDINAL_NUM, RF.NUMBER_PLATES, RF.RECEPTION_DATE, RPD.REPAIR_DESCRIPTION, CC.NAME_COM, CD.CUR_PRICE,RPD.COM_QUANTITY, CD.WAGE, RPD.TOTAL_PRICE, RPB.TOTAL_PAYMENT 
	FROM RECEPTION_FORMS RF, REPAIR_PAYMENT_BILL RPB, REPAIR_PAYMENT_DETAILS RPD, CAR_COMPONENTS CC, COMPONENT_DETAILS CD
	WHERE
		RF.ID_REC = @ID_REC AND
		RF.ID_REC = RPB.ID_REC AND
		RPB.ID_BILL = RPD.ID_BILL AND
		RPD.ID_COM = CC.ID_COM AND
		CC.ID_COM = CD.ID_COM AND
		RF.ID_GARA = CD.ID_GARA AND
		RPD.STATUS_RPD = 0
END
GO

CREATE OR ALTER FUNCTION dbo.GET_RPD_ORDINALNUM(@ID_BILL CHAR(10), @ID_COM CHAR(10))
RETURNS INT
AS
BEGIN
	DECLARE @RPD_ORDINAL_NUM INT
	SELECT @RPD_ORDINAL_NUM = RPD_ORDINAL_NUM
	FROM REPAIR_PAYMENT_DETAILS
	WHERE 
		ID_BILL = @ID_BILL AND
		ID_COM = @ID_COM
	RETURN @RPD_ORDINAL_NUM
END
GO

CREATE OR ALTER FUNCTION dbo.GET_MAX_IDBILL()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_IDBILL CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM REPAIR_PAYMENT_BILL) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_IDBILL = ID_BILL
	FROM REPAIR_PAYMENT_BILL
	ORDER BY CAST(SUBSTRING(ID_BILL, 4, LEN(ID_BILL))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_IDBILL, 4, LEN(@MAX_IDBILL))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END
GO

CREATE OR ALTER PROCEDURE USP_CREATE_REPAIRPAYMENTBILL
@ID_REC CHAR(10)
AS
BEGIN
	DECLARE @MAX_ID INT
	SET @MAX_ID = dbo.GET_MAX_IDBILL() + 1

	DECLARE @DATE DATETIME
	SET @DATE = GETDATE()
	INSERT REPAIR_PAYMENT_BILL (ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL) VALUES ('BIL' + CAST(@MAX_ID AS CHAR(7)), @ID_REC, @DATE, 0)
END
GO

CREATE OR ALTER PROCEDURE USP_INSERT_REPAIR_CARD_DETAILS
@ID_REC CHAR(10), @REPAIR_DESCRIPTION NVARCHAR(100), @NAME_COM NVARCHAR(100),  @CUR_PRICE MONEY, @COM_QUANTITY INT, @WAGE MONEY
AS
BEGIN
	DECLARE @ID_BILL CHAR(10)
	SELECT @ID_BILL = ID_BILL
	FROM REPAIR_PAYMENT_BILL 
	WHERE ID_REC = @ID_REC

	DECLARE @ID_GARA CHAR(10)
	SELECT @ID_GARA = ID_GARA
	FROM RECEPTION_FORMS
	WHERE ID_REC = @ID_REC

	DECLARE @ID_COM CHAR(10)
	SELECT @ID_COM = dbo.GET_IDCOMPONENT(@ID_GARA, @NAME_COM, @WAGE, @CUR_PRICE)

	INSERT INTO REPAIR_PAYMENT_DETAILS (ID_BILL, ID_COM, REPAIR_DESCRIPTION, COM_QUANTITY, STATUS_RPD)
	VALUES (@ID_BILL, @ID_COM, @REPAIR_DESCRIPTION, @COM_QUANTITY, 0)
END
GO

CREATE OR ALTER PROCEDURE USP_UPDATE_REPAIR_CAR_DETAILS
@RPD_ORDINAL_NUM INT, @REPAIR_DESCRIPTION NVARCHAR(100), @COM_QUANTITY INT
AS
BEGIN
	UPDATE REPAIR_PAYMENT_DETAILS
	SET REPAIR_DESCRIPTION = @REPAIR_DESCRIPTION,
		COM_QUANTITY = @COM_QUANTITY
	WHERE RPD_ORDINAL_NUM = @RPD_ORDINAL_NUM
END
GO