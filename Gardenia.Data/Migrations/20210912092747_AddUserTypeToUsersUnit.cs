﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddUserTypeToUsersUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "UsersUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "UsersUnits");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
