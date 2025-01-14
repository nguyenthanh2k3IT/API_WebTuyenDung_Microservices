using AutoMapper;
using Blog.API.Data;
using BuildingBlock.Core.Request;
using BuildingBlock.Core.Result;
using BuildingBlock.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Features.CategoryFeature.Commands
{
    public record Categories_DeleteCommand(DeleteRequest RequestData):ICommand<Result<bool>>;
    public class Categories_DeleteCommandHandler : ICommandHandler<Categories_DeleteCommand, Result<bool>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Categories_DeleteCommandHandler(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(Categories_DeleteCommand request, CancellationToken cancellationToken)
        {
            if (request.RequestData.Ids == null)
                throw new ApplicationException("Ids not found");

            var ids = request.RequestData.Ids.Select(s => Guid.Parse(s)).ToList();
            var query = await _context.Categories.Where(m => ids.Contains(m.Id)).ToListAsync();
            if (query == null || query.Count == 0) throw new ApplicationException($"Không tìm thấy trong dữ liệu có Id: {string.Join(";", request.RequestData.Ids)}");

            foreach (var item in query)
            {
                item.DeleteFlag = true;
                item.ModifiedDate = DateTime.Now;
                item.ModifiedUser = request.RequestData.ModifiedUser;
            }

            _context.Categories.UpdateRange(query);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }

   
}
