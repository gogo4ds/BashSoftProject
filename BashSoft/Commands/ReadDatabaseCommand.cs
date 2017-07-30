﻿using BashSoft.Contracts;

namespace BashSoft.Commands
{
    using Utilities;

    public class ReadDatabaseCommand : Command
    {
        public ReadDatabaseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 2)) return;

            var fileName = Data[1];
            Repository.LoadData(fileName);
        }
    }
}