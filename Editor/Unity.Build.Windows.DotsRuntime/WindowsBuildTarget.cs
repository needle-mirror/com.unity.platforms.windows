using System.Diagnostics;
using System.IO;
using System.Threading;
using Unity.Build;
using Unity.Build.Internals;
using Unity.Build.DotsRuntime;
using Unity.Build.Desktop.DotsRuntime;
using Debug = UnityEngine.Debug;

namespace Unity.Build.Windows.DotsRuntime
{
    public abstract class WindowsBuildTarget : BuildTarget
    {
        public override bool CanBuild => UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WindowsEditor;
        public override string ExecutableExtension => ".exe";
        public override string UnityPlatformName => nameof(UnityEditor.BuildTarget.StandaloneWindows64);
        
        public override bool Run(FileInfo buildTarget)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = buildTarget.FullName;
            startInfo.WorkingDirectory = buildTarget.Directory.FullName;
            
            return new DesktopRun().RunOnThread(startInfo);
        }

        internal override ShellProcessOutput RunTestMode(string exeName, string workingDirPath, int timeout)
        {
            var shellArgs = new ShellProcessArguments
            {
                Executable = $"{workingDirPath}/{exeName}.exe",
                Arguments = new string[] { },
            };

            return DesktopRun.RunTestMode(shellArgs, workingDirPath, timeout);
        }
    }

    class DotNetTinyWindowsBuildTarget : WindowsBuildTarget
    {
#if UNITY_EDITOR_WIN
        protected override bool IsDefaultBuildTarget => true;
#endif

        public override string DisplayName => "Windows .NET - Tiny";
        public override string BeeTargetName => "windows-dotnet";
        public override bool UsesIL2CPP => false;
    }

    class DotNetStandard20WindowsBuildTarget : WindowsBuildTarget
    {
        public override string DisplayName => "Windows .NET - .NET Standard 2.0";

        public override string BeeTargetName => "windows-dotnet-ns20";

        public override bool UsesIL2CPP => false;
    }

    class IL2CPPWindowsBuildTarget : WindowsBuildTarget
    {
        public override string DisplayName => "Windows IL2CPP - Tiny";
        public override string BeeTargetName => "windows-il2cpp";
        public override bool UsesIL2CPP => true;
    }
}
