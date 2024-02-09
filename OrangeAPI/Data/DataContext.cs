using Microsoft.EntityFrameworkCore;
using OrangeAPI.Entities;

namespace OrangeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options): base(options)
        {
            
        }

        public DbSet<Bank> Banks { get; set; }
    }
}
