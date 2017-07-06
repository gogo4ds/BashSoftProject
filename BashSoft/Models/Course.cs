using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Models
{
    public class Course
    {
        private string name;
        private Dictionary<string, Student> studentsByName;
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public Course(string name)
        {
            Name = name;
            studentsByName = new Dictionary<string, Student>();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(name), ExceptionMessages.NullOrEmptyValue);
                }

                name = value;
            }
        }

        public IReadOnlyDictionary<string, Student> StudentsByName => studentsByName;

        public void EnrollStudent(Student student)
        {
            if (studentsByName.ContainsKey(student.Username))
            {
                throw new ArgumentException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, student.Username, this.Name));
            }

            this.studentsByName.Add(student.Username, student);
        }
    }
}