using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.StaticData;
using BashSoft.Utilities;

namespace BashSoft.Commands
{
    using Exceptions;

    internal class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (Data.Length != 1)
            {
                throw new InvalidCommandException(Input);
            }

            Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database dropped!");
        }
    }
}