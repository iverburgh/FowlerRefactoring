using FluentAssertions;
using System.Collections.Immutable;

namespace VideoStore.Console.Tests
{
    public class VideoStoreTests
    {
        private VideoStore _subject;

        private IImmutableDictionary<string, Play> _plays;
        private Invoice _invoice;

        public VideoStoreTests()
        {
            _plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", PayType.Tragedy) },
                { "as-like", new Play("As You Like It", PayType.Comedy) },
                { "othello", new Play("Othello", PayType.Tragedy) }
            }.ToImmutableDictionary();

            _invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40)
            });
            _subject = new VideoStore();
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
    }
}