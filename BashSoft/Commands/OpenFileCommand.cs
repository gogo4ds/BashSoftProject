using System.ComponentModel;
using System.Diagnostics;
using BashSoft.Exceptions;

namespace BashSoft.Commands
{
    internal class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputIoManager)
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