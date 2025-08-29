
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class ApiDbContext: DbContext
{


    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }        
    public DbSet<User> Users { get; set; }
}


