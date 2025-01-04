using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.CategoryFeature.Commands
{
    public record Categories_UpdateCommand(CategoriesRequest RequestData):ICommand<Result<CategoryDto>>;
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
            var Categories = await _dataContext.Categories.FindAsync(request.RequestData.Slug);

            if (Categories == null)
            {
                throw new ApplicationException("Category not found");
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
}
