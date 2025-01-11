using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class ExperienceRepository : GenericRepository<Experience, Guid>, IExperienceRepository
{
    public ExperienceRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
