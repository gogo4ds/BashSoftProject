﻿namespace BashSoft.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IStudent : IComparable<IStudent>
    {
        string Username { get; }

        IReadOnlyDictionary<string, ICourse> EnrolledCourses { get; }

        IReadOnlyDictionary<string, double> MarksByCourseName { get; }

        void EnrollCourse(ICourse course);

        void SetMarksInCourse(string courseName, params int[] scores);
    }
}