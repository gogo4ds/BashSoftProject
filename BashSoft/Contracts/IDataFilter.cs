﻿namespace BashSoft.Contracts
{
    using System.Collections.Generic;

    public interface IDataFilter
    {
        void FilterAndTake(IDictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake);
    }
}