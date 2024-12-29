using Identity.API.Features.ProvinceFeature.Dto;

namespace Identity.API.Features.ProvinceFeature.Queries;

public record Province_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<ProvinceDto>>>;
public class Province_GetFilterQueryHandler : IQueryHandler<Province_GetFilterQuery, Result<IEnumerable<ProvinceDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Province_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<ProvinceDto>>> Handle(Province_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Provinces.OrderedListQuery(orderCol,orderDir)
							.ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch) ||
									 s.Code.Contains(request.RequestData.TextSearch));
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

        return Result<IEnumerable<ProvinceDto>>.Success(await query.ToListAsync());
	}
}