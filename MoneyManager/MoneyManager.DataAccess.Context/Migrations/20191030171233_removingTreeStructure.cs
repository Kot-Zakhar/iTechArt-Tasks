using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyManager.DataAccess.Context.Migrations
{
    public partial class removingTreeStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                WITH tree([Id], [Name], [RealParentId], [level])
                AS (SELECT [Id], [Name],  [Id], 0
	                FROM [Category]
	                WHERE [ParentId] IS NULL
                    UNION ALL
	                SELECT [Category].[Id], [Category].[Name], [tree].[RealParentId], [tree].[level] + 1
	                FROM [Category]
	                INNER JOIN [tree] ON [tree].[id] = [Category].[ParentId])
                
                UPDATE [Transaction] SET [CategoryId] = (
	                SELECT TOP(1) [tree].[RealParentId]
	                FROM [tree]
	                WHERE [tree].[Id] = [Transaction].[CategoryId]
	                );

                DELETE FROM [Category] WHERE [ParentId] IS NOT NULL;
            ");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
