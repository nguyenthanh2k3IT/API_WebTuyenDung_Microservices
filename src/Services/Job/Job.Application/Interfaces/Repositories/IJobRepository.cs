using EJob = Job.Domain.Entities.Job;

namespace Job.Application.Interfaces.Repositories;

public interface IJobRepository : IGenericRepository<EJob, Guid>
{
}
