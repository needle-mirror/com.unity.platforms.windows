using System.IO;
using UnityEditor;
using Unity.Build;
using Unity.Platforms.Build;
using BuildPipeline = Unity.Build.BuildPipeline;
using Unity.Platforms.Desktop.Build;

namespace Unity.Platforms.Windows.Build
{
    public static class MenuItemWindows
    {
        const string kBuildSettingsClassic = "Assets/Create/Build/BuildSettings Windows Classic";
        const string kBuildPipelineClassicAssetPath = "Packages/com.unity.platforms.windows/Editor/Unity.Platforms.Windows.Build/Assets/Windows Classic.buildpipeline";

        [MenuItem(kBuildSettingsClassic, true)]
        static bool CreateNewBuildSettingsAssetValidationClassic()
        {
            return Directory.Exists(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(kBuildSettingsClassic)]
        static void CreateNewBuildSettingsAssetClassic()
        {
            MenuItemDesktopBuildSettings.CreateNewBuildSettingsAssetClassic(kBuildPipelineClassicAssetPath);
        }
    }
}
