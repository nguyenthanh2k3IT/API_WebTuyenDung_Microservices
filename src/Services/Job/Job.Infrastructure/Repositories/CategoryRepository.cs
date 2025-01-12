using AutoMapper;
using Job.Application.Dtos;

namespace Job.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(DataContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
    }

    public async Task<CategoryDto> CreateAsync(Category category)
    {
        await IsSlugUnique(category.Slug,true);
        category.Id = Guid.NewGuid();
        Add(category,category.CreatedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(Category category)
    {
        var wCategory = await FindAsync(category.Id, true);
        var isUsed = await _context.Categories
                            .Where(s => s.Id != category.Id && s.Slug == category.Slug)
                            .CountAsync();
        if(isUsed > 0)
        {
            throw new ApplicationException($"Mã '{category.Slug}' đã được sử dụng.");
        }
        wCategory!.Slug = category.Slug;
        wCategory!.Name = category.Name;
        Update(wCategory!,category.ModifiedUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDto>(wCategory);
    }
}
