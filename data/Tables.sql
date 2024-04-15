﻿CREATE DATABASE CAR_GARAGE_MANAGEMENT
GO 
USE CAR_GARAGE_MANAGEMENT
GO
 
----- NOTE: STATUS: DELETED OR EXIST
----- 1: DELETED, 0:EXIST
-----CREATE TABLE 

CREATE TABLE CAR_GARA (
	ID_GARA CHAR(10) PRIMARY KEY,
	ADDRESS_GARA NVARCHAR(100) NOT NULL,
	PHONE_NUMBER_GARA CHAR(15) NOT NULL,
	STATUS_GARA BIT NOT NULL
)

CREATE TABLE STAFF_POSITION (
	ID_POS CHAR(10) PRIMARY KEY,
	NAME_POS NVARCHAR(50) NOT NULL,
)

CREATE TABLE STAFFS (
	ID_STAFF CHAR(10) PRIMARY KEY,
	NAME_STAFF NVARCHAR(100) NOT NULL,
	BIRTHDAY_STAFF SMALLDATETIME NOT NULL,
	ADDRESS_STAFF NVARCHAR(100) NOT NULL,
	EMAIL_STAFF VARCHAR(50) NOT NULL,
	PHONE_NUMBER_STAFF CHAR(15) NOT NULL,
	SALARY MONEY NOT NULL,
	ID_POSITION CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	STATUS_STAFF BIT NOT NULL
)

CREATE TABLE ACCOUNTS (
	ID_ACC CHAR(10) PRIMARY KEY,
	USERNAME VARCHAR(50) NOT NULL,
	PASSWORD VARCHAR(50) NOT NULL,
	ID_STAFF CHAR(10) NOT NULL,
	ACC_AUTHORIZATION BIT NOT NULL,
	STATUS_ACCOUNT BIT NOT NULL,
)

CREATE TABLE CUSTOMERS (
	ID_CUS CHAR(10) PRIMARY KEY,
	NAME_CUS NVARCHAR(100) NOT NULL,
	PHONE_NUMBER_CUS CHAR(15) NOT NULL,
	ADDRESS_CUS NVARCHAR(100) NOT NULL,
	DEBT MONEY DEFAULT 0,
	STATUS_CUS BIT NOT NULL,
)
select * from REVENUE_DETAILS
CREATE TABLE CUSTOMER_DETAILS  (
	ID_CUS CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL
)

CREATE TABLE CAR_BRANDS (
	ID_BRAND CHAR(10) PRIMARY KEY,
	NAME_BRAND NVARCHAR(100) NOT NULL,
	STATUS_BRAND BIT NOT NULL,
)

CREATE TABLE BRAND_DETAILS (
	ID_BRAND CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	STATUS_DETAILS BIT DEFAULT 0 
)

CREATE TABLE RECEPTION_FORMS (
	ID_REC CHAR(10) PRIMARY KEY,
	ID_CUS CHAR(10)  NOT NULL,
	ID_BRAND CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	NUMBER_PLATES VARCHAR(15) NOT NULL,
	RECEPTION_DATE SMALLDATETIME NOT NULL,
	STATUS_REC BIT
)

--- COM means Component
----PRICE column in CAR_COMPONENTS is used to store CURRENT PRICE
----PRICE column in GRN_Details is used to store PRICE for each import

CREATE TABLE CAR_COMPONENTS (
	ID_COM CHAR(10) PRIMARY KEY,
	NAME_COM NVARCHAR(100) NOT NULL,
	STATUS_COM BIT DEFAULT 0
)

CREATE TABLE COMPONENT_DETAILS (
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	WAGE MONEY DEFAULT 0,
	CUR_PRICE MONEY DEFAULT 0,
	STATUS_DETAILS BIT DEFAULT 0 
)

-----SUPPLIER is equal to PROVIDER
---- GRN means Good Received Note

