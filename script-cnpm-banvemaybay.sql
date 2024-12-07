USE master
go

create database CNPM_QuanLyBanVeMayBay
go

go
USE CNPM_QuanLyBanVeMayBay
go

--CREATE TABLE
-- Bảng 'Tài khoản'
CREATE TABLE TaiKhoan (
    MaTaiKhoan INT IDENTITY(1,1) PRIMARY KEY,
    TenTaiKhoan NVARCHAR(50) UNIQUE, -- Đảm bảo tên tài khoản là duy nhất
    MatKhau NVARCHAR(100)
);
GO

-- Bảng 'Khách hàng'
CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100),
    DiaChi NVARCHAR(255),
    Email NVARCHAR(100),
    NgaySinh DATE CHECK (NgaySinh <= GETDATE()), -- Kiểm tra ngày sinh nhỏ hơn hoặc bằng ngày hiện tại
    SoDienThoai NVARCHAR(20) UNIQUE, -- Đảm bảo số điện thoại là duy nhất
    MaTaiKhoan INT,
    CONSTRAINT FK_KHACHHANG_TAIKHOAN FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoan(MaTaiKhoan)
);
GO

-- Bảng 'Hành khách'
CREATE TABLE HanhKhach (
    MaHanhKhach INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100),
    DiaChi NVARCHAR(255),
    GioiTinh NVARCHAR(10),
    QuocTich NVARCHAR(50),
    NgaySinh DATE CHECK (NgaySinh <= GETDATE()),
    SoDienThoai NVARCHAR(20) UNIQUE,
    Email NVARCHAR(100),
    CCCD_Passport NVARCHAR(20) UNIQUE,
    MaKhachHang INT,
    CONSTRAINT FK_HANHKHACH_KHACHHANG FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);
GO

-- Bảng 'Sân bay'
CREATE TABLE SanBay (
    MaSanBay INT IDENTITY(1,1) PRIMARY KEY,
    TenSanBay NVARCHAR(100) UNIQUE,
    TenThanhPho NVARCHAR(100),
    VietTatSanBay NVARCHAR(10) UNIQUE
);
GO

-- Bảng 'Lộ trình'
CREATE TABLE LoTrinh (
    MaLoTrinh INT IDENTITY(1,1) PRIMARY KEY,
    MaSB_Di INT,
    MaSB_Den INT,
    CONSTRAINT FK_MASBDI_SB FOREIGN KEY (MaSB_Di) REFERENCES SanBay(MaSanBay),
    CONSTRAINT FK_MASBDEN_SB FOREIGN KEY (MaSB_Den) REFERENCES SanBay(MaSanBay)
);
GO

-- Bảng 'Hãng hàng không'
CREATE TABLE HangHangKhong (
    MaHangHangKhong INT IDENTITY(1,1) PRIMARY KEY,
    TenHangHangKhong NVARCHAR(100) NOT NULL
);
GO

-- Bảng 'Trạng thái chuyến bay'
CREATE TABLE TrangThaiChuyenBay (
    MaTrangThaiChuyenBay INT IDENTITY(1,1) PRIMARY KEY,
    TenTrangThaiChuyenBay NVARCHAR(40)
);
GO

-- Bảng 'Máy bay'
CREATE TABLE MayBay (
    MaMayBay INT IDENTITY(1,1) PRIMARY KEY,
    TenMayBay NVARCHAR(100) NOT NULL,
    SucChuaToiDa INT CHECK (SucChuaToiDa > 0)
);
GO

-- Bảng 'Chuyến bay'
CREATE TABLE ChuyenBay (
    MaChuyenBay INT IDENTITY(1,1) PRIMARY KEY,
    MaHangHangKhong INT,
    MaTrangThaiChuyenBay INT,
    MaLoTrinh INT,
    MaMayBay INT,
    GiaBay MONEY,
    CONSTRAINT FK_CHUYENBAY_SANBAY FOREIGN KEY (MaLoTrinh) REFERENCES LoTrinh(MaLoTrinh),
    CONSTRAINT FK_CHUYENBAY_HHK FOREIGN KEY (MaHangHangKhong) REFERENCES HangHangKhong(MaHangHangKhong),
    CONSTRAINT FK_CHUYENBAY_TRANGTHAI FOREIGN KEY (MaTrangThaiChuyenBay) REFERENCES TrangThaiChuyenBay(MaTrangThaiChuyenBay),
    CONSTRAINT FK_CHUYENBAY_MAYBAY FOREIGN KEY (MaMayBay) REFERENCES MayBay(MaMayBay)
);
GO
ALTER TABLE ChuyenBay
Add SLGhePhoThong INT
GO

ALTER TABLE ChuyenBay
Add SLGheThuongGia INT
GO


