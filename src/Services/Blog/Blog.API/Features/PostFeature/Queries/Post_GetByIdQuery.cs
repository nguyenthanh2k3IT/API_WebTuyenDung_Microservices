using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Features.CategoryFeature.Queries;
using Blog.API.Features.PostFeature.Dtos;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostFeature.Queries
{
    public record Post_GetByIdQuery(Guid Id): IQuery<Result<PostDto>>;
    public class Post_GetByIdQueryHandler : IQueryHandler<Post_GetByIdQuery, Result<PostDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Post_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PostDto>> Handle(Post_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Posts.Where(s => s.Id == request.Id)
                                       .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync();
            return Result<PostDto>.Success(posts);
        }
    }

}
