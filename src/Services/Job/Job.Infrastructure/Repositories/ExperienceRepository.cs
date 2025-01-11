namespace Job.Infrastructure.Repositories;

public class ExperienceRepository : GenericRepository<Experience, Guid>, IExperienceRepository
{
    public ExperienceRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
