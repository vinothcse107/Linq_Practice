using Microsoft.EntityFrameworkCore;

namespace API.Data_Context;
public class DbxContext : DbContext
{
      public DbxContext(DbContextOptions<DbxContext> options) : base(options) { }
      public DbSet<employee> employees { get; set; }
      public DbSet<department> departments { get; set; }
      public DbSet<Job_Grades> job_grades { get; set; }
      public DbSet<Job_History> job_history { get; set; }
      public DbSet<Jobs> jobs { get; set; }
      public DbSet<Countries> country { get; set; }
      public DbSet<Region> regions { get; set; }
      public DbSet<Locations> locations { get; set; }

}
