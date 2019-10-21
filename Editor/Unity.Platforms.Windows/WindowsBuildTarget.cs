using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

namespace Unity.Platforms.Windows
{
    public abstract class WindowsBuildTarget : BuildTarget
    {
        public override string GetExecutableExtension()
        {
            return ".exe";
        }

        public override string GetUnityPlatformName()
        {
            return nameof(UnityEditor.BuildTarget.StandaloneWindows64);
        }

        public override bool Run(FileInfo buildTarget)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = buildTarget.FullName;
            startInfo.WorkingDirectory = buildTarget.Directory.FullName;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            var process = new Process();
            process.StartInfo = startInfo;
            process.OutputDataReceived += (_, args) => Debug.Log(args.Data);
            process.ErrorDataReceived += (_, args) => Debug.LogError(args.Data);

            var success = process.Start();
            if (!success)
                return false;

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return true;
        }

        public override ShellProcessOutput RunTestMode(string exeName, string workingDirPath, int timeout)
        {
            var args = new string[] { };
            var workingDir = new DirectoryInfo(workingDirPath);
            var executable = $"{workingDirPath}/{exeName}.exe";

            var shellArgs = new ShellProcessArgs
            {
                Executable = executable,
                Arguments = args,
                WorkingDirectory = workingDir,
                ThrowOnError = false
            };

            // samples should be killed on timeout
            if (timeout > 0)
            {
                shellArgs.MaxIdleTimeInMilliseconds = timeout;
                shellArgs.MaxIdleKillIsAnError = false;
            }

            return Shell.Run(shellArgs);
        }
    }

    class DotNetWindowsBuildTarget : WindowsBuildTarget
    {
#if UNITY_EDITOR_WIN
        protected override bool IsDefaultBuildTarget => true;
#endif

        public override string GetDisplayName()
        {
            return "Windows .NET";
        }

        public override string GetBeeTargetName()
        {
            return "windows-dotnet";
        }
    }

    class IL2CPPWindowsBuildTarget : WindowsBuildTarget
    {
        public override string GetDisplayName()
        {
            return "Windows IL2CPP";
        }

        public override string GetBeeTargetName()
        {
            return "windows-il2cpp";
        }
    }
}
