using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Apitest.Controllers
{
      [Route("[controller]")]
      public class LinqJoin : Controller
      {
            public DbxContext context { get; }

            public LinqJoin(DbxContext context)
            {
                  this.context = context;
            }

            [HttpGet("Get_Employees_Joins_Department")]
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