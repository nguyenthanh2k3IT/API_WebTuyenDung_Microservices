using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.SizeFeature.Commands;

public record Size_UpdateCommand(Size RequestData) : ICommand<Result<SizeDto>>;

public class SizeUpdateCommandValidator : AbstractValidator<Size_UpdateCommand>
{
	public SizeUpdateCommandValidator()
	{

		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Name is required");

        RuleFor(command => command.RequestData.Value)
            .NotEmpty().WithMessage("Value is required");
    }
}

public class Size_UpdateCommandHandler : ICommandHandler<Size_UpdateCommand, Result<SizeDto>>
{

	private readonly DataContext _context;
	private readonly IMapper _mapper;

	public Size_UpdateCommandHandler(IMapper mapper, DataContext context)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<SizeDto>> Handle(Size_UpdateCommand request, CancellationToken cancellationToken)
	{
		var Size = await _context.Sizes.FindAsync(request.RequestData.Id);

		if (Size == null)
		{
			throw new ApplicationException("Size not found");
		}

		Size.Name = request.RequestData.Name;
		Size.Value = request.RequestData.Value;
		Size.ModifiedDate = DateTime.Now;
		Size.ModifiedUser = request.RequestData.ModifiedUser;

		_context.Sizes.Update(Size);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(Size));
	}
}