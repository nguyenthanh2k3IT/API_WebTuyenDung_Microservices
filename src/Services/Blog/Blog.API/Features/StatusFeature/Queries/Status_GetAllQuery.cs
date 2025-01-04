using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Features.StatusFeature.Dtos;
using BuildingBlock.Core.Abstractions;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.StatusFeature.Queries
{
    
    public record  Status_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<StatusDto>>>;
    public class  Status_GetAllQueryHandler : IQueryHandler< Status_GetAllQuery, Result<IEnumerable<StatusDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public  Status_GetAllQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StatusDto>>> Handle( Status_GetAllQuery request, CancellationToken cancellationToken)
        {
            var orderCol = request.RequestData.OrderCol;
            var orderDir = request.RequestData.OrderDir;

            IEnumerable<StatusDto> statuses= await _context.Statuses
                                                       .OrderedListQuery(orderCol, orderDir)
                                                       .ProjectTo<StatusDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return Result<IEnumerable<StatusDto>>.Success(statuses);
        }



    }
}
