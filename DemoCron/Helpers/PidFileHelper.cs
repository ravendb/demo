using System;
using System.Diagnostics;
using System.IO;

namespace DemoCron.Helpers
{
    public class PidFileHelper
    {
        public static string GetPidFileName(Type taskType)
        {
            return $"{taskType.Name}.pid";
        }

        public static bool IsTaskRunning(string pidFilename, out int? processId)
        {
            if (File.Exists(pidFilename) == false)
            {
                processId = null;
                return false;
            }

            var pidText = File.ReadAllText(pidFilename)?.Trim();
            var pid = int.Parse(pidText);
            processId = pid;
            try
            {
                using (var anotherProcess = Process.GetProcessById(pid))
                {
                    return anotherProcess.HasExited == false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsTaskRunning(Type taskType)
        {
            var pidFile = GetPidFileName(taskType);
            return IsTaskRunning(pidFile, out var _);
        }
    }
}
