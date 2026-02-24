# Agriculture Management System (ASP.NET Core MVC - N Layer Architecture)
Bu proje, **ASP.NET Core MVC** kullanılarak geliştirilmiş olup, yazılım mimarisi olarak **N-Layer Architecture** prensiplerine uygun şekilde tasarlanmıştır.  
Projede **Generic Repository Pattern**, **Dependency Injection (DI)** ve **ASP.NET Core Identity** kullanılarak sürdürülebilir ve test edilebilir bir yapı hedeflenmiştir.

## 🚀 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core
- MS SQL Server
- ASP.NET Core Identity
- N-Layer Architecture
- Generic Repository Pattern
- Dependency Injection (DI)
- ViewComponent Yapısı

## Katmanlı Mimari Yapısı

Proje aşağıdaki katmanlardan oluşmaktadır:

- **EntityLayer**  
  Veri modelleri (Entities) bu katmanda tutulur.

- **DataAccessLayer**  
  EF Core, DbContext ve Repository implementasyonları bu katmanda yer alır.

- **BusinessLayer**  
  İş kuralları, servis ve manager sınıfları bu katmanda tanımlanmıştır.

- **Presentation Layer (AgriculturePresentationn)**  
  MVC UI bileşenleri (Controllers, Views, ViewComponents) bu katmanda yer alır.

## Kullanılan Tasarım Desenleri

- Generic Repository Pattern  
- Dependency Injection  
- Layered Architecture  
- SOLID Prensiplerine uygun yapı

## Öne Çıkan Özellikler

- Katmanlı mimari ile bağımlılıkların azaltılması  
- Tekrar kullanılabilir Generic Repository yapısı  
- Identity ile kullanıcı doğrulama ve yetkilendirme  
- ViewComponent ile tekrar eden UI bileşenlerinin yönetimi  
- İş kurallarının Business Layer'da yönetilmesi

## ⚙️ Kurulum

1. Projeyi klonlayın:

```bash
git clone https://github.com/NisaKaraca/AgriculturePresentationn.git
```

2. `appsettings.Development.json` içerisine kendi SQL Server bağlantı bilginizi ekleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Integrated Security=True;TrustServerCertificate=True;"
}
```

3. Migration işlemleri için:

```bash
Update-Database
```

4. Projeyi çalıştırın.
