using System;

namespace Pyramid
{
    public class Parser
    {
        private Lexer _lexer;

        /*
         * expr     : sum ((>> | <<) sum)*
         * sum      : mul ((+ | -) mul)*
         * mul      : factor ((* | /) factor)*
         * factor   : INTEGER | LPAREN sum RPAREN
         */
        public Parser(string text)
        {
            _lexer = new Lexer(text);
        }

        private Node Factor()
        {
            if (_lexer.TryReadChar('('))
            {
                var factor = Expression();
                if (!_lexer.TryReadChar(')'))
                    throw new ArgumentException("Invalid parentheses placing");
                return factor;
            }
            if (!_lexer.TryReadInt(out var value))
                throw new ArgumentException();
            return new IntNode(value);
        }

        private Node Sum()
        {
            var node = Mul();

            if (_lexer.TryReadChar('+'))
                return new PlusNode(node, Mul());
            else if (_lexer.TryReadChar('-'))
                return new MinusNode(node, Mul());
            else
                return node;
        }

        private Node Mul()
        {
            var node = Factor();

            if (_lexer.TryReadChar('*'))
                return new MulNode(node, Factor());
            else if (_lexer.TryReadChar('/'))
                return new DivNode(node, Factor());
            else
                return node;
        }

        private Node Expression()
        {
            var node = Sum();

            if (_lexer.TryReadChar('>') && _lexer.TryReadChar('>'))
                return new BitwiseShiftNode(node, Sum());
            else if (_lexer.TryReadChar('<') && _lexer.TryReadChar('<'))
                return new BitwiseShiftNode(node, Sum(), false);
            else
                return node;
        }

        public Node Parse() => Expression();

        public int Evaluate() => Expression().Compute();
    }
}
