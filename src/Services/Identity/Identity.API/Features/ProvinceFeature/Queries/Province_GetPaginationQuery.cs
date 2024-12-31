using Identity.API.Features.ProvinceFeature.Dto;

namespace Identity.API.Features.ProvinceFeature.Queries;

public record Province_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<ProvinceDto>>>;
public class Province_GetPaginationQueryHandler : IQueryHandler<Province_GetPaginationQuery, Result<PaginatedList<ProvinceDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Province_GetPaginationQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<ProvinceDto>>> Handle(Province_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Provinces
							.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<ProvinceDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
            query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch) ||
                                     s.Code.Contains(request.RequestData.TextSearch));
        }

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<ProvinceDto>>.Success(paging);
	}
}