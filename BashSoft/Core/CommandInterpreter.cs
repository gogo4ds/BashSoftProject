namespace BashSoft.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using BashSoft.Commands;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using BashSoft.IO;

    public class CommandInterpreter : IInterpreter
    {
        private readonly IDirectoryManager inputOutputManager;
        private readonly IContentComparer judge;
        private readonly IDatabase repository;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpretCommand(string input)
        {
            var data = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var commandName = data[0];
            try
            {
                IExecutable command = this.ParseCommand(input, data, commandName);
                command.Execute();
            }
            catch (Win32Exception we)
            {
                OutputWriter.DisplayException(we.Message);
            }
            catch (DirectoryNotFoundException dnf)
            {
                OutputWriter.DisplayException(dnf.Message);
            }
            catch (InvalidPathException ip)
            {
                OutputWriter.DisplayException(ip.Message);
            }
            catch (ArgumentNullException an)
            {
                OutputWriter.DisplayException(an.Message);
            }
            catch (ArgumentOutOfRangeException aoore)
            {
                OutputWriter.DisplayException(aoore.Message);
            }
            catch (ArgumentException ae)
            {
                OutputWriter.DisplayException(ae.Message);
            }
            catch (DuplicateEntryInStructureException dn)
            {
                OutputWriter.DisplayException(dn.Message);
            }
            catch (KeyNotFoundException knf)
            {
                OutputWriter.DisplayException(knf.Message);
            }
            catch (CourseNotFoundException cnf)
            {
                OutputWriter.DisplayException(cnf.Message);
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private Command ParseCommand(string input, string[] data, string command)
        {
            switch (command)
            {
                case "open":
                    return new OpenFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "mkdir":
                    return new MakeDirectoryCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "ls":
                    return new TraverseFoldersCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "cmp":
                    return new CompareFilesCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "cdrel":
                    return new ChangeRelativePathCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "cdabs":
                    return new ChangeAbsolutePathCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "readdb":
                    return new ReadDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "help":
                    return new GetHelpCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "show":
                    return new ShowCourseCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, this.judge, this.repository,
                        this.inputOutputManager);

                case "order":
                    return new PrintOrderedStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "dropdb":
                    return new DropDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                case "display":
                    return new DisplayCommand(input, data, this.judge, this.repository, this.inputOutputManager);

                default:
                    throw new InvalidCommandException(input);
            }
        }
    }
}