-- Bảng 'Hạng ghế'
CREATE TABLE HangGhe (
    MaHangGhe INT IDENTITY(1,1) PRIMARY KEY,
    TenHangGhe NVARCHAR(50)
);
GO

-- Bảng 'Giá hàng ghế'
CREATE TABLE GiaHangGhe (
    MaHangGhe INT,
    MaHHK INT,
    Gia DECIMAL(18, 2) CHECK (Gia > 0),
    CONSTRAINT PK_GiaHangGhe PRIMARY KEY (MaHangGhe, MaHHK),
    CONSTRAINT FK_HANGGHE_GIA FOREIGN KEY (MaHangGhe) REFERENCES HangGhe(MaHangGhe),
    CONSTRAINT FK_HANGGHE_HHK FOREIGN KEY (MaHHK) REFERENCES HangHangKhong(MaHangHangKhong)
);
GO

-- Bảng 'Trạng thái vé'
CREATE TABLE TrangThaiVe (
    MaTTV INT IDENTITY(1,1) PRIMARY KEY,
    TenTTV NVARCHAR(20)
);
GO

-- Bảng 'Vé'
CREATE TABLE Ve (
    MaVe INT IDENTITY(1,1) PRIMARY KEY,
    MaHanhKhach INT NOT NULL,
    MaTTV INT NOT NULL,
    MaPhieuDat INT NULL,
    CONSTRAINT FK_VE_TRANGTHAIVE FOREIGN KEY (MaTTV) REFERENCES TrangThaiVe(MaTTV),
    CONSTRAINT FK_VE_HANHKHACH FOREIGN KEY (MaHanhKhach) REFERENCES HanhKhach(MaHanhKhach),
    CONSTRAINT FK_VE_PHIEUDAT FOREIGN KEY (MaPhieuDat) REFERENCES PhieuDat(MaPhieuDat)
);
GO

-- Bảng 'Phiếu đặt'
CREATE TABLE PhieuDat (
    MaPhieuDat INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT NOT NULL,
    NgayDat DATE,
    SoLuongVeDat INT CHECK (SoLuongVeDat > 0),
    CONSTRAINT FK_PHIEUDAT_KHACHHANG FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);
GO

-- Bảng 'Chi tiết phiếu đặt'
CREATE TABLE ChiTietPhieuDat (
    MaChiTietPhieuDat INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieuDat INT NOT NULL,
    MaVe INT NOT NULL,
    MaHanhKhach INT NOT NULL,
    MaHangGhe INT NOT NULL,
    CONSTRAINT FK_CTPD_PHIEUDAT FOREIGN KEY (MaPhieuDat) REFERENCES PhieuDat(MaPhieuDat),
    CONSTRAINT FK_CTPD_VE FOREIGN KEY (MaVe) REFERENCES Ve(MaVe),
    CONSTRAINT FK_CTPD_HANHKHACH FOREIGN KEY (MaHanhKhach) REFERENCES HanhKhach(MaHanhKhach),
    CONSTRAINT FK_CTPD_HANGGHE FOREIGN KEY (MaHangGhe) REFERENCES HangGhe(MaHangGhe)
);
GO

-- Bảng 'Hoá đơn'
CREATE TABLE HoaDon (
    MaHoaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieuDat INT,
    TongTien DECIMAL(18, 2) CHECK (TongTien >= 0),
    CONSTRAINT FK_HOADON_PHIEUDAT FOREIGN KEY (MaPhieuDat) REFERENCES PhieuDat(MaPhieuDat)
);
GO

-- Dữ liệu bảng 'Tài khoản'
INSERT INTO TaiKhoan (TenTaiKhoan, MatKhau)
VALUES 
(N'user1', N'password123'), 
(N'user2', N'password456'),
(N'user3', N'password789');
GO

-- Dữ liệu bảng 'Khách hàng'
INSERT INTO KhachHang (HoTen, DiaChi, Email, NgaySinh, SoDienThoai, MaTaiKhoan)
VALUES 
(N'Nguyễn Văn A', N'Hà Nội', N'van.a@gmail.com', '1990-05-12', N'0123456789', 1),
(N'Lê Thị B', N'Hồ Chí Minh', N'le.b@gmail.com', '1985-03-25', N'0987654321', 2),
(N'Phạm Văn C', N'Đà Nẵng', N'pham.c@gmail.com', '1995-08-15', N'0912345678', 3);
GO

