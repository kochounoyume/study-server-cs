using ASPDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPDemo.Server.Models;

public class DemoContext(DbContextOptions<DemoContext> options) : DbContext(options)
{
    public DbSet<DemoItem> demoItems { get; set; } = null!;
}