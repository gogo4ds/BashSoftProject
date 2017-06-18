using System;
using System.Collections.Generic;

namespace BashSoft
{
    public static class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<string>> wantedData, string wantedFilter, int studentsToTake)
        {
            //TODO:
        }

        private static void FilterAndTake(Dictionary<string, List<string>> wantedData, Predicate<double> givenFilter,
            int studentsToTake)
        {
            //TODO:
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
            var totalScore = 0;
            scoresOnTask.ForEach(s => totalScore+=s);

            var percentageOfAll = totalScore / scoresOnTask.Count * 100;
            var mark = percentageOfAll * 4 + 2;

            return mark;
        }
    }
}
