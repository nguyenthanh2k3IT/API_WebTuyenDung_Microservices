using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class PopularRepository : GenericRepository<Popular, Guid>, IPopularRepository
{
    public PopularRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<PopularDto> CreateAsync(Popular request)
    {
        await IsSlugUnique(request.Slug, true);
        request.Id = Guid.NewGuid();
        Add(request, request.CreatedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<PopularDto>(request);
    }

    public async Task<PopularDto> UpdateAsync(Popular request)
    {
        var wPopular = await FindAsync(request.Id, true);
        var isUsed = await _context.Populars
                            .Where(s => s.Id != request.Id && s.Slug == request.Slug)
                            .CountAsync();
        if (isUsed > 0)
        {
            throw new ApplicationException($"Mã '{request.Slug}' đã được sử dụng.");
        }
        wPopular!.Slug = request.Slug;
        wPopular!.Name = request.Name;
        Update(wPopular!, request.ModifiedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<PopularDto>(wPopular);
    }
}
