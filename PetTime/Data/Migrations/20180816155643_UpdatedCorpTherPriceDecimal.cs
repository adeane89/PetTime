using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class UpdatedCorpTherPriceDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TherapyCarts",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TimeLength",
                table: "TherapyCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CorporateCarts",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TimeLength",
                table: "CorporateCarts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeLength",
                table: "TherapyCarts");

            migrationBuilder.DropColumn(
                name: "TimeLength",
                table: "CorporateCarts");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "TherapyCarts",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "CorporateCarts",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
