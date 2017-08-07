namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.IO;
    using BashSoft.StaticData;
    using BashSoft.Utilities;

    [Alias("order")]
    internal class PrintOrderedStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintOrderedStudentsCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 5))
            {
                return;
            }

            var courseName = this.Data[1];
            var comparison = this.Data[2].ToLower();
            var takeCommand = this.Data[3].ToLower();
            var takeQuantity = this.Data[4].ToLower();

            this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
        }

        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName,
            string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    int studentsToTake;
                    if (int.TryParse(takeQuantity, out studentsToTake))
                    {
                        this.repository.FilterAndTake(courseName, comparison, studentsToTake);
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