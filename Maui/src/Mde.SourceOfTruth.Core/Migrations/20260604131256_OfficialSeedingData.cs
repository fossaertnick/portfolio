using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mde.SourceOfTruth.Core.Migrations
{
    /// <inheritdoc />
    public partial class OfficialSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222223"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222224"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222225"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222226"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222227"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222228"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222229"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222230"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222231"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222232"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222233"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222234"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222235"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222236"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222237"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222238"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222239"));

            migrationBuilder.DeleteData(
                table: "MediaItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222240"));

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DeviceName",
                value: "John's Android");

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                columns: new[] { "DeviceIdentifier", "DeviceName" },
                values: new object[] { "device-002", "Jack's Windows" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DeviceName",
                value: "John's iPhone");

            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333334"),
                columns: new[] { "DeviceIdentifier", "DeviceName" },
                values: new object[] { "RZCW0JQTST", "A54 van Nick" });

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "CreatedAt", "FilePath", "MemoriaId", "Type" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris2.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris3.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris4.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris5.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222226"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris6.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222227"), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris7.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222228"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/paris8.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222229"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/rome1.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222230"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/rome2.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222231"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/ventie1.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222232"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie2.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222233"), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie3.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222234"), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie4.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222235"), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie5.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222236"), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie6.jpg", new Guid("00000000-0000-0000-0000-000000000002"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222237"), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie7.jpg", new Guid("00000000-0000-0000-0000-000000000003"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222238"), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/venetie8.jpg", new Guid("00000000-0000-0000-0000-000000000003"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222239"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/sagrada1.jpg", new Guid("00000000-0000-0000-0000-000000000004"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222240"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/sagrada2.jpg", new Guid("00000000-0000-0000-0000-000000000004"), "Photo" }
                });
        }
    }
}
