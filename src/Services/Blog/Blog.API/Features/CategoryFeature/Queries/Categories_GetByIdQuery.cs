using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.CategoryFeature.Queries
{
    public record Categories_GetByIdQuery(Guid Id) : IQuery<Result<CategoryDto>>;
    public class Categories_GetByIdQueryHandler : IQueryHandler<Categories_GetByIdQuery, Result<CategoryDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Categories_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(Categories_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(s => s.Id == request.Id)
                                      .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                      .FirstOrDefaultAsync();
            return Result<CategoryDto>.Success(category);
        }
    }
}
