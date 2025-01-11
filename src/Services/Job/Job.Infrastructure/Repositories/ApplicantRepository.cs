namespace Job.Infrastructure.Repositories;

public class ApplicantRepository : GenericRepository<Applicant, Guid>, IApplicantRepository
{
    public ApplicantRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
