using HotelManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Middlewares;

public static class DatabaseMigrationMiddlewareExtensions
{
    public static IApplicationBuilder UseDatabaseMigrationMiddleware(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<HotelManagementDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        return app;
    }
}