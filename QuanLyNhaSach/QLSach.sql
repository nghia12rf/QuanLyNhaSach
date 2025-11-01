CREATE DATABASE QLSACH
GO
USE QLSACH
GO

CREATE TABLE TACGIA
(
    MATACGIA CHAR(5) PRIMARY KEY,
    TENTACGIA NVARCHAR(50),
    DIACHI NVARCHAR(50),
    TIEUSU NVARCHAR(100),
    DIENTHOAI CHAR(11)
)

CREATE TABLE CHUDE
(
    MACHUDE CHAR(5) PRIMARY KEY,
    TENCHUDE NVARCHAR(50)
)

CREATE TABLE NHAXUATBAN
(
    MANXB CHAR(5) PRIMARY KEY,
    TENNXB NVARCHAR(100),
    DIACHI NVARCHAR(100),
    DIENTHOAI CHAR(11)
)

CREATE TABLE SACH
(
    MASACH CHAR(5) PRIMARY KEY,
    TENSACH NVARCHAR(100),
    GIABAN MONEY,
    MOTA NVARCHAR(500),
    NGAYCAPNHAT DATE,
    ANHBIA CHAR(50),
    SOLUONGTON INT,
    MACHUDE CHAR(5),
    MANXB CHAR(5),
    MOI BIT,
    FOREIGN KEY (MACHUDE) REFERENCES CHUDE(MACHUDE),
    FOREIGN KEY (MANXB) REFERENCES NHAXUATBAN(MANXB)
)

CREATE TABLE THAMGIA
(
    MASACH CHAR(5),
    MATACGIA CHAR(5),
    VAITRO NVARCHAR(20),
    VITRI NVARCHAR(20),
    FOREIGN KEY (MASACH) REFERENCES SACH(MASACH),
    FOREIGN KEY (MATACGIA) REFERENCES TACGIA(MATACGIA),
    PRIMARY KEY (MASACH, MATACGIA)
)

CREATE TABLE KHACHHANG
(
    MAKH CHAR(5) PRIMARY KEY,
    TENKH NVARCHAR(50),
    NGAYSINH DATE,
    GIOITINH NVARCHAR(5),
    DIENTHOAI CHAR(11),
    TAIKHOAN CHAR(20),
    MATKHAU CHAR(20),
    EMAIL CHAR(50),
    DIACHI NVARCHAR(100)
)

CREATE TABLE DONHANG
(
    MADONHANG CHAR(5) PRIMARY KEY,
    NGAYGIAO DATE,
    NGAYDAT DATE,
    DATHANHTOAN BIT,
    TINHTRANGIAOHANG NVARCHAR(30),
    MAKH CHAR(5),
    FOREIGN KEY (MAKH) REFERENCES KHACHHANG(MAKH)
)

CREATE TABLE CHITIETDONHANG
(
    MADONHANG CHAR(5),
    MASACH CHAR(5),
    SOLUONG INT,
    DONGIA MONEY,
    FOREIGN KEY (MADONHANG) REFERENCES DONHANG(MADONHANG),
    FOREIGN KEY (MASACH) REFERENCES SACH(MASACH),
    PRIMARY KEY (MADONHANG, MASACH)
)
GO

INSERT INTO CHUDE(MACHUDE, TENCHUDE) VALUES
('CD001', N'Công nghệ thông tin'),
('CD002', N'Âm nhạc'),
('CD003', N'Khoa học kỹ thuật'),
('CD004', N'Khoa học vật lý'),
('CD005', N'Khoa học xã hội'),
('CD006', N'Phát triển bản thân'),
('CD007', N'Kinh Tế');
GO

INSERT INTO NHAXUATBAN(MANXB, TENNXB, DIACHI, DIENTHOAI) VALUES
('NXB01', N'Nhà xuất bản Trẻ', N'161B Lý Chính Thắng, P.7, Q.3, TP.HCM', '02839316289'),
('NXB02', N'Đại học Quốc gia', N'Khu phố 6, P.Linh Trung, Q.Thủ Đức, TP.HCM', '02837242160'),
('NXB03', N'Khoa học kỹ thuật', N'70 Trần Hưng Đạo, Q.Hoàn Kiếm, Hà Nội', '02439433182'),
('NXB04', N'Kim Đồng', N'55 Quang Trung, P.Nguyễn Du, Q.Hai Bà Trưng, Hà Nội', '02439434730'),
('NXB05', N'Hồng Đức', N'65 Tràng Thi, P.Hàng Bông, Q.Hoàn Kiếm, Hà Nội', '02439287640'),
('NXB06', N'Lao động - Xã hội', N'41B Lý Thái Tổ, Q.Hoàn Kiếm, Hà Nội', '02438253641'),
('NXB07', N'Nhà Phụ nữ', N'39 Hàng Chuối, Q.Hai Bà Trưng, Hà Nội', '02439710717');
GO

