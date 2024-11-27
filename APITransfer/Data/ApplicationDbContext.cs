using Microsoft.EntityFrameworkCore;
using APITransfer.Models;
using System.Collections.Generic;

namespace APITransfer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
