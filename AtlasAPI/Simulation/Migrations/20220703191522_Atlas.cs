using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simulation.Migrations
{
    public partial class Atlas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    ExpCode = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    AssigneedProject = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Access = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectKEY = table.Column<string>(type: "varchar(30)", nullable: false),
                    ProjectText = table.Column<string>(type: "varchar(50)", nullable: true),
                    ProjectTime = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectKEY);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    IssueID = table.Column<string>(type: "varchar(30)", nullable: false),
                    Project = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueType = table.Column<string>(type: "varchar(30)", nullable: false),
                    IssueSummary = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IssueDescription = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueStatus = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueResolution = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueStepsToRepro = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueSeverity = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueProbability = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueCreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IssueModifiedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    IssueFixVersion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueReporter = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueAssignee = table.Column<string>(type: "varchar(30)", nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.IssueID);
                    table.ForeignKey(
                        name: "FK_Issues_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityText = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ProjectOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ProjectNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueTypeOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueTypeNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueSummaryOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueSummaryNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueDescriptionOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueDescriptionNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueStatusOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueStatusNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueResolutionOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueResolutionNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueStepsToReproOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueStepsToReproNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueSeverityOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueSeverityNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueProbabilityOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueProbabilityNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueFixVersionOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueFixVersionNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueReporterOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueReporterNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueAssigneeOldValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueAssigneeNewValue = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ActivityUser = table.Column<string>(type: "varchar(30)", nullable: true),
                    ActivityTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IssueID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK_Activity_Issues_IssueID",
                        column: x => x.IssueID,
                        principalTable: "Issues",
                        principalColumn: "IssueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentName = table.Column<string>(type: "varchar(30)", nullable: true),
                    AttachmentContent = table.Column<byte[]>(type: "varbinary(8000)", nullable: true),
                    AttachmentTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IssueID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentID);
                    table.ForeignKey(
                        name: "FK_Attachments_Issues_IssueID",
                        column: x => x.IssueID,
                        principalTable: "Issues",
                        principalColumn: "IssueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentText = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    CommentUser = table.Column<string>(type: "varchar(30)", nullable: true),
                    CommentTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IssueID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_Issues_IssueID",
                        column: x => x.IssueID,
                        principalTable: "Issues",
                        principalColumn: "IssueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    VersionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VersionTag = table.Column<string>(type: "varchar(30)", nullable: true),
                    Project = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueType = table.Column<string>(type: "varchar(30)", nullable: false),
                    IssueSummary = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IssueDescription = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueStatus = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueResolution = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueStepsToRepro = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueSeverity = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueProbability = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueCreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    IssueModifiedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueFixVersion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IssueReporter = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueAssignee = table.Column<string>(type: "varchar(30)", nullable: true),
                    IssueID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.VersionID);
                    table.ForeignKey(
                        name: "FK_Versions_Issues_IssueID",
                        column: x => x.IssueID,
                        principalTable: "Issues",
                        principalColumn: "IssueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_IssueID",
                table: "Activity",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_IssueID",
                table: "Attachments",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_IssueID",
                table: "Comments",
                column: "IssueID");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserId",
                table: "Issues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_IssueID",
                table: "Versions",
                column: "IssueID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
