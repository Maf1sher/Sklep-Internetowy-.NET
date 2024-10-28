using Microsoft.EntityFrameworkCore;

namespace test_do_projektu.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        
    }
}
