using System;
using System.Collections.Generic;

namespace BashSoft
{
    public static class RepositorySorters
    {
        public static void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                OrderAndTake(wantedData, studentsToTake, CompareInOrder);
            }
            else if (comparison == "descending")
            {
                OrderAndTake(wantedData, studentsToTake, CompareDescendingOrder);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private static void OrderAndTake(Dictionary<string, List<int>> wantedData, int studentsToTake, 
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc)
        {
            //TODO:
        }

        private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> studentsWanted, int takeCount, 
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparison)
        {
            var valuesTaken = 0;
            var studentsSorted = new Dictionary<string, List<int>>();
            var nextInOrder = new KeyValuePair<string, List<int>>();
            
            while (valuesTaken < takeCount)
            {
                var isSorted = true;
                foreach (var student in studentsWanted)
                {
                    if (!string.IsNullOrEmpty(nextInOrder.Key))
                    {
                        int comparisonResult = comparison(student, nextInOrder);
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
                    nextInOrder = new KeyValuePair<string, List<int>>();
                }
            }

            return studentsSorted;
        }

        private static int CompareInOrder(KeyValuePair<string, List<int>> firstValue, KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (var m in firstValue.Value)
            {
                totalOfFirstMarks += m;
            }

            int totalOfSecondMarks = 0;
            foreach (var m in secondValue.Value)
            {
                totalOfSecondMarks += m;
            }

            return totalOfSecondMarks.CompareTo(totalOfFirstMarks);
        }

        private static int CompareDescendingOrder(KeyValuePair<string, List<int>> firstValue, KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (var m in firstValue.Value)
            {
                totalOfFirstMarks += m;
            }

            int totalOfSecondMarks = 0;
            foreach (var m in secondValue.Value)
            {
                totalOfSecondMarks += m;
            }

            return totalOfFirstMarks.CompareTo(totalOfSecondMarks);
        }
    }
}
