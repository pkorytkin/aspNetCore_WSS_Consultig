using Backend.Api.Entities;
using Backend.Api.Dtos;
namespace Backend.Api.Mapping;

public static class DepartmentMapping
{
    public static DepartmentDto ToDto(this Department entity)
    {
        List<GroupDto>? groups =entity.Groups!=null?entity.Groups.Select(x=>x.ToDto()).ToList():null;
        return new (entity.Id,entity.Name,groups!);
    }
    public static Department ToEntity(this DepartmentDto dto)
    {
        var groups=dto.Groups.Select((x)=>x.ToEntity()).ToList();
        Department department= new Department(){
            Id = dto.Id,
            Name = dto.Name,
            Groups=groups
        };

        return department;
        
    }
    public static Department ToEntity(this UpdateDepartmentDto dto,int Id)
    {
        var groups=dto.Groups.Select((x)=>x.ToEntity()).ToList();
        Department department= new Department(){
            Id = Id,
            Name = dto.Name,
            Groups=groups
        };

        return department;
        
    }
        public static Department ToEntity(this CreateDepartmentDto dto)
    {
        var groups=dto.Groups.Select((x)=>x.ToEntity()).ToList();
        Department department= new Department(){
            Name = dto.Name,
            Groups=groups
        };

        return department;
        
    }
}