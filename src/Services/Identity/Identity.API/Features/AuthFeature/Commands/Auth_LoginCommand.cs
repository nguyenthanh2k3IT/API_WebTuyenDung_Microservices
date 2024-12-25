using Identity.API.Features.AuthFeature.Dto;
using Identity.API.Interfaces;
using Identity.API.Models.AuthModel;

namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_LoginCommand(LoginRequest RequestData) : ICommand<Result<LoginDto>>;

public class AddUserCommandValidator : AbstractValidator<Auth_LoginCommand>
{
	public AddUserCommandValidator()
	{
		RuleFor(command => command.RequestData.Email).NotEmpty().WithMessage("Email is required");
		RuleFor(command => command.RequestData.Password).NotEmpty().WithMessage("Password is required");
	}
}

public class Auth_LoginCommandHandler : ICommandHandler<Auth_LoginCommand, Result<LoginDto>>
{
	private readonly DataContext _context;
	private readonly ITokenService _tokenService;
	private readonly IMapper _mapper;

	public Auth_LoginCommandHandler(
		DataContext context, 
		ITokenService tokenService, 
		IMapper mapper)
	{
		_context = context;
		_tokenService = tokenService;
		_mapper = mapper;
	}

	public async Task<Result<LoginDto>> Handle(Auth_LoginCommand request, CancellationToken cancellationToken)
	{
		var user = await _context.Users
								 .Where(s => s.Email == request.RequestData.Email
										  && s.Password == request.RequestData.Password)
								 .FirstOrDefaultAsync();
		if (user is null)
		{
			throw new ApplicationException("Email or password are not correct");
		}

		if (request.RequestData.IsAdmin is true && user.RoleId != RoleEnum.ADMIN)
		{
			throw new ApplicationException("Account dont have permission to access");
		}

		if (user.RoleId is null || user.StatusId is null)
		{
			throw new ApplicationException("Account dont have permission to access");
		}

        if (user.StatusId == UserStatusEnum.UNACTIVE)
        {
            throw new ApplicationException("Account is not verified");
        }

        if (user.StatusId == UserStatusEnum.BANNED)
		{
			throw new ApplicationException("Account was banned");
		}

		var login = new LoginDto()
		{
			User = _mapper.Map<ProfileDto>(user),
			Token = await _tokenService.GenerateToken(
				user.Id, user.Email,user.Fullname,
				user.Avatar ?? AvatarConstant.Default,
				user.Phone,user.RoleId.Value,
				user.StatusId.Value)
		};

		return Result<LoginDto>.Success(login);
	}
}
