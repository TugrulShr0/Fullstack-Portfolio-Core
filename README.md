# 🚀 ASP.NET Core 8.0 Portfolio & Management System

![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C# 12](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![EF Core 8](https://img.shields.io/badge/EF%20Core-8.0-512BD4?style=for-the-badge)
![MSSQL](https://img.shields.io/badge/MSSQL-Server-CC292B?style=for-the-badge&logo=microsoftsqlserver)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-7952B3?style=for-the-badge&logo=bootstrap)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

Modern, çok katmanlı mimari (N-Tier Architecture) ile geliştirilmiş, rol bazlı yetkilendirme (Admin, Moderatör, Yazar), dinamik içerik yönetimi, AJAX kullanıcı işlemleri ve canlı hava durumu API entegrasyonu barındıran **Portfolyo ve Yönetim Sistemi** projesidir.

## ✨ Öne Çıkan Özellikler

### 🌐 **Vitrin (Ana Sayfa)**
* **Dinamik ViewComponent Yapısı:** Hakkımda, Hizmetler, Yetenekler, Projeler, Deneyimler ve Referanslar veritabanından dinamik olarak çekilir.
* **Ziyaretçi İletişim Formu:** Ziyaretçilerin gönderdiği mesajlar anında veritabanına işlenir ve yönetim paneline düşer.

### 🛡️ **Gelişmiş Yönetim Paneli (Admin & Moderatör)**
* **Corona Dark Admin Teması:** Karanlık şablon ile modern kullanıcı arayüzü.
* **Dinamik Rol Yönetimi (`RoleController`):** Sistemde dinamik olarak yeni roller oluşturma ve kullanıcılara rol atama/çıkarma (Identity).
* **AJAX İle Yazar İşlemleri:** Sayfa yenilenmeden dinamik olarak yazar ekleme, listeleme ve silme.
* **İçerik Yönetimi:** Projeler, Hizmetler, Yetenekler, Referanslar, Deneyimler ve Sosyal Medya hesaplarının tam CRUD yönetimi.
* **Duyuru Yönetimi (`AnnouncementController`):** Yazarlara iletilecek duyuruları yayınlama ve düzenleme.
* **Otomatik Dosya Temizliği (`FileHelper`):** Resim güncellemelerinde eski görsellerin sunucu diskinden (`wwwroot`) otomatik silinmesi.

### ✍️ **Yazar Paneli (Writer Area)**
* **Skydash Teması:** Yazarlara özel kullanıcı dostu panel.
* **İç Mesajlaşma Sistemi:** Yazarlar ve Admin arasında gelen/giden mesaj kutusu (`ReceiverMessage` / `SenderMessage`).
* **Profil Yönetimi:** Profil resmi, ad-soyad ve şifre güncelleme.
* **Canlı Hava Durumu API:** OpenWeatherMap API ile canlı hava durumu takibi.

---

## 🏗️ Proje Mimarısı

Proje **N-Tier Architecture (Çok Katmanlı Mimari)** ve **SOLID** ilkelerine uygun olarak modüler bir yapıda tasarlanmıştır:

```text
Core_Proje /
├── 📂 EntityLayer          # Veritabanı Entity modelleri (DbContext nesneleri)
├── 📂 DataAccessLayer      # EF Core DbContext, Repositories & Migrations
├── 📂 BusinessLayer        # İş kuralları, Manager sınıfları & FluentValidation
├── 📂 Core_Proje           # Presentation Layer (ASP.NET Core MVC App)
└── 📂 Core_Proje_Api       # REST API Katmanı (Category & Weather Endpoints)
