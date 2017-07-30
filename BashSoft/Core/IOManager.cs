﻿using System;
using System.Collections.Generic;
using System.IO;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Utilities;

namespace BashSoft.Core
{
    public class IOManager : IDirectoryManager
    {
        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = SessionData.CurrentPath.Split('\\').Length;
            var subfolders = new Queue<string>();
            subfolders.Enqueue(SessionData.CurrentPath);

            while (subfolders.Count != 0)
            {
                var currentPath = subfolders.Dequeue();
                var identation = currentPath.Split('\\').Length - initialIdentation;

                if (depth - identation < 0)
                {
                    break;
                }

                OutputWriter.WriteMessageOnNewLine($"{new string('-', identation)}{currentPath}");

                try
                {
                    foreach (var file in Directory.GetFiles(currentPath))
                    {
                        int indexOfLastSlash = file.LastIndexOf('\\');
                        string fileName = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine(new string('-', indexOfLastSlash) + fileName);
                    }

                    foreach (var dirPath in Directory.GetDirectories(currentPath))
                    {
                        subfolders.Enqueue(dirPath);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }
        }

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.CurrentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf('\\');
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.CurrentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    //throw new ArgumentOutOfRangeException("indexOfLastSlash", ExceptionMessages.InvalidDestination);
                    throw new ArgumentOutOfRangeException(ExceptionMessages.UnableToGoHigherInThePartitionHierarchy);
                }
            }
            else
            {
                string currentPath = SessionData.CurrentPath;
                currentPath += "\\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
            }

            SessionData.CurrentPath = absolutePath;
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = GetCurrentDirectoryPath() + "\\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }

        private string GetCurrentDirectoryPath()
        {
            return SessionData.CurrentPath;
        }
    }
}