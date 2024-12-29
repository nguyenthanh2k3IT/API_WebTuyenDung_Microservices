using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.SizeFeature.Queries;

public record Size_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<SizeDto>>>;
public class Size_GetAllQueryHandler : IQueryHandler<Size_GetAllQuery, Result<IEnumerable<SizeDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Size_GetAllQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<SizeDto>>> Handle(Size_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<SizeDto> Sizes = await _context.Sizes.OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

        return Result<IEnumerable<SizeDto>>.Success(Sizes);
    }
}