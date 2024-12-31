using Identity.API.Features.CoverLetterFeature.Dtos;

namespace Identity.API.Features.CoverLetterFeature.Commands;

public record CoverLetter_AddCommand(CoverLetter RequestData) : ICommand<Result<CoverLetterDto>>;

public class CoverLetterAddCommandValidator : AbstractValidator<CoverLetter_AddCommand>
{
    public CoverLetterAddCommandValidator()
    {
        RuleFor(command => command.RequestData.Name)
            .NotEmpty().WithMessage("Tên thư ứng tuyển được để trống");

        RuleFor(command => command.RequestData.Content)
            .NotEmpty().WithMessage("Nội dung thư ứng tuyển không được để trống");
    }
}

public class CoverLetter_AddCommandHandler : ICommandHandler<CoverLetter_AddCommand, Result<CoverLetterDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CoverLetter_AddCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CoverLetterDto>> Handle(CoverLetter_AddCommand request, CancellationToken cancellationToken)
    {

        var CoverLetter = new CoverLetter()
        {
            Id = Guid.NewGuid(),
            Name = request.RequestData.Name,
            Content = request.RequestData.Content,
            UserId = request.RequestData.UserId,
            CreatedUser = request.RequestData.CreatedUser,
            ModifiedUser = request.RequestData.CreatedUser
        };

        _context.CoverLetters.Add(CoverLetter);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<CoverLetterDto>.Success(_mapper.Map<CoverLetterDto>(CoverLetter));
    }
}