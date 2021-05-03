using System;
using Xunit;
using Pyramid;

namespace Tests
{
    public class LexerTests
    {
        [Fact]
        public void SampleTest()
        {
            var lexer = new Lexer("    42    + 313");

            var isInt = lexer.TryReadInt(out var factor);
            Assert.True(isInt);
            Assert.Equal(42, factor);

            Assert.False(lexer.TryReadChar('-'));
            Assert.True(lexer.TryReadChar('+'));

            isInt = lexer.TryReadInt(out factor);
            Assert.True(isInt);
            Assert.Equal(313, factor);

            Assert.False(lexer.TryReadChar('-'));
            Assert.False(lexer.TryReadChar('+'));
        }
    }
}
