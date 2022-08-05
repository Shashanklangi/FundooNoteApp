using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_NotesTable_NoteId",
                table: "CollaboratorTable");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "CollaboratorTable",
                newName: "NoteID");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorTable_NoteId",
                table: "CollaboratorTable",
                newName: "IX_CollaboratorTable_NoteID");

            migrationBuilder.CreateTable(
                name: "LabelTable",
                columns: table => new
                {
                    LabelID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTable", x => x.LabelID);
                    table.ForeignKey(
                        name: "FK_LabelTable_UserTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LabelTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_NoteID",
                table: "LabelTable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_UserId",
                table: "LabelTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_UserTable_NoteID",
                table: "CollaboratorTable",
                column: "NoteID",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_UserTable_NoteID",
                table: "CollaboratorTable");

            migrationBuilder.DropTable(
                name: "LabelTable");

            migrationBuilder.RenameColumn(
                name: "NoteID",
                table: "CollaboratorTable",
                newName: "NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorTable_NoteID",
                table: "CollaboratorTable",
                newName: "IX_CollaboratorTable_NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_NotesTable_NoteId",
                table: "CollaboratorTable",
                column: "NoteId",
                principalTable: "NotesTable",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
