<details open>
<summary><strong>🇹🇷 Türkçe</strong></summary>

# Madrin Hotel CRM

Madrin Hotel CRM, otel işletmelerinin misafir yönetimi, rezervasyon, oda durumu takibi, faturalandırma ve temel operasyonlarını dijital ortamda etkin bir şekilde yönetmesini sağlayan modern bir CRM (Customer Relationship Management) sistemidir.

## Geliştirici Ekip

- [Rojbin Vefa Can Ağtaş](https://www.linkedin.com/in/rojbin-agtas/)
- [Yasemin Özhan](https://www.linkedin.com/in/yasemin-ozhan/)
- [Zafercan Demir](https://www.linkedin.com/in/zafercan-demir/)
- [Zeynep Kaska](https://www.linkedin.com/in/zeynep-kaska/)

## Proje Versiyonu

- 1.0 – 05.05.2025  
- 2.0 – 20.06.2025

===========================================================================

## 📖 İçindekiler

- [Genel Bakış](#genel-bakış)
- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Mimari Yapı](#mimari-yapı)
- [Temel Modüller](#temel-modüller)
- [Geliştirme Süreci](#geliştirme-süreci)
- [Kazançlar](#kazançlar)
- [Gelecekteki Geliştirmeler](#gelecekteki-geliştirmeler)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
- [Lisans](#lisans)

===========================================================================

## Genel Bakış

Madrin Hotel CRM, otel işletmeleri için özelleştirilmiş, güvenli ve kullanıcı dostu bir çözüm sunar. Admin ve resepsiyonist rollerine sahip kullanıcılar; misafir bilgileri, oda durumu, rezervasyonlar ve fatura işlemlerini kolaylıkla yönetebilir.

===========================================================================

## Kullanılan Teknolojiler

- **ASP.NET Core 8.0**  
- **Entity Framework Core (Code-First)**  
- **SQL Server**  
- **ASP.NET Identity**  
- **AutoMapper**  
- **Bootstrap & jQuery**  
- **AJAX**  
- **Serilog (Logging)**  
- **JWT Authentication**

===========================================================================

## Mimari Yapı

Proje, toplam **10 katmandan** oluşan modüler ve sürdürülebilir bir katmanlı mimari yapısıyla geliştirilmiştir:

1. **Entities** – Veri modelleri (Musteri, Oda, Rezervasyon, vb.)  
2. **DataAccess** – DbContext, migration işlemleri  
3. **DTO** – Veri transfer nesneleri  
4. **Business** – AutoMapper ve iş kuralları  
5. **Repositories** – Repository pattern & Unit of Work  
6. **Services** – Servis sınıfları ve interface'ler  
7. **Extensions** – Middleware, DI, JWT konfigürasyonları  
8. **API** – Web API katmanı (RESTful servisler)  
9. **MVC** – Kullanıcı arayüzü (Resepsiyonist)  
10. **MVCAdmin** – Yönetici paneli

===========================================================================

## Temel Modüller

- **Misafir Yönetimi**: Kayıt, güncelleme ve listeleme  
- **Rezervasyon Yönetimi**: Oda uygunluk sorgulama, rezervasyon işlemleri  
- **Oda Yönetimi**: Oda durumu takibi ve düzenleme  
- **Paket Yönetimi**: Konaklama, kahvaltı, spa vb. hizmet tanımlamaları  
- **Faturalandırma**: Manuel fatura oluşturma  
- **Kullanıcı Yetkilendirme**: Rol bazlı erişim kontrolü (Admin / Resepsiyonist)

===========================================================================

## Geliştirme Süreci

**2 Sprintte** tamamlanmıştır:

### Sprint 1: Backend Geliştirme (1–14. Gün)

- Katmanlı yapı kuruldu  
- EF Core ile veri modeli oluşturuldu  
- CRUD endpoint'leri geliştirildi  
- Identity ve JWT kimlik doğrulama sistemi entegre edildi

### Sprint 2: Arayüz ve Entegrasyon (15–28. Gün)

- MVC ve MVCAdmin katmanları oluşturuldu  
- Razor View + Bootstrap + jQuery ile UI geliştirildi  
- AJAX ile dinamik işlemler entegre edildi  
- Serilog loglama altyapısı kuruldu

===========================================================================

## Kazançlar

- Modüler ve genişletilebilir mimari  
- Güvenli kimlik doğrulama ve yetkilendirme  
- Kullanıcı dostu ve responsive arayüz  
- Web API ile servis entegrasyonlarına açık yapı  
- Gerçek dünya projeleri için uygulanabilir tasarım desenleri

===========================================================================

## Gelecekteki Geliştirmeler

- 🔐 Canlı İyzico Ödeme Entegrasyonu  
- 🧾 Otomatik Faturalandırma Sistemi  
- 📊 Dinamik Raporlama (Gelir, Doluluk Oranı vb.)  
- 📱 Gelişmiş Responsive Tasarım  
- 📨 SMS/E-posta Bildirimleri  
- 👤 Gelişmiş Misafir Profilleri  
- 🛡 Yeni Rol Tipleri (Temizlik, Finans, vb.)

===========================================================================

## ⚙ Kurulum ve Çalıştırma

```bash
# 1. Repository'yi klonlayın
git clone https://github.com/kullaniciAdi/madrin-hotel-crm.git

# 2. Visual Studio 2022 veya üstü ile açın

# 3. Gerekli NuGet paketlerini geri yükleyin
# 4. appsettings.json dosyasını yapılandırın
# 5. EF Core migration işlemlerini tamamlayın:
dotnet ef database update

# 6. Projeyi çalıştırın (F5 veya dotnet run)
</details>
```

<details open>
<summary><strong>🇬🇧 English</strong></summary>

# Madrin Hotel CRM

Madrin Hotel CRM is a modern Customer Relationship Management (CRM) system that enables hotel businesses to efficiently manage guest information, reservations, room statuses, invoicing, and core operations in a digital environment.

## 👥 Developer Team

- [Rojbin Vefa Can Ağtaş](https://www.linkedin.com/in/rojbin-agtas/)
- [Yasemin Özhan](https://www.linkedin.com/in/yasemin-ozhan/)
- [Zafercan Demir](https://www.linkedin.com/in/zafercan-demir/)
- [Zeynep Kaska](https://www.linkedin.com/in/zeynep-kaska/)

## 🧾 Project Versions

- 1.0 – 05.05.2025  
- 2.0 – 20.06.2025

---

## 📖 Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Architecture](#architecture)
- [Core Modules](#core-modules)
- [Development Process](#development-process)
- [Key Outcomes](#key-outcomes)
- [Future Improvements](#future-improvements)
- [Installation](#installation)
- [License](#license)

---

## 🧩 Overview

Madrin Hotel CRM offers a secure and user-friendly solution tailored for hotel operations. Users with Admin and Receptionist roles can easily manage guest records, room statuses, reservations, and invoices through a modern web interface.

---

## 💻 Technologies Used

- **ASP.NET Core 8.0**  
- **Entity Framework Core (Code-First)**  
- **SQL Server**  
- **ASP.NET Identity**  
- **AutoMapper**  
- **Bootstrap & jQuery**  
- **AJAX**  
- **Serilog (Logging)**  
- **JWT Authentication**

---

## 🏗 Architecture

The project is designed using a modular and maintainable **10-layered architecture**:

1. **Entities** – Data models (Guest, Room, Reservation, etc.)  
2. **DataAccess** – DbContext, migration configurations  
3. **DTO** – Data Transfer Objects  
4. **Business** – AutoMapper and business logic  
5. **Repositories** – Repository pattern & Unit of Work  
6. **Services** – Service classes and interfaces  
7. **Extensions** – Middleware, DI, JWT configurations  
8. **API** – Web API layer (RESTful services)  
9. **MVC** – Receptionist UI layer  
10. **MVCAdmin** – Admin panel layer

---

## 📦 Core Modules

- **Guest Management** – Create, update, and list guest records  
- **Reservation Management** – Room availability search and reservation creation  
- **Room Management** – Track and update room statuses  
- **Package Management** – Define services like accommodation, breakfast, spa, etc.  
- **Billing** – Manual invoice generation  
- **User Authorization** – Role-based access control (Admin / Receptionist)

---

## 🚧 Development Process

Completed in **2 sprints**:

### 🏁 Sprint 1 – Backend Development (Day 1–14)

- Established layered architecture  
- Created data models with EF Core  
- Developed CRUD endpoints  
- Integrated Identity & JWT authentication

### 🎯 Sprint 2 – UI and Integration (Day 15–28)

- Built MVC and MVCAdmin layers  
- UI created with Razor View, Bootstrap, and jQuery  
- AJAX used for dynamic interactions  
- Serilog integrated for logging

---

## 🌟 Key Outcomes

- Modular and scalable architecture  
- Secure identity and access management  
- User-friendly and responsive UI  
- RESTful API structure for integrations  
- Practical application of design patterns

---

## 🚀 Future Improvements

- 🔐 Live Iyzico Payment Integration  
- 🧾 Automated Invoicing System  
- 📊 Dynamic Reporting (Revenue, Occupancy Rate, etc.)  
- 📱 Enhanced Mobile Responsiveness  
- 📨 SMS/Email Notifications  
- 👤 Advanced Guest Profiles  
- 🛡 New User Roles (Housekeeping, Finance, etc.)

---

## ⚙ Installation

```bash
# 1. Clone the repository
git clone https://github.com/yourUsername/madrin-hotel-crm.git

# 2. Open the project with Visual Studio 2022 or later

# 3. Restore NuGet packages
# 4. Configure the appsettings.json file
# 5. Apply EF Core migrations
dotnet ef database update

# 6. Run the project
dotnet run
</details>
```
