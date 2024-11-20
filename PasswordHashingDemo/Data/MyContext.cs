using Microsoft.EntityFrameworkCore;
using PasswordHashingDemo.Models;

namespace PasswordHashingDemo.Data
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
    }

}
