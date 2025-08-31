using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEPARTMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FIRST_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    LAST_NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SALARY = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    HIRE_DATE = table.Column<DateTime>(type: "DATE", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "SYSDATE"),
                    MODIFIED_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYEES_DEPARTMENTS_DEPARTMENT_ID",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "DEPARTMENTS",
                columns: new[] { "ID", "CREATED_DATE", "DESCRIPTION", "NAME" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 31, 13, 49, 18, 342, DateTimeKind.Local).AddTicks(5689), "Information Technology", "IT" },
                    { 2, new DateTime(2025, 8, 31, 13, 49, 18, 342, DateTimeKind.Local).AddTicks(5759), "Human Resources", "HR" },
                    { 3, new DateTime(2025, 8, 31, 13, 49, 18, 342, DateTimeKind.Local).AddTicks(5766), "Finance Department", "Finance" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEES_DEPARTMENT_ID",
                table: "EMPLOYEES",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEES_EMAIL",
                table: "EMPLOYEES",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEES");

            migrationBuilder.DropTable(
                name: "DEPARTMENTS");
        }
    }
}
