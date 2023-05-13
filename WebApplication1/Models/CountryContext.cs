using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class CountryContext :DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options): base(options)  
        {

        }
        public DbSet<CountryItems> CountryItems { get; set; } = null;
    }
}
