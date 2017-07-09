using BashSoft.Exceptions;

namespace BashSoft.Utilities
{
    public static class CommandValidator
    {
        public static bool IsCommandValidLenght(string[] data, int validLenght)
        {
            if (data.Length == validLenght)
            {
                return true;
            }

            throw new InvalidCommandParametersCountException(data[0]);
        }
    }
}