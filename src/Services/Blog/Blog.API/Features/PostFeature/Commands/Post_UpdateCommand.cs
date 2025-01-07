using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.Core.Validators;
using BuildingBlock.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostFeature.Commands;

public record Post_UpdateCommand(PostRequest RequestData) : ICommand<Result<PostDto>>;
public class PostUpdateCommandValidator : AbstractValidator<Post_AddCommand>
{
    public PostUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id).RequiredID();

        RuleFor(command => command.RequestData.Slug).FieldNotEmpty("Mã bài viết");

        RuleFor(command => command.RequestData.Title).FieldNotEmpty("Tiêu đề bài viết");

        RuleFor(command => command.RequestData.Content).FieldNotEmpty("Nội dung bài viết");

        RuleFor(command => command.RequestData.Image).FieldNotEmpty("Ảnh nền bài viết");

        RuleFor(command => command.RequestData.CategoryId).NotNull().WithMessage($"Thể loại không được để trống");

        RuleFor(command => command.RequestData.StatusId).NotNull().WithMessage($"Trạng thái không được để trống");
    }
}
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
        var Post = await _dataContext.Posts.Include(s => s.TagNames).FirstOrDefaultAsync(s => s.Id == request.RequestData.Id);

        if (Post == null)
        {
            throw new ApplicationException("Không tìm thấy bài viết");
        }

        var category = await _dataContext.Categories
                                         .Where(s => s.Id == request.RequestData.CategoryId)
                                         .FirstOrDefaultAsync();

        if (category == null)
        {
            throw new ApplicationException("Không tìm thấy thể loại");
        }

        var status = await _dataContext.Statuses
                                       .Where(s => s.Id == request.RequestData.StatusId)
                                       .FirstOrDefaultAsync();

        if (status == null)
        {
            throw new ApplicationException("Không tìm thấy trạng thái bài viết");
        }

        Post.Category = category;
        Post.Status = status;
        Post.Slug = request.RequestData.Slug;
        Post.Content = request.RequestData.Content;
        Post.Title = request.RequestData.Title;
        Post.Image = request.RequestData.Image;
        Post.ModifiedDate = DateTime.UtcNow;
        
        if (Post.TagNames != null) Post.TagNames.Clear();

        if (request.RequestData.TagNames != null && request.RequestData.TagNames.Any())
        {
            var ids = request.RequestData.TagNames;
            Post.TagNames = await _dataContext.TagNames.Where(s => ids.Contains(s.Id)).ToListAsync();
        }

        _dataContext.Posts.Update(Post);

        await _dataContext.SaveChangesAsync(cancellationToken);

        return Result<PostDto>.Success(_mapper.Map<PostDto>(Post));
    }
}
