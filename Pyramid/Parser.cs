using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyramid
{
    public class Node
    {
        public Token Token { get; }
        public Node Left;
        public Node Right;

        public Node(Token token, Node left = null, Node right = null)
        {
            Token = token;
            Left = left;
            Right = right;
        }
    }

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
            return new Node(token);
        }

        // term : factor ((MUL | DIV) factor)*
        private Node Term()
        {
            var node = Factor();
            
            while (_currentToken.Type == TokenType.DIV || _currentToken.Type == TokenType.MUL)
            {
                var token = _currentToken;
                if (token.Type == TokenType.MUL)
                    Eat(TokenType.MUL);
                else if (token.Type == TokenType.DIV)
                    Eat(TokenType.DIV);
                node = new Node(token, node, Factor());
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
                    Eat(TokenType.PLUS);
                else if (token.Type == TokenType.MINUS)
                    Eat(TokenType.MINUS);
                node = new Node(token, node, Term());
            }

            return node;
        }

        public Node Parse() => Expression();

        private int ProcessNode(Node node)
        {
            switch (node.Token.Type)
            {
                case TokenType.INT:
                    return int.Parse(node.Token.Value);
                case TokenType.PLUS:
                    return ProcessNode(node.Left) + ProcessNode(node.Right);
                case TokenType.MINUS:
                    return ProcessNode(node.Left) - ProcessNode(node.Right);
                case TokenType.MUL:
                    return ProcessNode(node.Left) * ProcessNode(node.Right);
                case TokenType.DIV:
                    return ProcessNode(node.Left) / ProcessNode(node.Right);
                default:
                    throw new ArgumentException($"Unknown Token: {node.Token}");
            }
        }

        public int Evaluate() => ProcessNode(Expression());
    }
}
