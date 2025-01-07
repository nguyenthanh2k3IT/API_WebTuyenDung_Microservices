using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.TagNameFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.Core.Validators;
using BuildingBlock.CQRS;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.TagNameFeature.Commands;

public record  TagName_AddCommand(StatusRequest RequestData) : ICommand<Result<TagNameDto>>;
public class TagNameAddCommandValidator : AbstractValidator< TagName_AddCommand>
{
    public TagNameAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Slug).FieldNotEmpty("Mã thẻ");

        RuleFor(command => command.RequestData.Name).FieldNotEmpty("Tên thẻ");
    }
}
public class  TagName_AddCommandHandler : ICommandHandler< TagName_AddCommand, Result<TagNameDto>>
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    public  TagName_AddCommandHandler(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }
    public async Task<Result<TagNameDto>> Handle( TagName_AddCommand request, CancellationToken cancellationToken)
    {
        var exist = await _dataContext.TagNames.FirstOrDefaultAsync(s => s.Slug == request.RequestData.Slug);

        if (exist != null)
        {
            throw new ApplicationException("Mã thẻ đã được sử dụng");
        }

        var tagNames = new TagName()
        {
            Slug = request.RequestData.Slug,
            Name = request.RequestData.Name
        };
        _dataContext.TagNames.Add(tagNames);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return Result<TagNameDto>.Success(_mapper.Map<TagNameDto>(tagNames));

    }
}
