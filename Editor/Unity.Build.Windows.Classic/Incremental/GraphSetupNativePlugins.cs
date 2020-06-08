#if ENABLE_EXPERIMENTAL_INCREMENTAL_PIPELINE
using Bee.Core;
using NiceIO;
using System.IO;
using System.Linq;
using Unity.Build;
using Unity.Build.Classic.Private.IncrementalClassicPipeline;
using Unity.BuildSystem.NativeProgramSupport;
using UnityEditor;

namespace Unity.Build.Windows.Classic
{
    class GraphSetupNativePlugins : BuildStepBase
    {
        public override BuildResult Run(BuildContext context)
        {
            var classicContext = context.GetValue<IncrementalClassicSharedData>();
            var buildTarget = classicContext.BuildTarget;

            var nativePlugins = PluginImporter.GetImporters(buildTarget).Where(m => m.isNativePlugin);
            foreach (var p in nativePlugins)
            {
                string cpu = p.GetPlatformData(buildTarget, "CPU");
                switch (cpu)
                {
                    case "x86_64":
                    case "x86":
                    case "":
                        // TODO: fix me
                        CopyTool.Instance().Setup(classicContext.Architectures[Architecture.x64].DynamicLibraryDeployDirectory.Combine(cpu, Path.GetFileName(p.assetPath)), new NPath(Path.GetFullPath(p.assetPath)).MakeAbsolute());
                        break;
                    // This is a special case for CPU targets, means no valid CPU is selected
                    case "None":
                        continue;
                }
            }

            return context.Success();
        }
    }
}
#endif
