using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class ApplicantStatusRepository 
    : GenericRepository<ApplicantStatus, ApplicantStatusEnum>
    , IApplicantStatusRepository
{
    public ApplicantStatusRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }
}
