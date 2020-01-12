using System.Diagnostics;
using System.IO;
using Unity.Platforms.Build;

namespace Unity.Platforms.Windows.Build
{
    public class RunStepWindows : RunStep
    {
        public override bool CanRun(BuildConfiguration settings, out string reason)
        {
            var artifact = BuildArtifacts.GetBuildArtifact<BuildArtifactWindows>(settings);
            if (artifact == null)
            {
                reason = $"Could not retrieve build artifact '{nameof(BuildArtifactWindows)}'.";
                return false;
            }

            if (artifact.OutputTargetFile == null)
            {
                reason = $"{nameof(BuildArtifactWindows.OutputTargetFile)} is null.";
                return false;
            }

            if (!File.Exists(artifact.OutputTargetFile.FullName))
            {
                reason = $"Output target file '{artifact.OutputTargetFile.FullName}' not found.";
                return false;
            }

            reason = null;
            return true;
        }

        public override RunStepResult Start(BuildConfiguration settings)
        {
            var artifact = BuildArtifacts.GetBuildArtifact<BuildArtifactWindows>(settings);
            var process = new Process();
            process.StartInfo.FileName = artifact.OutputTargetFile.FullName;
            process.StartInfo.WorkingDirectory = artifact.OutputTargetFile.Directory?.FullName ?? string.Empty;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = true;

            if (!process.Start())
            {
                return Failure(settings, $"Failed to start process at '{process.StartInfo.FileName}'.");
            }

            return Success(settings, new RunInstanceWindows(process));
        }
    }
}
