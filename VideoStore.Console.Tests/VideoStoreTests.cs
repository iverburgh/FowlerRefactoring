using FluentAssertions;
using System.Collections.Immutable;
using Moq;
using VideoStore.Console.Models;
using VideoStore.Console.Persistence;
using VideoStore.Console.Tests.Mocks;

namespace VideoStore.Console.Tests
{
    public class VideoStoreTests
    {
        private VideoStore _subject;

        private readonly Mock<IRepository<Models.Customer>> _customerRepositoryMock;
        private readonly Mock<IRepository<Models.Performance>> _performanceRepositoryMock;
        private readonly Mock<IRepository<Models.Play>> _playRepositoryMock;

        private IImmutableDictionary<string, Play> _plays;
        private Invoice _invoice;

        public VideoStoreTests()
        {
            //_plays = new Dictionary<string, Play>
            //{
            //    { "hamlet", new Play("Hamlet", PayType.Tragedy) },
            //    { "as-like", new Play("As You Like It", PayType.Comedy) },
            //    { "othello", new Play("Othello", PayType.Tragedy) }
            //}.ToImmutableDictionary();

            //_invoice = new Invoice("BigCo", new List<Performance>
            //{
            //    new Performance("hamlet", 55),
            //    new Performance("as-like", 35),
            //    new Performance("othello", 40)
            //});

            _customerRepositoryMock = new Mock<IRepository<Models.Customer>>();
            _customerRepositoryMock
                .Setup(cr => cr.GetAll())
                .Returns(CustomerMocks.GetCustomerList());
            _performanceRepositoryMock = new Mock<IRepository<Models.Performance>>();
            _performanceRepositoryMock
                .Setup(pr => pr.GetAll())
                .Returns(PerformanceMocks.GetPerformanceList());
            _playRepositoryMock = new Mock<IRepository<Models.Play>>();
            _playRepositoryMock
                .Setup(pr => pr.GetAll())
                .Returns(PlayMocks.GetPlayList());

            _subject = new VideoStore(_customerRepositoryMock.Object, _performanceRepositoryMock.Object, _playRepositoryMock.Object);
        }

        [Fact]
        public void Statement_WhenCalled_ThenResultIsCorrect()
        {
            //arrange
            const string expectedResult = "Statement for BigCo\r\n" +
                                          "  Hamlet: $650.00 (55 seats)\r\n  As You Like It: $580.00 (35 seats)\r\n  Othello: $500.00 (40 seats)\r\n" +
                                          "Amount owed is $1,730.00\r\nYou earned 47 credits";
            //act
            var result = _subject.Statement(_invoice, _plays);
            //assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void GetInvoicesOfAllCustomers_WhenCalled_ThenResultIsCorrect()
        {
            //arrange
            const string expectedResult = "Statement for BigCo\r\n" +
                                          "  As You Like It: $580.00 (35 seats)\r\n  Hamlet: $650.00 (55 seats)\r\n  Othello: $500.00 (40 seats)\r\n" +
                                          "Amount owed is $1,730.00\r\nYou earned 47 credits";
            //act
            var result = _subject.GetInvoicesOfAllCustomers();
            //assert
            result.Should().Be(expectedResult);
        }

    }
}