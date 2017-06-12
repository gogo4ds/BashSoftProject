namespace BashSoft
{
    internal class Launcher
    {
        private static void Main()
        {
            IOManager.TraverseDirectory(2);

            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCOurse("Unity");

            //IOManager.CreateDirectoryInCurrentFolder("pesho");
        }
    }
}
