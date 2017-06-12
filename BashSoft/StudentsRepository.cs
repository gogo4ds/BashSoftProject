using System;
using System.Collections.Generic;
using System.IO;

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

        public static void GetAllStudentsFromCOurse(string courseName)
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

                foreach (string line in allInputLines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] tokens = line.Split();
                        string course = tokens[0];
                        string student = tokens[1];
                        int mark = int.Parse(tokens[2]);

                        if (!studentsByCorse.ContainsKey(course))
                        {
                            studentsByCorse.Add(course, new Dictionary<string, List<int>>());
                        }

                        if (!studentsByCorse[course].ContainsKey(student))
                        {
                            studentsByCorse[course].Add(student, new List<int>());
                        }

                        studentsByCorse[course][student].Add(mark);
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
