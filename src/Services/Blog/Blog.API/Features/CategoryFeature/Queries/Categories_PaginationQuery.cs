using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Paging;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using BuildingBlock.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.CategoryFeature.Queries
{
    public record Categories_PaginationQuery(PaginationRequest RequestData):IQuery<Result<PaginatedList<CategoryDto>>>;
    public class Categories_PaginationQueryHandler : IQueryHandler<Categories_PaginationQuery, Result<PaginatedList<CategoryDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
		public Categories_PaginationQueryHandler(IMapper mapper, DataContext context)
		{
			_context = context;
			_mapper = mapper;
		}
        public async Task<Result<PaginatedList<CategoryDto>>> Handle(Categories_PaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Categories
                                .OrderedListQuery(orderCol, orderDir)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch) ||
                                         s.Slug.Contains(request.RequestData.TextSearch));
            }

            var paging = await query.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                    .PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);

            return Result<PaginatedList<CategoryDto>>.Success(paging);
        }
    }

   
}
