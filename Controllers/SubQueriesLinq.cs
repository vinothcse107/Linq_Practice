using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Apitest.Controllers
{
      [Route("[controller]")]
      public class SubQueriesLinq : Controller
      {

            public DbxContext _context { get; }

            public SubQueriesLinq(DbxContext context)
            {
                  _context = context;
            }

            // From the following table, write a SQL query to find those employees
            // who get higher salary than the employee whose ID is 163. Return first name, last name.

            [HttpGet("Get_Employees_Salary_GT_ID_163")]
            public async Task<IActionResult> GetData29()
            {
                  var y = await (_context.employees.Where(w => w.EmployeeID == 163).Select(s => s.salary).ToListAsync());
                  var x = (
                        from q in y
                        from e in _context.employees.Where(w => w.salary > y.Average())
                        select new
                        {
                              FirstName = e.First_Name,
                              Lastname = e.Last_Name,
                              Salary = e.salary
                        }).ToList();
                  return Ok(x);
            }


            // [HttpGet("Get_Department_>=2Employees")]
            // public async Task<IActionResult> GetData29()
            // {
            //       var x = await (
            //       ).ToListAsync();
            //       return Ok(x);
            // }      // [HttpGet("Get_Department_>=2Employees")]
            // public async Task<IActionResult> GetData29()
            // {
            //       var x = await (
            //       ).ToListAsync();
            //       return Ok(x);
            // }

      }
}