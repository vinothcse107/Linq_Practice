﻿// <auto-generated />
using System;
using API.Data_Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apitest.Migrations
{
    [DbContext(typeof(DbxContext))]
    partial class DbxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiTest.Model.Countries", b =>
                {
                    b.Property<string>("Country_Id")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Country_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region_Id")
                        .HasColumnType("int");

                    b.HasKey("Country_Id");

                    b.HasIndex("Region_Id");

                    b.ToTable("country");
                });

            modelBuilder.Entity("ApiTest.Model.department", b =>
                {
                    b.Property<int>("Department_ID")
                        .HasColumnType("int");

                    b.Property<string>("Department_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Location_ID")
                        .HasColumnType("int");

                    b.Property<int>("Manager_ID")
                        .HasColumnType("int");

                    b.HasKey("Department_ID");

                    b.HasIndex("Location_ID");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("ApiTest.Model.employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<float>("Commission_PCT")
                        .HasColumnType("real");

                    b.Property<int>("Department_ID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Hire_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Job_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Last_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Manager_ID")
                        .HasColumnType("int");

                    b.Property<string>("Phone_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("salary")
                        .HasColumnType("real");

                    b.HasKey("EmployeeID");

                    b.HasIndex("Department_ID");

                    b.HasIndex("Job_Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("ApiTest.Model.Job_Grades", b =>
                {
                    b.Property<int>("Job_Grades_Id")
                        .HasColumnType("int");

                    b.Property<string>("Grade_Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Highest_Salary")
                        .HasColumnType("int");

                    b.Property<int>("Lowest_Salary")
                        .HasColumnType("int");

                    b.HasKey("Job_Grades_Id");

                    b.ToTable("job_grades");
                });

            modelBuilder.Entity("ApiTest.Model.Job_History", b =>
                {
                    b.Property<int>("Job_History_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Job_History_Id"), 1L, 1);

                    b.Property<int>("Department_Id")
                        .HasColumnType("int");

                    b.Property<int>("Employee_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Job_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Job_History_Id");

                    b.HasIndex("Department_Id");

                    b.HasIndex("Employee_ID");

                    b.HasIndex("Job_Id");

                    b.ToTable("job_history");
                });

            modelBuilder.Entity("ApiTest.Model.Jobs", b =>
                {
                    b.Property<string>("Job_ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Job_Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Max_Salary")
                        .HasColumnType("int");

                    b.Property<int>("Min_Salary")
                        .HasColumnType("int");

                    b.HasKey("Job_ID");

                    b.ToTable("jobs");
                });

            modelBuilder.Entity("ApiTest.Model.Locations", b =>
                {
                    b.Property<int>("Location_Id")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country_Id")
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Postal_Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State_Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street_Address")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Location_Id");

                    b.HasIndex("Country_Id");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("ApiTest.Model.Region", b =>
                {
                    b.Property<int>("Region_Id")
                        .HasColumnType("int");

                    b.Property<string>("Region_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Region_Id");

                    b.ToTable("regions");
                });

            modelBuilder.Entity("ApiTest.Model.Countries", b =>
                {
                    b.HasOne("ApiTest.Model.Region", "Region")
                        .WithMany("Countries")
                        .HasForeignKey("Region_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ApiTest.Model.department", b =>
                {
                    b.HasOne("ApiTest.Model.Locations", "Location")
                        .WithMany("Department")
                        .HasForeignKey("Location_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("ApiTest.Model.employee", b =>
                {
                    b.HasOne("ApiTest.Model.department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("Department_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTest.Model.Jobs", "Jobs")
                        .WithMany("Employees")
                        .HasForeignKey("Job_Id");

                    b.Navigation("Department");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("ApiTest.Model.Job_History", b =>
                {
                    b.HasOne("ApiTest.Model.department", "Department")
                        .WithMany("JobHistory")
                        .HasForeignKey("Department_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTest.Model.employee", "Employee")
                        .WithMany("JobHistory")
                        .HasForeignKey("Employee_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTest.Model.Jobs", "Jobs")
                        .WithMany("JobHistory")
                        .HasForeignKey("Job_Id");

                    b.Navigation("Department");

                    b.Navigation("Employee");

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("ApiTest.Model.Locations", b =>
                {
                    b.HasOne("ApiTest.Model.Countries", "Country")
                        .WithMany("Location")
                        .HasForeignKey("Country_Id");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ApiTest.Model.Countries", b =>
                {
                    b.Navigation("Location");
                });

            modelBuilder.Entity("ApiTest.Model.department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("JobHistory");
                });

            modelBuilder.Entity("ApiTest.Model.employee", b =>
                {
                    b.Navigation("JobHistory");
                });

            modelBuilder.Entity("ApiTest.Model.Jobs", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("JobHistory");
                });

            modelBuilder.Entity("ApiTest.Model.Locations", b =>
                {
                    b.Navigation("Department");
                });

            modelBuilder.Entity("ApiTest.Model.Region", b =>
                {
                    b.Navigation("Countries");
                });
#pragma warning restore 612, 618
        }
    }
}
