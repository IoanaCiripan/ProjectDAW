using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWeb.Migrations
{
    public partial class removeEmotions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieEmotion");

            migrationBuilder.DropTable(
                name: "Emotion");

            migrationBuilder.DropColumn(
                name: "PreferredEmotions",
                table: "Profile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredEmotions",
                table: "Profile",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Emotion",
                columns: table => new
                {
                    EmotionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emotion", x => x.EmotionId);
                });

            migrationBuilder.CreateTable(
                name: "MovieEmotion",
                columns: table => new
                {
                    MovieEmotionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmotionId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEmotion", x => x.MovieEmotionId);
                    table.ForeignKey(
                        name: "FK_MovieEmotion_Emotion_EmotionId",
                        column: x => x.EmotionId,
                        principalTable: "Emotion",
                        principalColumn: "EmotionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieEmotion_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieEmotion_EmotionId",
                table: "MovieEmotion",
                column: "EmotionId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEmotion_MovieId",
                table: "MovieEmotion",
                column: "MovieId");
        }
    }
}
