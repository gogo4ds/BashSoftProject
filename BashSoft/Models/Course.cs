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
        public Dictionary<string, Student> studentsByName;
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public Course(string name)
        {
            Name = name;
            studentsByName = new Dictionary<string, Student>();
        }

        public string Name { get; set; }

        public void EnrollStudent(Student student)
        {
            if (studentsByName.ContainsKey(student.Username))
            {
                OutputWriter.DisplayException(string.Format(
                    ExceptionMessages.StudentAlreadyEnrolledInGivenCourse, student.Username, this.Name));
                return;
            }

            this.studentsByName.Add(student.Username, student);
        }
    }
}