using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TestSync.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TestSync.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationContext db;

        public EmployeesController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            List<Employee> list = await db.Employees.AsQueryable().ToListAsync();
            return new JsonResult(new { result = list, count = list.Count });
        }


        [HttpPost("insert")]
        public async Task<ActionResult<Employee>> Insert(Employee value)
        {
            Employee employee = new Employee()
            {
                Name = value.Name,
            };

            var result = await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
            return new JsonResult(result.Entity);
        }

        [HttpPut("update({id})")]
        public async Task<ActionResult<Employee>> Update(Employee value)
        {
            Employee employee = await db.Employees.FindAsync(value.ID);
            employee.Name = value.Name;
            db.Employees.Update(employee);
            db.Attach(employee).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return new JsonResult(employee);
        }

        [HttpDelete("delete({id})")]
        public async Task<ActionResult> Delete(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return  Ok();
        }
    }
}