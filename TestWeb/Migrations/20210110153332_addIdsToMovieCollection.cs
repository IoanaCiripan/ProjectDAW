using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWeb.Migrations
{
    public partial class addIdsToMovieCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Movie_MovieId",
                table: "MovieCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_MovieStatus_MovieStatusId",
                table: "MovieCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Profile_ProfileId",
                table: "MovieCollection");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "MovieCollection",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieStatusId",
                table: "MovieCollection",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieCollection",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Movie_MovieId",
                table: "MovieCollection",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_MovieStatus_MovieStatusId",
                table: "MovieCollection",
                column: "MovieStatusId",
                principalTable: "MovieStatus",
                principalColumn: "MovieStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Profile_ProfileId",
                table: "MovieCollection",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Movie_MovieId",
                table: "MovieCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_MovieStatus_MovieStatusId",
                table: "MovieCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCollection_Profile_ProfileId",
                table: "MovieCollection");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "MovieCollection",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MovieStatusId",
                table: "MovieCollection",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "MovieCollection",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Movie_MovieId",
                table: "MovieCollection",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_MovieStatus_MovieStatusId",
                table: "MovieCollection",
                column: "MovieStatusId",
                principalTable: "MovieStatus",
                principalColumn: "MovieStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCollection_Profile_ProfileId",
                table: "MovieCollection",
                column: "ProfileId",
                principalTable: "Profile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
