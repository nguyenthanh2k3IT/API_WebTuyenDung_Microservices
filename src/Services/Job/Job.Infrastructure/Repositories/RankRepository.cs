using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class RankRepository : GenericRepository<Rank, Guid>, IRankRepository
{
    public RankRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<RankDto> CreateAsync(Rank request)
    {
        await IsSlugUnique(request.Slug, true);
        request.Id = Guid.NewGuid();
        Add(request, request.CreatedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<RankDto>(request);
    }

    public async Task<RankDto> UpdateAsync(Rank request)
    {
        var wRank = await FindAsync(request.Id, true);
        var isUsed = await _context.Ranks
                            .Where(s => s.Id != request.Id && s.Slug == request.Slug)
                            .CountAsync();
        if (isUsed > 0)
        {
            throw new ApplicationException($"Mã '{request.Slug}' đã được sử dụng.");
        }
        wRank!.Slug = request.Slug;
        wRank!.Name = request.Name;
        Update(wRank!, request.ModifiedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<RankDto>(wRank);
    }
}
