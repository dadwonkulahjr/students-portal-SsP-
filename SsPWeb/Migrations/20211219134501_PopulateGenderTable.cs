using Microsoft.EntityFrameworkCore.Migrations;

namespace SsPWeb.Migrations
{
    public partial class PopulateGenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genders Values('Male', 'Gender of a male.')");
            migrationBuilder.Sql("INSERT INTO Genders Values('Female', 'Gender of a female.')");
            migrationBuilder.Sql("INSERT INTO Genders Values('Other', 'Gender of a other.')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
