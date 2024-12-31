using Identity.API.Features.CoverLetterFeature.Dtos;

namespace Identity.API.Features.CoverLetterFeature.Queries;

public record CoverLetter_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<CoverLetterDto>>>;
public class CoverLetter_GetPaginationQueryHandler : IQueryHandler<CoverLetter_GetPaginationQuery, Result<PaginatedList<CoverLetterDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CoverLetter_GetPaginationQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CoverLetterDto>>> Handle(CoverLetter_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _context.CoverLetters.Where(s => s.UserId == request.RequestData.UserId)
                            .OrderByDescending(s => s.CreatedDate)
                            .ProjectTo<CoverLetterDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<CoverLetterDto>>.Success(paging);
    }
}