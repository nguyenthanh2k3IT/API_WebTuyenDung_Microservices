namespace Job.Application.Interfaces.Repositories;

public interface IGenderRepository : IGenericRepository<Gender, Guid>
{
    Task<GenderDto> CreateAsync(Gender request);
    Task<GenderDto> UpdateAsync(Gender request);
}
