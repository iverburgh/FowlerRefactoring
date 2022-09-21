namespace VideoStore.Console.Extensions
{
    internal static class PerformanceExtension
    {
        internal static Performance ToDomainPerformance(this Models.Performance performance)
        {
            return new Performance(performance.Play.ShortName, performance.Audience);
        }
    }
}
