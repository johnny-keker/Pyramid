using System;

namespace Pyramid
{
    public class Parser
    {
        private Lexer _lexer;

        public Parser(string text)
        {
            _lexer = new Lexer(text);
        }

        // factor : INTEGER | LPAREN expr RPAREN
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

        // term : factor ((MUL | DIV) factor)*
        private Node Term()
        {
            var node = Factor();

            if (_lexer.TryReadChar('*'))
                return new MulNode(node, Factor());
            else if (_lexer.TryReadChar('/'))
                return new DivNode(node, Factor());
            else
                return node;
        }

        // expr   : term ((PLUS | MINUS) term)*
        private Node Expression()
        {
            var node = Term();

            if (_lexer.TryReadChar('+'))
                return new PlusNode(node, Term());
            else if (_lexer.TryReadChar('-'))
                return new MinusNode(node, Term());
            else
                return node;
        }

        public Node Parse() => Expression();

        public int Evaluate() => Expression().Compute();
    }
}
