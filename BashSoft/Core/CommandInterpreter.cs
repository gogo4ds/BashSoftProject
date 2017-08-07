namespace BashSoft.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using BashSoft.Attributes;
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

        private IExecutable ParseCommand(string input, string[] data, string command)
        {
            Type typeOfCommand = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(type => type.GetCustomAttributes(typeof(AliasAttribute))
                                            .Where(atr => atr.Equals(command))
                                            .ToArray().Length > 0);

            if (typeOfCommand == null)
            {
                throw new InvalidCommandException(input);
            }

            Command exe = (Command)Activator.CreateInstance(typeOfCommand, input, data);
            this.InjectValuesInCommandFields(typeOfCommand, exe);

            return exe;
        }

        private void InjectValuesInCommandFields(Type typeOfCommand, Command exe)
        {
            Type typeOfInterpreter = typeof(CommandInterpreter);

            FieldInfo[] fieldsOfCommand =
                            typeOfCommand.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            FieldInfo[] fieldsOfInterpreter =
                typeOfInterpreter.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var fieldOfCommand in fieldsOfCommand)
            {
                Attribute atr = fieldOfCommand.GetCustomAttribute(typeof(InjectAttribute));
                if (atr != null)
                {
                    if (fieldsOfInterpreter.Any(x => x.FieldType == fieldOfCommand.FieldType))
                    {
                        fieldOfCommand.SetValue(exe,
                            fieldsOfInterpreter.First(f => f.FieldType == fieldOfCommand.FieldType)
                                .GetValue(this));
                    }
                }
            }
        }
    }
}