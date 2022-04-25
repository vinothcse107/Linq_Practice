namespace ApiTest.Model;
public class Locations
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public int Location_Id { get; set; }
      public ICollection<department> Department { get; set; }

      public string Street_Address { get; set; }
      public string Postal_Code { get; set; }
      public string City { get; set; }
      public string State_Province { get; set; }


      [ForeignKey("Country")]
      public string Country_Id { get; set; }
      [JsonIgnore]
      public Countries Country { get; set; }
}
