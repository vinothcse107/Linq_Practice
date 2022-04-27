using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apitest.Controllers
{
      [Route("[controller]")]
      public class LinqJoin : Controller
      {
            public DbxContext _context { get; }

            public LinqJoin(DbxContext context)
            {
                  _context = context;

            }

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

            [HttpGet("Get_Employees_Join_JobGrades")]
            public async Task<IActionResult> GetData17()
            {
                  var x = await (from e in _context.employees
                                 select new
                                 {
                                       FirstName = e.First_Name,
                                       LastName = e.Last_Name,
                                       Salary = e.salary,
                                       //    Grades = _context.job_grades.Select(
                                       //          s => (e.salary >= s.Lowest_Salary && e.salary <= s.Highest_Salary)
                                       //    )
                                 }).ToListAsync();
                  return Ok(x);
            }


      }
}