-- Dữ liệu bảng 'Hành khách'
INSERT INTO HanhKhach (HoTen, DiaChi, GioiTinh, QuocTich, NgaySinh, SoDienThoai, Email, CCCD_Passport, MaKhachHang)
VALUES
(N'Nguyễn Văn A', N'Hà Nội', N'Nam', N'Việt Nam', '1990-05-12', N'0123456789', N'van.a@gmail.com', N'123456789', 1),
(N'Lê Thị B', N'Hồ Chí Minh', N'Nữ', N'Việt Nam', '1985-03-25', N'0987654321', N'le.b@gmail.com', N'987654321', 2),
(N'Phạm Văn C', N'Đà Nẵng', N'Nam', N'Việt Nam', '1995-08-15', N'0912345678', N'pham.c@gmail.com', N'112233445', 3);
GO

-- Dữ liệu bảng 'Sân bay'
INSERT INTO SanBay (TenSanBay, TenThanhPho, VietTatSanBay)
VALUES 
(N'Sân bay Nội Bài', N'Hà Nội', N'HAN'),
(N'Sân bay Tân Sơn Nhất', N'Hồ Chí Minh', N'SGN'),
(N'Sân bay Đà Nẵng', N'Đà Nẵng', N'DAD');
GO

-- Dữ liệu bảng 'Lộ trình'
INSERT INTO LoTrinh (MaSB_Di, MaSB_Den)
VALUES 
(1, 2), -- Hà Nội -> Hồ Chí Minh
(1, 3), -- Hà Nội -> Đà Nẵng
(2, 3); -- Hồ Chí Minh -> Đà Nẵng
GO

-- Dữ liệu bảng 'Hãng hàng không'
INSERT INTO HangHangKhong (TenHangHangKhong)
VALUES 
(N'Vietnam Airlines'),
(N'Bamboo Airways'),
(N'VietJet Air');
GO

-- Dữ liệu bảng 'Trạng thái chuyến bay'
INSERT INTO TrangThaiChuyenBay (TenTrangThaiChuyenBay)
VALUES 
(N'Có sẵn'), 
(N'Hoàn thành');
GO

-- Dữ liệu bảng 'Máy bay'
INSERT INTO MayBay (TenMayBay, SucChuaToiDa)
VALUES 
(N'Boeing 737', 200), 
(N'Airbus A320', 180),
(N'Boeing 787', 300);
GO

-- Dữ liệu bảng 'Chuyến bay'
INSERT INTO ChuyenBay (MaHangHangKhong, MaTrangThaiChuyenBay, MaLoTrinh, MaMayBay, GiaBay)
VALUES
(1, 1, 1, 1, 1500000), -- Vietnam Airlines, Hà Nội -> Hồ Chí Minh
(2, 1, 2, 2, 1200000), -- Bamboo Airways, Hà Nội -> Đà Nẵng
(3, 1, 3, 3, 1000000); -- VietJet Air, Hồ Chí Minh -> Đà Nẵng
GO
Update ChuyenBay
Set SLGhePhoThong = 280, SLGheThuongGia = 20
Where MaMayBay = 3

select * from ChuyenBay
select * from MayBay

-- Dữ liệu bảng 'Hạng ghế'
INSERT INTO HangGhe (TenHangGhe)
VALUES 
(N'Hạng phổ thông'), 
(N'Hạng thương gia');
GO

-- Dữ liệu bảng 'Giá hàng ghế'
INSERT INTO GiaHangGhe (MaHangGhe, MaHHK, Gia)
VALUES
(1, 1, 1500000), -- Hạng phổ thông, Vietnam Airlines
(2, 2, 2500000), -- Hạng thương gia, Bamboo Airways
(1, 1, 3500000); 
GO

-- Dữ liệu bảng 'Trạng thái vé'
INSERT INTO TrangThaiVe (TenTTV)
VALUES 
(N'Chưa thanh toán'), 
(N'Đã đặt'), 
(N'Đã sử dụng'), 
(N'Hủy vé');
GO

-- Dữ liệu bảng 'Phiếu đặt'
INSERT INTO PhieuDat (MaKhachHang, NgayDat, SoLuongVeDat)
VALUES 
(1, '2024-12-01', 2), 
(2, '2024-12-02', 1);
GO

-- Dữ liệu bảng 'Vé'
INSERT INTO Ve (MaHanhKhach, MaTTV, MaPhieuDat)
VALUES
(1, 1, 1), -- 
(2, 1, 1), -- 
(3, 1, 2); --
GO

-- Dữ liệu bảng 'Chi tiết phiếu đặt'
INSERT INTO ChiTietPhieuDat (MaPhieuDat, MaVe, MaHanhKhach, MaHangGhe)
VALUES
(1, 1, 1, 1), 
(1, 2, 2, 2),
(2, 3, 3, 1);
GO

-- Dữ liệu bảng 'Hoá đơn'
INSERT INTO HoaDon (MaPhieuDat, TongTien)
VALUES 
(1, 3000000), 
(2, 1500000);
GO
