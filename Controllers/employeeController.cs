using Microsoft.EntityFrameworkCore;

namespace Apitest.Controllers;
public class employeeController : ControllerBase
{
      private DbxContext _context { get; set; }
      public employeeController(DbxContext context)
      {
            _context = context;
      }

      [HttpGet("GetDetails")]
      public IActionResult GetEmployees()
      {
            var x = _context.employees?.ToList();
            int xe = 5;
            var xr = xe switch
            {
                  _ when xe > 5 => "Greater",
                  _ when xe < 5 => "Lesser",
                  _ => "Equal"
            };
            return new OkObjectResult(x);

      }

      [HttpGet("RawSQL")]
      public IActionResult RawEmployees()
      {
            var x = _context.employees.FromSqlRaw("SELECT * FROM dbo.employees ;").ToList();
            return Ok(x);
      }

      [HttpGet("ProcedureSQL")]
      public IActionResult ExecProcedure()
      {
            // var x = _context.employees.FromSqlRaw("EXEC EmpJoinDept;").ToList();
            var x = (from e in _context.employees
                     join d in _context.departments
                     on e.Department_ID equals d.Department_ID
                     select new { e, d }).GroupBy(g => g.d.Department_ID)
                        .Select(s => new
                        {
                              Dept = s.Key,
                              Emp = s.Select(o =>
                                    o.e)
                        }).ToList();
            return Ok(x);
      }

}
