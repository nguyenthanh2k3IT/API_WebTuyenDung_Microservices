using Identity.API.Commons.Validators;
using Identity.API.Features.UserFeature.Dto;
using Identity.API.Interfaces;
using Identity.API.Models.UserModel;

namespace Identity.API.Features.UserFeature.Commands;

public record User_AddCommand(UserAddOrUpdateRequest RequestData) : ICommand<Result<UserDto>>;

public class UserAddCommandValidator : AbstractValidator<User_AddCommand>
{
	public UserAddCommandValidator()
	{

		RuleFor(command => command.RequestData.Email).EmailRule();

		RuleFor(command => command.RequestData.Password).PasswordRule();

		RuleFor(command => command.RequestData.Phone).PhoneRule();

		RuleFor(command => command.RequestData.Fullname).FullnameRule();
	}
}

public class User_AddCommandHandler : ICommandHandler<User_AddCommand, Result<UserDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public User_AddCommandHandler(
		IMapper mapper,
		DataContext context,
		IUserService userService)
	{
		_context = context;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<Result<UserDto>> Handle(User_AddCommand request, CancellationToken cancellationToken)
	{
		await _userService.CheckValidEmail(request.RequestData.Email);

		var user = new User();
		user.Email = request.RequestData.Email;
		user.Phone = request.RequestData.Phone;
		user.Fullname = request.RequestData.Fullname;
		user.Avatar = AvatarConstant.Default;

		if (!string.IsNullOrEmpty(request.RequestData.StatusId))
		{
			var status = await _context.Statuses.FindAsync(request.RequestData.StatusId);
			if (status != null)
			{
				user.StatusId = status.Id;
				user.Status = status;
			}
		}

		if (!string.IsNullOrEmpty(request.RequestData.RoleId))
		{
			var role = await _context.Roles.FindAsync(request.RequestData.RoleId);
			if (role != null)
			{
				user.RoleId = role.Id;
				user.Role = role;
			}
		}

		_context.Users.Add(user);
		int rows = await _context.SaveChangesAsync();

		return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
	}
}