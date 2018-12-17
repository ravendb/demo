using System.Threading.Tasks;

namespace DemoCron.Tasks
{
    internal abstract class CliTask
    {
        public abstract Task Run();
    }
}
