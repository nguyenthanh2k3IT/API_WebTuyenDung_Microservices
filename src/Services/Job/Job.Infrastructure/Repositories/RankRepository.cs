namespace Job.Infrastructure.Repositories;

public class RankRepository : GenericRepository<Rank, Guid>, IRankRepository
{
    public RankRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
