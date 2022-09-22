using VideoStore.Console.Models;

namespace VideoStore.Console.Tests.Mocks
{
    internal static class CustomerMocks
    {
        internal static IEnumerable<Customer> GetCustomerList()
        {
            return new List<Customer>()
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = "BigCo",
                }
            };
        }
    }
}