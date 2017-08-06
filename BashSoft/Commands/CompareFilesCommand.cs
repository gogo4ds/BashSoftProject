namespace BashSoft.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 3))
            {
                return;
            }

            var firstPath = this.Data[1];
            var secondPath = this.Data[2];

            this.Judge.CompareContent(firstPath, secondPath);
        }
    }
}