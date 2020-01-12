using System.IO;
using Unity.Platforms.Build;

namespace Unity.Platforms.Windows.Build
{
    [BuildStep(Name = "Produce Windows Artifacts", Description = "Producing Windows Artifacts", Category = "Windows Platform")]
    public sealed class BuildStepProduceWindowsArtifacts : BuildStep
    {
        public override BuildStepResult RunBuildStep(BuildContext context)
        {
            var report = context.GetValue<UnityEditor.Build.Reporting.BuildReport>();
            var artifact = context.GetOrCreateValue<BuildArtifactWindows>();
            artifact.OutputTargetFile = new FileInfo(report.summary.outputPath);
            return Success();
        }
    }
}
