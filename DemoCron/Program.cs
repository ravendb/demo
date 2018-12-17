using DemoCron.Arguments;
using DemoCron.Helpers;

namespace DemoCron
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup();
            RunTask(args);
        }

        private static void Setup()
        {
            ServiceLocator.Configure();

            ServiceLocator.Resolve<Startup>()
                .Configure();
        }

        private static void RunTask(string[] args)
        {
            var taskType = ArgumentsParser.GetTaskType(args);
            TaskRunner.Run(taskType).GetAwaiter().GetResult();
        }
    }
}
