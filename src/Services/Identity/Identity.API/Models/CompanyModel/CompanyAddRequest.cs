namespace Identity.API.Models.CompanyModel;

public class CompanyAddRequest : AddOrUpdateRequest
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
    public string? Wallpaper { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Introduction { get; set; } = string.Empty;
    public Guid SizeId { get; set; }
    public List<string> ProvinceIds { get; set; }
}
