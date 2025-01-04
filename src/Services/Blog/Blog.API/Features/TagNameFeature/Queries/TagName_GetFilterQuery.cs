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
    
    public record TagName_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<TagNameDto>>>;
    public class TagName_GetFilterQueryHandler : IQueryHandler<TagName_GetFilterQuery, Result<IEnumerable<TagNameDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TagName_GetFilterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TagNameDto>>> Handle(TagName_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.Statuses.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<TagNameDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.Slug.Contains(request.RequestData.TextSearch) ||
                                         s.Name.Contains(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<TagNameDto>>.Success(await query.ToListAsync());
        }
    }
}
