namespace ApiTest.Model;
public class Job_Grades
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      public int Job_Grades_Id { get; set; }
      public string Grade_Level { get; set; }
      public int Lowest_Salary { get; set; }
      public int Highest_Salary { get; set; }
}