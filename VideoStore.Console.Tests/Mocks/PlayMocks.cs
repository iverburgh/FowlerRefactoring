using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Console.Tests.Mocks
{
    internal static class PlayMocks
    {
        internal static IEnumerable<Models.Play> GetPlayList()
        {
            return new List<Models.Play>()
            {
                new ()
                {
                    Id = Guid.Parse("e42aae50-0d35-453d-8ba4-577aae3ccf68"),
                    Name = "Othello",
                    ShortName = "othello",
                    PayType = 0,
                },
                new ()
                {
                    Id = Guid.Parse("53b706f8-6156-43a1-95c9-32375171882d"),
                    Name = "As You Like It",
                    ShortName = "as-like",
                    PayType = 1,
                },
                new ()
                {
                    Id = Guid.Parse("3825ae15-e5c0-42a8-94d6-e53f0f5e8144"),
                    Name = "Hamlet",
                    ShortName = "hamlet",
                    PayType = 0,
                },
            };
        }
    }
}
