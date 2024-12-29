using BuildingBlock.Core.Paging;
using Identity.API.Features.CompanyFeature.Dto;
using Identity.API.Models.CompanyModel;

namespace Identity.API.Features.CompanyFeature.Queries;

public record Company_GetPaginationQuery(CompanyPaginationRequest RequestData) : IQuery<Result<PaginatedList<CompanyDto>>>;
public class Company_GetPaginationQueryHandler : IQueryHandler<Company_GetPaginationQuery, Result<PaginatedList<CompanyDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Company_GetPaginationQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<PaginatedList<CompanyDto>>> Handle(Company_GetPaginationQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.CompanyInfos
							.OrderedListQuery(orderCol, orderDir)
							.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Email.Contains(request.RequestData.TextSearch) || 
									 s.Fullname.Contains(request.RequestData.TextSearch));
		}

		var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
		return Result<PaginatedList<CompanyDto>>.Success(paging);
	}
}