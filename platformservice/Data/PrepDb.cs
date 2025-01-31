using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PrepDb
    {
       
        public static void PrepPopulation(IApplicationBuilder app ,bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<PrepDb>>();
                SeedData(dbContext,logger, true);
                //SeedData(dbContext,logger, isProd);
            }
        }
        public static void SeedData(AppDbContext dbContext,ILogger<PrepDb>  logger, bool isProd) 
        {
            if (isProd) 
            {
                Console.WriteLine("--> Attempting to Migrate" );
                try
                {
                    dbContext.Database.Migrate( );
                }
                catch ( Exception ex )
                {
                    Console.WriteLine($"not able to apply migration {ex.Message}");
                }
            }
            
            if (!dbContext.Platforms.Any())
            {
                logger.LogInformation("Adding data");
                dbContext.AddRange(
                    new Platform() { Name = "DotNet", Cost = "free", Publisher = "Microsoft" },
                    new Platform() { Name = "Docker", Cost = "free", Publisher = "Microsoft" },
                    new Platform() { Name = "mine", Cost = "money", Publisher = "Mikey" },
                    new Platform() { Name = "mine", Cost = "money", Publisher = "Mikey" });

                dbContext.SaveChanges();
                logger.LogInformation("Saving data");

            }
            else
            {
                logger.LogInformation("We have data");
            }
          
        }

    }
}
