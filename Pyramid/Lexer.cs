using System;
namespace Pyramid
{
    public class Lexer
    {
        private string _text;
        private int _position = 0;
        public char CurChar => _position > _text.Length - 1
            ? char.MinValue
            : _text[_position];

        public Lexer(string text)
        {
            _text = text;
        }

        private void SkipWhiteSpace()
        {
            while (char.IsWhiteSpace(CurChar))
                _position++;
        }

        public bool TryReadInt(out int res)
        {
            res = 0;
            SkipWhiteSpace();
            if (!char.IsDigit(CurChar)) return false;
            while (char.IsDigit(CurChar))
            {
                res *= 10;
                res += int.Parse(CurChar.ToString()); // maybe better way to parse int?
                _position++;
            }
            return true;
        }

        public bool TryReadChar(char expected)
        {
            SkipWhiteSpace();
            var res = CurChar == expected;
            if (res) _position++;
            return res;
        }
    }
}
