using Identity.API.Features.CompanyFeature.Dto;

namespace Identity.API.Features.CompanyFeature.Queries;

public record Company_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<CompanyDto>>>;
public class Company_GetAllQueryHandler : IQueryHandler<Company_GetAllQuery, Result<IEnumerable<CompanyDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Company_GetAllQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<CompanyDto>>> Handle(Company_GetAllQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		IEnumerable<CompanyDto> Companys = await _context.CompanyInfos.OrderedListQuery(orderCol, orderDir)
												   .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
												   .ToListAsync();

		return Result<IEnumerable<CompanyDto>>.Success(Companys);
	}
}