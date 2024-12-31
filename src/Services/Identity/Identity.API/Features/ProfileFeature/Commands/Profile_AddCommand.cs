using Identity.API.Features.ProfileFeature.Dtos;

namespace Identity.API.Features.ProfileFeature.Commands;

public record Profile_AddCommand(Data.Profile RequestData) : ICommand<Result<ProfileDto>>;

public class ProfileAddCommandValidator : AbstractValidator<Profile_AddCommand>
{
    public ProfileAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Name)
            .NotEmpty().WithMessage("Tên thư ứng tuyển được để trống");

        RuleFor(command => command.RequestData.FileName)
            .NotEmpty().WithMessage("Tên thư ứng tuyển được để trống");

        RuleFor(command => command.RequestData.OriginalFileName)
           .NotEmpty().WithMessage("Tên thư ứng tuyển được để trống");

        RuleFor(command => command.RequestData.FilePath)
           .NotEmpty().WithMessage("Tên thư ứng tuyển được để trống");
    }
}

public class Profile_AddCommandHandler : ICommandHandler<Profile_AddCommand, Result<ProfileDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Profile_AddCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ProfileDto>> Handle(Profile_AddCommand request, CancellationToken cancellationToken)
    {
        var count = await _context.Profiles.Where(s => s.UserId == request.RequestData.UserId).CountAsync();
        if (count >= 5)
        {
            throw new ApplicationException("Chỉ có thể tạo tối đa 5 hồ sơ xin việc");
        }
        var Profile = new Data.Profile()
        {
            Id = Guid.NewGuid(),
            Name = request.RequestData.Name,
            FileName = request.RequestData.FileName,
            FilePath = request.RequestData.FilePath,
            OriginalFileName = request.RequestData.OriginalFileName,
            UserId = request.RequestData.UserId,
            CreatedUser = request.RequestData.CreatedUser,
            ModifiedUser = request.RequestData.CreatedUser
        };

        _context.Profiles.Add(Profile);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<ProfileDto>.Success(_mapper.Map<ProfileDto>(Profile));
    }
}