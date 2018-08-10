using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetID",
                table: "PetCarts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PetOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOrders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PetOrderProducts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    PetOrderID = table.Column<Guid>(nullable: false),
                    ProductAnimalCount = table.Column<int>(nullable: true),
                    ProductDescription = table.Column<string>(nullable: true),
                    ProductEventType = table.Column<int>(nullable: true),
                    ProductID = table.Column<int>(nullable: true),
                    ProductLength = table.Column<int>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOrderProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PetOrderProducts_PetOrders_PetOrderID",
                        column: x => x.PetOrderID,
                        principalTable: "PetOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetCarts_PetID",
                table: "PetCarts",
                column: "PetID");

            migrationBuilder.CreateIndex(
                name: "IX_PetOrderProducts_PetOrderID",
                table: "PetOrderProducts",
                column: "PetOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCarts_Pets_PetID",
                table: "PetCarts",
                column: "PetID",
                principalTable: "Pets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCarts_Pets_PetID",
                table: "PetCarts");

            migrationBuilder.DropTable(
                name: "PetOrderProducts");

            migrationBuilder.DropTable(
                name: "PetOrders");

            migrationBuilder.DropIndex(
                name: "IX_PetCarts_PetID",
                table: "PetCarts");

            migrationBuilder.DropColumn(
                name: "PetID",
                table: "PetCarts");
        }
    }
}
