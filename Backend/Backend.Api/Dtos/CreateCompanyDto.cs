using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Api.Dtos
{
    public record CreateCompanyDto(string Name,ICollection<DepartmentDto> Departments);
}