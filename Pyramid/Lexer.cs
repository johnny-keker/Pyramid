using System;
namespace Pyramid
{
    public enum TokenType
    {
        INT, PLUS, MINUS, DIV, MUL, EOF
    }

    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString() => $"Token ({Type}, {Value})";
    }

    public class Lexer
    {
        private string _text;
        private int _position = 0;
        private char _curChar => _position > _text.Length - 1
            ? char.MinValue
            : _text[_position];

        public Lexer(string text)
        {
            _text = text;
        }

        private void SkipWhiteSpace()
        {
            while (char.IsWhiteSpace(_curChar))
                _position++;
        }

        private string ProcessInteger()
        {
            var res = 0;
            while (char.IsDigit(_curChar))
            {
                res *= 10;
                res += int.Parse(_curChar.ToString()); // maybe better way to parse int?
                _position++;
            }
            return res.ToString();
        }

        public Token Next()
        {
            if (char.IsWhiteSpace(_curChar))
                SkipWhiteSpace();

            switch (_curChar)
            {
                case var c when char.IsDigit(c):
                    return new Token(TokenType.INT, ProcessInteger());
                case '-':
                    _position++;
                    return new Token(TokenType.MINUS, "-");
                case '+':
                    _position++;
                    return new Token(TokenType.PLUS, "+");
                case '*':
                    _position++;
                    return new Token(TokenType.MUL, "*");
                case '/':
                    _position++;
                    return new Token(TokenType.DIV, "/");
                case char.MinValue:
                    return new Token(TokenType.EOF, null);
                default:
                    throw new ArgumentException($"Invalid char: {_curChar}");
            }
        }
    }
}
