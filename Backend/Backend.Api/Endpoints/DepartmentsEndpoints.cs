using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Api.Data;
using Backend.Api.Dtos;
using Backend.Api.Entities;
using Backend.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Endpoints
{
    public static class DepartmentEndpoints
    {
        const string GetDepartmentEndpointName="GetDepartments";
        public static RouteGroupBuilder MapDepartmentEndpoints(this WebApplication app){
            var group=app.MapGroup("departments").WithParameterValidation();
            // GET /departments
            group.MapGet("/", async (HierarchyContext dbContext)=>{
                return await dbContext.Department.Include(group=>group.Groups).Select(department=>department.ToDto()).AsNoTracking().ToListAsync();
            });
            
            //GET /departments/1
            group.MapGet("/{id}", async (int id,HierarchyContext dbContext)=>{
                Department? department = await dbContext.Department.FindAsync(id);
                if(department is null){
                    return Results.NotFound();
                }
                await dbContext.Entry(department).Collection(d=>d.Groups).LoadAsync();
                
                return Results.Ok(department.ToDto());
            }).WithName(GetDepartmentEndpointName);
            //POST /departments
            group.MapPost("/", async (CreateDepartmentDto newDepartment,HierarchyContext dbContext)=>{
                Department department=newDepartment.ToEntity();
                var company=await dbContext.Company.FindAsync(newDepartment.CompanyID);
                if(company is null){ return Results.NotFound();};

                company.Departments!.Add(department);

                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(GetDepartmentEndpointName,new{Id=department.Id},
                department.ToDto());
            });
            //PUT /departments
            group.MapPut("/{id}", async (int id,UpdateDepartmentDto updatedDepartment,HierarchyContext dbContext)=>{
                Department? department=await dbContext.Department.FindAsync(id);
                if(department is null)
                { 
                    return Results.NotFound();
                };

                dbContext.Department.Entry(department)
                .CurrentValues
                .SetValues(updatedDepartment.ToEntity(id));


                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });
            //DELETE /departments/1
            group.MapDelete("/{id}",async (int id,HierarchyContext dbContext)=>
            {
                await dbContext.Company.Where(dep=>dep.Id==id).ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });
            return group;
        }
    }
}