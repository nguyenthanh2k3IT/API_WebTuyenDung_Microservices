using AutoMapper;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Features.StatusFeature.Dtos;
using Blog.API.Models;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using FluentValidation;

namespace Blog.API.Features.StatusFeature.Commands
{
   
    public record Status_UpdateCommand(StatusRequest RequestData) : ICommand<Result<StatusDto>>;
    public class StatusUpdateCommandValidator : AbstractValidator<Status_UpdateCommand>
    {
        public StatusUpdateCommandValidator()
        {
            RuleFor(command => command.RequestData.Slug)
                .NotEmpty().WithMessage("Slug is required");

            RuleFor(command => command.RequestData.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
    public class Status_UpdateCommandHandler : ICommandHandler<Status_UpdateCommand, Result<StatusDto>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public Status_UpdateCommandHandler(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<Result<StatusDto>> Handle(Status_UpdateCommand request, CancellationToken cancellationToken)
        {
            var status = await _dataContext.Statuses.FindAsync(request.RequestData.Slug);

            if (status == null)
            {
                throw new ApplicationException("Post not found");
            }

          
            status.Name = request.RequestData.Name;
            status.ModifiedDate = DateTime.UtcNow;

            _dataContext.Statuses.Update(status);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Result<StatusDto>.Success(_mapper.Map<StatusDto>(status));
        }
    }
}
