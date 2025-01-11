using Job.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using EJob = Job.Domain.Entities.Job;

namespace Job.Application.Interfaces.Data;

public interface IDataContext
{
    DbSet<Applicant> Applicants { get; }
    DbSet<ApplicantStatus> ApplicantStatuses { get; }
    DbSet<Category> Categories { get; }
    DbSet<Experience> Experiences { get; }
    DbSet<Gender> Genders { get; }
    DbSet<EJob> Jobs { get; }
    DbSet<Popular> Populars { get; }
    DbSet<Province> Provinces { get; }
    DbSet<Rank> Ranks { get; }
    DbSet<RepCompany> Companies { get; }
    DbSet<WorkType> WorkTypes { get; }
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken token);
}
