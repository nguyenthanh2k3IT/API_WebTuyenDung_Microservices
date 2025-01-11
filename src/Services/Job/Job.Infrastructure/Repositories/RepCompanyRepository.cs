namespace Job.Infrastructure.Repositories;

public class RepCompanyRepository : GenericRepository<RepCompany, Guid>, IRepCompanyRepository
{
    public RepCompanyRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
