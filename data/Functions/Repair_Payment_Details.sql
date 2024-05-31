﻿--Lấy các thông tin cần thiết cho crdRepair
CREATE OR ALTER PROCEDURE USP_LOAD_REPAIR_CARD_DETAILS
@ID_REC CHAR(10)
AS
BEGIN
	SELECT RF.ID_REC, RPB.ID_BILL, RF.ID_GARA, RPD.RPD_ORDINAL_NUM, RF.NUMBER_PLATES, RF.RECEPTION_DATE, RPD.REPAIR_DESCRIPTION, CC.NAME_COM, CD.CUR_PRICE,RPD.COM_QUANTITY, CD.WAGE, RPD.TOTAL_PRICE, RPB.TOTAL_PAYMENT 
	FROM RECEPTION_FORMS RF, REPAIR_PAYMENT_BILL RPB, REPAIR_PAYMENT_DETAILS RPD, CAR_COMPONENTS CC, COMPONENT_DETAILS CD
	WHERE
		RF.ID_REC = @ID_REC AND
		RF.ID_REC = RPB.ID_REC AND
		RPB.ID_BILL = RPD.ID_BILL AND
		RPD.ID_COM = CC.ID_COM AND
		CC.ID_COM = CD.ID_COM AND
		RF.ID_GARA = CD.ID_GARA
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