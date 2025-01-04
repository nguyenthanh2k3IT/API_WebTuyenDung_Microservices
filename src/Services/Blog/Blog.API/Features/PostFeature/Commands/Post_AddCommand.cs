using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostFeature.Commands
{
    public record Post_AddCommand(PostRequest RequestData) : ICommand<Result<PostDto>>;

    public class Post_AddCommandHandler : ICommandHandler<Post_AddCommand, Result<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public Post_AddCommandHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<Result<PostDto>> Handle(Post_AddCommand request, CancellationToken cancellationToken)
        {
            var exist = await _dataContext.Posts.FindAsync(request.RequestData.Slug);

            if (exist != null)
            {
                throw new ApplicationException("Slug is already in uses");
            }
            var category = await _dataContext.Categories
                                   .Where(s => s.Id == request.RequestData.CategoryId).FirstOrDefaultAsync();
                                   
            var Post = new Post()
            {
                Slug = request.RequestData.Slug,
                Image = request.RequestData.Image,
                Content=request.RequestData.Content,
                Title=request.RequestData.Title

            };
            if (category == null)
            {
                throw new ApplicationException("category is invalid");
            }

           Post.Category = category;

            _dataContext.Posts.Add(Post);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return Result<PostDto>.Success(_mapper.Map<PostDto>(Post));

        }
    }
}
