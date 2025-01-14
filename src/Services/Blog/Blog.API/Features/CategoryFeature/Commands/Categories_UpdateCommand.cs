using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.Core.Validators;
using BuildingBlock.CQRS;
using FluentValidation;

namespace Blog.API.Features.CategoryFeature.Commands;

public record Categories_UpdateCommand(CategoriesRequest RequestData):ICommand<Result<CategoryDto>>;
public class CategoryUpdateCommandValidator : AbstractValidator<Categories_UpdateCommand>
{
    public CategoryUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id).RequiredID();

        RuleFor(command => command.RequestData.Slug).FieldNotEmpty("Mã thể loại");

        RuleFor(command => command.RequestData.Name).FieldNotEmpty("Tên thể loại");
    }
}
public class Categories_UpdateCommandHandler : ICommandHandler<Categories_UpdateCommand, Result<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    public Categories_UpdateCommandHandler(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }
    public async Task<Result<CategoryDto>> Handle(Categories_UpdateCommand request, CancellationToken cancellationToken)
    {
        var Categories = await _dataContext.Categories.FindAsync(request.RequestData.Id);

        if (Categories == null)
        {
            throw new ApplicationException($"Không tìm thấy thể loại với ID: {request.RequestData.Id}");
        }

        Categories.Slug = request.RequestData.Slug;
        Categories.Name = request.RequestData.Name;
        Categories.ModifiedDate = DateTime.UtcNow;
        Categories.ModifiedUser = request.RequestData.ModifiedUser;
       _dataContext.Categories.Update(Categories);

        await _dataContext.SaveChangesAsync(cancellationToken);

        return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(Categories));
    }
}
