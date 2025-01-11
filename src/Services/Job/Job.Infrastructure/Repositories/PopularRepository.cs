namespace Job.Infrastructure.Repositories;

public class PopularRepository : GenericRepository<Popular, Guid>, IPopularRepository
{
    public PopularRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