INSERT INTO TACGIA(MATACGIA, TENTACGIA, DIACHI, TIEUSU, DIENTHOAI) VALUES
('TG001', N'Nguyễn Bá Tồn', N'Q.Bình Thạnh, TP.HCM', N'Chuyên gia về thiết kế đồ họa', '0905123456'),
('TG002', N'Trần Hạ', N'Q.1, TP.HCM', N'Chuyên gia Photoshop', '0905654321'),
('TG003', N'John C. Maxwell', N'Florida, Hoa Kỳ', N'Diễn giả, tác giả nổi tiếng', '0912345678'),
('TG004', N'Napoleon Hill', N'Virginia, Hoa Kỳ', N'Tác giả sách self-help', '0987654321'),
('TG005', N'Phạm Công Anh', N'Hà Nội', N'Giảng viên CNTT', '0934567890');
GO

INSERT INTO SACH(MASACH, TENSACH, GIABAN, MOTA, NGAYCAPNHAT, ANHBIA, SOLUONGTON, MACHUDE, MANXB, MOI) VALUES
('S0001', N'Tự học Photoshop CS5 Qua hình ảnh', 82000, N'Nhằm đáp ứng nhu cầu tự học Photoshop, sách giới thiệu...', '2025-10-10', 'photoshop_cs5.jpg', 50, 'CD001', 'NXB03', 1),
('S0002', N'180 Thủ Thuật và Mẹo Hay Trong Flash CS4', 65000, N'Tổng hợp các thủ thuật hay trong Flash CS4', '2025-10-05', 'flash_cs4.jpg', 30, 'CD001', 'NXB03', 1),
('S0003', N'17 Nguyên Tắc Vàng Trong Làm Việc Nhóm', 120000, N'Tác giả John C. Maxwell', '2025-10-11', '17nguyentac.jpg', 20, 'CD006', 'NXB06', 1),
('S0004', N'Think and Grow Rich', 150000, N'Tác giả Napoleon Hill', '2025-09-28', 'think_grow_rich.jpg', 40, 'CD006', 'NXB05', 1),
('S0005', N'Thủ Thuật Thần Tốc Windows Vista', 55000, N'Các thủ thuật hay về Windows Vista', '2025-09-15', 'vista.jpg', 15, 'CD001', 'NXB02', 0),
('S0006', N'Tự học InDesign CS3', 70000, N'Sách hướng dẫn tự học InDesign CS3', '2025-08-20', 'indesign_cs3.jpg', 10, 'CD001', 'NXB03', 0),
('S0007', N'Công Nghệ Mạng Máy Tính', 110000, N'Giáo trình về mạng máy tính', '2025-10-02', 'mang_maytinh.jpg', 25, 'CD001', 'NXB02', 0),
('S0008', N'Hướng Dẫn Học Từng Bước Word 2010', 45000, N'Sách cho người tự học Word 2010', '2025-07-30', 'word_2010.jpg', 60, 'CD001', 'NXB02', 0);
GO

INSERT INTO THAMGIA(MASACH, MATACGIA, VAITRO, VITRI) VALUES
('S0001', 'TG001', N'Chủ biên', N'Bìa sách'),
('S0001', 'TG002', N'Tác giả', N'Bìa sách'),
('S0002', 'TG005', N'Tác giả', N'Trang 1'),
('S0003', 'TG003', N'Tác giả', N'Bìa sách'),
('S0004', 'TG004', N'Tác giả', N'Bìa sách'),
('S0005', 'TG005', N'Tác giả', N'Trang 1'),
('S0007', 'TG005', N'Chủ biên', N'Bìa sách');
GO

INSERT INTO KHACHHANG(MAKH, TENKH, NGAYSINH, GIOITINH, DIENTHOAI, TAIKHOAN, MATKHAU, EMAIL, DIACHI) VALUES
('KH001', N'Dương Thành Phát', '2000-05-15', N'Nam', '0918160670', 'thayphat', '123456', 'phetum@gmail.com', N'123 Trần Hưng Đạo'),
('KH002', N'Trần Văn An', '1999-01-20', N'Nam', '0905111222', 'an_tran', 'an123', 'antv@gmail.com', N'25 Võ Văn Tần, Q.3, TP.HCM'),
('KH003', N'Lê Thị Bích', '2002-11-30', N'Nữ', '0988333444', 'bichle', 'bich456', 'bichlt@yahoo.com', N'40 Nguyễn Văn Cừ, Q.5, TP.HCM');
GO

INSERT INTO DONHANG(MADONHANG, NGAYGIAO, NGAYDAT, DATHANHTOAN, TINHTRANGIAOHANG, MAKH) VALUES
('DH001', '2025-10-28', '2025-10-25', 1, N'Đã giao', 'KH001'),
('DH002', NULL, '2025-10-30', 0, N'Đang xử lý', 'KH002'),
('DH003', NULL, '2025-10-31', 1, N'Đang giao hàng', 'KH001');
GO

INSERT INTO CHITIETDONHANG(MADONHANG, MASACH, SOLUONG, DONGIA) VALUES
('DH001', 'S0001', 1, 82000),
('DH001', 'S0004', 1, 150000),
('DH002', 'S0007', 2, 110000),
('DH003', 'S0002', 1, 65000);
GO
