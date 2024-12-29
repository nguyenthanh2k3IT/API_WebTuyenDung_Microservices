using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Blog.API.Data;

public class DataContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostSave> PostSaves => Set<PostSave>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<TagName> TagNames => Set<TagName>();
    public DbSet<Status> Statuses => Set<Status>();
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}