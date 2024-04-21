using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Api.Dtos
{
    public record UpdateCompanyDto(int Id, string Name,ICollection<DepartmentDto> Departments);
}