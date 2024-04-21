using Backend.Api.Data;
using Backend.Api.Endpoints;
var builder = WebApplication.CreateBuilder(args);

var connectionString=builder.Configuration.GetConnectionString("DB");

builder.Services.AddSqlite<HierarchyContext>(connectionString);



var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapCompanyEndpoints();
app.MapDepartmentEndpoints();
//app.MapGroupEndpoints();
await app.MigrateDbAsync();
app.Run();
