using Job.Application.Interfaces.Repositories;

namespace Job.Application.Interfaces.Seedworks;

public interface IUnitOfWork
{
    IApplicantRepository Applicants { get; }
    IApplicantStatusRepository ApplicantStatuses { get; }
    IExperienceRepository Experience { get; }
    ICategoryRepository Categories { get; }
    IGenderRepository Genders { get; }
    IJobRepository Jobs { get; }
    IPopularRepository Populars { get; }
    IProvinceRepository Provinces { get; }
    IRankRepository Ranks { get; }
    IRepCompanyRepository Companies { get; }
    IWorkTypeRepository WorkTypes { get; }
    Task RemoveCacheAsync(string key);
    Task<int> CompleteAsync();
    void Dispose();
}
