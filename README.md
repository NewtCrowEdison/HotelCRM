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

- **🔐 Canlı İyzico Ödeme Entegrasyonu**
- **🧾 Otomatik Faturalandırma Sistemi**
- **📊 Dinamik Raporlama (Gelir, Doluluk Oranı vb.)**
- **📱 Gelişmiş Responsive Tasarım**
- **📨 SMS/E-posta Bildirimleri**
- **👤 Gelişmiş Misafir Profilleri**
- **🛡 Yeni Rol Tipleri (Temizlik, Finans, vb.)**

===========================================================================

## ⚙Kurulum ve Çalıştırma

```bash
# 1. Repository'yi klonlayın
git clone https://github.com/kullaniciAdi/madrin-hotel-crm.git

# 2. Visual Studio 2022 veya üstü ile açın

# 3. Gerekli NuGet paketlerini geri yükleyin
# 4. appsettings.json dosyasını yapılandırın
# 5. EF Core migration işlemlerini tamamlayın:
dotnet ef database update

# 6. Projeyi çalıştırın (F5 veya dotnet run)
