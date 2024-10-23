namespace MappingByAutoMapper;

public readonly record struct ReadPerson(int Id, string Name,int Age, string Email, string PhoneNumber);

public readonly record struct CreatePerson(string Name,int Age, string Email, string PhoneNumber);

public readonly record struct UpdatePerson(int Id,string Name,int Age, string Email, string PhoneNumber);