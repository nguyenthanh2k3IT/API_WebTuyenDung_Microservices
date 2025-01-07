using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using FluentValidation;
using BuildingBlock.Core.Validators;

namespace Blog.API.Features.CategoryFeature.Commands;

public record Categories_AddCommand(CategoriesRequest RequestData):ICommand<Result<CategoryDto>>;
public class CategoryAddCommandValidator : AbstractValidator<Categories_AddCommand>
{
    public CategoryAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Slug).FieldNotEmpty("Mã thể loại");

        RuleFor(command => command.RequestData.Name).FieldNotEmpty("Tên thể loại");
    }
}
public class Categories_AddCommandHandler : ICommandHandler<Categories_AddCommand, Result<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    public Categories_AddCommandHandler(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }
    public async Task<Result<CategoryDto>> Handle(Categories_AddCommand request, CancellationToken cancellationToken)
    {
        var exist = await _dataContext.Categories.FindAsync(request.RequestData.Slug);

        if (exist != null)
        {
            throw new ApplicationException("Slug is already in uses");
        }
        var Categories = new Category()
        {
            Slug = request.RequestData.Slug,
            Name = request.RequestData.Name,

        };
        _dataContext.Categories.Add(Categories);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(Categories));
    }
}
