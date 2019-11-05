using Microsoft.EntityFrameworkCore.Migrations;

namespace ShareMe.DataAccessLayer.Context.Migrations
{
    public partial class TagTableFixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tag",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tag");
        }
    }
}