CREATE TABLE GOOD_RECEIVED_NOTES (
	LOTNUMBER CHAR(10) PRIMARY KEY,
	SUPPLIER CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	IMPORT_TIME SMALLDATETIME NOT NULL,
	--Confirmer means the person who was responsible 
	--for signing the goods received note
	--DATA_ENTRY_STAFF is responsible for entering data to the system
	DATA_ENTRY_STAFF CHAR(10) NOT NULL, --USER
	TOTAL_PAYMENT_GRN MONEY DEFAULT 0,
	STATUS_GRN BIT NOT NULL,
)

CREATE TABLE SUPPLIERS (
	ID_SUPPLIER CHAR(10) PRIMARY KEY,
	NAME_SUPPLIER NVARCHAR(100) NOT NULL,
	PHONE_NUMBER_SUP CHAR(15) NOT NULL,
	ADDRESS_SUP NVARCHAR(100) NOT NULL,
	STATUS_SUPPLIER BIT DEFAULT 0
)

CREATE TABLE GRN_DETAILS(
	GRN_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	LOTNUMBER CHAR(10) NOT NULL,
	ID_COM CHAR(10) NOT NULL,
	COM_PRICE MONEY NOT NULL,
	COM_QUANTITY INT NOT NULL,
	GRN_TOTAL_PAYMENT MONEY DEFAULT 0,
	STATUS_GRN BIT NOT NULL
)

----- Rule of the number of cars that should be received each day

CREATE TABLE GARA_QUANTITY_RULES(
	ID_RULE INT IDENTITY PRIMARY KEY,
	ID_GARA CHAR(10) NOT NULL,
	RULE_DATE SMALLDATETIME NOT NULL,
	MAX_QUANTITY INT NOT NULL,
)

CREATE TABLE REPAIR_PAYMENT_BILL (
	ID_BILL CHAR(10) PRIMARY KEY,
	ID_REC CHAR(10) NOT NULL,
	COMPLETION_DATE SMALLDATETIME NOT NULL,
	TOTAL_PAYMENT MONEY DEFAULT 0.00,
	PAID MONEY DEFAULT 0.00,
	STATUS_BILL BIT DEFAULT 0
)

CREATE TABLE REPAIR_PAYMENT_DETAILS (
	RPD_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_BILL CHAR(10) NOT NULL,
	ID_COM CHAR(10) NOT NULL,
	REPAIR_DESCRIPTION NVARCHAR(100) NOT NULL,
	COM_QUANTITY INT NOT NULL,
	TOTAL_PRICE MONEY DEFAULT 0.00,
	STATUS_RPD BIT NOT NULL
)

CREATE TABLE INVENTORY_MANAGEMENT(
	IM_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	COM_QUANTITY INT NOT NULL
)

CREATE TABLE  BEGINING_INVENTORY (
	BI_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	RENDERING_TIME_BI SMALLDATETIME NOT NULL,
	COM_QUANTITY INT NOT NULL
)

CREATE TABLE ENDING_INVENTORY (
	EI_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	RENDERING_TIME_EI SMALLDATETIME NOT NULL,
	COM_QUANTITY INT NOT NULL
)

CREATE TABLE INCURRED_COSTS (
	CO_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10),
	ID_GARA CHAR(10) NOT NULL,
	INCURRED_COSTS_DATE SMALLDATETIME NOT NULL,
	STATUS_DESCRIPTION NVARCHAR(100) NOT NULL,
	COM_QUANTITY INT,
	INCURRED_COSTS_TTPRICE MONEY DEFAULT 0.00,
	STATUS_IC BIT NOT NULL
)

CREATE TABLE REVENUE (
	ID_REVENUE_REPORT CHAR(10) PRIMARY KEY,
	ID_GARA CHAR(10) NOT NULL,
	RENDERING_TIME SMALLDATETIME NOT NULL,
	TOTAL_REVENUE MONEY DEFAULT 0.00,
	--STATUS_REVENUE BIT NOT NULL
)

