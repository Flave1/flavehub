using Microsoft.EntityFrameworkCore.Migrations;

namespace flavehub.Data.Migrations
{
    public partial class AddedCommentedPropToCommentsObjs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Commented",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commented",
                table: "Comments");
        }
    }
}
