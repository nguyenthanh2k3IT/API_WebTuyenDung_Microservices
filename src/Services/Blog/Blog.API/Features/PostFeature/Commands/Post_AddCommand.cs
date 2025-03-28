﻿using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.Core.Validators;
using BuildingBlock.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostFeature.Commands;

public record Post_AddCommand(PostRequest RequestData) : ICommand<Result<PostDto>>;
public class PostAddCommandValidator : AbstractValidator<Post_AddCommand>
{
    public PostAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Slug).FieldNotEmpty("Mã bài viết");

        RuleFor(command => command.RequestData.Title).FieldNotEmpty("Tiêu đề bài viết");

        RuleFor(command => command.RequestData.Content).FieldNotEmpty("Nội dung bài viết");

        RuleFor(command => command.RequestData.Image).FieldNotEmpty("Ảnh nền bài viết");

        RuleFor(command => command.RequestData.CategoryId).NotNull().WithMessage($"Thể loại không được để trống");

        RuleFor(command => command.RequestData.StatusId).NotNull().WithMessage($"Trạng thái không được để trống");
    }
}
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
        var exist = await _dataContext.Posts.FirstOrDefaultAsync(s => s.Slug == request.RequestData.Slug);

        if (exist != null)
        {
            throw new ApplicationException("Mã bài viết đã được sử dụng");
        }

        var category = await _dataContext.Categories
                               .Where(s => s.Id == request.RequestData.CategoryId).FirstOrDefaultAsync();

        if (category == null)
        {
            throw new ApplicationException("Không tìm thấy thể loại bài viết");
        }

        var status = await _dataContext.Statuses
                                       .Where(s => s.Id == request.RequestData.StatusId)
                                       .FirstOrDefaultAsync();

        if (status == null)
        {
            throw new ApplicationException("Không tìm thấy trạng thái bài viết");
        }

        var Post = new Post()
        {
            Slug = request.RequestData.Slug,
            Image = request.RequestData.Image,
            Content = request.RequestData.Content,
            Title = request.RequestData.Title
        };

        Post.Category = category;
        Post.Status = status;

        if(request.RequestData.TagNames != null && request.RequestData.TagNames.Any())
        {
            var ids = request.RequestData.TagNames;
            Post.TagNames = await _dataContext.TagNames.Where(s => ids.Contains(s.Id)).ToListAsync();
        }

        _dataContext.Posts.Add(Post);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return Result<PostDto>.Success(_mapper.Map<PostDto>(Post));

    }
}
