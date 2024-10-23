namespace MappingByAutoMapper.Filter;

public record PersonFilter : BaseFilter
{
    public int? Age { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
}