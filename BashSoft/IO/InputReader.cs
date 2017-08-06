namespace BashSoft.IO
{
    using System;
    using BashSoft.Contracts;
    using BashSoft.StaticData;

    public class InputReader : IReader
    {
        private const string EndCommand = "quit";
        private readonly IInterpreter interpreter;

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
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                input = input.Trim();

                this.interpreter.InterpretCommand(input);
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}>");
            }
        }
    }
}