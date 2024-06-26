﻿IF DB_ID('CAR_GARAGE_MANAGEMENT') IS NULL
CREATE DATABASE CAR_GARAGE_MANAGEMENT

GO

IF DB_ID('CAR_GARAGE_MANAGEMENT') IS NOT NULL
USE CAR_GARAGE_MANAGEMENT
GO
 
-- Note: Status fields are used to check the status of the record 
-- (0: DELETED, 1: ACTIVE)

IF OBJECT_ID('CAR_GARA') IS NULL
CREATE TABLE CAR_GARA (
	ID_GARA CHAR(10) PRIMARY KEY,
	ADDRESS_GARA NVARCHAR(100) NOT NULL,
	PHONE_NUMBER_GARA CHAR(15) NOT NULL,
	STATUS_GARA BIT NOT NULL
)

IF OBJECT_ID('STAFF_POSITION') IS NULL
CREATE TABLE STAFF_POSITION (
	ID_POS CHAR(10) PRIMARY KEY,
	NAME_POS NVARCHAR(50) NOT NULL,
)

IF OBJECT_ID('STAFFS') IS NULL
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

IF OBJECT_ID('ACCOUNTS') IS NULL
CREATE TABLE ACCOUNTS (
	ID_ACC CHAR(10) PRIMARY KEY,
	USERNAME VARCHAR(50) NOT NULL,
	PASSWORD VARCHAR(50) NOT NULL,
	ID_STAFF CHAR(10) NOT NULL,
	ACC_AUTHORIZATION BIT NOT NULL,
	STATUS_ACCOUNT BIT NOT NULL,
)

IF OBJECT_ID('CUSTOMERS') IS NULL
CREATE TABLE CUSTOMERS (
	ID_CUS CHAR(10) PRIMARY KEY,
	NAME_CUS NVARCHAR(100) NOT NULL,
	PHONE_NUMBER_CUS CHAR(15) NOT NULL,
	ADDRESS_CUS NVARCHAR(100) NOT NULL,
	--DEBT MONEY DEFAULT 0,
	STATUS_CUS BIT NOT NULL,
)

IF OBJECT_ID('CUSTOMER_DETAILS') IS NULL
CREATE TABLE CUSTOMER_DETAILS  (
	ID_CUS CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	DEBT MONEY DEFAULT 0,
)

IF OBJECT_ID('CAR_BRANDS') IS NULL
CREATE TABLE CAR_BRANDS (
	ID_BRAND CHAR(10) PRIMARY KEY,
	NAME_BRAND NVARCHAR(100) NOT NULL,
	STATUS_BRAND BIT NOT NULL,
)

IF OBJECT_ID('BRAND_DETAILS') IS NULL
CREATE TABLE BRAND_DETAILS (
	ID_BRAND CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	STATUS_DETAILS BIT DEFAULT 0 
)

IF OBJECT_ID('RECEPTION_FORMS') IS NULL
CREATE TABLE RECEPTION_FORMS (
	ID_REC CHAR(10) PRIMARY KEY,
	ID_CUS CHAR(10)  NOT NULL,
	ID_BRAND CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	NUMBER_PLATES VARCHAR(15) NOT NULL,
	RECEPTION_DATE SMALLDATETIME NOT NULL,
	STATUS_REC BIT
)

-- COM: Component

IF OBJECT_ID('CAR_COMPONENTS') IS NULL
CREATE TABLE CAR_COMPONENTS (
	ID_COM CHAR(10) PRIMARY KEY,
	NAME_COM NVARCHAR(100) NOT NULL,
	STATUS_COM BIT DEFAULT 0
)

IF OBJECT_ID('COMPONENT_DETAILS ') IS NULL
CREATE TABLE COMPONENT_DETAILS (
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	WAGE MONEY DEFAULT 0,
	CUR_PRICE MONEY DEFAULT 0,
	STATUS_DETAILS BIT DEFAULT 0 
)

-- SUPPLIER: PROVIDER
-- GRN: Good Received Note

