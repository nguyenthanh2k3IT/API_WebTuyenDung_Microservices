namespace Job.Infrastructure.Repositories;

public class GenderRepository : GenericRepository<Gender, Guid>, IGenderRepository
{
    public GenderRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
