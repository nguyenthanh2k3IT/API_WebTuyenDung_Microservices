using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.CategoryFeature.Queries
{
    public record Categories_GetFillterQuery(FilterRequest RequestData):IQuery<Result<IEnumerable<CategoryDto>>>;
    public class Categories_GetFillterQueryHandler : IQueryHandler<Categories_GetFillterQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Categories_GetFillterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(Categories_GetFillterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Categories.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Slug.Contains(request.RequestData.TextSearch) ||
                                         s.Name.Contains(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<CategoryDto>>.Success(await query.ToListAsync());
        }
    }

  
}
