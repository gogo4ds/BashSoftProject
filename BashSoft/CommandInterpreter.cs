using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace BashSoft
{
    public static class CommandInterpreter
    {
        public static void InterpredCommand(string input)
        {
            string[] data = input.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);
            string command = data[0];
            switch (command)
            {
                case "open":
                    TryOpenFile(data);
                    break;
                case "mkdir":
                    TryCreateDirectory(data);
                    TryOpenFile(data);
                    break;
                case "ls":
                    TryTraverseFolder(data);
                    break;
                case "cmp":
                    TryCompareFiles(data);
                    break;
                case "cdRel":
                    TryChangePathRelatively(data);
                    break;
                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readDb":
                    TryReadDatabaseFromFile(data);
                    break;
                case "help":
                    TryGetHelp(data);
                    break;
                case "filter":
                    //TODO:
                    break;
                case "order":
                    //TODO:
                    break;
                case "decOrder":
                    //TODO:
                    break;
                case "download":
                    //TODO:
                    break;
                case "downloadAsynch":
                    //TODO:
                    break;
                default:
                    OutputWriter.DisplayException(string.Format(ExceptionMessages.InvalidCommand, input));
                    break;
            }
        } 

        private static void TryReadDatabaseFromFile(string[] data)
        {
            if (!IsCommandValidLenght(data, 2)) return;

            var fileName = data[1];
            StudentsRepository.InitializeData(fileName);
        }

        private static void TryChangePathAbsolute(string input, string[] data)
        {
            if (data.Length > 1)
            {
                var absolutePath = input.Substring(data[0].Length).Trim();
                IOManager.ChangeCurrentDirectoryAbsolute(absolutePath);
                return;
            }

            OutputWriter.DisplayException(string.Format(ExceptionMessages.InvalidCommandParametersCount, data[0]));
        }

        private static void TryChangePathRelatively(string[] data)
        {
            if (!IsCommandValidLenght(data, 2)) return;

            string relPath = data[1];
            IOManager.ChangeCurrentDirectoryRelative(relPath);
        }

        private static void TryCompareFiles(string[] data)
        {
            if (!IsCommandValidLenght(data, 3)) return;

            string firstPath = data[1];
            string secondPath = data[2];

            Tester.CompareContent(firstPath, secondPath);
        }

        private static void TryTraverseFolder(string[] data)
        {
            if (data.Length < 2)
            {
                IOManager.TraverseDirectory(0);
            }
            else if (data.Length == 2)
            {
                if (int.TryParse(data[1], out int depth))
                {
                    IOManager.TraverseDirectory(depth);
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                }
            }
            else
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.InvalidCommandParametersCount, data[0]));
            }
        }

        private static void TryCreateDirectory(string[] data)
        {
            if (!IsCommandValidLenght(data, 2)) return;

            string folderName = data[1];
            IOManager.CreateDirectoryInCurrentFolder(folderName);
        }

        private static void TryOpenFile(string[] data)
        {
            if (!IsCommandValidLenght(data, 2)) return;

            string fileName = data[1];
            try
            {
                Process.Start($"{SessionData.CurrentPath}\\{fileName}");
            }
            catch (Win32Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }         
        }

        private static bool IsCommandValidLenght(string[] data, int validLenght)
        {
            if (data.Length == validLenght)
            {
                return true;
            }

            OutputWriter.DisplayException(string.Format(ExceptionMessages.InvalidCommandParametersCount, data[0]));
            return false;
        }

        private static void TryGetHelp(string[] data)
        {
            if (!IsCommandValidLenght(data, 1)) return;

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
            OutputWriter.WriteMessageOnNewLine($"|{"get help – help",-98}|");
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteEmptyLine();
        }
    }
}
