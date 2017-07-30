using BashSoft.Contracts;

namespace BashSoft.Commands
{
    using Utilities;

    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 3)) return;

            string firstPath = Data[1];
            string secondPath = Data[2];

            Judge.CompareContent(firstPath, secondPath);
        }
    }
}