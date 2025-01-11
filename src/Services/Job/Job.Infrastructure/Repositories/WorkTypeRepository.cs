namespace Job.Infrastructure.Repositories;

public class WorkTypeRepository : GenericRepository<WorkType, Guid>, IWorkTypeRepository
{
    public WorkTypeRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
