namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    [Alias("cdrel")]
    public class ChangeRelativePathCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputIoManager;

        public ChangeRelativePathCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 2))
            {
                return;
            }

            var relPath = this.Data[1];
            this.inputOutputIoManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}