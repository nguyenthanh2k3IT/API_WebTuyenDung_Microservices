using Identity.API.Features.ProfileFeature.Dtos;

namespace Identity.API.Features.ProfileFeature.Queries;

public record Profile_GetPaginationQuery(PaginationRequest RequestData) : IQuery<Result<PaginatedList<ProfileDto>>>;
public class Profile_GetPaginationQueryHandler : IQueryHandler<Profile_GetPaginationQuery, Result<PaginatedList<ProfileDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Profile_GetPaginationQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<ProfileDto>>> Handle(Profile_GetPaginationQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        var query = _context.Profiles.Where(s => s.UserId == request.RequestData.UserId)
                            .OrderByDescending(s => s.CreatedDate)
                            .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider)
                            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.RequestData.TextSearch))
        {
            query = query.Where(s => s.Name.Contains(request.RequestData.TextSearch));
        }

        var paging = await query.PaginatedListAsync(request.RequestData.PageIndex, request.RequestData.PageSize);
        return Result<PaginatedList<ProfileDto>>.Success(paging);
    }
}