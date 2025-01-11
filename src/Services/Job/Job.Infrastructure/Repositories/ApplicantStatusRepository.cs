namespace Job.Infrastructure.Repositories;

public class ApplicantStatusRepository 
    : GenericRepository<ApplicantStatus, ApplicantStatusEnum>
    , IApplicantStatusRepository
{
    public ApplicantStatusRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
