using System;
using BashSoft.Exceptions;

namespace BashSoft.Commands
{
    public abstract class Command
    {
        private string input;
        private string[] data;
        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputIoManager;

        public Command(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputIoManager)
        {
            Input = input;
            Data = data;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputIoManager = inputOutputIoManager;
        }

        protected string Input
        {
            get => input;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidStringException();
                }

                input = value;
            }
        }

        protected string[] Data
        {
            get => data;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                data = value;
            }
        }

        protected Tester Judge => judge;

        protected StudentsRepository Repository => repository;

        protected IOManager InputOutputManager => inputOutputIoManager;

        public abstract void Execute();
    }
}