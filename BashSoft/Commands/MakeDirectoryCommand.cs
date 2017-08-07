namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputIoManager;

        [Inject]
        private IContentComparer judge;

        [Inject]
        private IDatabase repository;

        public MakeDirectoryCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 2))
            {
                return;
            }

            var folderName = this.Data[1];
            this.inputOutputIoManager.CreateDirectoryInCurrentFolder(folderName);
            var openFileCmd = new OpenFileCommand(this.Input, this.Data);
            openFileCmd.Execute();
        }
    }
}