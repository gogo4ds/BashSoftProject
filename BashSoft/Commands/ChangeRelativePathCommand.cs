namespace BashSoft.Commands
{
    using Utilities;

    public class ChangeRelativePathCommand : Command
    {
        public ChangeRelativePathCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 2)) return;

            string relPath = Data[1];
            InputOutputManager.ChangeCurrentDirectoryRelative(relPath);
        }
    }
}