using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mde.SourceOfTruth.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEditedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Occation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    MemoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Memorias_MemoriaId",
                        column: x => x.MemoriaId,
                        principalTable: "Memorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaItems_Memorias_MemoriaId",
                        column: x => x.MemoriaId,
                        principalTable: "Memorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Memorias",
                columns: new[] { "Id", "CreatedOn", "Description", "EventDate", "LastEditedOn", "Name", "Occation" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iconische toren en symbool van Parijs.", new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eiffel Tower", "Travel" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oud Romeins amfitheater.", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colosseum", "Travel" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Historische stadspoort in Berlijn.", new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brandenburg Gate", "Work" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beroemde basiliek ontworpen door Gaudí.", new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sagrada Familia", "Travel" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bekende klokkentoren van Londen.", new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Big Ben", "Other" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "HouseNumber", "Latitude", "Longitude", "MemoriaId", "Street" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Paris", "France", "5", 48.858370100000002, 2.2944813000000002, new Guid("00000000-0000-0000-0000-000000000001"), "Champ de Mars" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "Rome", "Italy", "1", 41.890210199999999, 12.492230899999999, new Guid("00000000-0000-0000-0000-000000000002"), "Piazza del Colosseo" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), "Berlin", "Germany", "1", 52.516274600000003, 13.377704100000001, new Guid("00000000-0000-0000-0000-000000000003"), "Pariser Platz" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), "Barcelona", "Spain", "401", 41.403629899999999, 2.1743557999999998, new Guid("00000000-0000-0000-0000-000000000004"), "Carrer de Mallorca" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), "London", "United Kingdom", "1", 51.500729200000002, -0.1246254, new Guid("00000000-0000-0000-0000-000000000005"), "Westminster" }
                });

            migrationBuilder.InsertData(
                table: "MediaItems",
                columns: new[] { "Id", "CreatedAt", "FilePath", "MemoriaId", "Type" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222220"), new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/france.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222221"), new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/frankrijk.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://06dfrpsm-44338.brs.devtunnels.ms/img/home.jpg", new Guid("00000000-0000-0000-0000-000000000001"), "Photo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_MemoriaId",
                table: "Addresses",
                column: "MemoriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaItems_MemoriaId",
                table: "MediaItems",
                column: "MemoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "MediaItems");

            migrationBuilder.DropTable(
                name: "Memorias");
        }
    }
}
