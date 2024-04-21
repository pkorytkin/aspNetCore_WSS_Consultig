using Microsoft.EntityFrameworkCore;
using Backend.Api.Entities;
namespace Backend.Api.Data;
public class HierarchyContext:
DbContext
{
    public DbSet<Company> Company => Set<Company>();
    public DbSet<Department> Department => Set<Department>();

    public DbSet<Group> Group => Set<Group>();
    public HierarchyContext(DbContextOptions<HierarchyContext> options):
    base(options)
    {
        
    }
}