using System.Diagnostics;
using System.IO;
using Unity.Build;
using UnityEngine;

namespace Unity.Platforms.Windows.Build
{
    public class RunStepWindows : RunStep
    {
        public override bool CanRun(BuildSettings settings, out string reason)
        {
            var artifact = BuildArtifacts.GetBuildArtifact<BuildArtifactsWindows>(settings);
            if (artifact == null)
            {
                reason = $"Could not retrieve build artifact '{nameof(BuildArtifactsWindows)}'.";
                return false;
            }

            if (artifact.OutputTargetFile == null)
            {
                reason = $"{nameof(BuildArtifactsWindows.OutputTargetFile)} is null.";
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

        public override RunStepResult Start(BuildSettings settings)
        {
            var artifact = BuildArtifacts.GetBuildArtifact<BuildArtifactsWindows>(settings);
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
