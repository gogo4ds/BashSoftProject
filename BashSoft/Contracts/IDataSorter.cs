namespace BashSoft.Contracts
{
    using System.Collections.Generic;

    public interface IDataSorter
    {
        void OrderAndTake(IDictionary<string, double> wantedData, string comparison, int studentsToTake);
    }
}