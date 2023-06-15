using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orient.Migrations
{
    public partial class initila : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    softwareEngineeringAttempts = table.Column<int>(type: "int", nullable: false),
                    softwareEngineeringCompletions = table.Column<int>(type: "int", nullable: false),
                    softwareEnginneringMeanScore = table.Column<int>(type: "int", nullable: false),
                    dataScienceingAttempts = table.Column<int>(type: "int", nullable: false),
                    dataScienceCompletions = table.Column<int>(type: "int", nullable: false),
                    dataSciencegMeanScore = table.Column<int>(type: "int", nullable: false),
                    UXAttempts = table.Column<int>(type: "int", nullable: false),
                    UXCompletions = table.Column<int>(type: "int", nullable: false),
                    UXMeanScore = table.Column<int>(type: "int", nullable: false),
                    gameAttempts = table.Column<int>(type: "int", nullable: false),
                    gameCompletions = table.Column<int>(type: "int", nullable: false),
                    gameMeanScore = table.Column<int>(type: "int", nullable: false),
                    msAttempts = table.Column<int>(type: "int", nullable: false),
                    msCompletions = table.Column<int>(type: "int", nullable: false),
                    msMeanScore = table.Column<int>(type: "int", nullable: false),
                    loginCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "unit1_Questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correctAnswear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear4 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit1_Questions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correct = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountStatistics");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "unit1_Questions");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
