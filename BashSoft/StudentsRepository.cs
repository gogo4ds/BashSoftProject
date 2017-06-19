using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool IsDataInitialized = false;
        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCorse;

        public static void GetStudentScoresFromCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossiblе(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, List<int>>(username, studentsByCorse[courseName][username]));
            }
        }

        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var student in studentsByCorse[courseName])
                {
                    OutputWriter.PrintStudent(student);
                }
            }   
        }

        public static void InitializeData(string fileName)
        {
            if (!IsDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                studentsByCorse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        public static void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (!IsQueryForCoursePossible(courseName)) return;

            if (studentsToTake == null)
            {
                studentsToTake = studentsByCorse[courseName].Count;
            }

            RepositoryFilters.FilterAndTake(studentsByCorse[courseName], givenFilter, studentsToTake.Value);
        }

        public static void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (!IsQueryForCoursePossible(courseName)) return;

            if (studentsToTake == null)
            {
                studentsToTake = studentsByCorse[courseName].Count;
            }

            RepositorySorters.OrderAndTake(studentsByCorse[courseName], comparison, studentsToTake.Value);
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (IsDataInitialized)
            {
                if (studentsByCorse.ContainsKey(courseName))
                {
                    return true;
                }

                OutputWriter.DisplayException(ExceptionMessages.InexistingCourseInDataBase);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private static bool IsQueryForStudentPossiblе(string courseName, string studentName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCorse[courseName].ContainsKey(studentName))
            {
                return true;
            }

            OutputWriter.DisplayException(ExceptionMessages.InexistingStudentInDataBase);
            return false;
        }

        private static void ReadData(string fileName)
        {
            string path = SessionData.CurrentPath + "\\" + fileName;

            if (File.Exists(path))
            {
                string[] allInputLines = File.ReadAllLines(path);

                var pattern = @"([A-Z][a-zA-Z#+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(\d+)";
                Regex regex = new Regex(pattern);

                foreach (string line in allInputLines)
                {
                    if (string.IsNullOrEmpty(line) || !regex.IsMatch(line)) continue;
                    Match currentMatch = regex.Match(line);
                    string course = currentMatch.Groups[1].Value;
                    string student = currentMatch.Groups[2].Value;
                    if (int.TryParse(currentMatch.Groups[3].Value, out int studentScore) && studentScore <= 100 && studentScore >= 0)
                    {
                        if (!studentsByCorse.ContainsKey(course))
                        {
                            studentsByCorse.Add(course, new Dictionary<string, List<int>>());
                        }

                        if (!studentsByCorse[course].ContainsKey(student))
                        {
                            studentsByCorse[course].Add(student, new List<int>());
                        }

                        studentsByCorse[course][student].Add(studentScore);
                    }
                }

                IsDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }           
        }
    }
}
