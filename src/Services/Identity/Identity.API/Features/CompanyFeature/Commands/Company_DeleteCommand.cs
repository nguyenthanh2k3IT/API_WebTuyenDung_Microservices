namespace Identity.API.Features.CompanyFeature.Commands;

public record Company_DeleteCommand(DeleteRequest RequestData) : ICommand<Result<bool>>;
public class Company_DeleteCommandHandler : ICommandHandler<Company_DeleteCommand, Result<bool>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Company_DeleteCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(Company_DeleteCommand request, CancellationToken cancellationToken)
    {
        if (request.RequestData.Ids == null)
            throw new ApplicationException("Ids not found");

        var ids = request.RequestData.Ids.Select(s => Guid.Parse(s)).ToList();
        var query = await _context.CompanyInfos.Include(s => s.User).Where(m => ids.Contains(m.Id)).ToListAsync();
        if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

        foreach (var item in query)
        {
            item.DeleteFlag = true;
            item.ModifiedDate = DateTime.Now;
            item.ModifiedUser = request.RequestData.ModifiedUser;
            item.User.DeleteFlag = true;
            item.User.ModifiedDate = DateTime.Now;
            item.ModifiedUser = request.RequestData.ModifiedUser;

            _context.Users.Update(item.User);
        }

        _context.CompanyInfos.UpdateRange(query);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}