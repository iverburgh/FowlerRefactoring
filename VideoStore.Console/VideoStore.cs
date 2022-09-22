using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VideoStore.Console.Extensions;
using VideoStore.Console.Models;

namespace VideoStore.Console
{
    public class VideoStore : IVideoStore
    {
        public string GetInvoicesOfAllCustomers()
        {
            var invoicesOfAllCustomers = string.Empty;
            var videoStoreContext = new VideoStoreContext();
            var customerList = videoStoreContext.Customers
                .Include(c => c.Performances)
                .ThenInclude(p => p.Play)
                .ToList();
            foreach (var customer in customerList)
            {
                var invoice = new Invoice(customer.Name, customer.Performances.Select(cp => cp.ToDomainPerformance()));
                var playDictionary = videoStoreContext.Plays.ToDictionary(p => p.ShortName ?? string.Empty,
                    p => new Play(p.Name, (PayType)p.PayType));
                invoicesOfAllCustomers += Statement(invoice, playDictionary);
            }
            return invoicesOfAllCustomers;
        }

        public string Statement(Invoice invoice, IReadOnlyDictionary<string, Play> plays)
        {
            int totalAmount = 0;
            int volumeCredits = 0;

            IFormatProvider format = new CultureInfo("en-US");

            var result = new StringBuilder().AppendLine($"Statement for {invoice.Customer}");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                int thisAmount;

                thisAmount = GetAmountForPerformance(play.PayType, perf.Audience);

                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if (PayType.Comedy == play.PayType) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                result.Append($"  {play.Name}: {(thisAmount / 100).ToString("C", format)}");
                result.AppendLine($" ({perf.Audience} seats)");
                totalAmount += thisAmount;
            }

            result.Append(format, $"Amount owed is {(totalAmount / 100).ToString("C", format)}");
            result.AppendLine("");
            result.Append($"You earned {volumeCredits} credits");
            return result.ToString();
        }

        private static int GetAmountForPerformance(PayType payType, int audience)
        {
            int amountForPerformance;
            switch (payType)
            {
                case PayType.Tragedy:
                    amountForPerformance = 40000;
                    if (audience > 30) amountForPerformance += 1000 * (audience - 30);

                    break;

                case PayType.Comedy:
                    amountForPerformance = 30000;
                    if (audience > 20) amountForPerformance += 10000 + 500 * (audience - 20);
                    amountForPerformance += 300 * audience;
                    break;

                default:
                    throw new Exception($"unknown type: {payType}");
            }
            return amountForPerformance;
        }
    }
}