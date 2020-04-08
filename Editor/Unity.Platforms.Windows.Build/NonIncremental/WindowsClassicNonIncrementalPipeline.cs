using System.IO;
using Unity.Build;
using Unity.Build.Classic;
using Unity.Build.Common;
using Unity.BuildSystem.NativeProgramSupport;
using UnityEditor;

namespace Unity.Platforms.Windows.Build
{
    sealed class WindowsClassicNonIncrementalPipeline : ClassicNonIncrementalPipelineBase
    {
        protected override BuildTarget BuildTarget => BuildTarget.StandaloneWindows64;
        public override Platform Platform => new WindowsPlatform();

        public override BuildStepCollection BuildSteps { get; } = new[]
        {
            typeof(SaveScenesAndAssetsStep),
            typeof(ApplyUnitySettingsStep),
            typeof(SwitchPlatfomStep),
            typeof(BuildPlayerStep),
            typeof(CopyAdditionallyProvidedFilesStep),
            typeof(WindowsProduceArtifactStep)
        };

        protected override BoolResult OnCanRun(RunContext context)
        {
            var artifact = context.GetLastBuildArtifact<WindowsArtifact>();
            if (artifact == null)
            {
                return BoolResult.False($"Could not retrieve build artifact '{nameof(WindowsArtifact)}'.");
            }

            if (artifact.OutputTargetFile == null)
            {
                return BoolResult.False($"{nameof(WindowsArtifact.OutputTargetFile)} is null.");
            }

            if (!File.Exists(artifact.OutputTargetFile.FullName))
            {
                return BoolResult.False($"Output target file '{artifact.OutputTargetFile.FullName}' not found.");
            }

            return BoolResult.True();
        }

        protected override RunResult OnRun(RunContext context)
        {
            return WindowsRunInstance.Create(context);
        }

        protected override void PrepareContext(BuildContext context)
        {
            base.PrepareContext(context);
            var classicData = context.GetValue<ClassicSharedData>();
            classicData.StreamingAssetsDirectory = $"{context.GetOutputBuildDirectory()}/{context.GetComponentOrDefault<GeneralSettings>().ProductName}_Data/StreamingAssets";
        }
    }
}
