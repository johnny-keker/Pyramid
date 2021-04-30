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

            var next = lexer.Next();
            Assert.Equal(TokenType.INT, next.Type);
            Assert.Equal("42", next.Value);

            next = lexer.Next();
            Assert.Equal(TokenType.PLUS, next.Type);
            Assert.Equal("+", next.Value);

            next = lexer.Next();
            Assert.Equal(TokenType.INT, next.Type);
            Assert.Equal("313", next.Value);

            next = lexer.Next();
            Assert.Equal(TokenType.EOF, next.Type);
            Assert.Null(next.Value);

            next = lexer.Next();
            Assert.Equal(TokenType.EOF, next.Type);
            Assert.Null(next.Value);
        }
    }
}
