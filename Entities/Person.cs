namespace MappingByAutoMapper.Entities;

public class Person : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}