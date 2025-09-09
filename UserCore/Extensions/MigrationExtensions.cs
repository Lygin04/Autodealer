using Microsoft.EntityFrameworkCore;
using UserCore.Data;

namespace UserCore.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using DataContext dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        dataContext.Database.Migrate();
    }
}