using BashSoft.Utilities;

namespace BashSoft.Commands
{
    public class MakeDirectoryCommand : Command
    {
        public MakeDirectoryCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 2)) return;

            string folderName = Data[1];
            InputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            var openFileCmd = new OpenFileCommand(Input, Data, Judge, Repository, InputOutputManager);
            openFileCmd.Execute();
        }
    }
}