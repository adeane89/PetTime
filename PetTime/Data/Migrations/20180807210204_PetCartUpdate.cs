using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class PetCartUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorporateCartID",
                table: "PetCarts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TherapyCartID",
                table: "PetCarts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PetCarts_CorporateCartID",
                table: "PetCarts",
                column: "CorporateCartID");

            migrationBuilder.CreateIndex(
                name: "IX_PetCarts_TherapyCartID",
                table: "PetCarts",
                column: "TherapyCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCarts_CorporateCarts_CorporateCartID",
                table: "PetCarts",
                column: "CorporateCartID",
                principalTable: "CorporateCarts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PetCarts_TherapyCarts_TherapyCartID",
                table: "PetCarts",
                column: "TherapyCartID",
                principalTable: "TherapyCarts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCarts_CorporateCarts_CorporateCartID",
                table: "PetCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_PetCarts_TherapyCarts_TherapyCartID",
                table: "PetCarts");

            migrationBuilder.DropIndex(
                name: "IX_PetCarts_CorporateCartID",
                table: "PetCarts");

            migrationBuilder.DropIndex(
                name: "IX_PetCarts_TherapyCartID",
                table: "PetCarts");

            migrationBuilder.DropColumn(
                name: "CorporateCartID",
                table: "PetCarts");

            migrationBuilder.DropColumn(
                name: "TherapyCartID",
                table: "PetCarts");
        }
    }
}
