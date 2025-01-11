using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class RankRepository : GenericRepository<Rank, Guid>, IRankRepository
{
    public RankRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
