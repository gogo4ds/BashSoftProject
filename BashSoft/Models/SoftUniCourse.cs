namespace BashSoft.Models
{
    using System.Collections.Generic;
    using BashSoft.Contracts;
    using BashSoft.Exceptions;

    public class SoftUniCourse : ICourse
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;
        private readonly Dictionary<string, IStudent> studentsByName;
        private string name;

        public SoftUniCourse(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidStringException();
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName => this.studentsByName;

        public void EnrollStudent(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, this.Name);
            }

            this.studentsByName.Add(student.Username, student);
        }

        public int CompareTo(ICourse other)
        {
            return string.CompareOrdinal(this.Name, other.Name);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}