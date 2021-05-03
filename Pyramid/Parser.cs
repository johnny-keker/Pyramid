using System;

namespace Pyramid
{
    public class Parser
    {
        private Lexer _lexer;

        /*
         * sum      : shift ((PLUS | MINUS) shift)*
         * shift    : term ((>> | <<) term)*
         * term     : factor ((MUL | DIV) factor)*
         * factor   : INTEGER | LPAREN sum RPAREN
         */
        public Parser(string text)
        {
            _lexer = new Lexer(text);
        }

        // factor : INTEGER | LPAREN sum RPAREN
        private Node Factor()
        {
            if (_lexer.TryReadChar('('))
            {
                var factor = Sum();
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

        // shift : term ((>> | <<) term)*
        private Node Shift()
        {
            var node = Term();

            if (_lexer.TryReadChar('>') && _lexer.TryReadChar('>'))
                return new BitwiseShiftNode(node, Term());
            else if (_lexer.TryReadChar('<') && _lexer.TryReadChar('<'))
                return new BitwiseShiftNode(node, Term(), false);
            else
                return node;
        }

        // sum   : shift ((PLUS | MINUS) shift)*
        private Node Sum()
        {
            var node = Shift();

            if (_lexer.TryReadChar('+'))
                return new PlusNode(node, Term());
            else if (_lexer.TryReadChar('-'))
                return new MinusNode(node, Term());
            else
                return node;
        }

        public Node Parse() => Sum();

        public int Evaluate() => Sum().Compute();
    }
}
