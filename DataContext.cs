using MappingByAutoMapper.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace MappingByAutoMapper;

public class DataContext(DbContextOptions<DataContext> options):DbContext(options)
{
    public DbSet<Person> People { get; set; }
}