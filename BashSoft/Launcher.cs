namespace BashSoft
{
    internal class Launcher
    {
        private static void Main()
        {
            //IOManager.ChangeCurrentDirectoryAbsolute(@"C:\Users\gogo4_000\Desktop\SoftUni\C# Fundamentals\C# Advanced\BashSoft\BashSoftProject\BashSoft\bin\Debug\gesho");
            //IOManager.TraverseDirectory(0);

            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentsFromCourse("Unity");

            //IOManager.CreateDirectoryInCurrentFolder("*2");
            InputReader.StartReadingCommands();
        }
    }
}
