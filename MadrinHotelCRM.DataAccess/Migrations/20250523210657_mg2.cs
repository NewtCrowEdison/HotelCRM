using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MadrinHotelCRM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DropColumn(
                name: "OtelCıkıs",
                table: "Rezervasyonlar");

            migrationBuilder.DropColumn(
                name: "OtelGiris",
                table: "Rezervasyonlar");

            migrationBuilder.AlterColumn<string>(
                name: "TcKimlik",
                table: "Personeller",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PasaportNo",
                table: "Personeller",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "YabanciUyrukluMu",
                table: "Personeller",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "TcNo",
                table: "Musteriler",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<string>(
                name: "PasaportNo",
                table: "Musteriler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "YabanciUyrukluMu",
                table: "Musteriler",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_PersonelRezervasyon_PersonelId",
                table: "PersonelRezervasyon",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelRezervasyon_RezervasyonId",
                table: "PersonelRezervasyon",
                column: "RezervasyonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonelRezervasyon");

            migrationBuilder.DropColumn(
                name: "PasaportNo",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "YabanciUyrukluMu",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "PasaportNo",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "YabanciUyrukluMu",
                table: "Musteriler");

            migrationBuilder.AddColumn<DateTime>(
                name: "OtelCıkıs",
                table: "Rezervasyonlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OtelGiris",
                table: "Rezervasyonlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "TcKimlik",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TcNo",
                table: "Musteriler",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", null, "Musteri", "MUSTERI" });
        }
    }
}
