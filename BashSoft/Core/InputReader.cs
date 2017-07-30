using System;
using BashSoft.Contracts;
using BashSoft.Utilities;

namespace BashSoft.Core
{
    public class InputReader : IReader
    {
        private const string EndCommand = "quit";
        private IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
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

                interpreter.InterpretCommand(input);
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}>");
            }
        }
    }
}