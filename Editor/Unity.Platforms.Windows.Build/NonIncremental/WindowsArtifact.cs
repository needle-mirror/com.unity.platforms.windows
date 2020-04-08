using System.IO;
using Unity.Build;

namespace Unity.Platforms.Windows.Build
{
    sealed class WindowsArtifact : IBuildArtifact
    {
        public FileInfo OutputTargetFile;
    }
}
