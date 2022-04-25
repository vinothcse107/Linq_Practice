
namespace ApiTest.Model;
public class Countries
{
      [Key]
      [MaxLength(2)]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public string Country_Id { get; set; }
      public ICollection<Locations> Location { get; set; }
      public string Country_Name { get; set; }


      [ForeignKey("Region")]
      public int Region_Id { get; set; }
      [JsonIgnore]
      public Region Region { get; set; }
}


