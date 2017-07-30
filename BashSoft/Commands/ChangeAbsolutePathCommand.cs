using BashSoft.Contracts;

namespace BashSoft.Commands
{
    using Exceptions;

    public class ChangeAbsolutePathCommand : Command
    {
        public ChangeAbsolutePathCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length > 1)
            {
                var absolutePath = Input.Substring(Data[0].Length).Trim();
                InputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
                return;
            }

            throw new InvalidCommandParametersCountException(Data[0]);
        }
    }
}