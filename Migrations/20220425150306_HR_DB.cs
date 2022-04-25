using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apitest.Migrations
{
      public partial class HR_DB : Migration
      {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.CreateTable(
                      name: "job_grades",
                      columns: table => new
                      {
                            Job_Grades_Id = table.Column<int>(type: "int", nullable: false),
                            Grade_Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Lowest_Salary = table.Column<int>(type: "int", nullable: false),
                            Highest_Salary = table.Column<int>(type: "int", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_job_grades", x => x.Job_Grades_Id);
                      });

                  migrationBuilder.CreateTable(
                      name: "jobs",
                      columns: table => new
                      {
                            Job_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                            Job_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Min_Salary = table.Column<int>(type: "int", nullable: false),
                            Max_Salary = table.Column<int>(type: "int", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_jobs", x => x.Job_ID);
                      });

                  migrationBuilder.CreateTable(
                      name: "regions",
                      columns: table => new
                      {
                            Region_Id = table.Column<int>(type: "int", nullable: false),
                            Region_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_regions", x => x.Region_Id);
                      });

                  migrationBuilder.CreateTable(
                      name: "country",
                      columns: table => new
                      {
                            Country_Id = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                            Country_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Region_Id = table.Column<int>(type: "int", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_country", x => x.Country_Id);
                            table.ForeignKey(
                          name: "FK_country_regions_Region_Id",
                          column: x => x.Region_Id,
                          principalTable: "regions",
                          principalColumn: "Region_Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateTable(
                      name: "locations",
                      columns: table => new
                      {
                            Location_Id = table.Column<int>(type: "int", nullable: false),
                            Street_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            State_Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Country_Id = table.Column<string>(type: "nvarchar(2)", nullable: true)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_locations", x => x.Location_Id);
                            table.ForeignKey(
                          name: "FK_locations_country_Country_Id",
                          column: x => x.Country_Id,
                          principalTable: "country",
                          principalColumn: "Country_Id");
                      });

                  migrationBuilder.CreateTable(
                      name: "departments",
                      columns: table => new
                      {
                            Department_ID = table.Column<int>(type: "int", nullable: false),
                            Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Manager_ID = table.Column<int>(type: "int", nullable: false),
                            Location_ID = table.Column<int>(type: "int", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_departments", x => x.Department_ID);
                            table.ForeignKey(
                          name: "FK_departments_locations_Location_ID",
                          column: x => x.Location_ID,
                          principalTable: "locations",
                          principalColumn: "Location_Id",
                          onDelete: ReferentialAction.Cascade);
                      });

                  migrationBuilder.CreateTable(
                      name: "employees",
                      columns: table => new
                      {
                            EmployeeID = table.Column<int>(type: "int", nullable: false),
                            First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                            Hire_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                            Job_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                            salary = table.Column<float>(type: "real", nullable: false),
                            Commission_PCT = table.Column<float>(type: "real", nullable: false),
                            Manager_ID = table.Column<int>(type: "int", nullable: false),
                            Department_ID = table.Column<int>(type: "int", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_employees", x => x.EmployeeID);
                            table.ForeignKey(
                          name: "FK_employees_departments_Department_ID",
                          column: x => x.Department_ID,
                          principalTable: "departments",
                          principalColumn: "Department_ID",
                          onDelete: ReferentialAction.Cascade);
                            table.ForeignKey(
                          name: "FK_employees_jobs_Job_Id",
                          column: x => x.Job_Id,
                          principalTable: "jobs",
                          principalColumn: "Job_ID");
                      });

                  migrationBuilder.CreateTable(
                      name: "job_history",
                      columns: table => new
                      {
                            Job_History_Id = table.Column<int>(type: "int", nullable: false)
                              .Annotation("SqlServer:Identity", "1, 1"),
                            Employee_ID = table.Column<int>(type: "int", nullable: false),
                            Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                            End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                            Job_Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                            Department_Id = table.Column<int>(type: "int", nullable: false)
                      },
                      constraints: table =>
                      {
                            table.PrimaryKey("PK_job_history", x => x.Job_History_Id);
                            table.ForeignKey(
                          name: "FK_job_history_departments_Department_Id",
                          column: x => x.Department_Id,
                          principalTable: "departments",
                          principalColumn: "Department_ID",
                          onDelete: ReferentialAction.Restrict);
                            table.ForeignKey(
                          name: "FK_job_history_employees_Employee_ID",
                          column: x => x.Employee_ID,
                          principalTable: "employees",
                          principalColumn: "EmployeeID",
                          onDelete: ReferentialAction.Cascade);
                            table.ForeignKey(
                          name: "FK_job_history_jobs_Job_Id",
                          column: x => x.Job_Id,
                          principalTable: "jobs",
                          principalColumn: "Job_ID");
                      });

                  migrationBuilder.CreateIndex(
                      name: "IX_country_Region_Id",
                      table: "country",
                      column: "Region_Id");

                  migrationBuilder.CreateIndex(
                      name: "IX_departments_Location_ID",
                      table: "departments",
                      column: "Location_ID");

                  migrationBuilder.CreateIndex(
                      name: "IX_employees_Department_ID",
                      table: "employees",
                      column: "Department_ID");

                  migrationBuilder.CreateIndex(
                      name: "IX_employees_Job_Id",
                      table: "employees",
                      column: "Job_Id");

                  migrationBuilder.CreateIndex(
                      name: "IX_job_history_Department_Id",
                      table: "job_history",
                      column: "Department_Id");

                  migrationBuilder.CreateIndex(
                      name: "IX_job_history_Employee_ID",
                      table: "job_history",
                      column: "Employee_ID");

                  migrationBuilder.CreateIndex(
                      name: "IX_job_history_Job_Id",
                      table: "job_history",
                      column: "Job_Id");

                  migrationBuilder.CreateIndex(
                      name: "IX_locations_Country_Id",
                      table: "locations",
                      column: "Country_Id");
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                  migrationBuilder.DropTable(
                      name: "job_grades");

                  migrationBuilder.DropTable(
                      name: "job_history");

                  migrationBuilder.DropTable(
                      name: "employees");

                  migrationBuilder.DropTable(
                      name: "departments");

                  migrationBuilder.DropTable(
                      name: "jobs");

                  migrationBuilder.DropTable(
                      name: "locations");

                  migrationBuilder.DropTable(
                      name: "country");

                  migrationBuilder.DropTable(
                      name: "regions");
            }
      }
}
