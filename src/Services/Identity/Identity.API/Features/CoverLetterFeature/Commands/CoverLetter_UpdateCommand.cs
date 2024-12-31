using Identity.API.Features.CoverLetterFeature.Dtos;

namespace Identity.API.Features.CoverLetterFeature.Commands;

public record CoverLetter_UpdateCommand(CoverLetter RequestData) : ICommand<Result<CoverLetterDto>>;

public class CoverLetterUpdateCommandValidator : AbstractValidator<CoverLetter_UpdateCommand>
{
    public CoverLetterUpdateCommandValidator()
    {
        RuleFor(command => command.RequestData.Id)
            .NotEmpty().WithMessage("Không tìm thấy ID");

        RuleFor(command => command.RequestData.Name)
            .NotEmpty().WithMessage("Tên thư ứng tuyển được để trống");

        RuleFor(command => command.RequestData.Content)
            .NotEmpty().WithMessage("Nội dung thư ứng tuyển không được để trống");
    }
}

public class CoverLetter_UpdateCommandHandler : ICommandHandler<CoverLetter_UpdateCommand, Result<CoverLetterDto>>
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CoverLetter_UpdateCommandHandler(IMapper mapper, DataContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CoverLetterDto>> Handle(CoverLetter_UpdateCommand request, CancellationToken cancellationToken)
    {
        var CoverLetter = await _context.CoverLetters.FindAsync(request.RequestData.Id);

        if (CoverLetter == null)
        {
            throw new ApplicationException("Không tìm thấy quy mô công ty");
        }

        CoverLetter.Name = request.RequestData.Name;
        CoverLetter.Content = request.RequestData.Content;
        CoverLetter.ModifiedDate = DateTime.Now;
        CoverLetter.ModifiedUser = request.RequestData.ModifiedUser;

        _context.CoverLetters.Update(CoverLetter);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<CoverLetterDto>.Success(_mapper.Map<CoverLetterDto>(CoverLetter));
    }
}