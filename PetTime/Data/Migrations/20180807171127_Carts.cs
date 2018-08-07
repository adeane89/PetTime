using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class Carts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetCartID",
                table: "Pets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PetCarts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetCarts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PetCartProducts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    PetCartID = table.Column<int>(nullable: false),
                    PetID = table.Column<int>(nullable: true),
                    PetProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetCartProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PetCartProducts_PetCarts_PetCartID",
                        column: x => x.PetCartID,
                        principalTable: "PetCarts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetCartProducts_Pets_PetID",
                        column: x => x.PetID,
                        principalTable: "Pets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetCartID",
                table: "Pets",
                column: "PetCartID");

            migrationBuilder.CreateIndex(
                name: "IX_PetCartProducts_PetCartID",
                table: "PetCartProducts",
                column: "PetCartID");

            migrationBuilder.CreateIndex(
                name: "IX_PetCartProducts_PetID",
                table: "PetCartProducts",
                column: "PetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetCarts_PetCartID",
                table: "Pets",
                column: "PetCartID",
                principalTable: "PetCarts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetCarts_PetCartID",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "PetCartProducts");

            migrationBuilder.DropTable(
                name: "PetCarts");

            migrationBuilder.DropIndex(
                name: "IX_Pets_PetCartID",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetCartID",
                table: "Pets");
        }
    }
}
