using Identity.API.Commons.Validators;
using Identity.API.Features.CompanyFeature.Dto;
using Identity.API.Models.CompanyModel;

namespace Identity.API.Features.CompanyFeature.Commands;

public record Company_AddCommand(CompanyAddRequest RequestData) : ICommand<Result<CompanyDto>>;

public class CompanyAddCommandValidator : AbstractValidator<Company_AddCommand>
{
    public CompanyAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Password).PasswordRule();

        RuleFor(command => command.RequestData.Email).EmailRule();

        RuleFor(command => command.RequestData.Phone).PhoneRule();

        RuleFor(command => command.RequestData.Fullname).FullnameRule();

        RuleFor(command => command.RequestData.Website)
            .NotEmpty().WithMessage("Website không được để trống")
            .MaximumLength(200).WithMessage("Website không được vượt quá 200 ký tự");

        RuleFor(command => command.RequestData.Address)
            .NotEmpty().WithMessage("Địa chỉ không được để trống")
            .MaximumLength(100).WithMessage("Địa chỉ không được vượt quá 100 ký tự");

        RuleFor(command => command.RequestData.Introduction)
            .MaximumLength(1000).WithMessage("Giới thiệu không được vượt quá 1000 ký tự");

        RuleFor(command => command.RequestData.SizeId)
            .NotEmpty().WithMessage("Quy mô công ty không được để trống");
    }
}

public class Company_AddCommandHandler : ICommandHandler<Company_AddCommand, Result<CompanyDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Company_AddCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CompanyDto>> Handle(Company_AddCommand request, CancellationToken cancellationToken)
    {
        var user = new User();
        user.Email = request.RequestData.Email;
        user.Password = request.RequestData.Password;
        user.Phone = request.RequestData.Phone;
        user.Fullname = request.RequestData.Fullname;
        user.Avatar = request.RequestData.Avatar ?? AvatarConstant.Default;
        user.RoleId = RoleEnum.COMPANY;

        var company = new CompanyInfo();
        company.Id = user.Id;
        company.User = user;
        company.Wallpaper = request.RequestData.Wallpaper ?? AvatarConstant.Default;
        company.Website = request.RequestData.Website;
        company.Address = request.RequestData.Address;
        company.Introduction = request.RequestData.Introduction;

        var size = await _context.Sizes.FindAsync(request.RequestData.SizeId);
        if (size == null)
        {
            throw new ApplicationException("Không tìm thấy quy mô công ty");
        }
        company.Size = size;
        company.SizeId = size.Id;

        if (request.RequestData.ProvinceIds != null && request.RequestData.ProvinceIds.Count > 0)
        {
            var ids = request.RequestData.ProvinceIds;
            company.Provinces = await _context.Provinces.Where(s => ids.Contains(s.Id)).ToListAsync();
        }

        company.ModifiedDate = DateTime.Now;
        company.ModifiedUser = request.RequestData.ModifiedUser;

        _context.CompanyInfos.Add(company);
        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<CompanyDto>.Success(_mapper.Map<CompanyDto>(company));
    }
}