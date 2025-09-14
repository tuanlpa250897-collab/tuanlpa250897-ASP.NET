-- Create SQLite Database for Expense Tracker

-- Users table
CREATE TABLE IF NOT EXISTS Users (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    Password TEXT NOT NULL,
    Role TEXT NOT NULL DEFAULT 'user',
    Avatar TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Categories table
CREATE TABLE IF NOT EXISTS Categories (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    Icon TEXT,
    Color TEXT,
    Type TEXT NOT NULL CHECK (Type IN ('income', 'expense'))
);

-- Transactions table
CREATE TABLE IF NOT EXISTS Transactions (
    Id TEXT PRIMARY KEY,
    UserId TEXT NOT NULL,
    CategoryId TEXT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Description TEXT NOT NULL,
    Date DATETIME NOT NULL,
    Type TEXT NOT NULL CHECK (Type IN ('income', 'expense')),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Budgets table
CREATE TABLE IF NOT EXISTS Budgets (
    Id TEXT PRIMARY KEY,
    UserId TEXT NOT NULL,
    CategoryId TEXT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Period TEXT NOT NULL CHECK (Period IN ('monthly', 'yearly')),
    Spent DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Insert sample categories
INSERT OR IGNORE INTO Categories (Id, Name, Icon, Color, Type) VALUES
('1', 'Ăn uống', '🍽️', '#EF4444', 'expense'),
('2', 'Di chuyển', '🚗', '#3B82F6', 'expense'),
('3', 'Giải trí', '🎮', '#8B5CF6', 'expense'),
('4', 'Mua sắm', '🛒', '#F59E0B', 'expense'),
('5', 'Sức khỏe', '💊', '#10B981', 'expense'),
('6', 'Học tập', '📚', '#6366F1', 'expense'),
('7', 'Lương', '💰', '#22C55E', 'income'),
('8', 'Đầu tư', '📈', '#059669', 'income'),
('9', 'Khác', '📝', '#6B7280', 'expense');

-- Insert sample user
INSERT OR IGNORE INTO Users (Id, Name, Email, Password, Role, CreatedAt) VALUES
('1', 'Người dùng', 'user@example.com', '123456', 'user', datetime('now'));

-- Insert sample transactions
INSERT OR IGNORE INTO Transactions (Id, UserId, CategoryId, Amount, Description, Date, Type) VALUES
('1', '1', '7', 15000000, 'Lương tháng 12', '2024-12-01', 'income'),
('2', '1', '1', 150000, 'Ăn trưa', '2024-12-15', 'expense'),
('3', '1', '2', 50000, 'Xe bus', '2024-12-15', 'expense'),
('4', '1', '3', 200000, 'Xem phim', '2024-12-14', 'expense'),
('5', '1', '4', 500000, 'Mua quần áo', '2024-12-13', 'expense');

-- Insert sample budgets
INSERT OR IGNORE INTO Budgets (Id, UserId, CategoryId, Amount, Period, Spent) VALUES
('1', '1', '1', 3000000, 'monthly', 1200000),
('2', '1', '2', 1000000, 'monthly', 800000),
('3', '1', '3', 2000000, 'monthly', 1500000);