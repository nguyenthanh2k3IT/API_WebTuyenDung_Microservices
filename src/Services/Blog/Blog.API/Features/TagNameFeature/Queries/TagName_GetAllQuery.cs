using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.TagNameFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.TagNameFeature.Queries
{
  
    public record TagName_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<TagNameDto>>>;
    public class TagName_GetAllQueryHandler : IQueryHandler<TagName_GetAllQuery, Result<IEnumerable<TagNameDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TagName_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TagNameDto>>> Handle(TagName_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<TagNameDto> statuses = await _context.TagNames
                                                       .OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<TagNameDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return Result<IEnumerable<TagNameDto>>.Success(statuses);
        }



    }
}
