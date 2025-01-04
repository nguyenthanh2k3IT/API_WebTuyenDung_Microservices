using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.PostSaveFeature.Dtos;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.PostSaveFeature.Queries
{
    
    public record PostSave_GetByIdQuery(Guid Id) : IQuery<Result<PostSaveDto>>;
    public class PostSave_GetByIdQueryHandler : IQueryHandler<PostSave_GetByIdQuery, Result<PostSaveDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostSave_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PostSaveDto>> Handle(PostSave_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var postsave = await _context.PostSaves.Where(s => s.Id == request.Id)
                                       .ProjectTo<PostSaveDto>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync();
            return Result<PostSaveDto>.Success(postsave);
        }
    }
}
