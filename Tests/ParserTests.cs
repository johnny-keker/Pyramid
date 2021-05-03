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

            Assert.True(ast is PlusNode);

            Assert.True(ast.Left is IntNode);
            Assert.Equal(42, ((IntNode)ast.Left).Value);

            Assert.True(ast.Right is IntNode);
            Assert.Equal(313, ((IntNode)ast.Right).Value);

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
