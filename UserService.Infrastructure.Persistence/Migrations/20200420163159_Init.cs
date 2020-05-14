using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentService.Infrastructure.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    email_address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customer_id_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "virtual_machine",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("virtual_machine_id_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rental",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(nullable: false),
                    virtual_machine_id = table.Column<Guid>(nullable: true),
                    start_time = table.Column<DateTime>(nullable: false),
                    end_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rental_id_pkey", x => x.id);
                    table.ForeignKey(
                        name: "customer_id_fkey",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "virtual_machine_id_fkey",
                        column: x => x.virtual_machine_id,
                        principalTable: "virtual_machine",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "ix_rentals_customer_id",
                table: "rental",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_rentals_virtual_machine_id",
                table: "rental",
                column: "virtual_machine_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rental");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "virtual_machine");
        }
    }
}
