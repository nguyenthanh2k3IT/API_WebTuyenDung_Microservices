using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Paging;
using BuildingBlock.Core.Request;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;

namespace Job.Infrastructure.Seedworks;

public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected DataContext _context;
    protected IMapper _mapper;
    protected DbSet<TEntity> _dbSet;

    public GenericRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _mapper = mapper;
    }

    #region =========== Common ===========
    public IQueryable<TEntity> Queryable()
    {
        IQueryable<TEntity> query = _dbSet;
        return query;
    }

    private void SetModifiedAttribute(TEntity entity, Guid? user = null)
    {
        var modifiedUser = typeof(TEntity).GetProperty("ModifiedUser");
        var modifiedDate = typeof(TEntity).GetProperty("ModifiedDate");

        if (modifiedUser is not null && user is not null)
        {
            modifiedUser.SetValue(entity, user);
        }

        if (modifiedDate is not null)
        {
            modifiedDate.SetValue(entity, DateTime.Now);
        }
    }

    private void SetCreatedAttribute(TEntity entity, Guid? user = null)
    {
        var createdDate = typeof(TEntity).GetProperty("CreatedDate");
        var createdUser = typeof(TEntity).GetProperty("CreatedUser");

        if (createdUser is not null && user is not null)
        {
            createdUser.SetValue(entity, user);
        }

        if (createdDate is not null)
        {
            createdDate.SetValue(entity, DateTime.Now);
        }
    }

    private void SetSoftDeletedAttribute(TEntity entity, Guid? user = null)
    {
        var deleteFlag = typeof(TEntity).GetProperty("DeleteFlag");
        var modifiedDate = typeof(TEntity).GetProperty("ModifiedDate");
        var modifiedUser = typeof(TEntity).GetProperty("ModifiedUser");

        if (deleteFlag is not null)
        {
            deleteFlag.SetValue(entity, true);
        }

        if (modifiedUser is not null && user is not null)
        {
            modifiedUser.SetValue(entity, user);
        }

        if (modifiedDate is not null)
        {
            modifiedDate.SetValue(entity, DateTime.Now);
        }
    }
    #endregion

    #region =========== Command ===========
    public virtual bool Add(TEntity entity, Guid? user = null)
    {
        try
        {
            SetCreatedAttribute(entity, user);
            _dbSet.Add(entity);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public virtual bool Update(TEntity entity, Guid? user = null)
    {
        try
        {
            SetModifiedAttribute(entity, user);
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public virtual bool Delete(TEntity entity, Guid? user = null)
    {
        try
        {
            _dbSet.Remove(entity);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public virtual bool SoftDelete(TEntity entity, Guid? user = null)
    {
        try
        {
            SetSoftDeletedAttribute(entity, user);
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #region =========== Command Range ===========
    public bool SoftDeleteRange(List<TEntity> entities, Guid? user = null)
    {
        try
        {
            foreach (var entity in entities)
            {
                SetSoftDeletedAttribute(entity, user);
            }
            _dbSet.UpdateRange(entities);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool AddRange(List<TEntity> entities, Guid? user = null)
    {
        try
        {
            foreach (var entity in entities)
            {
                SetCreatedAttribute(entity, user);
            }

            _dbSet.AddRange(entities);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DeleteRange(List<TEntity> entities, Guid? user = null)
    {
        try
        {
            _dbSet.RemoveRange(entities);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UpdateRange(List<TEntity> entities, Guid? user = null)
    {
        try
        {
            foreach (var entity in entities)
            {
                SetModifiedAttribute(entity, user);
            }

            _dbSet.UpdateRange(entities);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #region ======== Queries ========
    public virtual async Task<TEntity?> FindAsync(TKey id, bool isThrow = false)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null && isThrow)
        {
            throw new ApplicationException($"Không tìm thấy dữ liệu với ID: {id}");
        }
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = _dbSet;
        var data = await query.ToListAsync();
        return data;
    }

    public async Task<TEntity?> IsSlugUnique(string slug, bool isThrow = false)
    {
        var slugProperty = typeof(TEntity).GetProperty("Slug");

        if (slugProperty == null)
        {
            throw new ApplicationException($"Entity does not have a column named 'Slug'.");
        }

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, slugProperty);
        var value = Expression.Constant(slug);
        var equalExpression = Expression.Equal(property, value);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

        var entity = await _dbSet.FirstOrDefaultAsync(lambda);

        if (entity != null && isThrow)
        {
            throw new ApplicationException($"Mã '{slug}' đã được sử dụng.");
        }

        return entity;
    }

    public async Task<TEntity?> FindSlugAsync(string slug, bool isThrow = false)
    {
        var slugProperty = typeof(TEntity).GetProperty("Slug");

        if (slugProperty == null)
        {
            throw new ApplicationException($"Entity does not have a column named 'Slug'.");
        }

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, slugProperty);
        var value = Expression.Constant(slug);
        var equalExpression = Expression.Equal(property, value);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

        var entity = await _dbSet.FirstOrDefaultAsync(lambda);

        if (entity == null && isThrow)
        {
            throw new ApplicationException($"Không tìm thấy dữ liệu với mã: {slug}");
        }

        return entity;
    }

    public async Task<List<TEntity>> FindByIds(IEnumerable<TKey> ids, bool isThrow = false)
    {
        var idProperty = typeof(TEntity).GetProperty("Id");

        if (idProperty == null)
        {
            throw new ApplicationException($"Entity does not have a column named 'Id'.");
        }

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, idProperty);
        var containsMethod = typeof(Enumerable).GetMethods()
            .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(TKey));

        var idsConstant = Expression.Constant(ids);
        var containsExpression = Expression.Call(containsMethod, idsConstant, property);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);
        var entities = await _dbSet.Where(lambda).ToListAsync();

        if (!entities.Any() && isThrow)
        {
            throw new ApplicationException($"Không tìm thấy dữ liệu với ID: {string.Join(";", ids)}");
        }

        return entities;
    }
    #endregion

    #region Functional Queries
    public async Task<PaginatedList<TDto>> GetPaginatedList<TDto>
        (PaginationRequest request, IEnumerable<string> searchColumns = null)
        where TDto : class
    {
        var orderCol = request.OrderCol;
        var orderDir = request.OrderDir;

        var query = _dbSet.OrderedListQuery(orderCol, orderDir)
                          .AsNoTracking();

        if (!string.IsNullOrEmpty(request.TextSearch) && searchColumns != null)
        {
            query = ApplySearchFilter(query, request.TextSearch, searchColumns);
        }

        var count = await query.CountAsync();
        var items = await query.ProjectTo<TDto>(_mapper.ConfigurationProvider)
                               .Skip((request.PageIndex - 1) * request.PageSize)
                               .Take(request.PageSize)
                               .ToListAsync();

        return new PaginatedList<TDto>(items, count, request.PageIndex, request.PageSize);
    }

    public async Task<IEnumerable<TDto>> GetAllList<TDto>
        (BaseRequest request, IEnumerable<string> searchColumns = null)
        where TDto : class
    {
        var orderCol = request.OrderCol;
        var orderDir = request.OrderDir;

        var query = _dbSet.OrderedListQuery(orderCol, orderDir)
                          .AsNoTracking();

        if (!string.IsNullOrEmpty(request.TextSearch) && searchColumns != null)
        {
            query = ApplySearchFilter(query, request.TextSearch, searchColumns);
        }

        return await query.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<IEnumerable<TDto>> GetFilterList<TDto>
        (FilterRequest request, IEnumerable<string> searchColumns = null)
        where TDto : class
    {
        var orderCol = request.OrderCol;
        var orderDir = request.OrderDir;

        var query = _dbSet.OrderedListQuery(orderCol, orderDir)
                          .AsNoTracking();

        if (!string.IsNullOrEmpty(request.TextSearch) && searchColumns != null)
        {
            query = ApplySearchFilter(query, request.TextSearch, searchColumns);
        }

        if (request.Skip != null)
        {
            query = query.Skip(request.Skip.Value);
        }

        if (request.TotalRecord != null)
        {
            query = query.Take(request.TotalRecord.Value);
        }

        return await query.ProjectTo<TDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<TDto> GetOneRecord<TDto>(TKey id) where TDto : class
    {
        var idProperty = typeof(TEntity).GetProperty("Id");

        if (idProperty == null)
        {
            throw new ApplicationException($"Entity does not have a column named 'Id'.");
        }

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, idProperty);
        var value = Expression.Constant(id);
        var equalExpression = Expression.Equal(property, value);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

        var entity = await _dbSet.Where(lambda)
                                 .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync();

        return entity;
    }

    public async Task<TDto> GetSlugOneRecord<TDto>(string slug) where TDto : class
    {
        var idProperty = typeof(TEntity).GetProperty("Slug");

        if (idProperty == null)
        {
            throw new ApplicationException($"Entity does not have a column named 'Slug'.");
        }

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, idProperty);
        var value = Expression.Constant(slug);
        var equalExpression = Expression.Equal(property, value);

        var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

        var entity = await _dbSet.Where(lambda)
                                 .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync();

        return entity;
    }

    public async Task<bool> DeleteRecords(List<TKey> ids,Guid? userId = null)
    {
        // Lấy thuộc tính Id của thực thể
        var idProperty = typeof(TEntity).GetProperty("Id");
        if (idProperty == null)
        {
            throw new ApplicationException($"Entity '{typeof(TEntity).Name}' does not have a property named 'Id'.");
        }

        // Tạo điều kiện: e => ids.Contains(e.Id)
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, idProperty);

        // Tạo giá trị danh sách Ids dưới dạng Expression
        var idsExpression = Expression.Constant(ids);

        // Tạo phương thức Contains
        var containsMethod = typeof(List<TKey>).GetMethod("Contains", new[] { typeof(TKey) });
        if (containsMethod == null)
        {
            throw new ApplicationException("Could not find 'Contains' method on List<TKey>.");
        }

        // Tạo biểu thức: ids.Contains(e.Id)
        var containsExpression = Expression.Call(idsExpression, containsMethod, property);

        // Tạo lambda: e => ids.Contains(e.Id)
        var lambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);

        // Lấy các bản ghi thỏa điều kiện
        var entities = await _dbSet.Where(lambda).ToListAsync();

        if (entities == null || entities.Count == 0)
        {
            throw new ApplicationException($"Không tìm thấy dữ liệu với các Id: {string.Join(";", ids)}");
        }

        // Đánh dấu các bản ghi là đã xóa
        foreach (var entity in entities)
        {
            SetSoftDeletedAttribute(entity, userId);
        }

        _dbSet.UpdateRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion

    private IQueryable<TEntity> ApplySearchFilter(
        IQueryable<TEntity> query,
        string textSearch,
        IEnumerable<string> searchColumns)
    {
        var predicate = string.Join(" || ", searchColumns.Select(column => $"{column}.Contains(@0)"));
        return query.Where(predicate, textSearch);
    }
}
