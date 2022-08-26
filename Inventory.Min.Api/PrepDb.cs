using Inventory.Min.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Min.Api;

public static class PrepDb
{
    public static void PrepDatabase(WebApplicationBuilder builder)
    {
        using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
        {
            MigrateDb(serviceScope.ServiceProvider.GetService<InventoryDbContext>());
        }
    }

    private static void MigrateDb(InventoryDbContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        Console.WriteLine("Appling Migrations...");
        context.Database.Migrate();
    }
}