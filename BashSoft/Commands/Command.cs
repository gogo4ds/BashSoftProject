namespace BashSoft.Commands
{
    using System;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    public abstract class Command : IExecutable
    {
        private string[] data;
        private string input;

        protected Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
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

        public abstract void Execute();
    }
}