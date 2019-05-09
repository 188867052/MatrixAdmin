﻿// using System;
// using Microsoft.EntityFrameworkCore.Metadata;
// using Microsoft.EntityFrameworkCore.Migrations;

// namespace Core.Api.Migrations
// {
//    public partial class Init : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Icon",
//                columns: table => new
//                {
//                    Id = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    Size = table.Column<string>(type: "nvarchar(20)", nullable: true),
//                    Color = table.Column<string>(type: "nvarchar(50)", nullable: true),
//                    Custom = table.Column<string>(type: "nvarchar(60)", nullable: true),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Status = table.Column<int>(nullable: false),
//                    IsDeleted = table.Column<int>(nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false),
//                    CreatedByUserId = table.Column<int>(nullable: false),
//                    CreatedByUserName = table.Column<string>(nullable: true),
//                    ModifiedOn = table.Column<DateTime>(nullable: true),
//                    ModifiedByUserId = table.Column<int>(nullable: false),
//                    ModifiedByUserName = table.Column<string>(nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_DncIcon", x => x.Id);
//                });

// migrationBuilder.CreateTable(
//                name: "Menu",
//                columns: table => new
//                {
//                    Id = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Guid = table.Column<Guid>(nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    Url = table.Column<string>(type: "nvarchar(255)", nullable: true),
//                    Alias = table.Column<string>(type: "nvarchar(255)", nullable: true),
//                    Icon = table.Column<string>(type: "nvarchar(128)", nullable: true),
//                    ParentGuid = table.Column<Guid>(nullable: true),
//                    ParentName = table.Column<string>(nullable: true),
//                    Level = table.Column<int>(nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(800)", nullable: true),
//                    Sort = table.Column<int>(nullable: false),
//                    Status = table.Column<int>(nullable: false),
//                    IsDeleted = table.Column<int>(nullable: false),
//                    IsDefaultRouter = table.Column<int>(nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false),
//                    CreatedByUserId = table.Column<int>(nullable: false),
//                    CreatedByUserName = table.Column<string>(nullable: true),
//                    ModifiedOn = table.Column<DateTime>(nullable: true),
//                    ModifiedByUserId = table.Column<int>(nullable: false),
//                    ModifiedByUserName = table.Column<string>(nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_DncMenu", x => x.Guid);
//                });

// migrationBuilder.CreateTable(
//                name: "Role",
//                columns: table => new
//                {
//                    Id = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Status = table.Column<int>(nullable: false),
//                    IsDeleted = table.Column<int>(nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false),
//                    CreatedByUserId = table.Column<int>(nullable: false),
//                    CreatedByUserName = table.Column<string>(nullable: true),
//                    ModifiedOn = table.Column<DateTime>(nullable: true),
//                    ModifiedByUserId = table.Column<int>(nullable: false),
//                    ModifiedByUserName = table.Column<string>(nullable: true),
//                    IsSuperAdministrator = table.Column<bool>(nullable: false),
//                    IsBuiltin = table.Column<bool>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_DncRole", x => x.Code);
//                });

// migrationBuilder.CreateTable(
//                name: "User",
//                columns: table => new
//                {
//                    Id = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Guid = table.Column<Guid>(nullable: false),
//                    LoginName = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    DisplayName = table.Column<string>(type: "nvarchar(50)", nullable: true),
//                    Password = table.Column<string>(type: "nvarchar(255)", nullable: true),
//                    Avatar = table.Column<string>(type: "nvarchar(255)", nullable: true),
//                    UserType = table.Column<int>(nullable: false),
//                    IsLocked = table.Column<int>(nullable: false),
//                    Status = table.Column<int>(nullable: false),
//                    IsDeleted = table.Column<int>(nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false),
//                    CreatedByUserId = table.Column<int>(nullable: false),
//                    CreatedByUserName = table.Column<string>(nullable: true),
//                    ModifiedOn = table.Column<DateTime>(nullable: true),
//                    ModifiedByUserId = table.Column<int>(nullable: false),
//                    ModifiedByUserName = table.Column<string>(nullable: true),
//                    Description = table.Column<string>(type: "nvarchar(800)", nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_DncUser", x => x.Guid);
//                });

