﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lms.Account.EntityFrameworkCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "varchar(500) CHARACTER SET utf8mb4", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreateBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateBy = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
