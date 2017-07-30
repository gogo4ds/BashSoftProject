using System;
using System.Collections.Generic;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.IO;
using BashSoft.StaticData;
using BashSoft.Utilities;

namespace BashSoft.Commands
{
    public class DisplayCommand : Command
    {
        public DisplayCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            if (!CommandValidator.IsCommandValidLenght(Data, 3)) return;

            var entityToDisplay = Data[1];
            var sortType = Data[2];

            if (entityToDisplay.Equals("students", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<IStudent> studentComparer = CreateStudentComparator(sortType);
                ISimpleOrderedBag<IStudent> list = Repository.GetAllStudentsSorted(studentComparer);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<ICourse> courseComparer = CreateCourseComparator(sortType);
                ISimpleOrderedBag<ICourse> list = Repository.GetAllCoursesSorted(courseComparer);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(Input);
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

            throw new InvalidCommandException(Input);
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

            throw new InvalidCommandException(Input);
        }
    }
}