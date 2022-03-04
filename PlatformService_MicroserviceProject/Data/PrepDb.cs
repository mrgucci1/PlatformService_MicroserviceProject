using PlatformService_MicroserviceProject.Models;

namespace PlatformService_MicroserviceProject.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data...");
                context.Platforms.AddRange(
                    new Platform() { Name = "Jessie", Publisher = "Microsft", Cost = "Free" },
                    new Platform() { Name = "Bootise", Publisher = "Google", Cost = "Free" },
                    new Platform() { Name = "Chugie Plummie", Publisher = "Tesla", Cost = "20000" },
                    new Platform() { Name = "Wheaten", Publisher = "Tesla", Cost = "500" },
                    new Platform() { Name = "Baowsie", Publisher = "Tesla", Cost = "500" }
                    );
                context.SaveChanges();
            }
            else
                Console.WriteLine("We already have data");
        }
    }
}
