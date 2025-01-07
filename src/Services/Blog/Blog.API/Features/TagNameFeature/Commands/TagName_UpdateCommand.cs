using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.TagNameFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using FluentValidation;
using BuildingBlock.Core.Validators;

namespace Blog.API.Features.TagNameFeature.Commands;


public record TagName_UpdateCommand(StatusRequest RequestData) : ICommand<Result<TagNameDto>>;
public class StatusUpdateCommandValidator : AbstractValidator<TagName_UpdateCommand>
{
    public StatusUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id).RequiredID();

        RuleFor(command => command.RequestData.Slug).FieldNotEmpty("Mã thẻ");

        RuleFor(command => command.RequestData.Name).FieldNotEmpty("Tên thẻ");
    }
}
public class TagName_UpdateCommandHandler : ICommandHandler<TagName_UpdateCommand, Result<TagNameDto>>
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    public TagName_UpdateCommandHandler(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }
    public async Task<Result<TagNameDto>> Handle(TagName_UpdateCommand request, CancellationToken cancellationToken)
    {
        var tagNames = await _dataContext.TagNames.FindAsync(request.RequestData.Slug);

        if (tagNames == null)
        {
            throw new ApplicationException("Post not found");
        }


        tagNames.Name = request.RequestData.Name;
        tagNames.ModifiedDate = DateTime.UtcNow;

        _dataContext.TagNames.Update(tagNames);

        await _dataContext.SaveChangesAsync(cancellationToken);

        return Result<TagNameDto>.Success(_mapper.Map<TagNameDto>(tagNames));
    }
}
