namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using BashSoft.IO;
    using BashSoft.StaticData;

    [Alias("ls")]
    public class TraverseFoldersCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputIoManager;

        public TraverseFoldersCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length < 2)
            {
                this.inputOutputIoManager.TraverseDirectory(0);
            }
            else if (this.Data.Length == 2)
            {
                int depth;
                if (int.TryParse(this.Data[1], out depth))
                {
                    this.inputOutputIoManager.TraverseDirectory(depth);
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