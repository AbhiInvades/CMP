using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMP_Server_API.Migrations
{
    public partial class Telephonedatatypechanged2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    ClinicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Telephone = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.ClinicId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftTime = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => new { x.ClinicID, x.StaffID });
                    table.ForeignKey(
                        name: "FK_Staff_Clinic_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinic",
                        principalColumn: "ClinicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clinic",
                columns: new[] { "ClinicId", "Address", "Department", "Name", "Telephone" },
                values: new object[,]
                {
                    { 1, "Magarpatta, Pune-404040", 0, "Pune Clinic", 22334422L },
                    { 2, "Thane, Pune-404040", 1, "Mumbai Clinic", 33224422L },
                    { 3, "Churchgate, Pune-404040", 2, "Ayush Clinic", 22443322L },
                    { 4, "Hadapsar, Pune-404040", 3, "BestCare Clinic", 22554433L }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "ClinicID", "StaffID", "DOB", "FileName", "Gender", "Name", "Password", "Phone", "Role", "ShiftTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2005, 3, 2, 20, 59, 50, 561, DateTimeKind.Local).AddTicks(3929), "d7570c8a-22e9-4ce9-9827-450f37f35860_images.jpg", 0, "Piyush Mistry", "", 12341234L, 1, 1 },
                    { 1, 2, new DateTime(2004, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2921), "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg", 1, "Mayuri Nehra", "", 123413234L, 0, 0 },
                    { 1, 3, new DateTime(1995, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2973), "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg", 1, "Kedar Mhatre", "", 12341234L, 2, 1 },
                    { 1, 4, new DateTime(1985, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2976), "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg", 1, "Bhushan Mhatre", "", 12341234L, 2, 1 },
                    { 2, 5, new DateTime(1985, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2978), "d7570c8a-22e9-4ce9-9827-450f37f35860_images.jpg", 0, "Manish Patil", "", 12341234L, 1, 2 },
                    { 2, 6, new DateTime(1975, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2979), "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg", 1, "Monika Madane", "", 12341234L, 0, 2 },
                    { 2, 7, new DateTime(2005, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2981), "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg", 1, "Minakshi Mhatre", "", 12341234L, 1, 1 },
                    { 2, 8, new DateTime(2005, 3, 2, 20, 59, 50, 562, DateTimeKind.Local).AddTicks(2983), "33ec0650-60a0-4884-bfef-b0fd26c3d723_download.jpg", 1, "Molly Morgan", "", 12341234L, 1, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Clinic");
        }
    }
}
