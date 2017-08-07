namespace BashSoft.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    [Alias("show")]
    internal class ShowCourseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public ShowCourseCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            switch (this.Data.Length)
            {
                case 2:
                    {
                        var courseName = this.Data[1];
                        this.repository.GetAllStudentsFromCourse(courseName);
                    }

                    break;

                case 3:
                    {
                        var courseName = this.Data[1];
                        var username = this.Data[2];
                        this.repository.GetStudentScoresFromCourse(courseName, username);
                    }

                    break;

                default:
                    throw new InvalidCommandParametersCountException(this.Data[0]);
            }
        }
    }
}