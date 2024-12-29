using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.SizeFeature.Commands;

public record Size_AddCommand(Size RequestData) : ICommand<Result<SizeDto>>;

public class SizeAddCommandValidator : AbstractValidator<Size_AddCommand>
{
	public SizeAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Id)
			.NotEmpty().WithMessage("Id is required");

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");
	}
}

public class Size_AddCommandHandler : ICommandHandler<Size_AddCommand, Result<SizeDto>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Size_AddCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_AddCommand request, CancellationToken cancellationToken)
	{
		var exist = await _context.Sizes.FindAsync(request.RequestData.Id);

		if(exist != null)
		{
			throw new ApplicationException("Size id is already in uses");
		}

		var Size = new Size()
		{
			Id = Guid.NewGuid(),
			Name = request.RequestData.Name,
			Value = request.RequestData.Value,
			CreatedUser = request.RequestData.CreatedUser,
			ModifiedUser = request.RequestData.CreatedUser
		};

		_context.Sizes.Add(Size);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(Size));
	}
}