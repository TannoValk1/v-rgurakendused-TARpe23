using Microsoft.EntityFrameworkCore;
using Restap.Models;

namespace Restap.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Attendee> Attendees { get; set; }
    }
}
