using System.Reflection;
using EJob = Job.Domain.Entities.Job;

namespace Job.Infrastructure.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Gender> Genders => Set<Gender>();
    public DbSet<Applicant> Applicants => Set<Applicant>();
    public DbSet<ApplicantStatus> ApplicantStatuses => Set<ApplicantStatus>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<EJob> Jobs => Set<EJob>();
    public DbSet<Popular> Populars => Set<Popular>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<Rank> Ranks => Set<Rank>();
    public DbSet<RepCompany> Companies => Set<RepCompany>();
    public DbSet<WorkType> WorkTypes => Set<WorkType>();

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
