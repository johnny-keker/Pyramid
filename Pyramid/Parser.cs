using System;

namespace Pyramid
{
    public class Parser
    {
        private Lexer _lexer;

        /*
         * or       : xor (| xor)
         * xor      : and (^ and)*
         * and      : shift (& shift)*
         * shift    : sum ((>> | <<) sum)*
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

        private Node Mul()
        {
            var node = Factor();
            while (true)
            {
                var mul = _lexer.TryReadChar('*');
                var div = _lexer.TryReadChar('/');
                if (!(mul || div)) return node;
                node = mul
                    ? (Node)new MulNode(node, Factor())
                    : new DivNode(node, Factor());
            }
        }

        private Node Sum()
        {
            var node = Mul();
            while (true)
            {
                var plus = _lexer.TryReadChar('+');
                var minus = _lexer.TryReadChar('-');
                if (!(plus || minus)) return node;
                node = plus
                    ? (Node)new PlusNode(node, Mul())
                    : new MinusNode(node, Mul());
            }
        }

        private Node Shift()
        {
            var node = Sum();
            while (true)
            {
                var right = _lexer.TryReadChar('>') && _lexer.TryReadChar('>');
                var left = _lexer.TryReadChar('<') && _lexer.TryReadChar('<');
                if (!(right || left)) return node;
                node = new BitwiseShiftNode(node, Sum(), right);
            }
        }

        private Node And()
        {
            var node = Shift();

            while (_lexer.TryReadChar('&'))
                node = new AndNode(node, Shift());
            return node;
        }

        private Node Xor()
        {
            var node = And();

            while (_lexer.TryReadChar('^'))
                node = new XorNode(node, And());
            return node;
        }

        private Node Expression()
        {
            var node = Xor();

            while (_lexer.TryReadChar('|'))
                node = new OrNode(node, Xor());
            return node;
        }

        public Node Parse() => Expression();

        public int Evaluate() => Expression().Compute();
    }
}
