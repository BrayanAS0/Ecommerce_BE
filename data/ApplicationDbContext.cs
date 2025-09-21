using Microsoft.EntityFrameworkCore;

namespace Ecommerce_BE.data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
