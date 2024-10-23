using AutoMapper;
using AutoMapper.QueryableExtensions;
using MappingByAutoMapper.Entities;
using MappingByAutoMapper.Filter;
using MappingByAutoMapper.Response;

namespace MappingByAutoMapper.Service;

public class PersonService(DataContext context, IMapper mapper):IPersonService
{
    public PaginationResponse<IEnumerable<ReadPerson>> GetPeople(PersonFilter filter)
    {
        IQueryable<Person> people = context.People;
        if (filter.Name != null)
            people = people.Where(x => x.Name.ToLower()
                .Contains(filter.Name.ToLower()));
        if (filter.Age != null)
            people = people.Where(x => x.Age == filter.Age);
        if (filter.Email != null)
            people = people.Where(x => x.Email.ToLower()
                .Contains(filter.Email.ToLower()));
        if (filter.PhoneNumber != null)
            people = people.Where(x => x.PhoneNumber.ToLower()
                .Contains(filter.PhoneNumber.ToLower()));
        IQueryable<ReadPerson> result = people.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ProjectTo<ReadPerson>(mapper.ConfigurationProvider);
        int totalRecords = context.People.Count();
        return PaginationResponse<IEnumerable<ReadPerson>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public ReadPerson? GetPersonById(int id)
    {
        var person = (from u in context.People
            where u.IsDeleted == false
            select new ReadPerson()
            {
                Id = u.Id,
                Name = u.Name,
                Age = u.Age,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).Where(x=>x.Id==id).ProjectTo<ReadPerson>(mapper.ConfigurationProvider).FirstOrDefault();
        return person;
    }

    public bool CreatePerson(CreatePerson person)
    {
        bool existUser = context.People.Any(x =>
            x.Name.ToLower() == person.Name.ToLower() && x.IsDeleted == false);
        if (existUser) return false;
        context.People.Add(mapper.Map<Person>(person));
        context.SaveChanges();
        return true;
    }

    public bool UpdatePerson(UpdatePerson person)
    {
        Person? existingPerson = context.People.FirstOrDefault(x => x.IsDeleted == false && x.Id == person.Id);
        if (existingPerson is null) return false;
        context.People.Update(mapper.Map(person, existingPerson));
        context.SaveChanges();
        return true;
    }

    public bool DeletePerson(int id)
    {
        Person? existingPerson = context.People.FirstOrDefault(x => x.Id == id);
        if (existingPerson is null) return false;
        existingPerson.IsDeleted = true;
        existingPerson.DeletedAt = DateTime.UtcNow;
        existingPerson.UpdatedAt = DateTime.UtcNow;
        context.SaveChanges();
        return true;
    }
}