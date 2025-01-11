using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class RepCompanyRepository : GenericRepository<RepCompany, Guid>, IRepCompanyRepository
{
    public RepCompanyRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
