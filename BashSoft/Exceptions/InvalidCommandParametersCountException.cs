namespace BashSoft.Exceptions
{
    using System;

    public class InvalidCommandParametersCountException : Exception
    {
        private const string InvalidCommandParametersCount = "Wrong number of arguments for command '{0}'";

        public InvalidCommandParametersCountException(string commandName)
            : base(string.Format(InvalidCommandParametersCount, commandName))
        {
        }
    }
}