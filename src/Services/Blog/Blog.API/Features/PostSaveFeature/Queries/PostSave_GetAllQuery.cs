using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostSaveFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostSaveFeature.Queries
{

    public record PostSave_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<PostSaveDto>>>;
    public class PostSave_GetAllQueryHandler : IQueryHandler<PostSave_GetAllQuery, Result<IEnumerable<PostSaveDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostSave_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PostSaveDto>>> Handle(PostSave_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<PostSaveDto> statuses = await _context.PostSaves
                                                       .OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<PostSaveDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return Result<IEnumerable<PostSaveDto>>.Success(statuses);
        }
    }
}
