using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VideoStore.Console.Extensions;
using VideoStore.Console.Models;
using VideoStore.Console.Persistence;

namespace VideoStore.Console
{
    public class VideoStore : IVideoStore
    {
        private readonly IRepository<Models.Customer> _customerRepository;
        private readonly IRepository<Models.Performance> _performanceRepository;
        private readonly IRepository<Models.Play> _playRepository;

        public VideoStore(
            IRepository<Customer> customerRepository,
            IRepository<Models.Performance> performanceRepository,
            IRepository<Models.Play> playRepository)
        {
            _customerRepository = customerRepository;
            _performanceRepository = performanceRepository;
            _playRepository = playRepository;
        }

        public string GetInvoicesOfAllCustomers()
        {
            var invoicesOfAllCustomers = string.Empty;
            var customerList = _customerRepository.GetAll();
            foreach (var customer in customerList)
            {
                var invoice = new Invoice(customer.Name, _performanceRepository.GetAll().Select(cp => cp.ToDomainPerformance()));
                var playDictionary = _playRepository.GetAll().ToDictionary(p => p.ShortName ?? string.Empty,
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
                var amountForPerformance = GetAmountForPerformance(play.PayType, perf.Audience);
                volumeCredits += GetVolumeCreditsOfPerformance(perf.Audience, play.PayType);

                // print line for this order
                result.Append($"  {play.Name}: {(amountForPerformance / 100).ToString("C", format)}");
                result.AppendLine($" ({perf.Audience} seats)");
                totalAmount += amountForPerformance;
            }

            result.Append(format, $"Amount owed is {(totalAmount / 100).ToString("C", format)}");
            result.AppendLine("");
            result.Append($"You earned {volumeCredits} credits");
            return result.ToString();
        }

        private static int GetVolumeCreditsOfPerformance(int audience, PayType payType)
        {
            var volumeCredits = Math.Max(audience - 30, 0);
            if (PayType.Comedy == payType)
            {
                volumeCredits += (int)Math.Floor((decimal)audience / 5);
            }
            return volumeCredits;
        }

        private static int GetAmountForPerformance(PayType payType, int audience)
        {
            var amountForPerformance = 0;
            switch (payType)
            {
                case PayType.Tragedy:
                    amountForPerformance = 40000;
                    if (audience > 30) amountForPerformance += 1000 * (audience - 30);

                    return amountForPerformance;
                case PayType.Comedy:
                    amountForPerformance = 30000;
                    if (audience > 20) amountForPerformance += 10000 + 500 * (audience - 20);
                    amountForPerformance += 300 * audience;
                    return amountForPerformance;
                default:
                    throw new Exception($"unknown type: {payType}");
            }
        }
    }
}