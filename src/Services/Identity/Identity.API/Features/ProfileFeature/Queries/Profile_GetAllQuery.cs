using Identity.API.Features.ProfileFeature.Dtos;

namespace Identity.API.Features.ProfileFeature.Queries;

public record Profile_GetAllQuery(BaseRequest RequestData) : IQuery<Result<IEnumerable<ProfileDto>>>;
public class Profile_GetAllQueryHandler : IQueryHandler<Profile_GetAllQuery, Result<IEnumerable<ProfileDto>>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Profile_GetAllQueryHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<ProfileDto>>> Handle(Profile_GetAllQuery request, CancellationToken cancellationToken)
    {
        var orderCol = request.RequestData.OrderCol;
        var orderDir = request.RequestData.OrderDir;

        IEnumerable<ProfileDto> Profiles = await _context.Profiles.Where(s => s.UserId == request.RequestData.UserId)
                                                   .OrderByDescending(s => s.CreatedDate)
                                                   .ProjectTo<ProfileDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();

        return Result<IEnumerable<ProfileDto>>.Success(Profiles);
    }
}