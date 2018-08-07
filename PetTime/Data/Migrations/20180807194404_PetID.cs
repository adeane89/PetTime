using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class PetID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCartProducts_Pets_PetID",
                table: "PetCartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetCarts_PetCartID",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_PetCartID",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetCartID",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetProductID",
                table: "PetCartProducts");

            migrationBuilder.AlterColumn<int>(
                name: "PetID",
                table: "PetCartProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PetCartProducts_Pets_PetID",
                table: "PetCartProducts",
                column: "PetID",
                principalTable: "Pets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCartProducts_Pets_PetID",
                table: "PetCartProducts");

            migrationBuilder.AddColumn<int>(
                name: "PetCartID",
                table: "Pets",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PetID",
                table: "PetCartProducts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PetProductID",
                table: "PetCartProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetCartID",
                table: "Pets",
                column: "PetCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCartProducts_Pets_PetID",
                table: "PetCartProducts",
                column: "PetID",
                principalTable: "Pets",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetCarts_PetCartID",
                table: "Pets",
                column: "PetCartID",
                principalTable: "PetCarts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
