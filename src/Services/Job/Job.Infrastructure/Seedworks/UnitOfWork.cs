using AutoMapper;

namespace Job.Infrastructure.Seedworks;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
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

    public UnitOfWork(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        Genders = new GenderRepository(_context, _mapper);
        Categories = new CategoryRepository(_context, _mapper);
        Applicants = new ApplicantRepository(_context, _mapper);
        ApplicantStatuses = new ApplicantStatusRepository(_context, _mapper);
        Experience = new ExperienceRepository(_context, _mapper);
        Jobs = new JobRepository(_context, _mapper);
        Populars = new PopularRepository(_context, _mapper);
        Provinces = new ProvinceRepository(_context, _mapper);
        Ranks = new RankRepository(_context, _mapper);
        Companies = new RepCompanyRepository(_context, _mapper);
        WorkTypes = new WorkTypeRepository(_context, _mapper);
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