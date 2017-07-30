using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.StaticData;

namespace BashSoft.Commands
{
    using Utilities;

    internal class GetHelpCommand : Command
    {
        public GetHelpCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (CommandValidator.IsCommandValidLenght(Data, 1))
            {
                DisplayHelp();
            }
        }

        private void DisplayHelp()
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteMessageOnNewLine($"|{"make directory - mkdir: path ",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"traverse directory - ls: depth ",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"comparing files - cmp: path1 path2",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"change directory - changeDirREl:relative path",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"change directory - changeDir:absolute path",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"read students data base - readDb: path",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"download file - download: path of file (saved in current directory)",-98}|");
            OutputWriter.WriteMessageOnNewLine(
                $"|{"download file asinchronously - downloadAsynch: path of file (save in the current directory)",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"display data entities - display students/courses ascending/descending",-98}|");
            OutputWriter.WriteMessageOnNewLine($"|{"get help – help",-98}|");
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteEmptyLine();
        }
    }
}