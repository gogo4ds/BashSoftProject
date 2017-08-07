namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Utilities;

    [Alias("cmp")]
    public class CompareFilesCommand : Command
    {
        [Inject]
        private IContentComparer judge;

        public CompareFilesCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 3))
            {
                return;
            }

            var firstPath = this.Data[1];
            var secondPath = this.Data[2];

            this.judge.CompareContent(firstPath, secondPath);
        }
    }
}