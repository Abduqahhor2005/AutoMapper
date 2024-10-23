using MappingByAutoMapper.Filter;
using MappingByAutoMapper.Response;

namespace MappingByAutoMapper.Service;

public interface IPersonService
{
    PaginationResponse<IEnumerable<ReadPerson>> GetPeople(PersonFilter filter);
    ReadPerson? GetPersonById(int id);
    bool CreatePerson(CreatePerson person);
    bool UpdatePerson(UpdatePerson person);
    bool DeletePerson(int id);
}