IF OBJECT_ID('GOOD_RECEIVED_NOTES') IS NULL
CREATE TABLE GOOD_RECEIVED_NOTES (
	LOTNUMBER CHAR(10) PRIMARY KEY,
	SUPPLIER CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	IMPORT_TIME SMALLDATETIME NOT NULL,
	DATA_ENTRY_STAFF CHAR(10) NOT NULL, --  is responsible for entering data to the system
	TOTAL_PAYMENT_GRN MONEY DEFAULT 0,
	STATUS_GRN BIT NOT NULL,
)

IF OBJECT_ID('SUPPLIERS') IS NULL
CREATE TABLE SUPPLIERS (
	ID_SUPPLIER CHAR(10) PRIMARY KEY,
	NAME_SUPPLIER NVARCHAR(100) NOT NULL,
	PHONE_NUMBER_SUP CHAR(15) NOT NULL,
	ADDRESS_SUP NVARCHAR(100) NOT NULL,
	STATUS_SUPPLIER BIT DEFAULT 0
)

IF OBJECT_ID('GRN_DETAILS') IS NULL
CREATE TABLE GRN_DETAILS(
	GRN_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	LOTNUMBER CHAR(10) NOT NULL,
	ID_COM CHAR(10) NOT NULL,
	COM_PRICE MONEY NOT NULL, -- Imported price
	COM_QUANTITY INT NOT NULL,
	GRN_TOTAL_PAYMENT MONEY DEFAULT 0,
	STATUS_GRN BIT NOT NULL
)

----- Rule of the number of cars that should be received each day

IF OBJECT_ID('GARA_QUANTITY_RULES') IS NULL
CREATE TABLE GARA_QUANTITY_RULES(
	ID_RULE INT IDENTITY PRIMARY KEY,
	ID_GARA CHAR(10) NOT NULL,
	RULE_DATE SMALLDATETIME NOT NULL,
	MAX_QUANTITY INT NOT NULL,
)

IF OBJECT_ID('REPAIR_PAYMENT_BILL') IS NULL
CREATE TABLE REPAIR_PAYMENT_BILL (
	ID_BILL CHAR(10) PRIMARY KEY,
	ID_REC CHAR(10) NOT NULL,
	COMPLETION_DATE SMALLDATETIME NOT NULL,
	TOTAL_PAYMENT MONEY DEFAULT 0.00,
	PAID MONEY DEFAULT 0.00,
	STATUS_BILL BIT DEFAULT 0
)

IF OBJECT_ID('REPAIR_PAYMENT_DETAILS') IS NULL
CREATE TABLE REPAIR_PAYMENT_DETAILS (
	RPD_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_BILL CHAR(10) NOT NULL,
	ID_COM CHAR(10) NOT NULL,
	REPAIR_DESCRIPTION NVARCHAR(100) NOT NULL,
	COM_QUANTITY INT NOT NULL,
	TOTAL_PRICE MONEY DEFAULT 0.00,
	STATUS_RPD BIT NOT NULL
)

IF OBJECT_ID('INVENTORY_MANAGEMENT') IS NULL
CREATE TABLE INVENTORY_MANAGEMENT(
	IM_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	COM_QUANTITY INT NOT NULL
)

IF OBJECT_ID('BEGINING_INVENTORY') IS NULL
CREATE TABLE BEGINING_INVENTORY (
	BI_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	RENDERING_TIME_BI SMALLDATETIME NOT NULL,
	COM_QUANTITY INT NOT NULL
)

IF OBJECT_ID('ENDING_INVENTORY') IS NULL
CREATE TABLE ENDING_INVENTORY (
	EI_ORDINAL_NUM INT IDENTITY PRIMARY KEY,
	ID_COM CHAR(10) NOT NULL,
	ID_GARA CHAR(10) NOT NULL,
	RENDERING_TIME_EI SMALLDATETIME NOT NULL,
	COM_QUANTITY INT NOT NULL
)

IF OBJECT_ID('INCURRED_COSTS') IS NULL
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


IF OBJECT_ID('REVENUE') IS NULL
CREATE TABLE REVENUE (
	ID_REVENUE_REPORT CHAR(10) PRIMARY KEY,
	ID_GARA CHAR(10) NOT NULL,
	RENDERING_TIME SMALLDATETIME NOT NULL,
	TOTAL_REVENUE MONEY DEFAULT 0.00,
)

IF OBJECT_ID('REVENUE_DETAILS') IS NULL
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
