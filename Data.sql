/* TẠO DATABASE */
CREATE DATABASE QuanLyQuanCafe;
GO

USE QuanLyQuanCafe;
GO

/* BẢNG TÀI KHOẢN */
CREATE TABLE TaiKhoan (
    TenTaiKhoan NVARCHAR(100) PRIMARY KEY,
    TenHienThi NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(100) NOT NULL,
    Email NVARCHAR(500),
    DienThoai INT,
    DiaChi NVARCHAR(500),
    MatKhau NVARCHAR(500) NOT NULL DEFAULT 0,
    LoaiTK INT NOT NULL DEFAULT 0   -- 0: Nhân viên | 1: Chủ
);

/* BÀN ĂN */
CREATE TABLE BanAn (
    id INT IDENTITY PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa ai đặt',
    TrangThai NVARCHAR(100) NOT NULL DEFAULT N'Bàn đang trống'
);

/* BẢNG KHUYẾN MÃI */
CREATE TABLE KhuyenMai
(
    MaKhuyenMai INT PRIMARY KEY,
    TenKhuyenMai NVARCHAR(255),
    GiaTri DECIMAL(10, 2),
    DieuKien NVARCHAR(255),
    NgayBatDau DATETIME,
    NgayKetThuc DATETIME,
    TrangThai NVARCHAR(255)
);
GO

/* DANH MỤC ĐỒ ĂN */
CREATE TABLE FoodCategory (
    id INT IDENTITY PRIMARY KEY,
    Ten NVARCHAR(100)
);

/* NGUYÊN LIỆU */
CREATE TABLE NguyenLieu (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    SoLuong FLOAT NOT NULL
);

/* BẢNG ĐỒ ĂN */
CREATE TABLE Food (
    id INT IDENTITY PRIMARY KEY,
    Ten NVARCHAR(100) NOT NULL,
    idCategory INT NOT NULL,
    Gia FLOAT NOT NULL,
    FOREIGN KEY (idCategory) REFERENCES FoodCategory (id)
);

/* THÀNH PHẦN ĐỒ ĂN */
CREATE TABLE TPFood (
    TPFoodId INT NOT NULL,
    idNguyenLieu INT NOT NULL,
    soLuong FLOAT NOT NULL,
    PRIMARY KEY (TPFoodId, idNguyenLieu),
    FOREIGN KEY (TPFoodId) REFERENCES Food (id),
    FOREIGN KEY (idNguyenLieu) REFERENCES NguyenLieu (Id)
);

/* HÓA ĐƠN */
CREATE TABLE Bill (
    id INT IDENTITY PRIMARY KEY,
    DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
    DateCheckOut DATE,
    idTable INT NOT NULL,
    TrangThai INT NOT NULL DEFAULT 0
);
ALTER TABLE Bill ADD GiamGia INT DEFAULT 0;
ALTER TABLE Bill ADD totalPrice FLOAT DEFAULT 0;

ALTER TABLE Bill
ADD FOREIGN KEY (idTable) REFERENCES BanAn (id);

/* THÔNG TIN HÓA ĐƠN */
CREATE TABLE BillInfo (
    id INT IDENTITY PRIMARY KEY,
    idBill INT NOT NULL,
    idFood INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
    FOREIGN KEY (idBill) REFERENCES Bill (id),
    FOREIGN KEY (idFood) REFERENCES Food (id)
);

/* QUẢN LÝ LỊCH LÀM VIỆC */
CREATE TABLE QuanLy (
    TenTaiKhoan NVARCHAR(100),
    Buoi NVARCHAR(100),
    Thu2 BIT,
    Thu3 BIT,
    Thu4 BIT,
    Thu5 BIT,
    Thu6 BIT,
    Thu7 BIT,
    ChuNhat BIT,
    PRIMARY KEY (TenTaiKhoan, Buoi)
);

/* THÊM DỮ LIỆU KHỞI TẠO */
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO BanAn(Ten) VALUES (N'Bàn ' + CAST(@i AS NVARCHAR(10)));
    SET @i += 1;
