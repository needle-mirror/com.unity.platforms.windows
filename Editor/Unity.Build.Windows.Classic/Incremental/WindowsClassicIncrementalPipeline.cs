#if ENABLE_EXPERIMENTAL_INCREMENTAL_PIPELINE
using Bee.Toolchain.VisualStudio;
using NiceIO;
using System;
using System.Linq;
using Bee.Core;
using Bee.NativeProgramSupport;
using Unity.Build;
using Unity.Build.Classic.Private;
using Unity.Build.Classic.Private.IncrementalClassicPipeline;
using Unity.Build.Common;
using UnityEditor;

namespace Unity.Build.Windows.Classic
{
    class WindowsClassicIncrementalPipeline : ClassicIncrementalPipelineBase
    {
        protected override BuildTarget BuildTarget => BuildTarget.StandaloneWindows64;
        public override Platform Platform { get; } = new WindowsPlatform();

        public override BuildStepCollection BuildSteps { get; } = new[]
        {
            typeof(SetupCopiesFromSlimPlayerBuild),
            typeof(GraphCopyDefaultResources),
            typeof(GraphSetupCodeGenerationStep),
            typeof(GraphSetupIl2Cpp),
            typeof(GraphSetupNativePlugins),
            typeof(GraphSetupPlayerFiles),
            typeof(SetupAdditionallyProvidedFiles)
        };

        protected override void PrepareContext(BuildContext context)
        {
            base.PrepareContext(context);

            var classicContext = context.GetValue<IncrementalClassicSharedData>();
            classicContext.DataDeployDirectory = new NPath(context.GetOutputBuildDirectory()).Combine(context.GetComponentOrDefault<GeneralSettings>().ProductName + "_Data").MakeAbsolute();

            var hostToolChain = TypeCache.GetTypesDerivedFrom<ToolChainForHostProvider>().Select(Activator.CreateInstance).Cast<ToolChainForHostProvider>().Select(p => p.Provide()).First(t => t != null);

            var classicData = context.GetValue<ClassicSharedData>();
            classicData.StreamingAssetsDirectory = classicContext.DataDeployDirectory.Combine("StreamingAssets").ToString();

            var variationname = $"win64_{(context.IsDevelopmentBuild() ? "development" : "nondevelopment")}_{(context.UsesIL2CPP() ? "il2cpp" : "mono")}";
            classicContext.VariationDirectory = classicContext.PlayerPackageDirectory.Combine("Variations", variationname).MakeAbsolute();
            classicContext.UnityEngineAssembliesDirectory = classicContext.VariationDirectory.Combine("Data", "Managed");
            classicContext.IL2CPPDataDirectory = classicContext.DataDeployDirectory.Combine("il2cpp_data");
            classicContext.LibraryDeployDirectory = context.GetOutputBuildDirectory();

            classicContext.Architectures.Add(
                Architecture.x64,
                new ClassicBuildArchitectureData()
                {
                    DynamicLibraryDeployDirectory = classicContext.DataDeployDirectory.Combine("Plugins"),
                    IL2CPPLibraryDirectory = context.GetOutputBuildDirectory(),
                    BurstTarget = "x64_SSE4",
                    ToolChain = hostToolChain,
                    NativeProgramFormat = hostToolChain.DynamicLibraryFormat.WithLinkerSetting<MsvcDynamicLinker>(s => s.WithNoDefaultLibs("uuid.lib"))
                }
            );
        }

        protected override RunResult OnRun(RunContext context)
        {
            return WindowsRunInstance.Create(context);
        }
    }
}
#endif
