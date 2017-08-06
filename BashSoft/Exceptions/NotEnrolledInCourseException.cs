namespace BashSoft.Exceptions
{
    using System;

    public class NotEnrolledInCourseException : Exception
    {
        private const string NotEnrolledInCourse = "Student must be enrolled in a course before you set his mark.";

        public NotEnrolledInCourseException()
            : base(NotEnrolledInCourse)
        {
        }

        public NotEnrolledInCourseException(string message)
            : base(message)
        {
        }
    }
}