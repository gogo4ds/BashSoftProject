using System;
using BashSoft.Contracts;
using BashSoft.Core;
using BashSoft.Exceptions;

namespace BashSoft.Commands
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputIoManager;

        protected Command(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
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

        protected IContentComparer Judge => judge;

        protected IDatabase Repository => repository;

        protected IDirectoryManager InputOutputManager => inputOutputIoManager;

        public abstract void Execute();
    }
}