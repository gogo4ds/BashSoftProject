using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Student
    {
        private string username;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public Student(string username)
        {
            Username = username;
            enrolledCourses = new Dictionary<string, Course>();
            marksByCourseName = new Dictionary<string, double>();
        }

        public string Username
        {
            get => username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidStringException();
                }

                username = value;
            }
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses => enrolledCourses;
        public IReadOnlyDictionary<string, double> MarksByCourseName => marksByCourseName;

        public void EnrollCOurse(Course course)
        {
            if (EnrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(Username, course.Name);
            }

            enrolledCourses.Add(course.Name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!EnrolledCourses.ContainsKey(courseName))
            {
                throw new NotEnrolledInCourseException();
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.InvalidNumberOfScores);
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