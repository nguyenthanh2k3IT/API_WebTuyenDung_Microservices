namespace Job.Application.Common;

public class BaseDto<TKey>
{
    public TKey Id { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
}
