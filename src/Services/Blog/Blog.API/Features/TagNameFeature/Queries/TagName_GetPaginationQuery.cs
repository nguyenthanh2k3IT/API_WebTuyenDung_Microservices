using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.TagNameFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Paging;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.TagNameFeature.Queries
{
    
    public record TagName_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<TagNameDto>>>;
    public class TagName_GetPaginationQueryHandler : IQueryHandler<TagName_GetPaginationQuery, Result<PaginatedList<TagNameDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TagName_GetPaginationQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PaginatedList<TagNameDto>>> Handle(TagName_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.TagNames
                                .OrderedListQuery(orderCol, orderDir)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s =>
              s.Slug.Contains(request.RequestData.TextSearch) || s.Name.Contains(request.RequestData.TextSearch));
            }



            var paging = await query.ProjectTo<TagNameDto>(_mapper.ConfigurationProvider)
                                    .PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);

            return Result<PaginatedList<TagNameDto>>.Success(paging);
        }
    }
}
