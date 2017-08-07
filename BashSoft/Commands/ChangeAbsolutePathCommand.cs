namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    [Alias("cdabs")]
    public class ChangeAbsolutePathCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputIoManager;

        public ChangeAbsolutePathCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length > 1)
            {
                var absolutePath = this.Input.Substring(this.Data[0].Length).Trim();
                this.inputOutputIoManager.ChangeCurrentDirectoryAbsolute(absolutePath);
                return;
            }

            throw new InvalidCommandParametersCountException(this.Data[0]);
        }
    }
}