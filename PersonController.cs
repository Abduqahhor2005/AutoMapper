using MappingByAutoMapper.Filter;
using MappingByAutoMapper.Response;
using MappingByAutoMapper.Service;
using Microsoft.AspNetCore.Mvc;

namespace MappingByAutoMapper;

[ApiController]
[Route("api/people")]
public class PersonController(IPersonService personService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetCourses([FromQuery] PersonFilter filter)
        => Ok(ApiResponse<PaginationResponse<IEnumerable<ReadPerson>>>.Success(null,
            personService.GetPeople(filter)));
    [HttpGet("{id:int}")]
    public IActionResult GetPersonById(int id)
    {
        ReadPerson? res = personService.GetPersonById(id);
        return res != null
            ? Ok(ApiResponse<ReadPerson?>.Success(null, res))
            : NotFound(ApiResponse<ReadPerson?>.Fail(null, null));
    }

    [HttpPost]
    public IActionResult CreatePerson([FromBody] string name,int age, string email, string phoneNumber)
    {
        CreatePerson person = new CreatePerson(name, age, email,phoneNumber);
        bool res = personService.CreatePerson(person);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : BadRequest(ApiResponse<bool>.Fail(null, res));
    }

    [HttpPut]
    public IActionResult UpdateCourse(UpdatePerson person)
    {
        bool res = personService.UpdatePerson(person);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePerson(int id)
    {
        bool res = personService.DeletePerson(id);
        return res
            ? Ok(ApiResponse<bool>.Success(null, res))
            : NotFound(ApiResponse<bool>.Fail(null, res));
    }
}