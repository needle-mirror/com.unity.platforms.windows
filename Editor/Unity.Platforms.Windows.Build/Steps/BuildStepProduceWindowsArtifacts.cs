using System.Diagnostics;
using System.IO;
using Unity.Build;
using UnityEngine;

namespace Unity.Platforms.Windows.Build
{
    [BuildStep(description = k_Description, category = "Classic")]
    public sealed class BuildStepProduceWindowsArtifacts : BuildStep
    {
        const string k_Description = "Produce Windows Artifacts";

        public override string Description => k_Description;

        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            var report = context.GetValue<UnityEditor.Build.Reporting.BuildReport>();
            var artifact = context.GetOrCreateValue<BuildArtifactsWindows>();
            artifact.OutputTargetFile = new FileInfo(report.summary.outputPath);
            return Success();
        }
    }
}
