using Microsoft.EntityFrameworkCore;

namespace Apitest.Controllers;

[Route("[controller]")]
public class LinqJoin : Controller
{
      public DbxContext _context { get; }

      public LinqJoin(DbxContext context)
      { _context = context; }

      [HttpGet("Get_Employees_Joins_Department_GroupBY_DepartmentName")]
      public async Task<IActionResult> GetData14()
      {
            var x = await _context.departments.Join(
                  _context.employees,
                  i => i.Department_ID,
                  k => k.Department_ID,
                  (i, k) => new { i, k }
            ).GroupBy(o => o.i.Department_Name).Select(y => new
            {
                  Department = y.Key,
                  Employees = y.Select(s => s.k.First_Name)
            }).ToListAsync();
            return Ok(x);
      }

      [HttpGet("Get_Employees_Joins_Department_Joins_Locations")]
      public async Task<IActionResult> GetData15()
      {
            var x = await (from d in _context.departments
                           join e in _context.employees on d.Department_ID equals e.Department_ID
                           join l in _context.locations on d.Location_ID equals l.Location_Id
                           select new
                           {
                                 FirstName = e.First_Name,
                                 LastName = e.Last_Name,
                                 Department = d.Department_Name,
                                 City = l.City,
                                 State_Proviance = l.State_Province
                           }).ToListAsync();
            return Ok(x);
      }
      [HttpGet("Get_Employees_Joins_Department_Joins_Locations_GroupBy_Department_Name")]
      public async Task<IActionResult> GetData16()
      {
            var x = await (from d in _context.departments
                           join e in _context.employees on d.Department_ID equals e.Department_ID
                           join l in _context.locations on d.Location_ID equals l.Location_Id
                           select new { d, e, l })
                           .GroupBy(o => o.d.Department_Name)
                           .Select(s => new
                           {
                                 Department = s.Key,
                                 Employee = s.Select(i =>
                                          new
                                          {
                                                FirstName = i.e.First_Name,
                                                LastName = i.e.Last_Name,
                                                City = i.l.City,
                                                State_Proviance = i.l.State_Province
                                          }),

                           })
                           .ToListAsync();
            return Ok(x);
      }

      // From the following table, write a SQL query to find the first name, 
      // last name, salary, and job grade for all employees.

      [HttpGet("Get_Employees_Join_JobGrades")]
      public async Task<IActionResult> GetData17()
      {
            var x = await (from e in _context.employees
                           from j in _context.job_grades
                           where e.salary >= j.Lowest_Salary && e.salary <= j.Highest_Salary
                           select new
                           {
                                 FirstName = e.First_Name,
                                 LastName = e.Last_Name,
                                 Salary = e.salary,
                                 GradeLevel = j.Grade_Level
                           }).ToListAsync();
            return Ok(x);

            // SELECT E.First_Name, E.Last_Name, E.salary, J.Grade_Level
            // FROM employees E
            // JOIN job_grades J
            // ON E.salary BETWEEN J.Lowest_Salary AND J.Highest_Salary;
      }

      // From the following tables, write a SQL query to find all those employees who work 
      // in department ID 80 or 40. Return first name, last name, department number and department name
      [HttpGet("Get_Employees_From_40_80")]
      public async Task<IActionResult> GetData18()
      {
            var x = await (from d in _context.departments
                           join
                              e in _context.employees
                              on d.Department_ID equals e.Department_ID
                           where d.Department_ID == 40 || d.Department_ID == 80
                           select new
                           {
                                 FirstName = e.First_Name,
                                 LastName = e.Last_Name,
                                 Department_ID = d.Department_ID,
                                 DepartmentName = d.Department_Name
                           }).ToListAsync();
            return Ok(x);
      }
      // From the following tables, write a SQL query to find those employees whose first name contains a letter ‘z’.
      //  Return first name, last name, department, city, and state province.

      [HttpGet("Get_Employees_FirstName_Contains_Z")]
      public async Task<IActionResult> GetData19()
      {
            var y = await _context.employees.Where(w => w.salary <
                        (_context.employees.Where(w => w.EmployeeID == 182).Select(s => s.salary).Average()))
                        .Select(se => new
                        {
                              FirstName = se.First_Name,
                              LastName = se.Last_Name,
                              Salary = se.salary
                        }).ToListAsync();
            return Ok(y);
      }

      // From the following table, write a SQL query to find those employees who earn less 
      // than the employee of ID 182. Return first name, last name and salary.
      [HttpGet("Get_Employees_Salary_Less_Than_182")]
      public async Task<IActionResult> GetData21()
      {
            var y = await _context.employees.Where(w => w.salary <
                        (_context.employees.Where(w => w.EmployeeID == 182)
                        .Select(s => s.salary).Average()))
                        .Select(se => new
                        {
                              FirstName = se.First_Name,
                              LastName = se.Last_Name,
                              Salary = se.salary
                        }).ToListAsync();
            return Ok(y);
      }

      // From the following table, write a SQL query to find the employees 
      // and their managers. Return the first name of the employee and manager.
      [HttpGet("Get_Employees_With_Manager_Name")]
      public async Task<IActionResult> GetData22()
      {
            // Return Employee & Manager Name
            var x = await _context.employees.Join(
                        _context.employees,
                        e => e.Manager_ID,
                        m => m.Manager_ID,
                        (e, m) =>
                        new
                        {
                              FirstName = e.First_Name,
                              ManagerName = m.First_Name
                        }
            ).ToListAsync();

            // Return Employees Under a Manager
            var y = await _context.employees.Join(
                        _context.employees,
                        e => e.Manager_ID,
                        m => m.EmployeeID,
                        (e, m) => new { e, m }
            ).GroupBy(g => g.m.First_Name).Select(s => new
            {
                  Manager = s.Key,
                  employee = s.Select(k => k.e.First_Name)
            }).ToListAsync();
            return Ok(y);
      }


