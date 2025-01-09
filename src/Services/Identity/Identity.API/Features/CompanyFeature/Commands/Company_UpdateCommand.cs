using Identity.API.Commons.Validators;
using Identity.API.Features.CompanyFeature.Dto;
using Identity.API.Models.CompanyModel;

namespace Identity.API.Features.CompanyFeature.Commands;

public record Company_UpdateCommand(CompanyUpdateRequest RequestData) : ICommand<Result<CompanyDto>>;

public class CompanyUpdateCommandValidator : AbstractValidator<Company_UpdateCommand>
{
    public CompanyUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Id không được để trống");

        RuleFor(command => command.RequestData.Email).EmailRule();

        RuleFor(command => command.RequestData.Phone).PhoneRule();

        RuleFor(command => command.RequestData.Fullname).FullnameRule();

        RuleFor(command => command.RequestData.Website)
            .NotEmpty().WithMessage("Website không được để trống")
            .MaximumLength(200).WithMessage("Website không được vượt quá 200 ký tự");

        RuleFor(command => command.RequestData.Address)
            .NotEmpty().WithMessage("Địa chỉ không được để trống")
            .MaximumLength(250).WithMessage("Địa chỉ không được vượt quá 250 ký tự");

        RuleFor(command => command.RequestData.Introduction)
            .MaximumLength(1000).WithMessage("Giới thiệu không được vượt quá 1000 ký tự");

        RuleFor(command => command.RequestData.SizeId)
            .NotEmpty().WithMessage("Quy mô công ty không được để trống");
    }
}

public class Company_UpdateCommandHandler : ICommandHandler<Company_UpdateCommand, Result<CompanyDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public Company_UpdateCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CompanyDto>> Handle(Company_UpdateCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.CompanyInfos.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == request.RequestData.Id);

        if (company == null)
        {
            throw new ApplicationException("Không tìm thấy công ty");
        }

        company.User.Email = request.RequestData.Email;
        company.User.Phone = request.RequestData.Phone;
        company.User.Fullname = request.RequestData.Fullname;

        if (StringHelper.IsModified(company.User.Avatar, request.RequestData.Avatar))
        {
            company.User.Avatar = request.RequestData.Avatar;
        }

        if (StringHelper.IsModified(company.Wallpaper, request.RequestData.Wallpaper))
        {
            company.Wallpaper = request.RequestData.Wallpaper!;
        }

        company.Website = request.RequestData.Website;
        company.Address = request.RequestData.Address;
        company.Introduction = request.RequestData.Introduction;

        var size = await _context.Sizes.FindAsync(request.RequestData.SizeId);
        if(size == null)
        {
            throw new ApplicationException("Không tìm thấy quy mô công ty");
        }
        company.Size = size;
        company.SizeId = size.Id;

        if(company.Provinces != null && company.Provinces.Any()) 
            company.Provinces.Clear();

        if (request.RequestData.ProvinceIds != null && request.RequestData.ProvinceIds.Count > 0)
        {
            var ids = request.RequestData.ProvinceIds;
            company.Provinces = await _context.Provinces.Where(s => ids.Contains(s.Id)).ToListAsync();
        }

        company.ModifiedDate = DateTime.Now;
        company.ModifiedUser = request.RequestData.ModifiedUser;

        _context.CompanyInfos.Update(company);
        _context.Users.Update(company.User);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<CompanyDto>.Success(_mapper.Map<CompanyDto>(company));
    }
}