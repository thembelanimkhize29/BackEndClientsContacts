using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientsContactsProj.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToContactEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_contacts_email",
                table: "contacts",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contacts_email",
                table: "contacts");
        }
    }
}
