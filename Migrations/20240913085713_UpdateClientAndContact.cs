using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientsContactsProj.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientAndContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "contacts",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "contacts",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "contacts",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ClientCode",
                table: "clients",
                newName: "client_code");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "clients",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "client_code",
                table: "clients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "ClientsId",
                table: "client_contact_link",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "contacts",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "contacts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "contacts",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "client_code",
                table: "clients",
                newName: "ClientCode");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "clients",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ClientCode",
                table: "clients",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "ClientsId",
                table: "client_contact_link",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }
    }
}
