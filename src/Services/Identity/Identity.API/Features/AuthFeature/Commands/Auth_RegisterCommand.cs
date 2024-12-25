using Identity.API.Commons.Validators;
using Identity.API.Interfaces;
using Identity.API.Models.AuthModel;

namespace Identity.API.Features.AuthFeature.Commands;

public record Auth_RegisterCommand(RegisterRequest RequestData) : ICommand<Result<bool>>;

public class RegisterCommandValidator : AbstractValidator<Auth_RegisterCommand>
{
	public RegisterCommandValidator()
	{
		RuleFor(command => command.RequestData.Email).EmailRule();

		RuleFor(command => command.RequestData.Password).PasswordRule();

		RuleFor(command => command.RequestData.Phone).PhoneRule();

		RuleFor(command => command.RequestData.Fullname).FullnameRule();
	}
}

public class Auth_RegisterCommandHandler : ICommandHandler<Auth_RegisterCommand, Result<bool>>
{
	private readonly DataContext _context;
	//private readonly IEventBus _eventBus;
	private readonly IUserService _userService;

	public Auth_RegisterCommandHandler(
		DataContext context,
		//IEventBus eventBus,
		IUserService userService)
	{
		_context = context;
		//_eventBus = eventBus;
		_userService = userService;
	}

	public async Task<Result<bool>> Handle(Auth_RegisterCommand request, CancellationToken cancellationToken)
	{
		await _userService.CheckValidEmail(request.RequestData.Email);

		Guid userId = Guid.NewGuid();
		var user = new User()
		{
			Id = userId,
			Email = request.RequestData.Email,
			Password = request.RequestData.Password,
			Fullname = request.RequestData.Fullname,
			Phone = request.RequestData.Phone,
			StatusId = UserStatusEnum.UNACTIVE,
			RoleId = RoleEnum.JOBSEEKER,
			CreatedUser = userId,
			ModifiedUser = userId
		};

		_context.Users.Add(user);
		int rows = await _context.SaveChangesAsync();
		/*if(rows > 0)
		{
			await _eventBus.PublishAsync(new PushEmailEvent()
			{
				Email = user.Email,
				UserId = user.Id,
				Type = OTPConstant.RegisterType
			});
		}*/

		return Result<bool>.Success(true);
	}
}
