using Inventory.Min.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace Inventory.Min.Api;

public class ServicesRegister
{
    private readonly WebApplicationBuilder builder;

    public ServicesRegister(WebApplicationBuilder builder)
    {
        this.builder = builder;
    }

    public void RegisterServices()
    {
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        SetDbContextForLocalDb();
        //SetDbContextForDockerCompose();
        builder.Services.AddScoped<IItemRepo, ItemRepo<InventoryDbContext>>();
        builder.Services.AddScoped<IInventoryUnitOfWork, InventoryUnitOfWork<InventoryDbContext>>();
        builder.Services.AddControllers().AddNewtonsoftJson(
            s=>s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
    }

    private void SetDbContextForLocalDb()
    {
        var dbConfig = builder.Configuration.GetSection("DbConfig").Get<DbConfig>();
        var dbName = dbConfig?.DbNames!["MassTest"];
        if(string.IsNullOrWhiteSpace(dbName)) 
            throw new ArgumentException("Error: dbName is not configured!");
        builder.Services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={dbName}"));
    }

    private void SetDbContextForDockerCompose()
    {
        var server = builder.Configuration["DbServer"] ?? "localhost";
        var port = builder.Configuration["DbPort"] ?? "1433";
        var user = builder.Configuration["DbUser"] ?? "SA";
        var password = builder.Configuration["DbPassword"] ?? "Pa55w0rd2019";
        var database = builder.Configuration["Database"] ?? "InventoryMinData";

        builder.Services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID ={user};Password={password}"));
        PrepDb.PrepDatabase(builder);
    }
}