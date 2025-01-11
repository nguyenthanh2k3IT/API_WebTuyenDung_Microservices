using EJob = Job.Domain.Entities.Job;

namespace Job.Infrastructure.Repositories;

public class JobRepository : GenericRepository<EJob, Guid>, IJobRepository
{
    public JobRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
