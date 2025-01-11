using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class GenderRepository : GenericRepository<Gender, Guid>, IGenderRepository
{
    public GenderRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
