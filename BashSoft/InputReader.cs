using System;

namespace BashSoft
{
    public class InputReader
    {
        private const string EndCommand = "quit";
        private CommandInterpreter interpreter;

        public InputReader(CommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.CurrentPath}>");

            string input;
            while ((input = Console.ReadLine()) != EndCommand)
            {
                if (string.IsNullOrWhiteSpace(input)) continue;
                input = input.Trim();

                interpreter.InterpredCommand(input);
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}>");
            }
        }
    }
}