END;
GO


/* ============================
    THỦ TỤC – STORED PROCEDURES
============================ */

CREATE PROC usp_login
@username NVARCHAR(100), @password NVARCHAR(100)
AS
BEGIN
    SELECT * FROM TaiKhoan 
    WHERE TenTaiKhoan = @username AND MatKhau = @password;
END;
GO


CREATE PROC usp_getTableList
AS SELECT * FROM BanAn;
GO


CREATE PROC usp_updateAcc
@userName NVARCHAR(100), @displayName NVARCHAR(100), 
@passWord NVARCHAR(100), @newPass NVARCHAR(100),
@newEmail NVARCHAR(100), @newPhoneNumber NVARCHAR(20)
AS
BEGIN
    DECLARE @checkPass INT = 0;
    SELECT @checkPass = COUNT(*) 
    FROM TaiKhoan 
    WHERE TenTaiKhoan = @userName AND MatKhau = @passWord;

    IF(@checkPass = 1)
    BEGIN
        UPDATE TaiKhoan SET 
            TenHienThi = @displayName,
            MatKhau = CASE WHEN @newPass IS NOT NULL AND @newPass <> '' THEN @newPass ELSE MatKhau END,
            Email = ISNULL(@newEmail, Email),
            DienThoai = ISNULL(@newPhoneNumber, DienThoai)
        WHERE TenTaiKhoan = @userName;
    END
END;
GO

CREATE PROC usp_InsertBill
@idTable INT
AS
BEGIN
    INSERT INTO Bill (DateCheckIn, DateCheckOut, idTable, TrangThai, GiamGia)
    VALUES (GETDATE(), NULL, @idTable, 0, 0);
END;
GO

CREATE PROC usp_insertBillInfo
@idBill INT, @idFood INT, @soLuong INT
AS
BEGIN
    DECLARE @isExitsBillInfo INT;
    DECLARE @foodCount INT = 1;

    SELECT @isExitsBillInfo = id, @foodCount = SoLuong 
    FROM BillInfo 
    WHERE idBill = @idBill AND idFood = @idFood;

    IF(@isExitsBillInfo > 0)
    BEGIN
        DECLARE @newCount INT = @foodCount + @soLuong;

        IF(@newCount > 0)
            UPDATE BillInfo SET SoLuong = @newCount WHERE id = @isExitsBillInfo;
        ELSE
            DELETE FROM BillInfo WHERE id = @isExitsBillInfo;
    END
    ELSE
    BEGIN
        INSERT INTO BillInfo(idBill, idFood, SoLuong)
        VALUES (@idBill, @idFood, @soLuong);
    END
END;
GO

ALTER PROC [dbo].[USP_GetListBillByDate]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT t.Ten AS [Tên bàn], b.totalPrice AS [Tổng tiền], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], GiamGia AS [Giảm giá]
	FROM dbo.Bill AS b,BanAn AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.TrangThai = 1
	AND t.id = b.idTable
END
GO

/* TRIGGER */

CREATE TRIGGER utg_UpdateBillIn4
ON BillInfo FOR INSERT, UPDATE
AS
BEGIN
    DECLARE @idBill INT;
    SELECT @idBill = idBill FROM inserted;

    DECLARE @idTable INT;
    SELECT @idTable = idTable FROM Bill WHERE id = @idBill AND TrangThai = 0;

    DECLARE @count INT;
    SELECT @count = COUNT(*) FROM BillInfo WHERE idBill = @idBill;

    IF(@count > 0)
        UPDATE BanAn SET TrangThai = N'Bàn đang có người' WHERE id = @idTable;
    ELSE
        UPDATE BanAn SET TrangThai = N'Bàn đang trống' WHERE id = @idTable;
END;
GO

