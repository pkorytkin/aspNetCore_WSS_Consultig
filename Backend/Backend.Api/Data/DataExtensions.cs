using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Data;
public static class DataExtensions{
    public static async Task MigrateDbAsync(this WebApplication app){
        using var scope=app.Services.CreateScope();
        var dbContext=scope.ServiceProvider.GetService<HierarchyContext>();
        await dbContext!.Database.MigrateAsync();
    }
}