// migrationBuilder.CreateTable(
//                name: "Permission",
//                columns: table => new
//                {
//                    Id = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
//                    Code = table.Column<string>(type: "nvarchar(20)", nullable: false),
//                    MenuGuid = table.Column<Guid>(nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    ActionCode = table.Column<string>(type: "nvarchar(80)", nullable: false),
//                    Icon = table.Column<string>(nullable: true),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
//                    Status = table.Column<int>(nullable: false),
//                    IsDeleted = table.Column<int>(nullable: false),
//                    Type = table.Column<int>(nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false),
//                    CreatedByUserId = table.Column<int>(nullable: false),
//                    CreatedByUserName = table.Column<string>(nullable: true),
//                    ModifiedOn = table.Column<DateTime>(nullable: true),
//                    ModifiedByUserId = table.Column<int>(nullable: false),
//                    ModifiedByUserName = table.Column<string>(nullable: true)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_DncPermission", x => x.Code);
//                    table.ForeignKey(
//                        name: "FK_DncPermission_DncMenu_MenuGuid",
//                        column: x => x.MenuGuid,
//                        principalTable: "DncMenu",
//                        principalColumn: "Guid",
//                        onDelete: ReferentialAction.Cascade);
//                });

// migrationBuilder.CreateTable(
//                name: "UserRoleMapping",
//                columns: table => new
//                {
//                    UserGuid = table.Column<Guid>(nullable: false),
//                    RoleCode = table.Column<string>(nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_UserRoleMapping", x => new { x.UserGuid, x.RoleCode });
//                    table.ForeignKey(
//                        name: "FK_UserRoleMapping_DncRole_RoleCode",
//                        column: x => x.RoleCode,
//                        principalTable: "DncRole",
//                        principalColumn: "Code",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_DncUserRoleMapping_DncUser_UserGuid",
//                        column: x => x.UserGuid,
//                        principalTable: "DncUser",
//                        principalColumn: "Guid",
//                        onDelete: ReferentialAction.Restrict);
//                });

// migrationBuilder.CreateTable(
//                name: "RolePermissionMapping",
//                columns: table => new
//                {
//                    RoleCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
//                    PermissionCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
//                    CreatedOn = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_DncRolePermissionMapping", x => new { x.RoleCode, x.PermissionCode });
//                    table.ForeignKey(
//                        name: "FK_DncRolePermissionMapping_DncPermission_PermissionCode",
//                        column: x => x.PermissionCode,
//                        principalTable: "DncPermission",
//                        principalColumn: "Code",
//                        onDelete: ReferentialAction.Restrict);
//                    table.ForeignKey(
//                        name: "FK_DncRolePermissionMapping_DncRole_RoleCode",
//                        column: x => x.RoleCode,
//                        principalTable: "DncRole",
//                        principalColumn: "Code",
//                        onDelete: ReferentialAction.Restrict);
//                });

// migrationBuilder.CreateIndex(
//                name: "IX_Permission_Code",
//                table: "DncPermission",
//                column: "Code",
//                unique: true);

// migrationBuilder.CreateIndex(
//                name: "IX_Permission_MenuGuid",
//                table: "DncPermission",
//                column: "MenuGuid");

// migrationBuilder.CreateIndex(
//                name: "IX_Role_Code",
//                table: "DncRole",
//                column: "Code",
//                unique: true);

// migrationBuilder.CreateIndex(
//                name: "IX_RolePermissionMapping_PermissionCode",
//                table: "RolePermissionMapping",
//                column: "PermissionCode");

// migrationBuilder.CreateIndex(
//                name: "IX_DncUserRoleMapping_RoleCode",
//                table: "UserRoleMapping",
//                column: "RoleCode");
//        }

// protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Icon");

// migrationBuilder.DropTable(
//                name: "RolePermissionMapping");

// migrationBuilder.DropTable(
//                name: "UserRoleMapping");

// migrationBuilder.DropTable(
//                name: "Permission");

// migrationBuilder.DropTable(
//                name: "Role");

// migrationBuilder.DropTable(
//                name: "User");

// migrationBuilder.DropTable(
//                name: "Menu");
//        }
//    }
// }
