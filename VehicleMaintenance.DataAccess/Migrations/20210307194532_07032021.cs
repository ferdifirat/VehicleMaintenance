using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VehicleMaintenance.DataAccess.Migrations
{
    public partial class _07032021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionType_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PictureGroup",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PictureImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PictureGroup_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Status_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VehicleType_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PlateNo = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    VehicleTypeID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicle_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleType_VehicleTypeID",
                        column: x => x.VehicleTypeID,
                        principalTable: "VehicleType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VehicleID = table.Column<int>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PictureGroupID = table.Column<int>(nullable: true),
                    ExpectedTimeToFix = table.Column<DateTime>(nullable: false),
                    ResponsibleUserID = table.Column<int>(nullable: true),
                    LocationLongitude = table.Column<string>(nullable: true),
                    LocationLatitude = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Maintenance_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_PictureGroup_PictureGroupID",
                        column: x => x.PictureGroupID,
                        principalTable: "PictureGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_User_ResponsibleUserID",
                        column: x => x.ResponsibleUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintenance_Vehicle_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<TimeSpan>(nullable: false),
                    CreatedByUserID = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<TimeSpan>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaintenanceID = table.Column<int>(nullable: true),
                    ActionTypeID = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaintenanceHistory_ActionType_ActionTypeID",
                        column: x => x.ActionTypeID,
                        principalTable: "ActionType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceHistory_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaintenanceHistory_Maintenance_MaintenanceID",
                        column: x => x.MaintenanceID,
                        principalTable: "Maintenance",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionType_CreatedByUserID",
                table: "ActionType",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_CreatedByUserID",
                table: "Maintenance",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_PictureGroupID",
                table: "Maintenance",
                column: "PictureGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_ResponsibleUserID",
                table: "Maintenance",
                column: "ResponsibleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_StatusID",
                table: "Maintenance",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_UserID",
                table: "Maintenance",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_VehicleID",
                table: "Maintenance",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_ActionTypeID",
                table: "MaintenanceHistory",
                column: "ActionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_CreatedByUserID",
                table: "MaintenanceHistory",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceHistory_MaintenanceID",
                table: "MaintenanceHistory",
                column: "MaintenanceID");

            migrationBuilder.CreateIndex(
                name: "IX_PictureGroup_CreatedByUserID",
                table: "PictureGroup",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_CreatedByUserID",
                table: "Status",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedByUserID",
                table: "User",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_CreatedByUserID",
                table: "Vehicle",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_UserID",
                table: "Vehicle",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleTypeID",
                table: "Vehicle",
                column: "VehicleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleType_CreatedByUserID",
                table: "VehicleType",
                column: "CreatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceHistory");

            migrationBuilder.DropTable(
                name: "ActionType");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "PictureGroup");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
