using Identity.API.Commons.Validators;
using Identity.API.Features.UserFeature.Dto;
using Identity.API.Interfaces;
using Identity.API.Models.AuthModel;

namespace Identity.API.Features.UserFeature.Commands;

public record User_UpdateProfileCommand(ProfileUpdateRequest RequestData) : ICommand<Result<UserDto>>;

public class UserUpdateProfileCommandValidator : AbstractValidator<User_UpdateProfileCommand>
{
	public UserUpdateProfileCommandValidator()
	{
		RuleFor(command => command.RequestData.Email).EmailRule();

		RuleFor(command => command.RequestData.Phone).PhoneRule();

		RuleFor(command => command.RequestData.Fullname).FullnameRule();
	}
}

public class User_UpdateProfileCommandHandler : ICommandHandler<User_UpdateProfileCommand, Result<UserDto>>
{
	private readonly DataContext _context;
	private readonly IMapper _mapper;
	private readonly IUserService _userService;

	public User_UpdateProfileCommandHandler(
		IMapper mapper,
		DataContext context,
		IUserService userService)
	{
		_context = context;
		_mapper = mapper;
		_userService = userService;
	}

	public async Task<Result<UserDto>> Handle(User_UpdateProfileCommand request, CancellationToken cancellationToken)
	{
		var user = await _userService.FindValidUser(request.RequestData.Id);

		var exist = await _context.Users
					.Where(u => u.Id != user.Id && u.Email == request.RequestData.Email)
					.FirstOrDefaultAsync();

		if(exist is not null)
		{
			throw new ApplicationException("Email already in uses");
		}

		user.Email = request.RequestData.Email;
		user.Phone = request.RequestData.Phone;
		user.Fullname = request.RequestData.Fullname;
		user.Avatar = request.RequestData.Avatar ?? user.Avatar;

		_context.Users.Update(user);
		int rows = await _context.SaveChangesAsync();

		return Result<UserDto>.Success(_mapper.Map<UserDto>(user));
	}
}