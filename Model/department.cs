namespace ApiTest.Model
{
      public class department
      {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Department_ID { get; set; }
            public string Department_Name { get; set; }
            public int Manager_ID { get; set; }


            [ForeignKey("Location")]
            public int Location_ID { get; set; }
            [JsonIgnore]
            public Locations Location { get; set; }


            public ICollection<employee> Employees { get; set; }
            public ICollection<Job_History> JobHistory { get; set; }

      }
}