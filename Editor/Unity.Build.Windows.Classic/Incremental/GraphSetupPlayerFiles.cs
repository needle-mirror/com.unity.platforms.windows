#if ENABLE_EXPERIMENTAL_INCREMENTAL_PIPELINE
using Bee.Core;
using NiceIO;
using System;
using System.IO;
using Unity.Build;
using Unity.Build.Classic.Private.IncrementalClassicPipeline;
using Unity.Build.Common;

namespace Unity.Build.Windows.Classic
{
    class GraphSetupPlayerFiles : BuildStepBase
    {
        public override Type[] UsedComponents { get; } = { typeof(GeneralSettings) };

        public override BuildResult Run(BuildContext context)
        {
            var classicContext = context.GetValue<IncrementalClassicSharedData>();
            var playerDirectory = classicContext.VariationDirectory;

            var generalSettings = context.GetComponentOrDefault<GeneralSettings>();
            var appName = generalSettings.ProductName;
            var appData = new NPath(appName + "_Data");

            NPath outputBuildDirectory = new NPath(context.GetOutputBuildDirectory()).MakeAbsolute();
            foreach (var file in playerDirectory.Files(true))
            {
                if (file.Parent.FileName == "Managed")
                    continue;
                if (file.FileNameWithoutExtension == "WindowsPlayerHeadless")
                    continue;

                var targetRelativePath = file.RelativeTo(playerDirectory);

                if (file.FileName == "WindowsPlayer.exe")
                {
                    targetRelativePath = appName + ".exe";

                    var artifact = context.GetOrCreateValue<WindowsArtifact>();
                    artifact.OutputTargetFile = new FileInfo(outputBuildDirectory.Combine(targetRelativePath).ToString());
                }

                if (targetRelativePath.ToString().StartsWith("Data/"))
                    targetRelativePath = appData.Combine(targetRelativePath.RelativeTo("Data"));

                CopyTool.Instance().Setup(outputBuildDirectory.Combine(targetRelativePath), file.MakeAbsolute());
            }

            var appInfo = outputBuildDirectory.Combine(appData, "app.info");
            Backend.Current.AddWriteTextAction(appInfo,
                string.Join("\n", new[]
                {
                    generalSettings.CompanyName,
                    generalSettings.ProductName
                }));
            return context.Success();
        }
    }
}
#endif
