namespace BashSoft.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using BashSoft.IO;
    using BashSoft.StaticData;

    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, IContentComparer judge, IDatabase repository,
            IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length < 2)
            {
                this.InputOutputManager.TraverseDirectory(0);
            }
            else if (this.Data.Length == 2)
            {
                int depth;
                if (int.TryParse(this.Data[1], out depth))
                {
                    this.InputOutputManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                throw new InvalidCommandParametersCountException(this.Data[0]);
            }
        }
    }
}