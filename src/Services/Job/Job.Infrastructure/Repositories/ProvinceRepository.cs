using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class ProvinceRepository : GenericRepository<Province, string>, IProvinceRepository
{
    public ProvinceRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
