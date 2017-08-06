namespace BashSoft.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using BashSoft.Contracts;
    using BashSoft.DataStructures;
    using BashSoft.Exceptions;
    using BashSoft.IO;
    using BashSoft.Models;
    using BashSoft.StaticData;

    public class StudentsRepository : IDatabase
    {
        private readonly RepositoryFilter filter;
        private readonly RepositorySorter sorter;
        private Dictionary<string, ICourse> courses;
        private Dictionary<string, IStudent> students;

        public StudentsRepository(RepositorySorter sorter, RepositoryFilter filter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        public bool IsDataInitialized { get; set; }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (this.IsQueryForStudentPossiblе(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username,
                    this.courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentScoresFromCourse(courseName, student.Key);
                }
            }
        }

        public ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> comparer)
        {
            var sortedCourses = new SimpleSortedList<ICourse>(comparer);

            sortedCourses.AddAll(this.courses.Values);

            return sortedCourses;
        }

        public ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> comparer)
        {
            var sortedStudents = new SimpleSortedList<IStudent>(comparer);

            sortedStudents.AddAll(this.students.Values);

            return sortedStudents;
        }

        public void LoadData(string fileName)
        {
            if (this.IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataAlreadyInitializedException);
            }

            this.students = new Dictionary<string, IStudent>();
            this.courses = new Dictionary<string, ICourse>();
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            this.ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataAlreadyInitializedException);
            }

            this.students = null;
            this.courses = null;
            this.IsDataInitialized = false;
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (!this.IsQueryForCoursePossible(courseName))
            {
                return;
            }

            if (studentsToTake == null)
            {
                studentsToTake = this.courses[courseName].StudentsByName.Count;
            }

            var marks = this.courses[courseName].StudentsByName
                .ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

            this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (!this.IsQueryForCoursePossible(courseName))
            {
                return;
            }

            if (studentsToTake == null)
            {
                studentsToTake = this.courses[courseName].StudentsByName.Count;
            }

            var marks = this.courses[courseName].StudentsByName
                .ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

            this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (courseName == null)
            {
                throw new ArgumentNullException(nameof(courseName));
            }

            if (!this.IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            if (!this.courses.ContainsKey(courseName))
            {
                throw new CourseNotFoundException();
            }

            return true;
        }

        private bool IsQueryForStudentPossiblе(string courseName, string studentName)
        {
            if (this.IsQueryForCoursePossible(courseName) &&
                this.courses[courseName].StudentsByName.ContainsKey(studentName))
            {
                return true;
            }

            throw new KeyNotFoundException(ExceptionMessages.InexistingStudentInDataBase);
        }

        private void ReadData(string fileName)
        {
            var path = SessionData.CurrentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                var allInputLines = File.ReadAllLines(path);

                var pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                var regex = new Regex(pattern);

                foreach (var line in allInputLines)
                {
                    if (string.IsNullOrEmpty(line) || !regex.IsMatch(line))
                    {
                        continue;
                    }

                    var currentMatch = regex.Match(line);
                    var courseName = currentMatch.Groups[1].Value;
                    var username = currentMatch.Groups[2].Value;
                    var scoresStr = currentMatch.Groups[3].Value;
                    var scores = scoresStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                        .ToArray();

                    if (scores.Any(x => x < 0 || x > 100))
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                    }

                    if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                        continue;
                    }

                    if (!this.students.ContainsKey(username))
                    {
                        this.students.Add(username, new SoftUniStudent(username));
                    }

                    if (!this.courses.ContainsKey(courseName))
                    {
                        this.courses.Add(courseName, new SoftUniCourse(courseName));
                    }

                    var course = this.courses[courseName];
                    var student = this.students[username];

                    student.EnrollCourse(course);
                    student.SetMarksInCourse(courseName, scores);

                    course.EnrollStudent(student);
                }

                this.IsDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                throw new InvalidPathException();
            }
        }
    }
}