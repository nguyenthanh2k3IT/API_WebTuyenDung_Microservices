namespace Job.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
