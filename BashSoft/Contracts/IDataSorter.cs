using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IDataSorter
    {
        void OrderAndTake(IDictionary<string, double> wantedData, string comparison, int studentsToTake);
    }
}