CREATE TRIGGER utg_updateBill
ON Bill FOR UPDATE
AS
BEGIN
    DECLARE @idBill INT;
    SELECT @idBill = id FROM inserted;

    DECLARE @idTable INT;
    SELECT @idTable = idTable FROM Bill WHERE id = @idBill;

    DECLARE @count INT = 0;
    SELECT @count = COUNT(*) FROM Bill WHERE idTable = @idTable AND TrangThai = 0;

    IF(@count = 0)
        UPDATE BanAn SET TrangThai = N'Bàn đang trống' WHERE id = @idTable;
END;
GO

/* FUNCTION */
CREATE FUNCTION fuConvertToUnsign1 (@strInput NVARCHAR(4000))
RETURNS NVARCHAR(4000)
AS
BEGIN
    IF @strInput IS NULL OR @strInput = '' RETURN @strInput;

    DECLARE @SIGN_CHARS NCHAR(136) =
    N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR(272) + NCHAR(208);

    DECLARE @UNSIGN_CHARS NCHAR(136) =
    N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD';

    DECLARE @i INT = 1, @j INT;

    WHILE(@i <= LEN(@strInput))
    BEGIN
        SET @j = 1;
        WHILE(@j <= LEN(@SIGN_CHARS))
        BEGIN
            IF UNICODE(SUBSTRING(@SIGN_CHARS,@j,1)) = UNICODE(SUBSTRING(@strInput,@i,1))
            BEGIN
                SET @strInput = STUFF(@strInput,@i,1,SUBSTRING(@UNSIGN_CHARS,@j,1));
                BREAK;
            END
            SET @j += 1;
        END
        SET @i += 1;
    END

    RETURN REPLACE(@strInput,' ','-');
END;
GO

/* TẠO PROC THÊM LỊCH LÀM VIỆC */
CREATE PROCEDURE AddNewAccount
@TenTaiKhoan NVARCHAR(100)
AS
BEGIN
    INSERT INTO QuanLy VALUES (@TenTaiKhoan, 'Sang', 0,0,0,0,0,0,0);
    INSERT INTO QuanLy VALUES (@TenTaiKhoan, 'Chieu', 0,0,0,0,0,0,0);
    INSERT INTO QuanLy VALUES (@TenTaiKhoan, 'Toi', 0,0,0,0,0,0,0);
END;
GO

-- BÀN ĂN (20 bàn)
DECLARE @i INT = 1;
WHILE @i <= 20
BEGIN
    INSERT INTO BanAn(Ten) VALUES (N'Bàn ' + CAST(@i AS NVARCHAR(10)));
    SET @i += 1;
END;

-- THÊM DANH MỤC FOOD
INSERT INTO FoodCategory (Ten)
VALUES (N'Cà phê'), (N'Trà sữa'), (N'Nước ép'), (N'Sinh tố'), (N'Ăn vặt');

-- THÊM NGUYÊN LIỆU
INSERT INTO NguyenLieu (Name, SoLuong)
VALUES 
(N'Cà phê hạt', 500),
(N'Sữa đặc', 200),
(N'Đường', 300),
(N'Trà đen', 400),
(N'Siro đường', 300);

-- THÊM ĐỒ ĂN / ĐỒ UỐNG
INSERT INTO Food (Ten, idCategory, Gia)
VALUES
(N'Cà phê đen', 1, 20000),
(N'Cà phê sữa', 1, 25000),
(N'Trà sữa trân châu', 2, 30000),
(N'Trà đào', 2, 28000),
(N'Nước cam', 3, 25000),
(N'Sinh tố xoài', 4, 35000),
(N'Khoai tây chiên', 5, 30000);

-- THÊM THÀNH PHẦN ĐỒ ĂN (TPFood)
INSERT INTO TPFood (TPFoodId, idNguyenLieu, soLuong)
VALUES
(1, 1, 10),   -- Cà phê đen: cà phê hạt
(1, 3, 5),    -- Cà phê đen: đường
(2, 1, 10),   -- Cà phê sữa
(2, 2, 5),    -- sữa đặc
(3, 4, 15),   -- trà sữa trân châu: trà đen
(3, 3, 10),
(4, 4, 10),   -- trà đào
(5, 5, 12);   -- nước cam dùng siro đường

