using System;
using Xunit;
using Pyramid;

namespace Tests
{
    public class ParserTests
    {
        [Fact]
        public void ASTBuildingTest()
        {
            var parser = new Parser("    42    + 313");
            var ast = parser.Parse();

            Assert.Equal(TokenType.PLUS, ast.Token.Type);
            Assert.Equal(TokenType.INT, ast.Left.Token.Type);
            Assert.Equal("42", ast.Left.Token.Value);
            Assert.Equal(TokenType.INT, ast.Right.Token.Type);
            Assert.Equal("313", ast.Right.Token.Value);

            Assert.Null(ast.Left.Left);
            Assert.Null(ast.Left.Right);
            Assert.Null(ast.Right.Left);
            Assert.Null(ast.Right.Right);
        }

        [Fact]
        public void EvaluationTest()
        {
            var parser = new Parser("    42    + 313  * 3");

            Assert.Equal(981, parser.Evaluate());
        }
    }
}
