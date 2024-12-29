using Identity.API.Features.CompanyFeature.Dto;

namespace Identity.API.Features.CompanyFeature.Queries;

public record Company_GetByIdQuery(Guid Id) : IQuery<Result<CompanyDto>>;
public class Company_GetByIdQueryHandler : IQueryHandler<Company_GetByIdQuery, Result<CompanyDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Company_GetByIdQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<CompanyDto>> Handle(Company_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var Companys = await _context.CompanyInfos.Where(s => s.Id == request.Id)
								  .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
								  .FirstOrDefaultAsync();
		return Result<CompanyDto>.Success(Companys);
	}
}