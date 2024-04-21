using Backend.Api.Entities;
using Backend.Api.Dtos;
namespace Backend.Api.Mapping;

public static class GroupMapping
{
    public static GroupDto ToDto(this Group entity)
    {
        return new (entity.Id,entity.Name);
    }
    public static Group ToEntity(this GroupDto dto)
    {
        return new (){Id=dto.Id,Name=dto.Name};
    }
}