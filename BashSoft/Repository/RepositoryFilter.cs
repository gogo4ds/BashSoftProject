using System;
using System.Collections.Generic;
using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.StaticData;

namespace BashSoft.Repository
{
    public class RepositoryFilter : IDataFilter
    {
        public void FilterAndTake(IDictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake)
        {
            switch (wantedFilter.ToLower())
            {
                case "excellent":
                    FilterAndTake(studentsWithMarks, ExcellentFilter, studentsToTake);
                    break;

                case "average":
                    FilterAndTake(studentsWithMarks, AverageFilter, studentsToTake);
                    break;

                case "poor":
                    FilterAndTake(studentsWithMarks, PoorFilter, studentsToTake);
                    break;

                default:
                    throw new ArgumentException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private void FilterAndTake(IDictionary<string, double> studentsWithMarks, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counterForPrinted = 0;
            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counterForPrinted++;
                }
            }
        }

        private static bool ExcellentFilter(double mark)
        {
            return mark >= 5.0;
        }

        private static bool AverageFilter(double mark)
        {
            return mark < 5.0 && mark >= 3.5;
        }

        private static bool PoorFilter(double mark)
        {
            return mark < 3.5;
        }
    }
}