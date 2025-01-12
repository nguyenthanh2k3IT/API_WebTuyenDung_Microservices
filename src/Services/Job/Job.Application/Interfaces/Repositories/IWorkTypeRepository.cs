namespace Job.Application.Interfaces.Repositories;

public interface IWorkTypeRepository : IGenericRepository<WorkType, Guid>
{
    Task<WorkTypeDto> CreateAsync(WorkType request);
    Task<WorkTypeDto> UpdateAsync(WorkType request);
}
