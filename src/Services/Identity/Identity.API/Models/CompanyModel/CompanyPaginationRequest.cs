namespace Identity.API.Models.CompanyModel;

public class CompanyPaginationRequest : PaginationRequest
{
    public Guid SizeId { get; set; }
}
