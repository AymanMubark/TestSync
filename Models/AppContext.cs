using Microsoft.EntityFrameworkCore;

namespace TestSync.Models {
    public class ApplicationContext : DbContext {
        public ApplicationContext (DbContextOptions<ApplicationContext> options) : base (options) {

         }
    
        public DbSet<Employee> Employees { get; set; }
    }
}