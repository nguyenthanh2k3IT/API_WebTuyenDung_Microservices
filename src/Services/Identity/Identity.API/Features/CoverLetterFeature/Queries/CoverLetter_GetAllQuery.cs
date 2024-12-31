using Identity.API.Features.CoverLetterFeature.Dtos;

namespace Identity.API.Features.CoverLetterFeature.Queries;

public record CoverLetter_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<CoverLetterDto>>>;
public class CoverLetter_GetAllQueryHandler : IQueryHandler<CoverLetter_GetAllQuery, Result<IEnumerable<CoverLetterDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CoverLetter_GetAllQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CoverLetterDto>>> Handle(CoverLetter_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<CoverLetterDto> CoverLetters = await _context.CoverLetters.Where(s => s.UserId == request.RequestData.UserId)
                                                   .OrderByDescending(s => s.CreatedDate)
                                                   .ProjectTo<CoverLetterDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

        return Result<IEnumerable<CoverLetterDto>>.Success(CoverLetters);
    }
}