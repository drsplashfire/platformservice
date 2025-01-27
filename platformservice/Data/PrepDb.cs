using Microsoft.Extensions.Logging;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PrepDb
    {
       
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<PrepDb>>();
                SeedData(dbContext,logger);
            }
        }
        public static void SeedData(AppDbContext dbContext,ILogger<PrepDb>  logger) 
        {
            if (!dbContext.Platforms.Any()) 
            {
                logger.LogInformation("Adding data");
                dbContext.AddRange(
                    new Platform() {Name = "DotNet",Cost = "free", Publisher = "Microsoft" },
                    new Platform() {Name = "Docker",Cost = "free", Publisher = "Microsoft" },
                    new Platform() {Name = "mine",Cost = "money", Publisher = "Mikey" });

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