-- THÊM HÓA ĐƠN
INSERT INTO Bill (DateCheckIn, DateCheckOut, idTable, TrangThai, GiamGia, totalPrice)
VALUES 
(GETDATE(), NULL, 1, 0, 0, 0),
(GETDATE(), NULL, 2, 0, 0, 0),
(GETDATE(), NULL, 3, 0, 0, 0),
(GETDATE(), GETDATE(), 2, 1, 0, 100000);

-- THÊM CHI TIẾT HÓA ĐƠN (BillInfo)
INSERT INTO BillInfo (idBill, idFood, SoLuong)
VALUES
(1, 1, 3),
(2, 3, 2),
(3, 5, 1),
(4, 2, 4);

-- THÊM KHUYẾN MÃI
INSERT INTO KhuyenMai (MaKhuyenMai, TenKhuyenMai, GiaTri, DieuKien, NgayBatDau, NgayKetThuc, TrangThai)
VALUES 
(1, N'Khuyến mãi mùa hè', 10.00, N'Trên 500k', '2024-06-01', '2024-06-30', N'Còn hiệu lực'),
(2, N'Khuyến mãi ngày lễ', 15.00, N'Tất cả', '2024-09-01', '2024-09-10', N'Còn hiệu lực'),
(3, N'Khuyến mãi cuối năm', 20.00, N'Trên 1000k', '2024-12-01', '2024-12-31', N'Còn hiệu lực'),
(4, N'Tặng quà sinh nhật', 0.00, N'Tất cả', '2024-01-01', '2024-12-31', N'Còn hiệu lực'),
(5, N'Khuyến mãi Black Friday', 50.00, N'Trên 100k', '2024-11-25', '2024-11-29', N'Hết hiệu lực');

-- THÊM LỊCH LÀM VIỆC — QUANLY
-- Tài khoản 1
INSERT INTO QuanLy VALUES (N'Phạm Đức phương', 'Sang', 1,0,1,0,1,0,1);
INSERT INTO QuanLy VALUES (N'Phạm Đức phương', 'Chieu', 0,1,0,1,0,1,0);
INSERT INTO QuanLy VALUES (N'Phạm Đức phương', 'Toi', 1,1,1,1,1,1,1);

-- Tài khoản 2
INSERT INTO QuanLy VALUES ('lê việt Tiến', 'Sang', 1,0,1,0,1,0,1);
INSERT INTO QuanLy VALUES ('lê việt Tiến', 'Chieu', 0,1,0,1,0,1,0);
INSERT INTO QuanLy VALUES ('lê việt Tiến', 'Toi', 1,1,1,1,1,1,1);

-- Tài khoản 3
INSERT INTO QuanLy VALUES ('TaiKhoan3', 'Sang', 1,0,1,0,1,0,1);
INSERT INTO QuanLy VALUES ('TaiKhoan3', 'Chieu', 0,1,0,1,0,1,0);
INSERT INTO QuanLy VALUES ('TaiKhoan3', 'Toi', 1,1,1,1,1,1,1);

-- THÊM DỮ LIỆU BẢNG TAIKHOAN
INSERT INTO TaiKhoan
    (TenTaiKhoan, TenHienThi, VaiTro, Email, DienThoai, DiaChi, MatKhau, LoaiTK)
VALUES
    (N'phuongpd', N'Phạm Đức Phương', N'Chủ cửa hàng', N'phuong@example.com', 123456789, N'Hà Nội', N'123', 1),
    (N'lê việt Tiến',    N'Lê Việt Tiến',    N'Nhân viên',     N'tien@example.com',   987654321, N'Hà Nội', N'123', 0),
    (N'TaiKhoan3',       N'Nhân viên ca 3',  N'Nhân viên',     N'tk3@example.com',    555666777, N'Hà Nội', N'123', 0);

