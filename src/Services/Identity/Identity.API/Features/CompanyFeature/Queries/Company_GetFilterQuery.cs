using Identity.API.Features.CompanyFeature.Dto;

namespace Identity.API.Features.CompanyFeature.Queries;

public record Company_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<CompanyDto>>>;
public class Company_GetFilterQueryHandler : IQueryHandler<Company_GetFilterQuery, Result<IEnumerable<CompanyDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Company_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CompanyDto>>> Handle(Company_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.CompanyInfos.OrderedListQuery(orderCol,orderDir)
							.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Email.Contains(request.RequestData.TextSearch) ||
									 s.Fullname.Contains(request.RequestData.TextSearch));
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

        return Result<IEnumerable<CompanyDto>>.Success(await query.ToListAsync());
	}
}