using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.CategoryFeature.Dto;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.CategoryFeature.Queries
{
    public record Categories_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<CategoryDto>>>;
    public class Categories_GetAllQueryHandler : IQueryHandler<Categories_GetAllQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Categories_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(Categories_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<CategoryDto> categories = await _context.Categories
                                                       .OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return Result<IEnumerable<CategoryDto>>.Success(categories);
        }
    }
}
