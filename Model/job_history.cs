using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApiTest.Model;
public class Job_History
{
      [Key]
      public int Job_History_Id { get; set; }


      [ForeignKey("Employee")]
      public int Employee_ID { get; set; }
      [JsonIgnore]
      public employee Employee { get; set; }


      public DateTime Start_Date { get; set; }
      public DateTime End_Date { get; set; }

      [ForeignKey("Jobs")]
      public string Job_Id { get; set; }
      [JsonIgnore]
      public Jobs Jobs { get; set; }


      [ForeignKey("Department")]
      public int Department_Id { get; set; }
      [JsonIgnore]
      public department Department { get; set; }


}