using System;
using System.ComponentModel;

namespace DemoCron.Arguments
{
    internal class ArgumentsParser
    {
        private const TaskType DefaultTaskType = TaskType.DeleteUnusedDatabases;

        public static TaskType GetTaskType(string[] args)
        {
            if (args == null || args.Length == 0)
                return DefaultTaskType;

            if (Enum.TryParse(typeof(TaskType), args[0], out var result))
                return (TaskType)result;

            throw new InvalidEnumArgumentException(
                $"Allowed arguments are: {string.Join(", ", Enum.GetNames(typeof(TaskType)))}");
        }
    }
}
