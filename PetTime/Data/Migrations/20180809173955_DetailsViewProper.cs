using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class DetailsViewProper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalCount",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Pets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalCount",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Pets");
        }
    }
}
