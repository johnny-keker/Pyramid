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

        [Fact]
        public void ParenthesesTest()
        {
            var parser = new Parser("     (      42    +313  )   * 3");

            Assert.Equal(1065, parser.Evaluate());
        }

        [Fact]
        public void BitwiseShiftTest()
        {
            var arg = 0b_1101_1110_1010_1101;
            var parser = new Parser($"{arg} << 4");
            Assert.Equal(0b_1101_1110_1010_1101_0000, parser.Evaluate());

            parser = new Parser($"{arg} >> 4");
            Assert.Equal(0b_1101_1110_1010, parser.Evaluate());
        }

        [Fact]
        public void OperatorPrecedenceTest()
        {
            var parser = new Parser("(4200 & 39 + 313000 & 255 >> 1 & 70) | 32 ^ 15 & 10 + 3 << 2");
            var res = (4200 & 39 + 313000 & 255 >> 1 & 70) | 32 ^ 15 & 10 + 3 << 2;
            Assert.Equal(res, parser.Evaluate());

            parser = new Parser("2 - 2 - 2 + 4 + 4 - 2 - 2");
            res = 2 - 2 - 2 + 4 + 4 - 2 - 2;
            Assert.Equal(res, parser.Evaluate());

            parser = new Parser("2 * 2 * 2 * 2 / 2 / 2 / 2 * 4");
            res = 2 * 2 * 2 * 2 / 2 / 2 / 2 * 4;
            Assert.Equal(res, parser.Evaluate());

            parser = new Parser("127 >> 1 >> 2 >> 3 << 4 << 3 >> 2 << 1");
            res = 127 >> 1 >> 2 >> 3 << 4 << 3 >> 2 << 1;
            Assert.Equal(res, parser.Evaluate());

            parser = new Parser("127 ^ 42  ^ 55 ^ 66 ^ 77");
            res = 127 ^ 42 ^ 55 ^ 66 ^ 77;
            Assert.Equal(res, parser.Evaluate());

            parser = new Parser("127 & 42  & 55 & 66 & 77");
            res = 127 & 42 & 55 & 66 & 77;
            Assert.Equal(res, parser.Evaluate());

            parser = new Parser("0 | 42 | 55 | 66 | 77");
            res = 0 | 42 | 55 | 66 | 77;
            Assert.Equal(res, parser.Evaluate());
        }
    }
}
