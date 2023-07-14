using Microsoft.EntityFrameworkCore;
using Volunteer_API.Model;

namespace Volunteer_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Event> Events =>Set<Event>();

        
    }
}