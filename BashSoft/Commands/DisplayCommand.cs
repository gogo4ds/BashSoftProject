namespace BashSoft.Commands
{
    using System;
    using System.Collections.Generic;
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using BashSoft.IO;
    using BashSoft.Utilities;

    [Alias("display")]
    public class DisplayCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public DisplayCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(this.Data, 3))
            {
                return;
            }

            var entityToDisplay = this.Data[1];
            var sortType = this.Data[2];

            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                var studentComparer = this.CreateStudentComparator(sortType);
                var list = this.repository.GetAllStudentsSorted(studentComparer);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                var courseComparer = this.CreateCourseComparator(sortType);
                var list = this.repository.GetAllCoursesSorted(courseComparer);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }

        private IComparer<ICourse> CreateCourseComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((courseOne, courseTwo) => courseOne.CompareTo(courseTwo));
            }

            if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>.Create((courseOne, courseTwo) => courseTwo.CompareTo(courseOne));
            }

            throw new InvalidCommandException(this.Input);
        }

        private IComparer<IStudent> CreateStudentComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((studentOne, studentTwo) => studentOne.CompareTo(studentTwo));
            }

            if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>.Create((studentOne, studentTwo) => studentOne.CompareTo(studentTwo));
            }

            throw new InvalidCommandException(this.Input);
        }
    }
}