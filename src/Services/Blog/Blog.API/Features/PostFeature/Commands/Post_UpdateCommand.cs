using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;

namespace Blog.API.Features.PostFeature.Commands
{
    public record Post_UpdateCommand(PostRequest RequestData) : ICommand<Result<PostDto>>;
    public class Post_UpdateCommandHandler : ICommandHandler<Post_UpdateCommand, Result<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public Post_UpdateCommandHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<Result<PostDto>> Handle(Post_UpdateCommand request, CancellationToken cancellationToken)
        {
            var Post = await _dataContext.Posts.FindAsync(request.RequestData.Slug);

            if (Post == null)
            {
                throw new ApplicationException("Post not found");
            }

            Post.Slug = request.RequestData.Slug;
            Post.Content = request.RequestData.Content;
            Post.Title = request.RequestData.Title;
            Post.Image = request.RequestData.Image;
            Post.ModifiedDate = DateTime.UtcNow;
           
            _dataContext.Posts.Update(Post);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Result<PostDto>.Success(_mapper.Map<PostDto>(Post));
        }
    }
}
