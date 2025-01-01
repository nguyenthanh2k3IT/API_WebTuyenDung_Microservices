using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Features.StatusFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using FluentValidation;

namespace Blog.API.Features.StatusFeature.Commands
{
  
    public record  Status_AddCommand(StatusRequest RequestData) : ICommand<Result<StatusDto>>;
    public class StatusAddCommandValidator : AbstractValidator<Status_AddCommand>
    {
        public StatusAddCommandValidator()
        {
            RuleFor(command => command.RequestData.Slug)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(command => command.RequestData.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
    public class  Status_AddCommandHandler : ICommandHandler< Status_AddCommand, Result<StatusDto>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public  Status_AddCommandHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<Result<StatusDto>> Handle( Status_AddCommand request, CancellationToken cancellationToken)
        {
            var exist = await _dataContext.Statuses.FindAsync(request.RequestData.Slug);

            if (exist != null)
            {
                throw new ApplicationException("Slug is already in uses");
            }
            var Status= new Status()
            {
                Slug = request.RequestData.Slug,
                Name=request.RequestData.Name

            };
            _dataContext.Statuses.Add(Status);
            await _dataContext.SaveChangesAsync(cancellationToken);
            return Result<StatusDto>.Success(_mapper.Map<StatusDto>(Status));

        }
    }
}
