using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PetTime.Data.Migrations
{
    public partial class FixedPetCartProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "AnimalCount",
                table: "PetCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "PetCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "PetCartProducts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PetCartProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalCount",
                table: "PetCartProducts");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "PetCartProducts");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "PetCartProducts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PetCartProducts");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Pets",
                nullable: true);
        }
    }
}
