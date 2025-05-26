using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MadrinHotelCRM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EkPaketler",
                columns: table => new
                {
                    EkPaketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaketAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PaketAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkPaketler", x => x.EkPaketId);
                });

            migrationBuilder.CreateTable(
                name: "Etiketler",
                columns: table => new
                {
                    EtiketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtiketAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiketler", x => x.EtiketId);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    MusteriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YabanciUyrukluMu = table.Column<bool>(type: "bit", nullable: false),
                    PasaportNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TcNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TelNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cinsiyet = table.Column<int>(type: "int", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.MusteriId);
                });

            migrationBuilder.CreateTable(
                name: "OdaTipleri",
                columns: table => new
                {
                    OdaTipiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdaTurAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OdaAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdaTipleri", x => x.OdaTipiId);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YabanciUyrukluMu = table.Column<bool>(type: "bit", nullable: false),
                    PasaportNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TcKimlik = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.PersonelId);
                });

            migrationBuilder.CreateTable(
                name: "SistemLoglar",
                columns: table => new
                {
                    SistemLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZamanDamgasi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogSeviyesi = table.Column<int>(type: "int", nullable: false),
                    Kaynak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mesaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Istisna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HttpYontemi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogJsonVerisi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemLoglar", x => x.SistemLogId);
                });

            migrationBuilder.CreateTable(
                name: "Tarifeler",
                columns: table => new
                {
                    TarifeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarifeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IndirimOrani = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifeler", x => x.TarifeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeriBildirimler",
                columns: table => new
                {
                    GeriBildirimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Konu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mesaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeriBildirimler", x => x.GeriBildirimId);
                    table.ForeignKey(
                        name: "FK_GeriBildirimler_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusteriEtiketler",
                columns: table => new
                {
                    MusteriID = table.Column<int>(type: "int", nullable: false),
                    EtiketID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriEtiketler", x => new { x.MusteriID, x.EtiketID });
                    table.ForeignKey(
                        name: "FK_MusteriEtiketler_Etiketler_EtiketID",
                        column: x => x.EtiketID,
                        principalTable: "Etiketler",
                        principalColumn: "EtiketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusteriEtiketler_Musteriler_MusteriID",
                        column: x => x.MusteriID,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Odalar",
                columns: table => new
                {
                    OdaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdaTipiId = table.Column<int>(type: "int", nullable: false),
                    OdaNumarasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odalar", x => x.OdaId);
                    table.ForeignKey(
                        name: "FK_Odalar_OdaTipleri_OdaTipiId",
                        column: x => x.OdaTipiId,
                        principalTable: "OdaTipleri",
                        principalColumn: "OdaTipiId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenelTakipler",
                columns: table => new
                {
                    GenelTakipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YaratilmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DegistirilmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SilinmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IslemTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TabloAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KayitId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    EskiVeriJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YeniVeriJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenelTakipler", x => x.GenelTakipId);
                    table.ForeignKey(
                        name: "FK_GenelTakipler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MusteriEtkilesimler",
                columns: table => new
                {
                    MusteriEtkilesimId = table.Column<int>(type: "int", nullable: false),
                    EtkilesimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kanal = table.Column<int>(type: "int", nullable: false),
                    Notlar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusteriID = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriEtkilesimler", x => x.MusteriEtkilesimId);
                    table.ForeignKey(
                        name: "FK_MusteriEtkilesimler_Musteriler_MusteriEtkilesimId",
                        column: x => x.MusteriEtkilesimId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId");
                    table.ForeignKey(
                        name: "FK_MusteriEtkilesimler_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rezervasyonlar",
                columns: table => new
                {
                    RezervasyonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    OdaTipiId = table.Column<int>(type: "int", nullable: false),
                    TarifeId = table.Column<int>(type: "int", nullable: false),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    IptalTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IptalNedeni = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyonlar", x => x.RezervasyonId);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_OdaTipleri_OdaTipiId",
                        column: x => x.OdaTipiId,
                        principalTable: "OdaTipleri",
                        principalColumn: "OdaTipiId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Tarifeler_TarifeId",
                        column: x => x.TarifeId,
                        principalTable: "Tarifeler",
                        principalColumn: "TarifeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OdaTarifeleri",
                columns: table => new
                {
                    OdaId = table.Column<int>(type: "int", nullable: false),
                    TarifeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdaTarifeleri", x => new { x.OdaId, x.TarifeId });
                    table.ForeignKey(
                        name: "FK_OdaTarifeleri_Odalar_OdaId",
                        column: x => x.OdaId,
                        principalTable: "Odalar",
                        principalColumn: "OdaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OdaTarifeleri_Tarifeler_TarifeId",
                        column: x => x.TarifeId,
                        principalTable: "Tarifeler",
                        principalColumn: "TarifeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturalar",
                columns: table => new
                {
                    FaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RezervasyonId = table.Column<int>(type: "int", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    FaturaOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturalar", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_Faturalar_Rezervasyonlar_RezervasyonId",
                        column: x => x.RezervasyonId,
                        principalTable: "Rezervasyonlar",
                        principalColumn: "RezervasyonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonelRezervasyon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    RezervasyonId = table.Column<int>(type: "int", nullable: false),
                    CheckInTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Durum = table.Column<int>(type: "int", nullable: false),
                    Notlar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelRezervasyon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonelRezervasyon_Personeller_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Personeller",
                        principalColumn: "PersonelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonelRezervasyon_Rezervasyonlar_RezervasyonId",
                        column: x => x.RezervasyonId,
                        principalTable: "Rezervasyonlar",
                        principalColumn: "RezervasyonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RezervasyonPaketler",
                columns: table => new
                {
                    RezervasyonId = table.Column<int>(type: "int", nullable: false),
                    PaketId = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervasyonPaketler", x => new { x.RezervasyonId, x.PaketId });
                    table.ForeignKey(
                        name: "FK_RezervasyonPaketler_EkPaketler_PaketId",
                        column: x => x.PaketId,
                        principalTable: "EkPaketler",
                        principalColumn: "EkPaketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezervasyonPaketler_Rezervasyonlar_RezervasyonId",
                        column: x => x.RezervasyonId,
                        principalTable: "Rezervasyonlar",
                        principalColumn: "RezervasyonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Odemeler",
                columns: table => new
                {
                    OdemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaId = table.Column<int>(type: "int", nullable: false),
                    OdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OdemeYontemi = table.Column<int>(type: "int", nullable: false),
                    IslemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odemeler", x => x.OdemeId);
                    table.ForeignKey(
                        name: "FK_Odemeler_Faturalar_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturalar",
                        principalColumn: "FaturaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Personel", "PERSONEL" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faturalar_RezervasyonId",
                table: "Faturalar",
                column: "RezervasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_GenelTakipler_PersonelId",
                table: "GenelTakipler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_GeriBildirimler_MusteriId",
                table: "GeriBildirimler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriEtiketler_EtiketID",
                table: "MusteriEtiketler",
                column: "EtiketID");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriEtkilesimler_PersonelId",
                table: "MusteriEtkilesimler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Odalar_OdaTipiId",
                table: "Odalar",
                column: "OdaTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_OdaTarifeleri_TarifeId",
                table: "OdaTarifeleri",
                column: "TarifeId");

            migrationBuilder.CreateIndex(
                name: "IX_Odemeler_FaturaId",
                table: "Odemeler",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelRezervasyon_PersonelId",
                table: "PersonelRezervasyon",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelRezervasyon_RezervasyonId",
                table: "PersonelRezervasyon",
                column: "RezervasyonId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_MusteriId",
                table: "Rezervasyonlar",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_OdaTipiId",
                table: "Rezervasyonlar",
                column: "OdaTipiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_TarifeId",
                table: "Rezervasyonlar",
                column: "TarifeId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervasyonPaketler_PaketId",
                table: "RezervasyonPaketler",
                column: "PaketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GenelTakipler");

            migrationBuilder.DropTable(
                name: "GeriBildirimler");

            migrationBuilder.DropTable(
                name: "MusteriEtiketler");

            migrationBuilder.DropTable(
                name: "MusteriEtkilesimler");

            migrationBuilder.DropTable(
                name: "OdaTarifeleri");

            migrationBuilder.DropTable(
                name: "Odemeler");

            migrationBuilder.DropTable(
                name: "PersonelRezervasyon");

            migrationBuilder.DropTable(
                name: "RezervasyonPaketler");

            migrationBuilder.DropTable(
                name: "SistemLoglar");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Etiketler");

            migrationBuilder.DropTable(
                name: "Odalar");

            migrationBuilder.DropTable(
                name: "Faturalar");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "EkPaketler");

            migrationBuilder.DropTable(
                name: "Rezervasyonlar");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "OdaTipleri");

            migrationBuilder.DropTable(
                name: "Tarifeler");
        }
    }
}
