using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactureGenerator.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "stat",
                table: "companies",
                type: "varchar(17)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)");

            migrationBuilder.AlterColumn<string>(
                name: "nif",
                table: "companies",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "companies",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.CreateTable(
                name: "user_companies",
                columns: table => new
                {
                    userid = table.Column<string>(type: "text", nullable: false),
                    companyid = table.Column<int>(type: "integer", nullable: false),
                    assignedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_companies", x => new { x.userid, x.companyid });
                    table.ForeignKey(
                        name: "fk_user_companies_companies_companyid",
                        column: x => x.companyid,
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_companies_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_companies_companyid",
                table: "user_companies",
                column: "companyid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_companies");

            migrationBuilder.AlterColumn<string>(
                name: "stat",
                table: "companies",
                type: "varchar(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(17)");

            migrationBuilder.AlterColumn<string>(
                name: "nif",
                table: "companies",
                type: "varchar(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "companies",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");
        }
    }
}
