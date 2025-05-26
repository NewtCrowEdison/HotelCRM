using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MadrinHotelCRM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EkPaketler",
                columns: new[] { "EkPaketId", "Fiyat", "OlusturmaTarihi", "PaketAciklama", "PaketAdi" },
                values: new object[,]
                {
                    { 1, 750.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Misafirlerimize özel 1 saatlik spa ve masaj hizmeti.", "Spa ve Masaj Paketi" },
                    { 2, 1200.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Geliş ve dönüş için VIP transfer hizmeti.", "Havalimanı Transferi" },
                    { 3, 950.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deniz manzaralı restoranda 2 kişilik özel akşam yemeği.", "Romantik Akşam Yemeği" },
                    { 4, 300.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eğitimli personel tarafından saatlik çocuk bakımı.", "Çocuk Bakımı Hizmeti" },
                    { 5, 1800.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bodrum koylarını keşfedeceğiniz 4 saatlik tekne turu.", "Yarım Gün Tekne Turu" }
                });

            migrationBuilder.InsertData(
                table: "OdaTipleri",
                columns: new[] { "OdaTipiId", "Fiyat", "Kapasite", "OdaAciklama", "OdaTurAd" },
                values: new object[,]
                {
                    { 1, 1200.00m, 1, "Konforlu tek kişilik yatak, şehir manzaralı, mini bar ve ücretsiz Wi-Fi.", "Standart Tek Kişilik" },
                    { 2, 1800.00m, 2, "Geniş çift kişilik yatak, klima, balkon ve televizyon.", "Standart Çift Kişilik" },
                    { 3, 2500.00m, 2, "Deniz manzaralı, king size yatak, özel jakuzi, kahve makinesi.", "Deluxe Oda" },
                    { 4, 3200.00m, 4, "İki ayrı oda, geniş salon, çocuk yatağı, mutfak bölümü.", "Aile Odası" },
                    { 5, 4000.00m, 2, "Romantik dekorasyon, jakuzili banyo, özel teras, sürpriz ikramlar.", "Balayı Süiti" }
                });

            migrationBuilder.InsertData(
                table: "Tarifeler",
                columns: new[] { "TarifeId", "BaslangicTarihi", "BitisTarihi", "Fiyat", "IndirimOrani", "TarifeAdi" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2800.00m, 0, "Yaz Sezonu Standart Tarife" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1800.00m, 20, "Kış Kampanyası" },
                    { 3, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2200.00m, 15, "Sevgililer Günü Paketi" },
                    { 4, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3200.00m, 10, "Bayram Özel Tarife" },
                    { 5, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000.00m, 25, "Sonbahar Erken Rezervasyon" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EkPaketler",
                keyColumn: "EkPaketId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EkPaketler",
                keyColumn: "EkPaketId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EkPaketler",
                keyColumn: "EkPaketId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EkPaketler",
                keyColumn: "EkPaketId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EkPaketler",
                keyColumn: "EkPaketId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OdaTipleri",
                keyColumn: "OdaTipiId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OdaTipleri",
                keyColumn: "OdaTipiId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OdaTipleri",
                keyColumn: "OdaTipiId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OdaTipleri",
                keyColumn: "OdaTipiId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OdaTipleri",
                keyColumn: "OdaTipiId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tarifeler",
                keyColumn: "TarifeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tarifeler",
                keyColumn: "TarifeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tarifeler",
                keyColumn: "TarifeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tarifeler",
                keyColumn: "TarifeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tarifeler",
                keyColumn: "TarifeId",
                keyValue: 5);
        }
    }
}
