using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Features.StatusFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Paging;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.StatusFeature.Queries
{
    
    public record Status_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<StatusDto>>>;
    public class Status_GetPaginationQueryHandler : IQueryHandler<Status_GetPaginationQuery, Result<PaginatedList<StatusDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Status_GetPaginationQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PaginatedList<StatusDto>>> Handle(Status_GetPaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Statuses
                                .OrderedListQuery(orderCol, orderDir)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => 
              s.Slug.Contains(request.RequestData.TextSearch) || s.Name.Contains(request.RequestData.TextSearch));
            }



            var paging = await query.ProjectTo<StatusDto>(_mapper.ConfigurationProvider)
                                    .PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);

            return Result<PaginatedList<StatusDto>>.Success(paging);
        }
    }
}
