using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class UpdatedValueCorpPetCartProductProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "PetCartProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "PetCartProducts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "CorporateCarts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventType",
                table: "CorporateCarts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "PetCartProducts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "PetCartProducts",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "CorporateCarts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventType",
                table: "CorporateCarts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
