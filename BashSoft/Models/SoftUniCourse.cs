using System;
using System.Collections.Generic;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class SoftUniCourse : ICourse
    {
        private string name;
        private Dictionary<string, IStudent> studentsByName;
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public SoftUniCourse(string name)
        {
            Name = name;
            studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidStringException();
                }

                name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName => studentsByName;

        public void EnrollStudent(IStudent student)
        {
            if (studentsByName.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, Name);
            }

            this.studentsByName.Add(student.Username, student);
        }

        public int CompareTo(ICourse other) => string.CompareOrdinal(Name, other.Name);

        public override string ToString() => Name;
    }
}