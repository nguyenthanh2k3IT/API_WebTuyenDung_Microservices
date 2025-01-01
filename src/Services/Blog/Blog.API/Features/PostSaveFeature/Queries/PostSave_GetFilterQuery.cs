using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostSaveFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostSaveFeature.Queries
{
   
    public record PostSave_GetFilterQuery(PostSaveFilterRequest RequestData) : IQuery<Result<IEnumerable<PostSaveDto>>>;
    public class PostSave_GetFilterQueryHandler : IQueryHandler<PostSave_GetFilterQuery, Result<IEnumerable<PostSaveDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PostSave_GetFilterQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PostSaveDto>>> Handle(PostSave_GetFilterQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            var query = _context.PostSaves.OrderedListQuery(orderCol, orderDir)
                                .ProjectTo<PostSaveDto>(_mapper.ConfigurationProvider)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
            {
                query = query.Where(s => s.UserId.Contains(request.RequestData.TextSearch) ||
                                         s.PostId.Contains(request.RequestData.TextSearch));
            }

            if (request.RequestData.Skip != null)
            {
                query = query.Skip(request.RequestData.Skip.Value);
            }

            if (request.RequestData.TotalRecord != null)
            {
                query = query.Take(request.RequestData.TotalRecord.Value);
            }

            return Result<IEnumerable<PostSaveDto>>.Success(await query.ToListAsync());
        }
    }
}
