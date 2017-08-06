namespace BashSoft.Commands
{
    using System;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    public abstract class Command : IExecutable
    {
        private string[] data;
        private string input;

        protected Command(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
        {
            this.Input = input;
            this.Data = data;
            this.Judge = judge;
            this.Repository = repository;
            this.InputOutputManager = inputOutputIoManager;
        }

        protected string Input
        {
            get => this.input;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        protected string[] Data
        {
            get => this.data;
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                this.data = value;
            }
        }

        protected IContentComparer Judge { get; }

        protected IDatabase Repository { get; }

        protected IDirectoryManager InputOutputManager { get; }

        public abstract void Execute();
    }
}