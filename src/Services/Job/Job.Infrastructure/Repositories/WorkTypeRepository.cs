using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class WorkTypeRepository : GenericRepository<WorkType, Guid>, IWorkTypeRepository
{
    public WorkTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}