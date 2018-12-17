using System;
using System.Diagnostics;
using System.IO;

namespace DemoCron.Helpers
{
    internal class PidFileContext : IDisposable
    {
        private readonly string _filename;

        public PidFileContext(string filename)
        {
            _filename = filename;
            CreatePidFileForCurrentProcess(_filename);
        }

        public void Dispose()
        {
            File.Delete(_filename);
        }

        public void CreatePidFileForCurrentProcess(string filename)
        {
            if (PidFileHelper.IsTaskRunning(filename, out var pid))
            {
                throw new InvalidOperationException($"Process is already running (PID {pid}, PID file {filename})");
            }

            using (var currentProcess = Process.GetCurrentProcess())
            {
                File.WriteAllText(filename, currentProcess.Id.ToString());
            }
        }

        public static PidFileContext For(Type taskType)
        {
            return new PidFileContext(PidFileHelper.GetPidFileName(taskType));
        }

    }
}