CREATE TABLE REVENUE_DETAILS (
	RD_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_GARA CHAR(10) NOT NULL,
	ID_BRAND CHAR(10) NOT NULL,
	RENDER_TIME SMALLDATETIME,
	NUMBER_OF_REPAIRS INT DEFAULT 0,
	RATE FLOAT NOT NULL,
	TOTAL_MONEY MONEY DEFAULT 0.00,
	STATUS_RD BIT NOT NULL
)

GO

--- ADD FOREIGN KEY CONSTRAINT

--- STAFFS
ALTER TABLE STAFFS ADD CONSTRAINT FK_STAFF_POSITION
FOREIGN KEY (ID_POSITION) REFERENCES STAFF_POSITION(ID_POS)
ALTER TABLE STAFFS ADD CONSTRAINT  FK_STAFF_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA

----- CUSTOMER_DETAILS
ALTER TABLE CUSTOMER_DETAILS ADD CONSTRAINT PK_CUSTOMER_DETAILS PRIMARY KEY (ID_CUS, ID_GARA)
ALTER TABLE CUSTOMER_DETAILS ADD CONSTRAINT FK_CUSTOMER_DETAILS_ID_CUS 
FOREIGN KEY (ID_CUS) REFERENCES CUSTOMERS(ID_CUS)
ALTER TABLE CUSTOMER_DETAILS ADD CONSTRAINT FK_CUSTOMER_DETAILS_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

-------RECEPTION_FORMS 
ALTER TABLE RECEPTION_FORMS ADD CONSTRAINT FK_RECEPTION_FORM_ID_CUS 
FOREIGN KEY (ID_CUS) REFERENCES CUSTOMERS(ID_CUS)
ALTER TABLE RECEPTION_FORMS ADD CONSTRAINT FK_RECEPTION_FORMS_ID_BRAND
FOREIGN KEY (ID_BRAND) REFERENCES CAR_BRANDS(ID_BRAND)
ALTER TABLE RECEPTION_FORMS ADD CONSTRAINT FK_REC_ID_CAR
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

------ COMPONENT_DETAILS
ALTER TABLE COMPONENT_DETAILS ADD CONSTRAINT PK_COMPONENT_DETAILS
PRIMARY KEY (ID_COM, ID_GARA)
ALTER TABLE COMPONENT_DETAILS ADD CONSTRAINT FK_COMPONENT_DETAILS_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)
ALTER TABLE COMPONENT_DETAILS ADD CONSTRAINT FK_COMPONENT_DETAILS_ID_COM
FOREIGN KEY (ID_COM) REFERENCES CAR_COMPONENTS(ID_COM)

------- BRAND_DETAILS
ALTER TABLE BRAND_DETAILS ADD CONSTRAINT PK_BRAND_DETAILS PRIMARY KEY (ID_BRAND, ID_GARA)
ALTER TABLE BRAND_DETAILS ADD CONSTRAINT FK_BRAND_DETAILS_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)
ALTER TABLE BRAND_DETAILS ADD CONSTRAINT FK_BRAND_DETAILS_ID_BRAND
FOREIGN KEY (ID_BRAND) REFERENCES CAR_BRANDS(ID_BRAND)

-------GOOD_RECEIVED_NOTES
ALTER TABLE GOOD_RECEIVED_NOTES ADD CONSTRAINT FK_GOOD_RECEIVED_NOTES_USER
FOREIGN KEY (DATA_ENTRY_STAFF) REFERENCES ACCOUNTS(ID_ACC)
ALTER TABLE GOOD_RECEIVED_NOTES ADD CONSTRAINT FK_GOOD_RECEIVED_NOTES_ID_CAR
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)
ALTER TABLE GOOD_RECEIVED_NOTES ADD CONSTRAINT FK_GRN_SUPPLIER_GARA
FOREIGN KEY(SUPPLIER) REFERENCES SUPPLIERS(ID_SUPPLIER)

