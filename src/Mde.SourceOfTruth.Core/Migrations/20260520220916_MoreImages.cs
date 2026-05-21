using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mde.SourceOfTruth.Core.Migrations
{
    /// <inheritdoc />
    public partial class MoreImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "CreatedAt", "FilePath", "MemoriaId", "Type" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222223"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/koeken.jpg", new Guid("00000000-0000-0000-0000-000000000008"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/guatemala.jpg", new Guid("00000000-0000-0000-0000-000000000011"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/home.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
