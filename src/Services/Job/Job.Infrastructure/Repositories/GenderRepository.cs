using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class GenderRepository : GenericRepository<Gender, Guid>, IGenderRepository
{
    public GenderRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<GenderDto> CreateAsync(Gender request)
    {
        await IsSlugUnique(request.Slug, true);
        request.Id = Guid.NewGuid();
        Add(request, request.CreatedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<GenderDto>(request);
    }

    public async Task<GenderDto> UpdateAsync(Gender request)
    {
        var wGender = await FindAsync(request.Id, true);
        var isUsed = await _context.Categories
                            .Where(s => s.Id != request.Id && s.Slug == request.Slug)
                            .CountAsync();
        if (isUsed > 0)
        {
            throw new ApplicationException($"Mã '{request.Slug}' đã được sử dụng.");
        }
        wGender!.Slug = request.Slug;
        wGender!.Name = request.Name;
        Update(wGender!, request.ModifiedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<GenderDto>(wGender);
    }
}
