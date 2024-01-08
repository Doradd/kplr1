using System;
using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace ComplectPlus.Data.Migrations
{    /// <inheritdoc />
    public partial class AllClassesF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Doljnosts",
                columns: table => new
                {
                    DoljnostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoljnostName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doljnosts", x => x.DoljnostId);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ManufacturerPName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.ManufacturerId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerUrName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerAdress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.SellerId);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    StorageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.StorageId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoljnostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_Staffs_Doljnosts_DoljnostId",
                        column: x => x.DoljnostId,
                        principalTable: "Doljnosts",
                        principalColumn: "DoljnostId");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Deliveries_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "SellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issuances",
                columns: table => new
                {
                    IssuanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    DateIssuance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuances", x => x.IssuanceId);
                    table.ForeignKey(
                        name: "FK_Issuances_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearRelease = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryId = table.Column<int>(type: "int", nullable: true),
                    IssuanceId = table.Column<int>(type: "int", nullable: true),
                    StorageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_Components_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryId");
                    table.ForeignKey(
                        name: "FK_Components_Issuances_IssuanceId",
                        column: x => x.IssuanceId,
                        principalTable: "Issuances",
                        principalColumn: "IssuanceId");
                    table.ForeignKey(
                        name: "FK_Components_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "ManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "StorageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_CategoryId",
                table: "Components",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_DeliveryId",
                table: "Components",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_IssuanceId",
                table: "Components",
                column: "IssuanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ManufacturerId",
                table: "Components",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_StorageId",
                table: "Components",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SellerId",
                table: "Deliveries",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Issuances_StaffId",
                table: "Issuances",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_DoljnostId",
                table: "Staffs",
                column: "DoljnostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Positions_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Issuances");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Doljnosts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ima",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Secsurname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
