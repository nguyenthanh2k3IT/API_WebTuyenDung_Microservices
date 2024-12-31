using Identity.API.Features.ProvinceFeature.Dto;

namespace Identity.API.Features.ProvinceFeature.Commands;

public record Province_AddCommand(Province RequestData) : ICommand<Result<ProvinceDto>>;

public class ProvinceAddCommandValidator : AbstractValidator<Province_AddCommand>
{
    public ProvinceAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Name)
            .NotEmpty().WithMessage("Tên tỉnh thành không được để trống");

        RuleFor(command => command.RequestData.Code)
            .NotEmpty().WithMessage("Ma tỉnh thành không được để trống");

        RuleFor(command => command.RequestData.Area)
            .NotEmpty().WithMessage("Mã khu vực không được để trống");

        RuleFor(command => command.RequestData.AreaName)
            .NotEmpty().WithMessage("Tên khu vực không được để trống");
    }
}

public class Province_AddCommandHandler : ICommandHandler<Province_AddCommand, Result<ProvinceDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Province_AddCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ProvinceDto>> Handle(Province_AddCommand request, CancellationToken cancellationToken)
    {
        var Province = new Province()
        {
            Id = Generator.RandomNumberByLength(3).ToString(),
            Name = request.RequestData.Name,
            Code = request.RequestData.Code,
            Area = request.RequestData.Area,
            AreaName = request.RequestData.AreaName,
            CreatedUser = request.RequestData.CreatedUser,
            ModifiedUser = request.RequestData.CreatedUser
        };

        _context.Provinces.Add(Province);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<ProvinceDto>.Success(_mapper.Map<ProvinceDto>(Province));
    }
}