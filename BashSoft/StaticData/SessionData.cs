namespace BashSoft.StaticData
{
    using System.IO;

    public static class SessionData
    {
        public static string CurrentPath { get; set; } = Directory.GetCurrentDirectory();
    }
}