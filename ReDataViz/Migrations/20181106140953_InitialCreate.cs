using Microsoft.EntityFrameworkCore.Migrations;

namespace ReDataViz.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DtvizMessages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    OwnerEmailID = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DtvizMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    EmailID = table.Column<string>(nullable: false),
                    Passsword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EmailID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DtvizMessages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
