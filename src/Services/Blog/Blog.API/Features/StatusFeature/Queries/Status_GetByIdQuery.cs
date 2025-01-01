using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostFeature.Dtos;
using Blog.API.Features.StatusFeature.Dtos;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.StatusFeature.Queries
{

    public record Status_GetByIdQuery(string Slug) : IQuery<Result<StatusDto>>;
    public class Status_GetByIdQueryHandler : IQueryHandler<Status_GetByIdQuery, Result<StatusDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Status_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<StatusDto>> Handle(Status_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var posts = await _context.Statuses.Where(s => s.Slug == request.Slug)
                                       .ProjectTo<StatusDto>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync();
            return Result<StatusDto>.Success(posts);
        }
    }
}
