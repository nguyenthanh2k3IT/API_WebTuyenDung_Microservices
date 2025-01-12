namespace Job.Application.Interfaces.Repositories;

public interface IPopularRepository : IGenericRepository<Popular, Guid>
{
    Task<PopularDto> CreateAsync(Popular request);
    Task<PopularDto> UpdateAsync(Popular request);
}
