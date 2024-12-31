using Identity.API.Features.ProvinceFeature.Dto;

namespace Identity.API.Features.ProvinceFeature.Commands;

public record Province_UpdateCommand(Province RequestData) : ICommand<Result<ProvinceDto>>;

public class ProvinceUpdateCommandValidator : AbstractValidator<Province_UpdateCommand>
{
    public ProvinceUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Không tìm thấy ID");

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

public class Province_UpdateCommandHandler : ICommandHandler<Province_UpdateCommand, Result<ProvinceDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Province_UpdateCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ProvinceDto>> Handle(Province_UpdateCommand request, CancellationToken cancellationToken)
    {
        var Province = await _context.Provinces.FindAsync(request.RequestData.Id);

        if (Province == null)
        {
            throw new ApplicationException("Province not found");
        }

        Province.Name = request.RequestData.Name;
        Province.Code = request.RequestData.Code;
        Province.Area = request.RequestData.Area;
        Province.AreaName = request.RequestData.AreaName;
        Province.ModifiedDate = DateTime.Now;
        Province.ModifiedUser = request.RequestData.ModifiedUser;

        _context.Provinces.Update(Province);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<ProvinceDto>.Success(_mapper.Map<ProvinceDto>(Province));
    }
}