      // From the following tables, write a SQL query to find those employees who 
      // joined between 1st January 1993 and 31 August 1997. Return job title, department name,
      // employee name, and joining date of the job.

      [HttpGet("Get_Employees_Joined_Between")]
      public async Task<IActionResult> GetData23()
      {
            var y = await (from j in _context.job_history
                           join e in _context.employees on j.Employee_ID equals e.EmployeeID
                           join d in _context.departments on e.Department_ID equals d.Department_ID
                           join b in _context.jobs on j.Job_Id equals b.Job_ID
                           select new { j, e, d, b })
                           .Where(w => w.j.Start_Date >= new DateTime(1993, 01, 01)
                                    && w.j.Start_Date <= new DateTime(1997, 08, 31))
                           .Select(s => new
                           {
                                 JobTitle = s.b.Job_Title,
                                 DepartmentName = s.d.Department_Name,
                                 EmployeeName = s.e.First_Name,
                                 JoinDate = s.j.Start_Date
                           }).ToListAsync();

            return Ok(y);
      }

      // From the following tables, write a SQL query to find the difference between 
      // maximum salary of the job and salary of the employees.Return job title,
      //  employee name, and salary difference

      [HttpGet("Get_Employees_Joined_Salary_Diff_From MaxSalary")]
      public async Task<IActionResult> GetData24()
      {
            var y = await (from e in _context.employees
                           join j in _context.jobs on e.Job_Id equals j.Job_ID
                           select new { e, j })
                           .Select(s => new
                           {
                                 JobTitle = s.j.Job_Title,
                                 EmployeeName = s.e.First_Name,
                                 SalaryDifference = s.j.Max_Salary - s.e.salary
                           }).ToListAsync();

            return Ok(y);
      }

      // From the following table, write a SQL query to compute the average salary, 
      // number of employees received commission in that department.Return department name, 
      // average salary and number of employees.

      [HttpGet("Get_Employees_Average_Salary_By_Department")]
      public async Task<IActionResult> GetData25()
      {
            var y = await (from e in _context.employees
                           join d in _context.departments on e.Department_ID equals d.Department_ID
                           select new { e, d })
                           .GroupBy(g => g.d.Department_Name)
                           .Select(s => new
                           {
                                 DepartmentName = s.Key,
                                 Average = s.Select(t => t.e.salary).Average(),
                                 Count = s.Select(t => t.e.Department_ID).Count()
                           }).ToListAsync();

            return Ok(y);
      }

      // From the following table, write a SQL query to find the name of the country,
      // city, and departments, which are running there.
      [HttpGet("Get_Departments_By_Countries")]
      public async Task<IActionResult> GetData26()
      {
            var x = await (from c in _context.country
                           join l in _context.locations on c.Country_Id equals l.Country_Id
                           join d in _context.departments on l.Location_Id equals d.Location_ID
                           select new
                           {
                                 Country = c.Country_Name,
                                 City = l.City,
                                 Department = d.Department_Name
                           }).ToListAsync();

            return Ok(x);
      }

      // From the following tables, write a SQL query to find the department
      // name and the full name(first and last name) of the manager.

      [HttpGet("Get_Department_Managers")]
      public async Task<IActionResult> GetData27()
      {
            var x = await (
                  from d in _context.departments
                  join e in _context.employees on d.Manager_ID equals e.EmployeeID
                  select new
                  {
                        Department = d.Department_Name,
                        Manager = e.First_Name
                  }
            ).ToListAsync();
            return Ok(x);
      }

      // From the following table, write a SQL query to compute the
      //  average salary of employees for each job title

      [HttpGet("Get_Department_Salary_Average")]
      public async Task<IActionResult> GetData28()
      {
            var x = await
            (
                  from e in _context.employees
                  join j in _context.jobs on e.Job_Id equals j.Job_ID
                  select new { e, j }
            )
            .GroupBy(g => g.j.Job_Title)
            .Select(s => new
            {
                  JobTitle = s.Key,
                  AverageSalary = s.Select(u => u.e.salary).Average()
            })
            .ToListAsync();
            return Ok(x);
      }

      // From the following tables, write a SQL query to find those departments where at least 2 employees work.
      // Group the result set on country name and city. Return country name, city, and number of departments.

      [HttpGet("Get_Department_GT2_Employees")]
      public async Task<IActionResult> GetData29()
      {

            var y = _context.employees
                  .GroupBy(g => g.Department_ID)
                  .Where(w => w.Select(s => s.EmployeeID).Count() >= 2)
                  .Select(s => new { Dept = s.Key, Local = s.Select(q => q.Department.Location_ID) });

            var x = await (
                  from c in _context.country
                  join l in _context.locations on c.Country_Id equals l.Country_Id
                  join d in _context.departments on l.Location_Id equals d.Location_ID
                  from e in y
                  where e.Dept == (d.Department_ID)
                  select new
                  {
                        CountryName = c.Country_Name,
                        City = l.City,
                        // DeptCount = (y.Select(g => g.Local.GroupBy(w => w.Local)).Select(s => s.))
                        // Cant Find Count ..
                  })
            .ToListAsync();


            return Ok(x);
      }


      // [HttpGet("Get_Department_>=2Employees")]
      // public async Task<IActionResult> GetData29()
      // {
      //       var x = await (
      //       ).ToListAsync();
      //       return Ok(x);
      // }

      // [HttpGet("Get_Department_>=2Employees")]
      // public async Task<IActionResult> GetData29()
      // {
      //       var x = await (
      //       ).ToListAsync();
      //       return Ok(x);
      // }
}
