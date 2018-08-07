using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class petcategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryModelName",
                table: "Pets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CategoryModelName",
                table: "Pets",
                column: "CategoryModelName");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Categories_CategoryModelName",
                table: "Pets",
                column: "CategoryModelName",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Categories_CategoryModelName",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_CategoryModelName",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "CategoryModelName",
                table: "Pets");
        }
    }
}
