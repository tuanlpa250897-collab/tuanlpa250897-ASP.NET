# Cơ sở dữ liệu SQLite - Website Quản lý Chi tiêu Cá nhân

## Tổng quan
Website sử dụng SQLite làm cơ sở dữ liệu với Entity Framework Core để quản lý dữ liệu chi tiêu cá nhân.

## Cấu trúc Database

### 1. Bảng Users
- **Id**: Khóa chính (TEXT)
- **Name**: Tên người dùng (TEXT, NOT NULL)
- **Email**: Email đăng nhập (TEXT, UNIQUE, NOT NULL)
- **Password**: Mật khẩu (TEXT, NOT NULL)
- **Role**: Vai trò người dùng (TEXT, DEFAULT 'user')
- **Avatar**: Đường dẫn ảnh đại diện (TEXT)
- **CreatedAt**: Ngày tạo tài khoản (DATETIME)

### 2. Bảng Categories
- **Id**: Khóa chính (TEXT)
- **Name**: Tên danh mục (TEXT, NOT NULL)
- **Icon**: Biểu tượng emoji (TEXT)
- **Color**: Mã màu hex (TEXT)
- **Type**: Loại danh mục ('income' hoặc 'expense')

### 3. Bảng Transactions
- **Id**: Khóa chính (TEXT)
- **UserId**: ID người dùng (TEXT, NOT NULL)
- **CategoryId**: ID danh mục (TEXT, NOT NULL, FK)
- **Amount**: Số tiền (DECIMAL(18,2), NOT NULL)
- **Description**: Mô tả giao dịch (TEXT, NOT NULL)
- **Date**: Ngày giao dịch (DATETIME, NOT NULL)
- **Type**: Loại giao dịch ('income' hoặc 'expense')

### 4. Bảng Budgets
- **Id**: Khóa chính (TEXT)
- **UserId**: ID người dùng (TEXT, NOT NULL)
- **CategoryId**: ID danh mục (TEXT, NOT NULL, FK)
- **Amount**: Số tiền ngân sách (DECIMAL(18,2), NOT NULL)
- **Period**: Chu kỳ ('monthly' hoặc 'yearly')
- **Spent**: Số tiền đã chi (DECIMAL(18,2), DEFAULT 0)

## Dữ liệu mẫu

### Categories (Danh mục)
1. Ăn uống 🍽️ (Chi tiêu)
2. Di chuyển 🚗 (Chi tiêu)
3. Giải trí 🎮 (Chi tiêu)
4. Mua sắm 🛒 (Chi tiêu)
5. Sức khỏe 💊 (Chi tiêu)
6. Học tập 📚 (Chi tiêu)
7. Lương 💰 (Thu nhập)
8. Đầu tư 📈 (Thu nhập)
9. Khác 📝 (Chi tiêu)

### User mặc định
- **Email**: user@example.com
- **Password**: 123456
- **Name**: Người dùng

### Transactions mẫu
- Lương tháng 12: +15,000,000 VND
- Ăn trưa: -150,000 VND
- Xe bus: -50,000 VND
- Xem phim: -200,000 VND
- Mua quần áo: -500,000 VND

## Cách sử dụng

### 1. Khởi tạo Database
Database sẽ được tự động tạo khi chạy ứng dụng lần đầu tiên thông qua `DbInitializer.Initialize()`.

### 2. Kết nối Database
Connection string được cấu hình trong `Program.cs`:
```csharp
builder.Services.AddDbContext<ExpenseDbContext>(options =>
    options.UseSqlite("Data Source=expense.db"));
```

### 3. Truy cập Database
Sử dụng `DataService` để thao tác với database:
```csharp
public class DataService
{
    private readonly ExpenseDbContext _context;
    
    public DataService(ExpenseDbContext context)
    {
        _context = context;
    }
    
    // Các phương thức CRUD
}
```

## File Database
- **Tên file**: `expense.db`
- **Vị trí**: Thư mục gốc của project
- **Loại**: SQLite Database

## Lưu ý
- Database sẽ được tạo tự động khi chạy ứng dụng
- Dữ liệu mẫu chỉ được thêm nếu database trống
- File `expense.db` có thể được sao chép để backup dữ liệu
- Sử dụng Entity Framework Core để thao tác với database