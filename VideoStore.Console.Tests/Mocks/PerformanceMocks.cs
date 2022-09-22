using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Console.Tests.Mocks
{
    internal static class PerformanceMocks
    {
        internal static IEnumerable<Models.Performance> GetPerformanceList()
        {
            return new List<Models.Performance>()
            {
                new Models.Performance()
                {
                    Id   = Guid.NewGuid(),
                    Audience = 35,
                    Play = new Models.Play()
                    {
                        Id = Guid.Parse("53b706f8-6156-43a1-95c9-32375171882d"),
                        PayType = 1,
                        Name = "As You Like It",
                        ShortName = "as-like",
                    }
                },
                new Models.Performance()
                {
                    Id   = Guid.NewGuid(),
                    Audience = 55,
                    Play = new Models.Play()
                    {
                        Id = Guid.Parse("3825ae15-e5c0-42a8-94d6-e53f0f5e8144"),
                        PayType = 0,
                        Name = "Hamlet",
                        ShortName = "hamlet",
                    }
                },
                new Models.Performance()
                {
                 Id   = Guid.NewGuid(),
                 Audience = 40,
                 Play = new Models.Play()
                 {
                     Id = Guid.Parse("e42aae50-0d35-453d-8ba4-577aae3ccf68"),
                     PayType = 0,
                     Name = "Othello",
                     ShortName = "othello",
                 }
                },
            };
        }
    }
}
