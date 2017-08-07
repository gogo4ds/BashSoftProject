namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    [Alias("readdb")]
    public class ReadDatabaseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ReadDatabaseCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 2))
            {
                return;
            }

            var fileName = this.Data[1];
            this.repository.LoadData(fileName);
        }
    }
}