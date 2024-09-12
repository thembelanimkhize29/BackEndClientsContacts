using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientsContactsProj.Migrations
{
    /// <inheritdoc />
    public partial class AddStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_clients_ClientsId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_contacts_ContactsId",
                table: "ClientContact");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientContact",
                table: "ClientContact");

            migrationBuilder.RenameTable(
                name: "ClientContact",
                newName: "client_contact_link");

            migrationBuilder.RenameIndex(
                name: "IX_ClientContact_ContactsId",
                table: "client_contact_link",
                newName: "IX_client_contact_link_ContactsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_client_contact_link",
                table: "client_contact_link",
                columns: new[] { "ClientsId", "ContactsId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_client_contact_link_clients_ClientsId",
                table: "client_contact_link",
                column: "ClientsId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_client_contact_link_contacts_ContactsId",
                table: "client_contact_link",
                column: "ContactsId",
                principalTable: "contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_contact_link_clients_ClientsId",
                table: "client_contact_link");

            migrationBuilder.DropForeignKey(
                name: "FK_client_contact_link_contacts_ContactsId",
                table: "client_contact_link");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_client_contact_link",
                table: "client_contact_link");

            migrationBuilder.RenameTable(
                name: "client_contact_link",
                newName: "ClientContact");

            migrationBuilder.RenameIndex(
                name: "IX_client_contact_link_ContactsId",
                table: "ClientContact",
                newName: "IX_ClientContact_ContactsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientContact",
                table: "ClientContact",
                columns: new[] { "ClientsId", "ContactsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_clients_ClientsId",
                table: "ClientContact",
                column: "ClientsId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_contacts_ContactsId",
                table: "ClientContact",
                column: "ContactsId",
                principalTable: "contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
