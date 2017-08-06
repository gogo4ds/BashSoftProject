namespace BashSoft.Exceptions
{
    using System;

    public class CourseNotFoundException : Exception
    {
        private const string InexistingCourseInDataBase =
            "The course you are trying to get does not exist in the data base!";

        public CourseNotFoundException()
            : base(InexistingCourseInDataBase)
        {
        }

        public CourseNotFoundException(string message)
            : base(message)
        {
        }
    }
}