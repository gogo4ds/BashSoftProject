using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.StaticData;

namespace BashSoft.Commands
{
    using Utilities;

    internal class PrintFilteredStudentsCommand : Command
    {
        public PrintFilteredStudentsCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 5)) return;

            string courseName = Data[1];
            string filter = Data[2].ToLower();
            string takeCommand = Data[3].ToLower();
            string takeQuantity = Data[4].ToLower();

            TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        }

        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    Repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    if (int.TryParse(takeQuantity, out int studentsToTake))
                    {
                        Repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
            }
        }
    }
}