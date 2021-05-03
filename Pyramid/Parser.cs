using System;

namespace Pyramid
{
    public class Parser
    {
        private Lexer _lexer;
        private Token _currentToken;

        public Parser(string text)
        {
            _lexer = new Lexer(text);
            _currentToken = _lexer.Next();
        }

        private void Eat(TokenType type)
        {
            if (_currentToken.Type == type)
                _currentToken = _lexer.Next();
            else
                throw new ArgumentException();
        }

        // factor : INTEGER | LPAREN expr RPAREN
        private Node Factor()
        {
            if (_currentToken.Type != TokenType.INT)
                throw new ArgumentException(); // TODO: handle brackets
            var token = _currentToken;
            Eat(TokenType.INT);
            return new IntNode(int.Parse(token.Value));
        }

        // term : factor ((MUL | DIV) factor)*
        private Node Term()
        {
            var node = Factor();
            
            while (_currentToken.Type == TokenType.DIV || _currentToken.Type == TokenType.MUL)
            {
                var token = _currentToken;
                if (token.Type == TokenType.MUL)
                {
                    Eat(TokenType.MUL);
                    node = new MulNode(node, Factor());
                }
                else if (token.Type == TokenType.DIV)
                {
                    Eat(TokenType.DIV);
                    node = new DivNode(node, Factor());
                }
            }

            return node;
        }

        // expr   : term ((PLUS | MINUS) term)*
        private Node Expression()
        {
            var node = Term();

            while (_currentToken.Type == TokenType.PLUS || _currentToken.Type == TokenType.MINUS)
            {
                var token = _currentToken;
                if (token.Type == TokenType.PLUS)
                {
                    Eat(TokenType.PLUS);
                    node = new PlusNode(node, Term());
                }
                else if (token.Type == TokenType.MINUS)
                {
                    Eat(TokenType.MINUS);
                    node = new MinusNode(node, Term());
                }
            }

            return node;
        }

        public Node Parse() => Expression();

        public int Evaluate() => Expression().Compute();
    }
}
