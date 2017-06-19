using System;
using System.ComponentModel;
using System.Diagnostics;

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
                    TryOpenFile(input, data);
                    break;
                case "mkdir":
                    TryCreateDirectory(data);
                    TryOpenFile(input, data);
                    break;
                case "ls":
                    TryTraverseFolder(input, data);
                    break;
                case "cmp":
                    TryCompareFiles(input, data);
                    break;
                case "cdRel":
                    TryChangePathRelatively(input, data);
                    break;
                case "cdAbs":
                    TryChangePathAbsolute(input, data);
                    break;
                case "readDb":
                    TryReadDatabaseFromFile(input, data);
                    break;
                case "help":
                    TryGetHelp(input, data);
                    break;
                case "show":
                    TryShowWantedData(input, data);
                    break;
                case "filter":
                    TryFilterAndTake(input, data);
                    break;
                case "order":
                    TryOrderAndTake(input, data);
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

        private static void TryOrderAndTake(string input, string[] data)
        {
            if (!IsCommandValidLenght(data, 5)) return;

            string courseName = data[1];
            string comparison = data[2].ToLower();
            string takeCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();

            TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);
        }

        private static void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    StudentsRepository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    if (int.TryParse(takeQuantity, out int studentsToTake))
                    {
                        StudentsRepository.FilterAndTake(courseName, comparison, studentsToTake);
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

        private static void TryFilterAndTake(string input, string[] data)
        {
            if (!IsCommandValidLenght(data, 5)) return;

            string courseName = data[1];
            string filter = data[2].ToLower();
            string takeCommand = data[3].ToLower();
            string takeQuantity = data[4].ToLower();

            TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        }

        private static void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    StudentsRepository.FilterAndTake(courseName, filter);
                }
                else
                {
                    if (int.TryParse(takeQuantity, out int studentsToTake))
                    {
                        StudentsRepository.FilterAndTake(courseName, filter, studentsToTake);
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

        private static void TryShowWantedData(string input, string[] data)
        {
            switch (data.Length)
            {
                case 2:
                {
                    string courseName = data[1];
                    StudentsRepository.GetAllStudentsFromCourse(courseName);
                }
                    break;
                case 3:
                {
                    string courseName = data[1];
                    string username = data[2];
                    StudentsRepository.GetStudentScoresFromCourse(courseName, username);
                }
                    break;
                default:
                    OutputWriter.DisplayException(string.Format(ExceptionMessages.InvalidCommandParametersCount, data[0]));
                    break;
            }
        }

        private static void TryReadDatabaseFromFile(string input, string[] data)
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

        private static void TryChangePathRelatively(string input, string[] data)
        {
            if (!IsCommandValidLenght(data, 2)) return;

            string relPath = data[1];
            IOManager.ChangeCurrentDirectoryRelative(relPath);
        }

        private static void TryCompareFiles(string input, string[] data)
        {
            if (!IsCommandValidLenght(data, 3)) return;

            string firstPath = data[1];
            string secondPath = data[2];

            Tester.CompareContent(firstPath, secondPath);
        }

        private static void TryTraverseFolder(string input, string[] data)
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

        private static void TryOpenFile(string input, string[] data)
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

        private static void TryGetHelp(string input, string[] data)
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
