<div align="center">

# Quản Lý Quán Café  
### C# WinForms • SQL Server • Modern UI  

<img width="1330" height="839" alt="image" src="https://github.com/user-attachments/assets/2c72ed55-031b-4f42-9af5-caec97e0d91a" />

---

![Status](https://img.shields.io/badge/Status-Active-brightgreen?style=for-the-badge)
![Language](https://img.shields.io/badge/C%23-WinForms-512BD4?style=for-the-badge)
![Database](https://img.shields.io/badge/SQL_Server-Database-red?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-blue?style=for-the-badge)

Hệ thống quản lý quán café với giao diện hiện đại, biểu đồ trực quan và đầy đủ chức năng hỗ trợ bán hàng.
</div>

---

## Tính năng nổi bật

### Trang chủ (Dashboard)
- Món bán chạy nhất  
- Doanh thu tháng  
- Tổng số nhân viên / quản lý  
- Biểu đồ tròn doanh thu theo sản phẩm  
- Bảng chi tiết doanh thu từng món  

### Quản lý hóa đơn
- Tạo – sửa – xóa hóa đơn  
- Tính tổng tiền / giảm giá tự động  
- Chuyển bàn, gộp bàn  
- Cập nhật trạng thái bàn real-time  

### Quản lý món & nguyên liệu
- Danh mục món  
- Quản lý nguyên liệu & định lượng  
- Kiểm tra còn đủ nguyên liệu để bán  

### Khuyến mãi
- Thời gian hiệu lực  
- Điều kiện áp dụng  
- Tự động kiểm tra khuyến mãi hợp lệ  

### Thống kê
- Doanh thu theo ngày / tháng / thời gian  
- Xuất biểu đồ  

### Tài khoản & phân quyền
- Admin / Nhân viên / Quản lý  
- Đổi thông tin tài khoản  
- Bảo mật đăng nhập  

### Quản lý lịch làm việc
- 3 ca: Sáng – Chiều – Tối  
- Theo dõi thứ 2 → Chủ nhật  

## Công nghệ sử dụng

| Công nghệ | Mô tả |
|----------|-------|
| **C# WinForms** | Xây dựng giao diện |
| **SQL Server** | Hệ quản trị dữ liệu |
| **ADO.NET** | Kết nối database |
| **LiveCharts / ChartControl** | Biểu đồ thống kê |
| **Git + GitHub** | Quản lý mã nguồn |

## Cài đặt

### Import database
Mở SQL Server → chạy file: Data.sql

### Cấu hình chuỗi kết nối
Trong `DataProvider.cs`:
```csharp
private string ketNoiStr = "Data Source=.;Initial Catalog=QuanLyQuanCafe;Integrated Security=True;Encrypt=False";
```

### Chạy chương trình
Nhấn F5 trong Visual Studio

## Hỗ trợ

[![GitHub issues](https://img.shields.io/github/issues/ongtrumphuong/QuanLyQuanCafe?style=for-the-badge)](https://github.com/ongtrumphuong/QuanLyQuanCafe/issues)
[![GitHub stars](https://img.shields.io/github/stars/ongtrumphuong/QuanLyQuanCafe?style=for-the-badge)](https://github.com/ongtrumphuong/QuanLyQuanCafe/stargazers)

- Báo lỗi / Góp ý: tạo issue tại GitHub  
- Email liên hệ: 2224801030012@student.tdmu.edu.vn
- Rất cảm ơn mọi người đã góp ý.

<div align="center">
  Được tạo bởi sinh viên năm 2 thuộc Trường Đại học Thủ Dầu Một
</div> 




