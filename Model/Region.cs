namespace ApiTest.Model;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Region
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public int Region_Id { get; set; }
      public string Region_Name { get; set; }
      public ICollection<Countries> Countries { get; set; }

}
