using System;
using System.Collections.Generic;
using System.Linq;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.StaticData;
using BashSoft.Utilities;

namespace BashSoft.Models
{
    public class SoftUniStudent : IStudent
    {
        private string username;
        private Dictionary<string, ICourse> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public SoftUniStudent(string username)
        {
            Username = username;
            enrolledCourses = new Dictionary<string, ICourse>();
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

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses => enrolledCourses;
        public IReadOnlyDictionary<string, double> MarksByCourseName => marksByCourseName;

        public void EnrollCourse(ICourse course)
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

            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.InvalidNumberOfScores);
            }

            marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum() /
                (double)(SoftUniCourse.NumberOfTasksOnExam * SoftUniCourse.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }

        public int CompareTo(IStudent other) => string.CompareOrdinal(Username, other.Username);

        public override string ToString() => Username;
    }
}