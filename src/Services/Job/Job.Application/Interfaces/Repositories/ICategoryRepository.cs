using Job.Application.Dtos;

namespace Job.Application.Interfaces.Repositories;

public interface ICategoryRepository : IGenericRepository<Category, Guid>
{
    Task<CategoryDto> CreateAsync(Category category);
    Task<CategoryDto> UpdateAsync(Category category);
}
