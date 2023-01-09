using CyberBlog.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CyberBlog.DbContexts;
public class CyberDbContext : DbContext
{
    public CyberDbContext()
    {

    }
    public CyberDbContext(DbContextOptions<CyberDbContext> options) : base(options)
    {
    }

    public DbSet<Article> Article { get; set; }
    public DbSet<Message> Message { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}

