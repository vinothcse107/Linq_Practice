using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Apitest.Data_Context;

public static class Seed
{

      public static async Task SeedRegions(DbxContext context)
      {
            if (context.regions.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Regions.json");
            var JsonData = JsonSerializer.Deserialize<List<Region>>(Data);

            foreach (var x in JsonData)
            {
                  await context.regions.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Regions Seeding Done");
      }

      public static async Task SeedCountryAsync(DbxContext context)
      {
            if (context.country.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Countries.json");
            var JsonData = JsonSerializer.Deserialize<List<Countries>>(Data);

            foreach (var x in JsonData)
            {
                  await context.country.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("country Seeding Done");
      }
      public static async Task SeedLocationAsync(DbxContext context)
      {
            // if (context.regions.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Locations.json");
            var JsonData = JsonSerializer.Deserialize<List<Locations>>(Data);

            foreach (var x in JsonData)
            {
                  Console.WriteLine(x);
                  await context.locations.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Locations Seeding Done");
      }
      public static async Task SeedDepartment(DbxContext context)
      {
            // if (context.regions.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Department.json");
            var JsonData = JsonSerializer.Deserialize<List<department>>(Data);

            foreach (var x in JsonData)
            {
                  await context.departments.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Department Seeding Done");
      }
      public static async Task SeedEmployees(DbxContext context)
      {
            // if (context.employees.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Employees.json");
            var JsonData = JsonSerializer.Deserialize<List<employee>>(Data);

            foreach (var x in JsonData)
            {
                  await context.employees.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Employees Seeding Done");
      }
      public static async Task SeedJobs(DbxContext context)
      {
            // if (context.jobs.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Jobs.json");
            var JsonData = JsonSerializer.Deserialize<List<Jobs>>(Data);

            foreach (var x in JsonData)
            {
                  await context.jobs.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Jobs Seeding Done");
      }
      public static async Task SeedJobHistory(DbxContext context)
      {
            // if (context.job_history.Any()) return;
            var Data = System.IO.File.ReadAllText("Data_Context/SeedData/Job_History.json");
            var JsonData = JsonSerializer.Deserialize<List<Job_History>>(Data);

            foreach (var x in JsonData)
            {
                  await context.job_history.AddAsync(x);
            }
            await context.SaveChangesAsync();
            Console.WriteLine("Job_History Seeding Done");
      }
}