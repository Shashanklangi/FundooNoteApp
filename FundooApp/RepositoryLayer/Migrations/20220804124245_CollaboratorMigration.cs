using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class CollaboratorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorTable",
                columns: table => new
                {
                    CollabID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTable", x => x.CollabID);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_NotesTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NotesTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_NoteId",
                table: "CollaboratorTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_UserId",
                table: "CollaboratorTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTable");
        }
    }
}
