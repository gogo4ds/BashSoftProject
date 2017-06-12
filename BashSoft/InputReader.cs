using System;

namespace BashSoft
{
    public static class InputReader
    {
        private const string EndCommand = "quit";
        public static void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.CurrentPath}>");

            string input;
            while ((input = Console.ReadLine()) != EndCommand)
            {
                if (string.IsNullOrWhiteSpace(input)) continue;
                input = input.Trim();

                CommandInterpreter.InterpredCommand(input);
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}>");
            }
        }
    }
}
