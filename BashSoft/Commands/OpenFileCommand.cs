namespace BashSoft.Commands
{
    using System.Diagnostics;
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.StaticData;

    [Alias("open")]
    internal class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            Process.Start(SessionData.CurrentPath + "\\" + fileName);
        }
    }
}