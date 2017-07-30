using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.StaticData;

namespace BashSoft.Commands
{
    using Utilities;

    internal class PrintOrderedStudentsCommand : Command
    {
        public PrintOrderedStudentsCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 5)) return;

            string courseName = Data[1];
            string comparison = Data[2].ToLower();
            string takeCommand = Data[3].ToLower();
            string takeQuantity = Data[4].ToLower();

            TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
        }

        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    Repository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    if (int.TryParse(takeQuantity, out int studentsToTake))
                    {
                        Repository.FilterAndTake(courseName, comparison, studentsToTake);
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