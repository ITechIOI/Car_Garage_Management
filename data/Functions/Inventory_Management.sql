ALTER TABLE INVENTORY_MANAGEMENT
ADD CONSTRAINT FK_INVENTORY_COMPONENT FOREIGN KEY (ID_COM) REFERENCES CAR_COMPONENTS(ID_COM)

CREATE OR ALTER FUNCTION dbo.GET_MAX_LOTNUMBER()
RETURNS INT
AS
BEGIN
	DECLARE 
		@MAX_LOTNUMBER CHAR(10), 
		@MAX_NUM INT

	IF ((SELECT COUNT(*) FROM GOOD_RECEIVED_NOTES) = 0)
		RETURN 0

	SELECT TOP 1 @MAX_LOTNUMBER = LOTNUMBER
	FROM GOOD_RECEIVED_NOTES
	ORDER BY CAST(SUBSTRING(LOTNUMBER, 4, LEN(LOTNUMBER))AS INT) DESC;
	
	DECLARE @SUB_MAX CHAR(10) = SUBSTRING(@MAX_LOTNUMBER, 4, LEN(@MAX_LOTNUMBER))
	SET @MAX_NUM = CAST(@SUB_MAX AS INT)

	RETURN @MAX_NUM
END
GO


CREATE OR ALTER PROC USP_GETDATAFORINVENTORYREPORT
@gara CHAR(10), @ReportDate VARCHAR(20)
AS
BEGIN
    DECLARE @date SMALLDATETIME
	SET DATEFORMAT DMY
	SET @date = CAST(@ReportDate AS SMALLDATETIME) 
	SELECT NAME_COM, BEGINING_INVENTORY.COM_QUANTITY BI, SUM(INCURRED_COSTS.COM_QUANTITY) IC, ENDING_INVENTORY.COM_QUANTITY EI
	FROM BEGINING_INVENTORY
	LEFT JOIN ENDING_INVENTORY
	ON BEGINING_INVENTORY.ID_COM = ENDING_INVENTORY.ID_COM
	LEFT JOIN INCURRED_COSTS
	ON BEGINING_INVENTORY.ID_COM = INCURRED_COSTS.ID_COM
	JOIN CAR_COMPONENTS ON CAR_COMPONENTS.ID_COM = BEGINING_INVENTORY.ID_COM
	WHERE MONTH(BEGINING_INVENTORY.RENDERING_TIME_BI) = MONTH(@date)
		AND YEAR(BEGINING_INVENTORY.RENDERING_TIME_BI) = YEAR(@date)
		AND MONTH(ENDING_INVENTORY.RENDERING_TIME_EI) = MONTH(@date)
		AND YEAR(ENDING_INVENTORY.RENDERING_TIME_EI) = YEAR(@date)
		AND MONTH(INCURRED_COSTS.INCURRED_COSTS_DATE) = MONTH(@date)
		AND YEAR(INCURRED_COSTS.INCURRED_COSTS_DATE) = YEAR(@date)
		AND BEGINING_INVENTORY.ID_GARA = '' AND ENDING_INVENTORY.ID_GARA = @gara AND INCURRED_COSTS.ID_GARA = @gara
	GROUP BY INCURRED_COSTS.ID_COM,  NAME_COM, BEGINING_INVENTORY.COM_QUANTITY, ENDING_INVENTORY.COM_QUANTITY
END
GO

CREATE OR ALTER PROC SUMMARIZEBI
@date VARCHAR(20), @com CHAR(10), @gara CHAR(10), @quantity INT
AS
BEGIN
	DECLARE @date1 SMALLDATETIME
	SET DATEFORMAT DMY
	SET @date1 = CAST(@date AS smalldatetime)
	INSERT BEGINING_INVENTORY(ID_COM, ID_GARA, RENDERING_TIME_BI, COM_QUANTITY) VALUES (@com, @gara, @date1, @quantity)
END
GO

CREATE OR ALTER PROC SUMMARIZEEI
@date VARCHAR(20), @com CHAR(10), @gara CHAR(10), @quantity INT
AS
BEGIN
	DECLARE @date1 SMALLDATETIME
	SET DATEFORMAT DMY
	SET @date1 = CAST(@date AS smalldatetime)
	INSERT ENDING_INVENTORY(ID_COM, ID_GARA, RENDERING_TIME_EI, COM_QUANTITY) VALUES (@com, @gara, @date1, @quantity)
END
GO

