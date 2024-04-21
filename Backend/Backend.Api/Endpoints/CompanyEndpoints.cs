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
    public static class CompanyEndpoints
    {
        const string GetCompanyEndpointName="GetCompany";
        public static RouteGroupBuilder MapCompanyEndpoints(this WebApplication app){
            var group=app.MapGroup("companys").WithParameterValidation();
            // GET /companys
            group.MapGet("/", async (HierarchyContext dbContext)=>{
                return await dbContext.Company
                .Include(company=>company.Departments).ThenInclude(g=>g.Groups)
                .Select(company=>company.ToDto()).ToListAsync();
            });
            
            //GET /companys/1
            group.MapGet("/{id}", async (int id,HierarchyContext dbContext)=>{
                Company? company = await dbContext.Company.FindAsync(id);
                if(company is null){
                    return Results.NotFound();
                }
                await dbContext.Entry(company).Collection(d=>d.Departments).LoadAsync();
                //company.Departments.Select();
                return company is null? Results.NotFound():Results.Ok(company.ToDto());
            }).WithName(GetCompanyEndpointName);
            //POST /companys
            group.MapPost("/", async (CreateCompanyDto newCompany,HierarchyContext dbContext)=>{
                Company company=newCompany.ToEntity();
                dbContext.Company.Add(company);

                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute(GetCompanyEndpointName,new {id=company.Id},company.ToDto());
            });
            //PUT /companys
            group.MapPut("/{id}", async (int id,UpdateCompanyDto updatedCompany,HierarchyContext dbContext)=>{
                Company? company=await dbContext.Company.FindAsync(id);
                if(company is null)
                { 
                    return Results.NotFound();
                };

                dbContext.Company.Entry(company)
                .CurrentValues
                .SetValues(updatedCompany.ToEntity(id));


                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });
            //DELETE /companys/1
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