using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class CartUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "PetCarts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetCartID",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PetCarts_ApplicationUserID",
                table: "PetCarts",
                column: "ApplicationUserID",
                unique: true,
                filter: "[ApplicationUserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PetCarts_AspNetUsers_ApplicationUserID",
                table: "PetCarts",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetCarts_AspNetUsers_ApplicationUserID",
                table: "PetCarts");

            migrationBuilder.DropIndex(
                name: "IX_PetCarts_ApplicationUserID",
                table: "PetCarts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "PetCarts");

            migrationBuilder.DropColumn(
                name: "PetCartID",
                table: "AspNetUsers");
        }
    }
}
