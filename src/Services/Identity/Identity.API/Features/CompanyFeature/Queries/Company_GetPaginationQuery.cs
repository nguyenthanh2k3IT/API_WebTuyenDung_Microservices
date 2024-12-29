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
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.User.Email.Contains(request.RequestData.TextSearch) || 
									 s.User.Fullname.Contains(request.RequestData.TextSearch));
		}

		if (!StringHelper.IsNullOrEmpty(request.RequestData.SizeId))
		{
			query = query.Where(s => s.SizeId != request.RequestData.SizeId);
		}

		var paging = await query.ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
								.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);

		return Result<PaginatedList<CompanyDto>>.Success(paging);
	}
}