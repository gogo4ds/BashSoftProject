using System.Diagnostics;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Utilities;

namespace BashSoft.Commands
{
    internal class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 2)
            {
                throw new InvalidCommandException(Input);
            }

            string fileName = Data[1];
            Process.Start(SessionData.CurrentPath + "\\" + fileName);
        }
    }
}