using BashSoft.Contracts;

namespace BashSoft.Commands
{
    using Exceptions;

    internal class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            switch (Data.Length)
            {
                case 2:
                    {
                        string courseName = Data[1];
                        Repository.GetAllStudentsFromCourse(courseName);
                    }
                    break;

                case 3:
                    {
                        string courseName = Data[1];
                        string username = Data[2];
                        Repository.GetStudentScoresFromCourse(courseName, username);
                    }
                    break;

                default:
                    throw new InvalidCommandParametersCountException(Data[0]);
            }
        }
    }
}