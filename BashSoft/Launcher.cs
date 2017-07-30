using BashSoft.Contracts;
using BashSoft.Core;
using BashSoft.IO;
using BashSoft.Repository;
using BashSoft.Test;

namespace BashSoft
{
    internal class Launcher
    {
        private static void Main()
        {
            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositorySorter(), new RepositoryFilter());
            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}