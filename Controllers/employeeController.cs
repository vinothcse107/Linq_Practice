namespace Apitest.Controllers;
public class employeeController
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
            return new OkObjectResult(x);
      }

      [HttpPost("PutEmployees")]
      public IActionResult PostEmployees([FromBody] employee e)
      {
            return new OkObjectResult(' ');
      }
}
