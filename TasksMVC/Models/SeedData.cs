using Microsoft.EntityFrameworkCore;
using TasksMVC.Data;
using Task = TasksMVC.Models.Work;


namespace TasksMVC.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WorkContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WorkContext>>()))
            {
                // Look for any movies.
                if (context.Works.Any())
                {
                    return;   // DB has been seeded
                }

                context.Works.AddRange(
                    new Work
                    {
                        Name = "When Harry Met Sally",
                        CreatedDate = DateTime.Parse("1989-2-12"),
                        HandleDate = DateTime.Parse("1989-2-13"),
                        Description = "Romantic Comedy",
                        Status = TaskStatus.Done
                    },
                    new Work
                    {
                        Name = "new 2",
                        CreatedDate = DateTime.Parse("2022-2-12"),
                        HandleDate = DateTime.Parse("2022-2-13"),
                        Description = "Romantic Comedy",
                        Status = TaskStatus.Need
                    },
                    new Work
                    {
                        Name = "choi game",
                        CreatedDate = DateTime.Parse("2022-4-12"),
                        HandleDate = DateTime.Parse("2022-4-13"),
                        Description = "choi game chơi gaem",
                        Status = TaskStatus.Need
                    },
                    new Work
                    {
                        Name = "hoc bai",
                        CreatedDate = DateTime.Parse("2022-5-12"),
                        HandleDate = DateTime.Parse("2022-5-13"),
                        Description = "van la choi game nhung de ten khac",
                        Status = TaskStatus.Need
                    }

                  
                );
                context.SaveChanges();
            }
        }
    }
}
