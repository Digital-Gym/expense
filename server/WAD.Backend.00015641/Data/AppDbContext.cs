using Microsoft.EntityFrameworkCore;
using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses {  get; set; }
}
