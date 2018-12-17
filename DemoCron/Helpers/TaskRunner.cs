using System;
using System.Threading.Tasks;
using DemoCron.Arguments;
using DemoCron.Tasks;

namespace DemoCron.Helpers
{
    internal static class TaskRunner
    {
        public static Task Run(TaskType taskType)
        {
            var type = GetTaskType(taskType);
            var pidFilename = PidFileHelper.GetPidFileName(type);
            using (var pidFile = new PidFileContext(pidFilename))
            {
                var task = (CliTask)ServiceLocator.Resolve(type);
                return task.Run();
            }
        }

        private static Type GetTaskType(TaskType taskType)
        {
            switch (taskType)
            {
                case TaskType.DeleteUnusedDatabases:
                    return typeof(DeleteUnusedDatabasesTask);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
