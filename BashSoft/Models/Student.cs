using System.Collections.Generic;
using System.Linq;

namespace BashSoft.Models
{
    public class Student
    {
        private string username;
        private Dictionary<string, Course> enrolledCourses;
        public Dictionary<string, double> marksByCourseName;

        public Student(string username)
        {
            Username = username;
            enrolledCourses = new Dictionary<string, Course>();
            marksByCourseName = new Dictionary<string, double>();
        }

        public string Username { get; set; }

        public void EnrollCOurse(Course course)
        {
            if (enrolledCourses.ContainsKey(course.Name))
            {
                OutputWriter.DisplayException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, Username, course.Name));
                return;
            }

            enrolledCourses.Add(course.Name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!enrolledCourses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
                return;
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                return;
            }

            marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum() /
                (double)(Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}