-------GRN_DETAILS
ALTER TABLE GRN_DETAILS ADD CONSTRAINT FK_GRN_DETAILS_LOTNUMBER
FOREIGN KEY (LOTNUMBER) REFERENCES GOOD_RECEIVED_NOTES(LOTNUMBER)
ALTER TABLE GRN_DETAILS ADD CONSTRAINT FK_GRN_DETAILS_ID_COM
FOREIGN KEY (ID_COM) REFERENCES CAR_COMPONENTS(ID_COM)

------ GARA_QUANTITY_RULE

ALTER TABLE GARA_QUANTITY_RULES ADD CONSTRAINT FK_QUANTITY_RULES_ID_GARA 
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

------REPAIR_PAYMENT_BILL
ALTER TABLE REPAIR_PAYMENT_BILL ADD CONSTRAINT FK_REPAIR_PAYMENT_BILL
FOREIGN KEY (ID_REC) REFERENCES RECEPTION_FORMS(ID_REC)

------ REPAIR_PAYMENT_DETAILS
ALTER TABLE REPAIR_PAYMENT_DETAILS ADD CONSTRAINT FK_REPAIR_PAYMENT_DETAILS_IDBILL
FOREIGN KEY (ID_BILL) REFERENCES REPAIR_PAYMENT_BILL(ID_BILL)
ALTER TABLE REPAIR_PAYMENT_DETAILS ADD CONSTRAINT FK_REPAIR_PAYMENT_DETAILS_IDCOM
FOREIGN KEY (ID_COM) REFERENCES CAR_COMPONENTS(ID_COM)

------INVENTORY_MANAGEMENT

ALTER TABLE INVENTORY_MANAGEMENT ADD CONSTRAINT FK_INVENTORY_MANAGEMENT 
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

------BEGINING_INVENTORY
ALTER TABLE BEGINING_INVENTORY ADD CONSTRAINT FK_BEGINING_INVENTORY_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

-----ENDING_INVENTORY
ALTER TABLE ENDING_INVENTORY ADD CONSTRAINT FK_ENDING_INVENTORY_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

-----INCURRED_COSTS
ALTER TABLE INCURRED_COSTS ADD CONSTRAINT FK_INCURRED_COSTS_IDCOM
FOREIGN KEY (ID_COM) REFERENCES CAR_COMPONENTS(ID_COM)
ALTER TABLE INCURRED_COSTS ADD CONSTRAINT FK_INCURRED_COSTS_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA (ID_GARA)

-----REVENUE
ALTER TABLE REVENUE ADD CONSTRAINT FK_REVENUE_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA (ID_GARA)

-----REVENUE_DETAILS
ALTER TABLE REVENUE_DETAILS ADD CONSTRAINT FK_REVENUE_DETAILS_ID_COM
FOREIGN KEY (ID_BRAND) REFERENCES CAR_BRANDS(ID_BRAND)
ALTER TABLE REVENUE_DETAILS ADD CONSTRAINT FK_REVENUE_DETAILS_ID_GARA
FOREIGN KEY (ID_GARA) REFERENCES CAR_GARA(ID_GARA)

--------- DATA

SET DATEFORMAT DMY

---- CAR_GARA

