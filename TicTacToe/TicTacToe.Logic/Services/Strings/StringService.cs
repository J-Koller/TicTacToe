using System;

namespace TicTacToe.Api.Logic.Services.Strings
{
    public class StringService : IStringService
    {
        public bool IsAlphaNumeric(params string[] strings)
        {
            ArgumentNullException.ThrowIfNull(strings);

            foreach(string s in strings)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    return false;
                }

                foreach (char c in s)
                {
                    if (!(CharIsLatin(c) || char.IsDigit(c)) || char.IsWhiteSpace(c))
                    {
                        return false;
                    }
                };
            }

            return true;
        }

        private bool CharIsLatin(char c)
        {
            return (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z');
        }
    }
}
