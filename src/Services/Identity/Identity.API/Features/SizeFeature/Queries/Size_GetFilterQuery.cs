﻿using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.SizeFeature.Queries;

public record Size_GetFilterQuery(FilterRequest RequestData) : IQuery<Result<IEnumerable<SizeDto>>>;
public class Size_GetFilterQueryHandler : IQueryHandler<Size_GetFilterQuery, Result<IEnumerable<SizeDto>>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Size_GetFilterQueryHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<IEnumerable<SizeDto>>> Handle(Size_GetFilterQuery request, CancellationToken cancellationToken)
	{
		var orderCol = request.RequestData.OrderCol;
		var orderDir = request.RequestData.OrderDir;

		var query = _context.Sizes.OrderedListQuery(orderCol,orderDir)
							.ProjectTo<SizeDto>(_mapper.ConfigurationProvider)
							.AsNoTracking();

		if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
		{
			query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch) ||
									 s.Value.Contains(request.RequestData.TextSearch));
		}

		if (request.RequestData.Skip != null)
		{
			query = query.Skip(request.RequestData.Skip.Value);
		}

		if (request.RequestData.TotalRecord != null)
		{
			query = query.Take(request.RequestData.TotalRecord.Value);
		}

        return Result<IEnumerable<SizeDto>>.Success(await query.ToListAsync());
	}
}