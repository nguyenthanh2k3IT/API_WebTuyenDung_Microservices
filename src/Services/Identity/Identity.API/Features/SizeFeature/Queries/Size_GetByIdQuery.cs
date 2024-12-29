using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.SizeFeature.Queries;

public record Size_GetByIdQuery(Guid Id) : IQuery<Result<SizeDto>>;
public class Size_GetByIdQueryHandler : IQueryHandler<Size_GetByIdQuery, Result<SizeDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Size_GetByIdQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_GetByIdQuery request, CancellationToken cancellationToken)
	{
		var Sizes = await _context.Sizes.Where(s => s.Id == request.Id)
								  .ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
								  .FirstOrDefaultAsync();
		return Result<SizeDto>.Success(Sizes);
	}
}