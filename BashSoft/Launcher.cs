namespace BashSoft
{
    internal class Launcher
    {
        private static void Main()
        {
            //IOManager.ChangeCurrentDirectoryAbsolute(@"C:\Windows");
            //IOManager.TraverseDirectory(0);

            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCOurse("Unity");

            IOManager.CreateDirectoryInCurrentFolder("*2");
        }
    }
}
