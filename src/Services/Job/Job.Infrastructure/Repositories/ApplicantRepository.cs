using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class ApplicantRepository : GenericRepository<Applicant, Guid>, IApplicantRepository
{
    public ApplicantRepository(DataContext context,IMapper mapper) : base(context,mapper)
    {
        _context = context;
    }
}
