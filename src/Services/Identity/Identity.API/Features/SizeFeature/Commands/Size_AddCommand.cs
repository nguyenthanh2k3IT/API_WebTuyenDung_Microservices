using Identity.API.Features.SizeFeature.Dto;

namespace Identity.API.Features.SizeFeature.Commands;

public record Size_AddCommand(Size RequestData) : ICommand<Result<SizeDto>>;

public class SizeAddCommandValidator : AbstractValidator<Size_AddCommand>
{
	public SizeAddCommandValidator()
	{
		RuleFor(command => command.RequestData.Name)
			.NotEmpty().WithMessage("Tên quy mô không được để trống");

        RuleFor(command => command.RequestData.Value)
            .NotEmpty().WithMessage("Giá trị không được để trống");
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

		var size = new Size()
		{
			Id = Guid.NewGuid(),
			Name = request.RequestData.Name,
			Value = request.RequestData.Value,
			CreatedUser = request.RequestData.CreatedUser,
			ModifiedUser = request.RequestData.CreatedUser
		};

		_context.Sizes.Add(size);

		await _context.SaveChangesAsync(cancellationToken);

		return Result<SizeDto>.Success(_mapper.Map<SizeDto>(size));
	}
}