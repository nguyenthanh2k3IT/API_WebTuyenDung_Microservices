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
    public record Post_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<PostDto>>>;
    public class Post_GetAllQueryHandler : IQueryHandler<Post_GetAllQuery, Result<IEnumerable<PostDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Post_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PostDto>>> Handle(Post_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<PostDto> categories = await _context.Categories
                                                       .OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return Result<IEnumerable<PostDto>>.Success(categories);
        }



    }
    }
