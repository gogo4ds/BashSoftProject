namespace BashSoft.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string input, string[] data, IContentComparer judge, IDatabase repository,
            IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 2))
            {
                return;
            }

            var folderName = this.Data[1];
            this.InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            var openFileCmd = new OpenFileCommand(this.Input, this.Data, this.Judge, this.Repository,
                this.InputOutputManager);
            openFileCmd.Execute();
        }
    }
}