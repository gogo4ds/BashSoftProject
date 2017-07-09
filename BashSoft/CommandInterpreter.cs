using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using BashSoft.Commands;
using BashSoft.Exceptions;

namespace BashSoft
{
    public class CommandInterpreter
    {
        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputManager;

        public CommandInterpreter(Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpredCommand(string input)
        {
            string[] data = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string commandName = data[0];
            try
            {
                var command = ParseCommand(input, data, commandName);
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
                    return new OpenFileCommand(input, data, judge, repository, inputOutputManager);

                case "mkdir":
                    return new MakeDirectoryCommand(input, data, judge, repository, inputOutputManager);

                case "ls":
                    return new TraverseFoldersCommand(input, data, judge, repository, inputOutputManager);

                case "cmp":
                    return new CompareFilesCommand(input, data, judge, repository, inputOutputManager);

                case "cdRel":
                    return new ChangeRelativePathCommand(input, data, judge, repository, inputOutputManager);

                case "cdAbs":
                    return new ChangeAbsolutePathCommand(input, data, judge, repository, inputOutputManager);

                case "readDb":
                    return new ReadDatabaseCommand(input, data, judge, repository, inputOutputManager);

                case "help":
                    return new GetHelpCommand(input, data, judge, repository, inputOutputManager);

                case "show":
                    return new ShowCourseCommand(input, data, judge, repository, inputOutputManager);

                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, judge, repository, inputOutputManager);

                case "order":
                    return new PrintOrderedStudentsCommand(input, data, judge, repository, inputOutputManager);

                case "dropdb":
                    return new DropDatabaseCommand(input, data, judge, repository, inputOutputManager);

                //case "decOrder":
                //    //TODO:
                //    break;

                //case "download":
                //    //TODO:
                //    break;

                //case "downloadAsynch":
                //    //TODO:
                //    break;

                default:
                    throw new InvalidCommandException(input);
            }
        }
    }
}