namespace BashSoft.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    public class ReadDatabaseCommand : Command
    {
        public ReadDatabaseCommand(string input, string[] data, IContentComparer judge, IDatabase repository,
            IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 2))
            {
                return;
            }

            var fileName = this.Data[1];
            this.Repository.LoadData(fileName);
        }
    }
}