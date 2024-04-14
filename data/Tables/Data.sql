IF DB_ID('CAR_GARAGE_MANAGEMENT') IS NOT NULL
USE CAR_GARAGE_MANAGEMENT

SET DATEFORMAT DMY

-- CAR_GARA

INSERT INTO CAR_GARA 
VALUES 
('GR1', N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương', '0976667898', 0),
('GR2', N'Tân Lập, Tây Hòa, Dĩ An, Bình Dương', '0976667898', 0),
('GR3', N'123 Nguyễn Văn Cừ, Phường 5, Quận 8, TP Hồ Chí Minh', '0976667898', 0)
						
-- STAFF POSITION

INSERT INTO STAFF_POSITION 
VALUES 
('POS1', N'Quản lý'),
('POS2', N'Nhân viên sửa chữa'),
('POS3', N'Tạp vụ')

-- STAFFS

INSERT INTO STAFFS 
VALUES 
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

-- ACCOUNTS 
-- STATUS 0: MANAGER, STATUS 1: STAFF

INSERT INTO ACCOUNTS 
VALUES 
('AC1','vanteo123', '12345', 'STF1', 0, 0),
('AC2','vanty123', '12345', 'STF2', 0, 0),
('AC3','vandau123', '12345', 'STF3', 0, 0),
('AC4','thithom123', '12345', 'STF4', 1, 0),
('AC5','vanvo123', '12345', 'STF6', 1, 0),
('AC6','thisach123', '12345', 'STF8', 1, 0)

-- CUSTOMERS

INSERT INTO CUSTOMERS 
VALUES 
('CUS1', N'Nguyễn Thị Thơm', '0343435645', N'123 CMT8, Phường 5, Quận 10, TP Hồ Chí Minh', 0, 0),
('CUS2', N'Nguyễn Thị Mơ', '0232343564', N'234 Võ Nguyên Giáp, Phường 1, Quận 1, TP Hồ Chí Minh', 0, 0),
('CUS3', N'Nguyễn Văn Dậu', '0943785645', N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương', 0, 0)

-- CUSTOMER_DETAILS
INSERT INTO CUSTOMER_DETAILS(ID_CUS, ID_GARA) 
VALUES 
('CUS1', 'GR1'),
('CUS1', 'GR2'),
('CUS1', 'GR3'),
('CUS2', 'GR1'),
('CUS2', 'GR2'),
('CUS2', 'GR3'),
('CUS3', 'GR1'),
('CUS3', 'GR2'),
('CUS3', 'GR3')

-- CAR BRANDS

INSERT INTO CAR_BRANDS 
VALUES 
('BR1', N'Toyota',  0),
('BR2', N'Ford',  0),
('BR3', N'Toyota',  0),
('BR4', N'Mazda', 0),
('BR5', N'Suzuki',  0),
('BR6', N'BMW', 0)

-- BRAND_DETAILS

INSERT INTO BRAND_DETAILS 
VALUES 
('BR1', 'GR1', 0),
('BR1', 'GR2', 0),
('BR1', 'GR3', 0),
('BR2', 'GR1', 0),
('BR3', 'GR2', 0),
('BR2', 'GR3', 0),
('BR3', 'GR1', 0),
('BR4', 'GR1', 0),
('BR5', 'GR3', 0),
('BR6', 'GR2', 0)
						
-- RECEPTION_FORMS

INSERT INTO RECEPTION_FORMS 
VALUES 
('REC1', 'CUS1', 'BR1', 'GR1', '23-X2-234587', '22/3/2024', 0),
('REC2', 'CUS2', 'BR3', 'GR2', '51-X3-264587', '21/3/2024', 0),
('REC3', 'CUS3', 'BR5', 'GR3', '23-X2-639587', '1/2/2024', 0)
					
-- CAR_COMPONENTS

INSERT INTO CAR_COMPONENTS 
VALUES 
('COM1', N'Gương chiếu hậu', 0),
('COM2', N'Đèn pha',  0),
('COM3', N'Cản trước', 0),
('COM4', N'Thanh giảm chấn',  0),
('COM5', N'Kính chắn gió', 0),
('COM6', N'Phanh bánh trước', 0),
('COM7', N'Gương chiếu hậu', 0),
('COM8', N'Phanh bánh trước',  0),
('COM9', N'Tay cầm mở cửa', 0),
('COM10', N'Ốp chắn bánh xe',  0)

-- COMPONENTS_DETAILS

INSERT INTO COMPONENT_DETAILS (ID_COM, ID_GARA, WAGE,STATUS_DETAILS) 
VALUES 
('COM1', 'GR1', 1000000, 0),
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

-- SUPPLIERS

INSERT INTO SUPPLIERS 
VALUES 
('SUP1', 'Công ty TNHH Thương mại cổ phần Vũ Phương', 
'0983734743', N'123 Khởi Nghĩa Tam Kỳ, P12, Q8, TP HCM', 0),
('SUP2', 'Công ty phụ tùng ô tô Thượng Hải', 
'0983734743', N'Tân Lập, Đông Hòa, Dĩ An, Bình Dương', 0),
('SUP3', 'Công ty phụ tùng ô tô Mai Phương', 
'0983734743', N'123 Lý Thường Kiệt, P1, Q12, TP HCM', 0)

-- GOOD-RECEIVED_NOTES

INSERT INTO GOOD_RECEIVED_NOTES 
(LOTNUMBER, SUPPLIER, ID_GARA, IMPORT_TIME, DATA_ENTRY_STAFF, STATUS_GRN)
VALUES 
('LOT1', 'SUP1', 'GR1','22/2/2024',  'AC4', 0),
('LOT2', 'SUP2', 'GR2','22/5/2024', 'AC5', 0),
('LOT3', 'SUP2', 'GR3','22/4/2024', 'AC6', 0)


-- GRN_DETAILS
INSERT INTO GRN_DETAILS(LOTNUMBER, ID_COM, COM_PRICE, COM_QUANTITY, STATUS_GRN)
VALUES 
('LOT1', 'COM1', 2000000, 5, 0),
('LOT1', 'COM10', 1000000, 10, 0),
('LOT1', 'COM2', 500000, 3, 0),
('LOT1', 'COM4', 3000000, 35, 0),
('LOT1', 'COM3', 5000000, 10, 0),
('LOT1', 'COM7', 2300000, 20, 0)

---- GARA_QUANTITY_RULES

INSERT INTO GARA_QUANTITY_RULES(ID_GARA, RULE_DATE, MAX_QUANTITY)
VALUES 
('GR1', '21/2/2024', 5),
('GR2', '22/2/2024', 5),
('GR3', '23/2/2024', 5)

---- REPAIR_PAYMENT_BILL

INSERT INTO REPAIR_PAYMENT_BILL
(ID_BILL, ID_REC, COMPLETION_DATE, STATUS_BILL)
VALUES 
('BIL1', 'REC1', '22/2/2024', 0),
('BIL2', 'REC2', '22/2/2024', 0),
('BIL3', 'REC3', '22/2/2024', 0)

-- REPAIR_PAYMENT_DETAILS

INSERT INTO REPAIR_PAYMENT_DETAILS
(ID_BILL, ID_COM, REPAIR_DESCRIPTION, COM_QUANTITY, STATUS_RPD)
VALUES 
('BIL2', 'COM8', N'Cháy gương chiếu hậu', 1, 0),
('BIL1', 'COM2', N'Bảo trì(thay mới) đèn pha', 1, 0),
('BIL1', 'COM10', N'Cháy gương chiếu hậu', 1, 0),
('BIL3', 'COM4', N'Bể kính', 1, 0),
('BIL3', 'COM3', N'Bể thanh giảm chấn', 1, 0)

----- INCURRED_COST

INSERT INTO INCURRED_COSTS 
(ID_COM, ID_GARA, INCURRED_COSTS_DATE, STATUS_DESCRIPTION, COM_QUANTITY, STATUS_IC) 
VALUES 
('COM2', 'GR1','22-2-2024', N'Bị hỏng', 1, 0),
('COM3', 'GR3', '22-2-2024', N'Bị hỏng', 1, 0),
('COM4', 'GR1', '22-2-2024', N'Bị hỏng', 1, 0)


