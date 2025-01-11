using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class PopularRepository : GenericRepository<Popular, Guid>, IPopularRepository
{
    public PopularRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
