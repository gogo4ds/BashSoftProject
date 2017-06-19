using System;
using System.Collections.Generic;

namespace BashSoft
{
    public static class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter, int studentsToTake)
        {
            switch (wantedFilter.ToLower())
            {
                case "excellent":
                    FilterAndTake(wantedData, ExcellentFilter, studentsToTake);
                    break;
                case "average":
                    FilterAndTake(wantedData, AverageFilter, studentsToTake);
                    break;
                case "poor":
                    FilterAndTake(wantedData, PoorFilter, studentsToTake);
                    break;
                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
                    break;
            }
        }

        private static void FilterAndTake(Dictionary<string, List<int>> wantedData, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counterForPrinted = 0;
            foreach (var usernamePoints in wantedData)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                double averageMark = Average(usernamePoints.Value);
                if (givenFilter(averageMark))
                {
                    OutputWriter.PrintStudent(usernamePoints);
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

        private static double Average(List<int> scoresOnTask)
        {
            double totalScore = 0;
            scoresOnTask.ForEach(s => totalScore+=s);

            var percentageOfAll = totalScore / (scoresOnTask.Count * 100);
            var mark = percentageOfAll * 4 + 2;

            return mark;
        }
    }
}
