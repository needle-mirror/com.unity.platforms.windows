using System.IO;
using Unity.Build.Classic;
using Unity.Build.Common;
using Unity.Build.Editor;
using UnityEditor;
using BuildPipeline = Unity.Build.BuildPipeline;

namespace Unity.Platforms.Windows.Build
{
    public static class MenuItemWindows
    {
        const string k_CreateBuildConfigurationAssetClassic = BuildConfigurationMenuItem.k_BuildConfigurationMenu + "Windows Classic Build Configuration";
        const string k_BuildPipelineClassicAssetPath = "Packages/com.unity.platforms.windows/Editor/Unity.Platforms.Windows.Build/Assets/Windows Classic.buildpipeline";

        [MenuItem(k_CreateBuildConfigurationAssetClassic, true)]
        static bool CreateBuildConfigurationAssetClassicValidation()
        {
            return Directory.Exists(AssetDatabase.GetAssetPath(Selection.activeObject));
        }

        [MenuItem(k_CreateBuildConfigurationAssetClassic)]
        static void CreateBuildConfigurationAssetClassic()
        {
            var pipeline = AssetDatabase.LoadAssetAtPath<BuildPipeline>(k_BuildPipelineClassicAssetPath);
            Selection.activeObject = BuildConfigurationMenuItem.CreateAssetInActiveDirectory(
                "WindowsClassic", new GeneralSettings(), new SceneList(), new ClassicBuildProfile { Pipeline = pipeline });
        }
    }
}
