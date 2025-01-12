namespace Job.Application.Interfaces.Repositories;

public interface IExperienceRepository : IGenericRepository<Experience, Guid>
{
    Task<ExperienceDto> CreateAsync(Experience experience);
    Task<ExperienceDto> UpdateAsync(Experience experience);
}
