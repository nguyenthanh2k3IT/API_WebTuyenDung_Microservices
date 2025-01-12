namespace Job.Application.Interfaces.Repositories;

public interface IRankRepository : IGenericRepository<Rank, Guid>
{
    Task<RankDto> CreateAsync(Rank request);
    Task<RankDto> UpdateAsync(Rank request);
}
