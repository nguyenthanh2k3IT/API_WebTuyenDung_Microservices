using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Features.CategoryFeature.Queries;
using Blog.API.Features.PostFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostFeature.Queries
{
    public record Post_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<PostDto>>>;
    public class Post_GetFilterQueryHandler : IQueryHandler<Post_GetFilterQuery, Result<IEnumerable<PostDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Post_GetFilterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PostDto>>> Handle(Post_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Posts.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Slug.Contains(request.RequestData.TextSearch) ||
                                         s.Title.Contains(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<PostDto>>.Success(await query.ToListAsync());
        }
    }
}


