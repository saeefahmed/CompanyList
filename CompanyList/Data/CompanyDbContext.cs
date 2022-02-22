using CompanyList.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyList.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
    }
}
