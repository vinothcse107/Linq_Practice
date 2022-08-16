global using ApiTest.Model;
global using Apitest.Controllers;
global using Microsoft.AspNetCore.Mvc;
global using API.Data_Context;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Apitest.Data_Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbxContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("HR")));

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()){}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Middleware Ends Here

// Seeding Part

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
      var con = services.GetRequiredService<DbxContext>();
      con.Database.Migrate();
      // Run Once Only On Start

      // await Seed.SeedRegions(con);
      // await Seed.SeedCountryAsync(con);
      // await Seed.SeedLocationAsync(con);
      // await Seed.SeedDepartment(con);
      // await Seed.SeedJobs(con);
      // await Seed.SeedEmployees(con);
      // await Seed.SeedJobHistory(con);
      // await Seed.SeedJobGrades(con);
}
catch (Exception ex)
{
      var logger = services.GetRequiredService<ILogger<Program>>();
      logger.LogError(ex, "An error occurred during migration");
}

await app.RunAsync();