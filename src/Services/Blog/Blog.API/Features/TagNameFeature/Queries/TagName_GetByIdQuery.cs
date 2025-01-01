using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.API.Data;
using Blog.API.Features.TagNameFeature.Dtos;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.TagNameFeature.Queries
{
    
    public record TagName_GetByIdQuery(string Slug) : IQuery<Result<TagNameDto>>;
    public class TagName_GetByIdQueryHandler : IQueryHandler<TagName_GetByIdQuery, Result<TagNameDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TagName_GetByIdQueryHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<TagNameDto>> Handle(TagName_GetByIdQuery request, CancellationToken cancellationToken)
        {
            var tagsname = await _context.TagNames.Where(s => s.Slug == request.Slug)
                                       .ProjectTo<TagNameDto>(_mapper.ConfigurationProvider)
                                       .FirstOrDefaultAsync();
            return Result<TagNameDto>.Success(tagsname);
        }
    }
}
