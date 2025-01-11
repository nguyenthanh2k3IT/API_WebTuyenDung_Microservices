using BuildingBlock.Core.Paging;
using BuildingBlock.Core.Request;

namespace Job.Application.Interfaces.Seedworks;

public interface IGenericRepository<TEntity, in TKey> where TEntity : class
{
    // ================================ FUNCTIONAL QUERIES =================================
    Task<PaginatedList<TDto>> GetPaginatedList<TDto>
        (PaginationRequest request, IEnumerable<string> searchColumns = null) 
        where TDto : class;

    Task<IEnumerable<TDto>> GetAllList<TDto>
        (BaseRequest request, IEnumerable<string> searchColumns = null)
        where TDto : class;

    Task<IEnumerable<TDto>> GetFilterList<TDto>
        (FilterRequest request, IEnumerable<string> searchColumns = null)
        where TDto : class;

    Task<TDto> GetOneRecord<TDto>(TKey id) where TDto : class;

    Task<TDto> GetSlugOneRecord<TDto>(string slug) where TDto : class;

    // ===================================== QUERIES ======================================= 
    IQueryable<TEntity> Queryable();
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> FindAsync(TKey id, bool isThrow = false);
    Task<TEntity?> FindSlugAsync(string slug, bool isThrow = false);
    Task<TEntity?> IsSlugUnique(string slug, bool isThrow = false);
    Task<List<TEntity>> FindByIds(IEnumerable<TKey> ids, bool isThrow = false);

    // ===================================== COMMANDS ======================================= 
    bool Add(TEntity entity, Guid? user = null);
    bool Delete(TEntity entity, Guid? user = null);
    bool Update(TEntity entity, Guid? user = null);
    bool SoftDelete(TEntity entity, Guid? user = null);

    bool SoftDeleteRange(List<TEntity> entities, Guid? user = null);
    bool AddRange(List<TEntity> entities, Guid? user = null);
    bool DeleteRange(List<TEntity> entities, Guid? user = null);
    bool UpdateRange(List<TEntity> entities, Guid? user = null);

}