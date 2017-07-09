namespace BashSoft.Commands
{
    using Exceptions;

    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length < 2)
            {
                InputOutputManager.TraverseDirectory(0);
            }
            else if (Data.Length == 2)
            {
                if (int.TryParse(Data[1], out int depth))
                {
                    InputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                throw new InvalidCommandParametersCountException(Data[0]);
            }
        }
    }
}