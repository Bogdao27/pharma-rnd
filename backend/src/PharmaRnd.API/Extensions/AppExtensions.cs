using Microsoft.EntityFrameworkCore;
using PharmaRnd.Infrastructure;

namespace PharmaRnd.API.Extensions
{
    public static class AppExtensions
    {
        public static async Task ApplyMigrations(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