INSERT INTO CAR_GARA VALUES ('GR1', N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương', '0976667898', 0),
							('GR2', N'Tân Lập, Tây Hòa, Dĩ An, Bình Dương', '0976667898', 0),
							('GR3', N'123 Nguyễn Văn Cừ, Phường 5, Quận 8, TP Hồ Chí Minh', '0976667898', 0)
						
----- STAFF POSITION

INSERT INTO STAFF_POSITION VALUES ('POS1', N'Quản lý'),
									('POS2', N'Nhân viên sửa chữa'),
									('POS3', N'Tạp vụ')

----- STAFFS

INSERT INTO STAFFS VALUES 
	('STF1', N'Nguyễn Văn Tèo', '1/1/2004', N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương',
		'vanteo@gmail.com', '0344357896', 10000000, 'POS1', 'GR1', 0),
	('STF2', N'Nguyễn Văn Tý', '2/2/2004', N'Tân Lập, Tây Hòa, Dĩ An, Bình Dương',
		'vanty@gmail.com', '0344343346', 15000000, 'POS1', 'GR2', 0),
	('STF3', N'Nguyễn Văn Dậu', '3/3/2004', N'123 Nguyễn Văn Cừ, Phường 5, Quận 8, TP Hồ Chí Minh',
		'vandau@gmail.com', '0945357896', 19000000, 'POS1', 'GR3', 0),
	('STF4', N'Nguyễn Thị Thơm', '4/4/2004', N'123 Nguyễn Văn Cừ, Phường 5, Quận 8, TP Hồ Chí Minh',
		'thithom@gmail.com', '0245334896', 3000000, 'POS2', 'GR1', 0),
	('STF5', N'Hồ Văn Chương', '5/5/2004', N'123 Lý Thái Tổ, Phường 5, Quận 3, TP Hồ Chí Minh',
		'vanchuong@gmail.com', '0232357896', 5000000, 'POS3', 'GR1', 0),
	('STF6', N'Hồ Văn Vở', '6/6/2004', N'123 CMT8, Phường 5, Quận 10, TP Hồ Chí Minh',
		'vanvo@gmail.com', '0932567896', 3500000, 'POS2', 'GR2', 0),
	('STF7', N'Thái Văn Mẹo', '7/7/2004', N'456 Lý Thường Kiệt, Phường 5, Quận 10, TP Hồ Chí Minh',
		'vanmeo@gmail.com', '0932567896', 2000000, 'POS3', 'GR2', 0),
	('STF8', N'Lý Thị Sách', '8/8/2004', N'123 Mai Xuân Thưởng, Phường 5, Quận 11, TP Hồ Chí Minh',
		'thisach@gmail.com', '0252567896', 6000000, 'POS2', 'GR3', 0),
	('STF9', N'Hồ Văn Nha', '9/9/2004', N'234 Võ Nguyên Giáp, Phường 1, Quận 1, TP Hồ Chí Minh',
		'vannha@gmail.com', '0232567896', 25000000, 'POS3', 'GR3', 0)

----- ACCOUNTS 
----- STATUS 0: MANAGER, STATUS 1: STAFF

INSERT INTO ACCOUNTS VALUES ('AC1','vanteo123', '12345', 'STF1', 0, 0),
							('AC2','vanty123', '12345', 'STF2', 0, 0),
							('AC3','vandau123', '12345', 'STF3', 0, 0),
							('AC4','thithom123', '12345', 'STF4', 1, 0),
							('AC5','vanvo123', '12345', 'STF6', 1, 0),
							('AC6','thisach123', '12345', 'STF8', 1, 0)

----- CUSTOMERS

INSERT INTO CUSTOMERS VALUES ('CUS1', N'Nguyễn Thị Thơm', '0343435645', 
								N'123 CMT8, Phường 5, Quận 10, TP Hồ Chí Minh', 0, 0),
							('CUS2', N'Nguyễn Thị Mơ', '0232343564', 
								N'234 Võ Nguyên Giáp, Phường 1, Quận 1, TP Hồ Chí Minh', 0, 0),
							('CUS3', N'Nguyễn Văn Dậu', '0943785645', 
								N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương', 0, 0)

----- CAR BRANDS

INSERT INTO CAR_BRANDS VALUES ('BR1', N'Toyota',  0),
							('BR2', N'Ford',  0),
							('BR3', N'Toyota',  0),
							('BR4', N'Mazda', 0),
							('BR5', N'Suzuki',  0),
							('BR6', N'BMW', 0)

----- BRAND_DETAILS

INSERT INTO BRAND_DETAILS VALUES ('BR1', 'GR1', 0),
								('BR1', 'GR2', 0),
								('BR1', 'GR3', 0),
								('BR2', 'GR1', 0),
								('BR3', 'GR2', 0),
								('BR2', 'GR3', 0),
								('BR3', 'GR1', 0),
								('BR4', 'GR1', 0),
								('BR5', 'GR3', 0),
								('BR6', 'GR2', 0)
						
----- RECEPTION_FORMS

INSERT INTO RECEPTION_FORMS VALUES ('REC1', 'CUS1', 'BR1', 'GR1', '23-X2-234587', 
									'22/3/2024', 0),
								   ('REC2', 'CUS2', 'BR3', 'GR2', '51-X3-264587', 
									'21/3/2024', 0),
								   ('REC3', 'CUS3', 'BR5', 'GR3', '23-X2-639587', 
									'1/2/2024', 0)
					
------ CAR_COMPONENTS

INSERT INTO CAR_COMPONENTS VALUES ('COM1', N'Gương chiếu hậu', 0),
									('COM2', N'Đèn pha',  0),
									('COM3', N'Cản trước', 0),
									('COM4', N'Thanh giảm chấn',  0),
									('COM5', N'Kính chắn gió', 0),
									('COM6', N'Phanh bánh trước', 0),
									('COM7', N'Gương chiếu hậu', 0),
									('COM8', N'Phanh bánh trước',  0),
									('COM9', N'Tay cầm mở cửa', 0),
									('COM10', N'Ốp chắn bánh xe',  0)

------ COMPONENTS_DETAILS

INSERT INTO COMPONENT_DETAILS (ID_COM, ID_GARA, WAGE,STATUS_DETAILS) 
VALUES ('COM1', 'GR1', 1000000, 0),
	('COM2', 'GR1', 2000000, 0),
	('COM2', 'GR2', 500000, 0),
	('COM3', 'GR3', 200000, 0),
	('COM4', 'GR1', 600000, 0),
	('COM5', 'GR3', 1000000, 0),
	('COM6', 'GR3', 2000000, 0),
	('COM7', 'GR1', 500000, 0),
	('COM8', 'GR2', 800000, 0),
	('COM9', 'GR3', 300000, 0),
	('COM10', 'GR1', 4000000, 0),
	('COM1', 'GR3', 300000, 0),
	('COM2', 'GR3', 500000, 0),
	('COM3', 'GR1', 600000, 0),
	('COM4', 'GR3', 300000, 0),
	('COM3', 'GR2', 200000, 0)

---- SUPPLIERS

INSERT INTO SUPPLIERS 
VALUES ('SUP1', 'Công ty TNHH Thương mại cổ phần Vũ Phương', 
	'0983734743', N'123 Khởi Nghĩa Tam Kỳ, P12, Q8, TP HCM', 0),
	('SUP2', 'Công ty phụ tùng ô tô Thượng Hải', 
	'0983734743', N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương', 0),
	('SUP3', 'Công ty phụ tùng ô tô Mai Phương', 
	'0983734743', N'123 Lý Thường Kiệt, P1, Q12, TP HCM', 0)


----- GOOD-RECEIVED_NOTES

INSERT INTO GOOD_RECEIVED_NOTES 
(LOTNUMBER, SUPPLIER, ID_GARA, IMPORT_TIME, DATA_ENTRY_STAFF, STATUS_GRN)
VALUES ('LOT1', 'SUP1',
			'GR1','22/2/2024',  'AC4', 0),
		('LOT2', 'SUP2',
			'GR2','22/5/2024', 'AC5', 0),
		('LOT3', 'SUP2',
			'GR3','22/4/2024', 'AC6', 0)


----- GRN_DETAILS
INSERT INTO GRN_DETAILS(LOTNUMBER, ID_COM, COM_PRICE, COM_QUANTITY, STATUS_GRN)
					VALUES ('LOT1', 'COM1', 2000000, 5, 0),
							('LOT1', 'COM10', 1000000, 10, 0),
							('LOT1', 'COM2', 500000, 3, 0),
							('LOT1', 'COM4', 3000000, 35, 0),
							('LOT1', 'COM3', 5000000, 10, 0),
							('LOT1', 'COM7', 2300000, 20, 0)

---- GARA_QUANTITY_RULES

INSERT INTO GARA_QUANTITY_RULES(ID_GARA, RULE_DATE, MAX_QUANTITY)
VALUES ('GR1', '21/2/2024', 5),
		('GR2', '22/2/2024', 5),
		('GR3', '23/2/2024', 5)

---- REPAIR_PAYMENT_BILL

INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL1', 'REC1', '22/2/2024', 0),
		('BIL2', 'REC2', '22/2/2024', 0),
		('BIL3', 'REC3', '22/2/2024', 0)

INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL4', 'REC1', '22/2/2024', 0)
INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL12', 'REC1', '22/4/2024', 0),
		('BIL6', 'REC2', '22/4/2024', 0),
		('BIL7', 'REC3', '22/4/2024', 0)
INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL9', 'REC4', '22/4/2024', 0)
INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL14', 'REC4', '22/4/2024', 0)
INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL14', 'REC4', '22/4/2024', 0)
INSERT INTO REPAIR_PAYMENT_BILL(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES ('BIL15', 'REC4', '22/4/2024', 0)
UPDATE REPAIR_PAYMENT_BILL SET ID_REC='REC1' WHERE ID_BILL='BIL15'

INSERT INTO RECEPTION_FORMS VALUES ('REC4', 'CUS1', 'BR2', 'GR1', '23-X2-234587', 
									'22/3/2024', 0)
SELECT * FROM RECEPTION_FORMS
SELECT * FROM BRAND_DETAILS
DELETE FROM REPAIR_PAYMENT_BILL WHERE ID_BILL='BIL10'
SELECT * FROM REPAIR_PAYMENT_BILL
SELECT * FROM REVENUE_DETAILS
UPDATE REVENUE_DETAILS SET NUMBER_OF_REPAIRS=1, RATE=1 WHERE RD_ORDINAL_NUM=6 OR RD_ORDINAL_NUM=7


----- REPAIR_PAYMENT_DETAILS

INSERT INTO REPAIR_PAYMENT_DETAILS(ID_BILL, ID_COM, REPAIR_DESCRIPTION, COM_QUANTITY, STATUS_RPD)
							VALUES ('BIL2', 'COM8', N'Cháy gương chiếu hậu', 1, 0),
									('BIL1', 'COM2', N'Bảo trì(thay mới) đèn pha', 1, 0),
									('BIL1', 'COM10', N'Cháy gương chiếu hậu', 1, 0),
									('BIL3', 'COM4', N'Bể kính', 1, 0),
									('BIL3', 'COM3', N'Bể thanh giảm chấn', 1, 0)

----- INCURRED_COST

INSERT INTO INCURRED_COSTS (ID_COM, ID_GARA, INCURRED_COSTS_DATE, 
							STATUS_DESCRIPTION, COM_QUANTITY, STATUS_IC) 
								VALUES ('COM2', 'GR1','22/2/2024', N'Bị hỏng', 1, 0)
							INSERT INTO INCURRED_COSTS (ID_COM, ID_GARA, INCURRED_COSTS_DATE, 
							STATUS_DESCRIPTION, COM_QUANTITY, STATUS_IC) 
								VALUES ('COM3', 'GR3', '22/2/2024', N'Bị hỏng', 1, 0)
							INSERT INTO INCURRED_COSTS (ID_COM, ID_GARA, INCURRED_COSTS_DATE, 
							STATUS_DESCRIPTION, COM_QUANTITY, STATUS_IC) 
								VALUES ('COM4', 'GR1', '22/2/2024', N'Bị hỏng', 1, 0)

