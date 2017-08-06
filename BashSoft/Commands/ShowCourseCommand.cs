namespace BashSoft.Commands
{
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    internal class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, IContentComparer judge, IDatabase repository,
            IDirectoryManager inputOutputIoManager)
            : base(input, data, judge, repository, inputOutputIoManager)
        {
        }

        public override void Execute()
        {
            switch (this.Data.Length)
            {
                case 2:
                    {
                        var courseName = this.Data[1];
                        this.Repository.GetAllStudentsFromCourse(courseName);
                    }

                    break;

                case 3:
                    {
                        var courseName = this.Data[1];
                        var username = this.Data[2];
                        this.Repository.GetStudentScoresFromCourse(courseName, username);
                    }

                    break;

                default:
                    throw new InvalidCommandParametersCountException(this.Data[0]);
            }
        }
    }
}