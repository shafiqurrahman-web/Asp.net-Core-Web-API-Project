using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Provenance.DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 300, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true),
                    LocationTitle = table.Column<string>(maxLength: 300, nullable: true),
                    LocationAddress = table.Column<string>(maxLength: 2000, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 300, nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Firstname = table.Column<string>(maxLength: 300, nullable: false),
                    Lastname = table.Column<string>(maxLength: 300, nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 300, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provider_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionProduct_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("7cf9e80e-0ffc-4bd3-aa72-4ec80743545d"), "Admin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("68eec1b9-bb00-4a92-a01d-0cd2305722f1"), "User" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductHistory_ProductId",
                table: "ProductHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Provider_UserId",
                table: "Provider",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ProviderId",
                table: "Transaction",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProduct_ProductId",
                table: "TransactionProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProduct_TransactionId",
                table: "TransactionProduct",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHistory");

            migrationBuilder.DropTable(
                name: "TransactionProduct");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
