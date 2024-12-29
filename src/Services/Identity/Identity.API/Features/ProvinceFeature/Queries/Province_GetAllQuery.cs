using Identity.API.Features.ProvinceFeature.Dto;

namespace Identity.API.Features.ProvinceFeature.Queries;

public record Province_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<ProvinceDto>>>;
public class Province_GetAllQueryHandler : IQueryHandler<Province_GetAllQuery, Result<IEnumerable<ProvinceDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Province_GetAllQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProvinceDto>>> Handle(Province_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<ProvinceDto> Provinces = await _context.Provinces.OrderedListQuery(orderCol, orderDir)
                                                   .ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

        return Result<IEnumerable<ProvinceDto>>.Success(Provinces);
    }
}