using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "schema_1");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:schema_1.order_status", "Pending,Cancelled,Completed");

            migrationBuilder.Sql(@"
                CREATE CAST (schema_1.order_status AS text) WITH INOUT AS IMPLICIT;
                CREATE CAST (text AS schema_1.order_status) WITH INOUT AS IMPLICIT;
            ");

            migrationBuilder.CreateTable(
                name: "client",
                schema: "schema_1",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar", maxLength: 15, nullable: false),
                    lastname = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "schema_1",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    time = table.Column<TimeOnly>(type: "time", nullable: false, defaultValueSql: "CURRENT_TIME"),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "schema_1.order_status", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "order_client_id_fk",
                        column: x => x.client_id,
                        principalSchema: "schema_1",
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "client",
                schema: "schema_1",
                columns: new[] { "id", "name", "lastname", "birth_date" },
                values: new object[,]
                {
                    { 1, "Alexey", "Smirnov", new DateOnly(1990, 05, 12)},
                    { 2, "Ekaterina", "Kuznetsova", new DateOnly(1985, 08, 23)},
                    { 3, "Dmitry", "Popov", new DateOnly(1978, 11, 30)},
                    { 4, "Olga", "Lebedeva", new DateOnly(1995, 02, 14)},
                    { 5, "Sergey", "Kozlov", new DateOnly(1982, 07, 19)},
                    { 6, "Anna", "Novikova", new DateOnly(1992, 09, 25)},
                    { 7, "Pavel", "Morozov", new DateOnly(1988, 04, 05)},
                    { 8, "Tatyana", "Pavlova", new DateOnly(1973, 12, 10)},
                    { 9, "Nikolay", "Sokolov", new DateOnly(1998, 03, 22)},
                    { 10, "Maria", "Volkova", new DateOnly(1987, 06, 17)}
                });

            migrationBuilder.InsertData(
                table: "order",
                schema: "schema_1",
                columns: new[] { "id", "cost", "date", "time", "client_id", "status" },
                values: new object[,]
                {
                    { 7, 3200m, new DateOnly(2025, 8, 1), new TimeOnly(9, 45, 0), 1, "Pending" },
                    { 8, 15000m, new DateOnly(2023, 8, 23), new TimeOnly(13, 20, 0), 2, "Pending" },
                    { 6, 4800m, new DateOnly(2024, 5, 12), new TimeOnly(11, 30, 0), 1, "Pending" },
                    { 18, 5000m, new DateOnly(2025, 3, 19), new TimeOnly(0, 24, 11), 2, "Pending" },
                    { 16, 8900m, new DateOnly(2025, 10, 25), new TimeOnly(17, 0, 0), 4, "Pending" },
                    { 4, 5642m, new DateOnly(2025, 7, 19), new TimeOnly(12, 32, 56), 5, "Completed" },
                    { 2, 6703.42m, new DateOnly(2024, 7, 19), new TimeOnly(16, 32, 56), 5, "Completed" },
                    { 12, 4200m, new DateOnly(2024, 11, 30), new TimeOnly(12, 45, 0), 3, "Completed" },
                    { 14, 11000m, new DateOnly(2023, 2, 14), new TimeOnly(9, 0, 0), 4, "Completed" },
                    { 10, 6000m, new DateOnly(2025, 3, 15), new TimeOnly(16, 50, 0), 2, "Completed" },
                    { 11, 8300m, new DateOnly(2023, 11, 30), new TimeOnly(12, 0, 0), 3, "Completed" },
                    { 13, 7700m, new DateOnly(2025, 6, 10), new TimeOnly(15, 20, 0), 3, "Completed" },
                    { 19, 5000m, new DateOnly(2025, 3, 19), new TimeOnly(0, 27, 26), 2, "Cancelled" },
                    { 5, 7200m, new DateOnly(2023, 5, 12), new TimeOnly(10, 15, 0), 1, "Completed" },
                    { 15, 5300m, new DateOnly(2024, 2, 14), new TimeOnly(8, 30, 0), 4, "Completed" },
                    { 9, 9500m, new DateOnly(2024, 8, 23), new TimeOnly(14, 10, 0), 2, "Completed" },
                    { 17, 5000m, new DateOnly(2025, 5, 12), new TimeOnly(22, 51, 30), 1, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_client_id",
                schema: "schema_1",
                table: "order",
                column: "client_id");

            migrationBuilder.Sql(
            """
            CREATE OR REPLACE FUNCTION schema_1.get_avg_costs_by_hour()
                RETURNS TABLE(hour numeric, avg_cost numeric)
                LANGUAGE plpgsql
            AS $$
            BEGIN
                RETURN QUERY
                    SELECT EXTRACT(HOUR FROM o.time)::numeric AS hour, 
                           AVG(o.cost) AS avg_cost
                    FROM schema_1.order o
                    WHERE o.status = 'Completed'
                    GROUP BY hour
                    ORDER BY hour DESC;
            END;
            $$;
            """
            );

            migrationBuilder.Sql(
            """
            CREATE OR REPLACE FUNCTION schema_1.get_total_cost_of_birthday_orders()
                RETURNS TABLE(
                    sum numeric, 
                    client_name character varying, 
                    client_lastname character varying
                )
                LANGUAGE plpgsql
            AS $$
            BEGIN
                RETURN QUERY
                    SELECT SUM(o.cost)::numeric, 
                           c.name::character varying as client_name, 
                           c.lastname::character varying as client_lastname
                    FROM schema_1.order o
                    JOIN schema_1.client c ON o.client_id = c.id
                    WHERE o.status = 'Completed'
                      AND EXTRACT(MONTH FROM o.date) = EXTRACT(MONTH FROM c.birth_date)
                      AND EXTRACT(DAY FROM o.date) = EXTRACT(DAY FROM c.birth_date)
                    GROUP BY c.id, c.name, c.lastname;
            END;
            $$;
            """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP CAST IF EXISTS (schema_1.order_status AS text);
                DROP CAST IF EXISTS (text AS schema_1.order_status);
            ");

            migrationBuilder.DropTable(
                name: "order",
                schema: "schema_1");

            migrationBuilder.DropTable(
                name: "client",
                schema: "schema_1");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS schema_1.get_avg_costs_by_hour();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS schema_1.get_total_cost_of_birthday_orders();");
        }
    }
}
