namespace Job.Infrastructure.Seedworks;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;

    public IGenderRepository Genders { get; private set; }
    public ICategoryRepository Categories { get; private set; }
    public IApplicantRepository Applicants { get; private set; }
    public IApplicantStatusRepository ApplicantStatuses { get; private set; }
    public IExperienceRepository Experience { get; private set; }
    public IJobRepository Jobs { get; private set; }
    public IPopularRepository Populars { get; private set; }
    public IProvinceRepository Provinces { get; private set; }
    public IRankRepository Ranks { get; private set; }
    public IRepCompanyRepository Companies { get; private set; }
    public IWorkTypeRepository WorkTypes { get; private set; }

    public UnitOfWork(DataContext context)
    {
        _context = context;
        Genders = new GenderRepository(_context);
        Categories = new CategoryRepository(_context);
        Applicants = new ApplicantRepository(_context);
        ApplicantStatuses = new ApplicantStatusRepository(_context);
        Experience = new ExperienceRepository(_context);
        Jobs = new JobRepository(_context);
        Populars = new PopularRepository(_context);
        Provinces = new ProvinceRepository(_context);
        Ranks = new RankRepository(_context);
        Companies = new RepCompanyRepository(_context);
        WorkTypes = new WorkTypeRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public Task RemoveCacheAsync(string key)
    {
        try
        {
            // await _cache.RemoveCacheResponseAsync(key);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RemoveCacheAsync] - failed - error: {ex.Message}");
            return Task.FromException(ex); // Trả về Task lỗi khi có ngoại lệ
        }
    }
}