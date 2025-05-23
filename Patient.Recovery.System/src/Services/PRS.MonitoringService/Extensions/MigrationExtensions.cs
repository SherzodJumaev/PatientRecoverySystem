using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRS.Shared.Infrastructure.Data;

namespace PRS.MonitoringService.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using MonitoringDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<MonitoringDbContext>();

            try
            {
                dbContext.Database.EnsureCreated();
                Console.WriteLine("Migration successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("Migration failed" + $"\n{e.Message}" + $"\n{e.InnerException}");
            }
        }
    }
}