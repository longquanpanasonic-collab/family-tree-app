# 👨‍👩‍👧‍👦 Hệ Thống Quản Lý Gia Phả Trực Tuyến

Một ứng dụng web hoàn chỉnh để quản lý và theo dõi gia phả gia đình, xây dựng bằng **ASP.NET MVC 5**, **MySQL**, và **Bootstrap 5**.

## ✨ Tính Năng Chính

### 1. 🔐 Xác Thực & Phân Quyền
- Đăng ký tài khoản mới
- Đăng nhập an toàn
- Quên mật khẩu
- Phân quyền người dùng (Admin, User)
- Quản lý session

### 2. 👥 Quản Lý Thành Viên
- Thêm/Sửa/Xóa thành viên gia đình
- Quản lý quan hệ gia đình (Cha, Mẹ, Vợ/Chồng, Con cái)
- Hiển thị thông tin chi tiết
- Tìm kiếm nâng cao
- Lọc theo giới tính, năm sinh

### 3. 🌳 Sơ Đồ Gia Phả
- Vẽ sơ đồ gia phả interactive
- Hiển thị quan hệ gia đình trực quan
- Zoom, pan, tương tác

### 4. 📰 Quản Lý Tin Tức
- Tạo/Chỉnh sửa/Xóa tin tức
- Tin nổi bật (Featured)
- Phân loại tin
- Hiển thị tin trên trang chủ

### 5. 🖼️ Quản Lý Hình Ảnh
- Upload hình ảnh
- Quản lý gallery
- Liên kết ảnh với thành viên
- Xem ảnh lightbox

### 6. 🔍 Tìm Kiếm & Lọc
- Tìm kiếm theo tên
- Lọc theo giới tính
- Lọc theo năm sinh
- Tìm kiếm tin tức

### 7. 📱 Responsive Design
- Giao diện đáp ứng tất cả thiết bị
- Mobile-friendly
- Bootstrap 5
- CSS tối ưu

## 🛠️ Công Nghệ Sử Dụng

- **Backend**: ASP.NET MVC 5
- **Database**: MySQL
- **Frontend**: HTML5, CSS3, Bootstrap 5, JavaScript
- **ORM**: Entity Framework 6
- **Chart**: Chart.js (cho sơ đồ gia phả)

## 📁 Cấu Trúc Dự Án

```
FamilyTreeApp/
├── Models/                      # Entity Models
├── Controllers/                 # Business Logic
├── Views/                       # Razor Views
├── Content/                     # CSS & Bootstrap
├── Scripts/                     # JavaScript
├── Data/                        # Database Context
├── Migrations/                  # EF Migrations
├── App_Data/                    # Local Database
└── Web.config                   # Configuration
```

## 🚀 Cài Đặt

### Yêu Cầu
- Visual Studio 2019+
- .NET Framework 4.7.2+
- MySQL Server 5.7+

### Bước 1: Clone Repository
```bash
git clone https://github.com/longquanpanasonic-collab/family-tree-app.git
```

### Bước 2: Cấu Hình Database
Chỉnh sửa `Web.config` với thông tin MySQL của bạn

### Bước 3: Cài Đặt Dependencies
Mở Package Manager Console và chạy:
```bash
Install-Package EntityFramework
Install-Package MySql.Data.EntityFramework
Update-Database
```

### Bước 4: Chạy Ứng Dụng
Nhấn `Ctrl + F5` trong Visual Studio

## 📧 Liên Hệ
- GitHub: https://github.com/longquanpanasonic-collab

---
**Happy coding! 🚀**