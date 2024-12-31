namespace Identity.API.Features.ProfileFeature.Commands;

public record Profile_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Profile_DeleteCommandHandler : ICommandHandler<Profile_DeleteCommand, Result<bool>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Profile_DeleteCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(Profile_DeleteCommand request, CancellationToken cancellationToken)
    {
        if (request.RequestData.Ids == null)
            throw new ApplicationException("Ids not found");

        var ids = request.RequestData.Ids.Select(s => Guid.Parse(s)).ToList();
        var query = await _context.Profiles.Where(m => ids.Contains(m.Id)).ToListAsync();
        if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

        foreach (var item in query)
        {
            item.DeleteFlag = true;
            item.ModifiedDate = DateTime.Now;
            item.ModifiedUser = request.RequestData.ModifiedUser;
        }

        _context.Profiles.UpdateRange(query);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
