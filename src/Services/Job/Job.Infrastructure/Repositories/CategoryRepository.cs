using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
