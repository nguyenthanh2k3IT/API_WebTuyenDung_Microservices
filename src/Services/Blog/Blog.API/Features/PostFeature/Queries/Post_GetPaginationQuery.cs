using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Paging;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostFeature.Queries
{

    public record Post_PaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<PostDto>>>;
    public class Post_PaginationQueryHandler : IQueryHandler<Post_PaginationQuery, Result<PaginatedList<PostDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Post_PaginationQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PaginatedList<PostDto>>> Handle(Post_PaginationQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Posts
                                .OrderedListQuery(orderCol, orderDir)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Content.Contains(request.RequestData.TextSearch) ||
                                         s.Slug.Contains(request.RequestData.TextSearch)||s.Title.Contains(request.RequestData.TextSearch));
            }



            var paging = await query.ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                                    .PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);

            return Result<PaginatedList<PostDto>>.Success(paging);
        }
    }

}
