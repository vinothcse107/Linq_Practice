using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Apitest.Controllers
{
      [Route("[controller]")]
      public class Firex : Controller
      {
            private readonly DbxContext context;

            public Firex(ILogger<Firex> logger, DbxContext context)
            {
                  this.context = context;
            }

            // From the following table, write a SQL query to find those employees 
            // whose salary is less than 6000. Return full name(first and last name), and salary.

            [HttpGet("Get_Salary_Less_Than_6000")]
            public async Task<IActionResult> GetData1()
            {
                  var x = await context.employees
                              .Where(w => w.salary < 6000)
                              .Select(s => new
                              {
                                    FullName = s.First_Name + " " +
                                    s.Last_Name,
                                    Salary = s.salary
                              })
                              .ToListAsync();
                  return Ok(x);
            }



            // From the following table, write a SQL query to find those employees whose salary 
            // is higher than 8000. Return first name, last name and department number and salary.
            [HttpGet("Get_Salary__Greater_Than_8000")]
            public async Task<IActionResult> GetData2()
            {
                  var x = await context.employees
                              .Where(w => w.salary > 8000)
                              .Select(s => new
                              {
                                    FirstName = s.First_Name,
                                    LastName = s.Last_Name,
                                    Department_ID = s.Department_ID,
                                    Salary = s.salary
                              })
                              .ToListAsync();
                  return Ok(x);
            }


            // From the following table, write a SQL query to find those employees whose 
            // last name is "McEwen". Return first name, last name and department ID.
            [HttpGet("Get_LastName_McEwen")]
            public async Task<IActionResult> GetData3()
            {
                  var x = await context.employees
                              .Where(w => w.Last_Name.Equals("McEwen"))
                              .Select(s => new
                              {
                                    FirstName = s.First_Name,
                                    LastName = s.Last_Name,
                                    Department_ID = s.Department_ID
                              })
                              .ToListAsync();
                  return Ok(x);
            }


            // From the following table, write a SQL query to find the details of 'Marketing'
            //  department.Return all fields.
            [HttpGet("Get_Department_Details_Marketing")]
            public async Task<IActionResult> GetData4()
            {
                  var x = await context.departments
                              .Where(w => w.Department_Name.Equals("Marketing"))
                              .Select(s => new
                              {
                                    s
                              })
                              .ToListAsync();
                  return Ok(x);
            }


            // From the following table, write a SQL query to find those employees whose first 
            // name does not contain the letter ‘M’. Sort the result-set in ascending order by 
            // department ID.Return full name(first and last name together), hire_date, salary and department_id.
            [HttpGet("Get_FirstName_Not_M")]
            public async Task<IActionResult> GetData5()
            {
                  var x = await context.employees
                              .Where(w => !w.First_Name.Contains("M"))
                              .OrderBy(x => x.Department_ID)
                              .Select(s => new
                              {
                                    FullName = s.First_Name + " " + s.Last_Name,
                                    Hire_Date = s.Hire_Date,
                                    Salary = s.salary,
                                    Department_Id = s.Department_ID
                              })
                              .ToListAsync();
                  return Ok(x);
            }

            // From the following table, write a SQL query to find those 
            // employees who falls one of the following criteria : 
            // 1. whose salary is in the range of 8000, 12000 (Begin and end values are included.) and get some commission. 
            // 2. : those employees who joined before ‘2003-06-05’ and not included in the department number 40, 120 and 70. Return all fields
            [HttpGet("Get_Employees_Under_Condition")]
            public async Task<IActionResult> GetData6()
            {
                  var x = await context.employees
                              .Where(w => (w.salary >= 8000 && w.salary <= 12000) &&
                                          ((w.Department_ID != 40 || w.Department_ID != 70 || w.Department_ID != 120)
                                          && w.Hire_Date < new DateTime(2003, 06, 05))
                                    )
                              .ToListAsync();
                  return Ok(new { x = x, count = x.Count() });
            }

            //  From the following table, write a SQL query to find those employees whose salary is in the range 9000,17000 
            // (Begin and end values are included). Return full name, contact details and salary.

            [HttpGet("Get_Employees_Under_Condition1")]
            public async Task<IActionResult> GetData7()
            {
                  var x = await context.employees
                              .Where(w => (w.salary >= 9000 && w.salary <= 17000))
                              .Select(s => new
                              {
                                    FullName = s.First_Name + " " + s.Last_Name,
                                    Contact_Details = s.Phone_Number + " - " + s.Email,
                                    Renumeration = s.salary
                              })
                              .ToListAsync();
                  return Ok(x);
            }


            //  From the following table, write a SQL query to find those employees who were hired 
            // during November 5th, 2007 and July 5th, 2009. Return full name (first and last), job id and hire date.
            [HttpGet("Get_Employees_By_HireDate")]
            public async Task<IActionResult> GetData8()
            {
                  var x = await context.employees
                              .Where(w => w.Hire_Date >= new DateTime(1987, 05, 05) && w.Hire_Date <= new DateTime(1987, 09, 05))
                              .Select(s => new
                              {
                                    FullName = s.First_Name + " " + s.Last_Name,
                                    JobId = s.Job_Id,
                                    Hire_Date = s.Hire_Date
                              })
                              .ToListAsync();
                  return Ok(x);
            }

            //  From the following table, write a SQL query to find those employees whose first name contains the letters D, S, or N
            // sort the result-set in descending order by salary. Return all fields. 

            [HttpGet("Get_Employees_By_Same_Contains_SND")]
            public async Task<IActionResult> GetData9()
            {
                  var x = await context.employees
                              .Where(w => w.First_Name.Contains("d") || w.First_Name.Contains("s") || w.First_Name.Contains("n"))
                              .OrderByDescending(o => o.salary)
                              .ToListAsync();
                  return Ok(x);
            }


            // From the following table, write a SQL query to find those employees who earn above 11000 
            // or the seventh character in their phone number is 3. Sort the result-set in descending order by first name.
            // Return full name(first name and last name), hire date, commission percentage, email, and telephone separated by '-', and salary.
            [HttpGet("Get_Employees_By_Condition")]
            public async Task<IActionResult> GetData10()
            {
                  var x = await context.employees
                              .Where((w) => w.Phone_Number.Substring(6, 1) == "3" || w.salary >= 11000)
                              .OrderByDescending(o => o.First_Name)
                              .Select(s => new
                              {
                                    FullName = $"{s.First_Name} {s.Last_Name}",
                                    HireDate = s.Hire_Date,
                                    CommisionPercentage = s.Commission_PCT * 100,
                                    Contact = $"{s.Phone_Number} - {s.Email} "
                              })
                              .ToListAsync();
                  return Ok(x);
            }

            // From the following table, write a SQL query to count the number of employees,
            //  sum of all salary, and difference between the highest salary 
            // and lowest salary by each job id.Return job_id, count, sum, salary_difference.
            [HttpGet("Get_Department_Average")]
            public async Task<IActionResult> GetData11()
            {
                  var x = await context.jobs
                              .Select(s => new
                              {
                                    JobId = s.Job_ID,
                                    Count = s.Employees.Count(),
                                    Salary = (from x in s.Employees select x.salary).Sum(),
                                    Difference = (from x in s.Employees select x.salary).Max()
                                                - (from x in s.Employees select x.salary).Min()
                              })
                              .ToListAsync();
                  return Ok(x);
            }

            // From the following table, write a SQL query to find each job ids where two or more employees
            // worked for more than 300 days.Return job id.

            [HttpGet("Get_JobHistory_GT300")]
            public IActionResult GetData12()
            {
                  var x = context.job_history
                              .GroupBy(b => b.Job_Id)
                              .Where(w => w.Select(g => (g.End_Date - g.Start_Date).TotalDays >= 300).Count() >= 2)
                              .Select(s => new
                              {
                                    Job_Id = s.Key,
                              });
                  return Ok(x);
            }

            // From the following table, write a SQL query to count the number of cities in each
            //  country has. Return country ID and number of cities.
            [HttpGet("Get_Count_Of_Cities")]
            public IActionResult GetData13()
            {
                  var x = context.locations
                              .GroupBy(b => b.Country_Id)
                              .Select(s => new
                              {
                                    Job_Id = s.Key,
                                    Cities = s.Select(g => g.City).Count()
                              });
                  return Ok(x);
            }

            [HttpGet("Get_Employees_Under_ManagerID")]
            public IActionResult GetData14()
            {
                  var x = context.employees
                              .GroupBy(b => b.Manager_ID)
                              .Select(s => new
                              {
                                    Job_Id = s.Key,
                                    Cities = s.Select(g => g.EmployeeID).Count()
                              });
                  return Ok(x);
            }
      }
}