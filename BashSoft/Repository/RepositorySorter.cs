﻿namespace BashSoft.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BashSoft.Contracts;
    using BashSoft.IO;
    using BashSoft.StaticData;

    public class RepositorySorter : IDataSorter
    {
        public void OrderAndTake(IDictionary<string, double> wantedData, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                this.OrderAndTake(wantedData, studentsToTake, CompareInOrder);
            }
            else if (comparison == "descending")
            {
                this.OrderAndTake(wantedData, studentsToTake, CompareDescendingOrder);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static Dictionary<string, double> GetSortedStudents(IDictionary<string, double> studentsWanted,
                    int takeCount,
            Func<KeyValuePair<string, double>, KeyValuePair<string, double>, int> comparison)
        {
            var valuesTaken = 0;
            var studentsSorted = new Dictionary<string, double>();
            var nextInOrder = new KeyValuePair<string, double>();

            while (valuesTaken < takeCount)
            {
                var isSorted = true;
                foreach (var student in studentsWanted)
                {
                    if (!string.IsNullOrEmpty(nextInOrder.Key))
                    {
                        var comparisonResult = comparison(student, nextInOrder);
                        if (comparisonResult >= 0 && !studentsSorted.ContainsKey(student.Key))
                        {
                            nextInOrder = student;
                            isSorted = false;
                        }
                    }
                    else
                    {
                        if (!studentsSorted.ContainsKey(student.Key))
                        {
                            nextInOrder = student;
                            isSorted = false;
                        }
                    }
                }

                if (!isSorted)
                {
                    studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
                    valuesTaken++;
                    nextInOrder = new KeyValuePair<string, double>();
                }
            }

            return studentsSorted;
        }

        private static int CompareInOrder(KeyValuePair<string, double> firstValue,
                    KeyValuePair<string, double> secondValue)
        {
            return secondValue.Value.CompareTo(firstValue.Value);
        }

        private static int CompareDescendingOrder(KeyValuePair<string, double> firstValue,
                    KeyValuePair<string, double> secondValue)
        {
            return firstValue.Value.CompareTo(secondValue.Value);
        }

        private void OrderAndTake(IDictionary<string, double> wantedData, int studentsToTake,
            Func<KeyValuePair<string, double>, KeyValuePair<string, double>, int> comparisonFunc)
        {
            var students = GetSortedStudents(wantedData, studentsToTake, comparisonFunc);
            foreach (var st in students.OrderBy(x => x.Value).Take(studentsToTake)
                .ToDictionary(pair => pair.Key, pair => pair.Value))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(st.Key, st.Value));
            }
        }
    }
}