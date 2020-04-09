using NUnit.Framework;
using Unity.Build;
using UnityEditor;

namespace Unity.Platforms.Windows.Build.Tests
{
    class ClassicWindowsNonIncrementalPipelineTests
    {
#if UNITY_EDITOR_WIN
        const string k_PackagePath = "Packages/com.unity.platforms.windows/";
        const string k_TestPath = "Tests/Editor/Unity.Platforms.Windows.Build.Tests/";
        const string k_BuildConfigurationPath = k_PackagePath + k_TestPath + "WindowsClassicBuildConfiguration.buildconfiguration";

        [SetUp]
        public void SetUp()
        {
            BuildArtifacts.Clean();
        }

        [TearDown]
        public void TearDown()
        {
            BuildArtifacts.Clean();
        }

        [Test]
        public void Build()
        {
            var config = AssetDatabase.LoadAssetAtPath<BuildConfiguration>(k_BuildConfigurationPath);
            var result = config.Build();
            if (result.Failed)
                result.LogResult();
            Assert.That(result.Succeeded, Is.True);
        }
#endif
    }
}
