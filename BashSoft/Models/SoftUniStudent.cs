namespace BashSoft.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using BashSoft.StaticData;

    public class SoftUniStudent : IStudent
    {
        private readonly Dictionary<string, ICourse> enrolledCourses;
        private readonly Dictionary<string, double> marksByCourseName;
        private string username;

        public SoftUniStudent(string username)
        {
            this.Username = username;
            this.enrolledCourses = new Dictionary<string, ICourse>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string Username
        {
            get => this.username;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.username = value;
                }
                else
                {
                    throw new InvalidStringException();
                }
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses => this.enrolledCourses;

        public IReadOnlyDictionary<string, double> MarksByCourseName => this.marksByCourseName;

        public void EnrollCourse(ICourse course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.Username, course.Name);
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarksInCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                throw new NotEnrolledInCourseException();
            }

            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new ArgumentOutOfRangeException(ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, this.CalculateMark(scores));
        }

        public int CompareTo(IStudent other)
        {
            return string.CompareOrdinal(this.Username, other.Username);
        }

        public override string ToString()
        {
            return this.Username;
        }

        private double CalculateMark(int[] scores)
        {
            var percentageOfSolvedExam = scores.Sum() /
                                         (double)(SoftUniCourse.NumberOfTasksOnExam *
                                                   SoftUniCourse.MaxScoreOnExamTask);
            var mark = (percentageOfSolvedExam * 4) + 2;
            return mark;
        }
    }
}