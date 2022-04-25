namespace ApiTest.Model;
public class Jobs
{

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public string Job_ID { get; set; }
      public string Job_Title { get; set; }
      public int Min_Salary { get; set; }
      public int Max_Salary { get; set; }
      public ICollection<employee> Employees { get; set; }
      public ICollection<Job_History> JobHistory { get; set; }


}

