using Backend.Api.Entities;
using Backend.Api.Dtos;
namespace Backend.Api.Mapping;

public static class CompanyMapping
{
    public static CompanyDto ToDto(this Company entity)
    {
        List<DepartmentDto>? Departments =entity.Departments!=null?
        entity.Departments.Select(x=>x.ToDto()).ToList():null;
        return new CompanyDto(entity.Id,entity.Name,Departments!);
    }
    public static Company ToEntity(this CreateCompanyDto dto)
    {
        var departments=dto.Departments.Select((x)=>x.ToEntity()).ToList();
        Company company= new Company(){
            Name = dto.Name,
            Departments=departments
        };

        return company;
        
    }
    public static Company ToEntity(this UpdateCompanyDto dto,int Id)
    {
        var departments=dto.Departments.Select((x)=>x.ToEntity()).ToList();
        Company company= new Company(){
            Id=Id,
            Name = dto.Name,
            Departments=departments
        };

        return company;
        
    }
}