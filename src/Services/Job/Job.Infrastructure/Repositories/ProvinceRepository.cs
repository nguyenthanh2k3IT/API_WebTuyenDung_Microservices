namespace Job.Infrastructure.Repositories;

public class ProvinceRepository : GenericRepository<Province, string>, IProvinceRepository
{
    public ProvinceRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
