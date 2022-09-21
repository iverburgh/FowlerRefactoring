using Microsoft.EntityFrameworkCore;
using VideoStore.Console;
using VideoStore.Console.Extensions;
using VideoStore.Console.Models;

IVideoStore videoStore = new VideoStore.Console.VideoStore();
var videoStoreContext = new VideoStoreContext();

var customerList = videoStoreContext.Customers
    .Include(c => c.Performances)
    .ThenInclude(p => p.Play)
    .ToList();
foreach (var customer in customerList)
{
    var invoice = new Invoice(customer.Name, customer.Performances.Select(cp => cp.ToDomainPerformance()));
    var playDictionary = videoStoreContext.Plays.ToDictionary(p => p.ShortName ?? string.Empty, p => new VideoStore.Console.Play(p.Name, (PayType)p.PayType));
    var result = videoStore.Statement(invoice, playDictionary);
    Console.WriteLine(result);
}