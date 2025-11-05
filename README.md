# 🏞️ سامانه هوشمند گردشگری (Smart Tourism System)

یک سامانه هوشمند گردشگری مبتنی بر هوش مصنوعی که با استفاده از Gemini AI به کاربران در یافتن مقاصد گردشگری مناسب کمک می‌کند.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-purple)
![C#](https://img.shields.io/badge/C%23-12.0-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red)
![AI](https://img.shields.io/badge/AI-Gemini%20API-green)


## 📷 تصاویر

<img width="1435" height="983" alt="image" src="https://github.com/user-attachments/assets/511a35e0-4be1-4010-99dc-c167a446a99a" />



## ✨ ویژگی‌های اصلی

- 🤖 **دستیار هوشمند گردشگری** - مشاوره آنلاین با Gemini AI
- 🎯 **پیشنهادات هوشمند** - پیشنهاد مکان‌ها بر اساس علاقمندی کاربر
- 🔍 **جستجوی پیشرفته** - فیلتر بر اساس شهر، دسته‌بندی و امتیاز
- 📊 **سیستم نظردهی** - امتیازدهی و ثبت نظر برای مکان‌ها
- 💾 **ذخیره‌سازی هوشمند** - ثبت تاریخچه چت‌ها و پیشنهادات

## 🛠️ تکنولوژی‌های استفاده شده

### Backend
- **ASP.NET Core 8.0** - Razor Pages
- **Entity Framework Core** - ORM
- **SQL Server** - پایگاه داده
- **LINQ & Lambda** - queryهای دیتابیس

### Frontend
- **Bootstrap** - UI Framework
- **JavaScript** - تعاملات سمت کلاینت
- **Razor Syntax** - templating

### AI & APIs
- **Google Gemini AI** - هوش مصنوعی برای چت
- **RESTful APIs** - ارتباط با سرویس‌های خارجی

## 🚀 نحوه راه‌اندازی

### پیش‌نیازها
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) یا [VS Code](https://code.visualstudio.com/)

### نصب و راه‌اندازی

1. **کلون کردن ریپوزیتوری**
```bash
git clone https://github.com/your-username/SmartTourismSystem.git
cd SmartTourismSystem
```
2. **تنظیم connection string**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=SmartTourismDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```


3. **دریافت API Key از Google AI Studio**

- به [Google AI Studio](https://makersuite.google.com/app/apikey) بروید
- مقدار API Key جدید ایجاد کنید
- در فایل appsettings.json قرار دهید:

```json
{
  "Gemini": {
    "ApiKey": "AIzaSyxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
  }
}
```


4. **اجرای Migrationها و ایجاد دیتابیس**

```bash
dotnet ef database update
```


5. **اجرای پروژه**

```bash
dotnet run
```

6. **دسترسی به برنامه**

```bash
مرورگر: `https://localhost:7015`
```

## 📁 ساختار پروژه

```text
SmartTourismSystem/
├── Pages/                 # صفحات Razor
│   ├── Index.cshtml      # صفحه اصلی
│   └── Shared/           # Layoutها و partialها
├── Models/               # مدل‌های دیتابیس
│   ├── User.cs
│   ├── TouristPlace.cs
│   ├── AIChat.cs
│   └── ...
├── Services/             # سرویس‌های business logic
│   ├── GeminiService.cs
│   └── SmartTourismService.cs
│   └── ApplicationDbContext.cs # Context و Configuration
└── wwwroot/              # فایل‌های استاتیک
    ├── css/
    └── js/
```


## 🤝 مشارکت در پروژه

1. ابتدا Fork کنید

2. سپس Branch ایجاد کنید (git checkout -b feature/AmazingFeature)

3. سپس Commit کنید (git commit -m 'Add some AmazingFeature')

4. پس از اتمام کار Push کنید (git push origin feature/AmazingFeature)

5. و در نهایت Pull Request ایجاد کنید
