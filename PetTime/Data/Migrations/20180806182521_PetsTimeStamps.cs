using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class PetsTimeStamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "Pets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "Pets");
        }
    }
}
