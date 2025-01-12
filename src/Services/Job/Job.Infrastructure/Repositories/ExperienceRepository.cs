using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class ExperienceRepository : GenericRepository<Experience, Guid>, IExperienceRepository
{
    public ExperienceRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<ExperienceDto> CreateAsync(Experience experience)
    {
        await IsSlugUnique(experience.Slug, true);
        experience.Id = Guid.NewGuid();
        Add(experience, experience.CreatedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<ExperienceDto>(experience);
    }

    public async Task<ExperienceDto> UpdateAsync(Experience experience)
    {
        var wExperience = await FindAsync(experience.Id, true);
        var isUsed = await _context.Categories
                            .Where(s => s.Id != experience.Id && s.Slug == experience.Slug)
                            .CountAsync();
        if (isUsed > 0)
        {
            throw new ApplicationException($"Mã '{experience.Slug}' đã được sử dụng.");
        }
        wExperience!.Slug = experience.Slug;
        wExperience!.Name = experience.Name;
        Update(wExperience!, experience.ModifiedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<ExperienceDto>(wExperience);
    }
}
