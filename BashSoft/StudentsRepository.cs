using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BashSoft.Models;

namespace BashSoft
{
    public class StudentsRepository
    {
        public bool IsDataInitialized = false;
        private Dictionary<string, Dictionary<string, List<int>>> studentsByCorse;
        private RepositoryFilter filter;
        private RepositorySorter sorter;
        private Dictionary<string, Course> courses;
        private Dictionary<string, Student> students;

        public StudentsRepository(RepositorySorter sorter, RepositoryFilter filter)
        {
            this.filter = filter;
            this.sorter = sorter;
            this.studentsByCorse = new Dictionary<string, Dictionary<string, List<int>>>();
        }

        public void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossiblе(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username,
                    courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in courses[courseName].StudentsByName)
                {
                    GetStudentScoresFromCourse(courseName, student.Key);
                }
            }
        }

        public void LoadData(string fileName)
        {
            if (IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataAlreadyInitializedException);
            }

            students = new Dictionary<string, Student>();
            courses = new Dictionary<string, Course>();
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataAlreadyInitializedException);
            }

            students = null;
            courses = null;
            studentsByCorse = new Dictionary<string, Dictionary<string, List<int>>>();
            IsDataInitialized = false;
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (!IsQueryForCoursePossible(courseName)) return;

            if (studentsToTake == null)
            {
                studentsToTake = courses[courseName].StudentsByName.Count;
            }

            Dictionary<string, double> marks = courses[courseName].StudentsByName
                .ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

            filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (!IsQueryForCoursePossible(courseName)) return;

            if (studentsToTake == null)
            {
                studentsToTake = courses[courseName].StudentsByName.Count;
            }

            Dictionary<string, double> marks = courses[courseName].StudentsByName
                .ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

            sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (courseName == null)
            {
                throw new ArgumentNullException(nameof(courseName));
            }
            if (!IsDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            if (!courses.ContainsKey(courseName))
            {
                throw new KeyNotFoundException(ExceptionMessages.InexistingCourseInDataBase);
            }

            return true;
        }

        private bool IsQueryForStudentPossiblе(string courseName, string studentName)
        {
            if (IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentName))
            {
                return true;
            }

            throw new KeyNotFoundException(ExceptionMessages.InexistingStudentInDataBase);
        }

        private void ReadData(string fileName)
        {
            string path = SessionData.CurrentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                string[] allInputLines = File.ReadAllLines(path);

                var pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex regex = new Regex(pattern);

                foreach (string line in allInputLines)
                {
                    if (string.IsNullOrEmpty(line) || !regex.IsMatch(line)) continue;
                    Match currentMatch = regex.Match(line);
                    string courseName = currentMatch.Groups[1].Value;
                    string username = currentMatch.Groups[2].Value;
                    string scoresStr = currentMatch.Groups[3].Value;
                    int[] scores = scoresStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                        .ToArray();

                    if (scores.Any(x => x < 0 || x > 100))
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                    }

                    if (scores.Length > Course.NumberOfTasksOnExam)
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                        continue;
                    }

                    if (!students.ContainsKey(username))
                    {
                        students.Add(username, new Student(username));
                    }

                    if (!courses.ContainsKey(courseName))
                    {
                        courses.Add(courseName, new Course(courseName));
                    }

                    Course course = courses[courseName];
                    Student student = students[username];

                    student.EnrollCOurse(course);
                    student.SetMarksInCourse(courseName, scores);

                    course.EnrollStudent(student);
                }

                IsDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                throw new DirectoryNotFoundException(ExceptionMessages.InvalidPath);
            }
        }
    }
}