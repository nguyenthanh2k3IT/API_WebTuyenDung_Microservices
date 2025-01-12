using AutoMapper;

namespace Job.Infrastructure.Repositories;

public class WorkTypeRepository : GenericRepository<WorkType, Guid>, IWorkTypeRepository
{
    public WorkTypeRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<WorkTypeDto> CreateAsync(WorkType request)
    {
        await IsSlugUnique(request.Slug, true);
        request.Id = Guid.NewGuid();
        Add(request, request.CreatedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<WorkTypeDto>(request);
    }

    public async Task<WorkTypeDto> UpdateAsync(WorkType request)
    {
        var wWorkType = await FindAsync(request.Id, true);
        var isUsed = await _context.WorkTypes
                            .Where(s => s.Id != request.Id && s.Slug == request.Slug)
                            .CountAsync();
        if (isUsed > 0)
        {
            throw new ApplicationException($"Mã '{request.Slug}' đã được sử dụng.");
        }
        wWorkType!.Slug = request.Slug;
        wWorkType!.Name = request.Name;
        Update(wWorkType!, request.ModifiedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<WorkTypeDto>(wWorkType);
    }
}