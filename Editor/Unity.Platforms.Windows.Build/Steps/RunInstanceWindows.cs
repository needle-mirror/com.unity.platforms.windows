using System.Diagnostics;
using Unity.Platforms.Build;

namespace Unity.Platforms.Windows.Build
{
    public sealed class RunInstanceWindows : IRunInstance
    {
        Process m_Process;

        public bool IsRunning => !m_Process.HasExited;

        public RunInstanceWindows(Process process)
        {
            m_Process = process;
        }

        public void Dispose()
        {
            m_Process.Dispose();
        }
    }
}
